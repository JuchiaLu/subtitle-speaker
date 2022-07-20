
namespace SubtitleSpeaker
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.buttonPlayOrPause = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.richTextBoxSubtitle = new System.Windows.Forms.RichTextBox();
            this.richTextBoxFileName = new System.Windows.Forms.RichTextBox();
            this.labelCurrentTime = new System.Windows.Forms.Label();
            this.labelLastTime = new System.Windows.Forms.Label();
            this.labelArrows = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.trackBarVolume = new System.Windows.Forms.TrackBar();
            this.comboBoxInstalledVoices = new System.Windows.Forms.ComboBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.labelSyncTime = new System.Windows.Forms.Label();
            this.buttonAddSyncTime = new System.Windows.Forms.Button();
            this.buttonSubSyncTime = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemMaskTool = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAutoRate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCountDownPlay = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.trackBarRate = new System.Windows.Forms.TrackBar();
            this.labelSpeaker = new System.Windows.Forms.Label();
            this.labelVolume = new System.Windows.Forms.Label();
            this.labelRate = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRate)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.buttonOpenFile, "buttonOpenFile");
            this.buttonOpenFile.Image = global::SubtitleSpeaker.Properties.Resources.open;
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.UseVisualStyleBackColor = false;
            this.buttonOpenFile.Click += new System.EventHandler(this.buttonOpenFile_Click);
            // 
            // buttonPlayOrPause
            // 
            resources.ApplyResources(this.buttonPlayOrPause, "buttonPlayOrPause");
            this.buttonPlayOrPause.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonPlayOrPause.Image = global::SubtitleSpeaker.Properties.Resources.paly;
            this.buttonPlayOrPause.Name = "buttonPlayOrPause";
            this.buttonPlayOrPause.UseVisualStyleBackColor = true;
            this.buttonPlayOrPause.Click += new System.EventHandler(this.buttonPlayOrPause_Click);
            // 
            // buttonStop
            // 
            resources.ApplyResources(this.buttonStop, "buttonStop");
            this.buttonStop.Image = global::SubtitleSpeaker.Properties.Resources.stop;
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // richTextBoxSubtitle
            // 
            this.richTextBoxSubtitle.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.richTextBoxSubtitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxSubtitle.Cursor = System.Windows.Forms.Cursors.Default;
            this.richTextBoxSubtitle.DetectUrls = false;
            this.richTextBoxSubtitle.HideSelection = false;
            resources.ApplyResources(this.richTextBoxSubtitle, "richTextBoxSubtitle");
            this.richTextBoxSubtitle.Name = "richTextBoxSubtitle";
            this.richTextBoxSubtitle.ReadOnly = true;
            this.richTextBoxSubtitle.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.richTextBoxSubtitle_MouseDoubleClick);
            // 
            // richTextBoxFileName
            // 
            this.richTextBoxFileName.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.richTextBoxFileName.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.richTextBoxFileName, "richTextBoxFileName");
            this.richTextBoxFileName.Name = "richTextBoxFileName";
            this.richTextBoxFileName.ReadOnly = true;
            this.richTextBoxFileName.DoubleClick += new System.EventHandler(this.richTextBoxFileName_DoubleClick);
            // 
            // labelCurrentTime
            // 
            resources.ApplyResources(this.labelCurrentTime, "labelCurrentTime");
            this.labelCurrentTime.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.labelCurrentTime.Name = "labelCurrentTime";
            this.labelCurrentTime.Click += new System.EventHandler(this.labelSetCurrentTime_Click);
            // 
            // labelLastTime
            // 
            resources.ApplyResources(this.labelLastTime, "labelLastTime");
            this.labelLastTime.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelLastTime.Name = "labelLastTime";
            // 
            // labelArrows
            // 
            resources.ApplyResources(this.labelArrows, "labelArrows");
            this.labelArrows.Name = "labelArrows";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // progressBar
            // 
            this.progressBar.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.progressBar.ForeColor = System.Drawing.Color.MediumSeaGreen;
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Name = "progressBar";
            this.progressBar.Step = 1;
            // 
            // trackBarVolume
            // 
            resources.ApplyResources(this.trackBarVolume, "trackBarVolume");
            this.trackBarVolume.LargeChange = 10;
            this.trackBarVolume.Maximum = 100;
            this.trackBarVolume.Name = "trackBarVolume";
            this.trackBarVolume.SmallChange = 10;
            this.trackBarVolume.TickFrequency = 10;
            this.trackBarVolume.Value = 90;
            this.trackBarVolume.ValueChanged += new System.EventHandler(this.trackBarVolume_ValueChanged);
            // 
            // comboBoxInstalledVoices
            // 
            resources.ApplyResources(this.comboBoxInstalledVoices, "comboBoxInstalledVoices");
            this.comboBoxInstalledVoices.FormattingEnabled = true;
            this.comboBoxInstalledVoices.Name = "comboBoxInstalledVoices";
            this.comboBoxInstalledVoices.SelectedValueChanged += new System.EventHandler(this.comboBoxSelectVoices_SelectedValueChanged);
            // 
            // labelSyncTime
            // 
            resources.ApplyResources(this.labelSyncTime, "labelSyncTime");
            this.labelSyncTime.Name = "labelSyncTime";
            this.labelSyncTime.DoubleClick += new System.EventHandler(this.labelSyncTime_DoubleClick);
            // 
            // buttonAddSyncTime
            // 
            resources.ApplyResources(this.buttonAddSyncTime, "buttonAddSyncTime");
            this.buttonAddSyncTime.Name = "buttonAddSyncTime";
            this.buttonAddSyncTime.UseVisualStyleBackColor = true;
            this.buttonAddSyncTime.Click += new System.EventHandler(this.buttonAddSyncTime_Click);
            // 
            // buttonSubSyncTime
            // 
            resources.ApplyResources(this.buttonSubSyncTime, "buttonSubSyncTime");
            this.buttonSubSyncTime.Name = "buttonSubSyncTime";
            this.buttonSubSyncTime.UseVisualStyleBackColor = true;
            this.buttonSubSyncTime.Click += new System.EventHandler(this.buttonSubSyncTime_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            resources.ApplyResources(this.notifyIcon, "notifyIcon");
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemMaskTool,
            this.toolStripMenuItemAutoRate,
            this.toolStripMenuItemCountDownPlay,
            this.toolStripSeparator1,
            this.toolStripMenuItemExit});
            this.contextMenuStrip.Name = "contextMenuStrip";
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            // 
            // toolStripMenuItemMaskTool
            // 
            this.toolStripMenuItemMaskTool.Name = "toolStripMenuItemMaskTool";
            resources.ApplyResources(this.toolStripMenuItemMaskTool, "toolStripMenuItemMaskTool");
            this.toolStripMenuItemMaskTool.Click += new System.EventHandler(this.toolStripMenuItemMaskTool_Click);
            // 
            // toolStripMenuItemAutoRate
            // 
            this.toolStripMenuItemAutoRate.Checked = true;
            this.toolStripMenuItemAutoRate.CheckOnClick = true;
            this.toolStripMenuItemAutoRate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItemAutoRate.Name = "toolStripMenuItemAutoRate";
            resources.ApplyResources(this.toolStripMenuItemAutoRate, "toolStripMenuItemAutoRate");
            this.toolStripMenuItemAutoRate.CheckedChanged += new System.EventHandler(this.toolStripMenuItemAutoRate_CheckedChanged);
            // 
            // toolStripMenuItemCountDownPlay
            // 
            this.toolStripMenuItemCountDownPlay.Checked = true;
            this.toolStripMenuItemCountDownPlay.CheckOnClick = true;
            this.toolStripMenuItemCountDownPlay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItemCountDownPlay.Name = "toolStripMenuItemCountDownPlay";
            resources.ApplyResources(this.toolStripMenuItemCountDownPlay, "toolStripMenuItemCountDownPlay");
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripMenuItemExit
            // 
            this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
            resources.ApplyResources(this.toolStripMenuItemExit, "toolStripMenuItemExit");
            this.toolStripMenuItemExit.Click += new System.EventHandler(this.toolStripMenuItemExit_Click);
            // 
            // trackBarRate
            // 
            resources.ApplyResources(this.trackBarRate, "trackBarRate");
            this.trackBarRate.LargeChange = 1;
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
            // FormMain
            // 
            this.AllowDrop = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.labelRate);
            this.Controls.Add(this.labelVolume);
            this.Controls.Add(this.labelSpeaker);
            this.Controls.Add(this.trackBarRate);
            this.Controls.Add(this.buttonSubSyncTime);
            this.Controls.Add(this.buttonAddSyncTime);
            this.Controls.Add(this.labelSyncTime);
            this.Controls.Add(this.comboBoxInstalledVoices);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.labelLastTime);
            this.Controls.Add(this.labelCurrentTime);
            this.Controls.Add(this.richTextBoxFileName);
            this.Controls.Add(this.richTextBoxSubtitle);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonPlayOrPause);
            this.Controls.Add(this.buttonOpenFile);
            this.Controls.Add(this.labelArrows);
            this.Controls.Add(this.trackBarVolume);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.formMain_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.formMain_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.Button buttonPlayOrPause;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.RichTextBox richTextBoxSubtitle;
        private System.Windows.Forms.RichTextBox richTextBoxFileName;
        private System.Windows.Forms.Label labelCurrentTime;
        private System.Windows.Forms.Label labelLastTime;
        private System.Windows.Forms.Label labelArrows;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TrackBar trackBarVolume;
        private System.Windows.Forms.ComboBox comboBoxInstalledVoices;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Label labelSyncTime;
        private System.Windows.Forms.Button buttonAddSyncTime;
        private System.Windows.Forms.Button buttonSubSyncTime;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCountDownPlay;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMaskTool;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExit;
        private System.Windows.Forms.TrackBar trackBarRate;
        private System.Windows.Forms.Label labelSpeaker;
        private System.Windows.Forms.Label labelVolume;
        private System.Windows.Forms.Label labelRate;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAutoRate;
    }
}

