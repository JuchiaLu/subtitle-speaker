using SubtitlesParser.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SubtitleSpeaker
{
    public class BilingualSubtitle
    {
        public BilingualSubtitle(IEnumerable<SubtitleItem> subtitleItems)
        {
            subtitleItems = subtitleItems.OrderBy(item => item.StartTime);
            
            int lineIndex = 0;
            subtitleItems.ToList().ForEach(subtitleItem =>
            {
                var main = new SubtitleItem();
                var other = new SubtitleItem();

                main.StartTime = subtitleItem.StartTime;
                main.EndTime = subtitleItem.EndTime;

                other.StartTime = subtitleItem.StartTime;
                other.EndTime = subtitleItem.EndTime;

                if (subtitleItem.PlaintextLines.Count >= 2)
                {
                    foreach (var plaintextLine in subtitleItem.PlaintextLines)
                    {
                        if ("zh-CN".Equals(System.Globalization.CultureInfo.CurrentCulture.Name))
                        {
                            if (Regex.IsMatch(plaintextLine, "[\u4e00-\u9fa5]")) //匹配中文
                            {
                                main.PlaintextLines.Add(plaintextLine);
                            }
                            else
                            {
                                other.PlaintextLines.Add(plaintextLine);
                            }
                        }
                        else 
                        {
                            if (Regex.IsMatch(plaintextLine, "[A-z]")) //匹配英文
                            {
                                main.PlaintextLines.Add(plaintextLine);
                            }
                            else
                            {
                                other.PlaintextLines.Add(plaintextLine);
                            }
                        }
                        
                    }
                }
                else //有些双语里面不规范,极小部分只有一行
                {
                    main.PlaintextLines = subtitleItem.PlaintextLines;
                    other.PlaintextLines = subtitleItem.PlaintextLines;
                }
                int startTimeTotalSeconds =(int)TimeSpan.FromMilliseconds(main.StartTime).TotalSeconds;
                if (this.mainLanguage.TryAdd(startTimeTotalSeconds, main))// TODO 同一时间可能有多个字幕的处理
                {
                    this.mainLanguageIndex.TryAdd(startTimeTotalSeconds, lineIndex);
                    lineIndex++;
                }
                this.otherLanguage.Add(other);
            });
        }

        // key 是播放开始总秒数, value 是字幕
        private Dictionary<int, SubtitleItem> mainLanguage = new Dictionary<int, SubtitleItem>();

        // key 是播放开始总秒数, value 是行号
        private Dictionary<int, int> mainLanguageIndex = new Dictionary<int, int>();

        private List<SubtitleItem> otherLanguage = new List<SubtitleItem>();


        public bool Has(int startTimeTotalSeconds)
        {
            return this.mainLanguage.ContainsKey(startTimeTotalSeconds);
        }

        public int GetLineIndex(int startTimeTotalSeconds) 
        {
            return this.mainLanguageIndex[startTimeTotalSeconds];
        }

        public int GetNearLineIndex(int startTimeTotalSeconds)
        {
            foreach (var key in mainLanguageIndex.Keys)
            {
                if (key >= startTimeTotalSeconds)
                    return mainLanguageIndex[key];
            }
            return mainLanguageIndex.Last().Value;
        }

        public int GetSpeechLineTotalMilliseconds(int startTimeTotalSeconds) {
            return this.mainLanguage[startTimeTotalSeconds].EndTime - this.mainLanguage[startTimeTotalSeconds].StartTime;
        }

        public string GetSpeechLine(int startTimeTotalSeconds) {
            string s = "";
            this.mainLanguage[startTimeTotalSeconds].PlaintextLines.ForEach(x => {
                s += x;
            });
            return s;
        }

        public int GetFirstTotalSeconds()
        {
            return this.mainLanguage.First().Key;
        }

        public int GetLastTotalSeconds() 
        {
            return this.mainLanguage.Last().Key;
        }

        public void TimeSync(int offsetSeconds)
        {
            Dictionary<int, SubtitleItem> newMainLanguage = new Dictionary<int, SubtitleItem>();
            Dictionary<int, int> newMainLanguageIndex = new Dictionary<int, int>();

            int lineIndex = 0;

            foreach (var subtitleItem in this.mainLanguage.Values)
            {
                subtitleItem.StartTime +=  offsetSeconds*1000;
                subtitleItem.EndTime += offsetSeconds*1000;

                int startTimeTotalSeconds = (int)TimeSpan.FromMilliseconds(subtitleItem.StartTime).TotalSeconds;
                if (newMainLanguage.TryAdd(startTimeTotalSeconds, subtitleItem))
                {
                    newMainLanguageIndex.TryAdd(startTimeTotalSeconds, lineIndex);
                    lineIndex++;
                }
            }
            this.mainLanguage = newMainLanguage;
            this.mainLanguageIndex = newMainLanguageIndex;

            this.otherLanguage.ForEach(subtitleItem => {
                subtitleItem.StartTime += offsetSeconds * 1000;
                subtitleItem.EndTime += offsetSeconds * 1000;
            });
        }

        public List<SubtitleItem> GetOtherLanguageSubtitle() 
        {
            return this.otherLanguage;
        }

        public override string ToString() {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var subtitleItem in this.mainLanguage.Values)
            {
                string formatTimecodeLine()
                {
                    TimeSpan start = TimeSpan.FromMilliseconds(subtitleItem.StartTime);
                    //TimeSpan end = TimeSpan.FromMilliseconds(subtitleItem.EndTime);
                    return $"({start:hh\\:mm\\:ss}) ";
                }

                stringBuilder.Append(formatTimecodeLine());
                foreach (var line in subtitleItem.PlaintextLines)
                {
                    stringBuilder.Append(" ");
                    stringBuilder.Append(line);
                }
                stringBuilder.AppendLine();
            }
            return stringBuilder.ToString();
        }
    }
}
