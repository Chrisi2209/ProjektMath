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
    public class ExtendedLabel : Label
    {
        private void InitialiseValues()
        {
            // helper for constructors
            Padding = new Thickness(0);
            Margin = new Thickness(1, 0, 1, 0);
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
        }

        public ExtendedLabel(string content = "") : base()
        {
            InitialiseValues();
            Foreground = Brushes.White;

            Content = content;
        }

        public ExtendedLabel(char content = '\0') : base()
        {
            InitialiseValues();
            Foreground = Brushes.White;

            Content = content;
        }

        public ExtendedLabel(Brush foreground, string content = "") : base()
        {
            InitialiseValues();
            Foreground = foreground;

            Content = content;
        }


        public ExtendedLabel(Brush foreground, char content = '\0') : base()
        {
            InitialiseValues();
            Foreground = foreground;

            Content = content;
        }


    }
}
