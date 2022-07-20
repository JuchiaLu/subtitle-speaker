using System;
using System.Drawing;
using System.Windows.Forms;

namespace SubtitleSpeaker
{
    public partial class FormMask : Form
    {
        public FormMask()
        {
            InitializeComponent();
        }

        private void formMask_Load(object sender, EventArgs e)
        {

            if (Properties.Settings.Default.FormMaskSize.Width < 50 || Properties.Settings.Default.FormMaskSize.Height == 0)
            {
                int width = Screen.PrimaryScreen.Bounds.Width * 2 / 3;
                int height = 70;
                this.Size = new Size(width, height);
            }
            else
            {
                this.WindowState = Properties.Settings.Default.FormMaskState;
                if (this.WindowState == FormWindowState.Minimized) this.WindowState = FormWindowState.Normal;

                this.Location = Properties.Settings.Default.FormMaskLocation;
                this.Size = Properties.Settings.Default.FormMaskSize;
            }
            this.BackColor = Properties.Settings.Default.FormMaskColor;
            this.Opacity = Properties.Settings.Default.FormMaskOpacity;

            ToolStripMenuItem opacity = (ToolStripMenuItem)this.toolStripMenuItemOpacity.DropDownItems[Properties.Settings.Default.FormMaskOpacitySelectIndex];
            opacity.Checked = true;

            ToolStripMenuItem color = (ToolStripMenuItem)this.toolStripMenuItemColor.DropDownItems[Properties.Settings.Default.FormMaskColorSelectIndex];
            color.Checked = true;
        }

        private void formMask_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.FormMaskState = this.WindowState;
            if (this.WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.FormMaskLocation = this.Location;
                Properties.Settings.Default.FormMaskSize = this.Size;
            }
            else
            {
                Properties.Settings.Default.FormMaskLocation = this.RestoreBounds.Location;
                Properties.Settings.Default.FormMaskSize = this.RestoreBounds.Size;
            }

            Properties.Settings.Default.FormMaskColor = this.BackColor;
            Properties.Settings.Default.FormMaskOpacity = this.Opacity;

            for (int i = 0; i < this.toolStripMenuItemColor.DropDownItems.Count; i++)
            {

                ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)this.toolStripMenuItemColor.DropDownItems[i];

                if (toolStripMenuItem.Checked)
                {
                    Properties.Settings.Default.FormMaskColorSelectIndex = i;
                    break;
                }
            }

            for (int i = 0; i < this.toolStripMenuItemOpacity.DropDownItems.Count; i++)
            {

                ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)this.toolStripMenuItemOpacity.DropDownItems[i];

                if (toolStripMenuItem.Checked)
                {
                    Properties.Settings.Default.FormMaskOpacitySelectIndex = i;
                    break;
                }
            }
            Properties.Settings.Default.Save();
        }

        private void formMask_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Size = new Size(Screen.PrimaryScreen.Bounds.Width, this.Size.Height);
            this.Location = new Point(0, this.Location.Y);
        }

        private int oldX, oldY;
        private void formMask_MouseDown(object sender, MouseEventArgs e)
        {           
            if (e.Button == MouseButtons.Left)
            {
                this.oldX = e.Location.X;
                this.oldY = e.Location.Y;
            }
        }

        private void formMask_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.Location.X - this.oldX;
                this.Top += e.Location.Y - this.oldY;
            }
        }

        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void toolStripMenuItemColorCustom_Click(object sender, EventArgs e)
        {
            if (this.colorDialog.ShowDialog() == DialogResult.OK)
            {
                CheckedControl(this.toolStripMenuItemColor.DropDownItems, this.toolStripMenuItemColorCustom);
                this.BackColor = this.colorDialog.Color;
            }
        }

        private void toolStripMenuItemColorBlack_Click(object sender, EventArgs e)
        {
            CheckedControl(this.toolStripMenuItemColor.DropDownItems, this.toolStripMenuItemColorBlack);
            this.BackColor = Color.Black;
        }

        private void toolStripMenuItemColorWhite_Click(object sender, EventArgs e)
        {
            CheckedControl(this.toolStripMenuItemColor.DropDownItems, this.toolStripMenuItemColorWhite);
            this.BackColor = Color.White;
        }

        private void toolStripMenuItemColorGray_Click(object sender, EventArgs e)
        {
            CheckedControl(this.toolStripMenuItemColor.DropDownItems, this.toolStripMenuItemColorGray);
            this.BackColor = Color.Gray;
        }

        private void CheckedControl(ToolStripItemCollection toolStripItemCollection, ToolStripMenuItem toolStripMenuItem)
        {
            foreach (ToolStripMenuItem item in toolStripItemCollection)
            {
                if (item.Name == toolStripMenuItem.Name)
                {
                    item.Checked = true; 
                }
                else
                {
                    item.Checked = false;
                }
            }
        }

        private void toolStripMenuItemOpacity100_Click(object sender, EventArgs e)
        {
            CheckedControl(this.toolStripMenuItemOpacity.DropDownItems, this.toolStripMenuItemOpacity100);
            this.Opacity = 1;
        }

        private void toolStripMenuItemOpacity90_Click(object sender, EventArgs e)
        {
            CheckedControl(this.toolStripMenuItemOpacity.DropDownItems, this.toolStripMenuItemOpacity90);
            this.Opacity = 0.9;
        }

        private void toolStripMenuItemOpacity80_Click(object sender, EventArgs e)
        {
            CheckedControl(this.toolStripMenuItemOpacity.DropDownItems, this.toolStripMenuItemOpacity80);
            this.Opacity = 0.8;
        }

        private void toolStripMenuItemOpacity70_Click(object sender, EventArgs e)
        {
            CheckedControl(this.toolStripMenuItemOpacity.DropDownItems, this.toolStripMenuItemOpacity70);
            this.Opacity = 0.7;
        }

        private void toolStripMenuItemOpacity60_Click(object sender, EventArgs e)
        {
            CheckedControl(this.toolStripMenuItemOpacity.DropDownItems, this.toolStripMenuItemOpacity60);
            this.Opacity = 0.6;
        }

        private void toolStripMenuItemOpacity50_Click(object sender, EventArgs e)
        {
            CheckedControl(this.toolStripMenuItemOpacity.DropDownItems, this.toolStripMenuItemOpacity50);
            this.Opacity = 0.5;
        }

        private void toolStripMenuItemOpacity40_Click(object sender, EventArgs e)
        {
            CheckedControl(this.toolStripMenuItemOpacity.DropDownItems, this.toolStripMenuItemOpacity40);
            this.Opacity = 0.4;
        }

        private void toolStripMenuItemOpacity30_Click(object sender, EventArgs e)
        {
            CheckedControl(this.toolStripMenuItemOpacity.DropDownItems, this.toolStripMenuItemOpacity30);
            this.Opacity = 0.3;
        }

        private void toolStripMenuItemOpacity20_Click(object sender, EventArgs e)
        {
            CheckedControl(this.toolStripMenuItemOpacity.DropDownItems, this.toolStripMenuItemOpacity20);
            this.Opacity = 0.2;
        }

        private void toolStripMenuItemOpacity10_Click(object sender, EventArgs e)
        {
            CheckedControl(this.toolStripMenuItemOpacity.DropDownItems, this.toolStripMenuItemOpacity10);
            this.Opacity = 0.1;
        }


        private const int Guying_HTLEFT = 10;
        private const int Guying_HTRIGHT = 11;
        private const int Guying_HTTOP = 12;
        private const int Guying_HTTOPLEFT = 13;
        private const int Guying_HTTOPRIGHT = 14;
        private const int Guying_HTBOTTOM = 15;
        private const int Guying_HTBOTTOMLEFT = 0x10;
        private const int Guying_HTBOTTOMRIGHT = 17;

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0084:
                    base.WndProc(ref m);
                    Point vPoint = new Point((int)m.LParam & 0xFFFF, (int)m.LParam >> 16 & 0xFFFF);
                    vPoint = PointToClient(vPoint);
                    if (vPoint.X <= 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)Guying_HTTOPLEFT;
                        else if (vPoint.Y >= this.ClientSize.Height - 5)
                            m.Result = (IntPtr)Guying_HTBOTTOMLEFT;
                        else
                            m.Result = (IntPtr)Guying_HTLEFT;
                    else if (vPoint.X >= this.ClientSize.Width - 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)Guying_HTTOPRIGHT;
                        else if (vPoint.Y >= this.ClientSize.Height - 5)
                            m.Result = (IntPtr)Guying_HTBOTTOMRIGHT;
                        else
                            m.Result = (IntPtr)Guying_HTRIGHT;
                    else if (vPoint.Y <= 5)
                        m.Result = (IntPtr)Guying_HTTOP;
                    else if (vPoint.Y >= this.ClientSize.Height - 5)
                        m.Result = (IntPtr)Guying_HTBOTTOM;
                    break;
                case 0x0201://鼠标左键按下的消息
                    m.Msg = 0x00A1; //更改消息为非客户区按下鼠标
                    m.LParam = IntPtr.Zero; //默认值
                    m.WParam = new IntPtr(2); //鼠标放在标题栏内
                    base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);

                    break;
            }
        }
    }
}
