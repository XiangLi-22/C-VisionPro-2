using Cognex.VisionPro.ToolBlock;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _01_FrameGrabber链接相机拍照_加载tb
{
    public partial class Form2 : Form
    {
        CogToolBlock _toolBlock;
        public Form2(CogToolBlock Tb)
        {
            InitializeComponent();
            _toolBlock = Tb;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            cogToolBlockEditV21.Subject = _toolBlock;
        }
    }
}
