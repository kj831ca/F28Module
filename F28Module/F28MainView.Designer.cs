namespace F28Module
{
    partial class F28MainView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBoxModule = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelPressure = new System.Windows.Forms.Label();
            this.PressureUnit = new System.Windows.Forms.Label();
            this.LeakUnit = new System.Windows.Forms.Label();
            this.Leak = new System.Windows.Forms.Label();
            this.TemperatureUnit = new System.Windows.Forms.Label();
            this.Temperature = new System.Windows.Forms.Label();
            this.PAtmUnit = new System.Windows.Forms.Label();
            this.PAtm = new System.Windows.Forms.Label();
            this.Status = new System.Windows.Forms.Label();
            this.buttonSaveParam = new System.Windows.Forms.Button();
            this.buttonStartCycle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxModule
            // 
            this.listBoxModule.FormattingEnabled = true;
            this.listBoxModule.ItemHeight = 16;
            this.listBoxModule.Location = new System.Drawing.Point(38, 80);
            this.listBoxModule.Name = "listBoxModule";
            this.listBoxModule.Size = new System.Drawing.Size(120, 244);
            this.listBoxModule.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sensors List";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(405, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "F28 View";
            // 
            // labelPressure
            // 
            this.labelPressure.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelPressure.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPressure.Location = new System.Drawing.Point(300, 80);
            this.labelPressure.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPressure.Name = "labelPressure";
            this.labelPressure.Size = new System.Drawing.Size(147, 39);
            this.labelPressure.TabIndex = 17;
            this.labelPressure.Text = "--------";
            this.labelPressure.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PressureUnit
            // 
            this.PressureUnit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PressureUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PressureUnit.Location = new System.Drawing.Point(455, 80);
            this.PressureUnit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PressureUnit.Name = "PressureUnit";
            this.PressureUnit.Size = new System.Drawing.Size(128, 39);
            this.PressureUnit.TabIndex = 18;
            this.PressureUnit.Text = "--------";
            this.PressureUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LeakUnit
            // 
            this.LeakUnit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LeakUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LeakUnit.Location = new System.Drawing.Point(763, 80);
            this.LeakUnit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LeakUnit.Name = "LeakUnit";
            this.LeakUnit.Size = new System.Drawing.Size(128, 39);
            this.LeakUnit.TabIndex = 21;
            this.LeakUnit.Text = "--------";
            this.LeakUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Leak
            // 
            this.Leak.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Leak.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Leak.Location = new System.Drawing.Point(591, 80);
            this.Leak.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Leak.Name = "Leak";
            this.Leak.Size = new System.Drawing.Size(164, 39);
            this.Leak.TabIndex = 20;
            this.Leak.Text = "--------";
            this.Leak.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TemperatureUnit
            // 
            this.TemperatureUnit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TemperatureUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TemperatureUnit.Location = new System.Drawing.Point(763, 181);
            this.TemperatureUnit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TemperatureUnit.Name = "TemperatureUnit";
            this.TemperatureUnit.Size = new System.Drawing.Size(128, 39);
            this.TemperatureUnit.TabIndex = 28;
            this.TemperatureUnit.Text = "°C";
            this.TemperatureUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Temperature
            // 
            this.Temperature.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Temperature.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Temperature.Location = new System.Drawing.Point(591, 181);
            this.Temperature.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Temperature.Name = "Temperature";
            this.Temperature.Size = new System.Drawing.Size(164, 39);
            this.Temperature.TabIndex = 27;
            this.Temperature.Text = "--------";
            this.Temperature.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PAtmUnit
            // 
            this.PAtmUnit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PAtmUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PAtmUnit.Location = new System.Drawing.Point(455, 181);
            this.PAtmUnit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PAtmUnit.Name = "PAtmUnit";
            this.PAtmUnit.Size = new System.Drawing.Size(128, 39);
            this.PAtmUnit.TabIndex = 26;
            this.PAtmUnit.Text = "hPa";
            this.PAtmUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PAtm
            // 
            this.PAtm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PAtm.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PAtm.Location = new System.Drawing.Point(301, 181);
            this.PAtm.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PAtm.Name = "PAtm";
            this.PAtm.Size = new System.Drawing.Size(147, 39);
            this.PAtm.TabIndex = 25;
            this.PAtm.Text = "--------";
            this.PAtm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Status
            // 
            this.Status.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Status.Location = new System.Drawing.Point(300, 131);
            this.Status.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(591, 39);
            this.Status.TabIndex = 29;
            this.Status.Text = "--------";
            this.Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonSaveParam
            // 
            this.buttonSaveParam.Location = new System.Drawing.Point(38, 341);
            this.buttonSaveParam.Name = "buttonSaveParam";
            this.buttonSaveParam.Size = new System.Drawing.Size(120, 23);
            this.buttonSaveParam.TabIndex = 30;
            this.buttonSaveParam.Text = "Save Param";
            this.buttonSaveParam.UseVisualStyleBackColor = true;
            // 
            // buttonStartCycle
            // 
            this.buttonStartCycle.Location = new System.Drawing.Point(301, 259);
            this.buttonStartCycle.Name = "buttonStartCycle";
            this.buttonStartCycle.Size = new System.Drawing.Size(96, 23);
            this.buttonStartCycle.TabIndex = 31;
            this.buttonStartCycle.Text = "Start Cycle";
            this.buttonStartCycle.UseVisualStyleBackColor = true;
            // 
            // F28MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonStartCycle);
            this.Controls.Add(this.buttonSaveParam);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.TemperatureUnit);
            this.Controls.Add(this.Temperature);
            this.Controls.Add(this.PAtmUnit);
            this.Controls.Add(this.PAtm);
            this.Controls.Add(this.LeakUnit);
            this.Controls.Add(this.Leak);
            this.Controls.Add(this.PressureUnit);
            this.Controls.Add(this.labelPressure);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxModule);
            this.Name = "F28MainView";
            this.Size = new System.Drawing.Size(956, 569);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ListBox listBoxModule;
        public System.Windows.Forms.Label labelPressure;
        public System.Windows.Forms.Label PressureUnit;
        public System.Windows.Forms.Label LeakUnit;
        public System.Windows.Forms.Label Leak;
        public System.Windows.Forms.Label TemperatureUnit;
        public System.Windows.Forms.Label Temperature;
        public System.Windows.Forms.Label PAtmUnit;
        public System.Windows.Forms.Label PAtm;
        public System.Windows.Forms.Label Status;
        public System.Windows.Forms.Button buttonSaveParam;
        public System.Windows.Forms.Button buttonStartCycle;
    }
}
