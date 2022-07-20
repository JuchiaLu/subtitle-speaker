using System;
using System.Windows.Forms;

namespace SubtitleSpeaker
{
    public partial class FormSetCurrentTime : Form
    {
        public FormSetCurrentTime()
        {
            InitializeComponent();
        }

        private TimeSpan lastTime;

        private void formSetCurrentTime_Load(object sender, EventArgs e)
        {
            FormMain owner = (FormMain)this.Owner;

            this.lastTime = TimeSpan.FromSeconds(owner.GetLastTime());

            var currentTime = TimeSpan.FromSeconds(owner.GetCurrentTime());
            this.textBoxHours.Text = $"{currentTime:hh}";
            this.textBoxMinutes.Text = $"{currentTime:mm}";
            this.textBoxSeconds.Text = $"{currentTime:ss}";

            //this.labelConfirm.Select();
        }

        private void textBoxHours_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 只接受数字、退格、回车 这几个键
            if ((e.KeyChar < (char)Keys.D0 || e.KeyChar > (char)Keys.D9) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter)) 
            {
                e.Handled = true;
                return;
            }
            // 如果是回车，跳到分钟框
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.textBoxMinutes.Focus();
                e.Handled = true;
                return;
            }
            //如果是数字且之前已输入过一位数字了，拼接上本次输入的数字后，跳到分钟框
            if ((e.KeyChar != (char)Keys.Back) && (this.textBoxHours.Text.Length == 1))
            {
                this.textBoxHours.Text += e.KeyChar;
                this.textBoxMinutes.Focus();
                e.Handled = true;
            }
        }

        private void textBoxMinutes_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 只接受数字、退格、回车 这几个键
            if ((e.KeyChar < (char)Keys.D0 || e.KeyChar > (char)Keys.D9) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter))
            {
                e.Handled = true;
                return;
            }
            // 如果是回车，跳到秒钟框
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.textBoxSeconds.Focus();
                e.Handled = true;
                return;
            }
            //如果是数字且之前已输入过一位数字了，拼接上本次输入的数字后，跳到秒钟框
            if ((e.KeyChar != (char)Keys.Back) && (this.textBoxMinutes.Text.Length == 1))
            {
                this.textBoxMinutes.Text += e.KeyChar;
                this.textBoxSeconds.Focus();
                e.Handled = true;
            }
        }

        private void textBoxSeconds_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 只接受数字、退格、回车 这几个键
            if ((e.KeyChar < (char)Keys.D0 || e.KeyChar > (char)Keys.D9) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter))
            {
                e.Handled = true;
                return;
            }
            // 如果是回车，跳到秒钟框
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.labelConfirm.Select();
                e.Handled = true;
                return;
            }
            //如果是数字且之前已输入过一位数字了，拼接上本次输入的数字后，跳到确定按钮
            if ((e.KeyChar != (char)Keys.Back) && (this.textBoxSeconds.Text.Length == 1))
            {
                this.textBoxSeconds.Text += e.KeyChar;
                this.labelConfirm.Select();
                e.Handled = true;
            }
        }     

        private void textBoxHours_MouseEnter(object sender, EventArgs e)
        {
            this.textBoxHours.Focus();
        }

        private void textBoxHours_MouseLeave(object sender, EventArgs e)
        {
            this.labelConfirm.Select();      
        }

        private void textBoxMinutes_MouseEnter(object sender, EventArgs e)
        {
            this.textBoxMinutes.Focus();
        }

        private void textBoxMinutes_MouseLeave(object sender, EventArgs e)
        {
            this.labelConfirm.Select();
        }

        private void textBoxSeconds_MouseEnter(object sender, EventArgs e)
        {
            this.textBoxSeconds.Focus();
        }

        private void textBoxSeconds_MouseLeave(object sender, EventArgs e)
        {
            this.labelConfirm.Select();
        }

        private string oldHours = "00";
        private string oldMinutes = "00";
        private string oldSeconds = "00";

        private void textBoxHours_Enter(object sender, EventArgs e)
        {
            this.oldHours = this.textBoxHours.Text;
            this.textBoxHours.Text = "";
        }

        private void textBoxHours_Leave(object sender, EventArgs e)
        {
            if (this.textBoxHours.Text.Length == 0)
            {
                this.textBoxHours.Text = this.oldHours;
            }
            else
            {
                if (this.textBoxHours.Text.Length <= 1)
                    this.textBoxHours.Text = "0" + this.textBoxHours.Text;

                int hours = int.Parse(this.textBoxHours.Text);
                int minutes = int.Parse(this.textBoxMinutes.Text);
                int seconds = int.Parse(this.textBoxSeconds.Text);

                if (hours > 23 || hours > this.lastTime.Hours)
                {
                    this.textBoxHours.Text = $"{this.lastTime:hh}";
                    hours = this.lastTime.Hours;
                }

                //会影响到分钟的取值，需要重新校验分钟的取值
                if (hours == this.lastTime.Hours && minutes > this.lastTime.Minutes)
                {
                    this.textBoxMinutes.Text = $"{this.lastTime:mm}";
                    minutes = this.lastTime.Minutes;
                }

                //会影响到秒钟的取值，需要重新校验秒钟的取值
                if (hours == this.lastTime.Hours && minutes == this.lastTime.Minutes && seconds > this.lastTime.Seconds)
                {
                    this.textBoxSeconds.Text = $"{this.lastTime:ss}";
                }
            }
        }

        private void textBoxMinutes_Enter(object sender, EventArgs e)
        {
            this.oldMinutes = this.textBoxMinutes.Text;
            this.textBoxMinutes.Text = "";
        }

        private void textBoxMinutes_Leave(object sender, EventArgs e)
        {
            if (this.textBoxMinutes.Text.Length == 0)
            {
                this.textBoxMinutes.Text = this.oldMinutes;
            }
            else
            {
                if (this.textBoxMinutes.Text.Length <= 1)
                    this.textBoxMinutes.Text = "0" + this.textBoxMinutes.Text;

                int hours = int.Parse(this.textBoxHours.Text);
                int minutes = int.Parse(this.textBoxMinutes.Text);
                int seconds = int.Parse(this.textBoxSeconds.Text);

                if (minutes > 59)
                {
                    this.textBoxMinutes.Text = "59";
                    minutes = 59;
                }
                if (hours == this.lastTime.Hours && minutes > this.lastTime.Minutes)
                {
                    this.textBoxMinutes.Text = $"{this.lastTime:mm}";
                    minutes = this.lastTime.Minutes;
                }

                //会影响到秒钟的取值，需要重新校验秒钟的取值
                if (hours == this.lastTime.Hours && minutes == this.lastTime.Minutes && seconds > this.lastTime.Seconds)
                {
                    textBoxSeconds.Text = $"{this.lastTime:ss}";
                }
            }
        }

        private void textBoxSeconds_Enter(object sender, EventArgs e)
        {
            this.oldSeconds = this.textBoxSeconds.Text;
            this.textBoxSeconds.Text = "";
        }

        private void textBoxSeconds_Leave(object sender, EventArgs e)
        {
            if (this.textBoxSeconds.Text.Length == 0)
            {
                this.textBoxSeconds.Text = this.oldSeconds;
            }
            else
            {
                if (this.textBoxSeconds.Text.Length <= 1)
                    this.textBoxSeconds.Text = "0" + this.textBoxSeconds.Text;

                int hours = int.Parse(this.textBoxHours.Text);
                int minutes = int.Parse(this.textBoxMinutes.Text);
                int seconds = int.Parse(this.textBoxSeconds.Text);

                if (seconds > 59)
                {
                    this.textBoxSeconds.Text = "59";
                    seconds = 59;
                }
                if (hours == this.lastTime.Hours && minutes == this.lastTime.Minutes && seconds > this.lastTime.Seconds)
                {
                    textBoxSeconds.Text = $"{this.lastTime:ss}";
                }
            }
        }

        private void labelCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void labelConfirm_Click(object sender, EventArgs e)
        {
            Confirm();
        }

        private void labelConfirm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Confirm();
        }

        private void formSetCurrentTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                this.Close();
        }

        private void Confirm()
        {
            this.labelConfirm.Select();

            var currentTime = new TimeSpan(
                0, 
                int.Parse(this.textBoxHours.Text),
                int.Parse(this.textBoxMinutes.Text),
                int.Parse(this.textBoxSeconds.Text)
                );
            SubtitleSpeaker.FormMain owner = (SubtitleSpeaker.FormMain)this.Owner;
            owner.SetCurrentTime((int)currentTime.TotalSeconds);
            this.Close();
        }
    }
}
