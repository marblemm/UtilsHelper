using System.Drawing;

namespace CaptureTool
{
    internal class OperateObject
    {
        public OperateObject() { }

        public OperateObject(
            OperateType operateType, Color color, object data)
        {
            OperateType = operateType;
            Color = color;
            Data = data;
        }

        public OperateType OperateType { get; set; }

        public Color Color { get; set; }

        public object Data { get; set; }
    }
}
