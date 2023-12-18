using System.Collections.Generic;
using System.Windows;

namespace UrbanUI.WPF.Controls;

public class MenuItemGrouper : DependencyObject
{
   public static Dictionary<MenuItem, string> ElementToGroupNames = new Dictionary<MenuItem, string>();

   public static readonly DependencyProperty GroupNameProperty = DependencyProperty.RegisterAttached("GroupName", typeof(string), typeof(MenuItemGrouper), new PropertyMetadata(string.Empty, OnGroupNameChanged));

   private static bool _checkTriggerIsFromRechecking = false;
   private static MenuItem? _checkTriggerSource;
   private static bool _uncheckTriggerIsFromGroupUnchecking = false;

   public static void SetGroupName(MenuItem element, string value)
   {
      element.SetValue(GroupNameProperty, value);
   }

   public static string GetGroupName(MenuItem element)
   {
      return element.GetValue(GroupNameProperty).ToString();
   }

   private static void OnGroupNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
   {
      var menuItem = d as MenuItem;

      if (menuItem != null)
      {
         string newGroupName = e.NewValue.ToString();
         string oldGroupName = e.OldValue.ToString();
         if (string.IsNullOrEmpty(newGroupName))
         {
            RemoveCheckboxFromGrouping(menuItem);
         }
         else
         {
            if (newGroupName != oldGroupName)
            {
               if (!string.IsNullOrEmpty(oldGroupName))
               {
                  RemoveCheckboxFromGrouping(menuItem);
               }
               ElementToGroupNames.Add(menuItem, e.NewValue.ToString());
               menuItem.Checked += MenuItemChecked;
               menuItem.Unchecked += MenuItemUnChecked;
            }
         }
      }
   }

   private static void RemoveCheckboxFromGrouping(MenuItem menuItem)
   {
      ElementToGroupNames.Remove(menuItem);
      menuItem.Checked -= MenuItemChecked;
      menuItem.Unchecked -= MenuItemUnChecked;
   }

   static void MenuItemUnChecked(object sender, RoutedEventArgs e)
   {
      var menuItem = sender as MenuItem;
      if(menuItem.HoldUncheckingWhenGrouped && !_uncheckTriggerIsFromGroupUnchecking)
      {
         _checkTriggerSource = menuItem;
         _checkTriggerIsFromRechecking = true;
         menuItem.IsChecked = true;
      }

      _uncheckTriggerIsFromGroupUnchecking = false;
   }

   static void MenuItemChecked(object sender, RoutedEventArgs e)
   {
      var menuItem = e.OriginalSource as MenuItem;
      if (_checkTriggerIsFromRechecking && menuItem.HoldUncheckingWhenGrouped && menuItem == _checkTriggerSource)
      {
         _checkTriggerIsFromRechecking = false;
         return;
      }
      foreach (var item in ElementToGroupNames)
      {
         if (item.Key != menuItem && item.Value == GetGroupName(menuItem) && item.Key.IsChecked)
         {
            _uncheckTriggerIsFromGroupUnchecking = true;
            item.Key.IsChecked = false;
         }
      }
   }
}
