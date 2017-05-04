using System;
using System.Drawing;

namespace CAD
{
    [Serializable]
    class RectangleShape : BaseShape
    {
        public static bool IsInRectangle(Point p1, Point p2, Point p3)//�ж����λ��p3�Ƿ����߶�p1��p2�ϣ�0.1��Χ�ڣ�������Ƿ����棬���򷵻ؼ�
        {
            double iLen1 = Math.Abs(p1.Y - p2.Y);//���εĸ�
            double iLen2 = Math.Abs(p1.X - p2.X);//���εĿ�
            double iLen3,iLen4,iLen5,iLen6;

            if (p2.X > p1.X && p2.Y > p1.Y)
            {
                iLen3 = Math.Pow(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p3.Y - p1.Y, 2), 0.5) + Math.Pow(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p3.Y - p2.Y, 2), 0.5);
                //p3���������ߵľ���
                iLen4 = Math.Pow(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p3.Y - p1.Y, 2), 0.5) + Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p1.Y, 2), 0.5);
                //p3��������ϱߵľ���
                iLen5 = Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p1.Y, 2), 0.5) + Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p2.Y, 2), 0.5);
                //p3��������ұߵľ���
                iLen6 = Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p2.Y, 2), 0.5) + Math.Pow(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p3.Y - p2.Y, 2), 0.5);
                //p3��������±ߵľ���
                if ((iLen3 - iLen1) < .1 || (iLen4 - iLen2) < .1 || (iLen5 - iLen1) < .1 || (iLen6 - iLen2) < .1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (p2.X > p1.X && p2.Y < p1.Y)
            {
                iLen3 = Math.Pow(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p3.Y - p1.Y, 2), 0.5) + Math.Pow(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p3.Y - p2.Y, 2), 0.5);
                //p3���������ߵľ���
                iLen4 = Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p2.Y, 2), 0.5) + Math.Pow(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p3.Y - p2.Y, 2), 0.5);
                //p3��������ϱߵľ���
                iLen5 = Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p1.Y, 2), 0.5) + Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p2.Y, 2), 0.5);
                //p3��������ұߵľ���
                iLen6 = Math.Pow(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p3.Y - p1.Y, 2), 0.5) + Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p1.Y, 2), 0.5);
                //p3��������±ߵľ���
                if ((iLen3 - iLen1) < .1 || (iLen4 - iLen2) < .1 || (iLen5 - iLen1) < .1 || (iLen6 - iLen2) < .1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (p2.X < p1.X && p2.Y < p1.Y)
            {
                iLen5 = Math.Pow(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p3.Y - p1.Y, 2), 0.5) + Math.Pow(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p3.Y - p2.Y, 2), 0.5);
                //p3���������ߵľ���
                iLen4 = Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p2.Y, 2), 0.5) + Math.Pow(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p3.Y - p2.Y, 2), 0.5);
                //p3��������ϱߵľ���
                iLen3 = Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p1.Y, 2), 0.5) + Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p2.Y, 2), 0.5);
                //p3��������ұߵľ���
                iLen6 = Math.Pow(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p3.Y - p1.Y, 2), 0.5) + Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p1.Y, 2), 0.5);
                //p3��������±ߵľ���
                if ((iLen3 - iLen1) < .1 || (iLen4 - iLen2) < .1 || (iLen5 - iLen1) < .1 || (iLen6 - iLen2) < .1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                iLen5 = Math.Pow(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p3.Y - p1.Y, 2), 0.5) + Math.Pow(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p3.Y - p2.Y, 2), 0.5);
                //p3���������ߵľ���
                iLen6 = Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p2.Y, 2), 0.5) + Math.Pow(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p3.Y - p2.Y, 2), 0.5);
                //p3��������ϱߵľ���
                iLen3 = Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p1.Y, 2), 0.5) + Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p2.Y, 2), 0.5);
                //p3��������ұߵľ���
                iLen4 = Math.Pow(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p3.Y - p1.Y, 2), 0.5) + Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p1.Y, 2), 0.5);
                //p3��������±ߵľ���
                if ((iLen3 - iLen1) < .1 || (iLen4 - iLen2) < .1 || (iLen5 - iLen1) < .1 || (iLen6 - iLen2) < .1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public override bool CatchShape(Point testPoint)//��дͼ�εĲ�׽���������testPoint��ͼ����Χ�������棬���򷵻ؼ�
        {
            return IsInRectangle(GetP1(), GetP2(), testPoint);
        }

        public override void Draw(Graphics g)//��д��ͼ
        {
            g.DrawRectangle(new Pen(PenColor, Penwidth), GetP1().X, GetP1().Y, GetP2().X - GetP1().X, GetP2().Y - GetP1().Y);
            g.DrawRectangle(new Pen(PenColor, Penwidth), GetP1().X, GetP2().Y, GetP2().X - GetP1().X, GetP1().Y - GetP2().Y);
            g.DrawRectangle(new Pen(PenColor, Penwidth), GetP2().X, GetP2().Y, GetP1().X - GetP2().X, GetP1().Y - GetP2().Y);
            g.DrawRectangle(new Pen(PenColor, Penwidth), GetP2().X, GetP1().Y, GetP1().X - GetP2().X, GetP2().Y - GetP1().Y);
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
                        var tempPoint = new Point
                        {
                            X = GetP1().X + newPoint.X,
                            Y = GetP1().Y + newPoint.Y
                        };
                        //����X���������
                        //����Y���������
                        SetP1(tempPoint);
                        tempPoint = new Point
                        {
                            X = GetP2().X + newPoint.X,
                            Y = GetP2().Y + newPoint.Y
                        };
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
            RectangleShape copyRectangleShape=new RectangleShape();
            copyRectangleShape.SetP1(GetP1());//�������
            copyRectangleShape.SetP2(GetP2());//�����յ�
            copyRectangleShape.PenColor = PenColor;
            copyRectangleShape.Penwidth = Penwidth;
            return copyRectangleShape;
        }
    }
}
