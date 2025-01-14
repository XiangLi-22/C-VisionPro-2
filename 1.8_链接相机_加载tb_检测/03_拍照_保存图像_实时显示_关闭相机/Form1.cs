using Cognex.VisionPro;
using Cognex.VisionPro.Display;
using Cognex.VisionPro.ImageFile;
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

namespace _03_拍照_保存图像_实时显示_关闭相机
{
    public partial class Form1 : Form
    {

        //声明相机工具类对象
        CogAcqFifoTool CogAcqFifoTool;

        //创建地址
        string path = Directory.GetCurrentDirectory() + @"\acq.vpp";

        //声明相机工具拍摄的图片
        ICogImage img;


        public Form1()
        {
            InitializeComponent();
        }

        //连接相机
        private void Form1_Load(object sender, EventArgs e)
        {
            CogAcqFifoTool = CogSerializer.LoadObjectFromFile(path) as CogAcqFifoTool;
            cogAcqFifoEditV21.Subject = CogAcqFifoTool;
        }




        private void button3_Click(object sender, EventArgs e)
        {
            //保存图片

            //第一种方法(最常用): 通过CogImageFileTool 工具保存图像

            //创建文件地址
            
            string path = Directory.GetCurrentDirectory()+@"\image";

            //查看文件夹是否存在,如果不存在就创建
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            else
            {
                //保存图片

                //CogImageFileTool 主要用于处理与图像文件相关的操作，如加载、保存和显示图像。
                CogImageFileTool cogImageFileTool = new CogImageFileTool();

                //cogImageFileTool.InputImage  输入图像。此属性可设置为启用录制。或返回或设置用于启用图像录制的输入图像。在记录模式下，输入图像被附加到当前打开的图像文件
                cogImageFileTool.InputImage = img;

                //创建一个动态的图片名字,以当前的时间戳为名字
                DateTime dateTime = DateTime.Now;
                string time = dateTime.ToString("yyyyMMddHHmmss");

                string imagepath = $"{path}\\{time}.bmp";//拼接一个图片地址

                //尝试打开一个文件，并以写入模式 (Write) 进行操作。CogImageFileModeConstants.Write 指示该文件打开模式是用于写入图像数据。

                //cogImageFileTool.Operator.Open() 作用是打开或创建一个图像文件，准备进行读写操作。具体来说，Open 方法根据指定的路径和文件名，使用指定的模式打开图像文件，允许后续对文件进行图像数据的读取或写入。

                //用于打开并加载一个图像文件，可以指定图像文件的路径。加载后，你可以对该图像进行读,写等操作。
                cogImageFileTool.Operator.Open(imagepath, CogImageFileModeConstants.Write);


                //启动cogImageFileTool工具
                cogImageFileTool.Run();




                //第二种方法: 通过Bitmap保存图像




            }
            void save(CogDisplay display)//将图像显示的工具传进来
            {
                // 写到本地磁盘

                //设置保存路径
                string path1 = Directory.GetCurrentDirectory() + @"\Image2";

                //判断 是否有当前文件夹
                if (!Directory.Exists(path1))
                {
                    //说明没有当前文件夹
                    //创建文件夹
                    Directory.CreateDirectory(path1);
                }
                //定义图像名称
                string imageName = $"{DateTime.Now.ToString("yyyyMMddHHmmsss")}.jpeg";

                //通过Bitmap保存图像


                //display.CreateContentBitmap() CogDisplay 类的一个方法，用于从显示控件中创建一个图像内容的位图（Bitmap）。这个方法可以将显示控件（例如显示图像、结果等）中的内容转换成一个可以进一步处理或保存的位图格式。

                //CogDisplayContentBitmapConstants，用于指定你要从显示控件中提取的内容。常见的值包括：
                //CogDisplayContentBitmapConstants.Image：提取显示的图像。
                //CogDisplayContentBitmapConstants.Results：提取显示的检测结果（如标记、测量结果等）。
                //CogDisplayContentBitmapConstants.All：提取显示控件中的所有内容，包括图像和结果。


                Bitmap bmp = (Bitmap)display.CreateContentBitmap(CogDisplayContentBitmapConstants.Image);

                //参数1: 路径 名称
                //参数2:保存的图像的格式
                bmp.Save($"{path1}\\{imageName}", System.Drawing.Imaging.ImageFormat.Jpeg);

            }






            //通过CogImageFileTool 工具保存图像



        }

        private void button1_Click(object sender, EventArgs e)
        {
            //拍照



            //判断相机是否连接
            if (CogAcqFifoTool.Operator == null)
            {
                MessageBox.Show("相机未连接");
            }
            else
            {
                //运行相机工具
                CogAcqFifoTool.Run();

                //将相机工具的图片,传递给图像显示工具的图片
                img = CogAcqFifoTool.OutputImage;
                cogRecordDisplay1.Image = img;

                //设置拍摄的图片在图片显示工具中的自适应显示比例
                cogRecordDisplay1.Fit();
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            //实时显示

            //当我们进行点击实时显示时,文案变成关闭实时
            if (button2.Text == "实时显示")
            {
                button2.Text = "关闭实时";
                //图像显示工具中有一个方法可以将图像进行实时显示
                cogRecordDisplay1.StartLiveDisplay(CogAcqFifoTool,false);
            }
            else
            {
                button2.Text = "实时显示";
                //关闭实时显示
                cogRecordDisplay1.StopLiveDisplay();

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //关闭相机 , 可以按钮关闭相机,也可以在窗体关闭时关闭相机

            // 判断相机是否连接

            //CogAcqFifoTool.Operator属性 : 工具当前的操作状态
            if (CogAcqFifoTool.Operator != null)
            {
                //释放相机资源
                CogAcqFifoTool.Dispose();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //窗体关闭之前的事件,进行关闭相机

            // 判断相机是否连接

            //CogAcqFifoTool.Operator属性 : 工具当前的操作状态
            if (CogAcqFifoTool.Operator != null)
            {
                //释放相机资源
                CogAcqFifoTool.Dispose();
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //保存相机工具


            if (SaveVpp(CogAcqFifoTool))
            {
                MessageBox.Show(" 保存成功");
            }

        }

        public bool SaveVpp(CogAcqFifoTool cogAcqFifoTool)
        {
            try
            {
                CogSerializer.SaveObjectToFile(cogAcqFifoTool, path);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
