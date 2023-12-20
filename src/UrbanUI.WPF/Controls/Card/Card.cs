using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UrbanUI.WPF.Themes;

namespace UrbanUI.WPF.Controls
{
   [TemplatePart(Name = "PART_mainBorderButton", Type = typeof(Button))]
   public partial class Card : Control, ITheme
   {

      #region Dependency Properties

      #region DP: ImageBackgroundColor
      public static readonly DependencyProperty ImageBackgroundColorProperty = DependencyProperty.Register("ImageBackgroundColor", typeof(Brush), typeof(Card), new FrameworkPropertyMetadata(Brushes.Blue));

      public Brush ImageBackgroundColor
      {
         get { return (Brush)GetValue(ImageBackgroundColorProperty); }
         set { SetValue(ImageBackgroundColorProperty, value); }
      }
      #endregion DP: ImageBackgroundColor

      #region DP: ImageSource
      public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(Card), new FrameworkPropertyMetadata(null));

      public ImageSource ImageSource
      {
         get { return (ImageSource)GetValue(ImageSourceProperty); }
         set { SetValue(ImageSourceProperty, value); }
      }
      #endregion DP: ImageSource

      #region DP: ImageOpacity
      public double ImageOpacity
      {
         get { return (double)GetValue(ImageOpacityProperty); }
         set { SetValue(ImageOpacityProperty, value); }
      }

      public static readonly DependencyProperty ImageOpacityProperty = DependencyProperty.Register("ImageOpacity", typeof(double), typeof(Card), new PropertyMetadata(1.0));
      #endregion DP: ImageOpacity

      #region DP: TitleText
      public string TitleText
      {
         get { return (string)GetValue(TitleTextProperty); }
         set { SetValue(TitleTextProperty, value); }
      }

      public static readonly DependencyProperty TitleTextProperty = DependencyProperty.Register("TitleText", typeof(string), typeof(Card), new PropertyMetadata(null));
      #endregion DP: TitleText

      #region DP: SubText
      public string SubText
      {
         get { return (string)GetValue(SubTextProperty); }
         set { SetValue(SubTextProperty, value); }
      }

      public static readonly DependencyProperty SubTextProperty = DependencyProperty.Register("SubText", typeof(string), typeof(Card), new PropertyMetadata(null));
      #endregion DP: SubText

      #region DP: MessageText
      public string MessageText
      {
         get { return (string)GetValue(MessageTextProperty); }
         set { SetValue(MessageTextProperty, value); }
      }

      public static readonly DependencyProperty MessageTextProperty = DependencyProperty.Register("MessageText", typeof(string), typeof(Card), new PropertyMetadata(null));
      #endregion DP: MessageText

      #region DP: MouseHoverBackground
      public Brush MouseHoverBackground
      {
         get { return (Brush)GetValue(MouseHoverBackgroundProperty); }
         set { SetValue(MouseHoverBackgroundProperty, value); }
      }

      public static readonly DependencyProperty MouseHoverBackgroundProperty = DependencyProperty.Register(nameof(MouseHoverBackground), typeof(Brush), typeof(Card), new PropertyMetadata(Brushes.LightGray));
      #endregion  DP: MouseHoverBackground

      #region DP: MouseHoverForeground
      public Brush MouseHoverForeground
      {
         get { return (Brush)GetValue(MouseHoverForegroundProperty); }
         set { SetValue(MouseHoverForegroundProperty, value); }
      }

      public static readonly DependencyProperty MouseHoverForegroundProperty = DependencyProperty.Register(nameof(MouseHoverForeground), typeof(Brush), typeof(Card), new PropertyMetadata(Brushes.White));
      #endregion  DP: MouseHoverForeground

      #region DP: PressedBackground
      public Brush PressedBackground
      {
         get { return (Brush)GetValue(PressedBackgroundProperty); }
         set { SetValue(PressedBackgroundProperty, value); }
      }

      public static readonly DependencyProperty PressedBackgroundProperty = DependencyProperty.Register(nameof(PressedBackground), typeof(Brush), typeof(Card), new PropertyMetadata(Brushes.Gray));
      #endregion  DP: PressedBackground

      #region DP: PressedForeground
      public Brush PressedForeground
      {
         get { return (Brush)GetValue(PressedForegroundProperty); }
         set { SetValue(PressedForegroundProperty, value); }
      }

      public static readonly DependencyProperty PressedForegroundProperty = DependencyProperty.Register(nameof(PressedForeground), typeof(Brush), typeof(Card), new PropertyMetadata(Brushes.White));
      #endregion  DP: PressedForeground

      #region DP: DisabledBackground
      public Brush DisabledBackground
      {
         get { return (Brush)GetValue(DisabledBackgroundProperty); }
         set { SetValue(DisabledBackgroundProperty, value); }
      }

      public static readonly DependencyProperty DisabledBackgroundProperty = DependencyProperty.Register(nameof(DisabledBackground), typeof(Brush), typeof(Card), new PropertyMetadata(Brushes.Gray));
      #endregion  DP: DisabledBackground

      #region DP: DisabledForeground
      public Brush DisabledForeground
      {
         get { return (Brush)GetValue(DisabledForegroundProperty); }
         set { SetValue(DisabledForegroundProperty, value); }
      }

      public static readonly DependencyProperty DisabledForegroundProperty = DependencyProperty.Register(nameof(DisabledForeground), typeof(Brush), typeof(Card), new PropertyMetadata(Brushes.LightGray));
      #endregion  DP: DisabledForeground

      #region DP: IconPath
      public Geometry IconPath
      {
         get { return (Geometry)GetValue(IconPathProperty); }
         set { SetValue(IconPathProperty, value); }
      }
      public static readonly DependencyProperty IconPathProperty = DependencyProperty.Register(nameof(IconPath), typeof(Geometry), typeof(Card), new PropertyMetadata(null));
      #endregion DP: IconPath

      #region DP: IconColor
      public Brush IconColor
      {
         get { return (Brush)GetValue(IconColorProperty); }
         set { SetValue(IconColorProperty, value); }
      }
      public static readonly DependencyProperty IconColorProperty = DependencyProperty.Register(nameof(IconColor), typeof(Brush), typeof(Card), new PropertyMetadata(Brushes.White));
      #endregion DP: IconColor

      #region DP: IconBorderMinWidth
      public double IconBorderMinWidth
      {
         get { return (double)GetValue(IconBorderMinWidthProperty); }
         set { SetValue(IconBorderMinWidthProperty, value); }
      }
      public static readonly DependencyProperty IconBorderMinWidthProperty = DependencyProperty.Register(nameof(IconBorderMinWidth), typeof(double), typeof(Card), new PropertyMetadata(0.0));
      #endregion DP: IconBorderMinWidth

      #region DP: IconBorderMinHeight
      public double IconBorderMinHeight
      {
         get { return (double)GetValue(IconBorderMinHeightProperty); }
         set { SetValue(IconBorderMinHeightProperty, value); }
      }
      public static readonly DependencyProperty IconBorderMinHeightProperty = DependencyProperty.Register(nameof(IconBorderMinHeight), typeof(double), typeof(Card), new PropertyMetadata(0.0));
      #endregion DP: IconBorderMinHeight

      #region DP: CornerRadius
      public CornerRadius CornerRadius
      {
         get { return (CornerRadius)GetValue(CornerRadiusProperty); }
         set { SetValue(CornerRadiusProperty, value); }
      }
      public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(Card), new PropertyMetadata(new CornerRadius(10.0)));
      #endregion DP: CornerRadius

      #region DP: ImageBorderCornerRadius
      public CornerRadius ImageBorderCornerRadius
      {
         get { return (CornerRadius)GetValue(ImageBorderCornerRadiusProperty); }
         set { SetValue(ImageBorderCornerRadiusProperty, value); }
      }
      public static readonly DependencyProperty ImageBorderCornerRadiusProperty = DependencyProperty.Register(nameof(ImageBorderCornerRadius), typeof(CornerRadius), typeof(Card), new PropertyMetadata(new CornerRadius(10.0)));
      #endregion DP: ImageBorderCornerRadius

      #region DP: ImageMargin
      public Thickness ImageMargin
      {
         get { return (Thickness)GetValue(ImageMarginProperty); }
         set { SetValue(ImageMarginProperty, value); }
      }
      public static readonly DependencyProperty ImageMarginProperty = DependencyProperty.Register(nameof(ImageMargin), typeof(Thickness), typeof(Card), new PropertyMetadata(new Thickness(0)));
      #endregion DP: ImageMargin

      #region DP: SubTextFontSize
      public double SubTextFontSize
      {
         get { return (double)GetValue(SubTextFontSizeProperty); }
         set { SetValue(SubTextFontSizeProperty, value); }
      }
      public static readonly DependencyProperty SubTextFontSizeProperty = DependencyProperty.Register(nameof(SubTextFontSize), typeof(double), typeof(Card), new PropertyMetadata(12.0));
      #endregion DP: SubTextFontSize

      #region DP: MessageTextFontSize
      public double MessageTextFontSize
      {
         get { return (double)GetValue(MessageTextFontSizeProperty); }
         set { SetValue(MessageTextFontSizeProperty, value); }
      }
      public static readonly DependencyProperty MessageTextFontSizeProperty = DependencyProperty.Register(nameof(MessageTextFontSize), typeof(double), typeof(Card), new PropertyMetadata(10.0));
      #endregion DP: MessageTextFontSize

      #region DP: SubTextFontWeight
      public FontWeight SubTextFontWeight
      {
         get { return (FontWeight)GetValue(SubTextFontWeightProperty); }
         set { SetValue(SubTextFontWeightProperty, value); }
      }
      public static readonly DependencyProperty SubTextFontWeightProperty = DependencyProperty.Register(nameof(SubTextFontWeight), typeof(FontWeight), typeof(Card), new PropertyMetadata(FontWeights.Normal));
      #endregion DP: SubTextFontWeight

      #region DP: MessageTextFontWeight
      public FontWeight MessageTextFontWeight
      {
         get { return (FontWeight)GetValue(MessageTextFontWeightProperty); }
         set { SetValue(MessageTextFontWeightProperty, value); }
      }
      public static readonly DependencyProperty MessageTextFontWeightProperty = DependencyProperty.Register(nameof(MessageTextFontWeight), typeof(FontWeight), typeof(Card), new PropertyMetadata(FontWeights.Light));
      #endregion DP: MessageTextFontWeight

      #region DP: SubTextFontStyle
      public FontStyle SubTextFontStyle
      {
         get { return (FontStyle)GetValue(SubTextFontStyleProperty); }
         set { SetValue(SubTextFontStyleProperty, value); }
      }
      public static readonly DependencyProperty SubTextFontStyleProperty = DependencyProperty.Register(nameof(SubTextFontStyle), typeof(FontStyle), typeof(Card), new PropertyMetadata(FontStyles.Normal));
      #endregion DP: SubTextFontStyle

      #region DP: MessageTextFontStyle
      public FontStyle MessageTextFontStyle
      {
         get { return (FontStyle)GetValue(MessageTextFontStyleProperty); }
         set { SetValue(MessageTextFontStyleProperty, value); }
      }
      public static readonly DependencyProperty MessageTextFontStyleProperty = DependencyProperty.Register(nameof(MessageTextFontStyle), typeof(FontStyle), typeof(Card), new PropertyMetadata(FontStyles.Italic));
      #endregion DP: MessageTextFontStyle

      #endregion Dependency Properties

      #region Routing Events
      private void RaiseClickEvent()
      {
         Dispatcher.Invoke(() =>
         {
            RaiseEvent(new RoutedEventArgs(ClickEvent, this));
         });
      }

      #region RE: Click
      public static readonly RoutedEvent ClickEvent =
          EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Card));

      public event RoutedEventHandler Click
      {
         add { AddHandler(ClickEvent, value); }
         remove { RemoveHandler(ClickEvent, value); }
      }
      #endregion RE: Click


      #endregion Routing Events

      private Theme? _theme;

      private Button? _mainBorderButton;

      public Card()
      {
         //DefaultStyleKeyProperty.OverrideMetadata(typeof(Card), new FrameworkPropertyMetadata(typeof(Card)));
      }


      #region On Apply Template
      public override void OnApplyTemplate()
      {
         _mainBorderButton = GetTemplateChild("PART_mainBorderButton") as Button;

         base.OnApplyTemplate();

         if (_mainBorderButton != null)
         {
            _mainBorderButton.Click += delegate
            {
               this.RaiseClickEvent();
            };

            if (_theme == null)
            {
               _theme = Defaults.getDefaultThemeSetups();
            }
            SetThemeUI(_theme);
         }
      }
      #endregion On Apply Template

      #region Control Triggers
      private void FlatButton_Clicked(object sender, RoutedEventArgs e)
      {
         this.RaiseClickEvent();
      }
      #endregion Control Triggers


      #region Set Theme UIs
      public Theme GetTheme()
      {
         return _theme;
      }

      public void SetThemeUI(Theme theme, bool UpdateContentThemes = false)
      {
         _theme = theme;

         Background = theme.ItemBackground;
         MouseHoverBackground = theme.ItemHighlightBackground;
         MouseHoverForeground = theme.ItemHighlightForeground;
         PressedBackground = theme.ItemFocusBackground;

         Foreground = theme.ItemForeground;

         DisabledBackground = theme.DisabledBackground;
         DisabledForeground = theme.DisabledForeground;

         PressedForeground = theme.ItemFocusForeground;
      }
      #endregion Set Theme UIs
   }
}
