using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using VCSharpF28LightControlDemo;
using System.Timers;
using BaseModule;

namespace F28Module
{
    public class F28ModuleAPI:SystemResponsePublisher
    {
        F28LightCtrl _F28;
        List<string> errorMessages;
        List<F28Module> moduleList;
        Dictionary<string, F28LightCtrl.F28_PARAMETERS> paramTable;
        static F28ModuleAPI _instance;
        F28Config _config;
        F28configViewFeeder _configView;
        public event EventHandler<F28Module> OnNewModuleAdded;
        public event EventHandler<F28LightCtrl.F28_PARAMETERS> OnParameterLoad;
        F28ViewFeeder _viewFeeder;
        Timer realTimeTimer;
        bool isStartCycle = false;
        static object lockInstance = new object();
        public event EventHandler<F28LightCtrl.F28_REALTIME_CYCLE> OnRealTimeCycleUpdate;
        public static F28ModuleAPI instance()
        {
            lock (lockInstance)
            {
                if (_instance == null)
                {
                    _instance = new F28ModuleAPI();

                }
                return _instance;
            }
        }
        public IGUIView MainView
        {
            get { return _viewFeeder; }
        }
        public IGUIView ConfigView
        {
            get { return _configView;  }
        }
        public bool startCycle(F28Module module)
        {
            return (F28.StartCycle(module.ModuleID) == F28LightCtrl.F28_OK);
        }
        public bool startCycle()
        {
            F28Module module = _viewFeeder.SelectModule;
            return (F28.StartCycle(module.ModuleID) == F28LightCtrl.F28_OK);
        }
        public bool stopCycle()
        {
            F28Module module = _viewFeeder.SelectModule;
            return (F28.StopCycle(module.ModuleID) == F28LightCtrl.F28_OK);
        }
        private F28ModuleAPI()
        {
            _F28 = new F28LightCtrl();
            errorMessages = new List<string>();
            moduleList = new List<F28Module>();
            paramTable = new Dictionary<string, F28LightCtrl.F28_PARAMETERS>();
            _viewFeeder = new F28ViewFeeder(this);
            _configView = new F28configViewFeeder(this);
            init();
            _config = new F28Config(this);
            _config.loadXML("f28config.xml");
            _config.loadParameterFromXML("f28config.xml");
            realTimeTimer = new Timer();
            realTimeTimer.Interval = 100;
            realTimeTimer.Elapsed += RealTimeTimer_Elapsed;
           
        }
        public void startRealTimeUpdate()
        {
            if(_viewFeeder.SelectModule != null)
            {
                _viewFeeder.SelectModule.OnRealTimeUpdated += SelectModule_OnRealTimeUpdated;
            }
            realTimeTimer.Start();
        }
        
        private void SelectModule_OnRealTimeUpdated(object sender, F28LightCtrl.F28_REALTIME_CYCLE e)
        {
            OnRealTimeCycleUpdate?.Invoke(sender, e);
            if (e.bEndCycle == 0 && !isStartCycle)
            {
                isStartCycle = true;
            }
            if(isStartCycle && e.bEndCycle == 1)
            {
                isStartCycle = false;
                StateEventArgs doneArgs = new StateEventArgs("LeakCycleComplete", "");
                notifyDoneEventSubscribers(this, doneArgs);
            }

        }
        public F28LightCtrl.F28_RESULT GetLastResult()
        {
            F28LightCtrl.F28_RESULT tLastResult = new F28LightCtrl.F28_RESULT();
            if (_viewFeeder.SelectModule != null)
            {
                if (F28.GetLastResult(_viewFeeder.SelectModule.ModuleID, ref tLastResult) == F28LightCtrl.F28_OK)
                    return tLastResult;
            }
            return tLastResult;
        }

        private void RealTimeTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if(_viewFeeder.SelectModule != null)
            {
                _viewFeeder.SelectModule.GetRealTimeData();
            }
        }

        public F28MainView GetMainView()
        {
            return _viewFeeder.View;
        }
        public F28configViewFeeder ConfigViewFeeder
        {
            get { return _configView; }
        }
        public F28ParamView GetParamView()
        {
            return _configView.configView;
        }
        public bool isPortOpen
        {
            get;
            private set;
        }
        public void init()
        {
            _F28.Init();
            if (_F28.OpenChannel() == F28LightCtrl.F28_OK)
            {
                isPortOpen = true;
                //realTimeTimer.Start();
            }
            else
            {
                isPortOpen = false;
                errorMessages.Add("Unable to open the port !");
            }
        }
        public void addParamToTable(string profileName,F28LightCtrl.F28_PARAMETERS tParam)
        {
            if(!paramTable.ContainsKey(profileName))
            {
                paramTable.Add(profileName, tParam);
                OnParameterLoad?.Invoke(profileName, tParam);
            }
        }
        public void writeParamToF28(short moduleID,string name)
        {
            if(paramTable.ContainsKey(name))
            {
                F28LightCtrl.F28_PARAMETERS tParam = paramTable[name];
                F28.SetModuleParameters(moduleID, ref tParam);
            }
        }
        /// <summary>
        /// Write parameter into the default module
        /// </summary>
        /// <param name="name"></param>
        public void writeParamToF28(string name)
        {
            F28Module defaultModule = moduleList[0];
            if(defaultModule != null)
            {
                writeParamToF28(defaultModule.ModuleID, name);
            }
        }
        public F28LightCtrl F28
        {
            get { return _F28; }
        }
        public bool addModule(string address, byte moduleAddress, byte groupID, byte timeOut)
        {
            IPAddress IPAddress = IPAddress.Parse(address);
            uint ulAddress = BitConverter.ToUInt32(IPAddress.GetAddressBytes(), 0);
            short sModule = _F28.AddModule(ulAddress, moduleAddress, groupID, timeOut);
            if(sModule != F28LightCtrl.F28_FAIL)
            {
                F28Module f28Module = new F28Module(sModule, F28);
                moduleList.Add(f28Module);
                OnNewModuleAdded?.Invoke(this, f28Module);
                return true;
            }
            return false;
        }
        public bool readParameter(short moduleID, ref F28LightCtrl.F28_PARAMETERS tParam)
        {
            if (_F28.GetModuleParameters(moduleID, ref tParam) == F28LightCtrl.F28_OK)
            {
                return true;
            }
                return false;
        }
        public void SaveParam(string name,F28LightCtrl.F28_PARAMETERS testParam)
        {
            _config.saveXML("f28config.xml", testParam, name);
        }


    }
}
