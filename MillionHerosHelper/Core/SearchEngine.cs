using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Diagnostics;
using System.IO;
using System.Web;

namespace MillionHerosHelper
{
    static class SearchEngine
    {
        /// <summary>
        /// 取得关键字在搜索引擎文本库中的出现次数
        /// </summary>
        public static int StatisticsKeyword(string keyword)
        {
            const string strStart = "百度为您找到相关结果约";
            const string strEnd = "个";

            string data = GetSearchStringCompatible("http://www.baidu.com/s?wd=" + UrlEncode(keyword));

            int p = data.IndexOf(strStart);

            if (p == -1)
                return 1;
            int p2 = data.IndexOf(strEnd, p);
            if (p2 == -1)
                return 1;
            
            string countStr = data.Substring(p + strStart.Length, p2 - p - strStart.Length);
            countStr = countStr.Replace(",", "");
            
            Int32.TryParse(countStr, out int count);

            if (count == 0)
            {
                count = 1;
            }

            return count;
        }

        public static int StatisticsKeyword(string keyword, out string sourceData)
        {
            const string strStart = "百度为您找到相关结果约";
            const string strEnd = "个";
            string data = GetSearchStringCompatible("http://www.baidu.com/s?wd=" + UrlEncode(keyword));
            sourceData = data;

            int p = data.IndexOf(strStart);
            if (p == -1)
                return 1;
            int p2 = data.IndexOf(strEnd, p);
            if (p2 == -1)
                return 1;

            string countStr = data.Substring(p + strStart.Length, p2 - p - strStart.Length);
            countStr = countStr.Replace(",", "");

            Int32.TryParse(countStr, out int count);

            if (count == 0)
            {
                count = 1;
            }

            return count;
        }


        private static string GetSearchString(string url)
        {
            using (WebClient wc = new WebClient())
            {
                wc.Proxy = null;
                wc.Credentials = CredentialCache.DefaultCredentials;
                wc.Encoding = Encoding.UTF8;
                string str = wc.DownloadString(url);
                wc.Dispose();
                return str;
            }
        }

        private static string GetSearchStringCompatible(string url)
        {
            var uri = new Uri(url);
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(uri);
            webrequest.Proxy = null;

            webrequest.Accept = "text/html";
            webrequest.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9");
            webrequest.Headers.Add("Cache-Control", "max-age=0");
            webrequest.KeepAlive = true;
            webrequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.108 Safari/537.36";

            HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();

            Stream receiveStream = webresponse.GetResponseStream();
            Encoding enc = System.Text.Encoding.UTF8;
            StreamReader loResponseStream = new StreamReader(receiveStream, enc);

            string response = loResponseStream.ReadToEnd();

            loResponseStream.Close();
            webresponse.Close();
            return response;
        }

        /// <summary>
        /// 临时用的编码，仅编码有歧义的字符
        /// </summary>
        /// <param name="str">待编码url</param>
        /// <returns>结果</returns>
        public static string UrlEncode(string str)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("+", "%2B");
            dic.Add("#", "%23");
            dic.Add("&", "%26");
            foreach(string key in dic.Keys)
            {
                str = str.Replace(key, dic[key]);
            }

            return str;
        }

    }
}
