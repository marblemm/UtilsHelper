using System.Drawing;
using System.Windows.Forms;

namespace CAD
{
    public abstract class BaseTool
    {
        private CadFrame _refCadPanel;//关联画板

        private Point _upPoint;//鼠标弹起点
        private Point _downPoint;//鼠标按下点
        private Point _newMovePoint;//新的鼠标移动点
        private Point _oldMovePoint;//老的鼠标移动点
        private Point _newDragPoint;//新的鼠标拖动点
        private Point _oldDragPoint;//老的鼠标拖动点

        private BaseShape _operShape;//操作图形

        public Point GetDownPoint()
        {
            return _downPoint;
        }
        public void SetDownPoint(Point downPoint)
        {
            _downPoint = downPoint;
        }
        public Point GetNewDragPoint()
        {
            return _newDragPoint;
        }
        public void SetNewDragPoint(Point newDragPoint)
        {
            _newDragPoint = newDragPoint;
        }
        public Point GetNewMovePoint()
        {
            return _newMovePoint;
        }
        public void SetNewMovePoint(Point newMovePoint)
        {
            _newMovePoint = newMovePoint;
        }
        public Point GetOldDragPoint()
        {
            return _oldDragPoint;
        }
        public void SetOldDragPoint(Point oldDragPoint)
        {
            _oldDragPoint = oldDragPoint;
        }
        public Point GetOldMovePoint()
        {
            return _oldMovePoint;
        }
        public void SetOldMovePoint(Point oldMovePoint)
        {
            _oldMovePoint = oldMovePoint;
        }
        public Point GetUpPoint()
        {
            return _upPoint;
        }
        public void SetUpPoint(Point upPoint)
        {
            _upPoint = upPoint;
        }
        public CadFrame GetRefCadPanel()
        {
            return _refCadPanel;
        }
        public void SetRefCadPanel(CadFrame refCadPanel)
        {
            _refCadPanel = refCadPanel;
        }
        public BaseShape GetOperShape()
        {
            return _operShape;
        }
        public void SetOperShape(BaseShape operShape)
        {
            _operShape = operShape;
        }

        public abstract void MouseUp(object sender, MouseEventArgs e);//鼠标弹起的处理
        public abstract void MouseDown(object sender, MouseEventArgs e,CadFrame objC);//鼠标按下的处理
        public abstract void MouseMove(object sender, MouseEventArgs e);//鼠标移动的处理
        public abstract void MouseDrag(object sender, MouseEventArgs e);//鼠标拖动的处理

        public void SuperMouseUp(object sender, MouseEventArgs e)//鼠标释放
        {
            SetUpPoint(new Point(e.X, e.Y));//鼠标的弹起点的设定
            MouseUp(sender, e);//鼠标的弹起的设定
            SetUpPoint(new Point());//鼠标弹起点的设定
            SetDownPoint(new Point());//鼠标按下点的设定
            SetOldMovePoint(new Point());//老的鼠标移动点的设定
            SetNewMovePoint(new Point());//新的鼠标移动点的设定
            SetOldDragPoint(new Point());//老的鼠标拖动点的设定
            SetNewDragPoint(new Point());//新的鼠标拖动点的设定
            GetRefCadPanel().Record();//保存
        }

        public void SuperMouseDown(object sender, MouseEventArgs e,CadFrame objCad)//鼠标按下
        {
            SetUpPoint(new Point(e.X, e.Y));//鼠标的弹起点的设定
            SetDownPoint(new Point(e.X, e.Y));//鼠标按下点的设定
            SetOldMovePoint(new Point(e.X, e.Y));//老的鼠标移动点的设定
            SetNewMovePoint(new Point(e.X, e.Y));//新的鼠标移动点的设定
            SetOldDragPoint(new Point(e.X, e.Y));//老的鼠标拖动点的设定
            SetNewDragPoint(new Point(e.X, e.Y));//新的鼠标拖动点的设定
            MouseDown(sender, e,objCad);//鼠标按下的处理
        }

        public void SuperMouseMove(object sender, MouseEventArgs e)//鼠标移动
        {
            SetNewMovePoint(new Point(e.X, e.Y));//新的鼠标移动点的设定
            MouseMove(sender, e);//鼠标移动
            SetOldMovePoint(GetNewMovePoint());//老的鼠标移动点的设定
        }

        public void SuperMouseDrag(object sender, MouseEventArgs e)//鼠标拖动
        {
            SetNewDragPoint(new Point(e.X, e.Y));//新的鼠标拖动点的设定
            MouseDrag(sender, e);//鼠标拖动
            SetOldDragPoint(GetNewDragPoint());//老的鼠标拖动点的设定
        }

        public abstract void Set();//装载
        public abstract void UnSet();//卸载
    }
}
