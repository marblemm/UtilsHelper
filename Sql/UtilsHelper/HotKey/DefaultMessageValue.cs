namespace UtilsHelper.HotKey
{
    /// <summary>
    /// Windows ������Ϣ��ȫ
    /// </summary>
    public class DefaultMessageValue
    {
        //������Ϣ�ֲ�
        //        ��Ϣ��Χ    ˵ ��
        //      0 �� public const int WM_USER �C 1         //ϵͳ��Ϣ
        //      public const int WM_USER �� = 0x7FFF;				 //        //�Զ��崰����������Ϣ
        //      public const int WM_APP �� = 0xBFFF;				 //         //Ӧ�ó����Զ�����Ϣ
        //      = 0xC000;				 // �� = 0xFFFF;				 //         //Ӧ�ó����ַ�����Ϣ
        //      >= 0xFFFF;				 //                  //Ϊ�Ժ�ϵͳӦ�ñ���

        //����Windows��Ϣ

        public const int WmHotKey = 0x312;						 //  //������Ϣ���ȼ�
        public const int WmCreate = 0x0001;				 //;				    //������Ϣ������
        public const int WmDestory = 0x0002;				 //;				    //������Ϣ������
        public const int WmMove = 0x0003;				 //;				       //�ƶ�һ������
        public const int WmSize = 0x0005;				 //;				       //�ı�һ�����ڵĴ�С
        public const int WmActivate = 0x0006;				 //;				       //һ�����ڱ������ʧȥ����״̬
        public const int WmNull = 0x0000;				 // // ����Ϣ������Ϣ�������մ��ں���
        public const int WmSetfocus = 0x0007;				 //  ��ý����
        public const int WmKillfocus = 0x0008;				 //  ʧȥ����
        public const int WmEnable = 0x000A;				 //  Ӧ�ó���Enable״̬�ı�ʱ����
        public const int WmSetredraw = 0x000B;				 //  ���ô����Ƿ����ػ�
        public const int WmSettext = 0x000C;				 //  Ӧ�ó����ʹ���Ϣ������һ�����ڵ��ı�
        public const int WmGettext = 0x000D;				 //  Ӧ�ó����ʹ���Ϣ�����ƶ�Ӧ���ڵ��ı���������
        public const int WmGettextlength = 0x000E;				 //  �õ���һ�������йص��ı��ĳ��ȣ����������ַ���  
        public const int WmPaint = 0x000F;				 //  Ҫ��һ�������ػ��Լ�
        public const int WmClose = 0x0010;				 //  ��һ�����ڻ�Ӧ�ó���Ҫ�ر�ʱ����һ���ź�
        public const int WmQueryendsession = 0x0011;				 //  �û�ѡ������Ի����Ӧ�ó����Լ�����ExitWindows()����
        public const int WmQuit = 0x0012;				 //   ���������������л�Ӧ�ó������Postquitmessage()��������������Ϣ
        public const int WmQueryopen = 0x0013;				 //  ���û����ڻָ���ǰ�Ĵ�Сλ��ʱ���Ѵ���Ϣ���͸�ĳ��ͼ��
        public const int WmErasebkgnd = 0x0014;				 //  �����ڱ������뱻����ʱ�������ڴ��ڸı��Сʱ��
        public const int WmSyscolorchange = 0x0015;				 //  ��ϵͳ��ɫ�ı�ʱ�����ʹ���Ϣ�����ж�������
        public const int WmEndsession = 0x0016;				 //  ��ϵͳ���̷���public const int WM_QUERYENDSESSION��Ϣ�󣬴���Ϣ���͸�Ӧ�ó���
        public const int WmShowwindow = 0x0018;				 //  �����ػ���ʾ�����Ƿ��ʹ���Ϣ���������
        public const int WmActivateapp = 0x001C;				 //  ��ĳ�����ڽ�������ʱ����������ں͵�ǰ�������ʧȥ������ڻ��յ�����Ϣ��������Ϣ��Ӧ�ó����ĸ������Ǽ���ģ��ĸ��ǷǼ����
        public const int WmFontchange = 0x001D;				 //  ��ϵͳ��������Դ��仯ʱ���ʹ���Ϣ�����ж�������
        public const int WmTimechange = 0x001E;				 //��ϵͳ��ʱ��仯ʱ���ʹ���Ϣ�����ж�������
        public const int WmCancelmode = 0x001F;				 //���ʹ���Ϣ��ȡ��ĳ�����ڽ��еĲ���
        public const int WmSetcursor = 0x0020;				 //��������������ĳ���������ƶ����������û�б�����ʱ������Ϣ���ô���
        public const int WmMouseactivate = 0x0021;				 //�������ĳ���Ǽ���Ĵ����ж��û�����������ĳ�������ʹ���Ϣ����ǰ����
        public const int WmChildactivate = 0x0022;				 //���ʹ���Ϣ��MDI�Ӵ��ڵ��û�����˴��ڵı��������򵱴��ڱ�����ƶ����ı��С
        public const int WmQueuesync = 0x0023;				 //����Ϣ�ɻ��ڼ������ѵ�������ͣ�ͨ��WH_JOURNALPALYBACK��Hook���������û�������Ϣ
        public const int WmGetminmaxinfo = 0x0024;				 //������Ҫ��Ҫ�ı��С��λ��ʱ�����ʹ���Ϣ���ô���
        public const int WmPainticon = 0x0026;				 //������ͼ�꽫Ҫ���ػ�ʱ�����ʹ���Ϣ���ô���
        public const int WmIconerasebkgnd = 0x0027;				 //��һ����С�����ڵ�ͼ�����ػ�ǰ����ͼ�걳�����뱻�ػ�ʱ�����ʹ���Ϣ���ô���
        public const int WmNextdlgctl = 0x0028;				 //���ʹ���Ϣ��һ���Ի�������Ը��Ľ���λ��
        public const int WmSpoolerstatus = 0x002A;				 //����ӡ�����ж����ӻ����һ����ҵʱ��������Ϣ
        public const int WmDrawitem = 0x002B;				 //��Button��ComboBox��Listbox��Menu�ؼ�����۸ı�ʱ�����ʹ���Ϣ����Щ�ؼ���������
        public const int WmMeasureitem = 0x002C;				 //��Button��ComboBox��list box��ListView��Menu �����ʱ�����ʹ���Ϣ���ؼ���������
        public const int WmDeleteitem = 0x002D;				 //��ListBox �� ComboBox �����ٻ�ĳЩ��ͨ������LB_DELETESTRING��LB_RESETCONTENT�� CB_DELETESTRING��CB_RESETCONTENT ��Ϣ��ɾ��ʱ�����ʹ���Ϣ���ؼ���������
        public const int WmVkeytoitem = 0x002E;				 //һ������LBS_WANTKEYBOARDINPUT����ListBox�ؼ����ʹ���Ϣ�����������ߣ��Դ�����Ӧpublic const int WM_KEYDOWN��Ϣ
        public const int WmChartoitem = 0x002F;				 //һ������LBS_WANTKEYBOARDINPUT����ListBox�ؼ����ʹ���Ϣ�����������ߣ��Դ�����Ӧpublic const int WM_CHAR��Ϣ
        public const int WmSetfont = 0x0030;				 //Ӧ�ó�����ƿؼ�ʱ�����ʹ���Ϣ�õ��Ժ���������ƿؼ��е��ı�
        public const int WmGetfont = 0x0031;				 //Ӧ�ó����ʹ���Ϣ�õ���ǰ�ؼ������ı�������
        public const int WmSethotkey = 0x0032;				 //Ӧ�ó����ʹ���Ϣ��һ��������һ���ȼ������
        public const int WmGethotkey = 0x0033;				 //Ӧ�ó����ʹ���Ϣ���ж��ȼ���ĳ�������Ƿ��й���
        public const int WmQuerydragicon = 0x0037;				 //����Ϣ���͸���С�����ڣ����˴��ڽ�Ҫ���ϷŶ���������û�ж���ͼ�꣬Ӧ�ó���ͷ���һ��ͼ�����ľ�������û��Ϸ�ͼ��ʱϵͳ��ʾ���ͼ�����
        public const int WmCompareitem = 0x0039;				 //���ʹ���Ϣ���ж�ComboBox��ListBox�����ӵ�������λ��
        public const int WmCompacting = 0x0041;				 //��ʾ�ڴ��Ѿ�������
        public const int WmWindowposchanging = 0x0046;				 //������SetWindowPos()�����ı䴰�ڵĴ�С��λ�ú󣬷��ʹ���Ϣ���ô���
        public const int WmPower = 0x0048;				 //��ϵͳ���������״̬ʱ���ʹ���Ϣ�����н���
        public const int WmCopydata = 0x004A;				 //��һ��Ӧ�ó��򴫵����ݸ���һ��Ӧ�ó���ʱ���ʹ���Ϣ
        public const int WmCanceljournal = 0x004B;				 //��ĳ���û�ȡ��������־����״̬�����ʹ���Ϣ��Ӧ�ó���
        public const int WmNotify = 0x004E;				 //��ĳ���ؼ���ĳ���¼��Ѿ�����������ؼ���Ҫ�õ�һЩ��Ϣʱ�����ʹ���Ϣ�����ĸ�����
        public const int WmInputlangchangerequest = 0x0050;				 //���û�ѡ��ĳ���������ԣ����������Ե��ȼ��ı�
        public const int WmInputlangchange = 0x0051;				 //��Ӧ�ó����������Ըı���ʹ���Ϣ����Ӱ����������
        public const int WmTcard = 0x0052;				 //��Ӧ�ó����Ѿ���ʼ��Windows��������ʱ���ʹ���Ϣ��Ӧ�ó���
        public const int WmHelp = 0x0053;				 //���û�������F1�����ĳ���˵��Ǽ���ģ��ͷ��ʹ���Ϣ���˴��ڹ����Ĳ˵�������ͷ��͸��н���Ĵ��ڣ������ǰ��û�н��㣬�ͰѴ���Ϣ���͸���ǰ����Ĵ���
        public const int WmUserchanged = 0x0054;				 //���û��Ѿ���¼���˳����ʹ���Ϣ�����еĴ��ڣ����û���¼���˳�ʱϵͳ�����û��ľ���������Ϣ�����û���������ʱϵͳ���Ϸ��ʹ���Ϣ
        public const int WmNotifyformat = 0x0055;				 //���ÿؼ������ǵĸ�����ͨ������Ϣ���ж���public const int WM_NOTIFY��Ϣ����ʹ��ANSI����UNICODE��ʽ�Ľṹ��ʹ�ô˿ؼ���ʹĳ���ؼ������ĸ��ؼ������໥ͨ��
        public const int WmContextmenu = 0x007B;				 //        ���û���ĳ�������е���Ҽ������ʹ���Ϣ���ô���
        public const int WmStylechanging = 0x007C;				 //����Ҫ����SetWindowLong()�������ڵ�һ���������ʱ�����ʹ���Ϣ���ô���
        public const int WmStylechanged = 0x007D;				 //������SetWindowLong()�����ı��˴��ڵ�һ���������󣬷��ʹ���Ϣ���ô���
        public const int WmDisplaychange = 0x007E;				 //        ����ʾ���ķֱ��ʸı���ʹ���Ϣ�����еĴ���
        public const int WmGeticon = 0x007F;				 //        ���ʹ���Ϣ��ĳ�����ڣ�������ĳ�������й����Ĵ�ͼ���Сͼ��ľ��
        public const int WmSeticon = 0x0080;				 //        Ӧ�ó����ʹ���Ϣ��һ���µĴ�ͼ���Сͼ����ĳ�����ڹ���
        public const int WmNccreate = 0x0081;				 //        ��ĳ�����ڵ�һ�α�����ʱ������Ϣ��public const int WM_CREATE��Ϣ������ǰ����
        public const int WmNcdestroy = 0x0082;				 //        ����Ϣ֪ͨĳ�����ڣ��������ٷǿͻ���
        public const int WmNccalcsize = 0x0083;				 //        ������ĳ�����ڵĿͻ�����С��λ��ʱ���ʹ���Ϣ
        public const int WmNchittest = 0x0084;				 //        �ƶ���꣬��ס���ͷ����ʱ��������Ϣ
        public const int WmNcpaint = 0x0085;				 //        ��ĳ�����ڵĿ�ܱ��뱻����ʱ��Ӧ�ó����ʹ���Ϣ���ô���
        public const int WmNcactivate = 0x0086;				 //        ͨ���ı�ĳ�����ڵķǿͻ�������ʾ�����Ǵ��ڼ���ǷǼ���״̬ʱ������Ϣ�����͸��ô���
        public const int WmNcmousemove = 0x00A0;				 //        ������ڴ��ڵķǿͻ��������ڱ��������߿����ƶ�ʱ���ʹ���Ϣ���ô���
        public const int WmNclbuttondown = 0x00A1;				 //        ������ڴ��ڵķǿͻ���������������ʱ���ʹ���Ϣ
        public const int WmNclbuttonup = 0x00A2;				 //        ������ڴ��ڵķǿͻ������ͷ�������ʱ���ʹ���Ϣ
        public const int WmNclbuttondblclk = 0x00A3;				 //        ������ڴ��ڵķǿͻ�����˫��������ʱ���ʹ���Ϣ
        public const int WmNcrbuttondown = 0x00A4;				 //        ������ڴ��ڵķǿͻ�������������Ҽ�ʱ���ʹ���Ϣ
        public const int WmNcrbuttonup = 0x00A5;				 //        ������ڴ��ڵķǿͻ������ͷ�����Ҽ�ʱ���ʹ���Ϣ
        public const int WmNcrbuttondblclk = 0x00A6;				 //        ������ڴ��ڵķǿͻ�����˫������Ҽ�ʱ���ʹ���Ϣ
        public const int WmNcmbuttondown = 0x00A7;				 //        ������ڴ��ڵķǿͻ�������������м�ʱ���ʹ���Ϣ
        public const int WmNcmbuttonup = 0x00A8;				 //        ������ڴ��ڵķǿͻ������ͷ�����м�ʱ���ʹ���Ϣ
        public const int WmNcmbuttondblcl = 0x00A9;				 //        ������ڴ��ڵķǿͻ�����˫������м�ʱ���ʹ���Ϣ
        public const int WmKeydown = 0x0100;				 //        ����һ����ϵͳ�������¼�ʱδ���¡�ALT������
        public const int WmKeyup = 0x0101;				 //        �ͷ�һ����ϵͳ��
        public const int WmChar = 0x0102;				 //        ����ĳ������TranslateMessage()ת��public const int WM_KEYDOWN���ͱ���Ϣ
        public const int WmDeadchar = 0x0103;				 //        �ͷ�ĳ������TranslateMessage()ת��public const int WM_KEYUP���ͱ���Ϣ
        public const int WmSyskeydown = 0x0104;				 //        ����סALT��ͬʱ����������ʱ���ʹ���Ϣ��ӵ�м��̽���Ĵ���
        public const int WmSyskeyup = 0x0105;				 //        ���ͷ�һ����ͬʱ��סALT��ʱ���ʹ���Ϣ��ӵ�м��̽���Ĵ���
        public const int WmSyschar = 0x0106;				 //��TranslateMessage()ת��public const int WM_SYSKEYDOWN���ʹ���Ϣ��ӵ�м��̽���Ĵ���
        public const int WmSysdeadchar = 0x0107;				 //��TranslateMessage()ת��public const int WM_SYSKEYUP���ʹ���Ϣ��ӵ�м��̽���Ĵ���
        public const int WmInitdialog = 0x0110;				 //        �ڱ���ʾǰ���ʹ���Ϣ�Ի���ͨ���ô���Ϣ��ʼ���ؼ���ִ����������
        public const int WmCommand = 0x0111;				 //        ѡ�񴰿ڲ˵����ĳ���ؼ�����һ����Ϣ�����ĸ����ڻ���һ����ݼ�ʱ��������Ϣ
        public const int WmSyscommand = 0x0112;				 //        ѡ�񴰿ڲ˵����ѡ����󻯻���С��ʱ�����ʹ���Ϣ���ô���
        public const int WmTimer = 0x0113;				 //        �����˶�ʱ���¼�
        public const int WmHscroll = 0x0114;				 //        ������ˮƽ����������һ�������¼�ʱ���ʹ���Ϣ���ô��ں͹�������������
        public const int WmVscroll = 0x0115;				 //          �����ڴ�ֱ����������һ�������¼�ʱ���ʹ���Ϣ���ô��ں͹�������������
        public const int WmInitmenu = 0x0116;				 //          ��һ���˵���Ҫ������ʱ���ʹ���Ϣ���������ڰ��²˵�����²˵���ݼ�ʱ����������������ʾǰ���Ĳ˵�
        public const int WmInitmenupopup = 0x0117;				 //          ��һ�������˵����Ӳ˵���Ҫ������ʱ���ʹ���Ϣ����������ʾǰ���޸Ĳ˵������ظ��������˵�
        public const int WmMenuselect = 0x011F;				 //          ѡ��һ���˵���ʱ���ʹ���Ϣ���˵��������ߣ�һ���Ǵ��ڣ�
        public const int WmMenuchar = 0x0120;				 //          ���˵��ѱ��������û�������ĳ�������ǿ�ݼ��������ʹ���Ϣ���˵���������
        public const int WmEnteridle = 0x0121;				 //          ��һ����ģʽ�Ի����˵��������״̬ʱ���ʹ���Ϣ�����������ߣ�����״ָ̬�ڴ�����һ��������ǰ����Ϣ����Ϣ�ж�Ϊ��
        public const int WmMenurbuttonup = 0x0122;				 //          �����λ�ڲ˵�����ʱ���ͷ�����Ҽ���������Ϣ
        public const int WmMenudrag = 0x0123;				 //          ���϶��˵���ʱ�����ʹ���Ϣ���ϷŲ˵���������
        public const int WmMenugetobject = 0x0124;				 //          ���������˵�����ߴӲ˵��������Ƶ��˵������ײ�ʱ�����ʹ���Ϣ���ϷŲ˵���������
        public const int WmUninitmenupopup = 0x0125;				 //          �������˵������Ӳ˵�������ʱ��������Ϣ
        public const int WmMenucommand = 0x0126;				 //          ���û�ѡ��˵���ʱ��������Ϣ
        public const int WmChangeuistate = 0x0127;				 //          Ӧ�ó����ʹ���Ϣ�����û����棨UI��״̬Ӧ�����ı�
        public const int WmUpdateuistate = 0x0128;				 //          Ӧ�ó����ʹ���Ϣ�ı�ָ�����ڼ����Ӵ��ڵ��û����棨UI��״̬
        public const int WmQueryuistate = 0x0129;				 //          Ӧ�ó����ʹ���Ϣ�õ�ĳ�����ڵ��û����棨UI��״̬
        public const int WmCtlcolormsgbox = 0x0132;				 //          ������Ϣ��ǰ���ʹ���Ϣ�����ĸ����ڣ�ͨ����Ӧ������Ϣ�������ڿ���ͨ��ʹ�ø����������ʾ�豸�ľ����������Ϣ����ı��ͱ�����ɫ
        public const int WmCtlcoloredit = 0x0133;				 //          ���Ʊ༭�Ϳؼ�ǰ���ʹ���Ϣ�����ĸ����ڣ����������ñ༭����ı��ͱ�����ɫ
        public const int WmCtlcolorlistbox = 0x0134;				 //           �����б���ؼ�ǰ���ʹ���Ϣ�����ĸ����ڣ����������ñ༭����ı��ͱ�����ɫ
        public const int WmCtlcolorbtn = 0x0135;				 //           ���ư�ť�ؼ�ǰ���ʹ���Ϣ�����ĸ����ڣ����������ñ༭����ı��ͱ�����ɫ
        public const int WmCtlcolordlg = 0x0136;				 //  ���ƶԻ���ǰ���ʹ���Ϣ�����ĸ����ڣ����������ñ༭����ı��ͱ�����ɫ
        public const int WmCtlcolorscrollbar = 0x0137;				 //  ���ƹ������ؼ�ǰ���ʹ���Ϣ�����ĸ����ڣ����������ù������ؼ����ı��ͱ�����ɫ
        public const int WmCtlcolorstatic = 0x0138;				 //  ���ƾ�̬�ؼ�ǰ���ʹ���Ϣ�����ĸ����ڣ����������þ�̬�ؼ����ı��ͱ�����ɫ
        public const int WmMousemove = 0x0200;				 //  ����ƶ�
        public const int WmLbuttondown = 0x0201;				 //  ����������
        public const int WmLbuttonup = 0x0202;				 //  �ͷ�������
        public const int WmLbuttondblclk = 0x0203;				 //  ˫��������
        public const int WmRbuttondown = 0x0204;				 //  ��������Ҽ�
        public const int WmRbuttonup = 0x0205;				 //  �ͷ�����Ҽ�
        public const int WmRbuttondblclk = 0x0206;				 //  ˫������Ҽ�
        public const int WmMbuttondown = 0x0207;				 //  ��������м�
        public const int WmMbuttonup = 0x0208;				 //  �ͷ�����м�
        public const int WmMbuttondblclk = 0x0209;				 //  ˫������м�
        public const int WmMousewheel = 0x020A;				 //  ��������ת��ʱ���ʹ���Ϣ����ǰ��ý���Ĵ���
        public const int WmParentnotify = 0x0210;				 //  ��MDI�Ӵ��ڱ����������٣��򵱹��λ���Ӵ��������û�����һ������ʱ�����ʹ���Ϣ�����ĸ�����
        public const int WmEntermenuloop = 0x0211;				 //  ���ʹ���Ϣ֪ͨӦ�ó���������ڽ����Ѿ������˲˵�ģʽѭ��
        public const int WmExitmenuloop = 0x0212;				 //  ���ʹ���Ϣ֪ͨӦ�ó���������ڽ����Ѿ��˳��˲˵�ģʽѭ��
        public const int WmSizing = 0x0214;				 //  �������ڴ�Сʱ���ʹ���Ϣ�����ڣ�ͨ������ϢӦ�ó�����Լ��ӻ��޸Ĵ��ڴ�С��λ��
        public const int WmCapturechanged = 0x0215;				 //  �������趨Ϊ����������¼�ʱ�����ʹ���Ϣ���ô���
        public const int WmMoving = 0x0216;				 //  �ƶ�����ʱ���ʹ���Ϣ�����ڣ�ͨ������ϢӦ�ó�����Լ��ӻ��޸Ĵ��ڴ�С��λ��
        public const int WmPowerbroadcast = 0x0218;				 //  ���ʹ���Ϣ��Ӧ�ó���֪ͨ���йص�Դ�����¼�
        public const int WmDevicechange = 0x0219;				 //  ���豸��Ӳ�����øı�ʱ���ʹ���Ϣ��Ӧ�ó�����豸��������
        public const int WmMdicreate = 0x0220;				 //  Ӧ�ó����ʹ���Ϣ�����ĵ��Ŀͻ�����������һ��MDI�Ӵ���
        public const int WmMdidestroy = 0x0221;				 //  Ӧ�ó����ʹ���Ϣ�����ĵ��Ŀͻ��������ر�һ��MDI�Ӵ���
        public const int WmMdiactivate = 0x0222;				 //  Ӧ�ó����ʹ���Ϣ�����ĵ��Ŀͻ�����֪ͨ�ͻ����ڼ�����һ��MDI�Ӵ��ڣ����ͻ������յ�����Ϣ��������public const int WM_MDIACTIVE��Ϣ��MDI�Ӵ��ڣ�δ�����������
        public const int WmMdirestore = 0x0223;				 //  Ӧ�ó����ʹ���Ϣ��MDI�ͻ�����֪ͨ�Ӵ��ڻָ���ԭ����С
        public const int WmMdinext = 0x0224;				 //  Ӧ�ó����ʹ���Ϣ��MDI�ͻ����ڼ�����һ����ǰһ������
        public const int WmMdimaximize = 0x0225;				 //  Ӧ�ó����ʹ���Ϣ��MDI�ͻ����������һ��MDI�Ӵ���
        public const int WmMditile = 0x0226;				 //  Ӧ�ó����ʹ���Ϣ��MDI�ͻ�������ƽ�̷�ʽ������������MDI�Ӵ���
        public const int WmMdicascade = 0x0227;				 //  Ӧ�ó����ʹ���Ϣ��MDI�ͻ������Բ����ʽ������������MDI�Ӵ���
        public const int WmMdiiconarrange = 0x0228;				 //  Ӧ�ó����ʹ���Ϣ��MDI�ͻ�������������������С����MDI�Ӵ���
        public const int WmMdigetactive = 0x0229;				 //  Ӧ�ó����ʹ���Ϣ��MDI�ͻ��������ҵ�������Ӵ��ڵľ��
        public const int WmMdisetmenu = 0x0230;				 //  Ӧ�ó����ʹ���Ϣ��MDI�ͻ�������MDI�˵������Ӵ��ڵĲ˵�
        public const int WmEntersizemove = 0x0231;				 //  �����ڽ����ƶ���ı��Сģʽѭ��ʱ�����ʹ���Ϣ���ô���
        public const int WmExitsizemove = 0x0232;				 //  �������˳��ƶ���ı��Сģʽѭ��ʱ�����ʹ���Ϣ���ô���
        public const int WmDropfiles = 0x0233;				 //  ���û���Ӧ�ó��򴰿����϶�ĳ���ļ�ʱ����������Ϣ
        public const int WmMdirefreshmenu = 0x0234;				 //  Ӧ�ó����ʹ���Ϣ��MDI�ͻ�������ˢ�´��ڲ˵�
        public const int WmMousehover = 0x02A1;				 //  ������ڴ��ڿͻ�����ͣ����TrackMouseEvent()ָ����ʱ��ʱ�����ʹ���Ϣ���ô���
        public const int WmMouseleave = 0x02A3;				 //    ������뿪���ڿͻ�������TrackMouseEvent()ָ����ʱ��ʱ�����ʹ���Ϣ���ô���
        public const int WmCut = 0x0300;				 //  Ӧ�ó����ʹ���Ϣ��һ���༭���ComboBox��ɾ����ǰѡ����ı�
        public const int WmCopy = 0x0301;				 //  Ӧ�ó����ʹ���Ϣ��һ���༭���ComboBox�Ը��Ƶ�ǰѡ����ı���������
        public const int WmPaste = 0x0302;				 //  Ӧ�ó����ʹ���Ϣ��һ���༭���ComboBox�ԴӼ������еõ�����
        public const int WmClear = 0x0303;				 //  Ӧ�ó����ʹ���Ϣ��һ���༭���ComboBox�������ǰѡ�������
        public const int WmUndo = 0x0304;				 //  Ӧ�ó����ʹ���Ϣ��һ���༭���ComboBox�Գ������һ�β���
        public const int WmDestroyclipboard = 0x0307;				 //  ������EmptyClipboard()��ռ�����ʱ�����ʹ���Ϣ��������������
        public const int WmDrawclipboard = 0x0308;				 //  ������������ݱ仯ʱ���ʹ���Ϣ��������۲����еĵ�һ�����ڣ��������ü�����۲촰������ʾ�������������
        public const int WmPaintclipboard = 0x0309;				 //  �����������CF_OWNERDIPLAY��ʽ�������Ҽ�����۲촰�ڵĿͻ�����Ҫ�ػ�ʱ�����ʹ���Ϣ��������������
        public const int WmVscrollclipboard = 0x030A;				 //  �����������CF_OWNERDIPLAY��ʽ�������Ҽ�����۲촰�ڷ�����ֱ�������¼�ʱ��������۲촰�ڷ��ʹ���Ϣ��������������
        public const int WmSizeclipboard = 0x030B;				 //  �����������CF_OWNERDIPLAY��ʽ�������Ҽ�����۲촰�ڵĿͻ�����Ĵ�С�Ѿ��ı�ʱ��������۲촰�ڷ��ʹ���Ϣ���������������
        public const int WmAskcbformatname = 0x030C;				 //  ������۲촰�ڷ��ʹ���Ϣ���������������Ի��CF_OWNERDISPLAY�������ʽ������
        public const int WmChangecbchain = 0x030D;				 //  ��һ�����ڴӼ�����۲�������ȥʱ���ʹ���Ϣ��������۲����еĵ�һ������
        public const int WmHscrollclipboard = 0x030E;				 //  �����������CF_OWNERDIPLAY��ʽ�������Ҽ�����۲촰�ڷ���ˮƽ�������¼�ʱ��������۲촰�ڷ��ʹ���Ϣ��������������
        public const int WmQuerynewpalette = 0x030F;				 //  ���ʹ���Ϣ����Ҫ��ü��̽���Ĵ��ڣ�����Ϣʹ�����ڻ�ý���ʱͬʱ�л���ʵ�������߼���ɫ��
        public const int WmPaletteischanging = 0x0310;				 //  Ӧ�ó���Ҫʵ�������߼���ɫ��ʱ���ʹ���Ϣ֪ͨ����Ӧ�ó���
        public const int WmPalettechanged = 0x0311;				 //  ��ý���Ĵ���ʵ�������߼���ɫ����ʹ���Ϣ�����ж������ص��Ĵ��ڣ��Դ����ı�ϵͳ��ɫ��
        public const int WmHotkey = 0x0312;				 //  ���û�������RegisterHotKey()ע����ȼ�ʱ��������Ϣ
        public const int WmPrint = 0x0317;				 //  Ӧ�ó����ʹ���Ϣ�����ڣ�Ҫ�󴰿���ָ���豸�����л����Լ���һ��������Ǵ�ӡ���豸����
        public const int WmPrintclient = 0x0318;				 //  Ӧ�ó����ʹ���Ϣ�����ڣ�Ҫ�󴰿���ָ���豸�����л��ƴ��ڿͻ�����һ��������Ǵ�ӡ���豸����
        public const int WmApp = 0x8000;				 //  �����û��Զ�����Ϣ���Զ�����Ϣ����Ϊpublic const int WM_APP+X��XΪ������
        public const int WmUser = 0x0400;				 //   �����û��Զ�����Ϣ���Զ�����Ϣ����Ϊpublic const int WM_USER+X��XΪ������


        //sc����
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