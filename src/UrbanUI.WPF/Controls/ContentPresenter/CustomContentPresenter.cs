
using System.Windows;
using System.Windows.Controls;

namespace UrbanUI.WPF.Controls
{

   public class CustomContentPresenter : ContentPresenter
   {
      static CustomContentPresenter()
      {
         ContentProperty.OverrideMetadata(typeof(CustomContentPresenter), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnContentChanged)));
      }

      private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
      {
         var mcc = d as CustomContentPresenter;
         mcc?.ContentChanged?.Invoke(mcc, new DependencyPropertyChangedEventArgs(ContentProperty, e.OldValue, e.NewValue));
      }

      public event DependencyPropertyChangedEventHandler ContentChanged;
   }
}
