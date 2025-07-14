using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shell;
using UrbanUI.WPF.Win32.Helper;
using UrbanUI.WPF.Win32.Interop.Methods;
using UrbanUI.WPF.Win32.Interop.Values;

namespace UrbanUI.WPF.Win32.WinBUttonContext
{
   public class HTButtonSettings
   {
      private static HTButtonSettings _instance = null;
      private Window _windowSource;
      private Button _minimizeButton, _maximizeButton, _restoreButton, _closeButton;


      private WindowHelper.ButtonType _newButtonMouseEntered = WindowHelper.ButtonType.NONE;
      private WindowHelper.ButtonType _lastButtonMouseLeave = WindowHelper.ButtonType.NONE;
      private WindowHelper.ButtonType _lastButtonMouseDown = WindowHelper.ButtonType.NONE;
      private bool _newButtonMouseEnteredAlreadyTriggered = true;
      private bool _lastButtonMouseLeaveAlreadyTriggered = true;
      private bool _continueButtonInvocation = false;

      internal IntPtr Hwnd { get; set; }

      private static HTButtonSettings _getInstance()
      {
         if (_instance == null)
            _instance = new HTButtonSettings();
         return _instance;
      }

      private IntPtr InitHooks()
      {
         var Hwnd = this._windowSource.EnsureHandle();
         this._windowSource?.GetHwndSource(Hwnd)?.AddHook(this.HwndSourceHook);
         this.Hwnd = Hwnd;
         return Hwnd;
      }

      private void FixWindowChromeSetups()
      {
         //Source: https://learn.microsoft.com/en-us/dotnet/api/system.windows.shell.windowchrome?view=windowsdesktop-8.0&redirectedfrom=MSDN
         //When GlassFrameThickness is set to a negative value for any side, its coerced value will be equal to GlassFrameCompleteThickness.
         //This fixes the issue that the Snap Layout is not showing when on Normal Mode

         var windowChrome = WindowChrome.GetWindowChrome(_windowSource);
         if (windowChrome != null)
         {
            windowChrome.ResizeBorderThickness = _windowSource.ResizeMode == ResizeMode.NoResize ? default : new Thickness(4);
            windowChrome.GlassFrameThickness = new Thickness(-1);
         }
         else
         {
            windowChrome = new WindowChrome()
            {
               GlassFrameThickness = new Thickness(-1),
               ResizeBorderThickness = _windowSource.ResizeMode == ResizeMode.NoResize ? default : new Thickness(4),
               CaptionHeight = 0,
               CornerRadius = default,
               UseAeroCaptionButtons = false
            };

            WindowChrome.SetWindowChrome(_windowSource, windowChrome);
         }
      }

      public static IntPtr AddContextMenuHook(Window window, Button minimizeButton, Button maximizeButton, Button restoreButton, Button closeButton)
      {
         var instance = _getInstance();
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

            instance.FixWindowChromeSetups();

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

               if (returnPtr != IntPtr.Zero && IsOnClientArea(hwnd, msg, wParam, lParam))
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

      public bool IsOnClientArea(IntPtr hWnd, int uMsg, IntPtr wParam, IntPtr lParam)
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
   }



}
