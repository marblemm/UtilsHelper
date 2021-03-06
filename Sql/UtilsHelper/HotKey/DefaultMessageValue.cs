namespace UtilsHelper.HotKey
{
    /// <summary>
    /// Windows 常用消息大全
    /// </summary>
    public class DefaultMessageValue
    {
        //常用消息分布
        //        消息范围    说 明
        //      0 ～ public const int WM_USER – 1         //系统消息
        //      public const int WM_USER ～ = 0x7FFF;	    //自定义窗口类整数消息
        //      public const int WM_APP ～ = 0xBFFF;				 //应用程序自定义消息
        //      = 0xC000;				 // ～ = 0xFFFF;				 //应用程序字符串消息
        //      >= 0xFFFF;				 //为以后系统应用保留

        //常用Windows消息

        public const int WmHotKey = 0x312;						 //  //窗口消息：热键
        public const int WmCreate = 0x0001;				 //窗口消息：创建
        public const int WmDestory = 0x0002;				 //窗口消息：销毁
        public const int WmMove = 0x0003;				 //移动一个窗口
        public const int WmSize = 0x0005;				 //改变一个窗口的大小
        public const int WmActivate = 0x0006;				 //一个窗口被激活或失去激活状态



        public const int WmNull = 0x0000;				 // 空消息，此消息将被接收窗口忽略
        public const int WmSetfocus = 0x0007;				 //  获得焦点后
        public const int WmKillfocus = 0x0008;				 //  失去焦点
        public const int WmEnable = 0x000A;				 //  应用程序Enable状态改变时产生
        public const int WmSetredraw = 0x000B;				 //  设置窗口是否能重画
        public const int WmSettext = 0x000C;				 //  应用程序发送此消息来设置一个窗口的文本
        public const int WmGettext = 0x000D;				 //  应用程序发送此消息来复制对应窗口的文本到缓冲区
        public const int WmGettextlength = 0x000E;				 //  得到与一个窗口有关的文本的长度（不包含空字符）  
        public const int WmPaint = 0x000F;				 //  要求一个窗口重绘自己
        public const int WmClose = 0x0010;				 //  当一个窗口或应用程序要关闭时发送一个信号
        public const int WmQueryendsession = 0x0011;				 //  用户选择结束对话框或应用程序自己调用ExitWindows()函数
        public const int WmQuit = 0x0012;				 //   用来结束程序运行或应用程序调用Postquitmessage()函数来产生此消息
        public const int WmQueryopen = 0x0013;				 //  当用户窗口恢复以前的大小位置时，把此消息发送给某个图标
        public const int WmErasebkgnd = 0x0014;				 //  当窗口背景必须被擦除时（例如在窗口改变大小时）
        public const int WmSyscolorchange = 0x0015;				 //  当系统颜色改变时，发送此消息给所有顶级窗口
        public const int WmEndsession = 0x0016;				 //  当系统进程发出public const int WM_QUERYENDSESSION消息后，此消息发送给应用程序
        public const int WmShowwindow = 0x0018;				 //  当隐藏或显示窗口是发送此消息给这个窗口
        public const int WmActivateapp = 0x001C;				 //  当某个窗口将被激活时，将被激活窗口和当前活动（即将失去激活）窗口会收到此消息，发此消息给应用程序哪个窗口是激活的，哪个是非激活的
        public const int WmFontchange = 0x001D;				 //  当系统的字体资源库变化时发送此消息给所有顶级窗口
        public const int WmTimechange = 0x001E;				 //当系统的时间变化时发送此消息给所有顶级窗口
        public const int WmCancelmode = 0x001F;				 //发送此消息来取消某种正在进行的操作
        public const int WmSetcursor = 0x0020;				 //如果鼠标引起光标在某个窗口中移动且鼠标输入没有被捕获时，发消息给该窗口
        public const int WmMouseactivate = 0x0021;				 //当光标在某个非激活的窗口中而用户正按着鼠标的某个键发送此消息给当前窗口
        public const int WmChildactivate = 0x0022;				 //发送此消息给MDI子窗口当用户点击此窗口的标题栏，或当窗口被激活、移动、改变大小
        public const int WmQueuesync = 0x0023;				 //此消息由基于计算机的训练程序发送，通过WH_JOURNALPALYBACK的Hook程序分离出用户输入消息
        public const int WmGetminmaxinfo = 0x0024;				 //当窗口要将要改变大小或位置时，发送此消息给该窗口
        public const int WmPainticon = 0x0026;				 //当窗口图标将要被重绘时，发送此消息给该窗口
        public const int WmIconerasebkgnd = 0x0027;				 //在一个最小化窗口的图标在重绘前，当图标背景必须被重绘时，发送此消息给该窗口
        public const int WmNextdlgctl = 0x0028;				 //发送此消息给一个对话框程序以更改焦点位置
        public const int WmSpoolerstatus = 0x002A;				 //当打印管理列队增加或减少一条作业时发出此消息
        public const int WmDrawitem = 0x002B;				 //当Button，ComboBox，Listbox，Menu控件的外观改变时，发送此消息给这些控件的所有者
        public const int WmMeasureitem = 0x002C;				 //当Button，ComboBox，list box，ListView，Menu 项被创建时，发送此消息给控件的所有者
        public const int WmDeleteitem = 0x002D;				 //当ListBox 或 ComboBox 被销毁或当某些项通过发送LB_DELETESTRING、LB_RESETCONTENT、 CB_DELETESTRING、CB_RESETCONTENT 消息被删除时，发送此消息给控件的所有者
        public const int WmVkeytoitem = 0x002E;				 //一个具有LBS_WANTKEYBOARDINPUT风格的ListBox控件发送此消息给它的所有者，以此来响应public const int WM_KEYDOWN消息
        public const int WmChartoitem = 0x002F;				 //一个具有LBS_WANTKEYBOARDINPUT风格的ListBox控件发送此消息给它的所有者，以此来响应public const int WM_CHAR消息
        public const int WmSetfont = 0x0030;				 //应用程序绘制控件时，发送此消息得到以何种字体绘制控件中的文本
        public const int WmGetfont = 0x0031;				 //应用程序发送此消息得到当前控件绘制文本的字体
        public const int WmSethotkey = 0x0032;				 //应用程序发送此消息让一个窗口与一个热键相关联
        public const int WmGethotkey = 0x0033;				 //应用程序发送此消息来判断热键与某个窗口是否有关联
        public const int WmQuerydragicon = 0x0037;				 //此消息发送给最小化窗口，当此窗口将要被拖放而它的类中没有定义图标，应用程序就返回一个图标或光标的句柄，当用户拖放图标时系统显示这个图标或光标
        public const int WmCompareitem = 0x0039;				 //发送此消息来判定ComboBox或ListBox新增加的项的相对位置
        public const int WmCompacting = 0x0041;				 //显示内存已经很少了
        public const int WmWindowposchanging = 0x0046;				 //当调用SetWindowPos()函数改变窗口的大小和位置后，发送此消息给该窗口
        public const int WmPower = 0x0048;				 //当系统将进入挂起状态时发送此消息给所有进程
        public const int WmCopydata = 0x004A;				 //当一个应用程序传递数据给另一个应用程序时发送此消息
        public const int WmCanceljournal = 0x004B;				 //当某个用户取消程序日志激活状态，发送此消息给应用程序
        public const int WmNotify = 0x004E;				 //当某个控件的某个事件已经发生或这个控件需要得到一些信息时，发送此消息给它的父窗口
        public const int WmInputlangchangerequest = 0x0050;				 //当用户选择某种输入语言，或输入语言的热键改变
        public const int WmInputlangchange = 0x0051;				 //当应用程序输入语言改变后发送此消息给受影响的最顶级窗口
        public const int WmTcard = 0x0052;				 //当应用程序已经初始化Windows帮助例程时发送此消息给应用程序
        public const int WmHelp = 0x0053;				 //当用户按下了F1，如果某个菜单是激活的，就发送此消息给此窗口关联的菜单，否则就发送给有焦点的窗口，如果当前都没有焦点，就把此消息发送给当前激活的窗口
        public const int WmUserchanged = 0x0054;				 //当用户已经登录或退出后发送此消息给所有的窗口，当用户登录或退出时系统更新用户的具体设置信息，在用户更新设置时系统马上发送此消息
        public const int WmNotifyformat = 0x0055;				 //公用控件和它们的父窗口通过此消息来判断在public const int WM_NOTIFY消息中是使用ANSI还是UNICODE形式的结构，使用此控件能使某个控件与它的父控件进行相互通信
        public const int WmContextmenu = 0x007B;				 //        当用户在某个窗口中点击右键，则发送此消息给该窗口
        public const int WmStylechanging = 0x007C;				 //当将要调用SetWindowLong()函数窗口的一个或多个风格时，发送此消息给该窗口
        public const int WmStylechanged = 0x007D;				 //当调用SetWindowLong()函数改变了窗口的一个或多个风格后，发送此消息给该窗口
        public const int WmDisplaychange = 0x007E;				 //        当显示器的分辨率改变后发送此消息给所有的窗口
        public const int WmGeticon = 0x007F;				 //        发送此消息给某个窗口，返回与某个窗口有关联的大图标或小图标的句柄
        public const int WmSeticon = 0x0080;				 //        应用程序发送此消息让一个新的大图标或小图标与某个窗口关联
        public const int WmNccreate = 0x0081;				 //        当某个窗口第一次被创建时，此消息在public const int WM_CREATE消息被发送前发送
        public const int WmNcdestroy = 0x0082;				 //        此消息通知某个窗口，正在销毁非客户区
        public const int WmNccalcsize = 0x0083;				 //        当计算某个窗口的客户区大小和位置时发送此消息
        public const int WmNchittest = 0x0084;				 //        移动鼠标，按住或释放鼠标时产生此消息
        public const int WmNcpaint = 0x0085;				 //        当某个窗口的框架必须被绘制时，应用程序发送此消息给该窗口
        public const int WmNcactivate = 0x0086;				 //        通过改变某个窗口的非客户区来表示窗口是处于激活还是非激活状态时，此消息被发送给该窗口
        public const int WmNcmousemove = 0x00A0;				 //        当光标在窗口的非客户区（窗口标题栏及边框）内移动时发送此消息给该窗口
        public const int WmNclbuttondown = 0x00A1;				 //        当光标在窗口的非客户区并按下鼠标左键时发送此消息
        public const int WmNclbuttonup = 0x00A2;				 //        当光标在窗口的非客户区并释放鼠标左键时发送此消息
        public const int WmNclbuttondblclk = 0x00A3;				 //        当光标在窗口的非客户区并双击鼠标左键时发送此消息
        public const int WmNcrbuttondown = 0x00A4;				 //        当光标在窗口的非客户区并按下鼠标右键时发送此消息
        public const int WmNcrbuttonup = 0x00A5;				 //        当光标在窗口的非客户区并释放鼠标右键时发送此消息
        public const int WmNcrbuttondblclk = 0x00A6;				 //        当光标在窗口的非客户区并双击鼠标右键时发送此消息
        public const int WmNcmbuttondown = 0x00A7;				 //        当光标在窗口的非客户区并按下鼠标中键时发送此消息
        public const int WmNcmbuttonup = 0x00A8;				 //        当光标在窗口的非客户区并释放鼠标中键时发送此消息
        public const int WmNcmbuttondblcl = 0x00A9;				 //        当光标在窗口的非客户区并双击鼠标中键时发送此消息
        public const int WmKeydown = 0x0100;				 //        按下一个非系统键（按下键时未按下“ALT”键）
        public const int WmKeyup = 0x0101;				 //        释放一个非系统键
        public const int WmChar = 0x0102;				 //        按下某键，当TranslateMessage()转发public const int WM_KEYDOWN后发送本消息
        public const int WmDeadchar = 0x0103;				 //        释放某键，当TranslateMessage()转发public const int WM_KEYUP后发送本消息
        public const int WmSyskeydown = 0x0104;				 //        当按住ALT键同时按下其他键时发送此消息给拥有键盘焦点的窗口
        public const int WmSyskeyup = 0x0105;				 //        当释放一个键同时按住ALT键时发送此消息给拥有键盘焦点的窗口
        public const int WmSyschar = 0x0106;				 //当TranslateMessage()转发public const int WM_SYSKEYDOWN后发送此消息给拥有键盘焦点的窗口
        public const int WmSysdeadchar = 0x0107;				 //当TranslateMessage()转发public const int WM_SYSKEYUP后发送此消息给拥有键盘焦点的窗口
        public const int WmInitdialog = 0x0110;				 //        在被显示前发送此消息对话框，通常用此消息初始化控件和执行其他任务
        public const int WmCommand = 0x0111;				 //        选择窗口菜单项或某个控件发送一条消息给它的父窗口或按下一个快捷键时产生此消息
        public const int WmSyscommand = 0x0112;				 //        选择窗口菜单项或选择最大化或最小化时，发送此消息给该窗口
        public const int WmTimer = 0x0113;				 //        发生了定时器事件
        public const int WmHscroll = 0x0114;				 //        当窗口水平滚动条产生一个滚动事件时发送此消息给该窗口和滚动条的所有者
        public const int WmVscroll = 0x0115;				 //          当窗口垂直滚动条产生一个滚动事件时发送此消息给该窗口和滚动条的所有者
        public const int WmInitmenu = 0x0116;				 //          当一个菜单将要被激活时发送此消息，它发生在按下菜单项或按下菜单快捷键时，它允许程序在显示前更改菜单
        public const int WmInitmenupopup = 0x0117;				 //          当一个下拉菜单或子菜单将要被激活时发送此消息，它允许显示前在修改菜单而不必更改整个菜单
        public const int WmMenuselect = 0x011F;				 //          选择一条菜单项时发送此消息给菜单的所有者（一般是窗口）
        public const int WmMenuchar = 0x0120;				 //          当菜单已被激活且用户按下了某个键（非快捷键），发送此消息给菜单的所有者
        public const int WmEnteridle = 0x0121;				 //          当一个有模式对话框或菜单进入空闲状态时发送此消息给它的所有者，空闲状态指在处理完一条或几条先前的消息后，消息列队为空
        public const int WmMenurbuttonup = 0x0122;				 //          当光标位于菜单项上时，释放鼠标右键产生此消息
        public const int WmMenudrag = 0x0123;				 //          当拖动菜单项时，发送此消息给拖放菜单的所有者
        public const int WmMenugetobject = 0x0124;				 //          当光标移入菜单项或者从菜单项中心移到菜单项顶部或底部时，发送此消息给拖放菜单的所有者
        public const int WmUninitmenupopup = 0x0125;				 //          当下拉菜单或者子菜单被销毁时产生此消息
        public const int WmMenucommand = 0x0126;				 //          当用户选择菜单项时产生此消息
        public const int WmChangeuistate = 0x0127;				 //          应用程序发送此消息表明用户界面（UI）状态应当被改变
        public const int WmUpdateuistate = 0x0128;				 //          应用程序发送此消息改变指定窗口及其子窗口的用户界面（UI）状态
        public const int WmQueryuistate = 0x0129;				 //          应用程序发送此消息得到某个窗口的用户界面（UI）状态
        public const int WmCtlcolormsgbox = 0x0132;				 //          绘制消息框前发送此消息给它的父窗口，通过响应这条消息，父窗口可以通过使用给定的相关显示设备的句柄来设置消息框的文本和背景颜色
        public const int WmCtlcoloredit = 0x0133;				 //          绘制编辑型控件前发送此消息给它的父窗口，可用来设置编辑框的文本和背景颜色
        public const int WmCtlcolorlistbox = 0x0134;				 //           绘制列表框控件前发送此消息给它的父窗口，可用来设置编辑框的文本和背景颜色
        public const int WmCtlcolorbtn = 0x0135;				 //           绘制按钮控件前发送此消息给它的父窗口，可用来设置编辑框的文本和背景颜色
        public const int WmCtlcolordlg = 0x0136;				 //  绘制对话框前发送此消息给它的父窗口，可用来设置编辑框的文本和背景颜色
        public const int WmCtlcolorscrollbar = 0x0137;				 //  绘制滚动条控件前发送此消息给它的父窗口，可用来设置滚动条控件的文本和背景颜色
        public const int WmCtlcolorstatic = 0x0138;				 //  绘制静态控件前发送此消息给它的父窗口，可用来设置静态控件的文本和背景颜色
        public const int WmMousemove = 0x0200;				 //  鼠标移动
        public const int WmLbuttondown = 0x0201;				 //  按下鼠标左键
        public const int WmLbuttonup = 0x0202;				 //  释放鼠标左键
        public const int WmLbuttondblclk = 0x0203;				 //  双击鼠标左键
        public const int WmRbuttondown = 0x0204;				 //  按下鼠标右键
        public const int WmRbuttonup = 0x0205;				 //  释放鼠标右键
        public const int WmRbuttondblclk = 0x0206;				 //  双击鼠标右键
        public const int WmMbuttondown = 0x0207;				 //  按下鼠标中键
        public const int WmMbuttonup = 0x0208;				 //  释放鼠标中键
        public const int WmMbuttondblclk = 0x0209;				 //  双击鼠标中键
        public const int WmMousewheel = 0x020A;				 //  当鼠标滚轮转动时发送此消息给当前获得焦点的窗口
        public const int WmParentnotify = 0x0210;				 //  当MDI子窗口被创建或被销毁，或当光标位于子窗口上且用户按了一下鼠标键时，发送此消息给它的父窗口
        public const int WmEntermenuloop = 0x0211;				 //  发送此消息通知应用程序的主窗口进程已经进入了菜单模式循环
        public const int WmExitmenuloop = 0x0212;				 //  发送此消息通知应用程序的主窗口进程已经退出了菜单模式循环
        public const int WmSizing = 0x0214;				 //  调整窗口大小时发送此消息给窗口，通过此消息应用程序可以监视或修改窗口大小和位置
        public const int WmCapturechanged = 0x0215;				 //  当窗口设定为不捕获鼠标事件时，发送此消息给该窗口
        public const int WmMoving = 0x0216;				 //  移动窗口时发送此消息给窗口，通过此消息应用程序可以监视或修改窗口大小和位置
        public const int WmPowerbroadcast = 0x0218;				 //  发送此消息给应用程序通知它有关电源管理事件
        public const int WmDevicechange = 0x0219;				 //  当设备的硬件配置改变时发送此消息给应用程序或设备驱动程序
        public const int WmMdicreate = 0x0220;				 //  应用程序发送此消息给多文档的客户窗口来创建一个MDI子窗口
        public const int WmMdidestroy = 0x0221;				 //  应用程序发送此消息给多文档的客户窗口来关闭一个MDI子窗口
        public const int WmMdiactivate = 0x0222;				 //  应用程序发送此消息给多文档的客户窗口通知客户窗口激活另一个MDI子窗口，当客户窗口收到此消息后，它发出public const int WM_MDIACTIVE消息给MDI子窗口（未激活）来激活它
        public const int WmMdirestore = 0x0223;				 //  应用程序发送此消息给MDI客户窗口通知子窗口恢复到原来大小
        public const int WmMdinext = 0x0224;				 //  应用程序发送此消息给MDI客户窗口激活下一个或前一个窗口
        public const int WmMdimaximize = 0x0225;				 //  应用程序发送此消息给MDI客户窗口以最大化一个MDI子窗口
        public const int WmMditile = 0x0226;				 //  应用程序发送此消息给MDI客户窗口以平铺方式重新排列所有MDI子窗口
        public const int WmMdicascade = 0x0227;				 //  应用程序发送此消息给MDI客户窗口以层叠方式重新排列所有MDI子窗口
        public const int WmMdiiconarrange = 0x0228;				 //  应用程序发送此消息给MDI客户窗口重新排列所有最小化的MDI子窗口
        public const int WmMdigetactive = 0x0229;				 //  应用程序发送此消息给MDI客户窗口以找到激活的子窗口的句柄
        public const int WmMdisetmenu = 0x0230;				 //  应用程序发送此消息给MDI客户窗口用MDI菜单代替子窗口的菜单
        public const int WmEntersizemove = 0x0231;				 //  当窗口进入移动或改变大小模式循环时，发送此消息给该窗口
        public const int WmExitsizemove = 0x0232;				 //  当窗口退出移动或改变大小模式循环时，发送此消息给该窗口
        public const int WmDropfiles = 0x0233;				 //  当用户在应用程序窗口中拖动某个文件时，产生此消息
        public const int WmMdirefreshmenu = 0x0234;				 //  应用程序发送此消息给MDI客户窗口以刷新窗口菜单
        public const int WmMousehover = 0x02A1;				 //  当光标在窗口客户区悬停超过TrackMouseEvent()指定的时间时，发送此消息给该窗口
        public const int WmMouseleave = 0x02A3;				 //    当光标离开窗口客户区超过TrackMouseEvent()指定的时间时，发送此消息给该窗口
        public const int WmCut = 0x0300;				 //  应用程序发送此消息给一个编辑框或ComboBox以删除当前选择的文本
        public const int WmCopy = 0x0301;				 //  应用程序发送此消息给一个编辑框或ComboBox以复制当前选择的文本到剪贴板
        public const int WmPaste = 0x0302;				 //  应用程序发送此消息给一个编辑框或ComboBox以从剪贴板中得到数据
        public const int WmClear = 0x0303;				 //  应用程序发送此消息给一个编辑框或ComboBox以清除当前选择的内容
        public const int WmUndo = 0x0304;				 //  应用程序发送此消息给一个编辑框或ComboBox以撤消最后一次操作
        public const int WmDestroyclipboard = 0x0307;				 //  当调用EmptyClipboard()清空剪贴板时，发送此消息给剪贴板所有者
        public const int WmDrawclipboard = 0x0308;				 //  当剪贴板的内容变化时发送此消息给剪贴板观察链中的第一个窗口，它允许用剪贴板观察窗口来显示剪贴板的新内容
        public const int WmPaintclipboard = 0x0309;				 //  当剪贴板包含CF_OWNERDIPLAY格式的数据且剪贴板观察窗口的客户区需要重绘时，发送此消息给剪贴板所有者
        public const int WmVscrollclipboard = 0x030A;				 //  当剪贴板包含CF_OWNERDIPLAY格式的数据且剪贴板观察窗口发生垂直滚动条事件时，剪贴板观察窗口发送此消息给剪贴板所有者
        public const int WmSizeclipboard = 0x030B;				 //  当剪贴板包含CF_OWNERDIPLAY格式的数据且剪贴板观察窗口的客户区域的大小已经改变时，剪贴板观察窗口发送此消息给剪贴板的所有者
        public const int WmAskcbformatname = 0x030C;				 //  剪贴板观察窗口发送此消息给剪贴板所有者以获得CF_OWNERDISPLAY剪贴板格式的名字
        public const int WmChangecbchain = 0x030D;				 //  当一个窗口从剪贴板观察链中移去时发送此消息给剪贴板观察链中的第一个窗口
        public const int WmHscrollclipboard = 0x030E;				 //  当剪贴板包含CF_OWNERDIPLAY格式的数据且剪贴板观察窗口发生水平滚动条事件时，剪贴板观察窗口发送此消息给剪贴板所有者
        public const int WmQuerynewpalette = 0x030F;				 //  发送此消息给将要获得键盘焦点的窗口，此消息使窗口在获得焦点时同时有机会实现它的逻辑调色板
        public const int WmPaletteischanging = 0x0310;				 //  应用程序将要实现它的逻辑调色板时发送此消息通知所有应用程序
        public const int WmPalettechanged = 0x0311;				 //  获得焦点的窗口实现它的逻辑调色板后发送此消息给所有顶级并重叠的窗口，以此来改变系统调色板
        public const int WmHotkey = 0x0312;				 //  当用户按下由RegisterHotKey()注册的热键时产生此消息
        public const int WmPrint = 0x0317;				 //  应用程序发送此消息给窗口，要求窗口在指定设备环境中绘制自己，一般情况下是打印机设备环境
        public const int WmPrintclient = 0x0318;				 //  应用程序发送此消息给窗口，要求窗口在指定设备环境中绘制窗口客户区，一般情况下是打印机设备环境
        public const int WmApp = 0x8000;				 //  帮助用户自定义消息，自定义消息可以为public const int WM_APP+X，X为正整数
        public const int WmUser = 0x0400;				 //   帮助用户自定义消息，自定义消息可以为public const int WM_USER+X，X为正整数


        //sc命令
        public const int ScSize = 0xF000;
        public const int ScMove = 0xF010;
        public const int ScMinimize = 0xF020;
        public const int ScMaximize = 0xF030;
        public const int ScNextwindow = 0xF040;
        public const int ScPrevwindow = 0xF050;
        public const int ScClose = 0xF060;
        public const int ScVscroll = 0xF070;
        public const int ScHscroll = 0xF080;
        public const int ScMousemenu = 0xF090;
        public const int ScKeymenu = 0xF100;
        public const int ScArrange = 0xF110;
        public const int ScRestore = 0xF120;
        public const int ScTasklist = 0xF130;
        public const int ScScreensave = 0xF140;
        public const int ScHotkey = 0xF150;
        public const int ScDefault = 0xF160;
        public const int ScMonitorpower = 0xF170;
        public const int ScContexthelp = 0xF180;
        public const int ScSeparator = 0xF00F;

    }
}