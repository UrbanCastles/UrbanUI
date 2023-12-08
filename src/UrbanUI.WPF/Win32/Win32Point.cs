using System.Runtime.InteropServices;

namespace UrbanUI.WPF.Win32
{

   [StructLayout(LayoutKind.Sequential)]
   public struct Win32Point
   {
      public int X;
      public int Y;


      public Win32Point(int x, int y)
      {
         this.X = x;
         this.Y = y;
      }
   }
}
