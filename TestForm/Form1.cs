using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UtilsHelper.WindowsApiHelper;

namespace TestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FormClosed += Form1_FormClosed;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            var curId = WindowsApi.GlobalFindAtom("jiaao_test");
            var rst = WindowsApi.GlobalDeleteAtom(curId);//删除原子"jiaao_test"
        }
    }
}
