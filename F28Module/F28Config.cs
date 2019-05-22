using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Reflection;
using VCSharpF28LightControlDemo;

namespace F28Module
{
    public class F28Config
    {
        XDocument xdoc;
        F28ModuleAPI _controller;
        string fullPathFileName;
        string[] paramNames = {"usTypeTest","usTpsFillVol","usTpsTransfert",
        "usTpsFill",
        "usTpsStab",
        "usTpsTest",
        "usTpsDump",
        "usPress1Unit",
        "fPress1Min",
        "fPress1Max",
        "fSetFillP1",
        "fRatioMax",
        "fRatioMin",
        "usFillMode",  
        "usLeakUnit",
        "usRejectCalc",
        "usVolumeUnit",  
        "fVolume",
        "fRejectMin",
        "fRejectMax",
        "fCoeffAutoFill",
        "usOptions",      
        "fPatmSTD",
        "fTempSTD",
        "fFilterTime",
        "fOffsetLeak"
    };
        public F28Config(F28ModuleAPI owner)
        {
            _controller = owner;
        }
        public void loadXML(string xmlFile)
        {
            xdoc = XDocument.Load(xmlFile);

            if(xdoc == null)
            {
                throw (new Exception("Unable to load XML file: " + xmlFile));
            }
            fullPathFileName = xmlFile;
            var sensorNode = xdoc.Root.Elements("Sensors");
            foreach(XElement el in sensorNode.Descendants("Sensor"))
            {
                string ip = el.Attribute("ipAddress").Value;
                byte groupID = byte.Parse(el.Attribute("groupid").Value);
                byte timeOut = byte.Parse(el.Attribute("timeout").Value);
                _controller.addModule(ip, 1, groupID, timeOut);
            }
        }
        public void loadParameterFromXML(string xmlFile)
        {
            xdoc = XDocument.Load(xmlFile);

            if (xdoc == null)
            {
                throw (new Exception("Unable to load XML file: " + xmlFile));
            }
            var nodes = xdoc.Descendants("Settings");
            foreach(XElement n in nodes)
            {
                string profileName = n.Attribute("name").Value;
                F28LightCtrl.F28_PARAMETERS tParams = new F28LightCtrl.F28_PARAMETERS();
                var paramNode = n.Descendants("Parameter");
                object tP = tParams;
                foreach (XElement el in paramNode)
                {
                    string probName = el.Attribute("property_name").Value;
                    string typeStr = el.Attribute("type").Value;
                    string strVal = el.Attribute("value").Value;
                    var field = tParams.GetType().GetField(probName);
                    
                    if(typeStr== "ushort")
                    {
                        ushort uValue = ushort.Parse(strVal);
                        field.SetValue(tP, uValue);
                    } else
                    {
                        float fValue = float.Parse(strVal);
                        field.SetValue(tP, fValue);
                    }
                }
                tParams = (F28LightCtrl.F28_PARAMETERS)tP;
                _controller.addParamToTable(profileName, tParams);
            }
        }
        public void saveXML(string xmlFile,F28LightCtrl.F28_PARAMETERS testParam,string profileName)
        {
            xdoc = XDocument.Load(xmlFile);
            var f28Node = xdoc.Descendants("F28Config").FirstOrDefault();
            var settingNode = xdoc.Descendants("Settings").Where(g => g.Attribute("name").Value == profileName);
            if(settingNode.FirstOrDefault() == null )
            {
                //XElement el = new XElement("Settings");
                //XAttribute at = new XAttribute("name", profileName);
                //el.Add(at);
                //XElement p1 = new XElement("Parameter");
                //XAttribute p = new XAttribute("property_name", "bah bah");
                //p1.Add(p);
                //el.Add(p1);
                //f28Node.Add(el);
                XElement el = saveTestParamsToFile(testParam, profileName);
                f28Node.Add(el);
                xdoc.Save(xmlFile);
            }
           
        }
        public XElement saveTestParamsToFile(F28LightCtrl.F28_PARAMETERS testParam, string profileName)
        {
            XElement settingNode = new XElement("Settings");
            XAttribute name = new XAttribute("name", profileName);
            settingNode.Add(name);
            foreach(string probName in paramNames)
            {
                XElement paramNode = new XElement("Parameter");
                var field = testParam.GetType().GetField(probName);
                object val = field.GetValue(testParam);
                XAttribute at_name = new XAttribute("property_name", probName);
                paramNode.Add(at_name);
                XAttribute at_val = new XAttribute("value", val);
                paramNode.Add(at_val);
                XAttribute at_type;
                
                if(field.FieldType.FullName == "System.UInt16")
                {
                    at_type = new XAttribute("type", "ushort");
                }else
                {
                    at_type = new XAttribute("type", "float");
                }
                paramNode.Add(at_type);
                settingNode.Add(paramNode);
            }

            return settingNode;
        }
    }
}
