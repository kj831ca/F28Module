using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VCSharpF28LightControlDemo;
using System.Timers;
using BaseModule;

namespace F28Module
{
    public class F28ViewFeeder:IGUIView
    {
        F28MainView _mainView;
        F28ModuleAPI _api;
        //System.Timers.Timer realTimeTimers;
        F28Module _selectModule;
        public F28ViewFeeder(F28ModuleAPI api)
        {
            _api = api;
            _mainView = new F28MainView();
            _api.OnNewModuleAdded += _api_OnNewModuleAdded;
            subscribeToView();
            //realTimeTimers = new System.Timers.Timer();
            //realTimeTimers.Interval = 100;
            //realTimeTimers.Elapsed += RealTimeTimers_Elapsed;
            //realTimeTimers.Start();
        }

        private void subscribeToView()
        {
            _mainView.listBoxModule.SelectedIndexChanged += ListBoxModule_SelectedIndexChanged;
            _mainView.buttonSaveParam.Click += ButtonSaveParam_Click;
            _mainView.buttonStartCycle.Click += ButtonStartCycle_Click;
        }
        public F28Module SelectModule
        {
            get { return _selectModule; }
        }
        private void ButtonStartCycle_Click(object sender, EventArgs e)
        {
            if (_selectModule != null)
            {
                _api.startCycle(_selectModule);
            }
        }

        private void ButtonSaveParam_Click(object sender, EventArgs e)
        {
            F28LightCtrl.F28_PARAMETERS testParam = new F28LightCtrl.F28_PARAMETERS();
            if (_api.readParameter(_selectModule.ModuleID, ref testParam))
            {
                _api.SaveParam("mynewParam", testParam);
            }
        }

        private void ListBoxModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectModule = (F28Module)_mainView.listBoxModule.SelectedItem;
        }

        private void RealTimeTimers_Elapsed(object sender, ElapsedEventArgs e)
        {
            if(_selectModule != null)
            {
                _selectModule.GetRealTimeData();
            }
        }

        public F28MainView View
        {
            get
            {
                return _mainView;
            }

        }

        

        private void _api_OnNewModuleAdded(object sender, F28Module e)
        {
            _mainView.listBoxModule.Items.Add(e);
            e.OnRealTimeUpdated += E_OnRealTimeUpdated;
            _mainView.listBoxModule.SelectedIndex = 0;
        }
        private void updateRealTimeInfo(F28LightCtrl.F28_REALTIME_CYCLE tResult)
        {
            //Display pressure
            updateLable(_mainView.labelPressure, tResult.fPressureValue.ToString("F2"));
            if (tResult.bUnitPressure < (byte)F28LightCtrl.F28_PRESS_UNITS.NMAX_PRESS_UNITS)
            {
                updateLable(_mainView.PressureUnit, _api.F28.F28_STR_PRESS_UNITS[tResult.bUnitPressure]);
            }
            else
            {
                updateLable(_mainView.PressureUnit, "----");
            }
            // Leak
            if (tResult.bUnitLeak == (byte)F28LightCtrl.F28_LEAK_UNITS.LEAK_SCCM)
            {
                updateLable(_mainView.Leak, tResult.fLeakValue.ToString("F4"));
            }
            else
            {
                updateLable(_mainView.Leak, tResult.fLeakValue.ToString("F2"));
            }

            if (tResult.bUnitLeak < (byte)F28LightCtrl.F28_LEAK_UNITS.NMAX_LEAK_UNITS)
            {
                //LeakUnit.Text = F28.F28_STR_LEAK_UNITS[tResult.bUnitLeak];
                updateLable(_mainView.LeakUnit, _api.F28.F28_STR_LEAK_UNITS[tResult.bUnitLeak]);
            }
            // V2.0.0.4
            else if (tResult.bUnitLeak == (byte)F28LightCtrl.F28_LEAK_UNITS.LEAK_JET_CHECK)
            {
                //LeakUnit.Text = F28.F28_STR_LEAK_JET_CHECK;
                updateLable(_mainView.LeakUnit, _api.F28.F28_STR_LEAK_JET_CHECK);
            }
            else
            {
                updateLable(_mainView.LeakUnit, "----");
            }

            string status = "<Eoc:" + tResult.bEndCycle.ToString() + "> " + tResult.bStatus.ToString() + "->";
            updateLable(_mainView.Status, status);
            updateLable(_mainView.Temperature, tResult.fInternalTemperature.ToString("F2"));
            updateLable(_mainView.PAtm, tResult.fPatm.ToString("F2"));
        }
            // V2.0.0.4
            
        

        private void E_OnRealTimeUpdated(object sender, F28LightCtrl.F28_REALTIME_CYCLE e)
        {
            updateRealTimeInfo(e);
        }

        #region GUI_UPDATE
        private delegate void UpdateLable(Label lbl, string txt);
        private void updateLable(Label lbl,string txt)
        {
            if(lbl.InvokeRequired)
            {
                _mainView.Invoke(new UpdateLable(updateLable), new object[] { lbl, txt });
                return;
            }
            lbl.Text = txt;
        }

        UserControl IGUIView.View()
        {
            return _mainView;
        }
        public int SecurityLevel
        { get; set; }
        public void Refresh()
        {
            
        }

        public void CloseView()
        {
            
        }
        #endregion
    }
}
