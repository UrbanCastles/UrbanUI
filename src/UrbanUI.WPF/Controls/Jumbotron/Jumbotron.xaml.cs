using UrbanUI.WPF.Themes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace UrbanUI.WPF.Controls
{
   /// <summary>
   /// Interaction logic for Jumbotron.xaml
   /// </summary>
   public partial class Jumbotron : UserControl, ITheme
   {
      private Theme _theme = new Theme();

      #region Dependency Properties

      #region DP: ImageSource

      public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register( "ImageSource", typeof(ImageSource), typeof(Jumbotron), new FrameworkPropertyMetadata(null,
            FrameworkPropertyMetadataOptions.AffectsRender,
            OnImageSourcePropertyChanged));

      public ImageSource ImageSource
      {
         get { return (ImageSource)GetValue(ImageSourceProperty); }
         set { SetValue(ImageSourceProperty, value); }
      }

      private static void OnImageSourcePropertyChanged(
          DependencyObject d,
          DependencyPropertyChangedEventArgs e)
      {
         if (d is Jumbotron jumbotron)
         {
            jumbotron.jumbotronBackground.Source = e.NewValue as ImageSource;
         }
      }
      #endregion DP: ImageSource

      #region DP: ImageOpacity
      public double ImageOpacity
      {
         get { return (double)GetValue(ImageOpacityProperty); }
         set { SetValue(ImageOpacityProperty, value); }
      }

      // Using a DependencyProperty as the backing store for ImageOpacity.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty ImageOpacityProperty =
          DependencyProperty.Register("ImageOpacity", typeof(double), typeof(Jumbotron), new PropertyMetadata(1.0));
      #endregion DP: ImageOpacity

      #region DP: HeaderText
      public string HeaderText
      {
         get { return (string)GetValue(HeaderTextProperty); }
         set { SetValue(HeaderTextProperty, value); }
      }

      // Using a DependencyProperty as the backing store for IntroText.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty HeaderTextProperty =
          DependencyProperty.Register("HeaderText", typeof(string), typeof(Jumbotron), new PropertyMetadata(string.Empty));
      #endregion DP: HeaderText

      #region DP: IntroText
      public string IntroText
      {
         get { return (string)GetValue(IntroTextProperty); }
         set { SetValue(IntroTextProperty, value); }
      }

      // Using a DependencyProperty as the backing store for IntroText.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty IntroTextProperty =
          DependencyProperty.Register("IntroText", typeof(string), typeof(Jumbotron), new PropertyMetadata(string.Empty));
      #endregion DP: IntroText

      #region DP: TitleText
      public string TitleText
      {
         get { return (string)GetValue(TitleTextProperty); }
         set { SetValue(TitleTextProperty, value); }
      }

      // Using a DependencyProperty as the backing store for IntroText.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty TitleTextProperty =
          DependencyProperty.Register("TitleText", typeof(string), typeof(Jumbotron), new PropertyMetadata(string.Empty));
      #endregion DP: TitleText

      #region DP: MessageText
      public string MessageText
      {
         get { return (string)GetValue(MessageTextProperty); }
         set { SetValue(MessageTextProperty, value); }
      }

      // Using a DependencyProperty as the backing store for IntroText.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty MessageTextProperty =
          DependencyProperty.Register("MessageText", typeof(string), typeof(Jumbotron), new PropertyMetadata(string.Empty));
      #endregion DP: MessageText

      #region DP: ButtonText
      public string ButtonText
      {
         get { return (string)GetValue(ButtonTextProperty); }
         set { SetValue(ButtonTextProperty, value); }
      }

      // Using a DependencyProperty as the backing store for IntroText.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty ButtonTextProperty =
          DependencyProperty.Register("ButtonText", typeof(string), typeof(Jumbotron), new PropertyMetadata(string.Empty));
      #endregion DP: ButtonText

      #region DP: ButtonBackground
      public Brush ButtonBackground
      {
         get { return (Brush)GetValue(ButtonBackgroundProperty); }
         set { SetValue(ButtonBackgroundProperty, value); }
      }

      // Using a DependencyProperty as the backing store for ButtonBackground.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty ButtonBackgroundProperty =
          DependencyProperty.Register("ButtonBackground", typeof(Brush), typeof(Jumbotron), new PropertyMetadata(Brushes.White));
      #endregion  DP: ButtonBackground

      #region DP: ButtonForeground
      public Brush ButtonForeground
      {
         get { return (Brush)GetValue(ButtonForegroundProperty); }
         set { SetValue(ButtonForegroundProperty, value); }
      }

      // Using a DependencyProperty as the backing store for ButtonForeground.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty ButtonForegroundProperty =
          DependencyProperty.Register("ButtonForeground", typeof(Brush), typeof(Jumbotron), new PropertyMetadata(Brushes.White));
      #endregion  DP: ButtonForeground

      #region DP: ButtonPressedBackground
      public Brush ButtonPressedBackground
      {
         get { return (Brush)GetValue(ButtonPressedBackgroundProperty); }
         set { SetValue(ButtonPressedBackgroundProperty, value); }
      }

      // Using a DependencyProperty as the backing store for ButtonPressedBackground.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty ButtonPressedBackgroundProperty =
          DependencyProperty.Register("ButtonPressedBackground", typeof(Brush), typeof(Jumbotron), new PropertyMetadata(Brushes.Gray));
      #endregion  DP: ButtonPressedBackground

      #region DP: ButtonPressedForeground
      public Brush ButtonPressedForeground
      {
         get { return (Brush)GetValue(ButtonPressedForegroundProperty); }
         set { SetValue(ButtonPressedForegroundProperty, value); }
      }

      // Using a DependencyProperty as the backing store for ButtonPressedForeground.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty ButtonPressedForegroundProperty =
          DependencyProperty.Register("ButtonPressedForeground", typeof(Brush), typeof(Jumbotron), new PropertyMetadata(Brushes.White));
      #endregion  DP: ButtonPressedForeground

      #region DP: ButtonHighlightBackground
      public Brush ButtonHighlightBackground
      {
         get { return (Brush)GetValue(ButtonHighlightBackgroundProperty); }
         set { SetValue(ButtonHighlightBackgroundProperty, value); }
      }

      // Using a DependencyProperty as the backing store for ButtonHighlightBackground.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty ButtonHighlightBackgroundProperty =
          DependencyProperty.Register("ButtonHighlightBackground", typeof(Brush), typeof(Jumbotron), new PropertyMetadata(Brushes.White));
      #endregion  DP: ButtonHighlightBackground

      #region DP: ButtonHighlightForeground
      public Brush ButtonHighlightForeground
      {
         get { return (Brush)GetValue(ButtonHighlightForegroundProperty); }
         set { SetValue(ButtonHighlightForegroundProperty, value); }
      }

      // Using a DependencyProperty as the backing store for ButtonHighlightForeground.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty ButtonHighlightForegroundProperty =
          DependencyProperty.Register("ButtonHighlightForeground", typeof(Brush), typeof(Jumbotron), new PropertyMetadata(Brushes.White));
      #endregion  DP: ButtonHighlightForeground

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
          EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Jumbotron));

      public event RoutedEventHandler Click
      {
         add { AddHandler(ClickEvent, value); }
         remove { RemoveHandler(ClickEvent, value); }
      }
      #endregion RE: Click

      #endregion Routing Events

      public Jumbotron()
      {
         InitializeComponent();
      }

      public Jumbotron(Theme theme)
      {
         InitializeComponent();

         if(theme != null)
         {
            SetThemeUI(theme, true);
         }
      }

      public void SetThemeUI(Theme theme, bool UpdateContentThemes = false)
      {
         _theme = theme;
         this.Foreground = theme.ThemeForeground;

         var themeMenuBackground = theme.MenuBackground.Clone();
         themeMenuBackground.Opacity = 0.6;
         this.ButtonBackground = themeMenuBackground;
         this.ButtonForeground = theme.MenuForeground;
         this.ButtonPressedBackground = theme.MenuFocusBackground;
         this.ButtonPressedForeground = theme.MenuFocusForeground;
         this.ButtonHighlightBackground = theme.MenuHighlightBackground;
         this.ButtonHighlightForeground = theme.MenuHighlightForeground;
      }

      public Theme GetTheme()
      {
         return _theme;
      }

      private void mainButton_Click(object sender, RoutedEventArgs e)
      {
         RaiseClickEvent();
      }
   }
}
