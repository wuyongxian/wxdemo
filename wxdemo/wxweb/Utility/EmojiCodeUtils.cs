using System;
using System.Collections.Generic;

namespace YTDInfo.XJTLU.Donate.Common.Utility
{
    public class EmojiCodeUtils
    {
        private static EmojiCodeUtils instance;

        public static EmojiCodeUtils Instance()
        {
            if (instance == null)
            {
                instance = new EmojiCodeUtils();
                instance.Init();
            }
            return instance;
        }

        private EmojiCodeUtils()
        {
            Init();
        }

        public static string[] EmojiCodes = new string[]{
            "e415", "e056", "e057", "e414", "e405", "e106", "e418", "e417", "e40d", "e404", "e40a", "e105", "e409", "e40e", "e402", "e108", "e403", "e058", "e407", "e401", "e40f", "e40b", "e406", "e413", "e411", "e412", "e410", "e107", "e059", "e416", "e408", "e40c", "e11a", "e10c", "e022", "e023", "e329", "e32e", "e335", "e337", "e336", "e13c", "e331", "e03e", "e11d", "e05a", "e00e", "e421", "e00d", "e011", "e22e", "e22f", "e231", "e230", "e00f", "e14c", "e111", "e425", "e001", "e002", "e005", "e004", "e04e", "e11c", "e003", "e04a", "e04b", "e049", "e048", "e04c", "e13d", "e43e", "e04f", "e052", "e053", "e524", "e52c", "e52a", "e531", "e050", "e527", "e051", "e10b", "e52b", "e52f", "e109", "e01a", "e52d", "e521", "e52e", "e055", "e525", "e10a", "e522", "e054", "e520", "e032", "e303", "e307", "e308", "e437", "e445", "e11b", "e448", "e033", "e112", "e325", "e312", "e310", "e126", "e008", "e03d", "e00c", "e12a", "e009", "e145", "e144", "e03f", "e116", "e10f", "e101", "e13f", "e12f", "e311", "e113", "e30f", "e42b", "e42a", "e018", "e016", "e014", "e131", "e12b", "e03c", "e041", "e322", "e10e", "e43c", "e323", "e31c", "e034", "e035", "e045", "e047", "e30c", "e044", "e120", "e33b", "e33f", "e344", "e340", "e147", "e33a", "e34b", "e345", "e01d", "e10d", "e136", "e435", "e252", "e132", "e138", "e139", "e332", "e333", "e24e", "e24f", "e537", "e41d"
        };

        private static IList<string> emojiCode = new List<string>();
        private static IList<string> emojiCodeImg = new List<string>();
        private static IList<string> emojiCodeStr = new List<string>();

        public void Init()
        {
            emojiCodeImg.Clear();
            emojiCodeStr.Clear();
            foreach (string str in EmojiCodes)
            {
                string convertUnicode = ConvertUnicode(str);
                char[] charArray = convertUnicode.ToCharArray();
                string img = string.Format("/Content/images/Emojis/emoji_{0}.png", GetEmojiId(charArray[0]));
                emojiCodeImg.Add(img);
                emojiCodeStr.Add(convertUnicode);
            }
        }

        private static int GetEmojiId(char charStr)
        {
            int i = -1;
            if ((charStr < 57345) || (charStr > 58679))
            {
                return i;
            }
            if ((charStr >= 57345) && (charStr <= 57434))
            {
                i = charStr - 57345;
            }
            else if ((charStr >= 57601) && (charStr <= 57690))
            {
                i = charStr + 'Z' - 57601;
            }
            else if ((charStr >= 57857) && (charStr <= 57939))
            {
                i = charStr + '´' - 57857;
            }
            else if ((charStr >= 58113) && (charStr <= 58189))
            {
                i = charStr + 'ć' - 58113;
            }
            else if ((charStr >= 58369) && (charStr <= 58444))
            {
                i = charStr + 'Ŕ' - 58369;
            }
            else if ((charStr >= 58625) && (charStr <= 58679))
            {
                i = charStr + 'Ơ' - 58625;
            }
            return i;
        }

        private static string ConvertUnicode(string emo)
        {
            return string.Format("{0}", Convert.ToChar(Convert.ToInt32(emo, 16)));
        }


        public IList<string> EmojiImageUri
        {
            get
            {
                return new List<string>(emojiCodeImg);
            }
        }

        public IList<string> EmojiCode
        {
            get
            {
                return new List<string>(emojiCodeStr);
            }
        }

    }

    public class EmojiItem
    {
        public string EmojiCode { get; set; }
        public Uri EmojiImageUri { get; set; }
    }
}
