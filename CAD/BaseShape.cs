using System;
using System.Drawing;

namespace CAD
{
    [Serializable]
    public abstract class BaseShape
    {
        private bool _isSelected;//标识图形是否被选中
        
        private Point _p1;//第一个点
        private Point _p2;//第二个点

        public  Color PenColor;
        public  int Penwidth ;

        public void SetSelected()//设置为选中状态
        {
            _isSelected = true;
        }
        public void SetUnSelected()//设置为非选中状态
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

        public abstract void Draw(Graphics g);//画图形

        public abstract Point[] GetAllHitPoint();//得到所有图形
        public abstract void SetHitPoint(int hitPointIndex, Point newPoint);//设定热点
        public abstract BaseShape CopySelf();//复制


        public bool CatchHitPoint(Point hitPoint, Point testPoint)//测试热点捕捉
        {
            return GetHitPointRectangle(hitPoint).Contains(testPoint);
        }

        public int CatchShapPoint(Point testPoint)//捕捉图形
        {
            int hitPointIndex = -1;
            Point[] allHitPoint = GetAllHitPoint();//的到所有的热点
            for (int i = 0; i < allHitPoint.Length; i++)//循环捕捉判断
            {
                if (CatchHitPoint(allHitPoint[i], testPoint))
                {
                    return i + 1;//如果捕捉到了热点，返回热点的索引
                }
            }
            if(CatchShape(testPoint)) return 0;//没有捕捉到热点，捕捉到了图形，返回特别热点
            return hitPointIndex;//返回捕捉到的人点
            }
        public void DrawHitPoint(Point hitPoint, Graphics g)//画热点
        {
            g.DrawRectangle(new Pen(Color.Red,1), GetHitPointRectangle(hitPoint));
        }

        public void DrawAllHitPoint(Graphics g)//画所有热点
        {
            Point[] allHitPoint=GetAllHitPoint();
            for(int i=0;i<2;i++)
            {
                DrawHitPoint(allHitPoint[i],g);
            }
        }

        public Rectangle GetHitPointRectangle(Point hitPoint)//得到热点矩形，以热点为中心高宽5像素的矩形
        {
            Rectangle rect=new Rectangle();
            rect.X=hitPoint.X-2;
            rect.Y=hitPoint.Y-2;
            rect.Width=5;
            rect.Height=5;
            return rect;
        }

        public abstract bool CatchShape(Point testPoint);//图形捕捉

        public void SuperDraw(Graphics g)//公共画法
        {
            if(_isSelected) DrawAllHitPoint(g);
        }

        public static Pen GetPen(CadFrame objCad)//得到画笔
        {
            return new Pen(objCad.Clr,objCad.LineWidth);
        }
    }
}