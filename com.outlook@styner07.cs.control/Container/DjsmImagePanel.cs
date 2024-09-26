using System.Diagnostics;
using System.Drawing.Imaging;

namespace com.outlook_styner07.cs.control.Container
{
    public class DjsmImagePanel : Panel
    {
        private const int CROSSLINE_MARGIN = 10;

        private const string CONTEXT_NAME_ZOOM_SCALE_1 = "x 0.1";
        private const string CONTEXT_NAME_ZOOM_SCALE_05 = "x 0.05";
        private const string CONTEXT_NAME_ZOOM_SCALE_01 = "x 0.01";

        private const string CONTEXT_NAME_RESET = "Reset";
        private const string CONTEXT_NAME_SAVE_IMAGE = "Save Image";

        private const float ZOOM_SCALE_1 = 0.1f;
        private const float ZOOM_SCALE_05 = 0.05f;
        private const float ZOOM_SCALE_01 = 0.01f;

        private readonly string IMAGE_FORMAT_BMP = nameof(ImageFormat.Bmp);
        private readonly string IMAGE_FORMAT_JPG = nameof(ImageFormat.Jpeg);
        private readonly string IMAGE_FORMAT_PNG = nameof(ImageFormat.Png);
        private readonly string IMAGE_FORMAT_TIF = nameof(ImageFormat.Tiff);

        private Image? _image;

        private int _newWidth;
        private int _newHeight;

        private float _zoomScale = ZOOM_SCALE_1;
        private float _zoomFactor = 1.0f;

        private Point _imagePosition = new Point(0, 0);  // 이미지 초기 위치
        private Point _mouseDownPosition;
        private bool _isPanning = false;

        private bool _drawCrossLine = false;
        private Color _crossLineColor = Color.Red;
        private int _crossLineWidth = 1;

        private ContextMenuStrip _ctxMenu;
        private ToolStripMenuItem _ctxMenuZoomScale1;
        private ToolStripMenuItem _ctxMenuZoomScale05;
        private ToolStripMenuItem _ctxMenuZoomScale01;

        private bool _contextMenuEnabled = true;

        public Image? Image
        {
            get { return _image; }
            set
            {
                if (value == null)
                {
                    return;
                }

                _image = value;
                _newWidth = _image.Width;
                _newHeight = _image.Height;

                Invalidate();
            }
        }

        public DjsmImagePanel()
        {
            DoubleBuffered = true;
            InitializeContextMenu();
            LoadProperties();
        }

        public void ContextMenuEnabled(bool enable)
        {
            _contextMenuEnabled = enable;
        }

        public void DrawCrossLine(bool draw)
        {
            DrawCrossLine(draw, Color.Red);
        }

        public void DrawCrossLine(bool draw, Color lineColor)
        {
            DrawCrossLine(draw, lineColor, 1);
        }

        public void DrawCrossLine(bool draw, Color lineColor, int lineWidth)
        {
            _drawCrossLine = draw;
            _crossLineColor = lineColor;
            _crossLineWidth = lineWidth;

            Invalidate();
        }

        private void InitializeContextMenu()
        {
            _ctxMenu = new ContextMenuStrip { AutoClose = true };

            _ctxMenu.Items.Clear();
            _ctxMenu.Items.Add(_ctxMenuZoomScale1 = new ToolStripMenuItem(CONTEXT_NAME_ZOOM_SCALE_1, null));
            _ctxMenu.Items.Add(_ctxMenuZoomScale05 = new ToolStripMenuItem(CONTEXT_NAME_ZOOM_SCALE_05, null));
            _ctxMenu.Items.Add(_ctxMenuZoomScale01 = new ToolStripMenuItem(CONTEXT_NAME_ZOOM_SCALE_01, null));
            _ctxMenu.Items.Add(new ToolStripSeparator());
            _ctxMenu.Items.Add(CONTEXT_NAME_RESET);
            _ctxMenu.Items.Add(new ToolStripSeparator());
            _ctxMenu.Items.Add(CONTEXT_NAME_SAVE_IMAGE);

            _ctxMenu.ItemClicked += (sender, e) =>
            {
                _ctxMenu.Hide();

                if (e.ClickedItem != null)
                {
                    if (_image == null)
                    {
                        return;
                    }

                    switch (e.ClickedItem.Text)
                    {
                        case CONTEXT_NAME_ZOOM_SCALE_1:
                            _zoomScale = ZOOM_SCALE_1;
                            _ctxMenuZoomScale1.Checked = true;
                            _ctxMenuZoomScale05.Checked = false;
                            _ctxMenuZoomScale01.Checked = false;
                            break;
                        case CONTEXT_NAME_ZOOM_SCALE_05:
                            _zoomScale = ZOOM_SCALE_05;
                            _ctxMenuZoomScale1.Checked = false;
                            _ctxMenuZoomScale05.Checked = true;
                            _ctxMenuZoomScale01.Checked = false;
                            break;
                        case CONTEXT_NAME_ZOOM_SCALE_01:
                            _zoomScale = ZOOM_SCALE_01;
                            _ctxMenuZoomScale1.Checked = false;
                            _ctxMenuZoomScale05.Checked = false;
                            _ctxMenuZoomScale01.Checked = true;
                            break;
                        case CONTEXT_NAME_SAVE_IMAGE:
                            SaveFileDialog dlg = new SaveFileDialog
                            {
                                Filter =
                                $"Bitmap Image (.{IMAGE_FORMAT_BMP})|*.{IMAGE_FORMAT_BMP}" +
                                $"|JPEG Image (.{IMAGE_FORMAT_JPG})|*.{IMAGE_FORMAT_JPG}" +
                                $"|Portable Network Graphics (.{IMAGE_FORMAT_PNG})|*.{IMAGE_FORMAT_PNG}" +
                                $"|Tagged Image File Format (.{IMAGE_FORMAT_TIF})|*.{IMAGE_FORMAT_TIF}",
                                AddExtension = true,
                            };

                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                ImageFormat format;

                                if (dlg.FileName.EndsWith(IMAGE_FORMAT_JPG))
                                {
                                    format = ImageFormat.Jpeg;
                                }
                                else if (dlg.FileName.EndsWith(IMAGE_FORMAT_PNG))
                                {
                                    format = ImageFormat.Png;
                                }
                                else if (dlg.FileName.EndsWith(IMAGE_FORMAT_TIF))
                                {
                                    format = ImageFormat.Tiff;
                                }
                                else
                                {
                                    format = ImageFormat.Bmp;
                                }

                                _image.Save(dlg.FileName, format);
                            }
                            break;

                        case CONTEXT_NAME_RESET:
                            _zoomFactor = 1;

                            _imagePosition.X = Width < _image.Width ? 0 : (Width - _image.Width) / 2;
                            _imagePosition.Y = Height < _image.Height ? 0 : (Height - _image.Height) / 2;

                            _newWidth = _image.Width;
                            _newHeight = _image.Height;

                            Invalidate();
                            break;
                    }
                }
            };
        }

        private void LoadProperties()
        {
            _zoomScale = Properties.Settings.Default.zoomScale;

            if (_zoomScale == 0)
            {
                _zoomScale = ZOOM_SCALE_1;
            }

            switch (_zoomScale)
            {
                case ZOOM_SCALE_1:
                    _ctxMenuZoomScale1.Checked = true;
                    break;
                case ZOOM_SCALE_05:
                    _ctxMenuZoomScale05.Checked = true;
                    break;
                default:
                    _ctxMenuZoomScale01.Checked = true;
                    break;
            }
        }

        private void SaveProperties()
        {
            Properties.Settings.Default.zoomScale = _zoomScale;
            Properties.Settings.Default.Save();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            SaveProperties();
            base.OnHandleDestroyed(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            if (_image != null)
            {
                g.DrawImage(_image, new Rectangle(_imagePosition.X, _imagePosition.Y, _newWidth, _newHeight));
            }

            if (_drawCrossLine)
            {
                using (Pen pen = new Pen(_crossLineColor, _crossLineWidth))
                {
                    Rectangle rect = ClientRectangle;

                    /// draw vertical
                    g.DrawLine(pen, rect.Width / 2, CROSSLINE_MARGIN, rect.Width / 2, rect.Height - CROSSLINE_MARGIN);

                    /// draw horizontal
                    g.DrawLine(pen, CROSSLINE_MARGIN, rect.Height / 2, rect.Width - CROSSLINE_MARGIN, rect.Height / 2);
                }
            }
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);

            if (_image != null)
            {
                _imagePosition.X = Width < _newWidth ? 0 : (Width - _newWidth) / 2;
                _imagePosition.Y = Height < _newHeight ? 0 : (Height - _newHeight) / 2;
            }

            Invalidate();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (_image == null)
            {
                return;
            }

            Rectangle boundRect = new Rectangle(_imagePosition.X + 1, _imagePosition.Y + 1, _newWidth - 1, _newHeight - 1);

            if (boundRect.Contains(e.X, e.Y))
            {
                bool enlarge = false;
                if (e.Delta > 0)
                {
                    _zoomFactor += _zoomScale;
                    enlarge = true;
                }
                else if (e.Delta < 0)
                {
                    _zoomFactor = Math.Max(0.1f, _zoomFactor - _zoomScale);
                    enlarge = false;
                }

                int oldWidth = _newWidth;
                int oldHeight = _newHeight;

                _newWidth = (int)(_image.Width * _zoomFactor);
                _newHeight = (int)(_image.Height * _zoomFactor);

                //double deltaX = (oldWidth - _newWidth) * ((e.X - _imagePosition.X) / (double)oldWidth);
                //double deltaY = (oldHeight - _newHeight) * ((e.Y - _imagePosition.Y) / (double)oldHeight);
                double deltaX;
                double deltaY;

                /// 기어올라가는 현상 수정
                deltaX = (oldWidth - _newWidth) * ((e.X - _imagePosition.X) / (double)oldWidth) ;
                deltaY = (oldHeight - _newHeight) * ((e.Y - _imagePosition.Y) / (double)oldHeight) ;

                //if (enlarge)
                //{
                //    deltaX = (oldWidth - _newWidth) * ((e.X - _imagePosition.X) / (double)_newWidth);
                //    deltaY = (oldHeight - _newHeight) * ((e.Y - _imagePosition.Y) / (double)_newWidth);

                //}
                //else
                //{

                //}

                Debug.WriteLine($"{deltaX}  {deltaY}");

                _imagePosition.X = (int)(_imagePosition.X + deltaX);
                _imagePosition.Y = (int)(_imagePosition.Y + deltaY);

                Invalidate();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                _isPanning = true;
                _mouseDownPosition = e.Location;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_isPanning)
            {
                _imagePosition.X += e.X - _mouseDownPosition.X;
                _imagePosition.Y += e.Y - _mouseDownPosition.Y;

                _mouseDownPosition = e.Location;

                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button == MouseButtons.Left)
            {
                _isPanning = false;
            }
            else if (e.Button == MouseButtons.Right)
            {
                _ctxMenu.Show(this, e.X, e.Y);
            }
        }
    }
}
