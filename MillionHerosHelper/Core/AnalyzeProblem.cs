using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading;

namespace MillionHerosHelper
{
    static class AnalyzeProblem
    {
        private static string problemData;
        private static string[] answers;
        private static int problemCnt;
        private static int[] answerCnt;
        private static int[] problemAndAnswerCnt;
        /// <summary>
        /// 答案推荐算法
        /// </summary>
        /// <param name="problem">题目</param>
        /// <param name="answerArr">选项</param>
        public static AnalyzeResult Analyze(string problem, string[] answerArr)
        {
            //是否存在否定关键词
            bool oppose = Regex.IsMatch(problem, "不是|不属于|不包括|不可以|不包含|不需要|错误|没有|不会|不可能|找不到");
            answers = answerArr;
            //移除部分干扰搜索的关键字
            problem = RemoveUselessInfoAndPrivative(problem);

            #region 多线程获取信息
            Thread[] workThread = new Thread[1 + answerArr.Length * 2];//工作线程

            workThread[0] = new Thread(new ParameterizedThreadStart(WorkThread));
            WorkArgs args;

            args.Type = TaskType.GetProblemData;
            args.Index = -1;
            args.Text = problem;
            workThread[0].Start(args);

            answerCnt = new int[answerArr.Length];
            for (int i = 0; i < answerArr.Length; i++) 
            {
                args.Type = TaskType.AnswerCnt;
                args.Index = i;
                args.Text = answerArr[i];
                workThread[i + 1] = new Thread(new ParameterizedThreadStart(WorkThread));
                workThread[i + 1].Start(args);
            }

            problemAndAnswerCnt = new int[answerArr.Length];
            for (int i = 0; i < answerArr.Length; i++) 
            {
                args.Type = TaskType.ProblemAndAnswerCnt;
                args.Index = i;
                args.Text = problem + " " + "\"" + answerArr[i] + "\"";
                workThread[i + 1 + answerArr.Length] = new Thread(new ParameterizedThreadStart(WorkThread));
                workThread[i + 1 + answerArr.Length].Start(args);
            }

            while(true)//等待所有线程执行完毕
            {
                bool allExited = true;
                for (int i = 0; i < workThread.Length; i++) 
                {
                    if(workThread[i].IsAlive)
                    {
                        allExited = false;
                        break;
                    }
                }

                if (allExited)
                    break;
                else
                    Thread.Sleep(50);
            }
            #endregion

            #region 计算权值

            double[] pmiRank = CalculateRank.CalculatePMIRank(problemCnt, answerCnt, problemAndAnswerCnt);//PMI算法得到的权值
            double[] cntRank = CalculateRank.CalculateCountRank(problem, answerArr, pmiRank, problemData);///计算算法得到的权值
            double[] sumRank = new double[answerArr.Length];

            double pmiAddCnt = 0;//权值和
            for (int i = 0; i < answerArr.Length; i++)
            {
                pmiAddCnt += pmiRank[i] + cntRank[i];
            }

            int trueAnswerIndex = SearchTureAnswer(problemData, answerArr);//检索绝对正确的答案
            Debug.WriteLine("权威答案:" + trueAnswerIndex);

            if (trueAnswerIndex != -1)//匹配到权威答案
            {
                sumRank[trueAnswerIndex] = pmiAddCnt;
            }
            else//检索百度知道
            {
                if (SearchInBaiduZhiDao(problemData, answerArr, out int baiDuZhiDaoAnswerIndex, out int zdRatio))
                {
                    sumRank[baiDuZhiDaoAnswerIndex] += (double)pmiAddCnt * zdRatio / 100;
                    Debug.WriteLine(sumRank[baiDuZhiDaoAnswerIndex]);
                    Debug.WriteLine("百度知道答案:" + baiDuZhiDaoAnswerIndex + " Ratio:" + zdRatio);
                }
            }
            #endregion
            int maxIndex = 0;
            int minIndex = 0;
            for (int i = 0; i < answerArr.Length; i++)
            {
                sumRank[i] += pmiRank[i] + cntRank[i];
                if (sumRank[i] > sumRank[maxIndex])
                {
                    maxIndex = i;
                }

                if (sumRank[i] < sumRank[minIndex])
                {
                    minIndex = i;
                }
            }

            #region 计算概率
            double probabilitySum = 0;//权值和

            for (int i = 0; i < cntRank.Length; i++) 
            {
                probabilitySum += sumRank[i];
            }

            int[] probability = new int[answerArr.Length];
            for (int i = 0; i < answerArr.Length; i++)
            {
                probability[i] = (int)(sumRank[i] / probabilitySum * 100);
            }
            #endregion

            AnalyzeResult ar;//分析结果
            ar.CntRank = cntRank;
            ar.PMIRank = pmiRank;
            ar.Index = maxIndex;
            if (oppose) //存在否定词则取最小
            {
                ar.Index = minIndex;
            }
            ar.SumRank = sumRank;
            ar.Probability = probability;
            ar.Oppose = oppose;
            return ar;
        }

        private static void WorkThread(object arg)//获取数据的工作线程
        {
            WorkArgs args = (WorkArgs)arg;
            if(args.Type == TaskType.GetProblemData)//获取题目在文本库中出现概率
            {
                problemCnt = SearchEngine.StatisticsKeyword(args.Text, out problemData);
                BrowserForm.browserForm.HighlightAndShowPage(problemData, answers);//显示高亮搜索页面
            }
            else//获取选项在文本库中出现概率
            {
                int[] arr = (args.Type == TaskType.AnswerCnt) ? answerCnt : problemAndAnswerCnt;
                arr[args.Index] = SearchEngine.StatisticsKeyword(args.Text);
            }
        }

        /// <summary>
        /// 算法修正,移除无用信息并将否定换为肯定
        /// </summary>
        public static string RemoveUselessInfoAndPrivative(string str)
        {
            str = RemoveUselessInfo(str);
            //"不是", "不属于", "不包括", "不可以", "不包含", "不需要", "错误", "没有", "不会" 
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("不是", "是");
            dic.Add("不属于", "属于");
            dic.Add("不包括", "包括");
            dic.Add("不可以", "可以");
            dic.Add("不包含", "包含");
            dic.Add("不需要", "需要");
            dic.Add("错误", "正确");
            dic.Add("没有", "有");
            dic.Add("不会", "会");
            dic.Add("不可能", "可能");
            dic.Add("找不到", "能找到");
            foreach (string key in dic.Keys)
            {
                str = str.Replace(key, dic[key]);
            }
            Debug.WriteLine(str);
            return str;
        }

        /// <summary>
        /// 移除干扰选择的选项
        /// </summary>
        public static string RemoveUselessInfo(string str)
        {

            if (str.Length >= 38)
            {
                if (str.Contains("请问"))
                {
                    int p = str.IndexOf("请问");
                    str = str.Substring(p, str.Length - p);
                }
            }

            string[] dic = new string[] { "“", "”", "\"", "以下", "下列", "哪项", "选项" };
            string res = str;
            foreach (string key in dic)
            {
                res = res.Replace(key, "");
            }
            res = res.Replace("？", "?");
            return res;
        }

        /// <summary>
        /// 移除选项开头的ABC
        /// </summary>
        public static string RemoveABC(string str)
        {
            string res = str.Trim();
            res = Regex.Replace(res, @"^(A|B|C|a|b|c)", "");
            res = Regex.Replace(res, @"^(\.|:|：|，|,)", "");
            res = res.Trim();
            return res;
        }

        #region 精确分析百度数据
        //看不懂请自行抓取百度页面分析
        
        /// <summary>
        /// 依赖百度百科、图谱、计算器等, 寻找是否存在确定的答案
        /// </summary>
        /// <param name="data">整个搜索页面</param>
        /// <param name="answerArr">选项数组</param>
        /// <returns>找到返回下标,未找到返回-1</returns>
        public static int SearchTureAnswer(string data, string[] answerArr)
        {
            int p;

            #region 百度计算器
            p = data.IndexOf("mu=\"http://open.baidu.com/static/calculator/calculator.html\"");
            if (p != -1) //百度计算器 
            {
                if (CountItemsBeforeP(data, p) < 2)//确保词条在前两项
                {
                    //定位
                    p = data.IndexOf("<p class=\"op_new_val_screen_result\">", p);
                    if (p != -1)
                    {
                        int startP = data.IndexOf("<span>", p);
                        if (startP != -1)
                        {
                            int endP = data.IndexOf("</span>", startP);
                            if (endP != -1)
                            {
                                string ans = data.Substring(startP + "<span>".Length, endP - (startP + "<span>".Length));
                                ans = ans.Replace(" ", "");
                                int existCnt = 0;//正确答案存在个数
                                int index = 0;//正确答案下标
                                for (int i = 0; i < answerArr.Length; i++)
                                {
                                    if (ans.Contains(answerArr[i]))
                                    {
                                        existCnt++;
                                        index = i;
                                    }
                                }

                                if (existCnt == 1) //存在个数，只有一个才能确保是正确选项
                                {
                                    Debug.WriteLine("匹配到:百度计算器");
                                    return index;
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            #region 百度汉语
            p = data.IndexOf("<div class=\"op_exactqa_detail_s_answer\">");
            if (p != -1)
            {
                if (CountItemsBeforeP(data, p) < 2)
                {
                    const string startStr = "target=\"_blank\">";
                    const string endStr = "</a></span>";

                    int startP = data.IndexOf(startStr, p);
                    if (startP != -1) 
                    {
                        int endP = data.IndexOf(endStr, startP);
                        if (endP != -1)
                        {
                            string ans = data.Substring(startP + startStr.Length, endP - (startP + endStr.Length));
                            int existCnt = 0;
                            int index = 0;
                            for (int i = 0; i < answerArr.Length; i++)
                            {
                                if (ans.Contains(answerArr[i]))
                                {
                                    existCnt++;
                                    index = i;
                                }
                            }

                            if (existCnt == 1) 
                            {
                                Debug.WriteLine("匹配到:百度汉语");
                                return index;
                            }
                        }
                    }
                }
            }
            #endregion


            #region 百度知识图谱

            p = data.IndexOf("data-compress=\"off\">");
            if (p != -1)
            {
                p = data.IndexOf("setup({", p);
                if (p != -1) 
                {
                    if (CountItemsBeforeP(data, p) < 2)
                    {
                        const string startStr = "fbtext: '";
                        const string endStr = "',";

                        int startP = data.IndexOf(startStr, p);
                        if (startP != -1)
                        {
                            int endP = data.IndexOf(endStr, startP);
                            if (endP != -1)
                            {
                                string ans = data.Substring(startP + startStr.Length, endP - (startP + endStr.Length));
                                int existCnt = 0;
                                int index = 0;
                                for (int i = 0; i < answerArr.Length; i++)
                                {
                                    if (ans.Contains(answerArr[i]))
                                    {
                                        existCnt++;
                                        index = i;
                                    }
                                }

                                if (existCnt == 1) 
                                {
                                    Debug.WriteLine("匹配到:百度知识图谱");
                                    return index;
                                }
                            }
                        }
                    }
                }
            }

            #endregion

            #region 百度百科
            p = data.IndexOf("mu=\"https://baike.baidu.com/item/");
            if (p != -1)
            {
                if (CountItemsBeforeP(data, p) < 3)
                {
                    const string startStr = "百度百科</a>";
                    const string endStr = "baike.baidu.com";

                    int startP = data.IndexOf(startStr, p);
                    int endP = data.IndexOf(endStr, startP);
                    if (startP != -1 && endP != -1)
                    {
                        string ans = data.Substring(startP + startStr.Length, endP - (startP + endStr.Length));
                        int existCnt = 0;
                        int index = 0;
                        for (int i = 0; i < answerArr.Length; i++)
                        {
                            if (ans.Contains(answerArr[i]))
                            {
                                existCnt++;
                                index = i;
                            }
                        }

                        if (existCnt == 1)
                        {
                            Debug.WriteLine("匹配到:百度百科");
                            return index;
                        }
                    }
                }
            }
            #endregion

            #region 百度知道最佳答案
            p = data.IndexOf("<div class=\"op_best_answer_content\">");
            if (p != -1)
            {
                if (CountItemsBeforeP(data, p) < 2)
                {
                    const string startStr = "<div class=\"op_best_answer_content\">";
                    const string endStr = "<div class=\"op_best_answer_source c-clearfix\">";
                    int startP = p;
                    if (startP != -1)
                    {
                        int endP = data.IndexOf(endStr, startP);
                        if (endP != -1)
                        {
                            string ans = data.Substring(startP + startStr.Length, endP - (startP + endStr.Length));
                            int existCnt = 0;
                            int index = 0;
                            for (int i = 0; i < answerArr.Length; i++)
                            {
                                if (ans.Contains(answerArr[i]))
                                {
                                    existCnt++;
                                    index = i;
                                }
                            }

                            if (existCnt == 1)
                            {
                                Debug.WriteLine("匹配到:百度知道最佳答案");
                                return index;
                            }
                        }
                    }
                }
            }
            #endregion

            return -1;
        }

        public static bool SearchInBaiduZhiDao(string data, string[] answerArr,out int index, out int ratio)
        {
            const string startStr = "百度知道</a></h3><";
            const string endStr = "https://zhidao.baidu.com/que";
            int startP = data.IndexOf(startStr);
            for (int i = 0; i < 3 && startP != -1; i++)
            {
                int endP = data.IndexOf("https://zhidao.baidu.com/que", startP);
                if (endP != -1)
                {
                    int beforeIt = CountItemsBeforeP(data, startP);
                    if (beforeIt >= 3)
                    {
                        break;
                    }

                    string ans = data.Substring(startP + startStr.Length, endP - (startP + endStr.Length));
                    int existCnt = 0;//正确答案存在个数
                    int ind = 0;//正确答案下标
                    for (int i2 = 0; i2 < answerArr.Length; i2++)
                    {
                        if (ans.Contains(answerArr[i2]))
                        {
                            existCnt++;
                            ind = i2;
                        }
                    }

                    if (existCnt == 1) //存在个数，只有一个才能确保是正确选项
                    {
                        Debug.WriteLine("匹配到:百度知道概率");
                        index = ind;
                        ratio = 70 - 15 * (beforeIt - 1);
                        return true;
                    }
                }
            }
            index = -1;
            ratio = 0;
            return false;
        }

        /// <summary>
        /// 计算在P位置前面共有多少个搜索结果
        /// </summary>
        /// <param name="data">整个搜索结果页面</param>
        /// <param name="p">搜寻的位置</param>
        /// <returns></returns>
        public static int CountItemsBeforeP(string data, int p)
        {
            int cnt = 0;
            int searchP = data.IndexOf(Properties.Resources.Str_BeforeItem);
            while (searchP != -1 && searchP <= p) 
            {
                cnt++;
                searchP = data.IndexOf(Properties.Resources.Str_BeforeItem, searchP + Properties.Resources.Str_BeforeItem.Length);
            }
            return cnt;
        }
        #endregion
    }
}
