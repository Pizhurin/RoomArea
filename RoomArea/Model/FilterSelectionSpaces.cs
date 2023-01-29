using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI.Selection;

namespace RoomArea.Model
{
    public class FilterSelectionSpaces : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            if (elem is Room) return true;
            else return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }
}
