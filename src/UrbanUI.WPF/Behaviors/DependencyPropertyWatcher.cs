using System;
using System.ComponentModel;
using System.Windows;

namespace UrbanUI.WPF.Behaviors
{

   public class DependencyPropertyWatcher
   {
      private readonly DependencyObject _target;

      public DependencyPropertyWatcher(DependencyObject target)
      {
         _target = target;
      }

      public void Watch(DependencyProperty property, EventHandler handler)
      {
         var descriptor = DependencyPropertyDescriptor.FromProperty(property, _target.GetType());
         descriptor?.AddValueChanged(_target, handler);
      }

      public void Unwatch(DependencyProperty property, EventHandler handler)
      {
         var descriptor = DependencyPropertyDescriptor.FromProperty(property, _target.GetType());
         descriptor?.RemoveValueChanged(_target, handler);
      }
   }

}
