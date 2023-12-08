using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UrbanUI.WPF.Tools
{
   public static class VisualTreeHelperExtensions
   {
      public static IEnumerable<UIElement> FindVisualChildrenOfChildren(DependencyObject depObj)
      {
         if (depObj != null)
         {
            int count = VisualTreeHelper.GetChildrenCount(depObj);
            for (int i = 0; i < count; i++)
            {
               DependencyObject child = VisualTreeHelper.GetChild(depObj, i) as DependencyObject;
               if (child != null)
               {
                  if(child is UIElement element)
                  {
                     yield return element;
                  }

                  foreach (var grandchild in FindVisualChildrenOfChildren(child))
                  {
                     yield return grandchild;
                  }
               }
            }

            // Consider the Content property of ContentPresenter
            if (depObj is ContentPresenter contentPresenter)
            {
               object content = contentPresenter.Content;
               if (content is DependencyObject contentChild)
               {
                  foreach (var grandchild in FindVisualChildrenOfChildren(contentChild))
                  {
                     yield return grandchild;
                  }
               }
            }
         }
      }
   }
}
