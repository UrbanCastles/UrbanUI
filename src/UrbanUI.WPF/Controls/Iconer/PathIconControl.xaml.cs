using UrbanUI.WPF.Tools;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UrbanUI.WPF.Controls
{
   /// <summary>
   /// Interaction logic for PathIconControl.xaml
   /// </summary>
   public partial class PathIconControl : UserControl
   {
      #region DP: Path
      public string Path
      {
         get { return (string)GetValue(PathProperty); }
         set { SetValue(PathProperty, value); }
      }
      public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register("Path", typeof(string), typeof(PathIconControl), new PropertyMetadata(string.Empty, (d, t) =>
            {
               var PathIconControl = (PathIconControl)d;
               PathIconControl.SetIconDetails();
            }));
      #endregion DP: Path

      #region DP: Color
      public Brush Color
      {
         get { return (Brush)GetValue(ColorProperty); }
         set { SetValue(ColorProperty, value); }
      }

      // Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty ColorProperty =
          DependencyProperty.Register("Color", typeof(Brush), typeof(PathIconControl), new PropertyMetadata(Brushes.White, new PropertyChangedCallback((d, t) =>
          {
             var PathIconControl = (PathIconControl)d;
             PathIconControl.SetIconDetails();
          })));


      #endregion DP:Color

      #region DP: FlipVertically
      public bool FlipVertically
      {
         get { return (bool)GetValue(FlipVerticallyProperty); }
         set { SetValue(FlipVerticallyProperty, value); }
      }

      // Using a DependencyProperty as the backing store for FlipVertically.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty FlipVerticallyProperty =
          DependencyProperty.Register("FlipVertically", typeof(bool), typeof(PathIconControl), new PropertyMetadata(false, (d, t) =>
          {
             var PathIconControl = (PathIconControl)d;
             PathIconControl.SetIconDetails();

          }));
      #endregion DP:FlipVertically

      #region DP: FlipHorizontally
      public bool FlipHorizontally
      {
         get { return (bool)GetValue(FlipHorizontallyProperty); }
         set { SetValue(FlipHorizontallyProperty, value); }
      }

      // Using a DependencyProperty as the backing store for FlipVertically.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty FlipHorizontallyProperty =
          DependencyProperty.Register("FlipHorizontally", typeof(bool), typeof(PathIconControl), new PropertyMetadata(false, (d, t) =>
          {
             var PathIconControl = (PathIconControl)d;
             PathIconControl.SetIconDetails();

          }));
      #endregion DP:FlipHorizontally

      public PathIconControl()
      {
         InitializeComponent();
      }


      private void SetIconDetails()
      {
         PathGeometryTool.SetIconDetails(this.iconPath, this.Path, this.Color, this.FlipHorizontally, this.FlipVertically);
      }


   }
}
