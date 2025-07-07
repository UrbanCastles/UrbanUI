using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UrbanUI.WPF.Themes;

namespace UrbanUI.WPF.Controls
{
   public partial class Hyperlink : Button
   {
      #region Dependency Properties

      #region DP: Link
      public string Link
      {
         get { return (string)GetValue(LinkProperty); }
         set { SetValue(LinkProperty, value); }
      }
      public static readonly DependencyProperty LinkProperty = DependencyProperty.Register(nameof(Link), typeof(string), typeof(Hyperlink), new PropertyMetadata(string.Empty));
      #endregion DP: Link

      #endregion Dependency Properties


      protected override void OnClick()
      {
         base.OnClick();
         if (string.IsNullOrEmpty(Link))
         {
            return;
         }

         try
         {
            ProcessStartInfo sInfo = new ProcessStartInfo(new Uri(Link).AbsoluteUri)
            {
               UseShellExecute = true
            };

            Process.Start(sInfo);
         }
         catch (Exception e)
         {
            Debug.WriteLine(e);
         }
      }
   }
}
