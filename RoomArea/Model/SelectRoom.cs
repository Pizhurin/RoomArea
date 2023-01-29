using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Line = System.Windows.Shapes.Line;

namespace RoomArea.Model
{
    public class SelectRoom
    {
        ExternalCommandData _commandData;
        List<Line> _lines = new List<Line>();
        List<Curve> _curveList = new List<Curve>();
        public SelectRoom(ExternalCommandData commandData)
        {
            _commandData= commandData;
        }

        public List<Line> GetLines()
        {
            Document document = _commandData.Application.ActiveUIDocument.Document;
            UIDocument uIDocument = _commandData.Application.ActiveUIDocument;
            FilterSelectionSpaces filterSelection = new FilterSelectionSpaces();

            try
            {
                Reference reference = uIDocument.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, filterSelection, "Выберите помещение");

                var elem = document.GetElement(reference);
                Room room = elem as Room;

                SpatialElementBoundaryOptions boundaryOptions = new SpatialElementBoundaryOptions();
                boundaryOptions.SpatialElementBoundaryLocation = SpatialElementBoundaryLocation.Finish;
                var segments = room.GetBoundarySegments(boundaryOptions);

                foreach (var segment in segments)
                {
                    foreach (var curve in segment)
                    {
                        Curve cur = curve.GetCurve();
                        _curveList.Add(cur);
                    }
                }

                CurveToLine toLine = new CurveToLine(_curveList);
                _lines = toLine.GetLines();
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {

            }

            return _lines;
        }

    }
}
