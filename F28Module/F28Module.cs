using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using VCSharpF28LightControlDemo;
namespace F28Module
{
    public class F28Module
    {
        private F28LightCtrl _f28ctl;
        private short sModule = -1;
        private F28LightCtrl.F28_REALTIME_CYCLE tResult;
        private AutoCal tAutocal = new AutoCal();
        private F28LightCtrl.F28_PARAMETERS tParams = new F28LightCtrl.F28_PARAMETERS();
        private F28LightCtrl.F28_RESULT tLastResult = new F28LightCtrl.F28_RESULT();
        public event EventHandler<F28LightCtrl.F28_REALTIME_CYCLE> OnRealTimeUpdated;
        public short ModuleID
        {
            private set;
            get;
        }
        public IPAddress ipAddress
        {
            get;
            private set;
        }
        public string hwVersion
        {
            get;
            private set;
        }
        public string swVersion
        {
            get;
            private set;
        }
        public string serialNumber
        {
            get;
            private set;
        }
        public F28Module(short moduleID,F28LightCtrl controller)
        {
            ModuleID = moduleID;
            sModule = moduleID;
            _f28ctl = controller;
            tResult = new F28LightCtrl.F28_REALTIME_CYCLE();
            tAutocal.SetF28(ref _f28ctl);
            updateInfo();
        }
        public void GetRealTimeData()
        {
            //const string EMPTY_FIELD = "------";
            byte m_bEndCycle = 0;
            if (sModule != -1)
            {
                byte ucPhaseCal = tAutocal.GetPhase();
                string str;

                // 1- Run Auto Calibration Offset + Volume
                if (tAutocal.IsCalRunning())       // (ucPhaseCal != AutoCal.CAL_AUTO_PHASES.AUTO_IDDLE)
                {
                    // Display phase
                    str = tAutocal.GetPhaseStr(ucPhaseCal);
                    //m_labelCal.Text = str;

                    // Waiting Master leak
                    if (tAutocal.IsWaitingMasterLeak())    //(ucPhaseCal == AutoCal.CAL_AUTO_PHASES.AUTO_WAIT_MASTER_LEAK)
                    {
                        // timerRealTime.Stop();

                        string message = "Put master leak\n\nOK to Continue, Cancel to abort ? ";
                        string caption = "Master leak";

                        MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                        DialogResult result;

                        // Displays the MessageBox.
                        result = MessageBox.Show(message, caption, buttons);

                        if (result == System.Windows.Forms.DialogResult.OK)
                        {
                            tAutocal.RunCalContinue(true);
                        }
                        else
                        {
                            tAutocal.RunCalContinue(false);
                        }

                        ucPhaseCal = tAutocal.GetPhase();

                        str = tAutocal.GetPhaseStr(ucPhaseCal);

                        //m_labelCal.Text = str;

                        //timerRealTime.Start();
                    }

                    // Running End auto calibration ?
                    if (tAutocal.RunCal())
                    {
                        // ' 1- Read auto calibration errcode
                        if (tAutocal.GetCalAlarm() > 0)
                        {
                            str = "Calibration Alarm !!!";
                            //AddMessage(str);
                            //m_labelCal.Text = str;
                        }
                        else
                        {
                            //' 2- if no erreur -> Read & Save parameters
                            //AddMessage("F28_GetModuleParameters !!!");

                            if (_f28ctl.GetModuleParameters(sModule, ref tParams) == F28LightCtrl.F28_OK)
                            {
                                str = "Offset = " + tParams.fOffsetLeak.ToString("F4") + " - Volume = " + tParams.fVolume.ToString("F2");
                            }
                            else
                            {
                                str = "Read parameters error !!!";
                            }

                            //AddMessage(str);
                            //m_labelCal.Text = str;
                        }
                    }
                }


                // 2- Real time measurement

                F28LightCtrl.F28_REALTIME_CYCLE tResult = new F28LightCtrl.F28_REALTIME_CYCLE();

                if (_f28ctl.GetRealTimeData(sModule, ref tResult) == F28LightCtrl.F28_OK)
                {

                    // 1.403 :NEW:(1)
                    if (tAutocal.IsRunningVolumeCal())
                    {
                        tResult.fLeakValue *= 1000.0f;
                        tResult.bUnitLeak = (byte)F28LightCtrl.F28_LEAK_UNITS.LEAK_PASEC;
                    }

                    // Check end of cycle
                    if (tResult.bEndCycle == 1)
                    {
                        if (m_bEndCycle == 0)
                        {
                            if (_f28ctl.GetLastResult(sModule, ref tLastResult) == F28LightCtrl.F28_OK)
                            {
                                // 1.403 :NEW:(2)
                                if (tAutocal.IsRunningVolumeCal())
                                {
                                    tLastResult.fLeakValue *= 1000.0f;
                                    tLastResult.bUnitLeak = (byte)F28LightCtrl.F28_LEAK_UNITS.LEAK_PASEC;
                                }

                                //DisplayResultFrame(ref tLastResult);

                                //DisplayResultCount();
                            }
                        }

                        tResult.fPressureValue = tLastResult.fPressureValue;
                        tResult.bUnitPressure = tLastResult.bUnitPressure;
                        tResult.fLeakValue = tLastResult.fLeakValue;
                        tResult.bUnitLeak = tLastResult.bUnitLeak;
                    }

                    m_bEndCycle = tResult.bEndCycle;

                    OnRealTimeUpdated?.Invoke(this, tResult);
                    // Display pressure
                    //Pressure.Text = tResult.fPressureValue.ToString("F2");
                    //if (tResult.bUnitPressure < (byte)F28LightCtrl.F28_PRESS_UNITS.NMAX_PRESS_UNITS)
                    //{
                    //    PressureUnit.Text = F28.F28_STR_PRESS_UNITS[tResult.bUnitPressure];
                    //}
                    //else
                    //{
                    //    LeakUnit.Text = EMPTY_FIELD;
                    //}

                    // Leak
                    //    if (tResult.bUnitLeak == (byte)F28LightCtrl.F28_LEAK_UNITS.LEAK_SCCM)
                    //    {
                    //        Leak.Text = tResult.fLeakValue.ToString("F4");
                    //    }
                    //    else
                    //    {
                    //        Leak.Text = tResult.fLeakValue.ToString("F2");
                    //    }

                    //    if (tResult.bUnitLeak < (byte)F28LightCtrl.F28_LEAK_UNITS.NMAX_LEAK_UNITS)
                    //    {
                    //        LeakUnit.Text = F28.F28_STR_LEAK_UNITS[tResult.bUnitLeak];
                    //    }
                    //    // V2.0.0.4
                    //    else if (tResult.bUnitLeak == (byte)F28LightCtrl.F28_LEAK_UNITS.LEAK_JET_CHECK)
                    //    {
                    //        LeakUnit.Text = F28.F28_STR_LEAK_JET_CHECK;
                    //    }
                    //    // V2.0.0.4
                    //    else
                    //    {
                    //        LeakUnit.Text = EMPTY_FIELD;
                    //    }


                    //    // Status back color
                    //    if (tResult.bStatus > (byte)F28LightCtrl.F28_STATUS.F28_STATUS_READY)
                    //    {
                    //        Status.BackColor = Color.FromArgb(0, 128, 255);
                    //    }
                    //    else
                    //    {
                    //        // V2.0.0.4
                    //        if (tLastResult.bStatus == (byte)F28LightCtrl.F28_RESULT_STATUS_CODE.STATUS_GOOD_PART ||
                    //            tLastResult.bStatus == (byte)F28LightCtrl.F28_RESULT_STATUS_CODE.STATUS_ALARM_JET_CHECK_PASS)
                    //        {
                    //            Status.BackColor = Color.FromArgb(0, 128, 0);
                    //        }
                    //        else if (tLastResult.bStatus == (byte)F28LightCtrl.F28_RESULT_STATUS_CODE.STATUS_REF_FAIL_PART || tLastResult.bStatus == (byte)F28LightCtrl.F28_RESULT_STATUS_CODE.STATUS_TEST_FAIL_PART)
                    //        {
                    //            Status.BackColor = Color.FromArgb(200, 0, 0);
                    //        }
                    //        else
                    //        {
                    //            Status.BackColor = Color.FromArgb(245, 100, 10);
                    //        }
                    //    }


                    //    Status.Text = "<Eoc:" + tResult.bEndCycle.ToString() + "> " + tResult.bStatus.ToString() + "->";

                    //    if (tResult.bStatus != 0xFF)
                    //    {
                    //        if (tResult.bStatus <= (byte)F28LightCtrl.F28_STATUS.F28_STATUS_MAX)
                    //        {
                    //            Status.Text += F28.F28_STR_STATUS[tResult.bStatus];
                    //        }
                    //        else
                    //        {
                    //            Status.Text += EMPTY_FIELD;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        Status.Text += EMPTY_FIELD;
                    //    }

                    //    PAtm.Text = tResult.fPatm.ToString("F2");
                    //    Temperature.Text = tResult.fInternalTemperature.ToString("F2");
                    //}
                    //else
                    //{
                    //    Pressure.Text = EMPTY_FIELD;
                    //    PressureUnit.Text = EMPTY_FIELD;
                    //    Leak.Text = EMPTY_FIELD;
                    //    LeakUnit.Text = EMPTY_FIELD;
                    //    Status.Text = EMPTY_FIELD;
                    //    PAtm.Text = EMPTY_FIELD;
                    //    Temperature.Text = EMPTY_FIELD;
                    //}
                }
            }
        }
        private void updateInfo()
        {
            if (_f28ctl.RefreshModuleInformations(sModule) == F28LightCtrl.F28_OK)
            {
                uint ulIP = 0;                  // 1.504
                string strMACAddress = "";
                string strVersion = "";

                if (_f28ctl.GetAddressIP(sModule, ref ulIP) == F28LightCtrl.F28_OK && _f28ctl.GetMACAddress(sModule, ref strMACAddress) == F28LightCtrl.F28_OK)
                {
                    IPAddress IPAddress = new IPAddress((long)ulIP);

                    ipAddress = IPAddress;

                }

                if (_f28ctl.GetModuleHardVersion(sModule, ref strVersion) == F28LightCtrl.F28_OK)
                {
                    hwVersion = strVersion;
                }
                else
                {
                    hwVersion = "Sensor hard version unknown";
                }

                if (_f28ctl.GetModuleSoftVersion(sModule, ref strVersion) == F28LightCtrl.F28_OK)
                {
                    swVersion = strVersion;
                }
                else
                {
                    swVersion = "Software version unknown";
                }

                if (_f28ctl.GetETHHardVersion(sModule, ref strVersion) == F28LightCtrl.F28_OK)
                {
                    
                }
                else
                {
                    
                }

                if (_f28ctl.GetETHSoftVersion(sModule, ref strVersion) == F28LightCtrl.F28_OK)
                {
                   
                }
                else
                {
                    
                }

                if (_f28ctl.GetSerialNumber(sModule, ref strVersion) == F28LightCtrl.F28_OK)
                {
                    serialNumber = strVersion;
                }
                else
                {
                    serialNumber = "????????";
                }
            }
            else
            {
                throw (new Exception("Unable to read F28 module information"));
            }
        }
        public override string ToString()
        {
            return sModule.ToString();
        }
    }
   

}
