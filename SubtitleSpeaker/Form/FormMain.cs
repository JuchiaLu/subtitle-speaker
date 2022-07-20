using SubtitlesParser.Classes.Parsers;
using SubtitlesParser.Classes.Writers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Speech.Synthesis;
using System.Text.RegularExpressions;
using System.Timers;
using System.Windows.Forms;
using UtfUnknown;
using Timer = System.Timers.Timer;

namespace SubtitleSpeaker
{
    public partial class FormMain : Form
    {
        public enum SubtitleSpeakerState
        {
            UnReady  = -1,
            Ready = 0,
            CountDown = 1,
            Playing = 2,
            Paused = 3
        }

        public FormMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //初始化计时器
            this.timer.AutoReset = true;
            this.timer.Enabled = true;
            this.timer.Stop();
            this.timer.Elapsed += new ElapsedEventHandler(this.TimerUpHandler);

            // 初始化语音合成器
            foreach (var voice in this.speechSynthesizer.GetInstalledVoices())
            {
                this.comboBoxInstalledVoices.Items.Add(voice.VoiceInfo.Name);
            }
            this.comboBoxInstalledVoices.SelectedIndex = 0;

            //初始化遮挡工具：关闭时提示通知栏更新状态
            formMask.VisibleChanged += new EventHandler(this.toolStripMenuItemMaskTool_Click);

            //加载配置
            this.trackBarRate.Value = Properties.Settings.Default.FormMainRate;
            this.toolStripMenuItemAutoRate.Checked = Properties.Settings.Default.FormMainAutoRate;
            this.trackBarVolume.Value = Properties.Settings.Default.FormMainVolume;
            this.comboBoxInstalledVoices.SelectedIndex = Properties.Settings.Default.FormMainVoiceIndex;
            this.toolStripMenuItemCountDownPlay.Checked = Properties.Settings.Default.FormMainCountDownPlay;
        }


        private BilingualSubtitle bilingualSubtitle;


        private readonly SubParser subParser = new SubParser();

        private readonly SrtWriter srtWriter = new SrtWriter();

        private readonly SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();

        private readonly Timer timer = new Timer(1000);

        private readonly FormMask formMask = new FormMask();



        private int currentTimeTotalSeconds = 0;

        private int lastTimeTotalSeconds = 0;

        private SubtitleSpeakerState subtitleSpeakerState = SubtitleSpeakerState.UnReady;



        //计时器相关

        private delegate void TimeUpDelegate();
       
        private void TimerUpHandler(object sender, ElapsedEventArgs e)
        {
           Invoke(new TimeUpDelegate(TimerUpAction)); 
        }
       
        private void TimerUpAction()
        {
            // 判断是否结束
            if (this.currentTimeTotalSeconds > this.lastTimeTotalSeconds && this.speechSynthesizer.State != SynthesizerState.Speaking)
            {
                Stop();
                return;
            }

            //刷新显示当前时间
            TimeSpan currentTime = TimeSpan.FromSeconds(this.currentTimeTotalSeconds);
            this.labelCurrentTime.Text = $"{currentTime:hh\\:mm\\:ss}";

            //刷新显示进度条
            UpdateProgressBar();

            //是否有字幕需要朗读
            if (this.bilingualSubtitle.Has(this.currentTimeTotalSeconds))
            {
                // 高亮当前行字幕
                Highlight(this.bilingualSubtitle.GetLineIndex(this.currentTimeTotalSeconds));

                // 朗读当前行字幕
                Speech(this.currentTimeTotalSeconds);
            }

            this.currentTimeTotalSeconds += 1;
        }



        //通用
        public void SetCurrentTime(int currentTimeTotalSeconds)
        {
            this.currentTimeTotalSeconds = currentTimeTotalSeconds;
            TimeSpan time = TimeSpan.FromSeconds(this.currentTimeTotalSeconds);
            this.labelCurrentTime.Text = $"{time:hh\\:mm\\:ss}";

            Highlight(bilingualSubtitle.GetNearLineIndex(currentTimeTotalSeconds));

            UpdateProgressBar();
        }
        
        public void SetLastTime(int lastTimeTotalSeconds)
        {
            this.lastTimeTotalSeconds = lastTimeTotalSeconds;
            TimeSpan time = TimeSpan.FromSeconds(this.lastTimeTotalSeconds);
            this.labelLastTime.Text = $"{time:hh\\:mm\\:ss}";
        }
        
        public int GetCurrentTime()
        {
            return this.currentTimeTotalSeconds;
        }
        
        public int GetLastTime()
        {
            return this.lastTimeTotalSeconds;
        }
        
        private void Highlight(int lineIndex)
        {
            if (lineIndex >= 4)
            {
                this.richTextBoxSubtitle.SelectionStart = this.richTextBoxSubtitle.GetFirstCharIndexFromLine(lineIndex - 4);
                this.richTextBoxSubtitle.SelectionLength = 0;
                this.richTextBoxSubtitle.ScrollToCaret();
            }
            this.richTextBoxSubtitle.SelectionStart = this.richTextBoxSubtitle.GetFirstCharIndexFromLine(lineIndex);
            this.richTextBoxSubtitle.SelectionLength = this.richTextBoxSubtitle.Lines[lineIndex].Length;
        }
        
        private void UpdateProgressBar()
        {
            double temp = this.currentTimeTotalSeconds;
            double temp2 = this.lastTimeTotalSeconds;
            if (this.lastTimeTotalSeconds != 0)
                this.progressBar.Value = (int)(temp / temp2 * 100);
        }
        
        private void Speech(int startTimeTotalSeconds)
        {
            // TODO 这里的语速表只是测试少量数据得出的，不太精准
            int CalculateSpeechRate(int totalMilliseconds, string speechLine)
            {
                int words;

                if (Regex.IsMatch(speechLine, "[\u4e00-\u9fa5]"))
                    words = speechLine.Length;
                else
                    words = speechLine.Split(" ").Length;

                if (words == 0) return 0;

                double x = totalMilliseconds / words;

                if (x <= 95.50371111111112)
                {
                    return 10;
                }
                else if (x > 95.50371111111112 && x <= 106.09305)
                {
                    return 9;
                }
                else if (x > 106.09305 && x <= 117.57281666666667)
                {
                    return 8;
                }
                else if (x > 117.57281666666667 && x <= 130.91011111111112)
                {
                    return 7;
                }
                else if (x > 130.91011111111112 && x <= 146.12486666666666)
                {
                    return 6;
                }
                else if (x > 146.12486666666666 && x <= 161.50627222222224)
                {
                    return 5;
                }
                else if (x > 161.50627222222224 && x <= 178.88638333333336)
                {
                    return 4;
                }
                else if (x > 178.88638333333336 && x <= 200.4558777777778)
                {
                    return 3;
                }
                else if (x > 200.4558777777778 && x <= 222.64502222222222)
                {
                    return 2;
                }
                else if (x > 222.64502222222222 && x <= 247.1284222222222)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }

            string speechLine = this.bilingualSubtitle.GetSpeechLine(startTimeTotalSeconds);

            if (this.toolStripMenuItemAutoRate.Checked)
            {
                this.speechSynthesizer.Rate = CalculateSpeechRate(this.bilingualSubtitle.GetSpeechLineTotalMilliseconds(startTimeTotalSeconds), speechLine);
            }

            this.speechSynthesizer.SpeakAsyncCancelAll();
            this.speechSynthesizer.SpeakAsync(speechLine);
        }



        // 打开文件相关
        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            this.openFileDialog.Filter = "All Supported Formats|*.srt;*.sub;*.ssa;*.ass;*.ttml;*.vtt;*.xml|" +
            "SubRip|*.srt|MicroDvd|*.sub|SubViewer|*.sub|SubStationAlpha|*.ssa|AdvancedSubStationAlpha|*.ass|TTML|*.ttml|WebVTT|*.vtt|YoutubeXml|*.xml";

            this.openFileDialog.FileName = "";

            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                OpenFile(this.openFileDialog.FileName);
            }   
        }
        
        private void OpenFile(string fileFullName) {

            try
            {
                //解析字幕
                this.bilingualSubtitle = Parse(fileFullName);
            }
            catch (Exception ex) 
            {
                //TODO 显示错误信息
                this.richTextBoxFileName.Text = "Exception: " + ex.Message;
                return;
            }

            //显示已打开的文件
            this.richTextBoxFileName.Text = Path.GetFileName(fileFullName);

            //把字幕显示到富文本框
            this.richTextBoxSubtitle.Text = this.bilingualSubtitle.ToString();

            //解锁播放按钮
            this.buttonPlayOrPause.Enabled = true;

            //解锁字幕同步按钮
            this.buttonAddSyncTime.Enabled = true;
            this.labelSyncTime.Enabled = true;
            if (this.bilingualSubtitle.GetFirstTotalSeconds() > 0) 
                this.buttonSubSyncTime.Enabled = true;
            else
                this.buttonSubSyncTime.Enabled = false;

            //重置字幕同步时间
            this.labelSyncTime.Text = "0";

            //重置当前时间
            SetCurrentTime(0);

            //设置最后时间
            SetLastTime(this.bilingualSubtitle.GetLastTotalSeconds());

            //将当前时间标签的光标改为手，提示用户可以点击
            this.labelCurrentTime.Cursor = Cursors.Hand;

            //修改状态为 ready
            this.subtitleSpeakerState = SubtitleSpeakerState.Ready;

        }
        
        private BilingualSubtitle Parse(string fileFullName)
        {
            var fileStream = File.OpenRead(fileFullName);

            var mostLikelyFormat = this.subParser.GetMostLikelyFormat(fileFullName);

            var encoding = CharsetDetector.DetectFromStream(fileStream).Detected.Encoding;

            var subItems = this.subParser.ParseStream(fileStream, encoding, mostLikelyFormat);

            if (subItems == null) throw new Exception("File Format Not Supported !");

            return new BilingualSubtitle(subItems);
        }
        
        private void formMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] filesFullName = (string[])e.Data.GetData(DataFormats.FileDrop);
            OpenFile(filesFullName[0]);
        }
        
        private void formMain_DragEnter(object sender, DragEventArgs e)
        {
           List<string> allowExts = new List<string>() ;
            allowExts.Add(".srt");
            allowExts.Add(".sub");
            allowExts.Add(".ass");
            allowExts.Add(".ssa");
            allowExts.Add(".ttml");
            allowExts.Add(".vtt");
            allowExts.Add(".xml");
            allowExts.Add(".srt");

            string[] filesFullName = (string[])e.Data.GetData(DataFormats.FileDrop);
           
            if (
                e.Data.GetDataPresent(DataFormats.FileDrop) 
                && allowExts.Contains(Path.GetExtension(filesFullName[0]))
                && this.subtitleSpeakerState != SubtitleSpeakerState.Playing
                && this.subtitleSpeakerState != SubtitleSpeakerState.Paused
                )
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }



        //播放、暂停、停止相关
        private void buttonPlayOrPause_Click(object sender, EventArgs e)
        {   
            if (this.subtitleSpeakerState == SubtitleSpeakerState.Ready || this.subtitleSpeakerState == SubtitleSpeakerState.Paused)
            {
                if (this.toolStripMenuItemCountDownPlay.Checked && subtitleSpeakerState!= SubtitleSpeakerState.CountDown)
                { 
                    CountDownPlay(); 
                }
                else { 
                    Play();
                }
            }
            else if (this.subtitleSpeakerState == SubtitleSpeakerState.Playing)
            {
                Pause();
            }
        }
        
        private void Pause()
        {
            this.timer.Stop();
            this.speechSynthesizer.Pause();
            this.subtitleSpeakerState = SubtitleSpeakerState.Paused;
            this.buttonPlayOrPause.Image = global::SubtitleSpeaker.Properties.Resources.paly;
            //this.buttonPlayOrPause.Text = "▶︎";
        }
        
        private void Play()
        {
            if (this.subtitleSpeakerState == SubtitleSpeakerState.Ready)
            {
                this.buttonStop.Enabled = true;
                this.buttonOpenFile.Enabled = false;
            }
            else if (this.subtitleSpeakerState == SubtitleSpeakerState.Paused)
            {
                this.speechSynthesizer.Resume();
            }
            this.timer.Start();
            this.subtitleSpeakerState = SubtitleSpeakerState.Playing;
            this.buttonPlayOrPause.Image = global::SubtitleSpeaker.Properties.Resources.pause;
            //this.buttonPlayOrPause.Text = "⏸️";
        }
        
        private void CountDownPlay() 
        {
            SubtitleSpeakerState tempState = this.subtitleSpeakerState;
            this.subtitleSpeakerState = SubtitleSpeakerState.CountDown;
            int i = 3;
            Timer tempTimer = new Timer(1000);
            tempTimer.AutoReset = true;
            tempTimer.Enabled = true;
            tempTimer.Elapsed += new ElapsedEventHandler((object sender, ElapsedEventArgs e) => {
                this.speechSynthesizer.SpeakAsyncCancelAll();
                this.speechSynthesizer.Resume();
                this.speechSynthesizer.SpeakAsync(i.ToString());
                i--;
                if (i == 0) 
                {
                    tempTimer.Stop();
                    this.subtitleSpeakerState = tempState;
                    Invoke(new TimeUpDelegate(Play));
                }
            });
        }
        
        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (this.subtitleSpeakerState == SubtitleSpeakerState.Playing || this.subtitleSpeakerState == SubtitleSpeakerState.Paused) 
            {
                Stop();
            }
            
        }
        
        private void Stop()
        {
            // 1. 停止播放
            this.speechSynthesizer.SpeakAsyncCancelAll();
            this.speechSynthesizer.Resume(); //处理暂停的时候 CancelAll ,再次播放时没声音

            // 2. 停止计时
            this.timer.Stop();

            // 3. 将当前时间设置为 0 
            SetCurrentTime(0);

            // 4. 将当前字幕滚到首行
            this.richTextBoxSubtitle.Select(0, 0);
            this.richTextBoxSubtitle.ScrollToCaret();

            // 5. 将播放或暂停按钮图标设置为播放图标 
            this.buttonPlayOrPause.Image = global::SubtitleSpeaker.Properties.Resources.paly;
            //this.buttonPlayOrPause.Text = "▶︎";

            // 6. 解除打开文件按钮 
            this.buttonOpenFile.Enabled = true;

            // 7. 锁定停止播放按钮
            this.buttonStop.Enabled = false;

            // 8.修改状态
            this.subtitleSpeakerState = SubtitleSpeakerState.Ready;
        }



        //修改当前时间相关
        private void labelSetCurrentTime_Click(object sender, EventArgs e)
        {
            if (this.subtitleSpeakerState == SubtitleSpeakerState.UnReady) return;

            FormSetCurrentTime formSetCurrentTime = new FormSetCurrentTime();
            formSetCurrentTime.ShowDialog(this);
        }
        
        private void richTextBoxSubtitle_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.subtitleSpeakerState == SubtitleSpeakerState.UnReady) return;

            int firstCharIndex = this.richTextBoxSubtitle.GetFirstCharIndexOfCurrentLine();
            int lineIndex = this.richTextBoxSubtitle.GetLineFromCharIndex(firstCharIndex);

            //高亮所在行
            //Highlight(lineIndex);

            //获取行号所在的字幕的开始时间，并设为当前时间
            TimeSpan timeSpan = TimeSpan.Parse(this.richTextBoxSubtitle.Lines[lineIndex].Substring(1, 8));
            SetCurrentTime((int)timeSpan.TotalSeconds);

            //停止之前未播放完的声音
            this.speechSynthesizer.SpeakAsyncCancelAll();
        }


        
       //语速、音量、说话人控制相关

        private void toolStripMenuItemAutoRate_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.toolStripMenuItemAutoRate.Checked)
                this.speechSynthesizer.Rate = this.trackBarRate.Value;
        }

        private void trackBarVolume_ValueChanged(object sender, EventArgs e)
        {
            this.speechSynthesizer.Volume = this.trackBarVolume.Value;
        }
        
        private void trackBarRate_ValueChanged(object sender, EventArgs e)
        {
            this.speechSynthesizer.Rate = this.trackBarRate.Value;
        }
       
        private void comboBoxSelectVoices_SelectedValueChanged(object sender, EventArgs e)
        {
            this.speechSynthesizer.SelectVoice((string)this.comboBoxInstalledVoices.SelectedItem);
        }



        // 字幕时间同步相关
        private void buttonAddSyncTime_Click(object sender, EventArgs e)
        {
            SubtitleTimeSync(1);
        }
        
        private void buttonSubSyncTime_Click(object sender, EventArgs e)
        {
            SubtitleTimeSync(-1);
        }
        
        private void labelSyncTime_DoubleClick(object sender, EventArgs e)
        {
            int v = int.Parse(this.labelSyncTime.Text);
            if (v!=0)
                SubtitleTimeSync(-v);
        }
        
        private void SubtitleTimeSync(int offsetSeconds) {

            int firstCharIndex = this.richTextBoxSubtitle.GetFirstCharIndexOfCurrentLine();
            int lineIndex = this.richTextBoxSubtitle.GetLineFromCharIndex(firstCharIndex);

            //修改字幕时间
            this.bilingualSubtitle.TimeSync(offsetSeconds);

            //更新显示修改过后的字幕
            this.richTextBoxSubtitle.Text = this.bilingualSubtitle.ToString();

            //更新最后时间
            SetLastTime(this.bilingualSubtitle.GetLastTotalSeconds());

            //高亮原来所在行
            Highlight(lineIndex);

            //更新显示同步时间标签
            int v = int.Parse(this.labelSyncTime.Text) + offsetSeconds;
            if (v > 0)
                this.labelSyncTime.Text = "+" + v.ToString();
            else
                this.labelSyncTime.Text =  v.ToString();
            

            //更新按钮状态
            if (this.bilingualSubtitle.GetFirstTotalSeconds() == 0)
            {
                this.buttonSubSyncTime.Enabled = false;
            }
            else
            {
                this.buttonSubSyncTime.Enabled = true;
            }
        }



        //工具
        private void richTextBoxFileName_DoubleClick(object sender, EventArgs e)
        {
            this.richTextBoxFileName.Select(0, 0);
            if (subtitleSpeakerState != SubtitleSpeakerState.UnReady)
            {
                this.saveFileDialog.FileName = Path.GetFileNameWithoutExtension(this.richTextBoxFileName.Text) + ".Oher.srt";
                this.saveFileDialog.Filter = "|*.srt";
                if (this.saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var other = File.Create(this.saveFileDialog.FileName);
                        srtWriter.WriteStream(other, this.bilingualSubtitle.GetOtherLanguageSubtitle(), false);
                    }
                    catch (Exception ex)
                    {
                        // TODO 显示错误信息
                        this.richTextBoxFileName.Text = ex.Message;
                        return;
                    }
                }
            }
        }

        private void toolStripMenuItemMaskTool_Click(object sender, EventArgs e)
        {
            if (this.toolStripMenuItemMaskTool.Checked)
            {
                formMask.Hide();
                this.toolStripMenuItemMaskTool.Checked = false;
            }
            else
            {
                formMask.Show();
                this.toolStripMenuItemMaskTool.Checked = true;
            }
        }



        // 其他
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //拦截关闭按钮，转变成隐藏
            e.Cancel = true;
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            // 保存配置
            Properties.Settings.Default.FormMainAutoRate = this.toolStripMenuItemAutoRate.Checked;
            Properties.Settings.Default.FormMainCountDownPlay = this.toolStripMenuItemCountDownPlay.Checked;
            Properties.Settings.Default.FormMainVolume = this.trackBarVolume.Value;
            Properties.Settings.Default.FormMainVoiceIndex = this.comboBoxInstalledVoices.SelectedIndex;
            Properties.Settings.Default.Save();

            //退出
            this.Close();
            this.Dispose();
            Application.Exit();
        }

        
    }
}
