using System;
using System.Runtime.InteropServices;
using System.Windows.Media;
using System.Windows.Shell;
using UrbanUI.WPF.Win32.Helper;
using UrbanUI.WPF.Win32.Interop.Methods;
using UrbanUI.WPF.Win32.Interop.Values;

namespace UrbanUI.WPF.Controls.Window
{
    internal class WindowChromeHelper
    {
      private IntPtr hwnd;
      private static WindowChromeHelper _instance;

      public static void Initialize(IntPtr windowHandle)
      {
         if (_instance == null)
         {
            _instance = new WindowChromeHelper();
            _instance.SetWindowHandle(windowHandle);
         }
         else if(windowHandle != IntPtr.Zero)
            _instance.SetWindowHandle(windowHandle);
      }

      public static WindowChromeHelper getInstance(System.Windows.Window sourceWindow = null)
      {
         if(_instance == null)
         {
            if(sourceWindow != null)
            {
               Initialize(sourceWindow.GetHandle());
            }
            else
               Initialize(IntPtr.Zero);
         }

         return _instance;
      }

      public void SetWindowHandle(IntPtr hwnd)
      {
         this.hwnd = hwnd;
      }


      public void SetNativeFrameColor(Color color)
      {
         if (hwnd == IntPtr.Zero)
            return;

         uint colorRef = (uint)((color.R) | (color.G << 8) | (color.B << 16));
         InteropMethods.DwmSetWindowAttribute(hwnd, InteropValues.DWMWA_CAPTION_COLOR, ref colorRef, Marshal.SizeOf<uint>());
      }

      public void SetCornerPreference(WindowCornerPreference cornerPreference)
      {
         if (hwnd == IntPtr.Zero)
            return;

         try
         {
            uint prefValue = (uint)cornerPreference;
            InteropMethods.DwmSetWindowAttribute(hwnd, InteropValues.DWMWA_WINDOW_CORNER_PREFERENCE, ref prefValue, sizeof(uint));
         }
         catch { }
      }

      internal void InitializeWindowChromeSetups(System.Windows.Window window)
      {
         if (window == null)
            return;

         var windowChrome = WindowChrome.GetWindowChrome(window);
         if (windowChrome != null)
         {
            windowChrome.GlassFrameThickness = new System.Windows.Thickness(-1);
            windowChrome.ResizeBorderThickness = window.ResizeMode == System.Windows.ResizeMode.NoResize ? default : new System.Windows.Thickness(8);
            windowChrome.CaptionHeight = 0;
            windowChrome.UseAeroCaptionButtons = false;
            windowChrome.NonClientFrameEdges = NonClientFrameEdges.None;
         }
         else
         {
            windowChrome = new WindowChrome()
            {
               GlassFrameThickness = new System.Windows.Thickness(-1),
               ResizeBorderThickness = window.ResizeMode == System.Windows.ResizeMode.NoResize ? default : new System.Windows.Thickness(8),
               CaptionHeight = 0,
               UseAeroCaptionButtons = false,
               NonClientFrameEdges = NonClientFrameEdges.None,
            };
            WindowChrome.SetWindowChrome(window, windowChrome);
         }
      }

      public void SetWindowBackdrop(WindowBackdropType backdropType)
      {
         if (hwnd == IntPtr.Zero)
            return;

         try
         {
            uint backdropValue = (uint)backdropType;
            InteropMethods.DwmSetWindowAttribute(hwnd, InteropValues.DWMWA_SYSTEMBACKDROP_TYPE, ref backdropValue, sizeof(uint));
         }
         catch { }
      }

   }


   public enum WindowCornerPreference
   {
      Default = 0,      // Let the system decide
      DoNotRound = 1,   // Force square corners
      Round = 2,        // Fully rounded corners
      RoundSmall = 3    // Slightly rounded corners
   }


   public enum WindowBackdropType
   {
      Auto = 0,       // Let system decide
      None = 1,       // No backdrop (solid background)
      Mica = 2,       // Mica material
      Acrylic = 3,    // Acrylic blur (less commonly supported)
      Tabbed = 4      // Mica Alt (used for tabbed interfaces)
   }
}

