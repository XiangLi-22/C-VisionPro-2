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
using System.Xml.Linq;
using Cognex.VisionPro;

namespace _01_vs添加vp控件
{




    //报错原因: 1. 相机线程被visionpro占用
    //         2. 相机未连接





    public partial class Form1 : Form
    {


        //1. 创建相机工具类对象,用于存储vp中反序列化的对象
        public CogAcqFifoTool cogAcqFifoTool { get; set; }


        //2. 创建连接路径,应该创建动态的连接路径,即获取当前工作目录的路径
        public string path = Directory.GetCurrentDirectory() + @"/acq.vpp";


        public Form1()
        {
            InitializeComponent();
        }

        //private void cogAcqFifoEditV21_Load(object sender, EventArgs e)
        //{

        //}


        //3. 创建一个函数,用于将vs中拖得控件和vp的相机属性相连(当然也可以不创建函数,目的是将程序看的更加美观)
        public void Loadvp()
        {
            //将保存的相机工具类文件赋值给相机工具类,即将acq.vpp中的内容传递给我们创建的相机工具类



            //CogSerializer 是 VisionPro 中用于序列化（保存和加载）图像处理工具和算法状态的类。它允许将图像处理工具（例如，CogAcqFifoTool、CogImageTool 等）及其设置序列化为一个字节流，这样你就可以将其保存到文件中或通过网络传输，并且在需要时再将其反序列化回原来的状态。
            //CogSerializer.LoadObjectFromFile() 是 VisionPro 中的一个静态方法，用于从文件中加载并反序列化对象。这个方法通常用于加载先前保存的图像处理工具或其他 VisionPro 对象的配置。


            //固定格式: CogSerializer.LoadObjectFromFile(地址)

            cogAcqFifoTool =  CogSerializer.LoadObjectFromFile(path) as CogAcqFifoTool;

            
        }



        //4. 创建一个窗口加载时的事件

        private void Form1_Load(object sender, EventArgs e)
        {
            //已经将vp中的字节流反序列化成了我们声明的相机工具类对象
            Loadvp();

            //将我们的相机工具类对象,赋值给windowfrom窗体中的控件

            //固定格式: 控件名.subject = 相机工具类对象


            //Subject 属性是 CogAcqFifoEditV21 控件的一个重要属性。它的作用是绑定一个 VisionPro 工具（在你的示例中是 CogAcqFifoTool），使得控件能够显示该工具的设置，并允许用户通过控件编辑工具的属性。



            cogAcqFifoEditV21.Subject = cogAcqFifoTool;
        }
    }
}
