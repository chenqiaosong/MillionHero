using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
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

            string data = GetSearchString("http://www.baidu.com/s?wd=" + UrlEncode(keyword));

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
            string data = GetSearchString("http://www.baidu.com/s?wd=" + UrlEncode(keyword));

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
            string result;
            var uri = new Uri(url);
            HttpWebRequest webrequest = null;
            HttpWebResponse webresponse = null;

            try
            {
                
                webrequest = (HttpWebRequest)WebRequest.Create(uri);

                webrequest.Proxy = null;

                webrequest.KeepAlive = false;
                webrequest.Timeout = 10000;
                webrequest.AllowWriteStreamBuffering = false;
                webrequest.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");

                webresponse = (HttpWebResponse)webrequest.GetResponse();
                if (webresponse.ContentEncoding.ToLower().Contains("gzip"))
                {
                    using (GZipStream gZipStream = new GZipStream(webresponse.GetResponseStream(), CompressionMode.Decompress))
                    {
                        using (StreamReader sr = new StreamReader(gZipStream))
                        {
                            result = sr.ReadToEnd();
                        }
                    }
                }
                else
                {
                    using (Stream stream = webresponse.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
                        {
                            result = sr.ReadToEnd();
                        }
                    }

                }
            }
            catch
            {
                result = "";
            }
            finally
            {
                if (webresponse != null)
                {
                    webresponse.Close();
                }
                if (webrequest != null)
                {
                    webrequest.Abort();
                }
                webresponse = null;
                webrequest = null;
            }

           
            return result;
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
