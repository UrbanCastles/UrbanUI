using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UrbanUI.WPF.Themes;

namespace UrbanUI.WPF.Controls
{
   [TemplatePart(Name = "PART_mainGrid", Type = typeof(System.Windows.Controls.Grid))]
   [TemplatePart(Name = "PART_mainBorder", Type = typeof(System.Windows.Controls.Border))]
   internal partial class HighlightMarker : System.Windows.Controls.ContentControl, ITheme
   {

      private System.Windows.Controls.Grid _mainGrid;
      private System.Windows.Controls.Border _mainBorder;
      private Theme _theme;
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
               if(_unfinishedAnimState.Value == MarkerState.ToggleOn)
               {
                  ToggleOn();
               }
               else
               {
                  ToggleOff();
               }
            }
         };
      }

      #region Dependency Properties

      #region DP: Toggle
      public bool Toggle
      {
         get { return (bool)GetValue(ToggleProperty); }
         set { SetValue(ToggleProperty, value); }
      }
      public static readonly DependencyProperty ToggleProperty = DependencyProperty.Register(nameof(Toggle), typeof(bool), typeof(HighlightMarker), new PropertyMetadata(false, (d, p) =>
      {
         var _this = (HighlightMarker)d;
         if (_this.Toggle)
            _this.ToggleOn();
         else
            _this.ToggleOff();
      }));
      #endregion DP: Toggle

      #region DP: CornerRadius
      public CornerRadius CornerRadius
      {
         get { return (CornerRadius)GetValue(CornerRadiusProperty); }
         set { SetValue(CornerRadiusProperty, value); }
      }
      public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(HighlightMarker), new PropertyMetadata(new CornerRadius(4)));
      #endregion DP: CornerRadius

      #region DP: Orientation
      public Orientation Orientation
      {
         get { return (Orientation)GetValue(OrientationProperty); }
         set { SetValue(OrientationProperty, value); }
      }
      public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(HighlightMarker), new PropertyMetadata(Orientation.Vertical));
      #endregion DP: Orientation

      #endregion Dependency Properties

      #region Public Methods
      public void ToggleOn(double millisecSpeed = 170)
      {
         Animate(MarkerState.ToggleOn, millisecSpeed);
      }
      public void ToggleOff(double millisecSpeed = 170)
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
      private void Animate(MarkerState markerState, double millisecSpeed)
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

         DoubleAnimation sizeAnimation;
         //ObjectAnimationUsingKeyFrames visibilityAnimation;

         if (markerState is MarkerState.ToggleOn)
         {
            sizeAnimation = new DoubleAnimation()
            {
               From = this.Orientation == Orientation.Vertical ? GetHeight(_mainBorder) : GetWidth(_mainBorder),
               To = this.Orientation == Orientation.Vertical ? GetBorderMaxHeight() : GetBorderMaxWidth(),
               Duration = TimeSpan.FromMilliseconds(millisecSpeed)
            };
         }
         else
         {
            sizeAnimation = new DoubleAnimation()
            {
               From = this.Orientation == Orientation.Vertical ? GetBorderMaxHeight() : GetBorderMaxWidth(),
               To = 0,
               Duration = TimeSpan.FromMilliseconds(millisecSpeed)
            };
         }

         Storyboard storyboard = new Storyboard();
         storyboard.Children.Add(sizeAnimation);

         Storyboard.SetTarget(sizeAnimation, _mainBorder);
         if(this.Orientation == Orientation.Horizontal)
         {
            Storyboard.SetTargetProperty(sizeAnimation, new PropertyPath("Width"));
         }
         else
         {
            Storyboard.SetTargetProperty(sizeAnimation, new PropertyPath("Height"));
         }

         storyboard.Begin();
      }
      #endregion Animations

      #region Width and Height Fetcher
      private double GetBorderMaxWidth()
      {
         double mainGridWidth = _mainGrid.Width is double.NaN ? _mainGrid.ActualWidth : _mainGrid.Width;

         return mainGridWidth * .60;
      }
      private double GetWidth(FrameworkElement element)
      {
         return element.Width is double.NaN ? element.ActualWidth : element.Width;
      }

      private double GetBorderMaxHeight()
      {
         double mainGridHeight = _mainGrid.Height is double.NaN ? _mainGrid.ActualHeight : _mainGrid.Height;

         return mainGridHeight * .60;
      }
      private double GetHeight(FrameworkElement element)
      {
         return element.Height is double.NaN ? element.ActualHeight : element.Height;
      }
      #endregion Width and Height Fetcher

      #region Themeing
      public Theme GetTheme()
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
