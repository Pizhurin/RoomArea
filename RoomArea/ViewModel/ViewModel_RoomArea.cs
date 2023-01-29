using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Prism.Commands;
using RoomArea.Model;
using RoomArea.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Input;
using Grid = System.Windows.Controls.Grid;
using Line = System.Windows.Shapes.Line;
 

namespace RoomArea.ViewModel
{
    public class ViewModel_RoomArea : INotifyPropertyChanged
    {
        private ExternalCommandData _commandData;

        public DelegateCommand SelectRoom { get; }
        public DelegateCommand FillColor { get; }

        private Polyline _newPolyline;
        public Polyline NewPolyline
        {
            get { return _newPolyline; }
            set { _newPolyline = value; NotifyPropertyChanged(nameof(NewPolyline)); }
        }

        private Viewbox _viewbox;
        public Viewbox Viewbox
        {
            get { return _viewbox; }
            set { _viewbox = value; NotifyPropertyChanged(nameof(Viewbox)); }
        }

        private Grid _grids;
        public Grid Grids
        {
            get { return _grids; }
            set { _grids = value; NotifyPropertyChanged(nameof(Grids)); }
        }

        private string _lEdge = string.Empty;
        public string LEdge
        {
            get { return _lEdge; }
            set { _lEdge = value; NotifyPropertyChanged(nameof(LEdge)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel_RoomArea(ExternalCommandData commandData)
        {
            _commandData = commandData;

            SelectRoom = new DelegateCommand(OnSelectRoom);
            FillColor = new DelegateCommand(OnFillColor);

            _grids = new Grid();
            
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event EventHandler HideWindow;
        public event EventHandler ShowWindow;


        private void HideMainWindow()
        {
            HideWindow?.Invoke(this, EventArgs.Empty);
        }

        private void ShowMainWindow()
        {
            ShowWindow?.Invoke(this, EventArgs.Empty);
        }

        private void OnFillColor()
        {
            Brush brush = new SolidColorBrush(SelectColors.GetColor());
            NewPolyline.Fill = brush;
        }

        private void OnSelectRoom()
        {
            HideMainWindow();

            SelectRoom selectRoom = new SelectRoom(_commandData);
            List<Line> lines = selectRoom.GetLines();

            Grids.Children.Clear();
            PointCollection CollectionPoints = new PointCollection();
            NewPolyline = new Polyline();

            foreach (Line line in lines)
            {
                line.Stroke = Brushes.Black;
                line.StrokeThickness = 100;

                line.MouseLeftButtonDown += (s, e) => LEdge = LengthEdge.GetLengthEdge(s as Line);
                line.MouseEnter += (s, e) => line.Cursor = System.Windows.Input.Cursors.Hand;              

                Grids.Children.Add(line);

                CollectionPoints.Add(new System.Windows.Point(line.X1, line.Y1));
                CollectionPoints.Add(new System.Windows.Point(line.X2, line.Y2));
                NewPolyline.Points = CollectionPoints;
            }


            NewPolyline.Stroke = Brushes.White;
            Grids.Children.Add(NewPolyline);            
            Viewbox.Child = Grids;
           

            ShowMainWindow();

        }
    }
}

