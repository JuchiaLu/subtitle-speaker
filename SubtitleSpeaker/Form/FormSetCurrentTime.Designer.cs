
namespace SubtitleSpeaker
{
    partial class FormSetCurrentTime
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSetCurrentTime));
            this.labelHoursSeparator = new System.Windows.Forms.Label();
            this.labelMinutesSeparator = new System.Windows.Forms.Label();
            this.textBoxHours = new System.Windows.Forms.TextBox();
            this.textBoxMinutes = new System.Windows.Forms.TextBox();
            this.textBoxSeconds = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelCancel = new System.Windows.Forms.Label();
            this.labelConfirm = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelHoursSeparator
            // 
            resources.ApplyResources(this.labelHoursSeparator, "labelHoursSeparator");
            this.labelHoursSeparator.Name = "labelHoursSeparator";
            // 
            // labelMinutesSeparator
            // 
            resources.ApplyResources(this.labelMinutesSeparator, "labelMinutesSeparator");
            this.labelMinutesSeparator.Name = "labelMinutesSeparator";
            // 
            // textBoxHours
            // 
            resources.ApplyResources(this.textBoxHours, "textBoxHours");
            this.textBoxHours.Name = "textBoxHours";
            this.textBoxHours.Enter += new System.EventHandler(this.textBoxHours_Enter);
            this.textBoxHours.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxHours_KeyPress);
            this.textBoxHours.Leave += new System.EventHandler(this.textBoxHours_Leave);
            this.textBoxHours.MouseEnter += new System.EventHandler(this.textBoxHours_MouseEnter);
            this.textBoxHours.MouseLeave += new System.EventHandler(this.textBoxHours_MouseLeave);
            // 
            // textBoxMinutes
            // 
            resources.ApplyResources(this.textBoxMinutes, "textBoxMinutes");
            this.textBoxMinutes.Name = "textBoxMinutes";
            this.textBoxMinutes.Enter += new System.EventHandler(this.textBoxMinutes_Enter);
            this.textBoxMinutes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxMinutes_KeyPress);
            this.textBoxMinutes.Leave += new System.EventHandler(this.textBoxMinutes_Leave);
            this.textBoxMinutes.MouseEnter += new System.EventHandler(this.textBoxMinutes_MouseEnter);
            this.textBoxMinutes.MouseLeave += new System.EventHandler(this.textBoxMinutes_MouseLeave);
            // 
            // textBoxSeconds
            // 
            resources.ApplyResources(this.textBoxSeconds, "textBoxSeconds");
            this.textBoxSeconds.Name = "textBoxSeconds";
            this.textBoxSeconds.Enter += new System.EventHandler(this.textBoxSeconds_Enter);
            this.textBoxSeconds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSeconds_KeyPress);
            this.textBoxSeconds.Leave += new System.EventHandler(this.textBoxSeconds_Leave);
            this.textBoxSeconds.MouseEnter += new System.EventHandler(this.textBoxSeconds_MouseEnter);
            this.textBoxSeconds.MouseLeave += new System.EventHandler(this.textBoxSeconds_MouseLeave);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Gainsboro;
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Gainsboro;
            this.label3.Name = "label3";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Gainsboro;
            this.label1.Name = "label1";
            // 
            // labelCancel
            // 
            resources.ApplyResources(this.labelCancel, "labelCancel");
            this.labelCancel.ForeColor = System.Drawing.Color.Green;
            this.labelCancel.Name = "labelCancel";
            this.labelCancel.Click += new System.EventHandler(this.labelCancel_Click);
            // 
            // labelConfirm
            // 
            resources.ApplyResources(this.labelConfirm, "labelConfirm");
            this.labelConfirm.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelConfirm.Name = "labelConfirm";
            this.labelConfirm.Click += new System.EventHandler(this.labelConfirm_Click);
            this.labelConfirm.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.labelConfirm_PreviewKeyDown);
            // 
            // FormSetCurrentTime
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.Controls.Add(this.labelConfirm);
            this.Controls.Add(this.labelCancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxSeconds);
            this.Controls.Add(this.textBoxMinutes);
            this.Controls.Add(this.textBoxHours);
            this.Controls.Add(this.labelMinutesSeparator);
            this.Controls.Add(this.labelHoursSeparator);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FormSetCurrentTime";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.formSetCurrentTime_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.formSetCurrentTime_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelHoursSeparator;
        private System.Windows.Forms.Label labelMinutesSeparator;
        private System.Windows.Forms.TextBox textBoxHours;
        private System.Windows.Forms.TextBox textBoxMinutes;
        private System.Windows.Forms.TextBox textBoxSeconds;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelConfirm;
        private System.Windows.Forms.Label labelCancel;
    }
}