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
            summe[0] = new OperationPart();
            (summe[0] as OperationPart).Operation = '-';
            (summe[0] as OperationPart).Expression = new Number();
            ((summe[0] as OperationPart).Expression as Number).Num = 12;

            summe[1] = new OperationPart();
            (summe[1] as OperationPart).Operation = '+';
            (summe[1] as OperationPart).Expression = new Number();
            ((summe[1] as OperationPart).Expression as Number).Num = 5;

            summe[2] = new Product();
            (summe[2] as Product).Expressions = new BinArray<Expression>(2);

            (summe[2] as Product).Expressions[0] = new OperationPart();
            ((summe[2] as Product).Expressions[0] as OperationPart).Operation = '-';
            ((summe[2] as Product).Expressions[0] as OperationPart).Expression = new Number();
            (((summe[2] as Product).Expressions[0] as OperationPart).Expression as Number).Num = 23;
            
            (summe[2] as Product).Expressions[1] = new OperationPart();
            ((summe[2] as Product).Expressions[1] as OperationPart).Operation = '*';
            ((summe[2] as Product).Expressions[1] as OperationPart).Expression = new Number();
            (((summe[2] as Product).Expressions[1] as OperationPart).Expression as Number).Num = 17;

            summe[3] = new OperationPart();
            (summe[3] as OperationPart).Operation = '+';
            (summe[3] as OperationPart).Expression = new Fraction();

            ((summe[3] as OperationPart).Expression as Fraction).ExpressionA = new Sum();
            (((summe[3] as OperationPart).Expression as Fraction).ExpressionA as Sum).Expressions = new BinArray<Expression>(2);
            (((summe[3] as OperationPart).Expression as Fraction).ExpressionA as Sum).Expressions[0] = new OperationPart();
            ((((summe[3] as OperationPart).Expression as Fraction).ExpressionA as Sum).Expressions[0] as OperationPart).Operation = '+';
            ((((summe[3] as OperationPart).Expression as Fraction).ExpressionA as Sum).Expressions[0] as OperationPart).Expression = new Number();
            (((((summe[3] as OperationPart).Expression as Fraction).ExpressionA as Sum).Expressions[0] as OperationPart).Expression as Number).Num = 4;

            (((summe[3] as OperationPart).Expression as Fraction).ExpressionA as Sum).Expressions[1] = new OperationPart();
            ((((summe[3] as OperationPart).Expression as Fraction).ExpressionA as Sum).Expressions[1] as OperationPart).Operation = '-';

            ((((summe[3] as OperationPart).Expression as Fraction).ExpressionA as Sum).Expressions[1] as OperationPart).Expression = new Fraction();
            Fraction fr = ((((summe[3] as OperationPart).Expression as Fraction).ExpressionA as Sum).Expressions[1] as OperationPart).Expression as Fraction;
            fr.ExpressionA = new OperationPart();
            (fr.ExpressionA as OperationPart).Operation = '+';
            (fr.ExpressionA as OperationPart).Expression = new Number();
            ((fr.ExpressionA as OperationPart).Expression as Number).Num = 12123;
            fr.ExpressionB = new OperationPart();
            (fr.ExpressionB as OperationPart).Operation = '-';
            (fr.ExpressionB as OperationPart).Expression = new Number();
            ((fr.ExpressionB as OperationPart).Expression as Number).Num = 8;
            
            ((summe[3] as OperationPart).Expression as Fraction).ExpressionB = new OperationPart();
            (((summe[3] as OperationPart).Expression as Fraction).ExpressionB as OperationPart).Operation = '+';
            (((summe[3] as OperationPart).Expression as Fraction).ExpressionB as OperationPart).Expression = new Number();
            ((((summe[3] as OperationPart).Expression as Fraction).ExpressionB as OperationPart).Expression as Number).Num = 5;

            
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
        public bool? Visible { get; set; }
        public int Index
        {
            get
            {
                for (int i = 0; i < Length; i++)
                {
                    if (this == BackPointer[i]) return i;
                }
                throw new Exception();
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

        static public Expression Create(string input, Expression backPointer = null)
        {
            Expression expression;
            while (true)
            {
                expression = NullExpression.Create(input);
                if (expression != null) break;
                expression = UnitExpression.Create(input);
                if (expression != null) break;
                expression = DoubleExpression.Create(input);
                if (expression != null) break;
                expression = RepeatedExpression.Create(input);
                if (expression != null) break;
                throw new Exception();
            }
            expression.BackPointer = backPointer;
            return expression;
        }
        

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

        static public NullExpression Create(string input)
        {
            NullExpression nullExpression;
            while (true)
            {
                nullExpression = EmptyExpression.Create(input);
                if (nullExpression != null) break;
                nullExpression = Number.Create(input);
                if (nullExpression != null) break;
                nullExpression = Variable.Create(input);
                if (nullExpression != null) break;
                return null;
            }
            return nullExpression;
        }
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

        static public UnitExpression Create(string input)
        {
            UnitExpression unitExpression;
            string nextInput;
            while (true)
            {
                unitExpression = Function.Create(input, out nextInput);
                if (unitExpression != null) break;
                unitExpression = OperationPart.Create(input, out nextInput);
                if (unitExpression != null) break;
                return null;
            }
            unitExpression.Expression = Expression.Create(nextInput, unitExpression);
            return unitExpression;
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

        static public DoubleExpression Create(string input)
        {
            DoubleExpression doubleExpression;
            while (true)
            {
                doubleExpression = Fraction.Create(input);
                if (doubleExpression != null) break;
                doubleExpression = Power.Create(input);
                if (doubleExpression != null) break;
                return null;
            }
            return doubleExpression;
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

        static public RepeatedExpression Create(string input)
        {
            RepeatedExpression repetedExpression;
            while (true)
            {
                repetedExpression = Sum.Create(input);
                if (repetedExpression != null) break;
                repetedExpression = Product.Create(input);
                if (repetedExpression != null) break;
                return null;
            }
            return repetedExpression;
        }
    }

    public class EmptyExpression : NullExpression
    {
        static public new EmptyExpression Create(string input)
        {
            if (input == "") return new EmptyExpression();
            else return null;
        }

        public override string ToString()
        {
            return "";
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

        static public new Number Create(string input)
        {
            Number number = new Number();
            if (input.Length == 0 || input[0] < '0' || '9' < input[0]) return null;
            // using data field for number.num because property can't be an out parameter
            if (!long.TryParse(input, out number.num)) number = null;
            return number;
        }

        public override ExtendedDockPanel CreateUI() 
        {
            DockPanel = CreateLabelUI(Convert.ToString(Num));
            return DockPanel;
        }
        static public bool IsDigit(char c)
        {
            return '0' <= c && c <= '9';
        }

        static public void GoBehindNumber(string input, ref int index)
        {
            while (index < input.Length && IsDigit(input[index])) index++;
        }

        public override string ToString()
        {
            return Num.ToString();
        }
    }

    public class Variable : NullExpression
    {
        public string Name { get; set; }

        static public new Variable Create(string input)
        {
            Variable variable = new Variable();
            for (int i = 0; i < input.Length; i++)
            {
                if ((input[i] < 'a' || 'z' < input[i]) && (input[i] < 'A' || 'Z' < input[i])) return null;
            }
            variable.Name = input;
            return variable;
        }

        static public bool IsLetter(char c)
        {
            return 'a' <= c && c <= 'z' || 'A' <= c && c <= 'Z';
        }

        static public void GoBehindVariable(string input, ref int index)
        {
            while (index < input.Length && IsLetter(input[index])) index++;
        }
        
        public override string ToString()
        {
            return Name;
        }
    }

    public class OperationPart : UnitExpression
    {
        static Brush operationColor = Brushes.Blue;

        public char Operation { get; set; }

        static public OperationPart Create(string input, out string nextInput)
        {
            OperationPart operationPart = new OperationPart();
            nextInput = null;
            if (input.Length == 0) return null;
            int index = 1;
            switch (input[0])
            {
                case '+':
                case '-':
                    {
                        while (index < input.Length)
                        {
                            switch (input[index++])
                            {
                                case '+':
                                case '-': return null;
                                case '*':
                                case '/':
                                case '^': break;
                                case '(': Function.GoOutOfBracket(input, ref index); break;
                                case char c when Number.IsDigit(c): Number.GoBehindNumber(input, ref index); break;
                                case char c when Variable.IsLetter(c): Variable.GoBehindVariable(input, ref index); break;
                            }
                        }
                        break;
                    }
                case '*':
                    {
                        while (index < input.Length)
                        {
                            switch (input[index++])
                            {
                                case '+':
                                case '-':
                                case '*':
                                case '/': return null;
                                case '^': break;
                                case '(': Function.GoOutOfBracket(input, ref index); break;
                                case char c when Number.IsDigit(c): Number.GoBehindNumber(input, ref index); break;
                                case char c when Variable.IsLetter(c): Variable.GoBehindVariable(input, ref index); break;
                            }
                        }
                        break;
                    }
                default: return null;
            }
            operationPart.Operation = input[0];
            nextInput = input.Substring(1);
            return operationPart;
        }

        public override ExtendedDockPanel CreateUI()
        {
            // create the dockpanel
            DockPanel = new ExtendedDockPanel(
                Dock.Left,
                new ExtendedLabel(operationColor, Operation), Expression.CreateUI()
            );
            return DockPanel;
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
        public bool? VisibleOpen { get; set; }
        public char BracketOpen { get; set; }
        public bool? VisibleClose { get; set; }
        public char BracketClose { get; set; }

        public override ExtendedDockPanel CreateUI()
        {
            // create the dockpanel
            DockPanel = new ExtendedDockPanel(
                Dock.Left, 
                new ExtendedLabel(functionNameColor, Name), new ExtendedLabel(BracketOpen), Expression.CreateUI(), new ExtendedLabel(BracketClose)
            );
            return DockPanel;
        }

        static public Function Create(string input, out string nextInput)
        {
            Function funktion = new Function();
            nextInput = null;
            if (input.Length < 2) return null;
            switch (input[0])
            {
                case '(':
                    {
                        int index = 1;
                        GoOutOfBracket(input, ref index);
                        if (index < 0) throw new ArgumentOutOfRangeException();
                        if (index < input.Length) return null;
                        funktion.Name = "";
                        break;
                    }
                case char c when Variable.IsLetter(c):
                    {
                        int index = 1;
                        Variable.GoBehindVariable(input, ref index);
                        if (index == input.Length || input[index++] != '(') return null;
                        funktion.Name = input.Remove(index - 1);
                        GoOutOfBracket(input, ref index);
                        if (index < input.Length) return null;
                        break;
                    }
                default: return null;
            }
            funktion.BracketOpen = '(';
            funktion.BracketClose = ')';
            nextInput = input.Substring(funktion.Name.Length + 1, input.Length - funktion.Name.Length - 2);
            return funktion;
        }

        /// <summary>
        /// searches for the correct bracketClose
        /// </summary>
        /// <param name="input"></param>
        /// <param name="index">this index starts inside of the brackets, returns -1 when not found</param>
        /// <param name="bracketCounter"> '(' --> ++;     ')' --> --; </param>
        static public void GoOutOfBracket(string input, ref int index, int bracketCounter = 1)
        {
            for (; index < input.Length && 0 < bracketCounter; index++)
            {
                switch (input[index])
                {
                    case '(': bracketCounter++; break;
                    case ')': bracketCounter--; break;
                }
            }
            if (0 < bracketCounter) index = -1;
            return;
        }
    }

    public class Fraction : DoubleExpression
    {
        static public BinArray<Fraction> needsHeightUpdate = new BinArray<Fraction>();

        public override ExtendedDockPanel CreateUI()
        {
            Rectangle fractionBar = new Rectangle();

            fractionBar.Height = 2;
            fractionBar.Fill = Brushes.White;

            ExtendedDockPanel TopUi = ExpressionA.CreateUI();
            ExtendedDockPanel BottomUi = ExpressionB.CreateUI();

            // fraction bar should be bigger than content
            TopUi.SetSideMargin();
            BottomUi.SetSideMargin();

            DockPanel = new ExtendedDockPanel(Dock.Top, TopUi, fractionBar, BottomUi);

            // just a little distance before and after the fraction bar
            DockPanel.SetSideMargin();
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

            foreach (Fraction fraction in needsHeightUpdate)
            {
                // Heights
                if (Math.Round(fraction.ExpressionA.DockPanel.ActualHeight, 3) >
                    Math.Round(fraction.ExpressionB.DockPanel.ActualHeight, 3))
                {
                    fraction.ExpressionB.DockPanel.Height = fraction.ExpressionA.DockPanel.ActualHeight;
                }
                else if (Math.Round(fraction.ExpressionA.DockPanel.ActualHeight, 3) <
                    Math.Round(fraction.ExpressionB.DockPanel.ActualHeight, 3))
                {
                    fraction.ExpressionA.DockPanel.Height = fraction.ExpressionB.DockPanel.ActualHeight;
                }

                // line width TODO should this actually be a factor instead of additive offset?
                (fraction.DockPanel.Children[1] as Rectangle).Width = (fraction.DockPanel.Children[1] as Rectangle).ActualWidth * 1.1;
            }
        }
        
        static public Fraction Create(string input, out string nextInputA, out string nextInputB)
        {
            Fraction fraction = new Fraction();
            nextInputA = null;
            nextInputB = null;
            int index = 0;
            while (true)
            {
                switch (input[index++])
                {
                    case '(':
                        {
                            Function.GoOutOfBracket(input, ref index);
                            if (index < 0) throw new ArgumentOutOfRangeException();
                            break;
                        }
                    case char c when Number.IsDigit(c):
                        {
                            Number.GoBehindNumber(input, ref index);
                            break;
                        }
                    case char c when Variable.IsLetter(c):
                        {
                            Variable.GoBehindVariable(input, ref index);
                            break;
                        }
                    default: return null;
                }
                if (index == input.Length) return null;
                switch (input[index])
                {
                    case '/': break;
                    case '^': continue;
                    default: return null;
                }
                break;
            }
            nextInputA = input.Remove(index++);
            if (index == input.Length) return null;
            nextInputB = input.Substring(index);
            while (true)
            {
                switch (input[index++])
                {
                    case '(':
                        {
                            Function.GoOutOfBracket(input, ref index);
                            if (index < 0) throw new ArgumentOutOfRangeException();
                            break;
                        }
                    case char c when Number.IsDigit(c):
                        {
                            Number.GoBehindNumber(input, ref index);
                            break;
                        }
                    case char c when Variable.IsLetter(c):
                        {
                            Variable.GoBehindVariable(input, ref index);
                            break;
                        }
                    default: return null;
                }
                if (index == input.Length) break;
                if (input[index++] == '^') continue;
                return null;
            }
            return fraction;
        }

        public override string ToString()
        {
            return "(" + ExpressionA.ToString() + "/" + ExpressionB.ToString() + ")";
        }
    }

    public class Power : DoubleExpression
    {
        static public new Power Create(string input )
        {
            return null;
        }

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
        static public new Sum Create(string input)
        {
            return null;
        }

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
        static public new Product Create(string input)
        {
            return null;
        }

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
        private void InitialiseValues()
        {
            // helper for constructors
            Padding = new Thickness(0);
            Margin = new Thickness(1, 0, 1, 0);
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
        }

        public ExtendedLabel(string content="") : base() 
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

        public ExtendedDockPanel(): base() { }

        public void DockAt(UIElement element, Dock dockDirection)
        {
            DockPanel.SetDock(element, dockDirection);
            this.Children.Add(element);
        }

        public void SetSideMargin()
        {
            // Sometimes, some distance makes us happy
            Margin = new Thickness(5, 0, 5, 0);
        }
    }
}
