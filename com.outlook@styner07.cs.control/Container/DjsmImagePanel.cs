using System.Drawing.Imaging;

namespace com.outlook_styner07.cs.control.Container
{
    public class DjsmImagePanel : Panel
    {
        private const int CROSSLINE_MARGIN = 10;

        private const string CONTEXT_NAME_SAVE_IMAGE = "Save Image";
        private const string CONTEXT_NAME_RESET_ZOOM = "Reset Zoom";

        private readonly string IMAGE_FORMAT_BMP = nameof(ImageFormat.Bmp);
        private readonly string IMAGE_FORMAT_JPG = nameof(ImageFormat.Jpeg);
        private readonly string IMAGE_FORMAT_PNG = nameof(ImageFormat.Png);
        private readonly string IMAGE_FORMAT_TIF = nameof(ImageFormat.Tiff);

        private Image? _image;

        private int _newWidth;
        private int _newHeight;

        private float _zoomFactor = 1.0f;
        private Point _imagePosition = new Point(0, 0);  // 이미지 초기 위치
        private Point _mouseDownPosition;
        private bool _isPanning = false;

        private bool _drawCrossLine = false;
        private Color _crossLineColor = Color.Red;
        private int _crossLineWidth = 1;

        private bool _contextMenuEnabled = true;

        public Image? Image
        {
            get { return _image; }
            set
            {
                _image = value;
                Invalidate();
            }
        }

        private ContextMenuStrip _ctxMenu;

        public DjsmImagePanel()
        {
            DoubleBuffered = true;

            _ctxMenu = new ContextMenuStrip();
            _ctxMenu.Items.Clear();
            _ctxMenu.Items.Add(CONTEXT_NAME_SAVE_IMAGE);
            _ctxMenu.Items.Add(new ToolStripSeparator());
            _ctxMenu.Items.Add(CONTEXT_NAME_RESET_ZOOM);
            _ctxMenu.ItemClicked += (sender, e) =>
            {
                if (e.ClickedItem != null)
                {
                    if (_image == null)
                    {
                        return;
                    }

                    switch (e.ClickedItem.Text)
                    {
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

                            Invalidate();
                            break;
                    }
                }
            };
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            if (_image != null)
            {
                _newWidth = (int)(_image.Width * _zoomFactor);
                _newHeight = (int)(_image.Height * _zoomFactor);

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

            if (e.Delta > 0)
            {
                _zoomFactor += 0.1f;
            }
            else if (e.Delta < 0)
            {
                _zoomFactor = Math.Max(0.1f, _zoomFactor - 0.1f);
            }

            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            _ctxMenu.Hide();

            if (e.Button == MouseButtons.Left)
            {
                _isPanning = true;
                _mouseDownPosition = e.Location;  // 마우스 클릭 시의 위치 저장
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_isPanning)
            {
                // 현재 마우스 위치에서 클릭 시 위치를 빼서 이미지 위치 이동
                _imagePosition.X += e.X - _mouseDownPosition.X;
                _imagePosition.Y += e.Y - _mouseDownPosition.Y;

                // 현재 마우스 위치를 다시 저장
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
