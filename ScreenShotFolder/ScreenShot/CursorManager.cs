using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UtilsHelper.WindowsApiHelper;

namespace ScreenShot
{
    //    一种： 把图像文件放到项目的文件夹中
    //1 如果图像文件是.cur格式：
    //Cursor cur = new Cursor(文件名);
    //this.cursor= cur;
    //两句话 就完事
    //2 如果图像文件是其他格式 就麻烦一点


    //首先引入命名空间
    //using System.Runtime.InteropServices;
    //导入API
    //[DllImport("user32.dll")]
    //ublic static extern IntPtr LoadCursorFromFile(string fileName);
    //    接下来使用自己的鼠标样式
    //    IntPtr colorCursorHandle = LoadCursorFromFile("my.bmp");//鼠标图标路径
    //    Cursor myCursor = new Cursor(colorCursorHandle);
    //this.Cursor = myCursor；
    //二种： 把图像文件放到项目资源中
    //  1 添加引用 using System.Runtime.InteropServices；
    //2.2 在程序中声明光标资源加载函数LoadCursorFromFile；
    //[DllImport("user32")]
    //    private static extern IntPtr LoadCursorFromFile(string fileName);
    //2.3 声明数组 byte[] cursorbuffer =namespace.Resource.CursorName；
    //Namespace为资源文件所在项目的命名空间名称，CursorName对应光标资源文件名。
    //2.4 创建一个临时光标文件tempTest.dat；将cursorbuffer中的数据写入数据文件中；
    //FileStream fileStream = new FileStream("tempTest.dat", FileMode.Create);
    //    fileStream.Write(cursorbuffer, 0, cursorbuffer.Length);
    //2.5 关闭文件，利用API 函数LoadCursorFromFile从光标临时文件中创建光标。
    //fileStream.Close();
    //Cursor.Current =new Cursor(LoadCursorFromFile("temp001.dat"));

    //》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》
    //2013/6/9
    //其实加载光标就两种方式，1、直接用.cur文件直接获得Cursor对象；2、获得文件的内存缓存指针，然后获得Cursor对象，获得指针有两种方法①已知文件，由API函数LoadCursorFromFile（）获得指针；②如果是资源文件，则可以直接用Properties.Resources.资源名.GetHicon() 来获得；
    //所以有了资源文件，我们不必把资源文件写入文件，再通过LoadCursorFromFile（）获得
    public class CursorManager
    {
        public static Cursor Arrow
        {
            get
            {
                //    var path = @"E:\UtilsHelper\ScreenShotFolder\ScreenShot\Cursors\Cross.cur";
                //    var aa =  new Cursor(path);
                //    var bb = new Cursor("Arrow.cur");//不知道为什么报错


                return WindowsApiHelper.CreateCursor(@"..\..\Cursors\Arrow.cur");
            }
        }


        //WindowsApiHelper.CreateCursor(@"..\..\Cursors\Arrow.cur");

        public static readonly Cursor Cross = WindowsApiHelper.CreateCursor(@"..\..\Cursors\Cross.cur");

        public static readonly Cursor ArrowNew = WindowsApiHelper.CreateCursor(@"..\..\Cursors\ArrowNew.cur");

        public static readonly Cursor CrossNew = WindowsApiHelper.CreateCursor(@"..\..\Cursors\CrossNew.cur");

        private CursorManager() { }
    }
}
