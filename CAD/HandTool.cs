using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace CAD
{
    public class HandTool : BaseTool
    {
        public int CatchPointIndex = -1;//��׽�ȵ������

        public override void MouseDown(object sender, MouseEventArgs e,CadFrame objC)//��д���İ���
        {
            CatchPointIndex = -1;//���ò�׽�ȵ������
            if (GetOperShape() != null) GetOperShape().SetUnSelected();//���ǰ����������ѡ�е�״̬
            ArrayList allShapes = GetRefCadPanel().GetCurrentShapes();//�õ������ϵ�����ͼ��
            int catchPoint = -1;
            int i = 0;
            for (; i < allShapes.Count; i++)//��ÿ��ͼ�ν��в�׽����
            {
                catchPoint = ((BaseShape)allShapes[i]).CatchShapPoint(GetNewMovePoint());//��׽�����е�һ��ͼ��
                if (catchPoint > -1) break;//���񵽺�����ѭ��
            }
            if (catchPoint > -1)
            {
                CatchPointIndex = catchPoint;//���񵽺󣬽���ʱ���ȵ����õ�����������
                ((BaseShape)allShapes[i]).SetSelected();//���ò�׽����ͼ��Ϊѡ��״̬
                SetOperShape(((BaseShape)allShapes[i]));//��ѡ�е�ͼ���趨������Ĳ���ͼ�ε�״̬��
            }
            GetRefCadPanel().Refresh();//ˢ�»���
        }
        public override void MouseDrag(object sender, MouseEventArgs e)//��д�����϶��¼�
        {
            if (GetOperShape() != null)//����ѡ�е�ͼ��ʱ
            {
                Point setPoint = GetNewDragPoint();
                if (CatchPointIndex == 0)//�����׽���ƶ���ʱ
                {
                    setPoint = new Point();
                    setPoint.X = GetNewDragPoint().X - GetOldDragPoint().X;//����������
                    setPoint.Y = GetNewDragPoint().Y - GetOldDragPoint().Y;//����������
                }
                GetOperShape().SetHitPoint(CatchPointIndex, setPoint);//�����ȵ�
                GetRefCadPanel().Refresh();//ˢ�»���
            }
        }

        public BaseShape OldMoveShap;//�ƶ������ͼ��

        public override void MouseMove(object sender, MouseEventArgs e)//��д�����ƶ�
        {
            if (OldMoveShap != null) OldMoveShap.SetUnSelected();//����ƶ�ͼ��ѡ�е�״̬
            ArrayList allShapes = GetRefCadPanel().GetCurrentShapes();//�õ������ϵ�ͼ�μ���
            int catchPoint = -1;//��ʱ����Ĳ�׽�ȵ�
            int i = 0;
            for (; i < allShapes.Count; i++)//��ÿ��ͼ�β�׽����
            {
                catchPoint = ((BaseShape)allShapes[i]).CatchShapPoint(GetNewMovePoint());
                if (catchPoint > -1) break;//��׽������ѭ��
            }
            if (catchPoint > -1)//��׽����
            {
                ((BaseShape)allShapes[i]).SetSelected();//�趨��׽����ͼ��Ϊѡ��״̬
                OldMoveShap = (BaseShape)allShapes[i];//��ѡ�е�ͼ���趨������Ĳ���ͼ�ε�״̬��ȥ
            }
            GetRefCadPanel().Refresh();//ˢ�»���
        }

        public override void MouseUp(object sender, MouseEventArgs e)//��д�����ͷ�
        {
            GetRefCadPanel().Refresh();//ˢ�»���
        }

        public override void UnSet()//���ߵ�ж��
        {
            ArrayList allShapes = GetRefCadPanel().GetCurrentShapes();//�õ������ϵ�ͼ�μ���
            for (int i = 0; i < allShapes.Count; i++)//�������ͼ�ε�ѡ��״̬
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
