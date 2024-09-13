namespace com.outlook_styner07.cs.control.Container
{
    public class DjsmImagePanel : Panel
    {
        private Image? _image;
        private float _zoomFactor = 1.0f;
        private Point _imagePosition = new Point(0, 0);  // 이미지 초기 위치
        private Point _mouseDownPosition;
        private bool _isPanning = false;

        public Image? Image
        {
            get { return _image; }
            set
            {
                _image = value;
                Invalidate();
            }
        }

        public DjsmImagePanel()
        {
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Image == null)
            {
                return;
            }

            Graphics g = e.Graphics;

            int newWidth = (int)(_image.Width * _zoomFactor);
            int newHeight = (int)(_image.Height * _zoomFactor);

            g.DrawImage(_image, new Rectangle(_imagePosition.X, _imagePosition.Y, newWidth, newHeight));
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

            _isPanning = true;
            _mouseDownPosition = e.Location;  // 마우스 클릭 시의 위치 저장
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

            _isPanning = false;
        }
    }
}
