using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace MillionHerosHelper
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            textBox_X.Text = Config.CutX.ToString();
            textBox_Y.Text = Config.CutY.ToString();
            textBox_Height.Text = Config.CutHeight.ToString();
            textBox_Width.Text = Config.CutWidth.ToString();
            checkBox_PCScreen.Checked = Config.UseEmulator;
            checkBox_RemoveUselessInfo.Checked = Config.RemoveUselessInfo;

            textBox_API_KEY.Text = Config.OCR_API_KEY;
            textBox_SECRET_KEY.Text = Config.OCR_SECRET_KEY;

            switch(Config.HighlightMode)
            {
                case HLMode.NoHighLighting:
                    radioButton_NoHighLighting.Checked = true;
                    break;
                case HLMode.Fast:
                    radioButton_Fast.Checked = true;
                    break;
                case HLMode.Compatible:
                    radioButton_Compatible.Checked = true;
                    break;
                default:
                    radioButton_Fast.Checked = true;
                    break;
            }
        }

        private void button_HowToConnect_Click(object sender, EventArgs e)
        {
            const string msg =
                "1.安装手机对应的驱动程序(百度 手机型号+驱动)" + "\r\n\r\n" +
                "2.在安卓手机中进行如下操作:" + "\r\n" +
                "  点击 设置-关于手机-版本号(点击5次)" + "\r\n" +
                "  回退到设置界面-点击开发者选项-勾选USB调试" + "\r\n\r\n" +
                "3.使用数据线连接上电脑,点击检查连接,OK";
            MessageBox.Show(msg, "步骤", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button_CheckConnect_Click(object sender, EventArgs e)
        {
            if (ADB.CheckConnect())
            {
                label_ConnectStatus.Text = "连接成功";
                label_ConnectStatus.ForeColor = Color.Green;
            }
            else
            {
                label_ConnectStatus.Text = "未连接";
                label_ConnectStatus.ForeColor = Color.Red;
            }
        }

        private void button_GetScreenShort_Click(object sender, EventArgs e)
        {
            int x = 0, y = 0, width = 2, height = 2;
            try
            {
                x = int.Parse(textBox_X.Text);
                y = int.Parse(textBox_Y.Text);
                width = int.Parse(textBox_Width.Text);
                height = int.Parse(textBox_Height.Text);
            }
            catch
            {
                MessageBox.Show("截图信息格式不正确,必须为数字");
                return;
            }

            byte[] screenShot;
            try
            {
                if (checkBox_PCScreen.Checked)
                {
                    screenShot = BitmapOperation.CutScreen(new Point(x, y), new Size(width, height));
                }
                else
                {
                    if (!ADB.CheckConnect())
                    {
                        label_ConnectStatus.Text = "未连接";
                        label_ConnectStatus.ForeColor = Color.Red;
                        MessageBox.Show("连接手机失败,请按照步骤1配置!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string screenShotPath = ADB.GetScreenshotPath();
                    screenShot = BitmapOperation.CutImage(screenShotPath, new Point(x, y), new Size(width, height));

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("图像信息错误，请检查图片大小是否超出设备分辨率\r\n\r\n详细信息:\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            MemoryStream memoryStream = new MemoryStream(screenShot);
            pictureBox_ScreenShot.Image = Image.FromStream(memoryStream);
            memoryStream.Close();
        }

        private void button_SaveConfig_Click(object sender, EventArgs e)
        {
            int x = 0, y = 0, width = 2, height = 2;
            try
            {
                x = int.Parse(textBox_X.Text);
                y = int.Parse(textBox_Y.Text);
                width = int.Parse(textBox_Width.Text);
                height = int.Parse(textBox_Height.Text);
            }
            catch
            {
                MessageBox.Show("截图信息格式不正确,必须为数字");
                return;
            }

            Config.CutX = x;
            Config.CutY = y;
            Config.CutHeight = height;
            Config.CutWidth = width;
            Config.UseEmulator = checkBox_PCScreen.Checked;
            Config.RemoveUselessInfo = checkBox_RemoveUselessInfo.Checked;
            Config.OCR_API_KEY = textBox_API_KEY.Text;
            Config.OCR_SECRET_KEY = textBox_SECRET_KEY.Text;
            BaiDuOCR.InitBaiDuOCR(textBox_API_KEY.Text, textBox_SECRET_KEY.Text);

            if (radioButton_NoHighLighting.Checked)
            {
                Config.HighlightMode = HLMode.NoHighLighting;
            }
            else if (radioButton_Compatible.Checked) 
            {
                Config.HighlightMode = HLMode.Compatible;
            }
            else
            {
                Config.HighlightMode = HLMode.Fast;
            }

            Config.SaveConfig();
            MessageBox.Show("保存成功", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel_Apply_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://cloud.baidu.com/product/ocr/general");
        }

        private void button_GetArea_Click(object sender, EventArgs e)
        {
            Image screenShot;
            try
            {
                if (checkBox_PCScreen.Checked)
                {
                    byte[] temp = BitmapOperation.CutScreen(new Point(0, 0), Screen.AllScreens[0].Bounds.Size);
                    screenShot = Image.FromStream(new MemoryStream(temp));
                }
                else
                {
                    if (!ADB.CheckConnect())
                    {
                        label_ConnectStatus.Text = "未连接";
                        label_ConnectStatus.ForeColor = Color.Red;
                        MessageBox.Show("连接手机失败,请按照步骤1配置!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    byte[] temp = ADB.GetScreenshot();
                    screenShot = Image.FromStream(new MemoryStream(temp));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("图像信息错误，请检查是否连接成功\r\n\r\n详细信息:\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SelectScreenAreaForm selectScreenAreaForm = new SelectScreenAreaForm(screenShot);
            selectScreenAreaForm.SetArea += new SelectScreenAreaForm.SetAreaHandel(SetCutScreenArea);
            selectScreenAreaForm.Show();
            selectScreenAreaForm.Focus();
            this.Hide();
        }

        public void SetCutScreenArea(int x, int y, int width, int height)
        {
            textBox_X.Text = x.ToString();
            textBox_Y.Text = y.ToString();
            textBox_Height.Text = height.ToString();
            textBox_Width.Text = width.ToString();
            this.Show();
        }

        private void button_SaveCut_Click(object sender, EventArgs e)
        {
            string data = textBox_X.Text + "|" + textBox_Y.Text + "|" + textBox_Height.Text + "|" + textBox_Width.Text;
            if(saveFileDialog_Main.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(saveFileDialog_Main.FileName, data);
                }
                catch
                {
                    MessageBox.Show("保存失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_LoadCut_Click(object sender, EventArgs e)
        {
            if(openFileDialog_Main.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string data = File.ReadAllText(openFileDialog_Main.FileName, Encoding.Default);
                    string[] args = data.Split('|');
                    if (args.Length != 4)
                    {
                        MessageBox.Show("数据错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    textBox_X.Text = args[0];
                    textBox_Y.Text = args[1];
                    textBox_Height.Text = args[2];
                    textBox_Width.Text = args[3];
                }
                catch
                {
                    MessageBox.Show("读取失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
