using static com.outlook_styner07.cs.control.DjsmColorTable;

namespace com.outlook_styner07.cs.control.Container
{
    public class DjsmToolStripRenderer : ToolStripProfessionalRenderer
    {
        public DjsmToolStripRenderer() : base(new NntProfessionalColorTable())
        {
            RoundedEdges = false;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override void Initialize(ToolStrip toolStrip)
        {
            base.Initialize(toolStrip);
        }

        protected override void InitializeContentPanel(ToolStripContentPanel contentPanel)
        {
            base.InitializeContentPanel(contentPanel);
        }

        protected override void InitializeItem(ToolStripItem item)
        {
            base.InitializeItem(item);
        }

        protected override void InitializePanel(ToolStripPanel toolStripPanel)
        {
            base.InitializePanel(toolStripPanel);
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            base.OnRenderArrow(e);
        }

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderButtonBackground(e);
        }

        protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderDropDownButtonBackground(e);
        }

        protected override void OnRenderGrip(ToolStripGripRenderEventArgs e)
        {
            base.OnRenderGrip(e);
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            base.OnRenderImageMargin(e);
        }

        protected override void OnRenderItemBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderItemBackground(e);
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            base.OnRenderItemCheck(e);
        }

        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
        {
            base.OnRenderItemImage(e);
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            base.OnRenderItemText(e);
        }

        protected override void OnRenderLabelBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderLabelBackground(e);
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderMenuItemBackground(e);
        }

        protected override void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderOverflowButtonBackground(e);
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            base.OnRenderSeparator(e);
        }

        protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderSplitButtonBackground(e);
        }

        protected override void OnRenderStatusStripSizingGrip(ToolStripRenderEventArgs e)
        {
            base.OnRenderStatusStripSizingGrip(e);
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            base.OnRenderToolStripBackground(e);
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            base.OnRenderToolStripBorder(e);
        }

        protected override void OnRenderToolStripContentPanelBackground(ToolStripContentPanelRenderEventArgs e)
        {
            base.OnRenderToolStripContentPanelBackground(e);
        }

        protected override void OnRenderToolStripPanelBackground(ToolStripPanelRenderEventArgs e)
        {
            base.OnRenderToolStripPanelBackground(e);
        }

        protected override void OnRenderToolStripStatusLabelBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderToolStripStatusLabelBackground(e);
        }
    }

    public class NntProfessionalColorTable : ProfessionalColorTable
    {
        public override Color ButtonSelectedHighlight => DEFAULT_BACKGROUND_SELECTED_COLOR;

        public override Color ButtonSelectedHighlightBorder => DEFAULT_BORDER_COLOR;

        public override Color ButtonPressedHighlight => DEFAULT_BACKGROUND_FOCUSED_COLOR;

        public override Color ButtonPressedHighlightBorder => DEFAULT_BORDER_COLOR;

        public override Color ButtonCheckedHighlight => DEFAULT_BACKGROUND_SELECTED_COLOR;

        public override Color ButtonCheckedHighlightBorder => DEFAULT_BORDER_COLOR;

        public override Color ButtonPressedBorder => DEFAULT_BORDER_COLOR;

        public override Color ButtonSelectedBorder => DEFAULT_BORDER_COLOR;

        public override Color ButtonCheckedGradientBegin => DEFAULT_BACKGROUND_SELECTED_COLOR;

        public override Color ButtonCheckedGradientMiddle => DEFAULT_BACKGROUND_SELECTED_COLOR;

        public override Color ButtonCheckedGradientEnd => DEFAULT_BACKGROUND_SELECTED_COLOR;

        public override Color ButtonSelectedGradientBegin => DEFAULT_BACKGROUND_SELECTED_COLOR;

        public override Color ButtonSelectedGradientMiddle => DEFAULT_BACKGROUND_SELECTED_COLOR;

        public override Color ButtonSelectedGradientEnd => DEFAULT_BACKGROUND_SELECTED_COLOR;

        public override Color ButtonPressedGradientBegin => DEFAULT_BACKGROUND_FOCUSED_COLOR;

        public override Color ButtonPressedGradientMiddle => DEFAULT_BACKGROUND_FOCUSED_COLOR;

        public override Color ButtonPressedGradientEnd => DEFAULT_BACKGROUND_FOCUSED_COLOR;

        public override Color CheckBackground => DEFAULT_BACKGROUND_COLOR;

        public override Color CheckSelectedBackground => DEFAULT_BACKGROUND_SELECTED_COLOR;

        public override Color CheckPressedBackground => DEFAULT_BACKGROUND_SELECTED_COLOR;

        public override Color GripDark => base.GripDark;

        public override Color GripLight => base.GripLight;

        public override Color ImageMarginGradientBegin => DEFAULT_BORDER_COLOR;

        public override Color ImageMarginGradientMiddle => DEFAULT_BORDER_COLOR;

        public override Color ImageMarginGradientEnd => DEFAULT_BORDER_COLOR;

        public override Color ImageMarginRevealedGradientBegin => DEFAULT_BORDER_COLOR;

        public override Color ImageMarginRevealedGradientMiddle => DEFAULT_BORDER_COLOR;

        public override Color ImageMarginRevealedGradientEnd => DEFAULT_BORDER_COLOR;

        public override Color MenuStripGradientBegin => DEFAULT_BORDER_COLOR;

        public override Color MenuStripGradientEnd => DEFAULT_BORDER_COLOR;

        public override Color MenuItemSelected => DEFAULT_BACKGROUND_SELECTED_COLOR;

        public override Color MenuItemBorder => DEFAULT_BORDER_COLOR;

        public override Color MenuBorder => DEFAULT_BORDER_COLOR;

        public override Color MenuItemSelectedGradientBegin => DEFAULT_BACKGROUND_SELECTED_COLOR;

        public override Color MenuItemSelectedGradientEnd => DEFAULT_BACKGROUND_SELECTED_COLOR;

        public override Color MenuItemPressedGradientBegin => DEFAULT_BACKGROUND_FOCUSED_COLOR;

        public override Color MenuItemPressedGradientMiddle => DEFAULT_BACKGROUND_FOCUSED_COLOR;

        public override Color MenuItemPressedGradientEnd => DEFAULT_BACKGROUND_FOCUSED_COLOR;

        public override Color RaftingContainerGradientBegin => DEFAULT_BACKGROUND_SELECTED_COLOR;

        public override Color RaftingContainerGradientEnd => DEFAULT_BACKGROUND_SELECTED_COLOR;

        public override Color SeparatorDark => base.SeparatorDark;

        public override Color SeparatorLight => base.SeparatorLight;

        //public override Color StatusStripBorder => StatusStripBorder;

        public override Color StatusStripGradientBegin => Color.Transparent;

        public override Color StatusStripGradientEnd => Color.Transparent;

        public override Color ToolStripBorder => Color.Transparent;

        public override Color ToolStripDropDownBackground => DEFAULT_BACKGROUND_COLOR;

        public override Color ToolStripGradientBegin => DEFAULT_BACKGROUND_COLOR;

        public override Color ToolStripGradientMiddle => DEFAULT_BACKGROUND_COLOR;

        public override Color ToolStripGradientEnd => DEFAULT_BACKGROUND_COLOR;

        public override Color ToolStripContentPanelGradientBegin => DEFAULT_BACKGROUND_COLOR;

        public override Color ToolStripContentPanelGradientEnd => DEFAULT_BACKGROUND_COLOR;

        public override Color ToolStripPanelGradientBegin => DEFAULT_BACKGROUND_COLOR;

        public override Color ToolStripPanelGradientEnd => DEFAULT_BACKGROUND_COLOR;

        public override Color OverflowButtonGradientBegin => DEFAULT_BACKGROUND_COLOR;

        public override Color OverflowButtonGradientMiddle => DEFAULT_BACKGROUND_COLOR;

        public override Color OverflowButtonGradientEnd => DEFAULT_BACKGROUND_COLOR;

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
