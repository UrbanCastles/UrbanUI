using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UrbanUI.WPF.Themes;
using UrbanUI.WPF.Tools;

namespace UrbanUI.WPF.Controls
{
   [TemplatePart(Name = "contentGrid", Type = typeof(Grid))]
   [TemplatePart(Name = "collapseController", Type = typeof(SideBarMenuItem))]
   public partial class CollapsibleStackPane : UserControl, ITheme
   {
      private Theme _theme;
      private bool _isControllerInitialized = false;
      private SideBarMenuItem _collapseController;
      private Grid _contentGrid;
      private bool IsThereAwaitingThemeUpdateForContents = false;
      private bool _isAnimating = false;
      private bool _isExpanded = false;


      #region Dependency Properties

      #region IsExpanded
      public bool IsExpanded
      {
         get { return (bool)GetValue(IsExpandedProperty); }
         set { SetValue(IsExpandedProperty, value); }
      }

      public static readonly DependencyProperty IsExpandedProperty =
          DependencyProperty.Register("IsExpanded", typeof(bool), typeof(CollapsibleStackPane), new PropertyMetadata(true, (d, t) =>
          {
             var collapseStackPane = d as CollapsibleStackPane;
             if (collapseStackPane._collapseController != null)
             {
                collapseStackPane._collapseController.IsActiveSelected = collapseStackPane.IsExpanded;
             }
          }));
      #endregion IsExpanded

      #region Text
      public string Text
      {
         get { return (string)GetValue(TextProperty); }
         set { SetValue(TextProperty, value); }
      }

      public static readonly DependencyProperty TextProperty =
          DependencyProperty.Register("Text", typeof(string), typeof(CollapsibleStackPane), new PropertyMetadata("", (d, t) =>
          {
             var collapseStackPane = d as CollapsibleStackPane;
             if(collapseStackPane._collapseController != null)
             {
                collapseStackPane._collapseController.Text = collapseStackPane.Text;
             }
          }));
      #endregion Text

      #region AnimationSpeed
      public double AnimationSpeed
      {
         get { return (double)GetValue(AnimationSpeedProperty); }
         set { SetValue(AnimationSpeedProperty, value); }
      }
      public static readonly DependencyProperty AnimationSpeedProperty =
          DependencyProperty.Register("AnimationSpeed", typeof(double), typeof(CollapsibleStackPane), new PropertyMetadata(300.0));
      #endregion AnimationSpeed

      #endregion Dependency Properties


      public CollapsibleStackPane()
      {
         InitializeComponent();


         this.Loaded += delegate
         {
            SetThemeUI(_theme, true);
         };
      }
      public CollapsibleStackPane(Theme theme)
      {
         InitializeComponent();

         this.Loaded += delegate
         {
            SetThemeUI(theme, true);
         };
      }

      #region Themeing
      public override void OnApplyTemplate()
      {
         base.OnApplyTemplate();

         _collapseController = GetTemplateChild("collapseController") as SideBarMenuItem;
         _contentGrid = GetTemplateChild("contentGrid") as Grid;

         if(_collapseController != null )
         {
            InitMenuController(_theme);
            _collapseController.IsActiveSelected = this.IsExpanded;
         }

         if(IsThereAwaitingThemeUpdateForContents)
         {
            UpdateContentThemes();
         }
      }



      public Theme GetTheme()
      {
         return _theme;
      }

      public void SetThemeUI(Theme theme, bool UpdateContentThemes = false)
      {
         if(theme != null)
         {
            _theme = theme;
            UpdateControllerTheme(theme, UpdateContentThemes);
         }
      }

      private void InitMenuController(Theme theme)
      {
         var strokedPathArrowIcon = "M582 576c0 -8 -4 -17 -10 -23l-466 -466c-6 -6 -15 -10 -23 -10s-17 4 -23 10l-50 50c-6 6 -10 14 -10 23c0 8 4 17 10 23l393 393l-393 393c-6 6 -10 15 -10 23s4 17 10 23l50 50c6 6 15 10 23 10s17 -4 23 -10l466 -466c6 -6 10 -15 10 -23z";

         if (_collapseController != null)
         {
            this._collapseController.IconTextOrientation = Orientation.Horizontal;
            this._collapseController.IconHorizontalAlignment = HorizontalAlignment.Left;
            this._collapseController.TextHorizontalAlignment = HorizontalAlignment.Left;
            this._collapseController.IconPath = strokedPathArrowIcon;
            this._collapseController.IconWidth = 10;
            this._collapseController.IconHeight = 10;
            this._collapseController.IsToggling = true;

            this._collapseController.FocusedIconPath = strokedPathArrowIcon;
            this._collapseController.IsActiveFocusableMenu = true;
            this._collapseController.Text = this.Text;

            this._collapseController.SetThemeUI(theme);

            _isControllerInitialized = true;
         }
      }

      public void UpdateControllerTheme(Theme theme, bool UpdateContentThemes = false)
      {
         _theme = theme;
         if (_isControllerInitialized && UpdateContentThemes)
         {
            this._collapseController.SetThemeUI(theme);
         }
         else
         {
            InitMenuController(theme);
         }
      }
      #endregion Themeing

      #region Animations

      #region Grid height Animations
      private void ToggleContentHeight(bool expand)
      {
         double fromHeight = 0.0, toHeight = 0.0;

         if(_contentGrid != null)
         {

            _isExpanded = expand;

            _contentGrid.Height = expand ? double.NaN : 0;
            bool doAnim = false;

            if (expand && _contentGrid.ActualHeight == 0)
            {
               fromHeight = 0;
               toHeight = _contentGrid.Tag as double? ?? _contentGrid.ActualHeight;
               doAnim = true;
            }
            else if(!expand && _contentGrid.ActualHeight != 0)
            {
               fromHeight = _contentGrid.ActualHeight;
               toHeight = 0;
               doAnim = true;
            }

            _contentGrid.Tag = _contentGrid.ActualHeight;


            if (doAnim)
            {
               _isAnimating = true;
               this._collapseController.DisableClickEvents = true;
               DoubleAnimation animation = new DoubleAnimation
               {
                  From = fromHeight,
                  To = toHeight,
                  Duration = TimeSpan.FromMilliseconds(AnimationSpeed),
                  FillBehavior = FillBehavior.HoldEnd
               };

               Storyboard storyboard = new Storyboard();
               storyboard.Children.Add(animation);

               Storyboard.SetTarget(animation, _contentGrid);
               Storyboard.SetTargetProperty(animation, new PropertyPath(Grid.HeightProperty));

               storyboard.Completed += (sender, e) =>
               {
                  _contentGrid.BeginAnimation(Grid.HeightProperty, null);
                  _isAnimating = false;

                  if (expand)
                  {
                     _contentGrid.Height = double.NaN;
                  }
                  else
                  {
                     _contentGrid.Height = 0;
                  }

                  this._collapseController.DisableClickEvents = false;
               };

               storyboard.Begin();
               _collapseController?.RotateIcon(AnimationSpeed, animation.From == 0 ? 90 : -90);
            }
         }

      }
      #endregion Grid height Animations

      #endregion Animations

      #region Default Events
      private void collapseController_Toggled(object sender, RoutedEventArgs e)
      {
         if (e is MenuItemToggledEventArgs menuToggledEventArgs)
         {
            SetExpandOrCollapsed(menuToggledEventArgs.IsActive);
         }
      }

      private void SetExpandOrCollapsed(bool expanded)
      {
         ToggleContentHeight(expanded);
      }
      #endregion

      #region Children Themeing
      public void UpdateContentThemes()
      {
         if(_theme != null && _contentGrid != null)
         {
            var childrenImplementingITheme = VisualTreeHelperExtensions.FindVisualChildrenOfChildren(_contentGrid).Where(child => child is ITheme).Cast<ITheme>();

            foreach (var themeChild in childrenImplementingITheme)
            {
               themeChild?.SetThemeUI(_theme);
            }
            IsThereAwaitingThemeUpdateForContents = false;
         }
         else
         {
            IsThereAwaitingThemeUpdateForContents = true;
         }
      }
      #endregion Children Themeing

   }
}
