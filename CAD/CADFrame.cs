using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CAD
{
    public partial class CadFrame : Form
    {
        private BaseTool _currentTool;//��ǰ��Ӧ�ù���
        private ArrayList _currentShapes;//��ǰ��ʾ��ͼ�μ���
        private Hashtable _registerToolMap;//��ǰע��Ĺ��߼���
        private ArrayList _historyShapes;//��ʷͼ�εĿ��ռ���

        public  Color Clr = Color.Black;
        public  int LineWidth = 1;

        public const string LinetoolRegistername = "LINETOOL_REGISTERNAME";
        public const string HandtoolRegistername = "HANDTOOL_REGISTERNAME";
        public const string Rectanglet0OlRegistername = "RECTANGLET0OL_REGISTERNAME";
        public const string EllipsetoolRegistername = "ELLIPSETOOL_REGISTERNAME";
        public const string CircletoolRegistername = "CIRCLETOOL_REGISTERNAME";

        public CadFrame()
        {
            InitializeComponent();
            _currentShapes = new ArrayList();//ʵ����ǰ��ʾ��ͼ�μ��϶���
            _registerToolMap = new Hashtable();//ע�Ṥ�ߵļ��϶���
            _historyShapes = new ArrayList();//��ʷͼ�εĿ��ռ��϶���
            RegisterTool(LinetoolRegistername, new LineTool());//ע���߹���
            RegisterTool(HandtoolRegistername, new HandTool());//ע��ץȡ����
            RegisterTool(Rectanglet0OlRegistername, new RectangleTool());//ע����ι���
            RegisterTool(EllipsetoolRegistername, new EllipseTool());//ע����Բ����
            RegisterTool(CircletoolRegistername, new CircleTool());//ע��Բ�ι���
            Record();
        }

        public ArrayList GetCurrentShapes()
        {
            return _currentShapes;
        }

        public void SetCurrentShapes(ArrayList currentShapes)
        {
            _currentShapes = currentShapes;
        }

        public BaseTool GetCurrentTool()
        {
            return _currentTool;
        }

        public void SetCurrentTool(BaseTool currentTool)
        {
            _currentTool = currentTool;
        }

        public Hashtable GetRegisterToolMap()
        {
            return _registerToolMap;
        }

        public void SetRegisterToolMap(Hashtable registerToolMap)
        {
            _registerToolMap = registerToolMap;
        }

        public ArrayList GetHistoryShape()
        {
            return _historyShapes;
        }

        public void SetHistoryShapes(ArrayList historyShapes)
        {
            _historyShapes = historyShapes;
        }

        public void RegisterTool(string registerName, BaseTool registerTool)//ע�Ṥ��
        {
            GetRegisterToolMap().Add(registerName, registerTool);
            registerTool.SetRefCadPanel(this);
        }

        public void UseTool(string registerName)//Ӧ�ù���
        {
            if (GetCurrentTool() != null) GetCurrentTool().UnSet();//ж��֮ǰ�Ĺ���
            BaseTool setTool = (BaseTool)GetRegisterToolMap()[registerName];//��������Ҫ�õĹ���
            if (setTool != null)
            {
                setTool.Set();
                SetCurrentTool(setTool);//װ�ع���
            }
        }

        int _undoIndex;//���˵�����
        public void Record()//���ձ���ķ��� 
        {
            if (_undoIndex > 0)//���л���ʱ����ջ��˻�ÿ���
            {
                while (_undoIndex != 0)
                {
                    GetHistoryShape().RemoveAt(GetHistoryShape().Count - 1);
                    _undoIndex--;
                }
            }
            GetHistoryShape().Add(CloneShapArray(GetCurrentShapes()));//�������
        }
        public void Redo()//����
        {
            if (_undoIndex > 0)//���л���ʱ�ſ�����
            {
                _undoIndex--;//����ʷ����ȡ�ص���ǰͼ����
                SetCurrentShapes(CloneShapArray((ArrayList)GetHistoryShape()[GetHistoryShape().Count - 1 - _undoIndex]));
            }
            Refresh();
        }

        public void Undo()//����
        {
            if ((GetHistoryShape().Count - 1 - _undoIndex) > 0)//��ʷ�����л�����ʷ���ܻ���
            {
                _undoIndex++;//����ʷ����ȡ�ص���ǰͼ����
                SetCurrentShapes((CloneShapArray((ArrayList)GetHistoryShape()[GetHistoryShape().Count - 1 - _undoIndex])));
            }
            Refresh();
        }

        public ArrayList CloneShapArray(ArrayList shapeArrayList)//ͼ�μ�������
        {
            ArrayList returnShapeArrayList = new ArrayList();
            for (int i = 0; i < shapeArrayList.Count; i++)
            {
                returnShapeArrayList.Add(((BaseShape)shapeArrayList[i]).CopySelf());
            }
            return returnShapeArrayList;
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            UseTool(LinetoolRegistername);
        }

        private void btnHand_Click(object sender, EventArgs e)
        {
            UseTool(HandtoolRegistername);
        }

        private void btnRectangle_Click(object sender, EventArgs e)
        {
            UseTool(Rectanglet0OlRegistername);
        }

        private void btnEllipse_Click(object sender, EventArgs e)
        {
            UseTool(EllipsetoolRegistername);
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            UseTool(CircletoolRegistername);
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            Redo();
        }

        public void Clear()
        {
            _undoIndex = 0;
            SetHistoryShapes(new ArrayList());
            SetCurrentShapes(new ArrayList());
            Record();
            pictureBox1.Refresh();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Save(string filePath)
        {
            Stream s = File.Open(filePath, FileMode.Create, FileAccess.ReadWrite);
            BinaryFormatter b = new BinaryFormatter();
            for (int i = 0; i < GetCurrentShapes().Count; i++)
            {
                b.Serialize(s,GetCurrentShapes()[i]);
            }
            s.Close();
        }

        private void LoadFile(string filePath)
        {
            Stream s = File.Open(filePath, FileMode.Open, FileAccess.Read);
            BinaryFormatter c = new BinaryFormatter();
            ArrayList newShapes = new ArrayList();
            bool forFlat = true;
            for (int i = 0; forFlat; i++)
            {
                try
                {
                    newShapes.Add(c.Deserialize(s));
                }
                catch
                {
                    forFlat = false;
                }
            }
            s.Close();
            SetCurrentShapes(newShapes);
            SetHistoryShapes(new ArrayList());
            Record();
            _undoIndex = 0;
            pictureBox1.Refresh();
        }

        private void btnOpen_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                LoadFile(openFileDialog1.FileName);
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Save(saveFileDialog1.FileName);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //private void pictureBox1_Paint(object sender, PaintEventArgs e)
        //{
        //    Graphics g = e.Graphics;
        //    for (int i = 0; i < currentShapes.Count; i++)
        //    {
        //        ((BaseShape)currentShapes[i]).superDraw(g,this);
        //    }
        //}
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (int i = 0; i < _currentShapes.Count; i++)
            {
                
                string type=((BaseShape)_currentShapes[i]).GetType().ToString();
                switch (type)
                {
                    case "CAD.LineShape":
                        g.DrawLine(new Pen(((BaseShape)_currentShapes[i]).PenColor, ((BaseShape)_currentShapes[i]).Penwidth), ((BaseShape)_currentShapes[i]).GetP1(), ((BaseShape)_currentShapes[i]).GetP2());
                        break;
                    case "CAD.RectangleShape":
                        g.DrawRectangle(new Pen(((BaseShape)_currentShapes[i]).PenColor, ((BaseShape)_currentShapes[i]).Penwidth), ((BaseShape)_currentShapes[i]).GetP1().X, ((BaseShape)_currentShapes[i]).GetP1().Y, (((BaseShape)_currentShapes[i]).GetP2().X - ((BaseShape)_currentShapes[i]).GetP1().X), (((BaseShape)_currentShapes[i]).GetP2().Y - ((BaseShape)_currentShapes[i]).GetP1().Y));
                        g.DrawRectangle(new Pen(((BaseShape)_currentShapes[i]).PenColor, ((BaseShape)_currentShapes[i]).Penwidth), ((BaseShape)_currentShapes[i]).GetP1().X, ((BaseShape)_currentShapes[i]).GetP2().Y, (((BaseShape)_currentShapes[i]).GetP2().X - ((BaseShape)_currentShapes[i]).GetP1().X), (((BaseShape)_currentShapes[i]).GetP1().Y - ((BaseShape)_currentShapes[i]).GetP2().Y));
                        g.DrawRectangle(new Pen(((BaseShape)_currentShapes[i]).PenColor, ((BaseShape)_currentShapes[i]).Penwidth), ((BaseShape)_currentShapes[i]).GetP2().X, ((BaseShape)_currentShapes[i]).GetP2().Y, (((BaseShape)_currentShapes[i]).GetP1().X - ((BaseShape)_currentShapes[i]).GetP2().X), (((BaseShape)_currentShapes[i]).GetP1().Y - ((BaseShape)_currentShapes[i]).GetP2().Y));
                        g.DrawRectangle(new Pen(((BaseShape)_currentShapes[i]).PenColor, ((BaseShape)_currentShapes[i]).Penwidth), ((BaseShape)_currentShapes[i]).GetP2().X, ((BaseShape)_currentShapes[i]).GetP1().Y, (((BaseShape)_currentShapes[i]).GetP1().X - ((BaseShape)_currentShapes[i]).GetP2().X), (((BaseShape)_currentShapes[i]).GetP2().Y - ((BaseShape)_currentShapes[i]).GetP1().Y));
                        break;
                    case "CAD.EllipseShape":
                        g.DrawEllipse(new Pen(((BaseShape)_currentShapes[i]).PenColor, ((BaseShape)_currentShapes[i]).Penwidth), ((BaseShape)_currentShapes[i]).GetP1().X, ((BaseShape)_currentShapes[i]).GetP1().Y, (((BaseShape)_currentShapes[i]).GetP2().X - ((BaseShape)_currentShapes[i]).GetP1().X), (((BaseShape)_currentShapes[i]).GetP2().Y - ((BaseShape)_currentShapes[i]).GetP1().Y));
                        break;
                    case "CAD.CircleShape":
                        int r = (int)Math.Pow(Math.Pow(((BaseShape)_currentShapes[i]).GetP2().X - ((BaseShape)_currentShapes[i]).GetP1().X, 2) + Math.Pow(((BaseShape)_currentShapes[i]).GetP2().Y - ((BaseShape)_currentShapes[i]).GetP1().Y, 2), 0.5);
                        g.DrawEllipse(new Pen(((BaseShape)_currentShapes[i]).PenColor, ((BaseShape)_currentShapes[i]).Penwidth), ((BaseShape)_currentShapes[i]).GetP1().X-r, ((BaseShape)_currentShapes[i]).GetP1().Y-r, 2*r,2*r);
                        break;
                }
                ((BaseShape)_currentShapes[i]).SuperDraw(g);
            }
        }

        bool _isMouseDown;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            _isMouseDown = true;
            if (GetCurrentTool() != null) GetCurrentTool().SuperMouseDown(sender, e,this);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isMouseDown)
            {
                if (GetCurrentTool() != null) GetCurrentTool().SuperMouseDrag(sender, e);
            }
            else
            {
                if (GetCurrentTool() != null) GetCurrentTool().SuperMouseMove(sender, e);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            _isMouseDown = false;
            if (GetCurrentTool() != null) GetCurrentTool().SuperMouseUp(sender, e);
        }

        private void txtLineWidth_TextChanged(object sender, EventArgs e)
        {

            Bitmap bit = new Bitmap(picLineWidth.Width, picLineWidth.Height);
            Graphics g = Graphics.FromImage(bit);
            Pen pen = new Pen(Color.Black, int.Parse(txtLineWidth.Text));
            Point p1 = new Point();
            Point p2 = new Point();
            p1.X = 0;
            p1.Y = picLineWidth.Height / 2;
            p2.X = picLineWidth.Width;
            p2.Y = picLineWidth.Height / 2;
            g.DrawLine(pen, p1, p2);
            picLineWidth.Image = bit;
            tbarLineWidth.Value = int.Parse(txtLineWidth.Text);
            LineWidth = int.Parse(txtLineWidth.Text);
        }

        private void tbarLineWidth_Scroll(object sender, EventArgs e)
        {
            txtLineWidth.Text = tbarLineWidth.Value.ToString();
        }

        private void CADFrame_Load(object sender, EventArgs e)
        {
            Bitmap bit = new Bitmap(picLineWidth.Width, picLineWidth.Height);
            Graphics g = Graphics.FromImage(bit);
            Pen pen = new Pen(Color.Black, 1);
            Point p1 = new Point();
            Point p2 = new Point();
            p1.X = 0;
            p1.Y = picLineWidth.Height / 2;
            p2.X = picLineWidth.Width;
            p2.Y = picLineWidth.Height / 2;
            g.DrawLine(pen, p1, p2);
            picLineWidth.Image = bit;
            picCurrentColor.BackColor = Clr;
        }

        private void RectColor()
        {
            Bitmap bit = new Bitmap(30, 27);
            Graphics g = Graphics.FromImage(bit);
            Pen pen = new Pen(Color.Gray, 1);
            Point p1 = new Point();
            Point p2 = new Point();
            p1.X = 0;
            p1.Y = 15;
            p2.X = 15;
            p2.Y = 14;
            g.DrawLine(pen, p1, p2);
            picLineWidth.Image = bit;
        }

        private void Black_Click(object sender, EventArgs e)
        {
            Clr = Color.Black;
            picCurrentColor.BackColor = Clr;
        }

        private void White_Click(object sender, EventArgs e)
        {
            Clr = Color.White;
            picCurrentColor.BackColor = Clr;
        }

        private void Red_Click(object sender, EventArgs e)
        {
            Clr = Color.Red;
            picCurrentColor.BackColor = Clr;
        }

        private void Green_Click(object sender, EventArgs e)
        {
            Clr = Color.Green;
            picCurrentColor.BackColor = Clr;
        }

        private void Blue_Click(object sender, EventArgs e)
        {
            Clr = Color.Blue;
            picCurrentColor.BackColor = Clr;
        }

        private void Cyan_Click(object sender, EventArgs e)
        {
            Clr = Color.Cyan;
            picCurrentColor.BackColor = Clr;
        }

        private void Magente_Click(object sender, EventArgs e)
        {
            Clr = Color.Magenta;
            picCurrentColor.BackColor = Clr;
        }

        private void Yellow_Click(object sender, EventArgs e)
        {
            Clr = Color.Yellow;
            picCurrentColor.BackColor = Clr;
        }

        private void btnMoreColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Clr = colorDialog1.Color;
                picCurrentColor.BackColor = Clr;
            }
        }

        
    }
}