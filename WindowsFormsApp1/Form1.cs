using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XBE.DDD.Util;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private HookDemo hook = new HookDemo();

        public Form1()
        {
            InitializeComponent();
            hook.Init(DeviceChangedCallBack);
        }


        private int DeviceChangedCallBack(string name, bool remove)
        {
            if (remove)
            {
                listBox1.Items.Add($"Usb设备：{name}被移除");
                return -1;
            }
            else
            {
                listBox1.Items.Add($"Usb设备：{name}已连接");
                return 1;
            }

        }
    }
}
