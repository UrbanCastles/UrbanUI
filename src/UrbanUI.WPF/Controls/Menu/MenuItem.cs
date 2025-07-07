
using System.Windows;
using System.Windows.Media;

namespace UrbanUI.WPF.Controls
{
   public partial class MenuItem : System.Windows.Controls.MenuItem
   {
      #region Dependency Properties

      #region DP: Background
      public new Brush Background
      {
         get { return (Brush)GetValue(BackgroundProperty); }
         set { SetValue(BackgroundProperty, value); }
      }
      public static new readonly DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(MenuItem), new PropertyMetadata(Brushes.Transparent));


      #endregion DP:Background

      #region DP: MouseEnterBackground
      public Brush MouseEnterBackground
      {
         get { return (Brush)GetValue(MouseEnterBackgroundProperty); }
         set { SetValue(MouseEnterBackgroundProperty, value); }
      }
      public static readonly DependencyProperty MouseEnterBackgroundProperty = DependencyProperty.Register(nameof(MouseEnterBackground), typeof(Brush), typeof(MenuItem), new PropertyMetadata((Brush)new BrushConverter().ConvertFromString("#106ebe")));
      #endregion DP: MouseEnterBackground

      #region DP: MousePressedBackground
      public Brush MousePressedBackground
      {
         get { return (Brush)GetValue(MousePressedBackgroundProperty); }
         set { SetValue(MousePressedBackgroundProperty, value); }
      }
      public static readonly DependencyProperty MousePressedBackgroundProperty = DependencyProperty.Register(nameof(MousePressedBackground), typeof(Brush), typeof(MenuItem), new PropertyMetadata((Brush)new BrushConverter().ConvertFromString("#005a9e")));
      #endregion DP: MousePressedBackground

      #region DP: CheckedBackground
      public Brush CheckedBackground
      {
         get { return (Brush)GetValue(CheckedBackgroundProperty); }
         set { SetValue(CheckedBackgroundProperty, value); }
      }
      public static readonly DependencyProperty CheckedBackgroundProperty = DependencyProperty.Register(nameof(CheckedBackground), typeof(Brush), typeof(MenuItem), new PropertyMetadata((Brush)new BrushConverter().ConvertFromString("#005a9e")));
      #endregion DP: CheckedBackground

      #region DP: DisabledBackground
      public Brush DisabledBackground
      {
         get { return (Brush)GetValue(DisabledBackgroundProperty); }
         set { SetValue(DisabledBackgroundProperty, value); }
      }
      public static readonly DependencyProperty DisabledBackgroundProperty = DependencyProperty.Register(nameof(DisabledBackground), typeof(Brush), typeof(MenuItem), new PropertyMetadata(Brushes.Transparent));
      #endregion DP: MousePressedBackground

      #region DP: Foreground
      public new Brush Foreground
      {
         get { return (Brush)GetValue(ForegroundProperty); }
         set { SetValue(ForegroundProperty, value); }
      }
      public static new readonly DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(Brush), typeof(MenuItem), new PropertyMetadata(Brushes.White));
      #endregion DP:Foreground

      #region DP: MouseEnterForeground
      public Brush MouseEnterForeground
      {
         get { return (Brush)GetValue(MouseEnterForegroundProperty); }
         set { SetValue(MouseEnterForegroundProperty, value); }
      }
      public static readonly DependencyProperty MouseEnterForegroundProperty = DependencyProperty.Register(nameof(MouseEnterForeground), typeof(Brush), typeof(MenuItem), new PropertyMetadata(Brushes.White));
      #endregion DP: MouseEnterForeground

      #region DP: MousePressedForeground
      public Brush MousePressedForeground
      {
         get { return (Brush)GetValue(MousePressedForegroundProperty); }
         set { SetValue(MousePressedForegroundProperty, value); }
      }
      public static readonly DependencyProperty MousePressedForegroundProperty = DependencyProperty.Register(nameof(MousePressedForeground), typeof(Brush), typeof(MenuItem), new PropertyMetadata(Brushes.White));
      #endregion DP: MousePressedForeground

      #region DP: CheckedForeground
      public Brush CheckedForeground
      {
         get { return (Brush)GetValue(CheckedForegroundProperty); }
         set { SetValue(CheckedForegroundProperty, value); }
      }
      public static readonly DependencyProperty CheckedForegroundProperty = DependencyProperty.Register(nameof(CheckedForeground), typeof(Brush), typeof(MenuItem), new PropertyMetadata(Brushes.White));
      #endregion DP: CheckedForeground

      #region DP: DisabledForeground
      public Brush DisabledForeground
      {
         get { return (Brush)GetValue(DisabledForegroundProperty); }
         set { SetValue(DisabledForegroundProperty, value); }
      }
      public static readonly DependencyProperty DisabledForegroundProperty = DependencyProperty.Register(nameof(DisabledForeground), typeof(Brush), typeof(MenuItem), new PropertyMetadata(Brushes.LightGray));
      #endregion DP: MousePressedBackground

      #region DP: IconColor
      public Brush IconColor
      {
         get { return (Brush)GetValue(IconColorProperty); }
         set { SetValue(IconColorProperty, value); }
      }
      public static readonly DependencyProperty IconColorProperty = DependencyProperty.Register(nameof(IconColor), typeof(Brush), typeof(MenuItem), new PropertyMetadata(Brushes.White));
      #endregion DP:IconColor

      #region DP: MouseEnterIconColor
      public Brush MouseEnterIconColor
      {
         get { return (Brush)GetValue(MouseEnterIconColorProperty); }
         set { SetValue(MouseEnterIconColorProperty, value); }
      }

      public static readonly DependencyProperty MouseEnterIconColorProperty = DependencyProperty.Register(nameof(MouseEnterIconColor), typeof(Brush), typeof(MenuItem), new PropertyMetadata(Brushes.White));
      #endregion DP: MouseEnterIconColor

      #region DP: PressedIconColor
      public Brush PressedIconColor
      {
         get { return (Brush)GetValue(PressedIconColorProperty); }
         set { SetValue(PressedIconColorProperty, value); }
      }

      public static readonly DependencyProperty PressedIconColorProperty = DependencyProperty.Register(nameof(PressedIconColor), typeof(Brush), typeof(MenuItem), new PropertyMetadata(Brushes.Gray));
      #endregion DP:PressedIconColor

      #region DP: CheckedIconColor
      public Brush CheckedIconColor
      {
         get { return (Brush)GetValue(CheckedIconColorProperty); }
         set { SetValue(CheckedIconColorProperty, value); }
      }

      public static readonly DependencyProperty CheckedIconColorProperty = DependencyProperty.Register(nameof(CheckedIconColor), typeof(Brush), typeof(MenuItem), new PropertyMetadata(Brushes.White));
      #endregion DP:CheckedIconColor

      #region DP: DisabledIconColor
      public Brush DisabledIconColor
      {
         get { return (Brush)GetValue(DisabledIconColorProperty); }
         set { SetValue(DisabledIconColorProperty, value); }
      }
      public static readonly DependencyProperty DisabledIconColorProperty = DependencyProperty.Register(nameof(DisabledIconColor), typeof(Brush), typeof(MenuItem), new PropertyMetadata(Brushes.LightGray));
      #endregion DP: MousePressedBackground

      #region DP: FocusMarkerColor
      public Brush FocusMarkerColor
      {
         get { return (Brush)GetValue(FocusMarkerColorProperty); }
         set { SetValue(FocusMarkerColorProperty, value); }
      }
      public static readonly DependencyProperty FocusMarkerColorProperty = DependencyProperty.Register(nameof(FocusMarkerColor), typeof(Brush), typeof(MenuItem), new PropertyMetadata((Brush)new BrushConverter().ConvertFromString("#3bd4f8")));
      #endregion DP: FocusMarkerColor

      #region DP: IconPath
      public Geometry IconPath
      {
         get { return (Geometry)GetValue(IconPathProperty); }
         set { SetValue(IconPathProperty, value); }
      }
      public static readonly DependencyProperty IconPathProperty = DependencyProperty.Register(nameof(IconPath), typeof(Geometry), typeof(MenuItem), new PropertyMetadata(null));
      #endregion DP: IconPath

      #region DP: PressedIconPath
      public Geometry PressedIconPath
      {
         get { return (Geometry)GetValue(PressedIconPathProperty); }
         set { SetValue(PressedIconPathProperty, value); }
      }
      public static readonly DependencyProperty PressedIconPathProperty = DependencyProperty.Register(nameof(PressedIconPath), typeof(Geometry), typeof(MenuItem), new PropertyMetadata(null));
      #endregion DP: PressedIconPath

      #region DP: CheckedIconPath
      public Geometry CheckedIconPath
      {
         get { return (Geometry)GetValue(CheckedIconPathProperty); }
         set { SetValue(CheckedIconPathProperty, value); }
      }

      public static readonly DependencyProperty CheckedIconPathProperty = DependencyProperty.Register(nameof(CheckedIconPath), typeof(Geometry), typeof(MenuItem), new PropertyMetadata(null));
      #endregion DP:CheckedIconPath

      #region DP: CornerRadius
      public CornerRadius CornerRadius
      {
         get { return (CornerRadius)GetValue(CornerRadiusProperty); }
         set { SetValue(CornerRadiusProperty, value); }
      }

      public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(MenuItem), new PropertyMetadata(new CornerRadius(0)));
      #endregion DP: CornerRadius

      #region DP: TextStyle
      public TextDecorationCollection TextStyle
      {
         get { return (TextDecorationCollection)GetValue(TextStyleProperty); }
         set { SetValue(TextStyleProperty, value); }
      }
      public static readonly DependencyProperty TextStyleProperty = DependencyProperty.Register(nameof(TextStyle), typeof(TextDecorationCollection), typeof(MenuItem), new PropertyMetadata(null));
      #endregion DP: TextStyle

      #region DP: IconMargin
      public Thickness IconMargin
      {
         get { return (Thickness)GetValue(IconMarginProperty); }
         set { SetValue(IconMarginProperty, value); }
      }
      public static readonly DependencyProperty IconMarginProperty = DependencyProperty.Register(nameof(IconMargin), typeof(Thickness), typeof(MenuItem), new PropertyMetadata(new Thickness(4)));
      #endregion DP: IconMargin

      #region DP: HideIconWhenNull
      public bool HideIconWhenNull
      {
         get { return (bool)GetValue(HideIconWhenNullProperty); }
         set { SetValue(HideIconWhenNullProperty, value); }
      }
      public static readonly DependencyProperty HideIconWhenNullProperty = DependencyProperty.Register(nameof(HideIconWhenNull), typeof(bool), typeof(MenuItem), new PropertyMetadata(true));
      #endregion DP: HideIconWhenNull

      #region DP: PathIconWidth
      public double PathIconWidth
      {
         get { return (double)GetValue(PathIconWidthProperty); }
         set { SetValue(PathIconWidthProperty, value); }
      }
      public static readonly DependencyProperty PathIconWidthProperty = DependencyProperty.Register(nameof(PathIconWidth), typeof(double), typeof(MenuItem), new PropertyMetadata(12.0));
      #endregion DP: PathIconWidth

      #region DP: PathIconHeight
      public double PathIconHeight
      {
         get { return (double)GetValue(PathIconHeightProperty); }
         set { SetValue(PathIconHeightProperty, value); }
      }
      public static readonly DependencyProperty PathIconHeightProperty = DependencyProperty.Register(nameof(PathIconHeight), typeof(double), typeof(MenuItem), new PropertyMetadata(12.0));
      #endregion DP: PathIconHeight

      #region DP: FocusMarkerCorderRadius
      public CornerRadius FocusMarkerCorderRadius
      {
         get { return (CornerRadius)GetValue(FocusMarkerCorderRadiusProperty); }
         set { SetValue(FocusMarkerCorderRadiusProperty, value); }
      }
      public static readonly DependencyProperty FocusMarkerCorderRadiusProperty = DependencyProperty.Register(nameof(FocusMarkerCorderRadius), typeof(CornerRadius), typeof(MenuItem), new PropertyMetadata(new CornerRadius(4)));
      #endregion DP: FocusMarkerCorderRadius

      #region DP: ShowTopLevelHeaderArrow
      public bool ShowTopLevelHeaderArrow
      {
         get { return (bool)GetValue(ShowTopLevelHeaderArrowProperty); }
         set { SetValue(ShowTopLevelHeaderArrowProperty, value); }
      }
      public static readonly DependencyProperty ShowTopLevelHeaderArrowProperty = DependencyProperty.Register(nameof(ShowTopLevelHeaderArrow), typeof(bool), typeof(MenuItem), new PropertyMetadata(true));
      #endregion DP: ShowTopLevelHeaderArrow

      #region DP: PopupBackground
      public Brush PopupBackground
      {
         get { return (Brush)GetValue(PopupBackgroundProperty); }
         set { SetValue(PopupBackgroundProperty, value); }
      }
      public static readonly DependencyProperty PopupBackgroundProperty = DependencyProperty.Register(nameof(PopupBackground), typeof(Brush), typeof(MenuItem), new PropertyMetadata(Brushes.White));
      #endregion DP:

      #region DP: PopupBorderBrush
      public Brush PopupBorderBrush
      {
         get { return (Brush)GetValue(PopupBorderBrushProperty); }
         set { SetValue(PopupBorderBrushProperty, value); }
      }
      public static readonly DependencyProperty PopupBorderBrushProperty = DependencyProperty.Register(nameof(PopupBorderBrush), typeof(Brush), typeof(MenuItem), new PropertyMetadata((Brush)new BrushConverter().ConvertFromString("#ECECEC")));
      #endregion DP:PopupBorderBrush

      #region DP: PopupBorderThickness
      public Thickness PopupBorderThickness
      {
         get { return (Thickness)GetValue(PopupBorderThicknessProperty); }
         set { SetValue(PopupBorderThicknessProperty, value); }
      }
      public static readonly DependencyProperty PopupBorderThicknessProperty = DependencyProperty.Register(nameof(PopupBorderThickness), typeof(Thickness), typeof(MenuItem), new PropertyMetadata(new Thickness(1)));
      #endregion DP:PopupBorderBrush

      #region DP: PopupCornerRadius
      public CornerRadius PopupCornerRadius
      {
         get { return (CornerRadius)GetValue(PopupCornerRadiusProperty); }
         set { SetValue(PopupCornerRadiusProperty, value); }
      }
      public static readonly DependencyProperty PopupCornerRadiusProperty = DependencyProperty.Register(nameof(PopupCornerRadius), typeof(CornerRadius), typeof(MenuItem), new PropertyMetadata(new CornerRadius(2)));
      #endregion DP:PopupCornerRadius

      #region DP: CheckedType
      public MenuItemCheckedType CheckedType
      {
         get { return (MenuItemCheckedType)GetValue(CheckedTypeProperty); }
         set { SetValue(CheckedTypeProperty, value); }
      }
      public static readonly DependencyProperty CheckedTypeProperty = DependencyProperty.Register(nameof(CheckedType), typeof(MenuItemCheckedType), typeof(MenuItem), new PropertyMetadata(MenuItemCheckedType.Check));
      #endregion DP:CheckedType

      #region DP: HoldUncheckingWhenGrouped
      public bool HoldUncheckingWhenGrouped
      {
         get { return (bool)GetValue(HoldUncheckingWhenGroupedProperty); }
         set { SetValue(HoldUncheckingWhenGroupedProperty, value); }
      }
      public static readonly DependencyProperty HoldUncheckingWhenGroupedProperty = DependencyProperty.Register(nameof(HoldUncheckingWhenGrouped), typeof(bool), typeof(MenuItem), new PropertyMetadata(false));
      #endregion DP: HoldUncheckingWhenGrouped

      #endregion Dependency Properties

      public MenuItem() { }



      public override void OnApplyTemplate()
      {
         base.OnApplyTemplate();
         this.CheckedIconPath = this.CheckedIconPath ?? this.IconPath;
      }

   }
}