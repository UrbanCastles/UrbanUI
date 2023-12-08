
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using UrbanUI.WPF.Themes;

namespace UrbanUI.WPF.Controls
{

   public partial class SlideUpGrid : UserControl, ITheme
   {
      private Theme _theme;


      #region Dependency Properties

      #region Properties
      public SlideDirection Direction { get; set; } = SlideDirection.Up;
      public bool OpacityOnSlide { get; set; } = true;
      public double AnimationSpeed { get; set; } = 350.0;
      #endregion Properties

      #endregion Dependency Properties


      public SlideUpGrid()
      {
         InitializeComponent();
      }
      public SlideUpGrid(Theme theme)
      {
         InitializeComponent();
         SetThemeUI(theme, true);
      }

      #region Interface Methods
      public Theme GetTheme()
      {
         return _theme;
      }

      public void SetThemeUI(Theme theme, bool UpdateContentThemes = false)
      {
         _theme = theme;
      }
      #endregion Interface Methods

      private void contentPresenter_ContentChanged(object sender, DependencyPropertyChangedEventArgs e)
      {
         SlideAnimate();
      }

      #region Animations
      private void SlideAnimate()
      {
         var contentElement = this.Content as FrameworkElement;
         var mainWindow = Application.Current.MainWindow;

         if (contentElement != null)
         {
            TranslateTransform translateTransform = new TranslateTransform();
            contentElement.RenderTransform = translateTransform;

            DoubleAnimation slideAnimation = new DoubleAnimation()
            {
               Duration = TimeSpan.FromMilliseconds(AnimationSpeed),
               EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut },
               FillBehavior = FillBehavior.Stop
            };

            var axisOfSlide = string.Empty;
            if(Direction == SlideDirection.Down || Direction == SlideDirection.Up)
            {
               slideAnimation.From = Direction == SlideDirection.Down ? 0 - (mainWindow.Height / 2) : mainWindow.Height / 2;
               slideAnimation.To = 0;
               axisOfSlide = "Y";
            }
            else
            {
               slideAnimation.From = Direction == SlideDirection.Right ? 0 - (mainWindow.Width / 2) : mainWindow.Width / 2;
               slideAnimation.To = 0;
               axisOfSlide = "X";
            }

            DoubleAnimation fadeInAnimation = new DoubleAnimation()
            {
               From = 0,
               To = 1,
               Duration = TimeSpan.FromMilliseconds(AnimationSpeed),
               EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut },
               FillBehavior = FillBehavior.Stop
            };

            Storyboard storyboard = new Storyboard();

            Storyboard.SetTarget(slideAnimation, contentElement);
            Storyboard.SetTargetProperty(slideAnimation, new PropertyPath($"(FrameworkElement.RenderTransform).(TranslateTransform.{axisOfSlide})"));
            storyboard.Children.Add(slideAnimation);

            if(OpacityOnSlide)
            {
               Storyboard.SetTarget(fadeInAnimation, contentElement);
               Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath(FrameworkElement.OpacityProperty));
               storyboard.Children.Add(fadeInAnimation);
            }

            storyboard.Completed += delegate
            {
               contentElement.BeginAnimation((FrameworkElement.RenderTransformProperty as DependencyProperty), null);
               if(OpacityOnSlide)
               {
                  contentElement.BeginAnimation(FrameworkElement.OpacityProperty, null);
               }
            };

            storyboard.Begin();
         }
      }

      #endregion Animations
   }
}
