using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace UrbanUI.WPF.Tools
{
   public class PathGeometryTool
   {



      public static Geometry FlipPG(Geometry src, bool flipX = false, bool flipY = false)
      {
         Geometry dst = src.Clone();

         ScaleTransform flipTrans = new ScaleTransform()
         {
            ScaleX = flipX ? -1 : 1,
            ScaleY = flipY ? -1 : 1,
            CenterX = 0.5,
            CenterY = 0.5
         };
         dst.Transform = flipTrans;

         return dst;
      }




      public static void SetIconDetails(Path iconControl, string path, Brush color, bool flipHorizontally = false, bool flipVertically = false)
      {
         if (!string.IsNullOrEmpty(path))
         {
            try
            {
               Geometry geometry = Geometry.Parse(path);
               iconControl.Data = PathGeometryTool.FlipPG(geometry, flipHorizontally, flipVertically);
               iconControl.Fill = color;
            }
            catch (FormatException) { }
         }
         else
         {
            iconControl.Data = null;
         }
      }
   }
}
