using Autodesk.Revit.DB;
using System.Windows.Forms;


namespace RoomArea.Model
{
    public class SelectColors
    {
        public static System.Windows.Media.Color GetColor()
        {
            System.Windows.Media.Color colorSource = new System.Windows.Media.Color();
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.FullOpen = true;
            colorDialog.ShowDialog();
            System.Drawing.Color color = colorDialog.Color;
            colorSource.A = color.A;
            colorSource.R = color.R;
            colorSource.G = color.G;
            colorSource.B = color.B;

            return colorSource;
        }

    }
}
