using System;
using System.Drawing;

namespace CAD
{
    [Serializable]
    class EllipseShape : BaseShape
    {
        public static bool IsInEllipse(Point p1, Point p2, Point p3)//�ж����λ��p3�Ƿ�����Բ�ϣ�0.1��Χ�ڣ�������Ƿ����棬���򷵻ؼ�
        {
            Point pa1 = new Point();//�󽹵�
            Point pa2 = new Point();//�ҽ���
            double iLen2;
            if (Math.Abs(p1.X - p2.X) > Math.Abs(p1.Y - p2.Y))//�����ڸ�
            {
                double iLen1 = Math.Pow(Math.Abs(Math.Pow(p2.X - p1.X, 2) - Math.Pow(p2.Y - p1.Y, 2)), 0.5) / 2;//����ĳ���
                iLen2 = Math.Abs(p2.X - p1.X);//������������Ĺ̶������
                if (p2.X > p1.X&&p2.Y>p1.Y)//���ϵ�����
                { 
                    pa1.X = (int)(p1.X + iLen2 / 2 - iLen1);
                    pa1.Y = p1.Y + Math.Abs(p2.Y - p1.Y) / 2;
                    pa2.X = (int)(p1.X + iLen2 / 2 + iLen1);
                    pa2.Y = p1.Y + Math.Abs(p2.Y - p1.Y) / 2;
                }
                if(p2.X<p1.X&&p2.Y>p1.Y)//���ϵ�����
                {
                    pa1.X = (int)(p2.X + iLen2 / 2 - iLen1);
                    pa1.Y = p1.Y + Math.Abs(p2.Y - p1.Y) / 2;
                    pa2.X = (int)(p2.X + iLen2 / 2 + iLen1);
                    pa2.Y = p1.Y + Math.Abs(p2.Y - p1.Y) / 2;
                }
                if (p2.X < p1.X && p2.Y < p1.Y)//���µ�����
                {
                    pa1.X = (int)(p2.X + iLen2 / 2 - iLen1);
                    pa1.Y = p2.Y + Math.Abs(p1.Y - p2.Y) / 2;
                    pa2.X = (int)(p2.X + iLen2 / 2 + iLen1);
                    pa2.Y = p2.Y + Math.Abs(p1.Y - p2.Y) / 2;
                }
                if (p1.X < p2.X && p1.Y > p2.Y)//���µ�����
                {
                    pa1.X = (int)(p1.X + iLen2 / 2 - iLen1);
                    pa1.Y = p2.Y + Math.Abs(p1.Y - p2.Y) / 2;
                    pa2.X = (int)(p1.X + iLen2 / 2 + iLen1);
                    pa2.Y = p2.Y + Math.Abs(p1.Y - p2.Y) / 2;
                }
            }
            else
            {
                double iLen1 = Math.Pow(Math.Abs(Math.Pow(p2.X - p1.X, 2) - Math.Pow(p2.Y - p1.Y, 2)), 0.5) / 2;//����ĳ���
                iLen2 = Math.Abs(p2.Y - p1.Y);//������������Ĺ̶������
                if (p2.X > p1.X && p2.Y > p1.Y)//���ϵ�����
                {
                    pa1.X = p1.X + Math.Abs(p2.X - p1.X) / 2;
                    pa1.Y = (int)(p1.Y + Math.Abs(p2.Y - p1.Y) / 2 - iLen1);
                    pa2.X = p1.X + Math.Abs(p2.X - p1.X) / 2;
                    pa2.Y = (int)(p1.Y + Math.Abs(p2.Y - p1.Y) / 2 + iLen1);
                }
                if (p2.X < p1.X && p2.Y < p1.Y)//���µ�����
                {
                    pa1.X = p2.X + Math.Abs(p2.X - p1.X) / 2;
                    pa1.Y = (int)(p2.Y + Math.Abs(p2.Y - p1.Y) / 2 - iLen1);
                    pa2.X = p2.X + Math.Abs(p2.X - p1.X) / 2;
                    pa2.Y = (int)(p2.Y + Math.Abs(p2.Y - p1.Y) / 2 + iLen1);
                }
                if (p2.X > p1.X && p2.Y < p1.Y)//���µ�����
                {
                    pa1.X = p1.X + Math.Abs(p2.X - p1.X) / 2;
                    pa1.Y = (int)(p2.Y + Math.Abs(p2.Y - p1.Y) / 2 - iLen1);
                    pa2.X = p1.X + Math.Abs(p2.X - p1.X) / 2;
                    pa2.Y = (int)(p2.Y + Math.Abs(p2.Y - p1.Y) / 2 + iLen1);
                }
                if (p2.X < p1.X && p2.Y > p1.Y)//���ϵ�����
                {
                    pa1.X = p2.X + Math.Abs(p2.X - p1.X) / 2;
                    pa1.Y = (int)(p1.Y + Math.Abs(p2.Y - p1.Y) / 2 - iLen1);
                    pa2.X = p2.X + Math.Abs(p2.X - p1.X) / 2;
                    pa2.Y = (int)(p1.Y + Math.Abs(p2.Y - p1.Y) / 2 + iLen1);
                }
            }
            double iLen3 = Math.Pow(Math.Pow(p3.X - pa1.X, 2) + Math.Pow(p3.Y - pa1.Y, 2), 0.5) + Math.Pow(Math.Pow(p3.X - pa2.X, 2) + Math.Pow(p3.Y - pa2.Y, 2), 0.5);
            //p3�������������ľ����
            if (Math.Abs(iLen3-iLen2) < 5)
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
            return IsInEllipse(GetP1(), GetP2(), testPoint);
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
            EllipseShape copyEllipseShape = new EllipseShape();
            copyEllipseShape.SetP1(GetP1());//�������
            copyEllipseShape.SetP2(GetP2());//�����յ�
            copyEllipseShape.PenColor = PenColor;
            copyEllipseShape.Penwidth = Penwidth;
            return copyEllipseShape;
        }
    }
}
