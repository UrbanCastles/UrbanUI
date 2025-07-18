using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using UrbanUI.WPF.Win32.Helper;
using UrbanUI.WPF.Win32.Interop.Methods;
using UrbanUI.WPF.Win32.Interop.Values;
using static UrbanUI.WPF.Win32.Interop.Structs.InteropStructs;

namespace UrbanUI.WPF.Win32.WinApi
{
   public class NativeWindowInterop
   {
      private static NativeWindowInterop _instance = null;
      private UrbanUI.WPF.Controls.UrbanWindow _windowSource;
      private Button _minimizeButton, _maximizeButton, _restoreButton, _closeButton;


      private WindowHelper.ButtonType _newButtonMouseEntered = WindowHelper.ButtonType.NONE;
      private WindowHelper.ButtonType _lastButtonMouseLeave = WindowHelper.ButtonType.NONE;
      private WindowHelper.ButtonType _lastButtonMouseDown = WindowHelper.ButtonType.NONE;
      private bool _newButtonMouseEnteredAlreadyTriggered = true;
      private bool _lastButtonMouseLeaveAlreadyTriggered = true;
      private bool _continueButtonInvocation = false;

      //private bool HandleClientFrameBG = true;
      //private bool isHandlingClientFrameBG;

      internal IntPtr Hwnd { get; set; }
      private static bool _isHookInitialized = false;

      internal static NativeWindowInterop GetInstance()
      {
         if (_instance == null)
            _instance = new NativeWindowInterop();
         return _instance;
      }

      internal static bool IseInitialized()
      {
         return _instance != null;
      }

      internal static bool IsHookInitialized()
      {
         return _isHookInitialized;
      }

      private IntPtr InitHooks()
      {
         var Hwnd = this._windowSource.EnsureHandle();

         SetNativeFrameColor(Hwnd, ((SolidColorBrush)_windowSource.Background).Color);
         ScreenHelper.ExtendFrameIntoClientArea(Hwnd, _windowSource);

         this._windowSource?.GetHwndSource(Hwnd)?.AddHook(this.HwndSourceHook);

         this.Hwnd = Hwnd;
         _isHookInitialized = true;
         return Hwnd;
      }


      public static IntPtr AddContextMenuHook(UrbanUI.WPF.Controls.UrbanWindow window, Button minimizeButton, Button maximizeButton, Button restoreButton, Button closeButton)
      {
         var instance = GetInstance();
         if (instance != null)
         {
            instance._windowSource = window;

            instance._minimizeButton = minimizeButton;
            instance._maximizeButton = maximizeButton;
            instance._restoreButton = restoreButton;
            instance._closeButton = closeButton;

            if (window == null || maximizeButton == null || restoreButton == null)
            {
               throw new Exception("All parameters must have a value.");
            }

            return instance.InitHooks();
         }
         else
            return IntPtr.Zero;
      }

      private IntPtr HwndSourceHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
      {
         switch (msg)
         {
            case InteropValues.WM_NCHITTEST:
               var buttonType = GetButtonType(lParam, InteropValues.DPI_SCALE);
               IntPtr returnPtr = IntPtr.Zero;

               if (buttonType != _newButtonMouseEntered)
               {
                  if (buttonType != WindowHelper.ButtonType.NONE)
                     _newButtonMouseEnteredAlreadyTriggered = false;

                  if (_newButtonMouseEntered != WindowHelper.ButtonType.NONE)
                     _lastButtonMouseLeaveAlreadyTriggered = false;

                  _lastButtonMouseLeave = _newButtonMouseEntered;
                  _newButtonMouseEntered = buttonType;
               }

               if (_lastButtonMouseDown != _newButtonMouseEntered)
                  _continueButtonInvocation = false;

               switch (buttonType)
               {
                  case WindowHelper.ButtonType.MINIMIZEBUTTON:
                     returnPtr = new IntPtr(InteropValues.HTMINBUTTON);
                     break;
                  case WindowHelper.ButtonType.CLOSEBUTTON:
                     returnPtr = new IntPtr(InteropValues.HTCLOSE);
                     break;
                  case WindowHelper.ButtonType.MAXIMIZEBUTTON:
                  case WindowHelper.ButtonType.RESTOREBUTTON:
                     if (OSVersionHelper.IsWindows11_OrGreater && this._windowSource.ResizeMode != ResizeMode.NoResize && this._windowSource.ResizeMode != ResizeMode.CanMinimize)
                     {
                        returnPtr = new IntPtr(InteropValues.HTMAXBUTTON);
                     }
                     break;
               }

               if (!_lastButtonMouseLeaveAlreadyTriggered)
               {
                  var button = GetButton(_lastButtonMouseLeave);
                  MouseEventArgs mouseEventArgs = new MouseEventArgs(Mouse.PrimaryDevice, 0);
                  mouseEventArgs.RoutedEvent = Mouse.MouseLeaveEvent;
                  _lastButtonMouseLeaveAlreadyTriggered = true;
                  button?.RaiseEvent(mouseEventArgs);
               }
               if (!_newButtonMouseEnteredAlreadyTriggered)
               {
                  var button = GetButton(_newButtonMouseEntered);
                  MouseEventArgs mouseEventArgs = new MouseEventArgs(Mouse.PrimaryDevice, 0);
                  mouseEventArgs.RoutedEvent = Mouse.MouseEnterEvent;
                  _newButtonMouseEnteredAlreadyTriggered = true;
                  button?.RaiseEvent(mouseEventArgs);

               }

               if (returnPtr != IntPtr.Zero && IsOnClientArea(hwnd, (uint)msg, wParam, lParam))
               {
                  handled = true;
                  return returnPtr;
               }
               else
                  handled = false;
               break;

            case InteropValues.WM_NCLBUTTONDOWN:
               var buttonType_mousedown = GetButtonType(lParam, InteropValues.DPI_SCALE);
               _lastButtonMouseDown = buttonType_mousedown;
               if (buttonType_mousedown != WindowHelper.ButtonType.NONE)
               {
                  handled = true;
                  _continueButtonInvocation = true;
               }
               else
                  handled = false;
               break;

            case InteropValues.WM_NCLBUTTONUP:
               var buttonType_mouseup = GetButton(_lastButtonMouseDown);
               if (_continueButtonInvocation)
               {
                  _continueButtonInvocation = false;
                  if (buttonType_mouseup != null)
                  {
                     IInvokeProvider invokeProv = new ButtonAutomationPeer(buttonType_mouseup).GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                     invokeProv?.Invoke();
                     handled = true;
                  }
                  else
                     handled = false;
               }
               else
                  handled = false;
               break;

            case InteropValues.WM_GETMINMAXINFO:
               ScreenHelper.WmGetMinMaxInfo(hwnd, lParam, this._windowSource);
               handled = true;
               break;

            case InteropValues.WM_NCCALCSIZE:
            case InteropValues.WM_WINDOWPOSCHANGED:
            case InteropValues.WM_EXITSIZEMOVE:
               _windowSource?.UpdateTemplateCornerRadius();
               break;

            case InteropValues.WM_SIZE:
               /*if (_windowSource.IsLoaded && this.HandleClientFrameBG)
               {
                  this.HandleClientFrameBG = false;
                  this.isHandlingClientFrameBG = false;
               }*/
               ForceNativeWindowRedraw();
               break;

            //case InteropValues.WM_ERASEBKGND:
            //   return this.HandleClientFrameBackground(hwnd, wParam, lParam, out handled);

            default:
               handled = false;
               break;
         }
         return IntPtr.Zero;
      }

      private WindowHelper.ButtonType GetButtonType(IntPtr lParam, double DPI_SCALE = 1.0)
      {
         int x = lParam.ToInt32() & 0xffff;
         int y = lParam.ToInt32() >> 16;
         var _buttonMaximize = this._windowSource.ResizeMode == ResizeMode.CanMinimize ? null : (this._windowSource.WindowState == WindowState.Maximized ? this._restoreButton : this._maximizeButton);
         var _buttonMinimize = this._windowSource.ResizeMode == ResizeMode.NoResize ? null : this._minimizeButton;
         var _buttonClose = this._closeButton;

         if (getButtonRect(_buttonMaximize, DPI_SCALE).Contains(new Point(x, y)))
         {
            return this._windowSource.WindowState == WindowState.Maximized ? WindowHelper.ButtonType.RESTOREBUTTON : WindowHelper.ButtonType.MAXIMIZEBUTTON;
         }
         else if (getButtonRect(_buttonMinimize, DPI_SCALE).Contains(new Point(x, y)))
         {
            return WindowHelper.ButtonType.MINIMIZEBUTTON;
         }
         else if (getButtonRect(_buttonClose, DPI_SCALE).Contains(new Point(x, y)))
         {
            return WindowHelper.ButtonType.CLOSEBUTTON;
         }
         else
            return WindowHelper.ButtonType.NONE;
      }

      private Button GetButton(WindowHelper.ButtonType buttonType)
      {
         switch (buttonType)
         {
            case WindowHelper.ButtonType.MINIMIZEBUTTON:
               return this._minimizeButton;
            case WindowHelper.ButtonType.CLOSEBUTTON:
               return this._closeButton;
            case WindowHelper.ButtonType.MAXIMIZEBUTTON:
               return this._maximizeButton;
            case WindowHelper.ButtonType.RESTOREBUTTON:
               return this._restoreButton;
            default:
               return null;
         }
      }

      private Rect getButtonRect(Button button, double DPI_SCALE = 1.0)
      {
         if (button == null)
            return new Rect(0.0, 0.0, 0.0, 0.0);

         var btWidth = button.Width is double.NaN ? button.ActualWidth : button.Width;
         var btHeight = button.Height is double.NaN ? button.ActualHeight : button.ActualHeight;
         return new Rect(button.PointToScreen(new Point()), new Size(btWidth * DPI_SCALE, btHeight * DPI_SCALE));
      }

      public bool IsOnClientArea(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam)
      {
         if (uMsg == InteropValues.WM_NCHITTEST)
         {
            if (InteropMethods.DefWindowProc(hWnd, uMsg, wParam, lParam).ToInt32() == InteropValues.HTCLIENT)
            {
               return true;
            }
         }

         return false;
      }

      internal static void ForceNativeWindowRedraw()
      {
         if (_instance != null)
         {
            const uint SwpFlags = InteropValues.SWP_FRAMECHANGED | InteropValues.SWP_NOSIZE | InteropValues.SWP_NOMOVE | InteropValues.SWP_NOZORDER | InteropValues.SWP_NOOWNERZORDER | InteropValues.SWP_NOACTIVATE;
            InteropMethods.SetWindowPos(_instance.Hwnd, default, 0, 0, 0, 0, SwpFlags);
            InteropMethods.RedrawWindow(_instance.Hwnd, IntPtr.Zero, IntPtr.Zero, InteropValues.RDW_INVALIDATE | InteropValues.RDW_UPDATENOW | InteropValues.RDW_FRAME);
         }
      }

      public static void SetNativeFrameColor(IntPtr hwnd, Color color)
      {
         uint colorRef = (uint)((color.R) | (color.G << 8) | (color.B << 16));
         InteropMethods.DwmSetWindowAttribute(hwnd, InteropValues.DWMWA_CAPTION_COLOR, ref colorRef, Marshal.SizeOf<uint>());
      }


      public static CornerRadius GetSystemCornerRadius()
      {
         if (_isHookInitialized && Environment.OSVersion.Version.Major >= 10)
         {
            // Get current window rect
            if (InteropMethods.GetWindowRect(_instance.Hwnd, out RECT rect))
            {
               IntPtr monitor = InteropMethods.MonitorFromWindow(_instance.Hwnd, InteropValues.MONITOR_DEFAULTTONEAREST);
               MONITORINFO monitorInfo = new MONITORINFO();
               monitorInfo.cbSize = Marshal.SizeOf(typeof(MONITORINFO));
               if (InteropMethods.GetMonitorInfo(monitor, ref monitorInfo))
               {
                  RECT workArea = monitorInfo.rcWork;
                  RECT fullArea = monitorInfo.rcMonitor;

                  const int TOLERANCE = 2;

                  int width = rect.Right - rect.Left;
                  int height = rect.Bottom - rect.Top;

                  bool isMaximized =
                      Math.Abs(rect.Left - workArea.Left) <= TOLERANCE &&
                      Math.Abs(rect.Top - workArea.Top) <= TOLERANCE &&
                      Math.Abs(rect.Right - workArea.Right) <= TOLERANCE &&
                      Math.Abs(rect.Bottom - workArea.Bottom) <= TOLERANCE;

                  bool isSnappedLeft =
                      Math.Abs(rect.Left - workArea.Left) <= TOLERANCE &&
                      Math.Abs(width - (workArea.Right - workArea.Left) / 2) <= TOLERANCE;

                  bool isSnappedRight =
                      Math.Abs(rect.Right - workArea.Right) <= TOLERANCE &&
                      Math.Abs(width - (workArea.Right - workArea.Left) / 2) <= TOLERANCE;

                  if (isMaximized || isSnappedLeft || isSnappedRight)
                  {
                     return new CornerRadius(0);
                  }
               }
            }

            // Fallback to system preference
            DwmWindowCornerPreference pref;
            InteropMethods.DwmGetWindowAttribute(
                _instance.Hwnd,
                InteropValues.DWMWA_WINDOW_CORNER_PREFERENCE,
                out pref,
                sizeof(int)
            );

            return GetEffectiveCornerRadius(pref);
         }

         return new CornerRadius(0);
      }



      private static CornerRadius GetEffectiveCornerRadius(DwmWindowCornerPreference pref)
      {
         switch (pref)
         {
            case DwmWindowCornerPreference.Round:
               return new CornerRadius(8);
            case DwmWindowCornerPreference.RoundSmall:
               return new CornerRadius(4);
            case DwmWindowCornerPreference.DoNotRound:
               return new CornerRadius(0);
            case DwmWindowCornerPreference.Default:
               if (IsWindows11OrNewer())
                  return new CornerRadius(8);
               else
                  return new CornerRadius(0);
            default:
               return new CornerRadius(0);
         }
      }

      private static bool IsWindows11OrNewer()
      {
         // Windows 11 is version 10.0.22000 or newer
         Version windows11Version = new Version(10, 0, 22000);
         return Environment.OSVersion.Platform == PlatformID.Win32NT &&
                Environment.OSVersion.Version >= windows11Version;
      }


      internal static void Dispose()
      {
         if (_instance != null && _instance._windowSource != null)
         {
            _instance._windowSource?.GetHwndSource(_instance.Hwnd)?.RemoveHook(_instance.HwndSourceHook);
            _instance.Hwnd = default;
            _instance._windowSource = null;
         }
      }

      /*private IntPtr HandleClientFrameBackground(IntPtr hwnd, IntPtr wParam, IntPtr lParam, out bool handled)
      {
         handled = false;

         if (!_instance.HandleClientFrameBG || _instance.isHandlingClientFrameBG)
            return IntPtr.Zero;

         _instance.isHandlingClientFrameBG = true;

         try
         {
            if (!InteropMethods.GetClientRect(hwnd, out RECT rect))
               return IntPtr.Zero;

            var backgroundColor = _windowSource.Background is SolidColorBrush scb ? scb.Color : Colors.Transparent;
            uint colorRef = ToColorRef(backgroundColor);

            IntPtr hBrush = InteropMethods.CreateSolidBrush(colorRef);
            if (hBrush == IntPtr.Zero)
               return IntPtr.Zero;

            IntPtr hdc = InteropMethods.GetWindowDC(hwnd);
            if (hdc == IntPtr.Zero)
            {
               InteropMethods.DeleteObject(hBrush);
               return IntPtr.Zero;
            }
            int result = InteropMethods.FillRect(hdc, ref rect, hBrush);

            InteropMethods.ReleaseDC(hwnd, hdc);
            InteropMethods.DeleteObject(hBrush);

            if (result != 0)
            {
               handled = true;
               return new IntPtr(1);
            }
         }
         catch (Exception e)
         {
            int error = Marshal.GetLastWin32Error();
            Debug.WriteLine($"Win32 Error: {error}");
         }
         finally
         {
            _instance.isHandlingClientFrameBG = false;
         }
         return IntPtr.Zero;
      }


      private static uint ToColorRef(Color c) => ((uint)c.R) | ((uint)c.G << 8) | ((uint)c.B << 16);*/


   }



}
