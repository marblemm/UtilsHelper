using System;
using System.Drawing;

namespace CAD
{
    [Serializable]
    class LineShape : BaseShape
    {
        public static bool IsInLine(Point p1, Point p2, Point p3)//�ж����λ��p3�Ƿ����߶�p1��p2�ϣ�0.1��Χ�ڣ�������Ƿ����棬���򷵻ؼ�
        {
            double iLen1 = Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2);
            double iLen2 = Math.Pow(p1.X - p3.X, 2) + Math.Pow(p1.Y - p3.Y, 2);
            double iLen3 = Math.Pow(p2.X - p3.X, 2) + Math.Pow(p2.Y - p3.Y, 2);

            if (Math.Pow(iLen2, 0.5) + Math.Pow(iLen3, 0.5) - Math.Pow(iLen1, 0.5) < .1)
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
            return IsInLine(GetP1(), GetP2(), testPoint);
        }

        public override void Draw(Graphics g)//��д��ͼ
        {
            g.DrawLine(new Pen(Color.Black,1), GetP1(), GetP2());
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
            LineShape copyLineShape=new LineShape();
            copyLineShape.SetP1(GetP1());//�������
            copyLineShape.SetP2(GetP2());//�����յ�
            copyLineShape.PenColor = PenColor;
            copyLineShape.Penwidth = Penwidth;
            return copyLineShape;
        }
    }
}
