using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UrbanUI.WPF.Themes;

namespace UrbanUI.WPF.Controls;

public partial class Menu : System.Windows.Controls.Menu, ITheme
{

   #region Dependency Properties

   #region DP: Background
   public new Brush Background
   {
      get { return (Brush)GetValue(BackgroundProperty); }
      set { SetValue(BackgroundProperty, value); }
   }
   public static new readonly DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(Menu), new PropertyMetadata((Brush)new BrushConverter().ConvertFromString("#0078d7")));


   #endregion DP:Background

   #region DP: Orientation
   public Orientation Orientation
   {
      get { return (Orientation)GetValue(OrientationProperty); }
      set { SetValue(OrientationProperty, value); }
   }
   public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(Menu), new PropertyMetadata(Orientation.Vertical));
   #endregion DP: Orientation

   #region DP: CornerRadius
   public CornerRadius CornerRadius
   {
      get { return (CornerRadius)GetValue(CornerRadiusProperty); }
      set { SetValue(CornerRadiusProperty, value); }
   }
   public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(Menu), new PropertyMetadata(new CornerRadius(0)));
   #endregion DP: CornerRadius

   #region DP: IconTextOrientation
   public Orientation IconTextOrientation
   {
      get { return (Orientation)GetValue(IconTextOrientationProperty); }
      set { SetValue(IconTextOrientationProperty, value); }
   }
   public static readonly DependencyProperty IconTextOrientationProperty = DependencyProperty.Register(nameof(IconTextOrientation), typeof(Orientation), typeof(Menu), new PropertyMetadata(Orientation.Horizontal));
   #endregion DP: IconTextOrientation

   #region DP: ShowMenuSelectionMarker
   public bool ShowSelectionMarker
   {
      get { return (bool)GetValue(ShowSelectionMarkerProperty); }
      set { SetValue(ShowSelectionMarkerProperty, value); }
   }
   public static readonly DependencyProperty ShowSelectionMarkerProperty = DependencyProperty.Register(nameof(ShowSelectionMarker), typeof(bool), typeof(Menu), new PropertyMetadata(true));
   #endregion DP: ShowSelectionMarker

   #endregion Dependency Properties

   private Theme? _theme;
   public Menu() { }

   #region Themeing
   public void SetThemeUI(Theme theme, bool UpdateContentThemes = false)
   {
      _theme = theme;

      var menuItems_TopHeader = this.GetTopLevelMenus();
      foreach(var menuItem in menuItems_TopHeader)
      {
         menuItem.Background = _theme.MenuBackground;
         menuItem.Foreground = _theme.MenuForeground;
         menuItem.MouseEnterBackground = _theme.MenuHighlightBackground;
         menuItem.MouseEnterForeground = _theme.MenuHighlightForeground;
         menuItem.MousePressedBackground = _theme.MenuFocusBackground;
         menuItem.MousePressedForeground = _theme.MenuFocusForeground;
         menuItem.CheckedBackground = _theme.MenuFocusBackground;
         menuItem.CheckedForeground = _theme.MenuFocusForeground;
         menuItem.FocusMarkerColor = _theme.MenuFocusMarkerColor;
         menuItem.IconColor = _theme.MenuForeground;
         menuItem.MouseEnterIconColor = _theme.MenuForeground;
         menuItem.PressedIconColor = menuItem.CheckedIconColor = _theme.MenuFocusIconColor;
      }   
   }

   public Theme GetTheme()
   {
      return _theme;
   }
   #endregion Themeing
}


internal static class MenuHelper
{
   internal static IEnumerable<MenuItem> GetTopLevelMenus(this Menu menu)
   {
      var menuItems = menu.Items.Cast<MenuItem>().ToList();
      foreach (var menuItem in menuItems)
      {
         if (menuItem.Role == MenuItemRole.TopLevelHeader || menuItem.Role == MenuItemRole.TopLevelItem)
         {
            yield return menuItem;
         }
      }
   }
}
