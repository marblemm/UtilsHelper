using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CaptureTool
{
    public partial class DrawToolsControl : UserControl
    {
        #region Fields

        private CaptureImageToolColorTable _colorTable;
        private ToolStripButton _checkButton;

        private static readonly object EventButtonRedoClick = new object();
        private static readonly object EventButtonSaveClick = new object();
        private static readonly object EventButtonExitClick = new object();
        private static readonly object EventButtonAcceptClick = new object();
        private static readonly object EventButtonDrawStyleClick = new object();

        #endregion

        #region Constructors

        public DrawToolsControl()
        {
            InitializeComponent();
            DoubleBuffered = true;
            ResizeRedraw = true;
            InitEvents();

            toolStrip.Renderer = new ToolStripRendererEx();
        }

        #endregion

        #region Events

        public event EventHandler ButtonDrawStyleClick
        {
            add { Events.AddHandler(EventButtonDrawStyleClick, value); }
            remove { Events.RemoveHandler(EventButtonDrawStyleClick, value); }
        }

        public event EventHandler ButtonRedoClick
        {
            add { Events.AddHandler(EventButtonRedoClick, value); }
            remove { Events.RemoveHandler(EventButtonRedoClick, value); }
        }

        public event EventHandler ButtonSaveClick
        {
            add { Events.AddHandler(EventButtonSaveClick, value); }
            remove { Events.RemoveHandler(EventButtonSaveClick, value); }
        }

        public event EventHandler ButtonExitClick
        {
            add { Events.AddHandler(EventButtonExitClick, value); }
            remove { Events.RemoveHandler(EventButtonExitClick, value); }
        }

        public event EventHandler ButtonAcceptClick
        {
            add { Events.AddHandler(EventButtonAcceptClick, value); }
            remove { Events.RemoveHandler(EventButtonAcceptClick, value); }
        }

        #endregion

        #region Properties

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CaptureImageToolColorTable ColorTable
        {
            get
            {
                if (_colorTable == null)
                {
                    _colorTable = new CaptureImageToolColorTable();
                }
                return _colorTable;
            }
            set
            {
                _colorTable = value;
                Invalidate();
                toolStrip.Renderer = new ToolStripRendererEx(value);
            }
        }

        [Browsable(false)]
        public DrawStyle DrawStyle { get; private set; }

        [DefaultValue(typeof(DrawToolsDockStyle), "0")]
        [Browsable(false)]
        public DrawToolsDockStyle DrawToolsDockStyle { get; set; }

        private ToolStripButton CheckButton
        {
            get { return _checkButton; }
            set
            {
                if (_checkButton != null && _checkButton != value)
                {
                    _checkButton.Checked = false;
                }

                _checkButton = value;
                if (_checkButton != null)
                {
                    _checkButton.Checked = true;
                }
            }
        }

        protected override Size DefaultSize
        {
            get { return new Size(224, 29); }
        }

        #endregion

        #region Public Methods

        public void ResetItemState()
        {
            switch (DrawStyle)
            {
                case DrawStyle.Rectangle:
                    toolStripButtonRectangular.Checked = false;
                    break;
                case DrawStyle.Ellipse:
                    toolStripButtonEllipse.Checked = false;
                    break;
                case DrawStyle.Arrow:
                    toolStripButtonArrow.Checked = false;
                    break;
                case DrawStyle.Text:
                    toolStripButtonText.Checked = false;
                    break;
                case DrawStyle.Line:
                    toolStripButtonLine.Checked = false;
                    break;
            }
        }

        public void ResetDrawStyle()
        {
            ResetItemState();
            DrawStyle = DrawStyle.None;
        }

        #endregion

        #region Override Methods

        protected virtual void OnButtonDrawStyleClick(EventArgs e)
        {
            EventHandler handler = Events[EventButtonDrawStyleClick] as EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnButtonRedoClick(EventArgs e)
        {
            EventHandler handler = Events[EventButtonRedoClick] as EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnButtonSaveClick(EventArgs e)
        {
            EventHandler handler = Events[EventButtonSaveClick] as EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnButtonExitClick(EventArgs e)
        {
            EventHandler handler = Events[EventButtonExitClick] as EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnButtonAcceptClick(EventArgs e)
        {
            EventHandler handler = Events[EventButtonAcceptClick] as EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            SetRegion();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetRegion();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Cursor = Cursors.Default;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath path = GraphicsPathHelper.CreatePath(ClientRectangle, 8, RoundStyle.All, false))
            {
                using (SolidBrush brush = new SolidBrush(ColorTable.BackColorNormal))
                {
                    g.FillPath(brush, path);
                }
                using (Pen pen = new Pen(ColorTable.BorderColor))
                {
                    g.DrawPath(pen, path);

                    using (GraphicsPath innerPath = GraphicsPathHelper.CreatePath(ClientRectangle, 8, RoundStyle.All, true))
                    {
                        g.DrawPath(pen, innerPath);
                    }
                }
            }
        }

        #endregion

        #region Private Methods

        private void SetRegion()
        {
            using (GraphicsPath path = GraphicsPathHelper.CreatePath(ClientRectangle, 8, RoundStyle.All, false))
            {
                if (Region != null)
                {
                    Region.Dispose();
                }
                Region = new Region(path);
            }
        }

        private void InitEvents()
        {
            toolStrip.ItemClicked += ToolStripItemClicked;
        }

        private void ToolStripItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "toolStripButtonRectangular":
                    if (DrawStyle != DrawStyle.Rectangle)
                    {
                        DrawStyle = DrawStyle.Rectangle;
                        CheckButton = toolStripButtonRectangular;
                    }
                    else
                    {
                        DrawStyle = DrawStyle.None;
                        CheckButton = null;
                    }
                    OnButtonDrawStyleClick(e);
                    break;
                case "toolStripButtonEllipse":
                    ResetItemState();
                    if (DrawStyle != DrawStyle.Ellipse)
                    {
                        DrawStyle = DrawStyle.Ellipse;
                        CheckButton = toolStripButtonEllipse;
                    }
                    else
                    {
                        DrawStyle = DrawStyle.None;
                        CheckButton = null;
                    }
                    OnButtonDrawStyleClick(e);
                    break;
                case "toolStripButtonText":
                    ResetItemState();
                    if (DrawStyle != DrawStyle.Text)
                    {
                        DrawStyle = DrawStyle.Text;
                        CheckButton = toolStripButtonText;
                    }
                    else
                    {
                        DrawStyle = DrawStyle.None;
                        CheckButton = null;
                    }
                    OnButtonDrawStyleClick(e);
                    break;
                case "toolStripButtonArrow":
                    ResetItemState();
                    if (DrawStyle != DrawStyle.Arrow)
                    {
                        DrawStyle = DrawStyle.Arrow;
                        CheckButton = toolStripButtonArrow;
                    }
                    else
                    {
                        DrawStyle = DrawStyle.None;
                        CheckButton = null;
                    }
                    OnButtonDrawStyleClick(e);
                    break;
                case "toolStripButtonLine":
                    ResetItemState();
                    if (DrawStyle != DrawStyle.Line)
                    {
                        DrawStyle = DrawStyle.Line;
                        CheckButton = toolStripButtonLine;
                    }
                    else
                    {
                        DrawStyle = DrawStyle.None;
                        CheckButton = null;
                    }
                    OnButtonDrawStyleClick(e);
                    break;
                case "toolStripButtonRedo":
                    OnButtonRedoClick(e);
                    break;
                case "toolStripButtonSave":
                    OnButtonSaveClick(e);
                    break;
                case "toolStripButtonExit":
                    OnButtonExitClick(e);
                    break;
                case "toolStripButtonAccept":
                    OnButtonAcceptClick(e);
                    break;
            }
        }

        #endregion
    }
}
