using System;
using System.Drawing;

namespace CAD
{
    [Serializable]
    public abstract class BaseShape
    {
        private bool _isSelected;//��ʶͼ���Ƿ�ѡ��
        
        private Point _p1;//��һ����
        private Point _p2;//�ڶ�����

        public  Color PenColor;
        public  int Penwidth ;

        public void SetSelected()//����Ϊѡ��״̬
        {
            _isSelected = true;
        }
        public void SetUnSelected()//����Ϊ��ѡ��״̬
        {
            _isSelected = false;
        }
        public Point GetP1()
        {
            return _p1;
        }
        public void SetP1(Point p1)
        {
            _p1 = p1;
        }
        public Point GetP2()
        {
            return _p2;
        }
        public void SetP2(Point p2)
        {
            _p2 = p2;
        }

        public abstract void Draw(Graphics g);//��ͼ��

        public abstract Point[] GetAllHitPoint();//�õ�����ͼ��
        public abstract void SetHitPoint(int hitPointIndex, Point newPoint);//�趨�ȵ�
        public abstract BaseShape CopySelf();//����


        public bool CatchHitPoint(Point hitPoint, Point testPoint)//�����ȵ㲶׽
        {
            return GetHitPointRectangle(hitPoint).Contains(testPoint);
        }

        public int CatchShapPoint(Point testPoint)//��׽ͼ��
        {
            int hitPointIndex = -1;
            Point[] allHitPoint = GetAllHitPoint();//�ĵ����е��ȵ�
            for (int i = 0; i < allHitPoint.Length; i++)//ѭ����׽�ж�
            {
                if (CatchHitPoint(allHitPoint[i], testPoint))
                {
                    return i + 1;//�����׽�����ȵ㣬�����ȵ������
                }
            }
            if(CatchShape(testPoint)) return 0;//û�в�׽���ȵ㣬��׽����ͼ�Σ������ر��ȵ�
            return hitPointIndex;//���ز�׽�����˵�
            }
        public void DrawHitPoint(Point hitPoint, Graphics g)//���ȵ�
        {
            g.DrawRectangle(new Pen(Color.Red,1), GetHitPointRectangle(hitPoint));
        }

        public void DrawAllHitPoint(Graphics g)//�������ȵ�
        {
            Point[] allHitPoint=GetAllHitPoint();
            for(int i=0;i<2;i++)
            {
                DrawHitPoint(allHitPoint[i],g);
            }
        }

        public Rectangle GetHitPointRectangle(Point hitPoint)//�õ��ȵ���Σ����ȵ�Ϊ���ĸ߿�5���صľ���
        {
            Rectangle rect=new Rectangle();
            rect.X=hitPoint.X-2;
            rect.Y=hitPoint.Y-2;
            rect.Width=5;
            rect.Height=5;
            return rect;
        }

        public abstract bool CatchShape(Point testPoint);//ͼ�β�׽

        public void SuperDraw(Graphics g)//��������
        {
            if(_isSelected) DrawAllHitPoint(g);
        }

        public static Pen GetPen(CadFrame objCad)//�õ�����
        {
            return new Pen(objCad.Clr,objCad.LineWidth);
        }
    }
}