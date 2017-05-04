using System;
using System.Drawing;

namespace CAD
{
    [Serializable]
    class RectangleShape : BaseShape
    {
        public static bool IsInRectangle(Point p1, Point p2, Point p3)//判断鼠标位置p3是否在线段p1、p2上（0.1范围内），如果是返回真，否则返回假
        {
            double iLen1 = Math.Abs(p1.Y - p2.Y);//矩形的高
            double iLen2 = Math.Abs(p1.X - p2.X);//矩形的宽
            double iLen3,iLen4,iLen5,iLen6;

            if (p2.X > p1.X && p2.Y > p1.Y)
            {
                iLen3 = Math.Pow(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p3.Y - p1.Y, 2), 0.5) + Math.Pow(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p3.Y - p2.Y, 2), 0.5);
                //p3距离矩形左边的距离
                iLen4 = Math.Pow(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p3.Y - p1.Y, 2), 0.5) + Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p1.Y, 2), 0.5);
                //p3距离矩形上边的距离
                iLen5 = Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p1.Y, 2), 0.5) + Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p2.Y, 2), 0.5);
                //p3距离矩形右边的距离
                iLen6 = Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p2.Y, 2), 0.5) + Math.Pow(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p3.Y - p2.Y, 2), 0.5);
                //p3距离矩形下边的距离
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
                //p3距离矩形左边的距离
                iLen4 = Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p2.Y, 2), 0.5) + Math.Pow(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p3.Y - p2.Y, 2), 0.5);
                //p3距离矩形上边的距离
                iLen5 = Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p1.Y, 2), 0.5) + Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p2.Y, 2), 0.5);
                //p3距离矩形右边的距离
                iLen6 = Math.Pow(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p3.Y - p1.Y, 2), 0.5) + Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p1.Y, 2), 0.5);
                //p3距离矩形下边的距离
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
                //p3距离矩形左边的距离
                iLen4 = Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p2.Y, 2), 0.5) + Math.Pow(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p3.Y - p2.Y, 2), 0.5);
                //p3距离矩形上边的距离
                iLen3 = Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p1.Y, 2), 0.5) + Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p2.Y, 2), 0.5);
                //p3距离矩形右边的距离
                iLen6 = Math.Pow(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p3.Y - p1.Y, 2), 0.5) + Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p1.Y, 2), 0.5);
                //p3距离矩形下边的距离
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
                //p3距离矩形左边的距离
                iLen6 = Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p2.Y, 2), 0.5) + Math.Pow(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p3.Y - p2.Y, 2), 0.5);
                //p3距离矩形上边的距离
                iLen3 = Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p1.Y, 2), 0.5) + Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p2.Y, 2), 0.5);
                //p3距离矩形右边的距离
                iLen4 = Math.Pow(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p3.Y - p1.Y, 2), 0.5) + Math.Pow(Math.Pow(p2.X - p3.X, 2) + Math.Pow(p3.Y - p1.Y, 2), 0.5);
                //p3距离矩形下边的距离
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

        public override bool CatchShape(Point testPoint)//重写图形的捕捉。如果鼠标点testPoint在图形周围，返回真，否则返回假
        {
            return IsInRectangle(GetP1(), GetP2(), testPoint);
        }

        public override void Draw(Graphics g)//重写画图
        {
            g.DrawRectangle(new Pen(PenColor, Penwidth), GetP1().X, GetP1().Y, GetP2().X - GetP1().X, GetP2().Y - GetP1().Y);
            g.DrawRectangle(new Pen(PenColor, Penwidth), GetP1().X, GetP2().Y, GetP2().X - GetP1().X, GetP1().Y - GetP2().Y);
            g.DrawRectangle(new Pen(PenColor, Penwidth), GetP2().X, GetP2().Y, GetP1().X - GetP2().X, GetP1().Y - GetP2().Y);
            g.DrawRectangle(new Pen(PenColor, Penwidth), GetP2().X, GetP1().Y, GetP1().X - GetP2().X, GetP2().Y - GetP1().Y);
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
                        var tempPoint = new Point
                        {
                            X = GetP1().X + newPoint.X,
                            Y = GetP1().Y + newPoint.Y
                        };
                        //加上X坐标的增量
                        //加上Y坐标的增量
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
            RectangleShape copyRectangleShape=new RectangleShape();
            copyRectangleShape.SetP1(GetP1());//复制起点
            copyRectangleShape.SetP2(GetP2());//复制终点
            copyRectangleShape.PenColor = PenColor;
            copyRectangleShape.Penwidth = Penwidth;
            return copyRectangleShape;
        }
    }
}
