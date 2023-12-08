using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UrbanUI.WPF.Themes;

namespace UrbanUI.WPF.Controls
{
   [TemplatePart(Name = "PART_mainBorder", Type = typeof(Border))]
   [TemplatePart(Name = "PART_iconControl", Type = typeof(PathIconControl))]
   [TemplatePart(Name = "PART_contentHost", Type = typeof(ContentPresenter))]
   public partial class FlatButton : System.Windows.Controls.Button, ITheme
   {
      #region Dependency Properties

      #region DP: MouseEnterBackground
      public Brush MouseEnterBackground
      {
         get { return (Brush)GetValue(MouseEnterBackgroundProperty); }
         set { SetValue(MouseEnterBackgroundProperty, value); }
      }
      public static readonly DependencyProperty MouseEnterBackgroundProperty = DependencyProperty.Register(nameof(MouseEnterBackground), typeof(Brush), typeof(FlatButton), new PropertyMetadata(Brushes.Green));
      #endregion DP: MouseEnterBackground

      #region DP: MousePressedBackground
      public Brush MousePressedBackground
      {
         get { return (Brush)GetValue(MousePressedBackgroundProperty); }
         set { SetValue(MousePressedBackgroundProperty, value); }
      }
      public static readonly DependencyProperty MousePressedBackgroundProperty = DependencyProperty.Register(nameof(MousePressedBackground), typeof(Brush), typeof(FlatButton), new PropertyMetadata(Brushes.DarkGreen));
      #endregion DP: MousePressedBackground

      #region DP: MousePressedForeground
      public Brush MousePressedForeground
      {
         get { return (Brush)GetValue(MousePressedForegroundProperty); }
         set { SetValue(MousePressedForegroundProperty, value); }
      }
      public static readonly DependencyProperty MousePressedForegroundProperty = DependencyProperty.Register(nameof(MousePressedForeground), typeof(Brush), typeof(FlatButton), new PropertyMetadata(Brushes.White));
      #endregion DP: MousePressedForeground

      #region DP: DisabledBackground
      public Brush DisabledBackground
      {
         get { return (Brush)GetValue(DisabledBackgroundProperty); }
         set { SetValue(DisabledBackgroundProperty, value); }
      }
      public static readonly DependencyProperty DisabledBackgroundProperty = DependencyProperty.Register(nameof(DisabledBackground), typeof(Brush), typeof(FlatButton), new PropertyMetadata(Brushes.DarkGray));
      #endregion DP: MousePressedBackground

      #region DP: DisabledForeground
      public Brush DisabledForeground
      {
         get { return (Brush)GetValue(DisabledForegroundProperty); }
         set { SetValue(DisabledForegroundProperty, value); }
      }
      public static readonly DependencyProperty DisabledForegroundProperty = DependencyProperty.Register(nameof(DisabledForeground), typeof(Brush), typeof(FlatButton), new PropertyMetadata(Brushes.Gray));
      #endregion DP: MousePressedBackground

      #region DP: IconPath
      public string IconPath
      {
         get { return (string)GetValue(IconPathProperty); }
         set { SetValue(IconPathProperty, value); }
      }
      public static readonly DependencyProperty IconPathProperty = DependencyProperty.Register(nameof(IconPath), typeof(string), typeof(FlatButton), new PropertyMetadata(null));
      #endregion DP: IconPath

      #region DP: IconMargin
      public Thickness IconMargin
      {
         get { return (Thickness)GetValue(IconMarginProperty); }
         set { SetValue(IconMarginProperty, value); }
      }
      public static readonly DependencyProperty IconMarginProperty = DependencyProperty.Register(nameof(IconMargin), typeof(Thickness), typeof(FlatButton), new PropertyMetadata(new Thickness(0)));
      #endregion DP: IconMargin

      #region DP: PressedIconPath
      public string PressedIconPath
      {
         get { return (string)GetValue(PressedIconPathProperty); }
         set { SetValue(PressedIconPathProperty, value); }
      }
      public static readonly DependencyProperty PressedIconPathProperty = DependencyProperty.Register(nameof(PressedIconPath), typeof(string), typeof(FlatButton), new PropertyMetadata(null));
      #endregion DP: PressedIconPath

      #region DP: IconColor
      public Brush IconColor
      {
         get { return (Brush)GetValue(IconColorProperty); }
         set { SetValue(IconColorProperty, value); }
      }
      public static readonly DependencyProperty IconColorProperty = DependencyProperty.Register(nameof(IconColor), typeof(Brush), typeof(FlatButton), new PropertyMetadata(Brushes.Green));
      #endregion DP:IconColor

      #region DP: PressedIconColor
      public Brush PressedIconColor
      {
         get { return (Brush)GetValue(PressedIconColorProperty); }
         set { SetValue(PressedIconColorProperty, value); }
      }

      public static readonly DependencyProperty PressedIconColorProperty = DependencyProperty.Register(nameof(PressedIconColor), typeof(Brush), typeof(FlatButton), new PropertyMetadata(Brushes.Gray));
      #endregion DP:PressedIconColor

      #region DP: MouseEnterIconColor
      public Brush MouseEnterIconColor
      {
         get { return (Brush)GetValue(MouseEnterIconColorProperty); }
         set { SetValue(MouseEnterIconColorProperty, value); }
      }

      public static readonly DependencyProperty MouseEnterIconColorProperty = DependencyProperty.Register(nameof(MouseEnterIconColor), typeof(Brush), typeof(FlatButton), new PropertyMetadata(Brushes.Black));
      #endregion DP: MouseEnterIconColor

      #region DP: DisabledIconColor
      public Brush DisabledIconColor
      {
         get { return (Brush)GetValue(DisabledIconColorProperty); }
         set { SetValue(DisabledIconColorProperty, value); }
      }
      public static readonly DependencyProperty DisabledIconColorProperty = DependencyProperty.Register(nameof(DisabledIconColor), typeof(Brush), typeof(FlatButton), new PropertyMetadata(Brushes.LightGray));
      #endregion DP: MousePressedBackground

      #region DP: CornerRadius
      public CornerRadius CornerRadius
      {
         get { return (CornerRadius)GetValue(CornerRadiusProperty); }
         set { SetValue(CornerRadiusProperty, value); }
      }

      public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(FlatButton), new PropertyMetadata(new CornerRadius(0)));
      #endregion DP: CornerRadius

      #region DP: PathIconWidth
      public double PathIconWidth
      {
         get { return (double)GetValue(PathIconWidthProperty); }
         set { SetValue(PathIconWidthProperty, value); }
      }
      public static readonly DependencyProperty PathIconWidthProperty = DependencyProperty.Register(nameof(PathIconWidth), typeof(double), typeof(FlatButton), new PropertyMetadata(12.0));
      #endregion DP: PathIconWidth

      #region DP: PathIconHeight
      public double PathIconHeight
      {
         get { return (double)GetValue(PathIconHeightProperty); }
         set { SetValue(PathIconHeightProperty, value); }
      }
      public static readonly DependencyProperty PathIconHeightProperty = DependencyProperty.Register(nameof(PathIconHeight), typeof(double), typeof(FlatButton), new PropertyMetadata(12.0));
      #endregion DP: PathIconHeight

      #region DP: ButtonAppearance
      public ButtonAppearance? ButtonAppearance
      {
         get { return (ButtonAppearance)GetValue(ButtonAppearanceProperty); }
         set { SetValue(ButtonAppearanceProperty, value); }
      }
      public static readonly DependencyProperty ButtonAppearanceProperty = DependencyProperty.Register(nameof(ButtonAppearance), typeof(ButtonAppearance?), typeof(FlatButton), new PropertyMetadata(null));
      #endregion DP: ButtonAppearance

      #region DP: TextStyle
      public TextDecorationCollection TextStyle
      {
         get { return (TextDecorationCollection)GetValue(TextStyleProperty); }
         set { SetValue(TextStyleProperty, value); }
      }
      public static readonly DependencyProperty TextStyleProperty = DependencyProperty.Register(nameof(TextStyle), typeof(TextDecorationCollection), typeof(FlatButton), new PropertyMetadata(null));
      #endregion DP: TextStyle

      #endregion Dependency Properties

      private Theme? _theme;

      private Border? _mainBorder;
      private PathIconControl? _iconControl;
      private ContentPresenter? _contentHost;

      private bool _templateApplied = false;
      private bool _manuallyTriggerUIEvents = false;
      public FlatButton()
      {
         //DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatButton), new FrameworkPropertyMetadata(typeof(FlatButton)));
      }



      #region On Apply Template
      public override void OnApplyTemplate()
      {
         _mainBorder = GetTemplateChild("PART_mainBorder") as Border;
         _iconControl = GetTemplateChild("PART_iconControl") as PathIconControl;
         _contentHost = GetTemplateChild("PART_contentHost") as ContentPresenter;

         base.OnApplyTemplate();

         if (_mainBorder != null && _iconControl != null && _contentHost != null)
         {
            _templateApplied = true;

            this.MouseEnter += delegate
            {
               if(_manuallyTriggerUIEvents)
               {
                  _mainBorder.Background = this.MouseEnterBackground;
                  _iconControl.Color = this.MouseEnterIconColor;
               }
            };

            this.MouseLeave += delegate
            {
               if (_manuallyTriggerUIEvents)
               {
                  _mainBorder.Background = this.Background;
                  _iconControl.Color = this.IconColor;
               }
            };

            this.MouseLeftButtonDown += delegate
            {
               if (_manuallyTriggerUIEvents)
               {
                  _mainBorder.Background = this.MousePressedBackground;
                  _iconControl.Color = this.PressedIconColor;
                  _iconControl.Path = string.IsNullOrWhiteSpace(this.PressedIconPath) ? this.IconPath : this.PressedIconPath;
               }
            };

            this.MouseLeftButtonUp += delegate
            {
               if (_manuallyTriggerUIEvents)
               {
                  _mainBorder.Background = this.Background;
                  _iconControl.Color = this.IconColor;
                  _iconControl.Path = this.IconPath;
               }
            };

            if (_theme == null)
            {
               _theme = Defaults.getDefaultThemeSetups();
            }
            SetThemeUI(_theme);
         }
      }
      #endregion On Apply Template


      #region Set Theme
      public void SetThemeUI(Theme theme, bool UpdateContentThemes = false)
      {
         _theme = theme;
      }

      public Theme GetTheme()
      {
         return _theme;
      }
      #endregion Set Theme


      #region Manually Invoke IsMouseOver And IsPressed
      internal void EnableManualMouseTriggerUIEvents()
      {
         _manuallyTriggerUIEvents = true;
      }
      internal void DisableManualMouseTriggerUIEvents()
      {
         _manuallyTriggerUIEvents = false;
      }
      #endregion  Manually Invoke IsMouseOverAndIsPressed
   }
}
