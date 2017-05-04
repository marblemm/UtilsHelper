using System.Windows.Forms;

namespace CAD
{
    public class EllipseTool : BaseTool
    {
        public override void MouseDown(object sender, MouseEventArgs e, CadFrame objC)//重写线的鼠标按下
        {
            SetOperShape(new EllipseShape());
            GetOperShape().SetP1(GetDownPoint());
            GetOperShape().PenColor = objC.Clr;
            GetOperShape().Penwidth = objC.LineWidth;
            GetRefCadPanel().GetCurrentShapes().Add(GetOperShape());//
        }

        public override void MouseDrag(object sender, MouseEventArgs e)//重写线的鼠标拖动
        {
            GetOperShape().SetP2(GetNewDragPoint());
            GetRefCadPanel().Refresh();
        }

        public override void MouseMove(object sender, MouseEventArgs e)
        {
        }

        public override void MouseUp(object sender, MouseEventArgs e)
        {
        }

        public override void UnSet()
        {
        }

        public override void Set()
        {
        }
    }
}
