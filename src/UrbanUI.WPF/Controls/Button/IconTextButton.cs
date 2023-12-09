using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UrbanUI.WPF.Themes;

namespace UrbanUI.WPF.Controls
{
   [TemplatePart(Name = "PART_iconControl", Type = typeof(PathIconControl))]
   public partial class IconTextButton : Button, ITheme
   {
      #region Dependency Properties

      #region DP: IconPath
      public string IconPath
      {
         get { return (string)GetValue(IconPathProperty); }
         set { SetValue(IconPathProperty, value); }
      }
      public static readonly DependencyProperty IconPathProperty = DependencyProperty.Register(nameof(IconPath), typeof(string), typeof(IconTextButton), new PropertyMetadata(null));
      #endregion DP: IconPath

      #region DP: IconMargin
      public Thickness IconMargin
      {
         get { return (Thickness)GetValue(IconMarginProperty); }
         set { SetValue(IconMarginProperty, value); }
      }
      public static readonly DependencyProperty IconMarginProperty = DependencyProperty.Register(nameof(IconMargin), typeof(Thickness), typeof(IconTextButton), new PropertyMetadata(new Thickness(0)));
      #endregion DP: IconMargin

      #region DP: PressedIconPath
      public string PressedIconPath
      {
         get { return (string)GetValue(PressedIconPathProperty); }
         set { SetValue(PressedIconPathProperty, value); }
      }
      public static readonly DependencyProperty PressedIconPathProperty = DependencyProperty.Register(nameof(PressedIconPath), typeof(string), typeof(IconTextButton), new PropertyMetadata(null));
      #endregion DP: PressedIconPath

      #region DP: IconColor
      public Brush IconColor
      {
         get { return (Brush)GetValue(IconColorProperty); }
         set { SetValue(IconColorProperty, value); }
      }
      public static readonly DependencyProperty IconColorProperty = DependencyProperty.Register(nameof(IconColor), typeof(Brush), typeof(IconTextButton), new PropertyMetadata(Brushes.Green));
      #endregion DP:IconColor

      #region DP: PressedIconColor
      public Brush PressedIconColor
      {
         get { return (Brush)GetValue(PressedIconColorProperty); }
         set { SetValue(PressedIconColorProperty, value); }
      }

      public static readonly DependencyProperty PressedIconColorProperty = DependencyProperty.Register(nameof(PressedIconColor), typeof(Brush), typeof(IconTextButton), new PropertyMetadata(Brushes.Gray));
      #endregion DP:PressedIconColor

      #region DP: MouseEnterIconColor
      public Brush MouseEnterIconColor
      {
         get { return (Brush)GetValue(MouseEnterIconColorProperty); }
         set { SetValue(MouseEnterIconColorProperty, value); }
      }

      public static readonly DependencyProperty MouseEnterIconColorProperty = DependencyProperty.Register(nameof(MouseEnterIconColor), typeof(Brush), typeof(IconTextButton), new PropertyMetadata(Brushes.Black));
      #endregion DP: MouseEnterIconColor

      #region DP: DisabledIconColor
      public Brush DisabledIconColor
      {
         get { return (Brush)GetValue(DisabledIconColorProperty); }
         set { SetValue(DisabledIconColorProperty, value); }
      }
      public static readonly DependencyProperty DisabledIconColorProperty = DependencyProperty.Register(nameof(DisabledIconColor), typeof(Brush), typeof(IconTextButton), new PropertyMetadata(Brushes.LightGray));
      #endregion DP: MousePressedBackground

      #region DP: PathIconWidth
      public double PathIconWidth
      {
         get { return (double)GetValue(PathIconWidthProperty); }
         set { SetValue(PathIconWidthProperty, value); }
      }
      public static readonly DependencyProperty PathIconWidthProperty = DependencyProperty.Register(nameof(PathIconWidth), typeof(double), typeof(IconTextButton), new PropertyMetadata(12.0));
      #endregion DP: PathIconWidth

      #region DP: PathIconHeight
      public double PathIconHeight
      {
         get { return (double)GetValue(PathIconHeightProperty); }
         set { SetValue(PathIconHeightProperty, value); }
      }
      public static readonly DependencyProperty PathIconHeightProperty = DependencyProperty.Register(nameof(PathIconHeight), typeof(double), typeof(IconTextButton), new PropertyMetadata(12.0));
      #endregion DP: PathIconHeight

      #endregion Dependency Properties

      private Theme? _theme;

      private PathIconControl? _iconControl;

      private bool _templateApplied = false;
      private bool _manuallyTriggerUIEvents = false;
      public IconTextButton()
      {
         //DefaultStyleKeyProperty.OverrideMetadata(typeof(IconTextButton), new FrameworkPropertyMetadata(typeof(IconTextButton)));
      }



      #region On Apply Template
      public override void OnApplyTemplate()
      {
         _iconControl = GetTemplateChild("PART_iconControl") as PathIconControl;

         base.OnApplyTemplate();

         if (_iconControl != null)
         {
            _templateApplied = true;

            this.MouseEnter += delegate
            {
               if(_manuallyTriggerUIEvents)
               {
                  _iconControl.Color = this.MouseEnterIconColor;
               }
            };

            this.MouseLeave += delegate
            {
               if (_manuallyTriggerUIEvents)
               {
                  _iconControl.Color = this.IconColor;
               }
            };

            this.MouseLeftButtonDown += delegate
            {
               if (_manuallyTriggerUIEvents)
               {
                  _iconControl.Color = this.PressedIconColor;
                  _iconControl.Path = string.IsNullOrWhiteSpace(this.PressedIconPath) ? this.IconPath : this.PressedIconPath;
               }
            };

            this.MouseLeftButtonUp += delegate
            {
               if (_manuallyTriggerUIEvents)
               {
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
         base.EnableManualMouseTriggerUIEvents();
         _manuallyTriggerUIEvents = true;
      }
      internal void DisableManualMouseTriggerUIEvents()
      {
         base.DisableManualMouseTriggerUIEvents();
         _manuallyTriggerUIEvents = false;
      }
      #endregion  Manually Invoke IsMouseOverAndIsPressed
   }
}
