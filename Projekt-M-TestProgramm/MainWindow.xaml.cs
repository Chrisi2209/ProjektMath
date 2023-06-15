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
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Sum summe = new Sum();
            summe.Expressions = new BinArray<Expression>(4);
            summe[0] = new Function();
            (summe[0] as Function).Name = "-";
            (summe[0] as Function).Expression = new Number();
            ((summe[0] as Function).Expression as Number).Num = 12;

            summe[1] = new Function();
            (summe[1] as Function).Name = "+";
            (summe[1] as Function).Expression = new Number();
            ((summe[1] as Function).Expression as Number).Num = 5;

            summe[2] = new Product();
            (summe[2] as Product).Expressions = new BinArray<Expression>(2);

            (summe[2] as Product).Expressions[0] = new Function();
            ((summe[2] as Product).Expressions[0] as Function).Name = "-";
            ((summe[2] as Product).Expressions[0] as Function).Expression = new Number();
            (((summe[2] as Product).Expressions[0] as Function).Expression as Number).Num = 23;
            
            (summe[2] as Product).Expressions[1] = new Function();
            ((summe[2] as Product).Expressions[1] as Function).Name = "*";
            ((summe[2] as Product).Expressions[1] as Function).Expression = new Number();
            (((summe[2] as Product).Expressions[1] as Function).Expression as Number).Num = 17;

            summe[3] = new Function();
            (summe[3] as Function).Name = "+";
            (summe[3] as Function).Expression = new Fraction();

            ((summe[3] as Function).Expression as Fraction).ExpressionA = new Sum();
            (((summe[3] as Function).Expression as Fraction).ExpressionA as Sum).Expressions = new BinArray<Expression>(2);
            (((summe[3] as Function).Expression as Fraction).ExpressionA as Sum).Expressions[0] = new Function();
            ((((summe[3] as Function).Expression as Fraction).ExpressionA as Sum).Expressions[0] as Function).Name = "+";
            ((((summe[3] as Function).Expression as Fraction).ExpressionA as Sum).Expressions[0] as Function).Expression = new Number();
            (((((summe[3] as Function).Expression as Fraction).ExpressionA as Sum).Expressions[0] as Function).Expression as Number).Num = 4;

            (((summe[3] as Function).Expression as Fraction).ExpressionA as Sum).Expressions[1] = new Function();
            ((((summe[3] as Function).Expression as Fraction).ExpressionA as Sum).Expressions[1] as Function).Name = "-";

            ((((summe[3] as Function).Expression as Fraction).ExpressionA as Sum).Expressions[1] as Function).Expression = new Fraction();
            Fraction fr = ((((summe[3] as Function).Expression as Fraction).ExpressionA as Sum).Expressions[1] as Function).Expression as Fraction;
            fr.ExpressionA = new Function();
            (fr.ExpressionA as Function).Name = "+";
            (fr.ExpressionA as Function).Expression = new Number();
            ((fr.ExpressionA as Function).Expression as Number).Num = 12123;
            fr.ExpressionB = new Function();
            (fr.ExpressionB as Function).Name = "-";
            (fr.ExpressionB as Function).Expression = new Number();
            ((fr.ExpressionB as Function).Expression as Number).Num = 8;
            
            ((summe[3] as Function).Expression as Fraction).ExpressionB = new Function();
            (((summe[3] as Function).Expression as Fraction).ExpressionB as Function).Name = "+";
            (((summe[3] as Function).Expression as Fraction).ExpressionB as Function).Expression = new Number();
            ((((summe[3] as Function).Expression as Fraction).ExpressionB as Function).Expression as Number).Num = 5;

            
            summe.CreateUI();
            DockPanel.SetDock(summe.DockPanel, Dock.Left);
            EquationHistory.Children.Add(summe.DockPanel);

            // TestLabel.Content = summe[3].DockPanel.Width;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Fraction.updateHeightsAndLineWidths();
        }
    }


    public class Expression
    {
        // its own UI element
        protected ExtendedDockPanel dockPanel;
        public ExtendedDockPanel DockPanel {
            get {return dockPanel;} 
            protected set {dockPanel = value;}
        }

        public Expression BackPointer { get; set; }
        public int Index
        {
            get
            {
                for (int i = 0; i < Length; i++)
                {
                    if (this == BackPointer[i]) return i;
                }
                return -1;
            }
        }
        public virtual int Length
        {
            get { throw new Exception(); }
        }
        public virtual Expression this[int index]
        {
            get { throw new Exception(); }
            set { throw new Exception(); }
        }
        /* TODO
        static public Expression Create(string input, Expression backPointer = null)
        {

        }
        */

        public virtual ExtendedDockPanel CreateUI()
        {
            // Initialises the Dockpanel for this object
            DockPanel = new ExtendedDockPanel();
            // DockPanel.Margin = new Thickness(0);

            ExtendedDockPanel newChild;
            for (int i = 0; i < Length; i++)
            {
                newChild = this[i].CreateUI();
                DockPanel.DockAt(newChild, Dock.Left);
            }

            return DockPanel;
        }
    }


    public class NullExpression : Expression
    {
        /*
         * These are the expressions that only have a value in them, no more expression allowed in this thing.
         * Thus there is a Label and the DockPanel is no longer used.
         */

        public override int Length
        {
            get { return 0; }
        }


        public ExtendedDockPanel CreateLabelUI(string name)
        {
            dockPanel = new ExtendedDockPanel(Dock.Left, new ExtendedLabel(name));

            return dockPanel;
        }


    }

    public class UnitExpression : Expression
    {
        public Expression Expression { get; set; }
        public override int Length
        {
            get { return 1; }
        }
        public override Expression this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return Expression;
                    default: throw new IndexOutOfRangeException();
                }
            }
            set
            {
                switch (index)
                {
                    case 0: Expression = value; break;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }
    }

    public class DoubleExpression : Expression
    {
        public Expression ExpressionA { get; set; }
        public Expression ExpressionB { get; set; }
        public override int Length
        {
            get { return 2; }
        }
        public override Expression this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return ExpressionA;
                    case 1: return ExpressionB;
                    default: throw new IndexOutOfRangeException();
                }
            }
            set
            {
                switch (index)
                {
                    case 0: ExpressionA = value; break;
                    case 1: ExpressionB = value; break;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }
    }

    public class RepeatedExpression : Expression
    {
        public BinArray<Expression> Expressions { get; set; }
        public override int Length
        {
            get { return Expressions.Length; }
        }
        public override Expression this[int index]
        {
            get { return Expressions[index]; }
            set { Expressions[index] = value; }
        }
    }


    public class Number : NullExpression
    {
        private long num;
        public long Num
        {
            get { return num; }
            set { num = value; }
        }

        static public Number Create(string input)
        {
            Number number = new Number();
            // using data field for number.num because property can't be an out parameter
            if (!long.TryParse(input, out number.num)) number = null;
            return number;
        }

        public override ExtendedDockPanel CreateUI() 
        {
            DockPanel = CreateLabelUI(Convert.ToString(Num));
            return DockPanel;
        }

        public override string ToString()
        {
            return Num.ToString();
        }
    }

    public class Variable : NullExpression
    {
        public string Name { get; set; }
        /* TODO
        static new public Variable Create(string input)
        {
            Variable variable = new Variable();
        
        }
        */
        public override string ToString()
        {
            return Name;
        }
    }

    public class Function : UnitExpression
    {
        /*
         * Self declared and pre declared functions.
         * Special functions have specific names that are not allowed to be overridden in any kind!
         * ---
         * +: the addition sign
         * -: the subtraction sign
         * *: the multiplication sign
         * --- more to come ---
         * 
         */
        static Brush functionNameColor = Brushes.Orange;

        public string Name { get; set; }
        public string BracketOpen { get; set; }
        public string BracketClose { get; set; }

        public override ExtendedDockPanel CreateUI()
        {
            // create the dockpanel
            DockPanel = new ExtendedDockPanel(
                Dock.Left, 
                new ExtendedLabel(functionNameColor, Name), new ExtendedLabel(BracketOpen), Expression.CreateUI(), new ExtendedLabel(BracketClose)
            );
            return DockPanel;
        }
    }

    public class Fraction : DoubleExpression
    {
        static public BinArray<Fraction> needsHeightUpdate = new BinArray<Fraction>();

        public override ExtendedDockPanel CreateUI()
        {
            Rectangle a = new Rectangle();
            a.Height = 2;
            a.Fill = Brushes.White;

            DockPanel = new ExtendedDockPanel(Dock.Top, ExpressionA.CreateUI(), a, ExpressionB.CreateUI());
            DockPanel.Margin = new Thickness(10, 0, 10, 0);
            needsHeightUpdate.Append(this);

            return DockPanel;
        }

        static public void updateHeightsAndLineWidths()
        {
            /*
             * Updates all heights for the fractions that need a height update so that it is
             * centered again (upper expression is bigger than lower).
             * needsHeightUpdate is iterated in reverse because
             * MAYBE (TODO) then there can't be a fraction changed that would change the height of
             * a fraction that already has been reheighted.
             */

            for (int i = needsHeightUpdate.Length - 1; i >= 0; i--)
            {
                // Heights
                if (Math.Round(needsHeightUpdate[i].ExpressionA.DockPanel.ActualHeight, 3) > 
                    Math.Round(needsHeightUpdate[i].ExpressionB.DockPanel.ActualHeight, 3))
                {
                    needsHeightUpdate[i].ExpressionB.DockPanel.Height = needsHeightUpdate[i].ExpressionA.DockPanel.ActualHeight;
                }
                else if (Math.Round(needsHeightUpdate[i].ExpressionA.DockPanel.ActualHeight, 3) < 
                    Math.Round(needsHeightUpdate[i].ExpressionB.DockPanel.ActualHeight, 3))
                {
                    needsHeightUpdate[i].ExpressionA.DockPanel.Height = needsHeightUpdate[i].ExpressionB.DockPanel.ActualHeight;
                }

                // line width TODO should this actually be a factor instead of additive offset?
                (needsHeightUpdate[i].DockPanel.Children[1] as Rectangle).Width = (needsHeightUpdate[i].DockPanel.Children[1] as Rectangle).ActualWidth * 1.1;
            }
        }

        public override string ToString()
        {
            return "(" + ExpressionA.ToString() + "/" + ExpressionB.ToString() + ")";
        }
    }

    public class Power : DoubleExpression
    {
        public override string ToString()
        {
            return "(" + ExpressionA.ToString() + "^" + ExpressionB.ToString() + ")";
        }
    }

    public class Sum : RepeatedExpression
    {
        /*
         * Only for specific methods that are only possible with sums.
         * Here to group additions AND SUBTRACTIONS together
         * THE SUM DOES NOT ADD THE + AND - SIGNS, FUNCTION IS RESPONSIBLE FOR THIS!!!!
         */
        public override string ToString()
        {
            if (Length == 0) return "(+)";
            string output = "(";
            for (int i = 0; i < Length; i++) output += (this[0] is Number && (this[0] as Number).Num < 0 ? "" : "+") + this[0].ToString();
            output += ")";
            return output;
        }
    }

    public class Product : RepeatedExpression
    {
        public override string ToString()
        {
            if (Length == 0) return "(+)";
            string output = "(";
            for (int i = 0; i < Length; i++)
            {
                output += "*" + (this[0] is Number && (this[0] as Number).Num < 0 ? "(" + this[0].ToString() + ")" : this[0].ToString());
            }
            output += ")";
            return output;
        }
    }

    public class ExtendedLabel : Label
    {
        public ExtendedLabel(string content="") : base() 
        {
            Padding = new Thickness(0);
            Margin = new Thickness(1, 0, 1, 0);
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
            Foreground = Brushes.White;

            Content = content;
        }
        public ExtendedLabel(Brush foreground, string content = "") : base()
        {
            Padding = new Thickness(0);
            Margin = new Thickness(1, 0, 1, 0);
            Foreground = foreground;

            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;

            Content = content;
        }
    }

    public class ExtendedDockPanel: DockPanel
    {
        public ExtendedDockPanel(Dock dockDirection, params UIElement[] elements): base()
        {
            foreach (UIElement element in elements)
            {
                DockAt(element, dockDirection);
            }

            // Especially for fractions very helpful
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
        }

        public ExtendedDockPanel(): base() {}

        public void DockAt(UIElement element, Dock dockDirection)
        {
            DockPanel.SetDock(element, dockDirection);
            this.Children.Add(element);
        }
    }
}
