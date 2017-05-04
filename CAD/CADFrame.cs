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
        private BaseTool _currentTool;//当前的应用工具
        private ArrayList _currentShapes;//当前显示的图形集合
        private Hashtable _registerToolMap;//当前注册的工具集合
        private ArrayList _historyShapes;//历史图形的快照集合

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
            _currentShapes = new ArrayList();//实例当前显示的图形集合对象
            _registerToolMap = new Hashtable();//注册工具的集合对象
            _historyShapes = new ArrayList();//历史图形的快照集合对象
            RegisterTool(LinetoolRegistername, new LineTool());//注册线工具
            RegisterTool(HandtoolRegistername, new HandTool());//注册抓取工具
            RegisterTool(Rectanglet0OlRegistername, new RectangleTool());//注册矩形工具
            RegisterTool(EllipsetoolRegistername, new EllipseTool());//注册椭圆工具
            RegisterTool(CircletoolRegistername, new CircleTool());//注册圆形工具
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

        public void RegisterTool(string registerName, BaseTool registerTool)//注册工具
        {
            GetRegisterToolMap().Add(registerName, registerTool);
            registerTool.SetRefCadPanel(this);
        }

        public void UseTool(string registerName)//应用工具
        {
            if (GetCurrentTool() != null) GetCurrentTool().UnSet();//卸载之前的工具
            BaseTool setTool = (BaseTool)GetRegisterToolMap()[registerName];//加载现在要用的工具
            if (setTool != null)
            {
                setTool.Set();
                SetCurrentTool(setTool);//装载工具
            }
        }

        int _undoIndex;//回退的索引
        public void Record()//快照保存的方法 
        {
            if (_undoIndex > 0)//当有回退时，清空回退获得快照
            {
                while (_undoIndex != 0)
                {
                    GetHistoryShape().RemoveAt(GetHistoryShape().Count - 1);
                    _undoIndex--;
                }
            }
            GetHistoryShape().Add(CloneShapArray(GetCurrentShapes()));//保存快照
        }
        public void Redo()//重做
        {
            if (_undoIndex > 0)//当有回退时才可重做
            {
                _undoIndex--;//将历史快照取回到当前图形中
                SetCurrentShapes(CloneShapArray((ArrayList)GetHistoryShape()[GetHistoryShape().Count - 1 - _undoIndex]));
            }
            Refresh();
        }

        public void Undo()//回退
        {
            if ((GetHistoryShape().Count - 1 - _undoIndex) > 0)//历史快照中还有历史才能回退
            {
                _undoIndex++;//将历史快照取回到当前图形中
                SetCurrentShapes((CloneShapArray((ArrayList)GetHistoryShape()[GetHistoryShape().Count - 1 - _undoIndex])));
            }
            Refresh();
        }

        public ArrayList CloneShapArray(ArrayList shapeArrayList)//图形集合身复制
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