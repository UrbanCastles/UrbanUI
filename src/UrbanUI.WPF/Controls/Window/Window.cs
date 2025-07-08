using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using UrbanUI.WPF.Themes;
using UrbanUI.WPF.Win32;
using UrbanUI.WPF.Win32.Interop.Methods;
using Icon = UrbanUI.WPF.Themes.Icon;
using System.Windows.Media.Imaging;
using System.IO;
using UrbanUI.WPF.Win32.WinBUttonContext;
using System.Windows.Media;
using static UrbanUI.WPF.Win32.Interop.Structs.InteropStructs;
using System.Windows.Controls.Primitives;

namespace UrbanUI.WPF.Controls
{
   [TemplatePart(Name = "PART_minimizeButton", Type = typeof(Button))]
   [TemplatePart(Name = "PART_maximizeButton", Type = typeof(Button))]
   [TemplatePart(Name = "PART_restoreButton", Type = typeof(Button))]
   [TemplatePart(Name = "PART_closeButton", Type = typeof(Button))]
   [TemplatePart(Name = "PART_dragGrid", Type = typeof(Grid))]
   [TemplatePart(Name = "PART_MainGridContainer", Type = typeof(Border))]
   [TemplatePart(Name = "PART_windowIcon", Type = typeof(System.Windows.Controls.Image))]
   [TemplatePart(Name = "PART_windowTitle", Type = typeof(TextBlock))]
   [TemplatePart(Name = "PART_ContentPresenter", Type = typeof(ContentPresenter))]
   [TemplatePart(Name = "PART_WindowResizeGrip", Type = typeof(ResizeGrip))]

   public partial class Window : System.Windows.Window, ITheme
   {
      #region Initializations
      private bool mRestoreIfMove = false;
      private Theme _theme;

      internal Button minimizeButton, maximizeButton, restoreButton, closeButton;
      private ResizeGrip windowResizeGrip;
      private Grid dragGrid;
      private Border MainGridContainer;
      private System.Windows.Controls.Image windowIcon;
      private TextBlock windowTitle;
      private ContentPresenter contentPresenter;
      private bool _templateApplied = false;
      private bool _internalTreatAsGrip = false;
      #endregion Initializations


      public Window()
      {
         #region Setups
         DefaultStyleKeyProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata(typeof(Window)));

         double currentDPIScaleFactor =
                (double)SystemDPI.GetCurrentDPIScaleFactor();
         //Screen screen = Screen.FromHandle((new WindowInteropHelper(this)).Handle);

         Rect workingArea = ScreenHelper.GetWorkingArea(this);
         //base.MaxHeight = (double)(workingArea.Height + 16) / currentDPIScaleFactor;
         #endregion Setups

         #region On Loaded Setups
         this.Loaded += delegate
         {
            if (this.Icon == null)
            {
               try
               {
                  Stream iconStream = System.Windows.Application.GetResourceStream(new Uri("/UrbanUI.WPF;component/Themes/default_icon.ico", UriKind.Relative)).Stream;
                  BitmapImage iconBitmap = new BitmapImage();
                  iconBitmap.BeginInit();
                  iconBitmap.StreamSource = iconStream;
                  iconBitmap.EndInit();
                  this.Icon = iconBitmap;
               }
               catch { }
            }
         };

         #endregion On Loaded Setups
      }


      protected override void OnSourceInitialized(EventArgs e)
      {
         EnforceWindowAttributes();

         base.OnSourceInitialized(e);
         HTButtonSettings.AddContextMenuHook(this, minimizeButton, maximizeButton, restoreButton, closeButton);

      }

      #region On Apply Template
      public override void OnApplyTemplate()
      {
         windowResizeGrip = GetTemplateChild("PART_WindowResizeGrip") as ResizeGrip;
         if (windowResizeGrip != null)
            windowResizeGrip.Visibility = _internalTreatAsGrip ? Visibility.Visible : Visibility.Collapsed;

         minimizeButton = GetTemplateChild("PART_minimizeButton") as Button;
         if (minimizeButton != null)
            minimizeButton.Click += MinimizeButton_Click;

         maximizeButton = GetTemplateChild("PART_maximizeButton") as Button;
         if (maximizeButton != null)
            maximizeButton.Click += MaximizeAndRestoreButton_Click;

         restoreButton = GetTemplateChild("PART_restoreButton") as Button;
         if (restoreButton != null)
            restoreButton.Click += MaximizeAndRestoreButton_Click;

         closeButton = GetTemplateChild("PART_closeButton") as Button;
         if (closeButton != null)
            closeButton.Click += CloseButton_Click;

         dragGrid = GetTemplateChild("PART_dragGrid") as Grid;
         if (dragGrid != null)
         {
            dragGrid.MouseLeftButtonDown += Grid_MouseLeftButtonDown;
            dragGrid.MouseLeftButtonUp += Grid_MouseLeftButtonUp;
            dragGrid.MouseRightButtonUp += Grid_MouseRightButtonUp;
            dragGrid.MouseMove += Grid_MouseMove;
         }

         MainGridContainer = GetTemplateChild("PART_MainGridContainer") as Border;
         windowIcon = GetTemplateChild("PART_windowIcon") as System.Windows.Controls.Image;
         windowTitle = GetTemplateChild("PART_windowTitle") as TextBlock;
         contentPresenter = GetTemplateChild("PART_ContentPresenter") as ContentPresenter;

         this.SizeChanged += Window_SizeChanged;
         this.StateChanged += Window_StateChanged;

         base.OnApplyTemplate();

         if (minimizeButton != null && maximizeButton != null && restoreButton != null && closeButton != null)
         {
            _templateApplied = true;
            SetWindowButtonsStateVisibility();
            AddPressedAndHoveredFunctions();

            if (_theme == null)
            {
               _theme = Defaults.getDefaultThemeSetups();
            }
            SetThemeUI(_theme);
         }
      }
      #endregion On Apply Template


      #region On Initialized
      protected override void OnInitialized(EventArgs e)
      {
         base.OnInitialized(e);
      }
      #endregion On Initialized


      #region Window Attribute Forcing
      private void EnforceWindowAttributes()
      {
         if (this.ResizeMode == ResizeMode.CanResizeWithGrip) //to avoid my control template to not work
         {
            _internalTreatAsGrip = true;
            this.ResizeMode = ResizeMode.CanResize; // force template to be used
         }

         if (this.WindowStyle != WindowStyle.SingleBorderWindow) //controls what users can only set for this 
         {
            if (this.WindowStyle == WindowStyle.None)
               this.WindowStyle = WindowStyle.None; //retain
            else
               this.WindowStyle = WindowStyle.SingleBorderWindow;
         }
      }
      #endregion Window Attribute Forcing


      #region Manual Hover and Pressed Triggering
      //Due to blockage of HwndSourceHook, manually adding hover and press functions for buttons here
      private void AddPressedAndHoveredFunctions()
      {
         minimizeButton?.EnableManualMouseTriggerUIEvents();
         maximizeButton?.EnableManualMouseTriggerUIEvents();
         restoreButton?.EnableManualMouseTriggerUIEvents();
         closeButton?.EnableManualMouseTriggerUIEvents();
      }
      #endregion Manual Hover and Pressed Triggering


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
         if (_templateApplied && theme != null)
         {
            ChangeWindowButtonColor(minimizeButton, theme, null);

            ChangeWindowButtonColor(maximizeButton, theme, null);

            ChangeWindowButtonColor(restoreButton, theme, null);

            ChangeWindowButtonColor(closeButton, theme, null);

            closeButton.MouseEnterBackground = System.Windows.Media.Brushes.Red;

            //windowIcon.Color = theme.MenuFocusIconColor;
            windowTitle.Foreground = theme.ThemeForeground;
            base.Background = MainGridContainer.Background = theme.ThemeBackground;
         }
      }

      private void ChangeWindowButtonColor(Button winButton, Theme theme, Icon icon)
      {
         if (theme == null || winButton == null)
         {
            return;
         }

         winButton.Background = System.Windows.Media.Brushes.Transparent;
         winButton.MouseEnterBackground = theme.MenuHighlightBackground;
         winButton.MousePressedBackground = theme.MenuFocusBackground;
         winButton.MouseEnterIconColor = theme.MenuForeground;
         winButton.IconColor = theme.MenuForeground;
         winButton.PressedIconColor = winButton.IconColor;

         if (icon != null)
         {
            winButton.IconPath = Geometry.Parse(icon?.StrokePath);
         }
      }
      #endregion Change Theme

      #endregion Themeing


      #region Window Styling Functions
      private void MinimizeButton_Click(object sender, RoutedEventArgs e)
      {
         WindowState = WindowState.Minimized;
      }

      private void MaximizeAndRestoreButton_Click(object sender, RoutedEventArgs e)
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
            SetWindowState(WindowState, true);
            this.BorderThickness = new Thickness(20);
         }
         else
         {
            SetWindowState(WindowState, true);
            this.BorderThickness = new Thickness(0);
         }
      }

      private void Window_StateChanged(object sender, EventArgs e)
      {
         if (maximizeButton != null && restoreButton != null)
         {
            SetWindowButtonsStateVisibility();
         }
      }

      private void Grid_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
      {
         this.OpenSystemContextMenu(e);
         e.Handled = true;
         return;
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

      private void Grid_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
      {
         if (mRestoreIfMove)
         {
            mRestoreIfMove = false;

            double percentHorizontal = e.GetPosition(this).X / ActualWidth;
            double targetHorizontal = RestoreBounds.Width * percentHorizontal;

            double percentVertical = e.GetPosition(this).Y / ActualHeight;
            double targetVertical = RestoreBounds.Height * percentVertical;

            SetWindowState(WindowState.Normal);

            POINT lMousePosition;
            InteropMethods.GetCursorPos(out lMousePosition);

            Left = lMousePosition.X - targetHorizontal;
            Top = lMousePosition.Y - targetVertical;

            DragMove();
         }
      }


      private void SwitchWindowState()
      {
         SetWindowState(WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized);
      }

      private void SetWindowState(WindowState state, bool onlyChangeUI = false)
      {
         Thickness thickness = new Thickness(0);

         if (!onlyChangeUI)
         {
            WindowState = state;
         }

         if (MainGridContainer == null) return;

         if (this.WindowState == WindowState.Maximized)
         {
            MainGridContainer.Padding = new Thickness(
                SystemParameters.WorkArea.Left + 7,
                SystemParameters.WorkArea.Top + 7,
                (SystemParameters.PrimaryScreenWidth - SystemParameters.WorkArea.Right) + 7, 7);
         }
         else
         {
            MainGridContainer.Padding = new Thickness(0, 0, 0, 0);
         }

         if(windowResizeGrip != null && this._internalTreatAsGrip)
         {
            windowResizeGrip.Visibility = (this.WindowState == WindowState.Maximized) ? Visibility.Collapsed : Visibility.Visible;
         }
      }

      private void SetWindowButtonsStateVisibility()
      {
         bool canResize = (this.ResizeMode == ResizeMode.CanResize || this.ResizeMode == ResizeMode.CanResizeWithGrip);
         restoreButton.Visibility = canResize && (this.WindowState == WindowState.Maximized) ? Visibility.Visible : Visibility.Collapsed;
         maximizeButton.Visibility = canResize && (this.WindowState == WindowState.Normal || this.WindowState == WindowState.Minimized) ? Visibility.Visible : Visibility.Collapsed;

         minimizeButton.Visibility = (this.ResizeMode != ResizeMode.NoResize) ? Visibility.Visible : Visibility.Collapsed;
      }
      #endregion Window Styling Functions


      #region Native Context Menu
      private void OpenSystemContextMenu(MouseButtonEventArgs e)
      {
         System.Windows.Point position = e.GetPosition(this);
         System.Windows.Point screen = base.PointToScreen(position);
         SystemCommands.ShowSystemMenu(this, screen);
      }
      #endregion Native Context Menu
   }
}
