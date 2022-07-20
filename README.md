

## 本软件的初衷

看美剧学英语，很多人都有过这种想法，但发现看了这么多年，提升效果不明显，主要有以下问题：

- 看双语字幕：语速太快，只顾着看中文，被动忽视英文字幕。

- 看纯英文字幕：词汇量不够，几乎看不懂，连电影都快看不下去。

- 看纯英语字幕（但给某些单词加中文解释）：[这个](https://github.com/studyzy/LearnEnglishBySubtitle)开源软件可以做到这一点，但由于一个单词有多个意思，且电影中口语化比较严重，给的中文解释经常与电影要表达的不一致，会导致无法理解整个句子。

因此需要一种先强迫你把整个英文字幕看完，然后再告诉你中文解释的方法，所以有了本软件。



开始使用这个软件，可能会觉得有点混乱，毕竟这个合成的语音听着不太舒服，但多看几部电影就能适应，并忽视它的存在。

每部剧都有它的范畴，大量的相关词汇会反复出现，越看到后面越轻松，也许某一天，突然发现居然忘记打开本软件也把电影看完了。

## 软件使用说明

### 字幕文件打开

点击文件打开按钮或直接将文件拖入到程序即可

支持的字幕格式有：

- SubRip(.srt)
- WebVTT(.vtt)
- MicroDvd(.sub)
- SubViewer(.sub)
- Youtube Xml(.xml)
- SubStation Alpha(.ssa)
- Advanced SubStation Alpha(.ass)
- Timed Text Markup Language(.ttml)

### 当前时间设置

- 方式一：直接点击字幕的某一行，会自动设置当前时间为该行字幕的时间。

  <img src="https://raw.githubusercontent.com/JuchiaLu/subtitle-speaker/master/img/ff30d345-ed2d-4d6c-bbc8-6542e7a6a16c.gif" alt="ff30d345-ed2d-4d6c-bbc8-6542e7a6a16c" style="zoom: 67%;" />

- 方式二：点击绿色时间标签，会弹出时间设置窗口（这个窗口的输入做了非常人性化的处理）。

  <img src="https://raw.githubusercontent.com/JuchiaLu/subtitle-speaker/master/img/4c409416-ebd2-4414-9f2c-70c4ab7c08e8.gif" alt="4c409416-ebd2-4414-9f2c-70c4ab7c08e8" style="zoom: 67%;" />

### 字幕时间同步

- 下方的 “-” 和 “+” 按钮可以调整字幕时间，使其整体提前或推迟 n 秒。

  <img src="https://raw.githubusercontent.com/JuchiaLu/subtitle-speaker/master/img/2ffe8bee-f431-497b-824e-807480176084.gif" alt="2ffe8bee-f431-497b-824e-807480176084" style="zoom:67%;" />

- 双击中间的数字标签，字幕时间恢复到原始状态。

  <img src="https://raw.githubusercontent.com/JuchiaLu/subtitle-speaker/master/img/ba2e7c27-8584-400d-b84f-0ecab9c626a5.gif" alt="ba2e7c27-8584-400d-b84f-0ecab9c626a5" style="zoom:67%;" />

### 双语字幕分离

- 如果打开的字幕是双语字幕，程序会自动分离字幕，字幕朗读框只有中文字幕。

  <img src="https://raw.githubusercontent.com/JuchiaLu/subtitle-speaker/master/img/2a45f335-2c18-4109-b837-f628c5516a73.gif" alt="2a45f335-2c18-4109-b837-f628c5516a73" style="zoom:67%;" />

- 如果想导出分离后的英文字幕，请双击上方的文件名文本框，便会弹出保存窗口。

  <img src="https://raw.githubusercontent.com/JuchiaLu/subtitle-speaker/master/img/200ec779-a2e7-4aa2-82f2-c388c4daec8e.gif" alt="200ec779-a2e7-4aa2-82f2-c388c4daec8e" style="zoom:67%;" />

### 通知栏选项

右键通知栏图标，有以下三个选项：

- 自动语速

  勾选此选项后，朗读的语速根据每条字幕显示的时长和字符数自动调节。

  用途：字幕显示的时长并不取决于字符数，如果采用匀速朗读，字符数太多时，在规定时间内可能读不完，勾选本选项后自动调节朗读语速，尽可能的读完字符。

- 倒计时播放

  勾选此选项后，点击播放按钮，会倒计时三秒钟后才播放。

  用途：先设置软件的当前时间等于电影的当前播放时间，然后点击软件的播放按钮，你会听到“三、二、一”的倒计时，当听到“一”的同时按下电影的播放按钮，妈妈再也不用担心软件的当前时间与电影的当前时间不同步了。

- 遮挡工具

  勾选此选项后，将弹出一个遮挡工具窗口。

  用途：有的电影是双语硬字幕，遮挡工具可以用来遮挡掉其中的中文字幕，支持自定义大小、颜色，以及十档透明度可调节，还贴心的给强迫症和处女座人士提供了双击功能（双击遮挡工具窗口，窗口的长度会自动与屏幕的长度保持一致）。

### 其他隐藏选项

说话人选择、音量调节、固定语速设置的功能也已经实现，但由于不常用，为了保持软件界面简洁，不提供这几个功能，有需要的可以自己 fork 代码，把主界面拖宽一点，并把相关组件的 enable 选项打开，就能使用这几个选项。

<img src="https://raw.githubusercontent.com/JuchiaLu/subtitle-speaker/master/img/20220720030228.png" alt="20220720030228" style="zoom: 50%;" />

## 致谢

本软件使用到以下开源库，对作者的开源精神表示感谢

- 字幕格式解析器：https://github.com/AlexPoint/SubtitlesParser
- 文件编码识别器：https://github.com/CharsetDetector/UTF-unknown

同时写本软件之前，有尝试过搜索，查找现成的软件，发现有一款名叫 “SubtitleToSpeech（By William Sengdara）”的闭源软件，但其仅支持 ANSI 字符编码，且仅支持 srt 格式字幕，以及没有字幕遮挡、自动语速、双语字幕支持、倒计时播放、字幕时间同步等功能，所以不能满足我的需求，但本软件的 UI 布局有参考到它。