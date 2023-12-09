using System.Windows;

namespace UrbanUI.WPF.Controls
{

   public class ContentPresenter : System.Windows.Controls.ContentPresenter
   {
      static ContentPresenter()
      {
         ContentProperty.OverrideMetadata(typeof(ContentPresenter), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnContentChanged)));
      }

      private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
      {
         var mcc = d as ContentPresenter;
         mcc?.ContentChanged?.Invoke(mcc, new DependencyPropertyChangedEventArgs(ContentProperty, e.OldValue, e.NewValue));
      }

      public event DependencyPropertyChangedEventHandler ContentChanged;
   }
}
