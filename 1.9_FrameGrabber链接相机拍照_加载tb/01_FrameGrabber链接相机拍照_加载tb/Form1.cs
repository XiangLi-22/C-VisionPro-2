using Cognex.VisionPro;
using Cognex.VisionPro.ImageFile;
using Cognex.VisionPro.ToolBlock;
using Cognex.VisionPro3D;
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

namespace _01_FrameGrabber链接相机拍照_加载tb
{
    public partial class Form1 : Form
    {

        //CogFrameGrabbers 是系统中的帧抓取器管理器(多个相机连接管理)，用于获取 ICogFrameGrabber 对象(相机对象)。
        //ICogFrameGrabber 是单个帧抓取器设备的接口，它可以创建一个或多个 ICogAcqFifo 对象。
        //ICogAcqFifo 是最终用于图像采集的接口。
        //CogFrameGrabbers → ICogFrameGrabber → ICogAcqFifo → 图像采集。



        //获取并采集图像的完整流程
        //发现设备：
        //使用 CogFrameGrabbers 列举所有可用的帧抓取器设备。
        //选择设备：
        //使用 ICogFrameGrabber 表示具体的帧抓取器设备。
        //配置采集：
        //通过 ICogFrameGrabber.CreateAcqFifo 创建采集队列。
        //采集图像：
        //调用 ICogAcqFifo.Acquire 采集图像。


        //ICogAcqFifo.Complete 是一个事件（event），它会在帧采集完成后触发，用于通知程序采集操作已经完成。这通常被用在异步图像采集的场景中。
        //当相机采集到一帧图像后，Complete 事件会被触发，你可以在这个事件中处理采集完成后的逻辑，例如获取采集到的图像或触发下一步处理。

        //ICogAcqFifo.Complete 的常见用途
        //异步采集：通过注册 Complete 事件，可以在图像采集完成时自动执行回调处理，而无需程序主动轮询采集状态。
        //实时处理：在采集完成后立即执行后续的处理逻辑，如图像分析或保存图像。

        //Complete 事件的触发流程：
        //启动采集：
        //通过 /*ICogAcqFifo.StartAcquire()*/ 启动异步图像采集。
        //采集图像：
        //相机硬件完成图像采集。
        //事件触发：
        //采集完成后，Complete 事件被触发，通知程序采集完成。
        //回调执行：
        //自动调用之前注册的事件处理方法，处理采集完成后的逻辑。
        //Complete 的自动触发特点
        //无需手动调用：只要图像采集完成并注册了 Complete 事件，事件会自动触发。
        //异步机制：它是在异步采集模式下使用的，通过事件的自动触发实现采集完成的通知。


        public Form1()
        {
            InitializeComponent();
            MyLoad();
        }


        //多相机连接的各个相机的类型
        ICogFrameGrabber myFrame;
        //声明每个相机的图像采集接口
        ICogAcqFifo myFifo;
        //声明一个图像采集到的图片
        ICogImage image;


        private void Form1_Load(object sender, EventArgs e)
        {
            //在窗体加载时连接相机

            // 获取已经连接的硬件
            //它是连接图像采集硬件(如相机)与visionPro软件的关键桥梁,使得软件能够获取并处理来自相机的图像数据

            //多相机连接可以使用CogFrameGrabbers类 , 可以获取所有已经连接的相机(集合)
            CogFrameGrabbers cogFrame = new CogFrameGrabbers();
            if (cogFrame.Count < 1)
            {
                MessageBox.Show("相机链接失败");
            }

            //循环遍历相机集合中的相机,并创建图像采集接口,进行图像采集
            foreach (ICogFrameGrabber item in cogFrame)
            {
                //将我们循环遍历的相机赋值给在外面创建的全局变量
                myFrame = item;



                //创建图像采集接口,即创建myFifo(FIFO)
                //参数1: 图像类型,在我们连接相机时手动设置的图像类型
                //参数2: 像素类型,以我们设置的图像类型来决定 , Generic GigEVision (Mono)就是8位的灰度图,对应的就是Format8Grey
                //参数3和参数4: 使用默认值
                //返回值: ICogAcqFifo ,可以赋值给我们全局变量的myFifo
                myFifo = item.CreateAcqFifo("Generic GigEVision (Mono)", CogAcqFifoPixelFormatConstants.Format8Grey, 0, true);

            }
            //当采集图像完成后会自动触发一个事件处理函数,用于将我们采集到的图像在cogRecordDisplay1工具上显示出来
            myFifo.Complete += my_fifo;


         }
        private void my_fifo(object sender, CogCompleteEventArgs e)
        {
            //显示采集到的图像
            //可以从ICogAcqFifo.GetFifoState方法中获取到采集到的图像

            //mFifo.GetFifoState()方法用于获取FIFO的状态信息‌。具体来说，这个方法可以返回以下三个状态信息：
            //‌numPendingVal‌：表示等待中的数据包数量。
            //‌numReadyVal‌：表示已准备好的数据包数量。
            //‌busyVal‌：表示FIFO当前是否忙碌‌
            //这些信息可以帮助开发者了解FIFO的当前状态，从而进行相应的处理。例如，如果numReadyVal大于0，表示有数据包可供处理；如果numPendingVal大于0，表示有数据包正在等待处理；如果busyVal为true，表示FIFO当前正在忙碌中‌

            int numPendingVal, numReadyVal;
            bool busyVal;
            try
            {
                myFifo.GetFifoState(out numPendingVal, out numReadyVal, out busyVal);

                //CompleteAcquireEx 方法的参数： 如果 CompleteAcquireEx 的参数类型是接口（比如 ICogAcqFifo），那么这个接口可能允许接受实现了该接口的类作为参数。也就是说，CogAcqInfo 类可能实现了这个接口。
                CogAcqInfo info = new CogAcqInfo();


                //当已经准备好的数据包数量大于0时说明已经成功采集到了图像
                if (numReadyVal > 0)
                {
                    //就可以使用ICogAcqFifo.CompleteAcquireEx 用于完成图像采集任务。该方法的具体作用是结束图像采集过程，并释放相关资源。
                    image = myFifo.CompleteAcquireEx(info);//返回值是我们图像采集到的图片
                    cogRecordDisplay1.Image = image;//将图像采集到的图片展现到cogRecordDisplay1工具上
                    cogRecordDisplay1.Fit();//图像自适应cogRecordDisplay1工具的边框
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("图像采集失败",ex.Message);
            }


        }


        private void button1_Click(object sender, EventArgs e)
        {
            //拍照功能
            myFifo.StartAcquire();//启动异步图像采集。
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //在窗体关闭的时候,释放相机,相机在CogFrameGrabbers相机集合中,相机集合中有几个相机就释放几个相机
            CogFrameGrabbers cogFrameGrabbers = new CogFrameGrabbers();
            foreach (ICogFrameGrabber item in cogFrameGrabbers)
            {


                //item.Disconnect(bool releaseResources)
                //releaseResources 参数决定是否释放与相机相关的资源。具体的作用是：
                //false：仅断开连接，但 不释放资源。这意味着硬件仍然可以保持初始化状态，并且以后可以重新连接，复用相机的设置和资源。
                //true：断开连接并 释放资源，包括与硬件相关的资源。如果在断开后再尝试使用相机，可能需要重新初始化资源。


                item.Disconnect(false);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //设置曝光
            //OwnedExposureParams 是一个属性，表示与相机相关的曝光参数。通过 OwnedExposureParams，可以访问和设置相机的曝光时间（Exposure）以及其他曝光相关的设置。
            //Exposure 控制相机的曝光时间（单位通常为秒）。曝光时间直接影响图像的亮度，曝光时间越长，图像越亮；曝光时间越短，图像越暗。

            myFifo.OwnedExposureParams.Exposure = double.Parse(textBox1.Text);//将用户手动输入的textBox1.Text赋值给相机的曝光时间
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //保存图像
            //通过CogImageFileTool
            CogImageFileTool cogImageFileTool = new CogImageFileTool();
            //cogImageFileTool.InputImage  输入图像。此属性可设置为启用录制。或返回或设置用于启用图像录制的输入图像。在记录模式下，输入图像被附加到当前打开的图像文件
            cogImageFileTool.InputImage = image;

            //声明并初始化图片的文件路径
            string imagepath = Directory.GetCurrentDirectory()+@"\image";
            if (!Directory.Exists(imagepath))//判断当前图片路径文件是否存在,不存在就创建,存在就在对图片地址进行创建
            {
                Directory.CreateDirectory(imagepath);
            }
            else
            {
                string dateTime = (DateTime.Now).ToString("yyyyMMMddHHmmssfff");
                imagepath = imagepath + $"\\{dateTime}.bmp";
            }


            //尝试打开一个文件，并以写入模式 (Write) 进行操作。CogImageFileModeConstants.Write 指示该文件打开模式是用于写入图像数据。
            //cogImageFileTool.Operator.Open() 作用是打开或创建一个图像文件，准备进行读写操作。具体来说，Open 方法根据指定的路径和文件名，使用指定的模式打开图像文件，允许后续对文件进行图像数据的读取或写入。
            //用于打开并加载一个图像文件，可以指定图像文件的路径。加载后，你可以对该图像进行读,写等操作。
            cogImageFileTool.Operator.Open(imagepath, CogImageFileModeConstants.Write);

            //运行CogImageFileTool工具
            cogImageFileTool.Run();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //打开图片

            //设置当前图片的指定目录
            string imgpath = @"C:\Program Files\Cognex\VisionPro\Images\blobs.bmp";

            //通过CogImageFileTool的读取属性将图片读进来
            CogImageFileTool cogImageFileTool = new CogImageFileTool();
            cogImageFileTool.Operator.Open(imgpath, CogImageFileModeConstants.Read);
            cogImageFileTool.Run();

            //将我们读进来的图片用cogImageFileTool.OutputImage 输出出去,并赋值给cogRecordDisplay1工具,在页面上显示出来
            cogRecordDisplay1.Image = cogImageFileTool.OutputImage;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //手动选择

            //打开一个文件选择对话框
            OpenFileDialog ofd = new OpenFileDialog();
            //ofd.InitialDirectory = @"C:\Program Files\Cognex\VisionPro\Images";//设置文件对话框的初始化路径,可以省略
            //当点击确定时就可以获取到图片的路径
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //使用bitmap类进行读取操作
                Bitmap bitmap = new Bitmap(ofd.FileName);
                ICogImage cogImage = new CogImage8Grey(bitmap);//将bitmap类型的图片,转化成ICogImage类型的图片,CogImage8Grey实现了ICogImage接口,可以用CogImage8Grey类型的对象进行实例化
                cogRecordDisplay1.Image = cogImage;//将图片在cogRecordDisplay1工具上显示出来
                cogRecordDisplay1.Fit();
            }



        }

        CogToolBlock Tb;
        //将vp和vs进行连接的将函数,在from1的构造函数中调用,以便我们跳转到form2中不用等待
        private void MyLoad()
        {
            string path = Directory.GetCurrentDirectory()+ @"\测量1.vpp";
            Tb = CogSerializer.LoadObjectFromFile(path) as CogToolBlock;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //加载tb
            
            //点击时会跳转到from2中, 并将tb传给form2中,在form2页面上显示出来
            Form2 form2 = new Form2(Tb);
            form2.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //测量

            //当点击测量的时候会将新拍摄好的图片给Tb,进行测量,并将测量好的图片显示到cogRecordDisplay1工具上,并将测量结果显示到label2上

            Tb.Inputs["OutputImage"].Value = image;
            Tb.Run();

            //CreateLastRunRecord()：这是 CogToolBlock 类中的方法，用于创建并返回当前工具块的最后一次执行记录 (CogToolBlockRunRecord)，它包含了上一次工具块执行的详细信息。
            //SubRecords：这是一个属性，返回 执行记录的子记录。SubRecords 是一个集合（通常是 CogToolBlockRunSubRecord 类型的数组或列表），每个子记录表示工具块执行的某个具体步骤或子任务。
            cogRecordDisplay1.Record = Tb.CreateLastRunRecord().SubRecords[0];


            label2.Text = Tb.Outputs["Distance"].Value.ToString();
        }
    }
}
