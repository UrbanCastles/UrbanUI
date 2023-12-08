using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;
using System.Xml.Linq;
using UrbanUI.WPF.Themes;

namespace UrbanUI.WPF.Controls
{
   [TemplatePart(Name = "PART_ContentHost", Type = typeof(UrbanUI.WPF.Controls.ScrollViewer))]
   public partial class RichTextBox : System.Windows.Controls.RichTextBox, ITheme
   {

      private UrbanUI.WPF.Controls.ScrollViewer? _ContentHost;
      private Theme? _theme;
      public RichTextBox()
      {
         //DefaultStyleKeyProperty.OverrideMetadata(typeof(RichTextBox), new FrameworkPropertyMetadata(typeof(RichTextBox)));
      }

      #region Apply Template
      public override void OnApplyTemplate()
      {
         _ContentHost = GetTemplateChild("PART_ContentHost") as UrbanUI.WPF.Controls.ScrollViewer;

         base.OnApplyTemplate();
         if (_theme == null)
         {
            _theme = Defaults.getDefaultThemeSetups();
         }
         SetThemeUI(_theme);
      }
      #endregion Apply Template

      #region Depedencies
      public static readonly DependencyProperty ScrollBrushProperty =
          DependencyProperty.Register("ScrollBrush", typeof(Brush), typeof(RichTextBox), new PropertyMetadata(Brushes.Gray));

      public static readonly DependencyProperty ScrollTrackBrushProperty =
          DependencyProperty.Register("ScrollTrackBrush", typeof(Brush), typeof(RichTextBox), new PropertyMetadata(Brushes.Transparent));

      public static readonly DependencyProperty TextSelectionEnabledProperty =
         DependencyProperty.Register("TextSelectionEnabled", typeof(bool), typeof(RichTextBox), new PropertyMetadata(false));
      #endregion Depedencies

      #region Dependency Properties
      public Brush ScrollBrush
      {
         get => (Brush)GetValue(ScrollBrushProperty);
         set => SetValue(ScrollBrushProperty, value);
      }
      public Brush ScrollTrackBrush
      {
         get => (Brush)GetValue(ScrollTrackBrushProperty);
         set => SetValue(ScrollTrackBrushProperty, value);
      }
      public bool TextSelectionEnabled
      {
         get => (bool)GetValue(TextSelectionEnabledProperty);
         set => SetValue(TextSelectionEnabledProperty, value);
      }
      #endregion Dependency Properties

      #region Themeing
      public Theme GetTheme()
      {
         return _theme;
      }

      public void SetThemeUI(Theme theme, bool UpdateContentThemes = false)
      {
         _theme = theme;

         this.ScrollBrush = theme.ItemHighlightBackground;
         this.ScrollTrackBrush = Brushes.Transparent;

         if (_ContentHost != null)
         {
            _ContentHost.SetThemeUI(theme);
         }

      }
      #endregion Themeing


      //public void HighlightXml()
      //{
      //   string richText = new TextRange(this.Document.ContentStart, this.Document.ContentEnd).Text;
      //   XDocument xmlDocument = XDocument.Parse(richText);

      //}
   }
}
