using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCSharpF28LightControlDemo;
using BaseModule;
using System.Windows.Forms;

namespace F28Module
{
    class ParameterSet
    {
        public string name;
        public F28LightCtrl.F28_PARAMETERS parameters;
        public ParameterSet(string n, F28LightCtrl.F28_PARAMETERS tParam)
        {
            name = n;
            parameters = tParam;
        }
        public override string ToString()
        {
            return name;
        }
    }
    public class F28configViewFeeder:IGUIView
    {
        F28ParamView _view;
        F28ModuleAPI _api;
        public F28configViewFeeder(F28ModuleAPI api)
        {
            _api = api;
            _view = new F28ParamView();
            _api.OnParameterLoad += _api_OnParameterLoad;
            subscribeToView();
        }
        public F28ParamView configView
        {
            get { return _view; }
        }

        public int SecurityLevel
        { get; set; }

        private void _api_OnParameterLoad(object sender, F28LightCtrl.F28_PARAMETERS e)
        {
            ParameterSet myParamSet = new ParameterSet((string)sender, e);
            _view.comboBoxProfileList.Items.Add(new ParameterSet((string)sender, e));
            
        }

        private void subscribeToView()
        {
            _view.comboBoxProfileList.SelectedIndexChanged += ComboBoxProfileList_SelectedIndexChanged;
            _view.buttonWrite.Click += ButtonWrite_Click;
        }

        private void ButtonWrite_Click(object sender, EventArgs e)
        {
            ParameterSet pSet = (ParameterSet)_view.comboBoxProfileList.SelectedItem;
            if(pSet != null)
                _api.writeParamToF28(pSet.name);
        }

        private void ComboBoxProfileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ParameterSet pSet = (ParameterSet)_view.comboBoxProfileList.SelectedItem;
            _view.formLoad(pSet.parameters);
        }

        public UserControl View()
        {
            return _view;
        }

        public void Refresh()
        {
            if(_view.comboBoxProfileList.SelectedIndex == -1)
            {
                _view.comboBoxProfileList.SelectedIndex = 0;
            }
        }

        public void CloseView()
        {
            
        }
    }
}
