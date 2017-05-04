using System;
using System.Drawing;

namespace CAD
{
    [Serializable]
    class CircleShape : BaseShape
    {
        public static bool IsInCircle(Point p1, Point p2, Point p3)//�ж����λ��p3�Ƿ����߶�p1��p2�ϣ�0.1��Χ�ڣ�������Ƿ����棬���򷵻ؼ�
        {
            int r = (int)Math.Pow(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2), 0.5);
            Point p4 = new Point();//������������ϵ�
            p4.X = p1.X - r;
            if (Math.Abs(Math.Pow(Math.Pow(p1.X-p3.X,2)+Math.Pow(p1.Y-p3.Y,2),0.5)-r)< 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool CatchShape(Point testPoint)//��дͼ�εĲ�׽���������testPoint��ͼ����Χ�������棬���򷵻ؼ�
        {
            return IsInCircle(GetP1(), GetP2(), testPoint);
        }

        public override void Draw(Graphics g)//��д��ͼ
        {
            g.DrawLine(new Pen(Color.Black, 1), GetP1(), GetP2());
        }

        public override Point[] GetAllHitPoint()//���������ȵ�
        {
            Point[] allHitPoint = new Point[2];
            allHitPoint[0] = GetP1();
            allHitPoint[1] = GetP2();
            return allHitPoint;
        }

        public override void SetHitPoint(int hitPointIndex, Point newPoint)//��д�����ȵ�ķ���
        {
            switch (hitPointIndex)
            {
                case 0:
                    {
                        var tempPoint = new Point();
                        tempPoint.X = GetP1().X + newPoint.X;//����X���������
                        tempPoint.Y = GetP1().Y + newPoint.Y;//����Y���������
                        SetP1(tempPoint);
                        tempPoint = new Point();
                        tempPoint.X = GetP2().X + newPoint.X;
                        tempPoint.Y = GetP2().Y + newPoint.Y;
                        SetP2(tempPoint);
                        break;
                    }
                case 1:
                    {
                        SetP1(newPoint);//����P1���ȵ�
                        break;
                    }
                case 2:
                    {
                        SetP2(newPoint);//����P2���ȵ�
                        break;
                    }
            }
        }

        public override BaseShape CopySelf()//��д���Ʒ���
        {
            CircleShape copyCircleShape = new CircleShape();
            copyCircleShape.SetP1(GetP1());//�������
            copyCircleShape.SetP2(GetP2());//�����յ�
            copyCircleShape.PenColor = PenColor;
            copyCircleShape.Penwidth = Penwidth;
            return copyCircleShape;
        }
    }
}
