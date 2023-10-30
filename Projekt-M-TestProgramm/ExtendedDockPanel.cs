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
    public class ExtendedDockPanel : DockPanel
    {
        public static readonly DependencyProperty FontSizeProperty =
        DependencyProperty.Register(
            "FontSize",
            typeof(double),
            typeof(ExtendedDockPanel),
            new PropertyMetadata(12.0));

        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public ExtendedDockPanel(double fontSize, Dock dockDirection, params UIElement[] elements) : base()
        {
            FontSize = fontSize;

            foreach (UIElement element in elements)
            {
                DockAt(element, dockDirection);
            }

            // Especially for fractions very helpful
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
        }

        public ExtendedDockPanel() : base() { }

        public void DockAt(UIElement element, Dock dockDirection)
        {
            ExtendedDockPanel a = element as ExtendedDockPanel; ;
            ExtendedLabel b = element as ExtendedLabel;

            if (!(a is null))
            {
                a.FontSize = FontSize;
            }
            else if (!(b is null))
            {
                b.FontSize = FontSize;
            }

            DockPanel.SetDock(element, dockDirection);
            this.Children.Add(element);
        }

        public void SetSideMargin()
        {
            // Sometimes, some distance makes us happy
            Margin = new Thickness(5, 0, 5, 0);
        }

        public void Clear()
        {
            Children.Clear();
            /*
            for (int i = 0; i < Children.Count; i++)
            {
                Children.RemoveAt(i);
            }*/
        }
    }
}
