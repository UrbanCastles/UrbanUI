using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrbanUI.WPF.Themes
{

   public class Icon
   {

      public string Name { get; set; }
      public string StrokePath { get; set; }
      public string FilledPath { get; set; }
      public double Width { get; set; }
      public double Height { get; set; }
      public bool FlipX { get; set; }
      public bool FlipY { get; set; }
   }

   class CustomIcons
   {
      public List<Icon> icons = new List<Icon>();
   }
}
