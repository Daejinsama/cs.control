using System.Drawing.Imaging;

namespace com.outlook_styner07.cs.control.Container
{
    public class DjsmImagePanel : Panel
    {
        private const int CROSSLINE_MARGIN = 10;

        private const string CONTEXT_NAME_FIT_TO_FRAME = "Fit To Frame";
        private const string CONTEXT_NAME_RESET_ZOOM = "Reset zoom";
        private const string CONTEXT_NAME_SAVE_IMAGE = "Save Image";

        private const float ZOOM_SCALE_1 = 0.1f;
        //private const float ZOOM_SCALE_05 = 0.05f;
        //private const float ZOOM_SCALE_01 = 0.01f;

        private readonly string IMAGE_FORMAT_BMP = nameof(ImageFormat.Bmp);
        private readonly string IMAGE_FORMAT_JPG = nameof(ImageFormat.Jpeg);
        private readonly string IMAGE_FORMAT_PNG = nameof(ImageFormat.Png);
        private readonly string IMAGE_FORMAT_TIF = nameof(ImageFormat.Tiff);

        private Image? _image;

        private float _newWidth;
        private float _newHeight;

        private float _zoomScale = ZOOM_SCALE_1;
        private float _zoomFactor = 1.0f;

        private PointF _imagePosition = new PointF(0, 0);  // 이미지 초기 위치
        private PointF _mouseDownPosition;
        private bool _isPanning = false;

        private bool _drawCrossLine = false;
        private Color _crossLineColor = Color.Red;
        private int _crossLineWidth = 1;

        private bool _drawImageCenter = false;
        private Color _imageCenterColor = Color.Lime;
        private int _imageCenterWidth = 1;

        private ContextMenuStrip _ctxMenu;
        
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

                if (_image == null)
                {
                    _newWidth = value.Width;
                    _newHeight = value.Height;
                }

                _image = value;

                Invalidate();
            }
        }

        public DjsmImagePanel()
        {
            DoubleBuffered = true;
            InitializeContextMenu();
        }

        public void ContextMenuEnabled(bool enable)
        {
            _contextMenuEnabled = enable;
        }

        public void FitToFrame()
        {
            if (_image == null)
            {
                return;
            }
            float imageAspect = (float)_image.Width / _image.Height;
            float frameAspect = (float)Width / Height;

            if (frameAspect > imageAspect)
            {
                _newHeight = Height;
                _newWidth = (int)(Height * imageAspect);

                _imagePosition.X = (Width - _newWidth) / 2;
                _imagePosition.Y = 0;
            }
            else
            {
                _newWidth = Width;
                _newHeight = (int)(Width / imageAspect);

                _imagePosition.X = 0;
                _imagePosition.Y = (Height - _newHeight) / 2;
            }

            Invalidate();
        }

        public void DrawCrossLine(bool draw)
        {
            DrawCrossLine(draw, Color.Red, 1);
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

        public void DrawImageCenter(bool draw)
        {
            DrawImageCenter(draw, Color.Lime, 1);
        }

        public void DrawImageCenter(bool draw, Color lineColor)
        {
            DrawImageCenter(draw, lineColor, 1);
        }

        public void DrawImageCenter(bool draw, Color lineColor, int lineWidth)
        {
            _drawImageCenter = draw;
            _imageCenterColor = lineColor;
            _imageCenterWidth = lineWidth;

            Invalidate();
        }

        private void InitializeContextMenu()
        {
            _ctxMenu = new ContextMenuStrip { AutoClose = true };

            _ctxMenu.Items.Clear();
            _ctxMenu.Items.Add(CONTEXT_NAME_FIT_TO_FRAME);
            _ctxMenu.Items.Add(new ToolStripSeparator());
            _ctxMenu.Items.Add(CONTEXT_NAME_RESET_ZOOM);
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
                        //case CONTEXT_NAME_ZOOM_SCALE_1:
                        //    _zoomScale = ZOOM_SCALE_1;
                        //    _ctxMenuZoomScale1.Checked = true;
                        //    _ctxMenuZoomScale05.Checked = false;
                        //    _ctxMenuZoomScale01.Checked = false;
                        //    break;
                        //case CONTEXT_NAME_ZOOM_SCALE_05:
                        //    _zoomScale = ZOOM_SCALE_05;
                        //    _ctxMenuZoomScale1.Checked = false;
                        //    _ctxMenuZoomScale05.Checked = true;
                        //    _ctxMenuZoomScale01.Checked = false;
                        //    break;
                        //case CONTEXT_NAME_ZOOM_SCALE_01:
                        //    _zoomScale = ZOOM_SCALE_01;
                        //    _ctxMenuZoomScale1.Checked = false;
                        //    _ctxMenuZoomScale05.Checked = false;
                        //    _ctxMenuZoomScale01.Checked = true;
                        //    break;
                        case CONTEXT_NAME_FIT_TO_FRAME:
                            FitToFrame();
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

                        case CONTEXT_NAME_RESET_ZOOM:
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            if (_image != null)
            {
                g.DrawImage(_image, new RectangleF(_imagePosition.X, _imagePosition.Y, _newWidth, _newHeight));
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

            if (_image != null && _drawImageCenter)
            {
                using (Pen pen = new Pen(_imageCenterColor, _imageCenterWidth))
                {
                    RectangleF rect = new RectangleF(_imagePosition.X, _imagePosition.Y, _newWidth, _newHeight);

                    g.DrawLine(pen,
                        rect.X + rect.Width / 2,
                        rect.Y,
                        rect.X + rect.Width / 2,
                        rect.Y + rect.Height);

                    g.DrawLine(pen,
                        rect.X,
                        rect.Y + rect.Height / 2,
                        rect.X + rect.Width,
                        rect.Y + rect.Height / 2);
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

            RectangleF boundRect = new RectangleF(_imagePosition.X + 1, _imagePosition.Y + 1, _newWidth - 1, _newHeight - 1);

            if (boundRect.Contains(e.X, e.Y))
            {
                /* ---------------------------------------------------------------------------------------------------
                 * 줌 인 아웃을 반복할때 이미지가 쏠리는 원인은 데이터 타입 변환 과정에서의 정밀도 차이인 것으로 확인. 
                 * (float으로 통일 후 현상 개선)              
                 * ---------------------------------------------------------------------------------------------------*/
                if (e.Delta > 0)
                {
                    _zoomFactor += _zoomScale;
                }
                else if (e.Delta < 0)
                {
                    _zoomFactor = Math.Max(0.1f, _zoomFactor - _zoomScale);
                }

                float oldWidth = _newWidth;
                float oldHeight = _newHeight;

                _newWidth = _image.Width * _zoomFactor;
                _newHeight = _image.Height * _zoomFactor;

                float deltaX = (oldWidth - _newWidth) * ((e.X - _imagePosition.X) / oldWidth);
                float deltaY = (oldHeight - _newHeight) * ((e.Y - _imagePosition.Y) / oldHeight);

                //Debug.WriteLine($"{deltaX}  {deltaY}");

                _imagePosition.X = _imagePosition.X + deltaX;
                _imagePosition.Y = _imagePosition.Y + deltaY;

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
