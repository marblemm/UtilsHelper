using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace CAD
{
    public class HandTool : BaseTool
    {
        public int CatchPointIndex = -1;//捕捉热点的索引

        public override void MouseDown(object sender, MouseEventArgs e,CadFrame objC)//重写鼠标的按下
        {
            CatchPointIndex = -1;//重置捕捉热点的索引
            if (GetOperShape() != null) GetOperShape().SetUnSelected();//清除前操作对象中选中的状态
            ArrayList allShapes = GetRefCadPanel().GetCurrentShapes();//得到画板上的所有图形
            int catchPoint = -1;
            int i = 0;
            for (; i < allShapes.Count; i++)//对每个图形进行捕捉测试
            {
                catchPoint = ((BaseShape)allShapes[i]).CatchShapPoint(GetNewMovePoint());//捕捉集合中的一个图形
                if (catchPoint > -1) break;//捕获到后，跳出循环
            }
            if (catchPoint > -1)
            {
                CatchPointIndex = catchPoint;//捕获到后，将临时的热点设置到工具属性中
                ((BaseShape)allShapes[i]).SetSelected();//设置捕捉到的图形为选中状态
                SetOperShape(((BaseShape)allShapes[i]));//把选中的图形设定到本类的操作图形的状态中
            }
            GetRefCadPanel().Refresh();//刷新画板
        }
        public override void MouseDrag(object sender, MouseEventArgs e)//重写鼠标的拖动事件
        {
            if (GetOperShape() != null)//当有选中的图形时
            {
                Point setPoint = GetNewDragPoint();
                if (CatchPointIndex == 0)//如果捕捉到移动点时
                {
                    setPoint = new Point();
                    setPoint.X = GetNewDragPoint().X - GetOldDragPoint().X;//计算增量点
                    setPoint.Y = GetNewDragPoint().Y - GetOldDragPoint().Y;//计算增量点
                }
                GetOperShape().SetHitPoint(CatchPointIndex, setPoint);//设置热点
                GetRefCadPanel().Refresh();//刷新画板
            }
        }

        public BaseShape OldMoveShap;//移动处理的图形

        public override void MouseMove(object sender, MouseEventArgs e)//重写鼠标的移动
        {
            if (OldMoveShap != null) OldMoveShap.SetUnSelected();//清除移动图形选中的状态
            ArrayList allShapes = GetRefCadPanel().GetCurrentShapes();//得到画板上的图形集合
            int catchPoint = -1;//临时处理的捕捉热点
            int i = 0;
            for (; i < allShapes.Count; i++)//对每个图形捕捉测试
            {
                catchPoint = ((BaseShape)allShapes[i]).CatchShapPoint(GetNewMovePoint());
                if (catchPoint > -1) break;//捕捉到跳出循环
            }
            if (catchPoint > -1)//捕捉到后
            {
                ((BaseShape)allShapes[i]).SetSelected();//设定捕捉到的图形为选中状态
                OldMoveShap = (BaseShape)allShapes[i];//将选中的图形设定到本类的操作图形的状态中去
            }
            GetRefCadPanel().Refresh();//刷新画板
        }

        public override void MouseUp(object sender, MouseEventArgs e)//重写鼠标的释放
        {
            GetRefCadPanel().Refresh();//刷新画板
        }

        public override void UnSet()//工具的卸载
        {
            ArrayList allShapes = GetRefCadPanel().GetCurrentShapes();//得到画板上的图形集合
            for (int i = 0; i < allShapes.Count; i++)//清除所有图形的选中状态
            {
                ((BaseShape)allShapes[i]).SetUnSelected();
            }
            GetRefCadPanel().Refresh();
        }

        public override void Set()
        {
            
        }
    }
}
