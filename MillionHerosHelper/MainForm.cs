using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;

namespace MillionHerosHelper
{
    public partial class MainForm : Form
    {

        private ConfigForm configForm;
        private BrowserForm browserForm;
        private Thread solveProblemThread;
        
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //便于并发请求同时进行
            ServicePointManager.MaxServicePoints = 512;
            ServicePointManager.DefaultConnectionLimit = 512;
            System.Net.ServicePointManager.DefaultConnectionLimit = 64;

            //禁用跨线程UI操作检查
            Control.CheckForIllegalCrossThreadCalls = false;

            Config.LoadConfig();

            BaiDuOCR.InitBaiDuOCR(Config.OCR_API_KEY, Config.OCR_SECRET_KEY);

            browserForm = new BrowserForm();
            browserForm.Show();
            MainForm_Move(null, null);

            HotKey.RegisterHotKey(Handle, 100, HotKey.KeyModifiers.None, Keys.F7);
        }

        private void button_Config_Click(object sender, EventArgs e)
        {
            if (configForm == null || configForm.IsDisposed)
            {
                configForm = new ConfigForm();
                configForm.Show();
                configForm.Focus();
            }
            else
            {
                configForm.WindowState = FormWindowState.Normal;
                configForm.Focus();
            }
        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            button_Config.Enabled = false;
            button_Start.Enabled = false;
            solveProblemThread = new Thread(new ThreadStart(BeginSolveProblem));
            solveProblemThread.Start();

            int timeUsed = 0;
            System.Timers.Timer monitor = new System.Timers.Timer(100);//答题时间监视
            monitor.Elapsed += (object _sender, System.Timers.ElapsedEventArgs _args) =>
            {
                if (solveProblemThread == null)
                {
                    monitor.Stop();
                    monitor.Close();
                }
                else if (timeUsed > 22000)
                {
                    //solveProblemThread.Abort();
                    FinishSolveProblem();
                    label_Message.Text = "题目分析超时";
                    label_Message.ForeColor = Color.Red;
                    MessageBox.Show("答题过程超过22秒,自动终止.\r\n请确保您的网络环境良好!", "执行超时", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    timeUsed += 100;
                }
            };
            monitor.Start();
        }

        private void BeginSolveProblem()
        {
            try
            {
                SolveProblem();
            }
            catch(ADBException ex)
            {
                label_Message.Text = "手机连接出错";
                label_Message.ForeColor = Color.Red;
                MessageBox.Show("请确保已连接手机并配置正确" + "\r\n\r\n详情:\r\n" + ex.ToString(), "ADB手机连接错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(OCRException ex)
            {
                label_Message.Text = "题目识别出错";
                label_Message.ForeColor = Color.Red;
                MessageBox.Show("请确保手机在题目界面" + "\r\n\r\n详情:\r\n" + ex.ToString(), "文本识别错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(APIException ex)
            {
                label_Message.Text = "网络连接出错";
                label_Message.ForeColor = Color.Red;
                MessageBox.Show("请确保网络连接正常以及API可用" + "\r\n\r\n详情:\r\n" + ex.ToString(), "API错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(IndexOutOfRangeException ex)
            {
                label_Message.Text = "题目识别出错";
                label_Message.ForeColor = Color.Red;
                MessageBox.Show("请确保手机在题目界面" + "\r\n\r\n详情:\r\n" + ex.ToString(), "解析错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(WebException ex)
            {
                label_Message.Text = "网络连接出错";
                label_Message.ForeColor = Color.Red;
                MessageBox.Show("请确保网络环境良好" + "\r\n\r\n详情:\r\n" + ex.ToString(), "网络错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(Exception ex)
            {
                label_Message.Text = "未知错误";
                label_Message.ForeColor = Color.Red;
                MessageBox.Show(ex.ToString(), "未知错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                FinishSolveProblem();
            }
        }


        private void FinishSolveProblem()
        {
            button_Config.Enabled = true;
            button_Start.Enabled = true;
            solveProblemThread = null;
        }

        private void MainForm_Move(object sender, EventArgs e)
        {
            if (browserForm != null && !browserForm.IsDisposed) 
            {
                browserForm.Location = new Point(this.Location.X + this.Width + 10, browserForm.Location.Y);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Config.SaveConfig();
            HotKey.UnregisterHotKey(Handle, 100);
        }

        private void linkLabel_Author_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/Azure99");
        }

        private void linkLabel_SourceCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/Azure99/MillionHerosHelper");
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            if (m.Msg == WM_HOTKEY)
            {
                if (m.WParam.ToInt32() == 100) 
                {
                    if(button_Start.Enabled)
                    {
                        button_Start_Click(null, null);
                    }
                }
            }

            base.WndProc(ref m);
        }

        private void SolveProblem()//答题
        {
            if(!checkBox_InPutProblem.Checked)
            {
                label_Message.Text = "正在获取手机界面";
                label_Message.ForeColor = Color.Orange;
                //获取屏幕截图
                string screenShotPath;
                byte[] smallScreenShot;
                try
                {
                    if (Config.UseEmulator)//是否为模拟器
                    {
                        smallScreenShot = BitmapOperation.CutScreen(new Point(Config.CutX, Config.CutY), new Size(Config.CutWidth, Config.CutHeight));
                    }
                    else
                    {
                        screenShotPath = ADB.GetScreenshotPath();
                        smallScreenShot = BitmapOperation.CutImage(screenShotPath, new Point(Config.CutX, Config.CutY), new Size(Config.CutWidth, Config.CutHeight));
                        System.IO.File.Delete(screenShotPath);
                    }
                }
                catch (Exception ex)
                {
                    throw new ADBException("获取的屏幕截图无效!" + ex);
                }

                label_Message.Text = "正在识别题目信息";
                //调用API识别文字
                string recognizeResult = BaiDuOCR.Recognize(smallScreenShot);

                string[] recRes = Regex.Split(recognizeResult, "\r\n|\r|\n");
                //检查识别结果正确性
                CheckOCRResult(recRes);
                //显示识别结果

                int notEmptyIndex = recRes.Length - 1;
                while (String.IsNullOrEmpty(recRes[notEmptyIndex]))//忽略空行
                {
                    notEmptyIndex--;
                }

                textBox_AnswerC.Text = recRes[notEmptyIndex--];
                textBox_AnswerB.Text = recRes[notEmptyIndex--];
                textBox_AnswerA.Text = recRes[notEmptyIndex--];

                string problem = recRes[0];

                int dotP = problem.IndexOf('.');
                if (dotP != -1)
                {
                    problem = problem.Substring(dotP + 1, problem.Length - dotP - 1);
                }

                for (int i = 1; i <= notEmptyIndex; i++)
                {
                    problem += recRes[i];
                }

                textBox_Problem.Text = problem;
            }


            //浏览器跳转到搜索页面
            browserForm.Jump("http://www.baidu.com/s?wd=" + textBox_Problem.Text);
            browserForm.Show();
            browserForm.WindowState = FormWindowState.Normal;

            label_Message.Text = "正在分析题目";
            //分析问题
            string[] answerArr = new string[] { textBox_AnswerA.Text, textBox_AnswerB.Text, textBox_AnswerC.Text };
            AnalyzeResult aRes = AnalyzeProblem.Analyze(textBox_Problem.Text, answerArr);
            char[] ans = new char[3] { 'A', 'B', 'C' };
            label_Message.Text = "最有可能选择:" + ans[aRes.Index] + "项!" + answerArr[aRes.Index];
            if (aRes.Oppose)
            {
                label_Message.Text += "(包含否定词)";
            }

            label_Message.ForeColor = Color.Green;
            label_AnalyzeA.ForeColor = Color.DarkGreen;
            label_AnalyzeB.ForeColor = Color.DarkGreen;
            label_AnalyzeC.ForeColor = Color.DarkGreen;


            switch(aRes.Index)
            {
                case 0: label_AnalyzeA.ForeColor = Color.Red;break;
                case 1: label_AnalyzeB.ForeColor = Color.Red; break;
                case 2: label_AnalyzeC.ForeColor = Color.Red; break;
            }

            //显示概率
            label_AnalyzeA.Text = "概率:" + aRes.Probability[0].ToString() + "%";
            label_AnalyzeB.Text = "概率:" + aRes.Probability[1].ToString() + "%";
            label_AnalyzeC.Text = "概率:" + aRes.Probability[2].ToString() + "%";
        }

        private void CheckOCRResult(string[] arr)
        {
            if (arr.Length > 9)
            {
                throw new OCRException("识别到的文本过多");
            }
            else if (arr.Length > 0 && arr.Length < 4)
            {
                throw new OCRException("识别到的文本过少");
            }
            else if (arr.Length == 0)
            {
                throw new OCRException("没有识别到文本");
            }
        }

        private void checkBox_InPutProblem_CheckedChanged(object sender, EventArgs e)
        {
            textBox_Problem.ReadOnly = !checkBox_InPutProblem.Checked;
            textBox_AnswerA.ReadOnly = !checkBox_InPutProblem.Checked;
            textBox_AnswerB.ReadOnly = !checkBox_InPutProblem.Checked;
            textBox_AnswerC.ReadOnly = !checkBox_InPutProblem.Checked;
        }
    }
}
