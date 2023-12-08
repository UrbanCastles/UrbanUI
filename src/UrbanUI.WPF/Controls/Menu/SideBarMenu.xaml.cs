
using UrbanUI.WPF.ControlGenerators;
using UrbanUI.WPF.HybridControls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using UrbanUI.WPF.Tools;

namespace UrbanUI.WPF.Controls
{
   /// <summary>
   /// Interaction logic for SideBarMenu.xaml
   /// </summary>
   /// 
   [ContentProperty("MenuItems")]
   public partial class SideBarMenu : UserControl
   {
      #region Dependency Properties

      #region DP: MenuItems
      public ObservableList<SideBarMenuItem> MenuItems = new ObservableList<SideBarMenuItem>();
      #endregion DP: MenuItems

      #endregion Dependency Properties

      #region Routing Events
      private void RaiseMenuSelectedEvent(object pageToShow)
      {
         Dispatcher.Invoke(() =>
         {
            var eventArgs = new MenuSelectedEventArgs(MenuSelectedEvent, this, pageToShow);
            RaiseEvent(eventArgs);
         });
      }

      #region RE: MenuSelected
      public static readonly RoutedEvent MenuSelectedEvent =
          EventManager.RegisterRoutedEvent("MenuSelected", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SideBarMenu));

      public event RoutedEventHandler MenuSelected
      {
         add { AddHandler(MenuSelectedEvent, value); }
         remove { RemoveHandler(MenuSelectedEvent, value); }
      }
      #endregion RE: MenuSelected

      #endregion Routing Events


      public SideBarMenu()
      {
         InitializeComponent();

         this.Unloaded += (s, e) =>
         {
            MenuItems.Clear();
         };

         MenuItems.CollectionChanged += (s, e) =>
         {
            menuContents.Children.Clear();
            subMenuContents.Children.Clear();

            if (MenuItems != null)
            {
               foreach (SideBarMenuItem item in MenuItems)
               {
                  if (item.IsActiveFocusableMenu && !item.MenuTriggerFocusAlreadyAdded)
                  {
                     item.Selected += (s, e) =>
                     {
                        Dispatcher.Invoke(() =>
                        {
                           ActivateOnlyOne(s);
                        });
                     };
                     item.MenuTriggerFocusAlreadyAdded = true;
                  }

                  if (item.IsSubMenuItem)
                     subMenuContents.Children.Add(item);
                  else
                     menuContents.Children.Add(item);
               }
            }
         };
      }

      private void ActivateOnlyOne(object sender)
      {
         foreach (SideBarMenuItem item in MenuItems)
         {
            if ((SideBarMenuItem)sender != item)
               item.IsActiveSelected = false;
         }
      }



      #region Menu Methods
      public SideBarMenuItem GetAMenuItem(string menuText)
      {
         foreach (var menu in MenuItems)
         {
            if (menu.Text == menuText) return menu;
         }

         return null;
      }


      public void SetActivateMenu(int Index)
      {
         if (MenuItems.Count+1 >= Index && Index >= 0)
         {
            var item = MenuItems[Index];
            if (!item.IsActiveSelected)
            {
               item.IsActiveSelected = true;
            }
         }
      }


      public void SetActivateMenu(string menuText)
      {
         if(MenuItems.Count > 0)
         {
            foreach (SideBarMenuItem item in MenuItems)
            {
               if(item.Text == menuText && !item.IsActiveSelected)
               {
                  item.IsActiveSelected = true;
               }
            }
         }
      }

      /// <summary>
      /// Create set of Menus by given dictionary. key will be the text of the Menu, and the second string will be the icon (based on icon library json)
      /// </summary>
      /// <param name="menus"></param>
      /// <param name="theme"></param>
      public void CreateMenu(List<MenuSetup> menus)
      {
         MenuItems.Clear();
         foreach (var item in menus)
         {
            var generatedMenu = ControlGenerator.GenerateMenuItem(item.Theme, item.Icon, item.MenuText, item.IsFooterMenu, item.IsFocusable);
            MenuItems.Add(generatedMenu);
            generatedMenu.Selected += delegate
            {
               RaiseMenuSelectedEvent(item.Page);
            };
         }
      }

      #endregion Menu Methods
   }
}
