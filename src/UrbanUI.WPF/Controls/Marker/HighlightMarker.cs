using System;
using System.Windows;
using System.Windows.Media.Animation;
using UrbanUI.WPF.Themes;

namespace UrbanUI.WPF.Controls
{
   [TemplatePart(Name = "PART_mainGrid", Type = typeof(System.Windows.Controls.Grid))]
   [TemplatePart(Name = "PART_mainBorder", Type = typeof(System.Windows.Controls.Border))]
   internal partial class HighlightMarker : System.Windows.Controls.ContentControl, ITheme
   {

      private System.Windows.Controls.Grid? _mainGrid;
      private System.Windows.Controls.Border? _mainBorder;
      private Theme? _theme;
      private bool _templateApplied = false;
      private bool _hasUnfinishedSetupAnim = false;
      private MarkerState? _unfinishedAnimState = null;

      public HighlightMarker()
      {
         //DefaultStyleKeyProperty.OverrideMetadata(typeof(HighlightMarker), new FrameworkPropertyMetadata(typeof(HighlightMarker)));

         this.Loaded += delegate
         {
            if(_hasUnfinishedSetupAnim && _unfinishedAnimState != null)
            {
               Animate(_unfinishedAnimState.Value);
            }
         };
      }

      #region Public Methods
      public void ToggleOn(double millisecSpeed = 300)
      {
         Animate(MarkerState.ToggleOn, millisecSpeed);
      }
      public void ToggleOff(double millisecSpeed = 300)
      {
         Animate(MarkerState.ToggleOff, millisecSpeed);
      }
      #endregion Public Methods

      #region Apply Templates
      public override void OnApplyTemplate()
      {
         _mainGrid = GetTemplateChild("PART_mainGrid") as System.Windows.Controls.Grid;
         _mainBorder = GetTemplateChild("PART_mainBorder") as System.Windows.Controls.Border;

         base.OnApplyTemplate();

         if ( _mainGrid != null && _mainBorder != null)
         {
            _templateApplied = true;
            SetThemeUI(_theme);
         }
      }
      #endregion Apply Templates

      #region Animations
      private void Animate(MarkerState markerState, double millisecSpeed = 300)
      {
         if (!_templateApplied)
         {
            _hasUnfinishedSetupAnim = true;
            _unfinishedAnimState = markerState;
            return;
         }
         else
         {
            _unfinishedAnimState = null;
            _hasUnfinishedSetupAnim = false;
         }

         DoubleAnimation heightAnimation;
         //ObjectAnimationUsingKeyFrames visibilityAnimation;

         if (markerState is MarkerState.ToggleOn)
         {
            heightAnimation = new DoubleAnimation()
            {
               From = GetHeight(_mainBorder),
               To = GetBorderMaxHeight(),
               Duration = TimeSpan.FromMilliseconds(millisecSpeed)
            };
         }
         else
         {
            heightAnimation = new DoubleAnimation()
            {
               From = GetBorderMaxHeight(),
               To = 0,
               Duration = TimeSpan.FromMilliseconds(millisecSpeed)
            };
         }

         ElasticEase easingFunction = new ElasticEase
         {
            EasingMode = EasingMode.EaseOut,
            Oscillations = 2,
            Springiness = 2
         };

         heightAnimation.EasingFunction = easingFunction;

         Storyboard storyboard = new Storyboard();
         storyboard.Children.Add(heightAnimation);

         Storyboard.SetTarget(heightAnimation, _mainBorder);
         Storyboard.SetTargetProperty(heightAnimation, new PropertyPath("Height"));

         storyboard.Begin();
      }
      #endregion Animations

      #region Height Fetcher
      private double GetBorderMaxHeight()
      {
         double mainGridHeight = _mainGrid.Height is double.NaN ? _mainGrid.ActualHeight : _mainGrid.Height;

         return mainGridHeight * .60;
      }
      private double GetHeight(FrameworkElement element)
      {
         return element.Height is double.NaN ? element.ActualHeight : element.Height;
      }
      #endregion Height Fetcher

      #region Themeing
      public Theme? GetTheme()
      {
         return _theme;
      }

      public void SetThemeUI(Theme theme, bool UpdateContentThemes = false)
      {
         _theme = theme;
      }
      #endregion Themeing
   }

   internal enum MarkerState
   {
      ToggleOn,
      ToggleOff
   }
}
