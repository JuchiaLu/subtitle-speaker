using SubtitlesParser.Classes;
using SubtitlesParser.Classes.Parsers;
using SubtitlesParser.Classes.Writers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Speech.Synthesis;
using System.Text;
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
            CountDowning = 1,
            Playing = 2,
            Paused = 3
        }

        private readonly Dictionary<string, string> languageDict = new Dictionary<string, string>()
        {
            {"Afrikaans", "afr"},
            {"Albanian", "sqi"},
            {"Arabic", "ara"},
            {"Bengali", "ben"},
            {"Bulgarian", "bul"},
            {"Chinese", "zho"},
            {"Croatian", "hrv"},
            {"Czech", "ces"},
            {"Danish", "dan"},
            {"Dutch", "nld"},
            {"English", "eng"},
            {"Estonian", "est"},
            {"Finnish", "fin"},
            {"French", "fra"},
            {"German", "deu"},
            {"Greek", "ell"},
            {"Gujarati", "guj"},
            {"Hebrew", "heb"},
            {"Hindi", "hin"},
            {"Hungarian", "hun"},
            {"Indonesian", "ind"},
            {"Italian", "ita"},
            {"Japanese", "jpn"},
            {"Kannada", "kan"},
            {"Korean", "kor"},
            {"Latvian", "lav"},
            {"Lithuanian", "lit"},
            {"Macedonian", "mkd"},
            {"Malayalam", "mal"},
            {"Marathi", "mar"},
            {"Nepali", "nep"},
            {"Norwegian", "nor"},
            {"Panjabi", "pan"},
            {"Persian", "fas"},
            {"Polish", "pol"},
            {"Portuguese", "por"},
            {"Romanian", "ron"},
            {"Russian", "rus"},
            {"Slovak", "slk"},
            {"Slovenian", "slv"},
            {"Somali", "som"},
            {"Spanish", "spa"},
            {"Swahili", "swa"},
            {"Swedish", "swe"},
            {"Tagalog", "tgl"},
            {"Tamil", "tam"},
            {"Telugu", "tel"},
            {"Thai", "tha"},
            {"Turkish", "tur"},
            {"Twi", "twi"},
            {"Ukrainian", "ukr"},
            {"Urdu", "urd"},
            {"Vietnamese", "vie"}
        };

        
        private readonly Timer timer = new Timer(1000);

        private readonly SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();


        private readonly SubParser subParser = new SubParser();

        private readonly SrtWriter srtWriter = new SrtWriter();


        private readonly FormMask formMask = new FormMask();

        private readonly FormSetCurrentTime formSetCurrentTime = new FormSetCurrentTime();

        private readonly FormSetting formSetting;


        private string mainLanguage = "cho";

        private string subLanguage = "eng";

        private int rate = 0;


        private BilingualSubtitle bilingualSubtitle;

        private int currentTimeTotalSeconds = 0;

        private int lastTimeTotalSeconds = 0;

        private SubtitleSpeakerState state = SubtitleSpeakerState.UnReady;


        public FormMain()
        {
            this.InitializeComponent();

            //初始化计时器
            this.timer.AutoReset = true;
            this.timer.Enabled = true;
            this.timer.Stop();
            this.timer.Elapsed += new ElapsedEventHandler(this.TimerUpHandler);

            //遮挡工具事件绑定：显示状态变化时，通知状态栏更新勾选状态
            this.formMask.VisibleChanged += new EventHandler(this.toolStripMenuItemMaskTool_Click);


            //尝试从系统全球化信息覆盖设置主语言
            string cultureLanguage = System.Globalization.CultureInfo.CurrentCulture.ThreeLetterISOLanguageName;
            if (this.languageDict.ContainsValue(cultureLanguage))
            {
                this.mainLanguage = cultureLanguage;
            }
            //尝试从用户配置文件覆盖设置主语言
            if (!string.IsNullOrEmpty(Properties.Settings.Default.FormMainMainLanguage))
            {
                this.mainLanguage = Properties.Settings.Default.FormMainMainLanguage;
            }
            //尝试从用户配置文件覆盖设置次语言
            if (!string.IsNullOrEmpty(Properties.Settings.Default.FormMainSubLanguage))
            {
                this.subLanguage = Properties.Settings.Default.FormMainSubLanguage;
            }


            //初始化语音合成器
            try
            {
                //解决获取不到所有安装的说话人
                SpeechApiReflectionHelper.InjectOneCoreVoices(this.speechSynthesizer);
                //从配置文件设置声音
                if (!string.IsNullOrEmpty(Properties.Settings.Default.FormMainVoiceName))
                {
                    this.speechSynthesizer.SelectVoice(Properties.Settings.Default.FormMainVoiceName); //说话人
                }
            }
            catch (Exception ex)
            {
                //do nothing
            }
            this.rate = this.speechSynthesizer.Rate = Properties.Settings.Default.FormMainRate;  //语速
            this.speechSynthesizer.Volume = Properties.Settings.Default.FormMainVolume;  //音量

            //初始化设置窗口（注：rate 改变会关闭自动语速，所以该代码需要放在加载用户配置的 AutoRate 之前）
            this.formSetting = new FormSetting(this.languageDict, this.speechSynthesizer.GetInstalledVoices(), this);
            this.formSetting.SetValue(this.mainLanguage, this.subLanguage, this.speechSynthesizer.Voice.Name, this.speechSynthesizer.Volume, this.rate);

            this.toolStripMenuItemAutoRate.Checked = Properties.Settings.Default.FormMainAutoRate; //是否开启自动语速
            this.toolStripMenuItemCountDownPlay.Checked = Properties.Settings.Default.FormMainCountDownPlay; //是否开启倒计时播放
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }


        //计时器相关
        private delegate void TimeUpDelegate();
       
        private void TimerUpHandler(object sender, ElapsedEventArgs e)
        {
           this.Invoke(new TimeUpDelegate(this.TimerUpAction)); 
        }
       
        private void TimerUpAction()
        {
            // 判断是否结束
            if (this.currentTimeTotalSeconds > this.lastTimeTotalSeconds)
            {
                //等待最后一条字幕朗读完后才停止
                if (this.speechSynthesizer.State != SynthesizerState.Speaking)
                {
                    this.Stop();
                }
            }
            else
            {
                //刷新显示当前时间
                TimeSpan currentTime = TimeSpan.FromSeconds(this.currentTimeTotalSeconds);
                this.labelCurrentTime.Text = $"{currentTime:hh\\:mm\\:ss}";

                //刷新显示进度条
                this.UpdateProgressBar();

                if (this.bilingualSubtitle.Has(this.currentTimeTotalSeconds))
                {
                    // 高亮当前行字幕
                    this.HighlightLine(this.bilingualSubtitle.GetLineIndex(this.currentTimeTotalSeconds));

                    // 朗读当前行字幕
                    this.SpeechLine(this.currentTimeTotalSeconds);
                }

                this.currentTimeTotalSeconds += 1;
            }
        }


        //通用
        public void SetCurrentTotalSeconds(int currentTimeTotalSeconds)
        {
            this.currentTimeTotalSeconds = currentTimeTotalSeconds;
            
            TimeSpan currentTime = TimeSpan.FromSeconds(this.currentTimeTotalSeconds);
            this.labelCurrentTime.Text = $"{currentTime:hh\\:mm\\:ss}";

            this.UpdateProgressBar();

            this.HighlightLine(this.bilingualSubtitle.GetNearLineIndex(currentTimeTotalSeconds));
        }
        
        public void SetLastTotalSeconds(int lastTimeTotalSeconds)
        {
            this.lastTimeTotalSeconds = lastTimeTotalSeconds;

            TimeSpan time = TimeSpan.FromSeconds(this.lastTimeTotalSeconds);
            this.labelLastTime.Text = $"{time:hh\\:mm\\:ss}";

            this.UpdateProgressBar();
        }
        
        public int GetCurrentTotalSeconds()
        {
            return this.currentTimeTotalSeconds;
        }
        
        public int GetLastTotalSeconds()
        {
            return this.lastTimeTotalSeconds;
        }

        private void UpdateProgressBar()
        {
            //转换成 double，相除后才能得到小数，小数再乘以 100 得到百分比
            double currentTimeTotalSeconds = this.currentTimeTotalSeconds;
            double lastTimeTotalSeconds = this.lastTimeTotalSeconds;

            if (lastTimeTotalSeconds != 0 && currentTimeTotalSeconds <= lastTimeTotalSeconds)
            {
                this.progressBar.Value = (int)(currentTimeTotalSeconds / lastTimeTotalSeconds * 100);
            }
        }

        private void HighlightLine(int index)
        {
            //滚动到该行的前四行
            if (index >= 4)
            {
                this.richTextBoxSubtitle.SelectionStart = this.richTextBoxSubtitle.GetFirstCharIndexFromLine(index - 4);
                this.richTextBoxSubtitle.SelectionLength = 0;
                this.richTextBoxSubtitle.ScrollToCaret();
            }
            //高亮该行
            this.richTextBoxSubtitle.SelectionStart = this.richTextBoxSubtitle.GetFirstCharIndexFromLine(index);
            this.richTextBoxSubtitle.SelectionLength = this.richTextBoxSubtitle.Lines[index].Length;
        }
        
        private void SpeechLine(int startTimeTotalSeconds)
        {
            string speechLine = this.bilingualSubtitle.GetSpeechLine(startTimeTotalSeconds);

            // TODO 这里的语速表只是测试少量数据得出的，不太精准，而且也不支持多语言
            static int CalculateSpeechRate(string speechLine, int durationMilliseconds)
            {
                int wordCount;

                if (Regex.IsMatch(speechLine, "[\u4e00-\u9fa5]"))
                    wordCount = speechLine.Length;
                else
                    wordCount = speechLine.Split(" ").Length;

                if (wordCount == 0) return 0;

                double x = durationMilliseconds / wordCount;

                if (x <= 95.50371111111112)
                {
                    return 7; //return 10
                }
                else if (x > 95.50371111111112 && x <= 106.09305)
                {
                    return 7; //return 9
                }
                else if (x > 106.09305 && x <= 117.57281666666667)
                {
                    return 7; //return 8
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

            //如果开启自动语速，则计算语速 
            if (this.toolStripMenuItemAutoRate.Checked)
            {
                int durationMilliseconds = this.bilingualSubtitle.GetSpeechLineDurationMilliseconds(startTimeTotalSeconds);
                this.speechSynthesizer.Rate = CalculateSpeechRate(speechLine, durationMilliseconds);
            }
            else
            {
                this.speechSynthesizer.Rate = this.rate;
            }

            this.speechSynthesizer.SpeakAsyncCancelAll();
            this.speechSynthesizer.SpeakAsync(speechLine);
        }


        // 打开文件相关
        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            //过滤文件格式
            this.openFileDialog.Filter = "All Supported Formats|*.srt;*.sub;*.ssa;*.ass;*.ttml;*.vtt;*.xml|" +
            "SubRip|*.srt|MicroDvd|*.sub|SubViewer|*.sub|SubStationAlpha|*.ssa|AdvancedSubStationAlpha|*.ass|TTML|*.ttml|WebVTT|*.vtt|YoutubeXml|*.xml";

            //清空历史选择名称
            this.openFileDialog.FileName = "";

            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.OpenFile(this.openFileDialog.FileName);
            }   
        }

        private void formMain_DragEnter(object sender, DragEventArgs e)
        {
            List<string> allowExts = new List<string> { ".srt", ".sub", ".ass", ".ssa", ".ttml", ".vtt", ".xml", ".srt" };

            string[] filesFullName = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (e.Data.GetDataPresent(DataFormats.FileDrop)
                && allowExts.Contains(Path.GetExtension(filesFullName[0]))
                && this.state != SubtitleSpeakerState.CountDowning
                && this.state != SubtitleSpeakerState.Playing
                && this.state != SubtitleSpeakerState.Paused)
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void formMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] filesFullName = (string[])e.Data.GetData(DataFormats.FileDrop);
            this.OpenFile(filesFullName[0]);
        }

        private void OpenFile(string fileFullName) 
        {
            try
            {
                //解析字幕
                this.bilingualSubtitle = this.ParseFile(fileFullName);
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
            this.labelSyncTime.Enabled = true;
            this.buttonAddSyncTime.Enabled = true;
            if (this.bilingualSubtitle.GetFirstTotalSeconds() > 0)
            {
                this.buttonSubSyncTime.Enabled = true;
            }
            else
            {
                this.buttonSubSyncTime.Enabled = false;
            }

            //重置字幕同步时间
            this.labelSyncTime.Text = "0";

            //重置当前时间
            this.SetCurrentTotalSeconds(0);

            //重置最后时间
            this.SetLastTotalSeconds(this.bilingualSubtitle.GetLastTotalSeconds());

            //将当前时间标签的光标改为手，提示用户可以点击
            this.labelCurrentTime.Cursor = Cursors.Hand;

            //修改状态为 ready
            this.state = SubtitleSpeakerState.Ready;
        }
        
        private BilingualSubtitle ParseFile(string fileFullName)
        {
            FileStream fileStream = File.OpenRead(fileFullName);

            // 猜测文件字符编码和字幕格式
            Encoding encoding = CharsetDetector.DetectFromStream(fileStream).Detected.Encoding;
            SubtitlesFormat mostLikelyFormat = this.subParser.GetMostLikelyFormat(fileFullName);

            List<SubtitleItem> subtitleItems = this.subParser.ParseStream(fileStream, encoding, mostLikelyFormat);

            if (subtitleItems == null) throw new Exception("File Format Or Encoding Not Supported !");

            return new BilingualSubtitle(subtitleItems, this.mainLanguage, this.subLanguage);
        }


        //播放、暂停、停止相关
        private void buttonPlayOrPause_Click(object sender, EventArgs e)
        {   
            if (this.state == SubtitleSpeakerState.Ready || this.state == SubtitleSpeakerState.Paused)
            {
                if (this.toolStripMenuItemCountDownPlay.Checked)
                { 
                    this.CountDownPlay(); 
                }
                else 
                { 
                    this.Play();
                }
            }
            else if (this.state == SubtitleSpeakerState.Playing)
            {
                this.Pause();
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (this.state == SubtitleSpeakerState.Playing || this.state == SubtitleSpeakerState.Paused)
            {
                this.Stop();
            }
        }
        
        private void Play()
        {
            if (this.state == SubtitleSpeakerState.Ready)
            {
                this.buttonStop.Enabled = true;
                this.buttonOpenFile.Enabled = false;
            }
            else if (this.state == SubtitleSpeakerState.Paused)
            {
                this.speechSynthesizer.Resume();
            }
            this.timer.Start();
            this.buttonPlayOrPause.Image = Properties.Resources.pause;
            this.state = SubtitleSpeakerState.Playing;
        }
        
        private void CountDownPlay() 
        {
            //原来的状态：可能是 paused 或 ready
            SubtitleSpeakerState tempState = this.state; 
            
            //临时计时器
            Timer tempTimer = new Timer(1000);
            tempTimer.AutoReset = true;
            tempTimer.Enabled = true;
            
            int i = 3;
            tempTimer.Elapsed += new ElapsedEventHandler((object sender, ElapsedEventArgs e) => 
            {
                this.state = SubtitleSpeakerState.CountDowning;

                this.speechSynthesizer.SpeakAsyncCancelAll();
                this.speechSynthesizer.Resume();
                this.speechSynthesizer.Rate = 0;
                this.speechSynthesizer.SpeakAsync(i.ToString());
                
                i--;
                if (i == 0) 
                {
                    tempTimer.Stop();
                    this.state = tempState;
                    this.Invoke(new TimeUpDelegate(this.Play));
                }
            });
        }

        private void Pause()
        {
            this.timer.Stop();
            this.speechSynthesizer.Pause();
            this.buttonPlayOrPause.Image = Properties.Resources.paly;
            this.state = SubtitleSpeakerState.Paused;
        }

        private void Stop()
        {
            // 1. 停止计时
            this.timer.Stop();

            // 2. 停止声音
            this.speechSynthesizer.SpeakAsyncCancelAll();
            this.speechSynthesizer.Resume(); //处理暂停的时候 CancelAll ,再次播放时没声音

            // 3. 将当前时间设置为 0 
            this.SetCurrentTotalSeconds(0);

            // 4. 清除行高亮
            this.richTextBoxSubtitle.Select(0, 0);

            // 5. 将播放或暂停按钮图标设置为播放图标 
            this.buttonPlayOrPause.Image = Properties.Resources.paly;

            // 6. 锁定停止播放按钮
            this.buttonStop.Enabled = false;

            // 7. 解除打开文件按钮 
            this.buttonOpenFile.Enabled = true;

            // 8.修改状态
            this.state = SubtitleSpeakerState.Ready;
        }


        //修改当前时间相关
        private void labelSetCurrentTime_Click(object sender, EventArgs e)
        {
            if (this.state != SubtitleSpeakerState.UnReady)
            {
                this.formSetCurrentTime.ShowDialog(this);
            }
        }
        
        private void richTextBoxSubtitle_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.state != SubtitleSpeakerState.UnReady)
            {
                //获取双击的行号
                int firstCharIndex = this.richTextBoxSubtitle.GetFirstCharIndexOfCurrentLine();
                int lineIndex = this.richTextBoxSubtitle.GetLineFromCharIndex(firstCharIndex);

                //获取行号所在的字幕的开始时间
                TimeSpan currentTime = TimeSpan.Parse(this.richTextBoxSubtitle.Lines[lineIndex].Substring(1, 8));

                //设为为当前时间
                this.SetCurrentTotalSeconds((int)currentTime.TotalSeconds);

                //停止之前未播放完的声音
                this.speechSynthesizer.SpeakAsyncCancelAll();
            }
        }


        // 字幕时间同步相关
        private void buttonAddSyncTime_Click(object sender, EventArgs e)
        {
            this.SubtitleTimeSync(1);
        }
        
        private void buttonSubSyncTime_Click(object sender, EventArgs e)
        {
            this.SubtitleTimeSync(-1);
        }
        
        private void labelSyncTime_DoubleClick(object sender, EventArgs e)
        {
            int offsetSeconds = int.Parse(this.labelSyncTime.Text);
            if (offsetSeconds != 0)
            { 
                this.SubtitleTimeSync(-offsetSeconds);
            }
        }
        
        private void SubtitleTimeSync(int offsetSeconds) 
        {
            //获取当前行号
            int firstCharIndex = this.richTextBoxSubtitle.GetFirstCharIndexOfCurrentLine();
            int lineIndex = this.richTextBoxSubtitle.GetLineFromCharIndex(firstCharIndex);

            //修改字幕时间
            this.bilingualSubtitle.TimeSync(offsetSeconds);

            //更新显示修改过后的字幕
            this.richTextBoxSubtitle.Text = this.bilingualSubtitle.ToString();

            //可能需要更新当前时间
            if (this.currentTimeTotalSeconds > this.bilingualSubtitle.GetLastTotalSeconds())
            {
                this.SetCurrentTotalSeconds(this.bilingualSubtitle.GetLastTotalSeconds());
            }

            //更新最后时间
            this.SetLastTotalSeconds(this.bilingualSubtitle.GetLastTotalSeconds());

            //高亮原来所在行
            this.HighlightLine(lineIndex);

            //更新显示同步时间标签
            offsetSeconds = int.Parse(this.labelSyncTime.Text) + offsetSeconds;
            if (offsetSeconds > 0)
            {
                this.labelSyncTime.Text = "+" + offsetSeconds.ToString();
            }
            else
            {
                this.labelSyncTime.Text = offsetSeconds.ToString();
            }

            //更新按钮状态
            if (this.bilingualSubtitle.GetFirstTotalSeconds() <= 0)
            {
                this.buttonSubSyncTime.Enabled = false;
            }
            else
            {
                this.buttonSubSyncTime.Enabled = true;
            }
        }


        //语言、语速、音量、说话人、自动语速相关
        public void ChangeLanguage(string mainLanguage, string subLanguage)
        {
            this.mainLanguage = mainLanguage;
            this.subLanguage = subLanguage;

            if (this.state != SubtitleSpeakerState.UnReady)
            {
                if (this.state == SubtitleSpeakerState.Playing || this.state == SubtitleSpeakerState.Paused)
                {
                    this.Stop();
                }

                this.bilingualSubtitle.Divide(mainLanguage, subLanguage);

                //重置富文本框
                this.richTextBoxSubtitle.Text = this.bilingualSubtitle.ToString();

                //重置字幕同步按钮状态
                if (this.bilingualSubtitle.GetFirstTotalSeconds() > 0)
                    this.buttonSubSyncTime.Enabled = true;
                else
                    this.buttonSubSyncTime.Enabled = false;

                //重置字幕同步时间
                this.labelSyncTime.Text = "0";

                //重置当前时间
                this.SetCurrentTotalSeconds(0);

                //重置最后时间
                this.SetLastTotalSeconds(this.bilingualSubtitle.GetLastTotalSeconds());
            }
        }

        public void ChangeVoice(string voiceNmae)
        {
            this.speechSynthesizer.SelectVoice(voiceNmae);
        }

        public void ChangeRate(int rate)
        {
            this.rate = rate;
            this.speechSynthesizer.Rate = rate;

            this.toolStripMenuItemAutoRate.Checked = false;
        }

        public void ChangeVolume(int volume)
        {
            this.speechSynthesizer.Volume = volume;
        }

        private void toolStripMenuItemAutoRate_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.toolStripMenuItemAutoRate.Checked)
            {
                this.speechSynthesizer.Rate = this.rate;
            }
        }


        //导出分离后的次语言字幕
        private void richTextBoxFileName_DoubleClick(object sender, EventArgs e)
        {
            //去除双击选中文本效果
            this.richTextBoxFileName.Select(0, 0);

            if (state != SubtitleSpeakerState.UnReady)
            {
                this.saveFileDialog.FileName = Path.GetFileNameWithoutExtension(this.richTextBoxFileName.Text) + "." + this.subLanguage + ".srt";
                this.saveFileDialog.Filter = "|*.srt";
               
                if (this.saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        FileStream subSubtitle = File.Create(this.saveFileDialog.FileName);
                        this.srtWriter.WriteStream(subSubtitle, this.bilingualSubtitle.GetSubSubtitleItems(), false);
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


        //打开设置窗口
        private void toolStripMenuItemOtherSetting_Click(object sender, EventArgs e)
        {
            this.formSetting.Show();
            this.formSetting.WindowState = FormWindowState.Normal;
            this.formSetting.Activate();
        }

        //切换字幕遮盖工具窗口
        private void toolStripMenuItemMaskTool_Click(object sender, EventArgs e)
        {
            if (this.toolStripMenuItemMaskTool.Checked)
            {
                this.formMask.Hide();
                this.toolStripMenuItemMaskTool.Checked = false;
            }
            else
            {
                this.formMask.Show();
                this.toolStripMenuItemMaskTool.Checked = true;
            }
        }


        //拦截关闭按钮，转变成隐藏
        private void formMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        //双击通知图标打开主页面
        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        //退出前保存用户配置
        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.FormMainMainLanguage = this.mainLanguage; //主语言
            Properties.Settings.Default.FormMainSubLanguage = this.subLanguage; //次语言
            Properties.Settings.Default.FormMainVoiceName = this.speechSynthesizer.Voice.Name; //说话人
            Properties.Settings.Default.FormMainVolume = this.speechSynthesizer.Volume; //音量
            Properties.Settings.Default.FormMainRate = this.rate; //语速

            Properties.Settings.Default.FormMainAutoRate = this.toolStripMenuItemAutoRate.Checked; //是否开启自动语速
            Properties.Settings.Default.FormMainCountDownPlay = this.toolStripMenuItemCountDownPlay.Checked; //是否开启倒计时播放

            Properties.Settings.Default.Save();

            //退出
            this.Close();
            this.Dispose();
            Application.Exit();
        }
    }
}
