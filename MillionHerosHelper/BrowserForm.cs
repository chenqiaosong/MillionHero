using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using mshtml;

namespace MillionHerosHelper
{
    public partial class BrowserForm : Form
    {
        private string[] answers;
        private string[] color = new string[] { "yellow", "limegreen", "lightblue" };
        private char[] option = new char[] { 'A', 'B', 'C' };
        public static BrowserForm browserForm;

        public BrowserForm()
        {
            InitializeComponent();
        }

        private void BrowserForm_Load(object sender, EventArgs e)
        {
            browserForm = this;
        }

        public void Jump(string url)
        {
            webBrowser_Main.Url = new Uri(url);
        }

        public void HighlightAndShowPage(string data, string[] answerArr)
        {
            
            answers = answerArr;
            System.Diagnostics.Debug.WriteLine(answers.Length);
            webBrowser_Main.DocumentText = data;
            
        }

        private void webBrowser_Main_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            HTMLDocument document = (HTMLDocument)webBrowser_Main.Document.DomDocument;
            IHTMLDOMNode bodyNode = (IHTMLDOMNode)webBrowser_Main.Document.Body.DomElement;

            for (int i = 0; i < answers.Length; i++)
            {
                int.TryParse(answers[i], out int judge);
                if (judge > 0 && judge <= 10)
                    continue;
                HighLightingText(document, bodyNode, answers[i], i);
            }
        }

        private void HighLightingText(HTMLDocument document, IHTMLDOMNode node, string keyword, int cnt)
        {
            // nodeType = 3：text节点 
            if (node.nodeType == 3)
            {
                string nodeText = node.nodeValue.ToString();
                // 如果找到了关键字 
                if (nodeText.Contains(keyword))
                {
                    IHTMLDOMNode parentNode = node.parentNode;
                    // 将关键字作为分隔符，将文本分离，并逐个添加到原text节点的父节点 
                    string[] result = nodeText.Split(new string[] { keyword }, StringSplitOptions.None);
                    for (int i = 0; i < result.Length - 1; i++)
                    {
                        if (result[i] != "")
                        {
                            IHTMLDOMNode txtNode = document.createTextNode(option[cnt] + result[i] + option[cnt]);
                            parentNode.insertBefore(txtNode, node);
                        }
                        IHTMLDOMNode orgNode = document.createTextNode(option[cnt] + keyword + option[cnt]);
                        IHTMLDOMNode hilightedNode = (IHTMLDOMNode)document.createElement("SPAN");
                        IHTMLStyle style = ((IHTMLElement)hilightedNode).style;
                        style.color = "black";
                        style.backgroundColor = color[cnt];
                        hilightedNode.appendChild(orgNode);

                        parentNode.insertBefore(hilightedNode, node);
                    }
                    if (result[result.Length - 1] != "")
                    {
                        IHTMLDOMNode postNode = document.createTextNode(option[cnt] + result[result.Length - 1] + option[cnt]);
                        parentNode.insertBefore(postNode, node);
                    }
                    parentNode.removeChild(node);
                } // End of nodeText.Contains(keyword) 
            }
            else
            {
                // 如果不是text节点，则递归搜索其子节点 
                IHTMLDOMChildrenCollection childNodes = node.childNodes as IHTMLDOMChildrenCollection;
                foreach (IHTMLDOMNode n in childNodes)
                {
                    HighLightingText(document, n, keyword, cnt);
                }
            }
        }

    private void BrowserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
