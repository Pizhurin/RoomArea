using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using Line = System.Windows.Shapes.Line;

namespace RoomArea.Model
{
    public class LengthEdge
    {
        
        public static string GetLengthEdge(Line line)
        {
            double x1 = line.X1;
            double y1 = line.Y1;
            double x2 = line.X2;
            double y2 = line.Y2;


            double LengthEdge = Math.Round((Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2))/1000), 2);

            return $"{LengthEdge.ToString()} м.";
            
        }

    }
}
