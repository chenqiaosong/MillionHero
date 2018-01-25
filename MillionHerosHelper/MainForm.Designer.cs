namespace MillionHerosHelper
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label_RankA = new System.Windows.Forms.Label();
            this.label_RankB = new System.Windows.Forms.Label();
            this.label_RankC = new System.Windows.Forms.Label();
            this.button_Config = new System.Windows.Forms.Button();
            this.button_Start = new System.Windows.Forms.Button();
            this.label_Problem = new System.Windows.Forms.Label();
            this.label_AnswerA = new System.Windows.Forms.Label();
            this.label_AnswerB = new System.Windows.Forms.Label();
            this.label_AnswerC = new System.Windows.Forms.Label();
            this.textBox_Problem = new System.Windows.Forms.TextBox();
            this.textBox_AnswerA = new System.Windows.Forms.TextBox();
            this.textBox_AnswerB = new System.Windows.Forms.TextBox();
            this.textBox_AnswerC = new System.Windows.Forms.TextBox();
            this.label_Message = new System.Windows.Forms.Label();
            this.label_AnalyzeA = new System.Windows.Forms.Label();
            this.label_AnalyzeB = new System.Windows.Forms.Label();
            this.label_AnalyzeC = new System.Windows.Forms.Label();
            this.linkLabel_Author = new System.Windows.Forms.LinkLabel();
            this.linkLabel_SourceCode = new System.Windows.Forms.LinkLabel();
            this.label_Version = new System.Windows.Forms.Label();
            this.checkBox_InPutProblem = new System.Windows.Forms.CheckBox();
            this.linkLabel_DLNewVer = new System.Windows.Forms.LinkLabel();
            this.label_Info = new System.Windows.Forms.Label();
            this.button_SearchA = new System.Windows.Forms.Button();
            this.button__SearchB = new System.Windows.Forms.Button();
            this.button__SearchC = new System.Windows.Forms.Button();
            this.button_Pay = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_RankA
            // 
            this.label_RankA.AutoSize = true;
            this.label_RankA.Location = new System.Drawing.Point(122, 199);
            this.label_RankA.Name = "label_RankA";
            this.label_RankA.Size = new System.Drawing.Size(0, 15);
            this.label_RankA.TabIndex = 10;
            // 
            // label_RankB
            // 
            this.label_RankB.AutoSize = true;
            this.label_RankB.Location = new System.Drawing.Point(122, 265);
            this.label_RankB.Name = "label_RankB";
            this.label_RankB.Size = new System.Drawing.Size(0, 15);
            this.label_RankB.TabIndex = 11;
            // 
            // label_RankC
            // 
            this.label_RankC.AutoSize = true;
            this.label_RankC.Location = new System.Drawing.Point(122, 337);
            this.label_RankC.Name = "label_RankC";
            this.label_RankC.Size = new System.Drawing.Size(0, 15);
            this.label_RankC.TabIndex = 12;
            // 
            // button_Config
            // 
            this.button_Config.Location = new System.Drawing.Point(26, 23);
            this.button_Config.Name = "button_Config";
            this.button_Config.Size = new System.Drawing.Size(89, 35);
            this.button_Config.TabIndex = 13;
            this.button_Config.Text = "配置向导";
            this.button_Config.UseVisualStyleBackColor = true;
            this.button_Config.Click += new System.EventHandler(this.button_Config_Click);
            // 
            // button_Start
            // 
            this.button_Start.Location = new System.Drawing.Point(168, 12);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(160, 60);
            this.button_Start.TabIndex = 14;
            this.button_Start.Text = "开始答题(快捷键F7)";
            this.button_Start.UseVisualStyleBackColor = true;
            this.button_Start.Click += new System.EventHandler(this.button_Start_Click);
            // 
            // label_Problem
            // 
            this.label_Problem.AutoSize = true;
            this.label_Problem.Location = new System.Drawing.Point(30, 135);
            this.label_Problem.Name = "label_Problem";
            this.label_Problem.Size = new System.Drawing.Size(45, 15);
            this.label_Problem.TabIndex = 15;
            this.label_Problem.Text = "问题:";
            // 
            // label_AnswerA
            // 
            this.label_AnswerA.AutoSize = true;
            this.label_AnswerA.Location = new System.Drawing.Point(30, 232);
            this.label_AnswerA.Name = "label_AnswerA";
            this.label_AnswerA.Size = new System.Drawing.Size(53, 15);
            this.label_AnswerA.TabIndex = 16;
            this.label_AnswerA.Text = "选项A:";
            // 
            // label_AnswerB
            // 
            this.label_AnswerB.AutoSize = true;
            this.label_AnswerB.Location = new System.Drawing.Point(30, 323);
            this.label_AnswerB.Name = "label_AnswerB";
            this.label_AnswerB.Size = new System.Drawing.Size(53, 15);
            this.label_AnswerB.TabIndex = 17;
            this.label_AnswerB.Text = "选项B:";
            // 
            // label_AnswerC
            // 
            this.label_AnswerC.AutoSize = true;
            this.label_AnswerC.Location = new System.Drawing.Point(30, 424);
            this.label_AnswerC.Name = "label_AnswerC";
            this.label_AnswerC.Size = new System.Drawing.Size(45, 15);
            this.label_AnswerC.TabIndex = 18;
            this.label_AnswerC.Text = "选项C";
            // 
            // textBox_Problem
            // 
            this.textBox_Problem.Font = new System.Drawing.Font("宋体", 11F);
            this.textBox_Problem.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox_Problem.Location = new System.Drawing.Point(100, 132);
            this.textBox_Problem.Multiline = true;
            this.textBox_Problem.Name = "textBox_Problem";
            this.textBox_Problem.ReadOnly = true;
            this.textBox_Problem.Size = new System.Drawing.Size(368, 81);
            this.textBox_Problem.TabIndex = 19;
            // 
            // textBox_AnswerA
            // 
            this.textBox_AnswerA.Font = new System.Drawing.Font("宋体", 13F);
            this.textBox_AnswerA.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox_AnswerA.Location = new System.Drawing.Point(34, 266);
            this.textBox_AnswerA.Name = "textBox_AnswerA";
            this.textBox_AnswerA.ReadOnly = true;
            this.textBox_AnswerA.Size = new System.Drawing.Size(439, 32);
            this.textBox_AnswerA.TabIndex = 20;
            // 
            // textBox_AnswerB
            // 
            this.textBox_AnswerB.Font = new System.Drawing.Font("宋体", 13F);
            this.textBox_AnswerB.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox_AnswerB.Location = new System.Drawing.Point(33, 365);
            this.textBox_AnswerB.Name = "textBox_AnswerB";
            this.textBox_AnswerB.ReadOnly = true;
            this.textBox_AnswerB.Size = new System.Drawing.Size(440, 32);
            this.textBox_AnswerB.TabIndex = 21;
            // 
            // textBox_AnswerC
            // 
            this.textBox_AnswerC.Font = new System.Drawing.Font("宋体", 13F);
            this.textBox_AnswerC.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox_AnswerC.Location = new System.Drawing.Point(33, 461);
            this.textBox_AnswerC.Name = "textBox_AnswerC";
            this.textBox_AnswerC.ReadOnly = true;
            this.textBox_AnswerC.Size = new System.Drawing.Size(440, 32);
            this.textBox_AnswerC.TabIndex = 22;
            // 
            // label_Message
            // 
            this.label_Message.AutoSize = true;
            this.label_Message.Font = new System.Drawing.Font("宋体", 13F);
            this.label_Message.Location = new System.Drawing.Point(12, 520);
            this.label_Message.Name = "label_Message";
            this.label_Message.Size = new System.Drawing.Size(395, 22);
            this.label_Message.TabIndex = 23;
            this.label_Message.Text = "答题之前记得点击配置向导进行配置哦!";
            // 
            // label_AnalyzeA
            // 
            this.label_AnalyzeA.AutoSize = true;
            this.label_AnalyzeA.Font = new System.Drawing.Font("宋体", 10F);
            this.label_AnalyzeA.ForeColor = System.Drawing.Color.DarkGreen;
            this.label_AnalyzeA.Location = new System.Drawing.Point(322, 230);
            this.label_AnalyzeA.Name = "label_AnalyzeA";
            this.label_AnalyzeA.Size = new System.Drawing.Size(0, 17);
            this.label_AnalyzeA.TabIndex = 24;
            // 
            // label_AnalyzeB
            // 
            this.label_AnalyzeB.AutoSize = true;
            this.label_AnalyzeB.Font = new System.Drawing.Font("宋体", 10F);
            this.label_AnalyzeB.ForeColor = System.Drawing.Color.DarkGreen;
            this.label_AnalyzeB.Location = new System.Drawing.Point(322, 321);
            this.label_AnalyzeB.Name = "label_AnalyzeB";
            this.label_AnalyzeB.Size = new System.Drawing.Size(0, 17);
            this.label_AnalyzeB.TabIndex = 25;
            // 
            // label_AnalyzeC
            // 
            this.label_AnalyzeC.AutoSize = true;
            this.label_AnalyzeC.Font = new System.Drawing.Font("宋体", 10F);
            this.label_AnalyzeC.ForeColor = System.Drawing.Color.DarkGreen;
            this.label_AnalyzeC.Location = new System.Drawing.Point(322, 422);
            this.label_AnalyzeC.Name = "label_AnalyzeC";
            this.label_AnalyzeC.Size = new System.Drawing.Size(0, 17);
            this.label_AnalyzeC.TabIndex = 26;
            // 
            // linkLabel_Author
            // 
            this.linkLabel_Author.AutoSize = true;
            this.linkLabel_Author.Location = new System.Drawing.Point(386, 9);
            this.linkLabel_Author.Name = "linkLabel_Author";
            this.linkLabel_Author.Size = new System.Drawing.Size(87, 15);
            this.linkLabel_Author.TabIndex = 27;
            this.linkLabel_Author.TabStop = true;
            this.linkLabel_Author.Text = "By Azure99";
            this.linkLabel_Author.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_Author_LinkClicked);
            // 
            // linkLabel_SourceCode
            // 
            this.linkLabel_SourceCode.AutoSize = true;
            this.linkLabel_SourceCode.Location = new System.Drawing.Point(413, 36);
            this.linkLabel_SourceCode.Name = "linkLabel_SourceCode";
            this.linkLabel_SourceCode.Size = new System.Drawing.Size(87, 15);
            this.linkLabel_SourceCode.TabIndex = 28;
            this.linkLabel_SourceCode.TabStop = true;
            this.linkLabel_SourceCode.Text = "SourceCode";
            this.linkLabel_SourceCode.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_SourceCode_LinkClicked);
            // 
            // label_Version
            // 
            this.label_Version.AutoSize = true;
            this.label_Version.Location = new System.Drawing.Point(360, 36);
            this.label_Version.Name = "label_Version";
            this.label_Version.Size = new System.Drawing.Size(39, 15);
            this.label_Version.TabIndex = 29;
            this.label_Version.Text = "v1.2";
            // 
            // checkBox_InPutProblem
            // 
            this.checkBox_InPutProblem.AutoSize = true;
            this.checkBox_InPutProblem.Location = new System.Drawing.Point(34, 90);
            this.checkBox_InPutProblem.Name = "checkBox_InPutProblem";
            this.checkBox_InPutProblem.Size = new System.Drawing.Size(164, 19);
            this.checkBox_InPutProblem.TabIndex = 30;
            this.checkBox_InPutProblem.Text = "手动输入自定义题目";
            this.checkBox_InPutProblem.UseVisualStyleBackColor = true;
            this.checkBox_InPutProblem.CheckedChanged += new System.EventHandler(this.checkBox_InPutProblem_CheckedChanged);
            // 
            // linkLabel_DLNewVer
            // 
            this.linkLabel_DLNewVer.AutoSize = true;
            this.linkLabel_DLNewVer.Location = new System.Drawing.Point(386, 62);
            this.linkLabel_DLNewVer.Name = "linkLabel_DLNewVer";
            this.linkLabel_DLNewVer.Size = new System.Drawing.Size(97, 15);
            this.linkLabel_DLNewVer.TabIndex = 31;
            this.linkLabel_DLNewVer.TabStop = true;
            this.linkLabel_DLNewVer.Text = "下载最新版本";
            this.linkLabel_DLNewVer.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_DLNewVer_LinkClicked);
            // 
            // label_Info
            // 
            this.label_Info.AutoSize = true;
            this.label_Info.ForeColor = System.Drawing.Color.Red;
            this.label_Info.Location = new System.Drawing.Point(217, 91);
            this.label_Info.Name = "label_Info";
            this.label_Info.Size = new System.Drawing.Size(157, 15);
            this.label_Info.TabIndex = 32;
            this.label_Info.Text = "答案推荐算法仅供参考";
            // 
            // button_SearchA
            // 
            this.button_SearchA.Location = new System.Drawing.Point(100, 228);
            this.button_SearchA.Name = "button_SearchA";
            this.button_SearchA.Size = new System.Drawing.Size(75, 23);
            this.button_SearchA.TabIndex = 33;
            this.button_SearchA.Text = "搜索";
            this.button_SearchA.UseVisualStyleBackColor = true;
            this.button_SearchA.Click += new System.EventHandler(this.button_SearchA_Click);
            // 
            // button__SearchB
            // 
            this.button__SearchB.Location = new System.Drawing.Point(100, 319);
            this.button__SearchB.Name = "button__SearchB";
            this.button__SearchB.Size = new System.Drawing.Size(75, 23);
            this.button__SearchB.TabIndex = 34;
            this.button__SearchB.Text = "搜索";
            this.button__SearchB.UseVisualStyleBackColor = true;
            this.button__SearchB.Click += new System.EventHandler(this.button__SearchB_Click);
            // 
            // button__SearchC
            // 
            this.button__SearchC.Location = new System.Drawing.Point(100, 420);
            this.button__SearchC.Name = "button__SearchC";
            this.button__SearchC.Size = new System.Drawing.Size(75, 23);
            this.button__SearchC.TabIndex = 35;
            this.button__SearchC.Text = "搜索";
            this.button__SearchC.UseVisualStyleBackColor = true;
            this.button__SearchC.Click += new System.EventHandler(this.button__SearchC_Click);
            // 
            // button_Pay
            // 
            this.button_Pay.Location = new System.Drawing.Point(389, 83);
            this.button_Pay.Name = "button_Pay";
            this.button_Pay.Size = new System.Drawing.Size(105, 30);
            this.button_Pay.TabIndex = 36;
            this.button_Pay.Text = "打赏作者";
            this.button_Pay.UseVisualStyleBackColor = true;
            this.button_Pay.Click += new System.EventHandler(this.button_Pay_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 563);
            this.Controls.Add(this.button_Pay);
            this.Controls.Add(this.button__SearchC);
            this.Controls.Add(this.button__SearchB);
            this.Controls.Add(this.button_SearchA);
            this.Controls.Add(this.label_Info);
            this.Controls.Add(this.linkLabel_DLNewVer);
            this.Controls.Add(this.checkBox_InPutProblem);
            this.Controls.Add(this.label_Version);
            this.Controls.Add(this.linkLabel_SourceCode);
            this.Controls.Add(this.linkLabel_Author);
            this.Controls.Add(this.label_AnalyzeC);
            this.Controls.Add(this.label_AnalyzeB);
            this.Controls.Add(this.label_AnalyzeA);
            this.Controls.Add(this.label_Message);
            this.Controls.Add(this.textBox_AnswerC);
            this.Controls.Add(this.textBox_AnswerB);
            this.Controls.Add(this.textBox_AnswerA);
            this.Controls.Add(this.textBox_Problem);
            this.Controls.Add(this.label_AnswerC);
            this.Controls.Add(this.label_AnswerB);
            this.Controls.Add(this.label_AnswerA);
            this.Controls.Add(this.label_Problem);
            this.Controls.Add(this.button_Start);
            this.Controls.Add(this.button_Config);
            this.Controls.Add(this.label_RankC);
            this.Controls.Add(this.label_RankB);
            this.Controls.Add(this.label_RankA);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "超级答题助手";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Move += new System.EventHandler(this.MainForm_Move);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label_RankA;
        private System.Windows.Forms.Label label_RankB;
        private System.Windows.Forms.Label label_RankC;
        private System.Windows.Forms.Button button_Config;
        private System.Windows.Forms.Button button_Start;
        private System.Windows.Forms.Label label_Problem;
        private System.Windows.Forms.Label label_AnswerA;
        private System.Windows.Forms.Label label_AnswerB;
        private System.Windows.Forms.Label label_AnswerC;
        private System.Windows.Forms.TextBox textBox_Problem;
        private System.Windows.Forms.TextBox textBox_AnswerA;
        private System.Windows.Forms.TextBox textBox_AnswerB;
        private System.Windows.Forms.TextBox textBox_AnswerC;
        private System.Windows.Forms.Label label_Message;
        private System.Windows.Forms.Label label_AnalyzeA;
        private System.Windows.Forms.Label label_AnalyzeB;
        private System.Windows.Forms.Label label_AnalyzeC;
        private System.Windows.Forms.LinkLabel linkLabel_Author;
        private System.Windows.Forms.LinkLabel linkLabel_SourceCode;
        private System.Windows.Forms.Label label_Version;
        private System.Windows.Forms.CheckBox checkBox_InPutProblem;
        private System.Windows.Forms.LinkLabel linkLabel_DLNewVer;
        private System.Windows.Forms.Label label_Info;
        private System.Windows.Forms.Button button_SearchA;
        private System.Windows.Forms.Button button__SearchB;
        private System.Windows.Forms.Button button__SearchC;
        private System.Windows.Forms.Button button_Pay;
    }
}

