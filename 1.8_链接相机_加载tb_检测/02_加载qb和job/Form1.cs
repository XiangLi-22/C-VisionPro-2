using Cognex.VisionPro;
using Cognex.VisionPro.DSCameraSetup.Implementation.Internal;
using Cognex.VisionPro.Implementation;
using Cognex.VisionPro.PMAlign;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//引入QuickBuild的命名空间
//注意如果没有添加CogJobManager这个控件,需要在"引用"上面添加using Cognex.VisionPro.QuickBuild引用
using Cognex.VisionPro.QuickBuild;
using Cognex.VisionPro.ToolGroup;

namespace _02_加载qb和job
{
    public partial class Form1 : Form
    {
        //创建一个工具类,可以写到点击事件中
        //public CogPMAlignTool cogPMAlignTool;
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            CogJobManager cogJobManager = CogSerializer.LoadObjectFromFile(Directory.GetCurrentDirectory() + @"/qb.vpp") as CogJobManager;
            cogJobManagerEdit1.Subject = cogJobManager;


            //比较麻烦的获取工具的方法

            //从QuickBuild中获取job
            //0 索引  QuickBuild 中的第一个job
            CogJob job = cogJobManager.Job(0);

            //从QuickBuild中获取job,从job中获取工具
            CogToolGroup group = job.VisionTool as CogToolGroup;//获取到工具组
            CogPMAlignTool pma = group.Tools[0] as CogPMAlignTool;//从工具组中获取到具体的工具


            //获取到工具的某一个属性
            double x = pma.Results[0].GetPose().TranslationX;

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //cogJob
            CogJob cogJob = CogSerializer.LoadObjectFromFile(Directory.GetCurrentDirectory()+ @"/gb.vpp") as CogJob;

            //cogJobEdit1要的是一个工具组
            cogJobEdit1.Subject = cogJob.VisionTool as CogToolGroup;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            //分开
            //CogPMAlignTool cogPMAlignTool = CogSerializer.LoadObjectFromFile(Directory.GetCurrentDirectory() + @"/pma.vpp") as CogPMAlignTool;
            //cogPMAlignEditV21.Subject = cogPMAlignTool;

            //合并
            cogPMAlignEditV21.Subject = CogSerializer.LoadObjectFromFile(Directory.GetCurrentDirectory() + @"/pma.vpp") as CogPMAlignTool;



            //获取到工具的某一个属性
            //double x = pma.Results[0].GetPose().TranslationX;
        }
    }
}
