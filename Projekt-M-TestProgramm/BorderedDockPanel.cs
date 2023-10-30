using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Simon;

namespace Projekt_M_TestProgramm
{
    class BorderedDockPanel
    {
        ExtendedDockPanel dockPanel;
        ExtendedDockPanel DockPanel { get { return dockPanel; } set { dockPanel = value; } }

        DashedBorder border;
        DashedBorder Border { get { return border; } set { border = value; } }

        private void InitBorder()
        {
            Border = new DashedBorder();
            Border.UseDashedBorder = true;

            Color borderColor = new Color();
            borderColor.R = 0x87;
            borderColor.G = 0x87;
            borderColor.B = 0x87;
            Border.DashedBorderBrush = new SolidColorBrush(borderColor);
            Border.StrokeDashArray = new DoubleCollection(new double[] { 2, 1 });
            Border.BorderThickness = new Thickness(3);
            Border.CornerRadius = new CornerRadius(5, 5, 5, 5);
        }

        public BorderedDockPanel(double fontSize, Dock dockDirection, params UIElement[] elements)
        {
            DockPanel = new ExtendedDockPanel(fontSize, dockDirection, elements);
            InitBorder();

            //Border.Child.SetValue 
        }

        public BorderedDockPanel() : base() { }
    }
}
