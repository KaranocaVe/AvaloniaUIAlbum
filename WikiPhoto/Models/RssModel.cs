using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CodeHollow.FeedReader;

namespace WikiPhoto.Models
{
    public class RssModel
    {
        public async Task<IEnumerable<RssPhotoItem>> GetImageUrlFromRss(string url)
        {
            Feed? response = FeedReader.ReadAsync("https://commons.wikimedia.org/w/api.php?action=featuredfeed&feed=potd&feedformat=atom&language=zh").Result;
            List<RssPhotoItem> result = new();


            foreach (FeedItem? variable in response.Items)
            {
                string getMonth = @"\d+";
                string getDay = @"\d+";
                string getImgUrl = @"srcset=""[^""]*\s(\S+)\s\d+x""";
                string getInfo = @"<div[^>]*description[^>]*>.*?<\/div>";
                string cleanInfo = @"<[^>]+>";

                
                MatchCollection monthMatch = Regex.Matches(variable.Title, getMonth);
                MatchCollection dayMatch = Regex.Matches(variable.Title, getDay);
                Match imgMatch = Regex.Match(variable.Description, getImgUrl);
                Match infoMatch = Regex.Match(variable.Description, getInfo);
                
                if (monthMatch.Count>0 && dayMatch.Count>0 && imgMatch.Success && infoMatch.Success)
                {
                    string month = monthMatch[0].Value;
                    string day = dayMatch[1].Value;
                    string imageUrl = imgMatch.Groups[1].Value;
                    string info = Regex.Replace(infoMatch.Value, cleanInfo, "");
                    result.Add(new RssPhotoItem
                    {
                        month = int.Parse(month),
                        day = int.Parse(day),
                        imageUrl = imageUrl,
                        info = info
                    });
                }
            }
            return result;
        }

        public class RssPhotoItem
        {
            public int day;
            public string imageUrl;
            public string info;
            public int month;
        }
    }
}
