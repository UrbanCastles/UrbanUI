using System.Windows;

namespace UrbanUI.WPF.Controls
{

   public class MenuSelectedEventArgs : RoutedEventArgs
   {
      public object PageToShow { get; }

      public MenuSelectedEventArgs(RoutedEvent routedEvent, object source, object pageToShow)
          : base(routedEvent, source)
      {
         PageToShow = pageToShow;
      }
   }

   public class MenuItemToggledEventArgs : RoutedEventArgs
   {
      public bool IsActive { get; }

      public MenuItemToggledEventArgs(RoutedEvent routedEvent, object source, bool isActive)
          : base(routedEvent, source)
      {
         IsActive = isActive;
      }
   }
}
