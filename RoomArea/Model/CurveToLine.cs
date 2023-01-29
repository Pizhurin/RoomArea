using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using Line = System.Windows.Shapes.Line;

namespace RoomArea.Model
{
    public class CurveToLine
    {
        private readonly List<Curve> _curves;
        private List<Line> _lines = new List<Line>();
        private double _offcetX = double.MaxValue;
        private double _offcetY = double.MaxValue;

        private double _maxY = double.MinValue;



        public CurveToLine(List<Curve> curves)
        {
            _curves = curves;
            GetMinXY();
            GetMaxY();
        }

        private void GetMinXY()
        {
            foreach (Curve curve in _curves)
            {
                XYZ pointStart = curve.GetEndPoint(0);
                XYZ pointEnd = curve.GetEndPoint(1);
                if (pointStart.X < _offcetX) _offcetX = pointStart.X;
                if (pointStart.Y < _offcetY) _offcetY = pointStart.Y;
                if (pointEnd.X < _offcetX) _offcetX = pointEnd.X;
                if (pointEnd.Y < _offcetY) _offcetY = pointEnd.Y;
            }
        }


        private void GetMaxY()
        {
            foreach (Curve curve in _curves)
            {
                XYZ pointStart = curve.GetEndPoint(0);
                XYZ pointEnd = curve.GetEndPoint(1);
                if (pointStart.Y > _maxY) _maxY = pointStart.Y;
                if (pointEnd.Y > _maxY) _maxY = pointEnd.Y;
            }
        }

        public List<Line> GetLines()
        {
            foreach (Curve curve in _curves)
            {
                XYZ pointStart = curve.GetEndPoint(0);
                XYZ pointEnd = curve.GetEndPoint(1);

                Line line = new Line();
                //Если меньше нуля, то прибавляем значение
                if (_offcetX <= 0)
                {
                    line.X1 = Math.Round(UnitUtils.ConvertFromInternalUnits(pointStart.X + Math.Abs(_offcetX), DisplayUnitType.DUT_MILLIMETERS), 0);
                    line.X2 = Math.Round(UnitUtils.ConvertFromInternalUnits(pointEnd.X + Math.Abs(_offcetX), DisplayUnitType.DUT_MILLIMETERS), 0);
                }
                else
                {
                    line.X1 = Math.Round(UnitUtils.ConvertFromInternalUnits(pointStart.X - _offcetX, DisplayUnitType.DUT_MILLIMETERS), 0);
                    line.X2 = Math.Round(UnitUtils.ConvertFromInternalUnits(pointEnd.X - _offcetX, DisplayUnitType.DUT_MILLIMETERS), 0);
                }
                //Если меньше нуля, то прибавляем значение
                if (_offcetY <= 0)
                {
                    double currentStartY = Math.Round(UnitUtils.ConvertFromInternalUnits(pointStart.Y + Math.Abs(_offcetY), DisplayUnitType.DUT_MILLIMETERS), 0);
                    double currentEndY = Math.Round(UnitUtils.ConvertFromInternalUnits(pointEnd.Y + Math.Abs(_offcetY), DisplayUnitType.DUT_MILLIMETERS), 0);
                    double maxY = Math.Round(UnitUtils.ConvertFromInternalUnits(_maxY + Math.Abs(_offcetY), DisplayUnitType.DUT_MILLIMETERS), 0);

                    line.Y1 = maxY - currentStartY;
                    line.Y2 = maxY - currentEndY;

                    Line lineTest = new Line();
                    lineTest.Y1 = Math.Round(UnitUtils.ConvertFromInternalUnits(pointStart.Y + Math.Abs(_offcetY), DisplayUnitType.DUT_MILLIMETERS), 0);
                    lineTest.Y2 = Math.Round(UnitUtils.ConvertFromInternalUnits(pointEnd.Y + Math.Abs(_offcetY), DisplayUnitType.DUT_MILLIMETERS), 0);

                }
                else
                {
                    double currentStartY = Math.Round(UnitUtils.ConvertFromInternalUnits(pointStart.Y - _offcetY, DisplayUnitType.DUT_MILLIMETERS), 0);
                    double currentEndY = Math.Round(UnitUtils.ConvertFromInternalUnits(pointEnd.Y - _offcetY, DisplayUnitType.DUT_MILLIMETERS), 0);
                    double maxY = Math.Round(UnitUtils.ConvertFromInternalUnits(_maxY + -_offcetY, DisplayUnitType.DUT_MILLIMETERS), 0);

                    line.Y1 = maxY - currentStartY;
                    line.Y2 = maxY - currentEndY;
                }



                _lines.Add(line);
            }
            return _lines;
        }



    }
}
