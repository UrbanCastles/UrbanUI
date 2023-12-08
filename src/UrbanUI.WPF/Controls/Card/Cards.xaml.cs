using UrbanUI.WPF.HybridControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using UrbanUI.WPF.Themes;

namespace UrbanUI.WPF.Controls
{
   /// <summary>
   /// Interaction logic for Cards.xaml
   /// </summary>
   public partial class Cards : UserControl, ITheme
   {
      private bool IsMoussEntered = false;
      private bool IsMoussPressed = false;

      private Theme currentTheme = new Theme();

      #region Dependency Properties

      #region DP: ImageBackgroundColor
      public static readonly DependencyProperty ImageBackgroundColorProperty = DependencyProperty.Register("ImageBackgroundColor", typeof(Brush), typeof(Cards), new FrameworkPropertyMetadata(Brushes.Blue));

      public Brush ImageBackgroundColor
      {
         get { return (Brush)GetValue(ImageBackgroundColorProperty); }
         set { SetValue(ImageBackgroundColorProperty, value); }
      }
      #endregion DP: ImageBackgroundColor

      #region DP: ImageSource
      public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(Cards), new FrameworkPropertyMetadata(null));

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

      public static readonly DependencyProperty ImageOpacityProperty =
          DependencyProperty.Register("ImageOpacity", typeof(double), typeof(Cards), new PropertyMetadata(1.0));
      #endregion DP: ImageOpacity

      #region DP: TitleText
      public string TitleText
      {
         get { return (string)GetValue(TitleTextProperty); }
         set { SetValue(TitleTextProperty, value); }
      }

      public static readonly DependencyProperty TitleTextProperty =
          DependencyProperty.Register("TitleText", typeof(string), typeof(Cards), new PropertyMetadata(string.Empty, (d,t) =>
          {
             var card = (Cards)d;
             card.titleText.Visibility = string.IsNullOrWhiteSpace(card.TitleText) ? Visibility.Collapsed : Visibility.Visible;
          }));
      #endregion DP: TitleText

      #region DP: SubText
      public string SubText
      {
         get { return (string)GetValue(SubTextProperty); }
         set { SetValue(SubTextProperty, value); }
      }

      public static readonly DependencyProperty SubTextProperty =
          DependencyProperty.Register("SubText", typeof(string), typeof(Cards), new PropertyMetadata(string.Empty, (d, t) =>
          {
             var card = (Cards)d;
             card.subtext.Visibility = string.IsNullOrWhiteSpace(card.SubText) ? Visibility.Collapsed : Visibility.Visible;
          }));
      #endregion DP: SubText

      #region DP: MessageText
      public string MessageText
      {
         get { return (string)GetValue(MessageTextProperty); }
         set { SetValue(MessageTextProperty, value); }
      }

      public static readonly DependencyProperty MessageTextProperty =
          DependencyProperty.Register("MessageText", typeof(string), typeof(Cards), new PropertyMetadata(string.Empty, (d, t) =>
          {
             var card = (Cards)d;
             card.messageText.Visibility = string.IsNullOrWhiteSpace(card.MessageText) ? Visibility.Collapsed : Visibility.Visible;
          }));
      #endregion DP: MessageText

      #region DP: Foreground
      public Brush Foreground
      {
         get { return (Brush)GetValue(ForegroundProperty); }
         set { SetValue(ForegroundProperty, value); }
      }

      public static readonly DependencyProperty ForegroundProperty =
          DependencyProperty.Register("Foreground", typeof(Brush), typeof(Cards), new PropertyMetadata(Brushes.White));
      #endregion  DP: Foreground

      #region DP: HighlightedBackground
      public Brush HighlightedBackground
      {
         get { return (Brush)GetValue(HighlightedBackgroundProperty); }
         set { SetValue(HighlightedBackgroundProperty, value); }
      }

      public static readonly DependencyProperty HighlightedBackgroundProperty =
          DependencyProperty.Register("HighlightedBackground", typeof(Brush), typeof(Cards), new PropertyMetadata(Brushes.LightGray));
      #endregion  DP: HighlightedBackground

      #region DP: HighlightedForeground
      public Brush HighlightedForeground
      {
         get { return (Brush)GetValue(HighlightedForegroundProperty); }
         set { SetValue(HighlightedForegroundProperty, value); }
      }

      public static readonly DependencyProperty HighlightedForegroundProperty =
          DependencyProperty.Register("HighlightedForeground", typeof(Brush), typeof(Cards), new PropertyMetadata(Brushes.White));
      #endregion  DP: HighlightedForeground

      #region DP: PressedBackground
      public Brush PressedBackground
      {
         get { return (Brush)GetValue(PressedBackgroundProperty); }
         set { SetValue(PressedBackgroundProperty, value); }
      }

      public static readonly DependencyProperty PressedBackgroundProperty =
          DependencyProperty.Register("PressedBackground", typeof(Brush), typeof(Cards), new PropertyMetadata(Brushes.Gray));
      #endregion  DP: PressedBackground

      #region DP: PressedForeground
      public Brush PressedForeground
      {
         get { return (Brush)GetValue(PressedForegroundProperty); }
         set { SetValue(PressedForegroundProperty, value); }
      }

      public static readonly DependencyProperty PressedForegroundProperty =
          DependencyProperty.Register("PressedForeground", typeof(Brush), typeof(Cards), new PropertyMetadata(Brushes.White));
      #endregion  DP: PressedForeground

      #region DP: DisabledBackground
      public Brush DisabledBackground
      {
         get { return (Brush)GetValue(DisabledBackgroundProperty); }
         set { SetValue(DisabledBackgroundProperty, value); }
      }

      public static readonly DependencyProperty DisabledBackgroundProperty =
          DependencyProperty.Register("DisabledBackground", typeof(Brush), typeof(Cards), new PropertyMetadata(Brushes.Gray, (d, t) =>
          {

          }));
      #endregion  DP: DisabledBackground

      #region DP: Icon
      public Icon Icon
      {
         get { return (Icon)GetValue(IconProperty); }
         set { SetValue(IconProperty, value); }
      }
      public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(Icon), typeof(Cards), new PropertyMetadata(null, (d, t) =>
            {
               var card = (Cards)d;
               card.SetIconDetails();
            }));
      #endregion DP: Icon

      #region DP: IconColor
      public Brush IconColor
      {
         get { return (Brush)GetValue(IconColorProperty); }
         set { SetValue(IconColorProperty, value); }
      }
      public static readonly DependencyProperty IconColorProperty =
            DependencyProperty.Register("IconColor", typeof(Brush), typeof(Cards), new PropertyMetadata(Brushes.White));
      #endregion DP: IconColor

      #region DP: ImageMargin
      public Thickness ImageMargin
      {
         get { return (Thickness)GetValue(ImageMarginProperty); }
         set { SetValue(ImageMarginProperty, value); }
      }
      public static readonly DependencyProperty ImageMarginProperty =
            DependencyProperty.Register("ImageMargin", typeof(Thickness), typeof(Cards), new PropertyMetadata(new Thickness(0)));
      #endregion DP: ImageMargin

      #region DP: SubTextFontSize
      public double SubTextFontSize
      {
         get { return (double)GetValue(SubTextFontSizeProperty); }
         set { SetValue(SubTextFontSizeProperty, value); }
      }
      public static readonly DependencyProperty SubTextFontSizeProperty =
          DependencyProperty.Register("SubTextFontSize", typeof(double), typeof(Cards), new PropertyMetadata(12.0));
      #endregion DP: SubTextFontSize

      #region DP: MessageTextFontSize
      public double MessageTextFontSize
      {
         get { return (double)GetValue(MessageTextFontSizeProperty); }
         set { SetValue(MessageTextFontSizeProperty, value); }
      }
      public static readonly DependencyProperty MessageTextFontSizeProperty =
          DependencyProperty.Register("MessageTextFontSize", typeof(double), typeof(Cards), new PropertyMetadata(10.0));
      #endregion DP: MessageTextFontSize

      #region DP: SubTextFontWeight
      public FontWeight SubTextFontWeight
      {
         get { return (FontWeight)GetValue(SubTextFontWeightProperty); }
         set { SetValue(SubTextFontWeightProperty, value); }
      }
      public static readonly DependencyProperty SubTextFontWeightProperty =
          DependencyProperty.Register("SubTextFontWeight", typeof(FontWeight), typeof(Cards), new PropertyMetadata(FontWeights.Normal));
      #endregion DP: SubTextFontWeight

      #region DP: MessageTextFontWeight
      public FontWeight MessageTextFontWeight
      {
         get { return (FontWeight)GetValue(MessageTextFontWeightProperty); }
         set { SetValue(MessageTextFontWeightProperty, value); }
      }
      public static readonly DependencyProperty MessageTextFontWeightProperty =
          DependencyProperty.Register("MessageTextFontWeight", typeof(FontWeight), typeof(Cards), new PropertyMetadata(FontWeights.Light));
      #endregion DP: MessageTextFontWeight

      #region DP: SubTextFontStyle
      public FontStyle SubTextFontStyle
      {
         get { return (FontStyle)GetValue(SubTextFontStyleProperty); }
         set { SetValue(SubTextFontStyleProperty, value); }
      }
      public static readonly DependencyProperty SubTextFontStyleProperty =
          DependencyProperty.Register("SubTextFontStyle", typeof(FontStyle), typeof(Cards), new PropertyMetadata(FontStyles.Normal));
      #endregion DP: SubTextFontStyle

      #region DP: MessageTextFontStyle
      public FontStyle MessageTextFontStyle
      {
         get { return (FontStyle)GetValue(MessageTextFontStyleProperty); }
         set { SetValue(MessageTextFontStyleProperty, value); }
      }
      public static readonly DependencyProperty MessageTextFontStyleProperty =
          DependencyProperty.Register("MessageTextFontStyle", typeof(FontStyle), typeof(Cards), new PropertyMetadata(FontStyles.Italic));
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
          EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Cards));

      public event RoutedEventHandler Click
      {
         add { AddHandler(ClickEvent, value); }
         remove { RemoveHandler(ClickEvent, value); }
      }
      #endregion RE: Click


      #endregion Routing Events


      public Cards()
      {
         InitializeComponent();
         this.IsEnabledChanged += delegate
         {
            SetThemeUI(currentTheme, true);
         };
      }

      public void SetThemeUI(Theme theme, bool UpdateContentThemes = false)
      {
         currentTheme = theme;
         SetColorUI(currentTheme);
      }

      public Theme GetTheme()
      {
         return currentTheme;
      }


      private void SetColorUI(Theme theme)
      {
         if (this.IsEnabled)
         {
            if (!IsMoussEntered && !IsMoussPressed)
            {
               mainBorder.Background = theme.ItemBackground;
            }
            else
            {
               mainBorder.Background = IsMoussPressed ? theme.ItemFocusBackground : theme.ItemHighlightBackground;
            }
         }
         else
         {

            mainBorder.Background = theme.DisabledBackground;
         }


         if (this.IsEnabled)
         {
            if ((!IsMoussEntered && !IsMoussPressed))
            {
               titleText.Foreground = theme.ItemForeground;
               subtext.Foreground = theme.ItemForeground;
               messageText.Foreground = theme.ItemForeground;
            }
            else
            {
               titleText.Foreground = IsMoussPressed ? theme.ItemFocusForeground : theme.ItemHighlightForeground;
               subtext.Foreground = IsMoussPressed ? theme.ItemFocusForeground : theme.ItemHighlightForeground;
               messageText.Foreground = IsMoussPressed ? theme.ItemFocusForeground : theme.ItemHighlightForeground;
            }
         }
         else
         {
            titleText.Foreground = theme.DisabledForeground;
            subtext.Foreground = theme.DisabledForeground;
            messageText.Foreground = theme.DisabledForeground;
         }


      }

      private void ImageButton_MouseEnter(object sender, MouseEventArgs e)
      {
         if(!IsMoussEntered)
         {
            IsMoussEntered = true;
            SetColorUI(currentTheme);
         }
      }

      private void ImageButton_MouseLeave(object sender, MouseEventArgs e)
      {
         if (IsMoussEntered)
         {
            IsMoussEntered = false;
            SetColorUI(currentTheme);
         }
      }

      private void mainImgButton_Click(object sender, RoutedEventArgs e)
      {
         RaiseClickEvent();
      }

      private void mainImgButton_PressHold(object sender, HybridControls.PressHoldEventArgs e)
      {
         if(e.Event == PressHoldEventArgs.EventType.Press)
         {
            if (!IsMoussPressed)
            {
               IsMoussPressed = true;
               SetColorUI(currentTheme);
            }
         }
         else
         {
            if (IsMoussPressed)
            {
               IsMoussPressed = false;
               SetColorUI(currentTheme);
            }
         }
      }


      private void SetIconDetails()
      {
         if(this.Icon != null)
         {
            this.pathIcon.Path = this.Icon.StrokePath;
            this.pathIcon.FlipHorizontally = this.Icon.FlipX;
            this.pathIcon.FlipVertically = this.Icon.FlipY;
         }
      }
   }
}
