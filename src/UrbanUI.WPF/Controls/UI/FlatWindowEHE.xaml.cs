using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using UrbanUI_WPF.Controls;
using UrbanUI_WPF.Themes;
using static UrbanUI_WPF.Win32.Win32Point;

namespace UrbanUI_WPF.UI
{

    [TemplatePart(Name = "minimizeButton", Type = typeof(WindowButton))]
    [TemplatePart(Name = "maximizeButton", Type = typeof(WindowButton))]
    [TemplatePart(Name = "closeButton", Type = typeof(WindowButton))]
    [TemplatePart(Name = "dragTopGrid", Type = typeof(Grid))]
    [TemplatePart(Name = "MainGridContainer", Type = typeof(Border))]
    [TemplatePart(Name = "windowIcon", Type = typeof(PathIconControl))]
    [TemplatePart(Name = "windowTitle", Type = typeof(TextBlock))]
    public partial class FlatWindow : Window, ITheme
    {
        const string ICON_MAXIMIZE = "M37.5 162.5V37.5H162.5V162.5H37.5zM150 50H50V150H150V50z";
        const string ICON_MINIMIZE = "M175 100V87.5H37.5V100H175z";
        const string ICON_CLOSE = "M88.95 100L31.975 43.025L43.025 31.975L100 88.95L156.975 31.975L168.025 43.025L111.05 100L168.025 156.975L156.975 168.025L100 111.05L43.025 168.025L31.975 156.975L88.95 100z";
        const string ICON_NORMAL = "M37.5 137.5V25H150V137.5H37.5zM137.5 37.5H50V125H137.5V37.5zM62.5 137.5H75V150H162.5V62.5H150V50H175V162.5H62.5V137.5z";

        private bool mRestoreIfMove = false;
        private Theme _theme;

        private WindowButton minimizeButton, maximizeButton, closeButton;
        private Grid dragTopGrid;
        private Border MainGridContainer;
        private PathIconControl windowIcon;
        private TextBlock windowTitle;
        private bool _templateApplied = false;

        public FlatWindow()
        {
            InitializeComponent();

            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatWindow), new FrameworkPropertyMetadata(typeof(FlatWindow)));
        }

        #region On Apply Template
        public override void OnApplyTemplate()
        {
            minimizeButton = GetTemplateChild("minimizeButton") as WindowButton;
            if (minimizeButton != null)
                minimizeButton.Click += MinimizeButton_Click;

            maximizeButton = GetTemplateChild("maximizeButton") as WindowButton;
            if (maximizeButton != null)
                maximizeButton.Click += MaximizeButton_Click;

            closeButton = GetTemplateChild("closeButton") as WindowButton;
            if (closeButton != null)
                closeButton.Click += CloseButton_Click;

            dragTopGrid = GetTemplateChild("dragTopGrid") as Grid;
            if (dragTopGrid != null)
            {
                dragTopGrid.MouseLeftButtonDown += Grid_MouseLeftButtonDown;
                dragTopGrid.MouseLeftButtonUp += Grid_MouseLeftButtonUp;
                dragTopGrid.MouseMove += Grid_MouseMove;
            }

            MainGridContainer = GetTemplateChild("MainGridContainer") as Border;
            windowIcon = GetTemplateChild("MainGridContainer") as PathIconControl;
            windowTitle = GetTemplateChild("MainGridContainer") as TextBlock;

            SizeChanged += Window_SizeChanged;

            base.OnApplyTemplate();

            _templateApplied = true;

            SetThemeUI(_theme);
        }
        #endregion On Apply Template


        #region Themeing
        public void SetThemeUI(Theme theme, bool UpdateContentThemes = false)
        {
            _theme = theme;
            ChangeTheme(theme);
        }

        public Theme GetTheme()
        {
            return _theme;
        }




        #region Change Theme
        private void ChangeTheme(Theme theme)
        {
            if (_templateApplied)
            {
                ChangeWindowButtonColor(minimizeButton, theme, new Icon() { StrokePath = ICON_MINIMIZE, FilledPath = ICON_MINIMIZE, Width = Height = 1200 });

                ChangeWindowButtonColor(maximizeButton, theme, new Icon() { StrokePath = ICON_MAXIMIZE, FilledPath = ICON_MAXIMIZE, Width = Height = 1200 });

                ChangeWindowButtonColor(closeButton, theme, new Icon() { StrokePath = ICON_CLOSE, FilledPath = ICON_CLOSE, Width = Height = 1200 });
                closeButton.MouseEnterBackground = Brushes.Red;

                windowIcon.Color = theme.MenuFocusIconColor;
                windowTitle.Foreground = theme.ThemeForeground;
                MainGridContainer.Background = theme.ThemeBackground;
            }
        }

        private void ChangeWindowButtonColor(WindowButton winButton, Theme theme, Icon icon)
        {
            if (theme == null)
            {
                return;
            }

            winButton.ButtonBackground = Brushes.Transparent;
            winButton.MouseEnterBackground = theme.MenuHighlightBackground;
            winButton.MousePressedBackground = theme.MenuFocusBackground;
            winButton.MouseEnterIconColor = theme.MenuForeground;
            winButton.IconColor = theme.MenuForeground;
            winButton.PressedIconColor = theme.MenuFocusIconColor;
            winButton.IconPath = icon?.StrokePath;
            winButton.PressedIconPath = string.IsNullOrWhiteSpace(icon?.StrokePath) ? icon?.StrokePath : icon?.FilledPath;
        }
        #endregion Change Theme

        #endregion Themeing


        #region Window Styling Functions
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchWindowState();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            if (WindowState == WindowState.Maximized)
            {
                SwitchWindowState(WindowState, true);
                BorderThickness = new Thickness(8);
            }
            else
            {
                SwitchWindowState(WindowState, true);
                BorderThickness = new Thickness(0);
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (ResizeMode == ResizeMode.CanResize || ResizeMode == ResizeMode.CanResizeWithGrip)
                {
                    SwitchWindowState();
                }

                return;
            }

            else if (WindowState == WindowState.Maximized)
            {
                mRestoreIfMove = true;
                return;
            }

            DragMove();
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            mRestoreIfMove = false;
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (mRestoreIfMove)
            {
                mRestoreIfMove = false;

                double percentHorizontal = e.GetPosition(this).X / ActualWidth;
                double targetHorizontal = RestoreBounds.Width * percentHorizontal;

                double percentVertical = e.GetPosition(this).Y / ActualHeight;
                double targetVertical = RestoreBounds.Height * percentVertical;

                SwitchWindowState(WindowState.Normal);

                POINT lMousePosition;
                GetCursorPos(out lMousePosition);

                Left = lMousePosition.X - targetHorizontal;
                Top = lMousePosition.Y - targetVertical;

                DragMove();
            }
        }




        private void SwitchWindowState()
        {
            SwitchWindowState(WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized);
        }

        private void SwitchWindowState(WindowState state, bool onlyChangeUI = false)
        {
            if (!onlyChangeUI)
            {
                WindowState = state;
            }
            if (maximizeButton != null)
            {
                maximizeButton.PressedIconPath = maximizeButton.IconPath = state == WindowState.Maximized ? ICON_NORMAL : ICON_MAXIMIZE;
            }
            MainGridContainer.BorderThickness = state == WindowState.Maximized ? new Thickness(0.0) : new Thickness(0.5);
            MainGridContainer.CornerRadius = state == WindowState.Maximized ? new CornerRadius(0.0) : new CornerRadius(10.0);
        }
        #endregion Window Styling Functions
    }
}
