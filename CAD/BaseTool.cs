using System.Drawing;
using System.Windows.Forms;

namespace CAD
{
    public abstract class BaseTool
    {
        private CadFrame _refCadPanel;//��������

        private Point _upPoint;//��굯���
        private Point _downPoint;//��갴�µ�
        private Point _newMovePoint;//�µ�����ƶ���
        private Point _oldMovePoint;//�ϵ�����ƶ���
        private Point _newDragPoint;//�µ�����϶���
        private Point _oldDragPoint;//�ϵ�����϶���

        private BaseShape _operShape;//����ͼ��

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

        public abstract void MouseUp(object sender, MouseEventArgs e);//��굯��Ĵ���
        public abstract void MouseDown(object sender, MouseEventArgs e,CadFrame objC);//��갴�µĴ���
        public abstract void MouseMove(object sender, MouseEventArgs e);//����ƶ��Ĵ���
        public abstract void MouseDrag(object sender, MouseEventArgs e);//����϶��Ĵ���

        public void SuperMouseUp(object sender, MouseEventArgs e)//����ͷ�
        {
            SetUpPoint(new Point(e.X, e.Y));//���ĵ������趨
            MouseUp(sender, e);//���ĵ�����趨
            SetUpPoint(new Point());//��굯�����趨
            SetDownPoint(new Point());//��갴�µ���趨
            SetOldMovePoint(new Point());//�ϵ�����ƶ�����趨
            SetNewMovePoint(new Point());//�µ�����ƶ�����趨
            SetOldDragPoint(new Point());//�ϵ�����϶�����趨
            SetNewDragPoint(new Point());//�µ�����϶�����趨
            GetRefCadPanel().Record();//����
        }

        public void SuperMouseDown(object sender, MouseEventArgs e,CadFrame objCad)//��갴��
        {
            SetUpPoint(new Point(e.X, e.Y));//���ĵ������趨
            SetDownPoint(new Point(e.X, e.Y));//��갴�µ���趨
            SetOldMovePoint(new Point(e.X, e.Y));//�ϵ�����ƶ�����趨
            SetNewMovePoint(new Point(e.X, e.Y));//�µ�����ƶ�����趨
            SetOldDragPoint(new Point(e.X, e.Y));//�ϵ�����϶�����趨
            SetNewDragPoint(new Point(e.X, e.Y));//�µ�����϶�����趨
            MouseDown(sender, e,objCad);//��갴�µĴ���
        }

        public void SuperMouseMove(object sender, MouseEventArgs e)//����ƶ�
        {
            SetNewMovePoint(new Point(e.X, e.Y));//�µ�����ƶ�����趨
            MouseMove(sender, e);//����ƶ�
            SetOldMovePoint(GetNewMovePoint());//�ϵ�����ƶ�����趨
        }

        public void SuperMouseDrag(object sender, MouseEventArgs e)//����϶�
        {
            SetNewDragPoint(new Point(e.X, e.Y));//�µ�����϶�����趨
            MouseDrag(sender, e);//����϶�
            SetOldDragPoint(GetNewDragPoint());//�ϵ�����϶�����趨
        }

        public abstract void Set();//װ��
        public abstract void UnSet();//ж��
    }
}
