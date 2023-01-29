using Autodesk.Revit.UI;
using RoomArea.ViewModel;
using System.Windows;

namespace RoomArea.View
{
    /// <summary>
    /// Interaction logic for View_RoomArea.xaml
    /// </summary>
    public partial class View_RoomArea : Window
    {

        public View_RoomArea(ExternalCommandData commandData)
        {
            InitializeComponent();

            ViewModel_RoomArea vm_RoomArea = new ViewModel_RoomArea(commandData);

            vm_RoomArea.HideWindow += (s, e) => this.Hide();
            vm_RoomArea.ShowWindow += (s, e) => this.Show();

            vm_RoomArea.Viewbox = View_Viewbox;


            DataContext = vm_RoomArea;
        }
    }
}
