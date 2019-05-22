using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VCSharpF28LightControlDemo;

namespace F28Module
{
    public partial class F28ParamView : UserControl
    {
        // 1.500 Uses with "wOptions" parameter
        enum eOptions : int
        {
            BIT_SIGN = 0,
            BIT_NO_NEGATIVE,
            BIT_MODE_INDIRECT,
            BIT_AUTOZERO_PIEZO2,
            BIT_PRESSURE_COMP,
            BIT_ELECTRONIC_REGULATOR		// 1.500 Electronic Regulator in use
        };

        // 1.500  Uses with "wFillMode" parameter
        enum eFillMode
        {
            FIL_STANDARD,
            FILL_AUTO,
            FILL_INSTRUCTION,
            FILL_RAMP						// Fill with Ramp
        };
        private void SetBit(ref ushort wOption, int n, bool bState)
        {
            if (bState)
                wOption |= (ushort)(1 << n);
            else
                wOption &= (ushort)~(1 << n);
        }

        private int IsBit(ushort wOption, int n)
        {
            return (wOption & (1 << n)) >> n;
        }
        public F28ParamView()
        {
            InitializeComponent();
        }
        public void updateParam(ref F28LightCtrl.F28_PARAMETERS m_tPara)
        {
            m_tPara.usTypeTest = (ushort)cboTestType.SelectedIndex;

            m_tPara.usTpsFill = ushort.Parse(edtFillTime.Text);
            m_tPara.usTpsStab = ushort.Parse(edtStabTime.Text);
            m_tPara.usTpsTest = ushort.Parse(edtTestTime.Text);
            m_tPara.usTpsFillVol = ushort.Parse(edtFillVolTime.Text);
            m_tPara.usTpsTransfert = ushort.Parse(edtTransfertTime.Text);
            m_tPara.usTpsDump = ushort.Parse(edtDumpTime.Text);

            m_tPara.usPress1Unit = (ushort)cboP1Unit.SelectedIndex;
            m_tPara.fPress1Max = float.Parse(edtMaxP1.Text);
            m_tPara.fPress1Min = float.Parse(edtMinP1.Text);
            m_tPara.fRatioMax = float.Parse(edtRatioMax.Text);
            m_tPara.fRatioMin = float.Parse(edtRatioMin.Text);

            m_tPara.fSetFillP1 = float.Parse(edtSetPoint.Text);
            m_tPara.usFillMode = (ushort)cboFillType.SelectedIndex;

            // V2.006
            if (cboLeakUnit.SelectedIndex > (int)F28LightCtrl.F28_LEAK_UNITS.LEAK_POINTS)   // hole in unit list
            {
                m_tPara.usLeakUnit = (ushort)(cboLeakUnit.SelectedIndex + 6);
            }
            else
            {
                m_tPara.usLeakUnit = (ushort)cboLeakUnit.SelectedIndex;
            }
            // V2.006
            m_tPara.fRejectMax = float.Parse(edtLeakMax.Text);
            m_tPara.fRejectMin = float.Parse(edtLeakMin.Text);

            m_tPara.usVolumeUnit = (ushort)cboVolUnit.SelectedIndex;
            m_tPara.fVolume = float.Parse(edtVolume.Text);
            m_tPara.usRejectCalc = (ushort)cboRejectClac.SelectedIndex;


            // 1.402 Options
            SetBit(ref m_tPara.usOptions, (int)eOptions.BIT_SIGN, (chkSign.CheckState == CheckState.Checked));
            SetBit(ref m_tPara.usOptions, (int)eOptions.BIT_NO_NEGATIVE, chkNoNegative.CheckState == CheckState.Checked);
            SetBit(ref m_tPara.usOptions, (int)eOptions.BIT_PRESSURE_COMP, chkPressureCorr.CheckState == CheckState.Checked);

            // 1.500 Electronic Regulator
            SetBit(ref m_tPara.usOptions, (int)eOptions.BIT_ELECTRONIC_REGULATOR, chkElecReg.CheckState == CheckState.Checked);

            m_tPara.fPatmSTD = float.Parse(edtAbsPress.Text);
            m_tPara.fTempSTD = float.Parse(edtTemp.Text);
            m_tPara.fFilterTime = float.Parse(edtFilter.Text);

            m_tPara.fOffsetLeak = float.Parse(edtLeakOffset.Text);

        }
        public void formLoad(F28LightCtrl.F28_PARAMETERS m_tPara)
        {
            cboTestType.SelectedIndex = m_tPara.usTypeTest;

            edtFillTime.Text = m_tPara.usTpsFill.ToString();
            edtStabTime.Text = m_tPara.usTpsStab.ToString();
            edtTestTime.Text = m_tPara.usTpsTest.ToString();
            edtFillVolTime.Text = m_tPara.usTpsFillVol.ToString();
            edtTransfertTime.Text = m_tPara.usTpsTransfert.ToString();
            edtDumpTime.Text = m_tPara.usTpsDump.ToString();

            cboP1Unit.SelectedIndex = m_tPara.usPress1Unit;
            edtMaxP1.Text = m_tPara.fPress1Max.ToString();
            edtMinP1.Text = m_tPara.fPress1Min.ToString();
            edtRatioMax.Text = m_tPara.fRatioMax.ToString();
            edtRatioMin.Text = m_tPara.fRatioMin.ToString();

            edtSetPoint.Text = m_tPara.fSetFillP1.ToString();
            cboFillType.SelectedIndex = m_tPara.usFillMode;

            // V2.006
            if (m_tPara.usLeakUnit > (ushort)F28LightCtrl.F28_LEAK_UNITS.LEAK_POINTS)   // hole in unit list
            {
                cboLeakUnit.SelectedIndex = m_tPara.usLeakUnit - 6;
            }
            else
            {
                cboLeakUnit.SelectedIndex = m_tPara.usLeakUnit;
            }
            // V2.006
            edtLeakMax.Text = m_tPara.fRejectMax.ToString();
            edtLeakMin.Text = m_tPara.fRejectMin.ToString();

            cboVolUnit.SelectedIndex = m_tPara.usVolumeUnit;
            edtVolume.Text = m_tPara.fVolume.ToString("F2");
            cboRejectClac.SelectedIndex = m_tPara.usRejectCalc;

            // 1.402 Options
            chkSign.CheckState = IsBit(m_tPara.usOptions, (int)eOptions.BIT_SIGN) > 0 ? CheckState.Checked : CheckState.Unchecked;
            chkNoNegative.CheckState = IsBit(m_tPara.usOptions, (int)eOptions.BIT_NO_NEGATIVE) > 0 ? CheckState.Checked : CheckState.Unchecked;
            chkPressureCorr.CheckState = IsBit(m_tPara.usOptions, (int)eOptions.BIT_PRESSURE_COMP) > 0 ? CheckState.Checked : CheckState.Unchecked;

            // 1.500 Electronic Regulator
            chkElecReg.CheckState = IsBit(m_tPara.usOptions, (int)eOptions.BIT_ELECTRONIC_REGULATOR) > 0 ? CheckState.Checked : CheckState.Unchecked;



            edtAbsPress.Text = m_tPara.fPatmSTD.ToString("F2");
            edtTemp.Text = m_tPara.fTempSTD.ToString("F2");
            edtFilter.Text = m_tPara.fFilterTime.ToString();

            edtLeakOffset.Text = m_tPara.fOffsetLeak.ToString("F4");

        }
    }
}
