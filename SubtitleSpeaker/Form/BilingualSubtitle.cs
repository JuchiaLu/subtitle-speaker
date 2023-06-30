using LanguageDetection;
using Newtonsoft.Json.Linq;
using SubtitlesParser.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubtitleSpeaker
{
    public class BilingualSubtitle
    {
        public BilingualSubtitle(IEnumerable<SubtitleItem> subtitleItems, string mainLanguage, string subLanguage)
        {
            this.subtitleItems = subtitleItems.OrderBy(item => item.StartTime);
            
            this.Divide(mainLanguage, subLanguage);
        }

        //原始字幕条目
        private readonly IEnumerable<SubtitleItem> subtitleItems = new List<SubtitleItem>();

        //主语言字幕条目字典：key 是播放开始总秒数, value 是条目
        private Dictionary<int, SubtitleItem> mainSubtitleItemDict = new Dictionary<int, SubtitleItem>();

        //主语言字幕行号字典：key 是播放开始总秒数, value 是行号
        private Dictionary<int, int> mainSubtitleLineDict = new Dictionary<int, int>();

        //次语言字幕条目
        private List<SubtitleItem> subSubtitleItems = new List<SubtitleItem>();


        public void Divide(string mainLanguage, string subLanguage)
        {
            //清空原有数据
            this.mainSubtitleItemDict.Clear();
            this.mainSubtitleLineDict.Clear();
            this.subSubtitleItems.Clear();

            //重新初始化语言检测器
            LanguageDetector languageDetector = new LanguageDetector();
            if (mainLanguage.Equals(subLanguage))
            {
                languageDetector.AddLanguages(mainLanguage);
            }
            else
            {
                languageDetector.AddLanguages(mainLanguage, subLanguage);
            }

            //分离主语言和次语言
            int lineIndex = 0;
            this.subtitleItems.ToList().ForEach(subtitleItem =>
            {
                SubtitleItem main = new SubtitleItem();
                SubtitleItem sub = new SubtitleItem();

                main.StartTime = subtitleItem.StartTime;
                main.EndTime = subtitleItem.EndTime;

                sub.StartTime = subtitleItem.StartTime;
                sub.EndTime = subtitleItem.EndTime;


                if (subtitleItem.PlaintextLines.Count == 1) //有些双语字幕里，极小部分只有一行
                {
                    main.PlaintextLines.Add(subtitleItem.PlaintextLines.First());
                    sub.PlaintextLines.Add(subtitleItem.PlaintextLines.First());
                }
                else if(subtitleItem.PlaintextLines.Count > 1)
                {
                    foreach (string plaintextLine in subtitleItem.PlaintextLines)
                    {
                        string detectedLanguage = languageDetector.Detect(plaintextLine);

                        if (string.IsNullOrEmpty(detectedLanguage))
                        {
                            main.PlaintextLines.Add(plaintextLine);
                            sub.PlaintextLines.Add(plaintextLine);
                        }
                        else if (mainLanguage.Equals(detectedLanguage))
                        {
                            main.PlaintextLines.Add(plaintextLine);
                        }
                        else
                        {
                            sub.PlaintextLines.Add(plaintextLine);
                        }
                    }
                }

                int startTimeTotalSeconds = (int)TimeSpan.FromMilliseconds(main.StartTime).TotalSeconds;
                if (this.mainSubtitleItemDict.TryAdd(startTimeTotalSeconds, main))
                {
                    this.mainSubtitleLineDict.TryAdd(startTimeTotalSeconds, lineIndex);
                    lineIndex++;
                }
                //同一时间有多个字幕条目
                else 
                {
                    SubtitleItem exist = this.mainSubtitleItemDict.GetValueOrDefault(startTimeTotalSeconds);
                    exist.PlaintextLines.AddRange(main.PlaintextLines);
                }

                this.subSubtitleItems.Add(sub);
            });
        }


        public bool Has(int startTimeTotalSeconds)
        {
            return this.mainSubtitleItemDict.ContainsKey(startTimeTotalSeconds);
        }


        public int GetLineIndex(int startTimeTotalSeconds) 
        {
            return this.mainSubtitleLineDict[startTimeTotalSeconds];
        }

        public int GetNearLineIndex(int startTimeTotalSeconds)
        {
            foreach (int key in this.mainSubtitleLineDict.Keys)
            {
                if (key >= startTimeTotalSeconds)
                { 
                    return this.mainSubtitleLineDict[key];
                }
            }
            return this.mainSubtitleLineDict.Last().Value;
        }

        public string GetSpeechLine(int startTimeTotalSeconds)
        {
            return string.Join(" ", this.mainSubtitleItemDict[startTimeTotalSeconds].PlaintextLines);
        }

        public int GetSpeechLineDurationMilliseconds(int startTimeTotalSeconds) {
            return this.mainSubtitleItemDict[startTimeTotalSeconds].EndTime - this.mainSubtitleItemDict[startTimeTotalSeconds].StartTime;
        }


        public int GetFirstTotalSeconds()
        {
            return this.mainSubtitleItemDict.First().Key;
        }

        public int GetLastTotalSeconds() 
        {
            return this.mainSubtitleItemDict.Last().Key;
        }

        public List<SubtitleItem> GetSubSubtitleItems()
        {
            return this.subSubtitleItems;
        }


        public void TimeSync(int offsetSeconds)
        {
            Dictionary<int, SubtitleItem> newMainSubtitleItemDict = new Dictionary<int, SubtitleItem>();
            Dictionary<int, int> newmainSubtitleLineDict = new Dictionary<int, int>();

            int lineIndex = 0;
            foreach (SubtitleItem subtitleItem in this.mainSubtitleItemDict.Values)
            {
                subtitleItem.StartTime +=  offsetSeconds*1000;
                subtitleItem.EndTime += offsetSeconds*1000;

                int startTimeTotalSeconds = (int)TimeSpan.FromMilliseconds(subtitleItem.StartTime).TotalSeconds;
                if (newMainSubtitleItemDict.TryAdd(startTimeTotalSeconds, subtitleItem))
                {
                    newmainSubtitleLineDict.TryAdd(startTimeTotalSeconds, lineIndex);
                    lineIndex++;
                }
                //同一时间有多个字幕条目
                else
                {
                    SubtitleItem exist = newMainSubtitleItemDict.GetValueOrDefault(startTimeTotalSeconds);
                    exist.PlaintextLines.AddRange(subtitleItem.PlaintextLines);
                }
            }
            this.mainSubtitleItemDict = newMainSubtitleItemDict;
            this.mainSubtitleLineDict = newmainSubtitleLineDict;

            this.subSubtitleItems.ForEach(subtitleItem => {
                subtitleItem.StartTime += offsetSeconds * 1000;
                subtitleItem.EndTime += offsetSeconds * 1000;
            });
        }

        public override string ToString() {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (SubtitleItem subtitleItem in this.mainSubtitleItemDict.Values)
            {
                TimeSpan start = TimeSpan.FromMilliseconds(subtitleItem.StartTime);
                stringBuilder.Append($"({start:hh\\:mm\\:ss}) ");

                stringBuilder.Append(string.Join(" ", subtitleItem.PlaintextLines));

                stringBuilder.Append(Environment.NewLine);
            }

            if (stringBuilder.Length >= Environment.NewLine.Length)
            {
                stringBuilder.Length -= Environment.NewLine.Length;
            }

            return stringBuilder.ToString();
        }
    }
}