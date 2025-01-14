namespace _02_加载qb和job
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.cogPMAlignEditV21 = new Cognex.VisionPro.PMAlign.CogPMAlignEditV2();
            this.cogJobManagerEdit1 = new Cognex.VisionPro.QuickBuild.CogJobManagerEdit();
            this.cogJobEdit1 = new Cognex.VisionPro.QuickBuild.CogJobEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cogPMAlignEditV21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cogJobEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(666, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(154, 71);
            this.button1.TabIndex = 0;
            this.button1.Text = "加载qb";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 111);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(139, 71);
            this.button2.TabIndex = 1;
            this.button2.Text = "加载jb";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(139, 69);
            this.button3.TabIndex = 2;
            this.button3.Text = "加载工具";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // cogPMAlignEditV21
            // 
            this.cogPMAlignEditV21.Location = new System.Drawing.Point(171, 12);
            this.cogPMAlignEditV21.MinimumSize = new System.Drawing.Size(489, 0);
            this.cogPMAlignEditV21.Name = "cogPMAlignEditV21";
            this.cogPMAlignEditV21.Size = new System.Drawing.Size(489, 78);
            this.cogPMAlignEditV21.SuspendElectricRuns = false;
            this.cogPMAlignEditV21.TabIndex = 3;
            // 
            // cogJobManagerEdit1
            // 
            this.cogJobManagerEdit1.Location = new System.Drawing.Point(854, 21);
            this.cogJobManagerEdit1.Name = "cogJobManagerEdit1";
            this.cogJobManagerEdit1.ShowLocalizationTab = false;
            this.cogJobManagerEdit1.Size = new System.Drawing.Size(111, 60);
            this.cogJobManagerEdit1.Subject = null;
            this.cogJobManagerEdit1.TabIndex = 4;
            // 
            // cogJobEdit1
            // 
            this.cogJobEdit1.AllowDrop = true;
            this.cogJobEdit1.ContextMenuCustomizer = null;
            this.cogJobEdit1.Location = new System.Drawing.Point(157, 111);
            this.cogJobEdit1.MinimumSize = new System.Drawing.Size(489, 0);
            this.cogJobEdit1.Name = "cogJobEdit1";
            this.cogJobEdit1.ShowNodeToolTips = true;
            this.cogJobEdit1.Size = new System.Drawing.Size(846, 545);
            this.cogJobEdit1.SuspendElectricRuns = false;
            this.cogJobEdit1.TabIndex = 5;
            this.cogJobEdit1.ToolSyncObject = null;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 668);
            this.Controls.Add(this.cogJobEdit1);
            this.Controls.Add(this.cogJobManagerEdit1);
            this.Controls.Add(this.cogPMAlignEditV21);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.cogPMAlignEditV21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cogJobEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private Cognex.VisionPro.PMAlign.CogPMAlignEditV2 cogPMAlignEditV21;
        private Cognex.VisionPro.QuickBuild.CogJobManagerEdit cogJobManagerEdit1;
        private Cognex.VisionPro.QuickBuild.CogJobEdit cogJobEdit1;
    }
}

