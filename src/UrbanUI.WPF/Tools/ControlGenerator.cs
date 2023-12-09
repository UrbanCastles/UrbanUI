using System;
using System.Windows;
using System.Windows.Media.Imaging;
using UrbanUI.WPF.Controls;
using UrbanUI.WPF.Themes;

namespace UrbanUI.WPF.ControlGenerators
{
   public static class ControlGenerator
   {


      public static Card generateCard(Theme theme, string Title, string subtitle, string message, double? width = null, double? height = null, bool IsEnabled = true, string uriImgSource = null, Icon icon = null)
      {
         var card = new Card();
         card.Width = width != null ? width.Value : card.Width;
         card.Height = height != null ? height.Value : card.Height;
         card.TitleText = Title;
         card.SubText = subtitle;
         card.MessageText = message;
         card.ImageBackgroundColor = theme.MenuFocusIconColor;
         if(icon != null)
         {
            card.IconPath = icon.StrokePath;
            card.IconFlipVertically = icon.FlipY;
            card.IconFlipHorizontally = icon.FlipX;
         }
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




      public static SideBarMenuItem GenerateMenuItem(Theme theme, Icon icon, string Text, bool IsSubMenu = false, bool IsFocusable = true)
      {
         if (icon == null)
            icon = new Icon();

         var sidebarMenuItem = new SideBarMenuItem()
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

      public static Card GenerateThemeCard(Theme theme, bool IsDarkTheme = false)
      {
         if (theme == null)
            return new Card();

         var newThemeCard = new Card()
         {
            Width = 80.0,
            MinHeight = 110.0,
            TitleText = theme.Name,
            MessageText = theme.Details,
            ImageBackgroundColor = IsDarkTheme ? theme.MenuFocusForeground : theme.MenuFocusBackground,
            Margin = new Thickness(5),
            ImageMargin = new Thickness(20),
            FontSize = 11,
            MessageTextFontSize = 10,
            MessageTextFontWeight = FontWeights.Light,
            IconBorderMinWidth = 40,
            IconBorderMinHeight = 60
         };
         return newThemeCard;
      }

   }
}
