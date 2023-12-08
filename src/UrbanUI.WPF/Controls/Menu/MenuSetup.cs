using System;

using UrbanUI.WPF.Themes;

namespace UrbanUI.WPF.Controls
{
   public class MenuSetup
   {
      public MenuSetup() { }
      public MenuSetup(string menuText, Icon icon, Theme theme, bool isFooterMenu = false, bool isFocusable = true, Object page = null)
      {
         this.MenuText = menuText;
         this.Icon = icon;
         this.IsFooterMenu = isFooterMenu;
         this.IsFocusable = isFocusable;
         this.Theme = theme;
         this.Page = page;
      }

      public string MenuText { get; set; }
      public bool IsFooterMenu { get; set; } = false;
      public bool IsFocusable { get; set; } = true;
      public Theme Theme { get; set; }
      public Object Page { get; set; }
      public Icon Icon { get; set; }
   }
}
