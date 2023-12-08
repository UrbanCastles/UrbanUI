using System;
using System.Windows;
using System.Windows.Media.Imaging;
using UrbanUI.WPF.Themes;

namespace UrbanUI.WPF.ControlGenerators
{
   public static class ControlGenerator
   {


      public static UrbanUI.WPF.Controls.Cards generateCard(Theme theme, string Title, string subtitle, string message, double? width = null, double? height = null, bool IsEnabled = true, string uriImgSource = null, Icon icon = null)
      {
         var card = new UrbanUI.WPF.Controls.Cards();
         card.Width = width != null ? width.Value : card.Width;
         card.Height = height != null ? height.Value : card.Height;
         card.TitleText = Title;
         card.SubText = subtitle;
         card.MessageText = message;
         card.ImageBackgroundColor = theme.MenuFocusIconColor;
         card.Icon = icon;
         card.IconColor = theme.ThemeBackground;
         card.ImageMargin = new Thickness(40);

         if (!string.IsNullOrWhiteSpace(uriImgSource))
         {
            string baseUri = !uriImgSource.StartsWith(@"pack://application:,,,") ? @"pack://application:,,," + uriImgSource : uriImgSource;
            Uri img = new Uri(baseUri);
            card.ImageSource = new BitmapImage(img);
         }
         card.IsEnabled = IsEnabled;
         card.SetThemeUI(theme);
         return card;
      }




      public static UrbanUI.WPF.Controls.SideBarMenuItem GenerateMenuItem(Theme theme, Icon icon, string Text, bool IsSubMenu = false, bool IsFocusable = true)
      {
         if (icon == null)
            icon = new Icon();

         var sidebarMenuItem = new UrbanUI.WPF.Controls.SideBarMenuItem()
         {
            Width = 70,
            Height = 70,
            IconPath = icon?.StrokePath,
            FocusedIconPath = icon?.FilledPath,
            IsSubMenuItem = IsSubMenu,
            IsActiveFocusableMenu = IsFocusable,
            Text = Text,
            FlipIconHorizontally = icon.FlipX,
            FlipIconVertically = icon.FlipY,
            FontSize = 10,
            IconWidth = 20,
            IconHeight = 20,
            TextMargin = new Thickness(0, 4,0,0)
         };
         sidebarMenuItem.SetThemeUI(theme);
         return sidebarMenuItem;
      }

      public static UrbanUI.WPF.Controls.Cards GenerateThemeCard(Theme theme, bool IsDarkTheme = false)
      {
         if (theme == null)
            return new UrbanUI.WPF.Controls.Cards();

         var newThemeCard = new UrbanUI.WPF.Controls.Cards()
         {
            Width = 80.0,
            Height = 110.0,
            TitleText = theme.Name,
            MessageText = theme.Details,
            ImageBackgroundColor = IsDarkTheme ? theme.MenuFocusForeground : theme.MenuFocusBackground,
            Margin = new Thickness(5),
            FontSize = 11,
            MessageTextFontSize = 10,
            MessageTextFontWeight = FontWeights.Light
         };
         return newThemeCard;
      }

   }
}
