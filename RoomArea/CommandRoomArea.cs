using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RoomArea.View;

namespace RoomArea
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class CommandRoomArea : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            View_RoomArea v_RoomArea = new View_RoomArea(commandData);
            v_RoomArea.Width = 600;
            v_RoomArea.Height = 400;
            v_RoomArea.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            v_RoomArea.ShowDialog();

            return Result.Succeeded;
        }
    }
}
