using System;
using System.Drawing;

namespace CAD
{
    [Serializable]
    class CircleShape : BaseShape
    {
        public static bool IsInCircle(Point p1, Point p2, Point p3)//判断鼠标位置p3是否在线段p1、p2上（0.1范围内），如果是返回真，否则返回假
        {
            int r = (int)Math.Pow(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2), 0.5);
            Point p4 = new Point();//矩形区域的左上点
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

        public override bool CatchShape(Point testPoint)//重写图形的捕捉。如果鼠标点testPoint在图形周围，返回真，否则返回假
        {
            return IsInCircle(GetP1(), GetP2(), testPoint);
        }

        public override void Draw(Graphics g)//重写画图
        {
            g.DrawLine(new Pen(Color.Black, 1), GetP1(), GetP2());
        }

        public override Point[] GetAllHitPoint()//返回所有热点
        {
            Point[] allHitPoint = new Point[2];
            allHitPoint[0] = GetP1();
            allHitPoint[1] = GetP2();
            return allHitPoint;
        }

        public override void SetHitPoint(int hitPointIndex, Point newPoint)//重写设置热点的方法
        {
            switch (hitPointIndex)
            {
                case 0:
                    {
                        var tempPoint = new Point();
                        tempPoint.X = GetP1().X + newPoint.X;//加上X坐标的增量
                        tempPoint.Y = GetP1().Y + newPoint.Y;//加上Y坐标的增量
                        SetP1(tempPoint);
                        tempPoint = new Point();
                        tempPoint.X = GetP2().X + newPoint.X;
                        tempPoint.Y = GetP2().Y + newPoint.Y;
                        SetP2(tempPoint);
                        break;
                    }
                case 1:
                    {
                        SetP1(newPoint);//设置P1的热点
                        break;
                    }
                case 2:
                    {
                        SetP2(newPoint);//设置P2的热点
                        break;
                    }
            }
        }

        public override BaseShape CopySelf()//重写身复制方法
        {
            CircleShape copyCircleShape = new CircleShape();
            copyCircleShape.SetP1(GetP1());//复制起点
            copyCircleShape.SetP2(GetP2());//复制终点
            copyCircleShape.PenColor = PenColor;
            copyCircleShape.Penwidth = Penwidth;
            return copyCircleShape;
        }
    }
}
