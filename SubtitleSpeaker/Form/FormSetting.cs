using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Speech.Synthesis;
using System.Windows.Forms;


namespace SubtitleSpeaker
{
    public partial class FormSetting : Form
    {
        public FormSetting(Dictionary<string, string> languageDict, ReadOnlyCollection<InstalledVoice> installedVoices, FormMain formMain) 
        {
            this.InitializeComponent();

            //初始化主语言列表
            this.comboBoxMainLanguages.DataSource = new BindingSource(languageDict, null);
            this.comboBoxMainLanguages.DisplayMember = "Key";
            this.comboBoxMainLanguages.ValueMember = "Value";

            //初始化次语言列表
            this.comboBoxSubLanguages.DataSource = new BindingSource(languageDict, null);
            this.comboBoxSubLanguages.DisplayMember = "Key";
            this.comboBoxSubLanguages.ValueMember = "Value";

            //初始化语音合成器列表
            foreach (InstalledVoice voice in installedVoices)
            {
                this.comboBoxInstalledVoices.Items.Add(voice.VoiceInfo.Name);
            }

            this.formMain = formMain;
        }

        private readonly FormMain formMain;

        public void SetValue(string mainLanguage, string subLanguage, string voiceName, int volume, int rate)
        {
            this.comboBoxMainLanguages.SelectedValue = mainLanguage;
            this.comboBoxSubLanguages.SelectedValue = subLanguage;
            
            this.comboBoxInstalledVoices.SelectedItem = voiceName;

            this.trackBarVolume.Value = volume;
            this.trackBarRate.Value = rate;
        }

        private void comboBoxMainLanguages_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.formMain != null && this.comboBoxMainLanguages.SelectedItem != null && this.comboBoxSubLanguages.SelectedItem != null)
            {
                this.formMain.ChangeLanguage(this.comboBoxMainLanguages.SelectedValue.ToString(), this.comboBoxSubLanguages.SelectedValue.ToString());
            }
        }

        private void comboBoxSubLanguages_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.formMain != null && this.comboBoxMainLanguages.SelectedItem != null && this.comboBoxSubLanguages.SelectedItem != null)
            {
                this.formMain.ChangeLanguage(this.comboBoxMainLanguages.SelectedValue.ToString(), this.comboBoxSubLanguages.SelectedValue.ToString());
            }
        }

        private void comboBoxSelectVoices_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.formMain != null && this.comboBoxInstalledVoices.SelectedItem != null)
            {
                this.formMain.ChangeVoice((string)this.comboBoxInstalledVoices.SelectedItem);
            }
        }

        private void trackBarVolume_ValueChanged(object sender, EventArgs e)
        {
            if (this.formMain != null)
            { 
                this.formMain.ChangeVolume(this.trackBarVolume.Value);
            }
        }

        private void trackBarRate_ValueChanged(object sender, EventArgs e)
        {
            if (this.formMain != null)
            {
                this.formMain.ChangeRate(this.trackBarRate.Value);
            }
        }

        private void formSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
