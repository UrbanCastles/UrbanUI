using UrbanUI.WPF.Themes;
using System.Windows;
using System.Windows.Media;

namespace UrbanUI.WPF.Controls
{

   public class ScrollViewer : System.Windows.Controls.ScrollViewer, ITheme
   {
      #region Depedencies
      public static readonly DependencyProperty ScrollBrushProperty =
          DependencyProperty.Register("ScrollBrush", typeof(Brush), typeof(ScrollViewer), new PropertyMetadata(Brushes.Gray));

      public static readonly DependencyProperty ScrollTrackBrushProperty =
          DependencyProperty.Register("ScrollTrackBrush", typeof(Brush), typeof(ScrollViewer), new PropertyMetadata(Brushes.Transparent));


      #endregion Depedencies

      #region Dependency Properties
      public Brush ScrollBrush
      {
         get { return (Brush)GetValue(ScrollBrushProperty); }
         set { SetValue(ScrollBrushProperty, value); }
      }
      public Brush ScrollTrackBrush
      {
         get { return (Brush)GetValue(ScrollTrackBrushProperty); }
         set { SetValue(ScrollTrackBrushProperty, value); }
      }
      #endregion Dependency Properties



      private Theme _theme;

      static ScrollViewer()
      {
         //DefaultStyleKeyProperty.OverrideMetadata(typeof(ScrollViewer), new FrameworkPropertyMetadata(typeof(ScrollViewer)));
      }

      public override void OnApplyTemplate()
      {
         base.OnApplyTemplate();
         if (_theme == null)
         {
            _theme = Defaults.getDefaultThemeSetups();
         }
         SetThemeUI(_theme);
      }

      public Theme GetTheme()
      {
         return _theme;
      }

      public void SetThemeUI(Theme theme, bool UpdateContentThemes = false)
      {
         _theme = theme;

         this.ScrollBrush = theme.ItemHighlightBackground;
         this.ScrollTrackBrush = Brushes.Transparent;

      }
   }
}
