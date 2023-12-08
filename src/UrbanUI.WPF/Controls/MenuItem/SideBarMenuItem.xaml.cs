using UrbanUI.WPF.Themes;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace UrbanUI.WPF.Controls
{
   /// <summary>
   /// Interaction logic for SideBarMenuItem.xaml
   /// </summary>
   public partial class SideBarMenuItem : UserControl, ITheme
   {
      private Theme _theme;

      #region Custom Properties
      private bool IsMouseEntered { get; set; } = false;
      public bool IsSubMenuItem { get; set; } = false;
      public bool IsActiveFocusableMenu { get; set; } = true;
      public bool MenuTriggerFocusAlreadyAdded { get; set; } = false;
      public bool IsToggling { get; set; } = false;
      public bool DisableClickEvents { get; set; } = false;
      #endregion Custom Properties 

      #region Dependency Properties

      #region DP: ShowMarker
      public bool ShowMarker
      {
         get { return (bool)GetValue(ShowMarkerProperty); }
         set { SetValue(ShowMarkerProperty, value); }
      }

      // Using a DependencyProperty as the backing store for IsActiveSelected.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty ShowMarkerProperty =
          DependencyProperty.Register("ShowMarker", typeof(bool), typeof(SideBarMenuItem), new PropertyMetadata(true, new PropertyChangedCallback((d, t) =>
          {
             var sidebarMenuTtem = (SideBarMenuItem)d;
             sidebarMenuTtem.selectionMarkerBorder.Visibility = sidebarMenuTtem.ShowMarker ? Visibility.Visible : Visibility.Collapsed;
          })));
      #endregion DP: ShowMarker

      #region DP: IsActiveSelected
      public bool IsActiveSelected
      {
         get { return (bool)GetValue(IsActiveSelectedProperty); }
         set { SetValue(IsActiveSelectedProperty, value); }
      }

      // Using a DependencyProperty as the backing store for IsActiveSelected.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty IsActiveSelectedProperty =
          DependencyProperty.Register("IsActiveSelected", typeof(bool), typeof(SideBarMenuItem), new PropertyMetadata(false, new PropertyChangedCallback((d, t) =>
          {
             var sidebarMenuTtem = (SideBarMenuItem)d;

             if(sidebarMenuTtem.ShowMarker)
             {
                if (sidebarMenuTtem.IsActiveSelected)
                   sidebarMenuTtem.selectionMarkerBorder.ToggleOn();
                else
                   sidebarMenuTtem.selectionMarkerBorder.ToggleOff();
             }

             sidebarMenuTtem.mainBorder.Background = sidebarMenuTtem.IsActiveSelected ? sidebarMenuTtem.MouseHightlightColor : sidebarMenuTtem.ItemBackground;
             sidebarMenuTtem.SetIconDetails(sidebarMenuTtem.IsActiveSelected ? sidebarMenuTtem.FocusedIconPath : sidebarMenuTtem.IconPath,
                                            sidebarMenuTtem.IsActiveSelected ? sidebarMenuTtem.FocusIconColor : sidebarMenuTtem.IconColor);
             sidebarMenuTtem.menuTextBlock.Foreground = sidebarMenuTtem.IsActiveSelected ? sidebarMenuTtem.FocusTextColor : sidebarMenuTtem.TextForeground;

             sidebarMenuTtem.RaiseCustomEvent();
          })));
      #endregion DP: IsActiveSelected

      #region DP: FocusMarkerColor
      public Brush FocusMarkerColor
      {
         get { return (Brush)GetValue(FocusMarkerColorProperty); }
         set { SetValue(FocusMarkerColorProperty, value); }
      }

      // Using a DependencyProperty as the backing store for FocusMarkerColor.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty FocusMarkerColorProperty =
          DependencyProperty.Register("FocusMarkerColor", typeof(Brush), typeof(SideBarMenuItem), new PropertyMetadata(Brushes.Green, new PropertyChangedCallback((d, t) =>
          {
             var sidebarMenuItem = (SideBarMenuItem)d;

          })));
      #endregion DP: FocusMarkerColor

      #region DP: FocusMarkerCorderRadius
      public CornerRadius FocusMarkerCorderRadius
      {
         get { return (CornerRadius)GetValue(FocusMarkerCorderRadiusProperty); }
         set { SetValue(FocusMarkerCorderRadiusProperty, value); }
      }

      // Using a DependencyProperty as the backing store for FocusMarkerCorderRadius.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty FocusMarkerCorderRadiusProperty =
          DependencyProperty.Register("FocusMarkerCorderRadius", typeof(CornerRadius), typeof(SideBarMenuItem), new PropertyMetadata(new CornerRadius(4)));


      #endregion DP: FocusMarkerCorderRadius

      #region DP: IconPath
      public string IconPath
      {
         get { return (string)GetValue(IconPathProperty); }
         set { SetValue(IconPathProperty, value); }
      }
      public static readonly DependencyProperty IconPathProperty =
            DependencyProperty.Register("IconPath", typeof(string), typeof(SideBarMenuItem), new PropertyMetadata(string.Empty,
               new PropertyChangedCallback((d, t) =>
               {
                  var sideBarMenuItem = (SideBarMenuItem)d;
                  sideBarMenuItem.SetIconDetails();
               })));
      #endregion DP: IconPath

      #region DP: FocusedIconPath
      public string FocusedIconPath
      {
         get { return (string)GetValue(FocusedIconPathProperty); }
         set { SetValue(FocusedIconPathProperty, value); }
      }
      public static readonly DependencyProperty FocusedIconPathProperty =
            DependencyProperty.Register("FocusedIconPath", typeof(string), typeof(SideBarMenuItem), new PropertyMetadata(string.Empty,
               new PropertyChangedCallback((d, t) =>
               {
                  var sideBarMenuItem = (SideBarMenuItem)d;
               })));
      #endregion DP: FocusedIconPath

      #region DP: IconColor
      public Brush IconColor
      {
         get { return (Brush)GetValue(IconColorProperty); }
         set { SetValue(IconColorProperty, value); }
      }

      // Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty IconColorProperty =
          DependencyProperty.Register("IconColor", typeof(Brush), typeof(SideBarMenuItem), new PropertyMetadata(Brushes.Green, new PropertyChangedCallback((d, t) =>
          {
             var sideBarMenuItem = (SideBarMenuItem)d;
             sideBarMenuItem.SetIconDetails();
          })));


      #endregion DP:IconColor

      #region DP: FocusIconColor
      public Brush FocusIconColor
      {
         get { return (Brush)GetValue(FocusIconColorProperty); }
         set { SetValue(FocusIconColorProperty, value); }
      }

      public static readonly DependencyProperty FocusIconColorProperty =
          DependencyProperty.Register("FocusIconColor", typeof(Brush), typeof(SideBarMenuItem), new PropertyMetadata(Brushes.Gray, new PropertyChangedCallback((d, t) =>
          {
             var sideBarMenuItem = (SideBarMenuItem)d;
          })));


      #endregion DP:FocusIconColor

      #region DP: MouseHightlightColor
      public Brush MouseHightlightColor
      {
         get { return (Brush)GetValue(MouseHightlightColorProperty); }
         set { SetValue(MouseHightlightColorProperty, value); }
      }

      public static readonly DependencyProperty MouseHightlightColorProperty = DependencyProperty.Register("MouseHightlightColor", typeof(Brush), typeof(SideBarMenuItem), new PropertyMetadata(Brushes.LightGray));
      #endregion DP: MouseHightlightColor

      #region DP: IconHightlightColor
      public Brush IconHightlightColor
      {
         get { return (Brush)GetValue(IconHightlightColorProperty); }
         set { SetValue(IconHightlightColorProperty, value); }
      }

      public static readonly DependencyProperty IconHightlightColorProperty = DependencyProperty.Register("IconHightlightColor", typeof(Brush), typeof(SideBarMenuItem), new PropertyMetadata(Brushes.Black));
      #endregion DP: IconHightlightColor

      #region DP: TextHightlightColor
      public Brush TextHightlightColor
      {
         get { return (Brush)GetValue(TextHightlightColorProperty); }
         set { SetValue(TextHightlightColorProperty, value); }
      }

      public static readonly DependencyProperty TextHightlightColorProperty = DependencyProperty.Register("TextHightlightColor", typeof(Brush), typeof(SideBarMenuItem), new PropertyMetadata(Brushes.White));
      #endregion DP: TextHightlightColor

      #region DP: FocusTextColor
      public Brush FocusTextColor
      {
         get { return (Brush)GetValue(FocusTextColorProperty); }
         set { SetValue(FocusTextColorProperty, value); }
      }

      public static readonly DependencyProperty FocusTextColorProperty = DependencyProperty.Register("FocusTextColor", typeof(Brush), typeof(SideBarMenuItem), new PropertyMetadata(Brushes.White));
      #endregion DP: FocusTextColor

      #region DP: ItemBackground
      public Brush ItemBackground
      {
         get { return (Brush)GetValue(ItemBackgroundProperty); }
         set { SetValue(ItemBackgroundProperty, value); }
      }

      public static readonly DependencyProperty ItemBackgroundProperty = DependencyProperty.Register("ItemBackground", typeof(Brush), typeof(SideBarMenuItem), new PropertyMetadata(Brushes.Gray, new PropertyChangedCallback((d, t) =>
      {
         var sideBarMenuItem = (SideBarMenuItem)d;
         sideBarMenuItem.Dispatcher?.Invoke(new Action(() =>
         {
            sideBarMenuItem.mainBorder.Background = sideBarMenuItem.IsMouseEntered ? sideBarMenuItem.MouseHightlightColor : sideBarMenuItem.ItemBackground;
         }));
      })));
      #endregion DP: ItemBackground

      #region DP: TextForeground
      public Brush TextForeground
      {
         get { return (Brush)GetValue(TextForegroundProperty); }
         set { SetValue(TextForegroundProperty, value); }
      }

      public static readonly DependencyProperty TextForegroundProperty = DependencyProperty.Register("TextForeground", typeof(Brush), typeof(SideBarMenuItem), new PropertyMetadata(Brushes.Gray, new PropertyChangedCallback((d, t) =>
      {
         var sideBarMenuItem = (SideBarMenuItem)d;
         sideBarMenuItem.Dispatcher?.Invoke(new Action(() =>
         {
            sideBarMenuItem.menuTextBlock.Foreground = sideBarMenuItem.IsMouseEntered ? sideBarMenuItem.TextHightlightColor : sideBarMenuItem.TextForeground;
         }));
      })));
      #endregion DP: TextForeground

      #region DP: ItemRadius
      public CornerRadius ItemRadius
      {
         get { return (CornerRadius)GetValue(ItemRadiusProperty); }
         set { SetValue(ItemRadiusProperty, value); }
      }

      public static readonly DependencyProperty ItemRadiusProperty =
          DependencyProperty.Register("ItemRadius", typeof(CornerRadius), typeof(SideBarMenuItem), new PropertyMetadata(new CornerRadius(4)));
      #endregion DP: ItemRadius

      #region DP: MenuText
      public string Text
      {
         get { return (string)GetValue(TextProperty); }
         set { SetValue(TextProperty, value); }
      }

      // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty TextProperty =
          DependencyProperty.Register("Text", typeof(string), typeof(SideBarMenuItem), new PropertyMetadata(string.Empty, (d, t) =>
          {
             var sideBarMenuItem = d as SideBarMenuItem;
             sideBarMenuItem.Dispatcher.Invoke(() =>
             {
                if (string.IsNullOrEmpty(sideBarMenuItem.Text))
                {
                   sideBarMenuItem.menuTextBlock.Visibility = Visibility.Collapsed;
                }
                else
                {
                   sideBarMenuItem.menuTextBlock.Text = sideBarMenuItem.Text;

                   if (sideBarMenuItem.menuTextBlock.Visibility != Visibility.Visible)
                      sideBarMenuItem.menuTextBlock.Visibility = Visibility.Visible;
                }
             });
          }));


      #endregion DP: MenuText

      #region DP: FlipIconVertically
      public bool FlipIconVertically
      {
         get { return (bool)GetValue(FlipIconVerticallyProperty); }
         set { SetValue(FlipIconVerticallyProperty, value); }
      }

      // Using a DependencyProperty as the backing store for FlipIconVertically.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty FlipIconVerticallyProperty =
          DependencyProperty.Register("FlipIconVertically", typeof(bool), typeof(SideBarMenuItem), new PropertyMetadata(false, (d, t) =>
          {
             var sideBarMenuItem = (SideBarMenuItem)d;
             sideBarMenuItem.SetIconDetails();

          }));
      #endregion DP:FlipIconVertically

      #region DP: FlipIconHorizontally
      public bool FlipIconHorizontally
      {
         get { return (bool)GetValue(FlipIconHorizontallyProperty); }
         set { SetValue(FlipIconHorizontallyProperty, value); }
      }

      // Using a DependencyProperty as the backing store for FlipIconVertically.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty FlipIconHorizontallyProperty =
          DependencyProperty.Register("FlipIconHorizontally", typeof(bool), typeof(SideBarMenuItem), new PropertyMetadata(false, (d, t) =>
          {
             var sideBarMenuItem = (SideBarMenuItem)d;
             sideBarMenuItem.SetIconDetails();

          }));
      #endregion DP:FlipIconHorizontally

      #region DP: IconTextOrientation
      public Orientation IconTextOrientation
      {
         get { return (Orientation)GetValue(IconTextOrientationProperty); }
         set { SetValue(IconTextOrientationProperty, value); }
      }

      // Using a DependencyProperty as the backing store for IconTextOrientation.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty IconTextOrientationProperty =
          DependencyProperty.Register("IconTextOrientation", typeof(Orientation), typeof(SideBarMenuItem), new PropertyMetadata(Orientation.Vertical));


      #endregion DP: IconTextOrientation

      #region DP: IconMargin
      public Thickness IconMargin
      {
         get { return (Thickness)GetValue(IconMarginProperty); }
         set { SetValue(IconMarginProperty, value); }
      }

      // Using a DependencyProperty as the backing store for IconMargin.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty IconMarginProperty =
          DependencyProperty.Register("IconMargin", typeof(Thickness), typeof(SideBarMenuItem), new PropertyMetadata(new Thickness(0)));
      #endregion DP: IconMargin

      #region DP: TextMargin
      public Thickness TextMargin
      {
         get { return (Thickness)GetValue(TextMarginProperty); }
         set { SetValue(TextMarginProperty, value); }
      }

      // Using a DependencyProperty as the backing store for TextMargin.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty TextMarginProperty =
          DependencyProperty.Register("TextMargin", typeof(Thickness), typeof(SideBarMenuItem), new PropertyMetadata(new Thickness(0)));
      #endregion DP: TextMargin

      #region DP: IconWidth
      public double IconWidth
      {
         get { return (double)GetValue(IconWidthProperty); }
         set { SetValue(IconWidthProperty, value); }
      }

      // Using a DependencyProperty as the backing store for IconWidth.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty IconWidthProperty =
          DependencyProperty.Register("IconWidth", typeof(double), typeof(SideBarMenuItem), new PropertyMetadata(22.0));
      #endregion DP: IconWidth

      #region DP: IconHeight
      public double IconHeight
      {
         get { return (double)GetValue(IconHeightProperty); }
         set { SetValue(IconHeightProperty, value); }
      }

      // Using a DependencyProperty as the backing store for IconHeight.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty IconHeightProperty =
          DependencyProperty.Register("IconHeight", typeof(double), typeof(SideBarMenuItem), new PropertyMetadata(22.0));
      #endregion DP: IconHeight

      #region DP: IconHorizontalAlignment
      public HorizontalAlignment IconHorizontalAlignment
      {
         get { return (HorizontalAlignment)GetValue(IconHorizontalAlignmentProperty); }
         set { SetValue(IconHorizontalAlignmentProperty, value); }
      }

      // Using a DependencyProperty as the backing store for IconHorizontalAlignment.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty IconHorizontalAlignmentProperty =
          DependencyProperty.Register("IconHorizontalAlignment", typeof(HorizontalAlignment), typeof(SideBarMenuItem), new PropertyMetadata(HorizontalAlignment.Center));
      #endregion DP: IconHorizontalAlignment

      #region DP: TextHorizontalAlignment
      public HorizontalAlignment TextHorizontalAlignment
      {
         get { return (HorizontalAlignment)GetValue(TextHorizontalAlignmentProperty); }
         set { SetValue(TextHorizontalAlignmentProperty, value); }
      }

      public static readonly DependencyProperty TextHorizontalAlignmentProperty =
          DependencyProperty.Register("TextHorizontalAlignment", typeof(HorizontalAlignment), typeof(SideBarMenuItem), new PropertyMetadata(HorizontalAlignment.Center));
      #endregion DP: TextHorizontalAlignment
      #endregion Dependency Properties

      #region Routing Events
      private void RaiseCustomEvent()
      {

         if (this.IsToggling)
         {
            this.Dispatcher.Invoke(() =>
            {
               var eventArgs = new MenuItemToggledEventArgs(ToggledEvent, this, this.IsActiveSelected);
               this.RaiseEvent(eventArgs);
            });
         }
         else
         {
            if (IsActiveSelected)
            {
               Dispatcher.Invoke(() =>
               {
                  RaiseEvent(new RoutedEventArgs(SelectedEvent, this));
               });
            }
         }
      }

      #region RE: Toggled
      public static readonly RoutedEvent ToggledEvent =
          EventManager.RegisterRoutedEvent("Toggled", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SideBarMenuItem));

      public event RoutedEventHandler Toggled
      {
         add { AddHandler(ToggledEvent, value); }
         remove { RemoveHandler(ToggledEvent, value); }
      }
      #endregion RE: Toggled

      #region RE: Selected
      public static readonly RoutedEvent SelectedEvent =
          EventManager.RegisterRoutedEvent("Selected", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SideBarMenuItem));

      public event RoutedEventHandler Selected
      {
         add { AddHandler(SelectedEvent, value); }
         remove { RemoveHandler(SelectedEvent, value); }
      }
      #endregion RE: Selected

      #endregion Routing Events

      public SideBarMenuItem()
      {
         InitializeComponent();
      }

      public SideBarMenuItem(Theme theme)
      {
         InitializeComponent();
         SetThemeUI(theme, true);
      }




      #region Animations
      private void AnimateGrowShrink(object obj, bool IsGrow)
      {
         DoubleAnimation heightAnimation = new DoubleAnimation();
         heightAnimation.From = IsGrow ? 0 : 100;
         heightAnimation.To = IsGrow ? 100 : 0;
         heightAnimation.Duration = TimeSpan.FromMilliseconds(200);
         //MessageBox.Show(heightAnimation.From + "\n" + heightAnimation.To);

         Storyboard storyboard = new Storyboard();
         storyboard.Children.Add(heightAnimation);

         Storyboard.SetTargetProperty(heightAnimation, new PropertyPath(Border.HeightProperty));
         Storyboard.SetTarget(heightAnimation, ((FrameworkElement)obj));

         storyboard.Begin();
      }

      public void RotateIcon(double milliseconds, double rotationAngle)
      {


         RotateTransform rotateTransform = new RotateTransform();
         double currentRotAngle = 0;

         if(iconControl.RenderTransform is RotateTransform rotTransform)
         {
            currentRotAngle = rotTransform.Angle;
         }
         else
         {
            currentRotAngle = 0;
         }

         iconControl.RenderTransformOrigin = new Point(0.5, 0.5);
         iconControl.RenderTransform = rotateTransform;

         DoubleAnimation rotateAnimation = new DoubleAnimation
         {
            From = currentRotAngle,
            To = currentRotAngle + rotationAngle,
            Duration = TimeSpan.FromMilliseconds(milliseconds),
            FillBehavior = FillBehavior.HoldEnd
         };

         rotateTransform.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
      }
      #endregion


      private void SetIconDetails(string iconPath = null, Brush iconColor = null, bool? FlipVertically = null, bool? FlipHorizontally = null)
      {
         this.Dispatcher.Invoke(new Action(() =>
         {
            this.iconControl.Path = string.IsNullOrWhiteSpace(iconPath) ? IconPath : iconPath;
            this.iconControl.Color = iconColor == null ?IconColor : iconColor;
            this.iconControl.FlipVertically = FlipVertically == null ? FlipIconVertically : FlipVertically.Value;
            this.iconControl.FlipHorizontally = FlipVertically == null ? FlipIconHorizontally : FlipVertically.Value;
         }));
      }


      #region Default Events
      private void ImageButton_Click(object sender, RoutedEventArgs e)
      {
         if(!DisableClickEvents)
         {
            if (IsActiveFocusableMenu && !IsToggling)
            {
               IsActiveSelected = true;
            }
            else if (IsToggling)
            {
               IsActiveSelected = !IsActiveSelected;
            }
            else if (!IsToggling)
            {
               Dispatcher.Invoke(() =>
               {
                  RaiseEvent(new RoutedEventArgs(SelectedEvent, this));
               });
            }
         }
      }

      private void sideBarMenuItem_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
      {
         Dispatcher.Invoke(() =>
         {
            this.IsMouseEntered = true;

            if (!this.IsActiveSelected)
            {
               this.mainBorder.Background = this.MouseHightlightColor;
               this.SetIconDetails(string.Empty, this.IconHightlightColor);
               this.menuTextBlock.Foreground = this.TextHightlightColor;
            }
         });
      }

      private void sideBarMenuItem_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
      {
         Dispatcher.Invoke(() =>
         {
            this.IsMouseEntered = false;
            if (!this.IsActiveSelected)
            {
               this.mainBorder.Background = this.ItemBackground;
               this.SetIconDetails(string.Empty, this.IconColor);
               this.menuTextBlock.Foreground = this.TextForeground;
            }
         });
      }
      #endregion Default Events



      public void SetThemeUI(Theme theme, bool UpdateContentThemes = false)
      {
         _theme = theme;
         if(theme != null)
         {
            ItemBackground = theme?.MenuBackground;
            MouseHightlightColor = theme?.MenuHighlightBackground;
            FocusMarkerColor = theme?.MenuFocusMarkerColor;
            IconColor = theme?.MenuForeground;
            FocusIconColor = theme?.MenuFocusIconColor;
            IconHightlightColor = theme?.ThemeForeground;
            TextForeground = theme?.MenuForeground;
            FocusTextColor = theme?.MenuFocusForeground;
            TextHightlightColor = theme?.MenuHighlightForeground;
         }
      }

      public Theme GetTheme()
      {
         return _theme;
      }
   }
}
