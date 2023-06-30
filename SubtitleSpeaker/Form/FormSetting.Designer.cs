
namespace SubtitleSpeaker
{
    partial class FormSetting
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSetting));
            this.trackBarVolume = new System.Windows.Forms.TrackBar();
            this.comboBoxInstalledVoices = new System.Windows.Forms.ComboBox();
            this.trackBarRate = new System.Windows.Forms.TrackBar();
            this.labelSpeaker = new System.Windows.Forms.Label();
            this.labelVolume = new System.Windows.Forms.Label();
            this.labelRate = new System.Windows.Forms.Label();
            this.comboBoxSubLanguages = new System.Windows.Forms.ComboBox();
            this.comboBoxMainLanguages = new System.Windows.Forms.ComboBox();
            this.labelMainLanguage = new System.Windows.Forms.Label();
            this.labelSubLanguage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRate)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBarVolume
            // 
            this.trackBarVolume.LargeChange = 10;
            resources.ApplyResources(this.trackBarVolume, "trackBarVolume");
            this.trackBarVolume.Maximum = 100;
            this.trackBarVolume.Name = "trackBarVolume";
            this.trackBarVolume.SmallChange = 10;
            this.trackBarVolume.TickFrequency = 10;
            this.trackBarVolume.Value = 90;
            this.trackBarVolume.ValueChanged += new System.EventHandler(this.trackBarVolume_ValueChanged);
            // 
            // comboBoxInstalledVoices
            // 
            this.comboBoxInstalledVoices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInstalledVoices.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxInstalledVoices, "comboBoxInstalledVoices");
            this.comboBoxInstalledVoices.Name = "comboBoxInstalledVoices";
            this.comboBoxInstalledVoices.SelectedValueChanged += new System.EventHandler(this.comboBoxSelectVoices_SelectedValueChanged);
            // 
            // trackBarRate
            // 
            this.trackBarRate.LargeChange = 1;
            resources.ApplyResources(this.trackBarRate, "trackBarRate");
            this.trackBarRate.Minimum = -10;
            this.trackBarRate.Name = "trackBarRate";
            this.trackBarRate.ValueChanged += new System.EventHandler(this.trackBarRate_ValueChanged);
            // 
            // labelSpeaker
            // 
            resources.ApplyResources(this.labelSpeaker, "labelSpeaker");
            this.labelSpeaker.Name = "labelSpeaker";
            // 
            // labelVolume
            // 
            resources.ApplyResources(this.labelVolume, "labelVolume");
            this.labelVolume.Name = "labelVolume";
            // 
            // labelRate
            // 
            resources.ApplyResources(this.labelRate, "labelRate");
            this.labelRate.Name = "labelRate";
            // 
            // comboBoxSubLanguages
            // 
            this.comboBoxSubLanguages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSubLanguages.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxSubLanguages, "comboBoxSubLanguages");
            this.comboBoxSubLanguages.Name = "comboBoxSubLanguages";
            this.comboBoxSubLanguages.SelectedValueChanged += new System.EventHandler(this.comboBoxSubLanguages_SelectedValueChanged);
            // 
            // comboBoxMainLanguages
            // 
            this.comboBoxMainLanguages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMainLanguages.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxMainLanguages, "comboBoxMainLanguages");
            this.comboBoxMainLanguages.Name = "comboBoxMainLanguages";
            this.comboBoxMainLanguages.SelectedValueChanged += new System.EventHandler(this.comboBoxMainLanguages_SelectedValueChanged);
            // 
            // labelMainLanguage
            // 
            resources.ApplyResources(this.labelMainLanguage, "labelMainLanguage");
            this.labelMainLanguage.Name = "labelMainLanguage";
            // 
            // labelSubLanguage
            // 
            resources.ApplyResources(this.labelSubLanguage, "labelSubLanguage");
            this.labelSubLanguage.Name = "labelSubLanguage";
            // 
            // FormSetting
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.labelSubLanguage);
            this.Controls.Add(this.labelMainLanguage);
            this.Controls.Add(this.comboBoxMainLanguages);
            this.Controls.Add(this.comboBoxSubLanguages);
            this.Controls.Add(this.labelRate);
            this.Controls.Add(this.labelVolume);
            this.Controls.Add(this.labelSpeaker);
            this.Controls.Add(this.trackBarRate);
            this.Controls.Add(this.comboBoxInstalledVoices);
            this.Controls.Add(this.trackBarVolume);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormSetting";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formSetting_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TrackBar trackBarVolume;
        private System.Windows.Forms.ComboBox comboBoxInstalledVoices;
        private System.Windows.Forms.TrackBar trackBarRate;
        private System.Windows.Forms.Label labelSpeaker;
        private System.Windows.Forms.Label labelVolume;
        private System.Windows.Forms.Label labelRate;
        private System.Windows.Forms.ComboBox comboBoxSubLanguages;
        private System.Windows.Forms.ComboBox comboBoxMainLanguages;
        private System.Windows.Forms.Label labelMainLanguage;
        private System.Windows.Forms.Label labelSubLanguage;
    }
}

