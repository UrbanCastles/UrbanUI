using System.Windows.Controls;
using System.Windows.Input;
using UrbanUI.WPF.Themes;
using UrbanUI.WPF.Win32.Interop.Methods;
using Icon = UrbanUI.WPF.Themes.Icon;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Media;
using static UrbanUI.WPF.Win32.Interop.Structs.InteropStructs;
using System.Windows.Controls.Primitives;
using System.Windows;
using System;
using UrbanUI.WPF.Win32.Interop.Values;
using System.Windows.Shell;
using UrbanUI.WPF.Win32.WinApi;
using UrbanUI.WPF.Behaviors;

namespace UrbanUI.WPF.Controls
{
   [System.Windows.TemplatePart(Name = "PART_minimizeButton", Type = typeof(Button))]
   [System.Windows.TemplatePart(Name = "PART_maximizeButton", Type = typeof(Button))]
   [System.Windows.TemplatePart(Name = "PART_restoreButton", Type = typeof(Button))]
   [System.Windows.TemplatePart(Name = "PART_closeButton", Type = typeof(Button))]
   [System.Windows.TemplatePart(Name = "PART_dragGrid", Type = typeof(Grid))]
   [System.Windows.TemplatePart(Name = "PART_MainGridContainer", Type = typeof(Border))]
   [System.Windows.TemplatePart(Name = "PART_windowIcon", Type = typeof(System.Windows.Controls.Image))]
   [System.Windows.TemplatePart(Name = "PART_windowTitle", Type = typeof(TextBlock))]
   [System.Windows.TemplatePart(Name = "PART_ContentPresenter", Type = typeof(ContentPresenter))]
   [System.Windows.TemplatePart(Name = "PART_WindowResizeGrip", Type = typeof(ResizeGrip))]

   public partial class UrbanWindow : System.Windows.Window, ITheme
   {
      #region Initializations
      private bool mRestoreIfMove = false;
      private NonClientFrameEdges _defaultClientEdgeSetup = NonClientFrameEdges.None;
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

      static UrbanWindow()
      {
         DefaultStyleKeyProperty.OverrideMetadata(typeof(UrbanWindow), new FrameworkPropertyMetadata(typeof(UrbanWindow)));
      }

      public UrbanWindow()
      {
         #region Setups
         this.SetResourceReference(StyleProperty, typeof(UrbanWindow));
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

            var _watcher = new DependencyPropertyWatcher(this);
            _watcher.Watch(Control.BackgroundProperty, (o, e) =>
            {
               if(NativeWindowInterop.IseInitialized() && NativeWindowInterop.IsHookInitialized())
               {
                  NativeWindowInterop.SetNativeFrameColor(NativeWindowInterop.GetInstance().Hwnd, ((SolidColorBrush)this.Background).Color);
               }
            });
         };
         #endregion On Loaded Setups

         this.Closed += delegate
         {
            NativeWindowInterop.Dispose();
         };

         AppDomain.CurrentDomain.ProcessExit += (s, e) =>
         {
            NativeWindowInterop.Dispose();
         };
      }


      protected override void OnSourceInitialized(EventArgs e)
      {
         base.OnSourceInitialized(e);
         EnforceWindowAttributes();
         InitializeWindowChromeSetups();
         NativeWindowInterop.AddContextMenuHook(this, minimizeButton, maximizeButton, restoreButton, closeButton);
      }

      #region On Apply Template
      public override void OnApplyTemplate()
      {

         base.OnApplyTemplate();

         if (ReadLocalValue(BorderBrushProperty) == DependencyProperty.UnsetValue)
         {
            BorderBrush = Brushes.Transparent;
         }

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

         this.StateChanged += Window_StateChanged;

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

      private void Window_StateChanged(object sender, EventArgs e)
      {
         SetWindowButtonsStateVisibility();
      }

      private void Grid_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
      {
         this.OpenSystemContextMenu(e);
         e.Handled = true;
         return;
      }

      private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         if(this.WindowStyle != WindowStyle.None)
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
      }

      private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
      {
         mRestoreIfMove = false;
      }

      private void Grid_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
      {
         if (mRestoreIfMove && this.WindowStyle != WindowStyle.None)
         {
            mRestoreIfMove = false;

            double percentHorizontal = e.GetPosition(this).X / ActualWidth;
            double targetHorizontal = RestoreBounds.Width * percentHorizontal;

            double percentVertical = e.GetPosition(this).Y / ActualHeight;
            double targetVertical = RestoreBounds.Height * percentVertical;

            if(this.WindowState != WindowState.Normal)
               this.WindowState = WindowState.Normal;

            POINT lMousePosition;
            InteropMethods.GetCursorPos(out lMousePosition);

            Left = lMousePosition.X - targetHorizontal;
            Top = lMousePosition.Y - targetVertical;

            DragMove();
         }
      }


      private void SwitchWindowState()
      {
         this.WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
      }



      private void SetWindowButtonsStateVisibility()
      {
         var isNoneStyle = this.WindowStyle == WindowStyle.None;
         var isToolWindow = this.WindowStyle == WindowStyle.ToolWindow;

         if (maximizeButton != null && restoreButton != null && closeButton != null)
         {

            var canMinimize = this.ResizeMode != ResizeMode.NoResize;
            var canResize = this.ResizeMode == ResizeMode.CanResize || this.ResizeMode == ResizeMode.CanResizeWithGrip;

            minimizeButton.Visibility = canMinimize && !isNoneStyle && !isToolWindow ? Visibility.Visible : Visibility.Collapsed;

            maximizeButton.Visibility = canResize && this.WindowState != WindowState.Maximized && !isNoneStyle && !isToolWindow ? Visibility.Visible : Visibility.Collapsed;

            restoreButton.Visibility = canResize && this.WindowState == WindowState.Maximized && !isNoneStyle && !isToolWindow ? Visibility.Visible : Visibility.Collapsed;

            closeButton.Visibility = !isNoneStyle ? Visibility.Visible : Visibility.Collapsed;
         }

         if (MainGridContainer != null)
         {
            if (this.WindowState == WindowState.Maximized)
            {
               if (this.WindowStyle == WindowStyle.SingleBorderWindow || this.WindowStyle == WindowStyle.ThreeDBorderWindow)
               {
                  MainGridContainer.Padding = new Thickness(
                      SystemParameters.WorkArea.Left + 7,
                      SystemParameters.WorkArea.Top + 7,
                      (SystemParameters.PrimaryScreenWidth - SystemParameters.WorkArea.Right) + 7, 7);
               }
               else if (this.WindowStyle == WindowStyle.ToolWindow)
               {
                  MainGridContainer.Padding = new Thickness(0, 0, 0, 0);
               }
            }
            else
            {
               MainGridContainer.Padding = new Thickness(0, 0, 0, 0);
            }

            if (windowResizeGrip != null && this._internalTreatAsGrip)
            {
               windowResizeGrip.Visibility = (this.WindowState == WindowState.Maximized) ? Visibility.Collapsed : Visibility.Visible;
            }
         }
      }

      internal void UpdateTemplateCornerRadius()
      {
         if (MainGridContainer != null)
         {
            MainGridContainer.CornerRadius = NativeWindowInterop.GetSystemCornerRadius();
         }
      }

      private void InitializeWindowChromeSetups()
      {
         //Source: https://learn.microsoft.com/en-us/dotnet/api/system.windows.shell.windowchrome?view=windowsdesktop-8.0&redirectedfrom=MSDN
         //When GlassFrameThickness is set to a negative value for any side, its coerced value will be equal to GlassFrameCompleteThickness.
         //This fixes the issue that the Snap Layout is not showing when on Normal Mode

         var windowChrome = WindowChrome.GetWindowChrome(this);
         if (windowChrome != null)
         {
            windowChrome.GlassFrameThickness = new Thickness(-1);
            windowChrome.ResizeBorderThickness = this.ResizeMode == ResizeMode.NoResize ? default : new Thickness(8);
            
            windowChrome.CaptionHeight = 0;
            windowChrome.CornerRadius = default;
            windowChrome.UseAeroCaptionButtons = false;
            windowChrome.NonClientFrameEdges = NonClientFrameEdges.None;
         }
         else
         {
            windowChrome = new WindowChrome()
            {
               GlassFrameThickness = new Thickness(-1),
               ResizeBorderThickness = this.ResizeMode == ResizeMode.NoResize ? default : new Thickness(8),
               CaptionHeight = 0,
               CornerRadius = default,
               UseAeroCaptionButtons = false,
               NonClientFrameEdges = NonClientFrameEdges.None
            };
            WindowChrome.SetWindowChrome(this, windowChrome);
         }
      }
      #endregion Window Styling Functions


      #region Native Context Menu
      private void OpenSystemContextMenu(MouseButtonEventArgs e)
      {
         System.Windows.Point position = e.GetPosition(this);
         System.Windows.Point screen = PointToScreen(position);

         // Note: Both ShowSystemMenu and PointToScreen apply DPI scaling internally.
         // Using both can cause double scaling and incorrect menu positioning.
         screen.X = screen.X / InteropValues.DPI_SCALE;
         screen.Y = screen.Y / InteropValues.DPI_SCALE;

         SystemCommands.ShowSystemMenu(this, screen);
      }
      #endregion Native Context Menu
   }
}
