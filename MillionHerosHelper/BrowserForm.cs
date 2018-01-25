using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace MillionHerosHelper
{
    public partial class BrowserForm : Form
    {
        public BrowserForm()
        {
            InitializeComponent();
        }

        private void BrowserForm_Load(object sender, EventArgs e)
        {

        }

        public void Jump(string url)
        {
            webBrowser_Main.Url = new Uri(url);
        }

        public void JumpAndHighlighting(string problem, string[] answer)
        {
            SearchEngine.StatisticsKeyword(problem, out string data);
;           webBrowser_Main.DocumentText = Highlighting(data, answer);
        }

        public string Highlighting(string data, string[] answers)
        {
            foreach(string answer in answers)
            {
                if(Regex.IsMatch(answer, "^[0-9]*$") || Regex.IsMatch(answer, "<|>|:|：|\""))
                {
                    return data;
                }
            }


            string[] color = new string[] { "yellow", "limegreen", "lightblue" };
            char[] chars = new char[] { 'A', 'B', 'C' };
            for (int i = 0; i < answers.Length; i++) 
            {
                data = Regex.Replace(data, Regex.Escape(answers[i]),
                    "<span style=\"background: "+color[i]+"; \">" +chars[i] + answers[i] + chars[i] + "</span>",
                    RegexOptions.IgnoreCase);
            }

            return data;
        }

        private void BrowserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
