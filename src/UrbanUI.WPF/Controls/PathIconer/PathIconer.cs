using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UrbanUI.WPF.Controls;

public partial class PathIconer: Control
{
   #region DP: DataPath
   public Geometry DataPath
   {
      get { return (Geometry)GetValue(DataPathProperty); }
      set { SetValue(DataPathProperty, value); }
   }
   public static readonly DependencyProperty DataPathProperty = DependencyProperty.Register(nameof(DataPath), typeof(Geometry), typeof(PathIconer), new PropertyMetadata(null));
   #endregion DP: DataPath

   #region DP: Fill
   public Brush Fill
   {
      get { return (Brush)GetValue(FillProperty); }
      set { SetValue(FillProperty, value); }
   }
   public static readonly DependencyProperty FillProperty = DependencyProperty.Register(nameof(Fill), typeof(Brush), typeof(PathIconer), new PropertyMetadata(Brushes.White));
   #endregion DP:Fill
}
