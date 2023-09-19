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
            KeyDown += ScrollViewer_KeyDown;
            /*
            Sum summe = new Sum();
            summe.Expressions = new BinArray<Expression>(4);
            summe[0] = new OperationPart();
            (summe[0] as OperationPart).Operation = '-';
            (summe[0] as OperationPart).Expression = new Number();
            ((summe[0] as OperationPart).Expression as Number).Num = 12;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Update heights on fractions so that the fraction bars are centered
            //Fraction.updateHeightsAndLineWidths();
        }

        Key key;
        Expression expression;
        private void ScrollViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Oem5)
            {
                e.Handled = true;
                InputManagementSystem.AddInput(e.Key, '^');
                expression = Expression.Create(InputManagementSystem.ConvertToStringInfo(InputManagementSystem.strCur, out _));
                EquationHistory.DockAt(expression.CreateUI(), Dock.Top);
            }
            else key = e.Key;
        }
        private void ScrollViewer_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (InputManagementSystem.AddInput(key, e.Text[0])) { }
            StringInfo strInf = InputManagementSystem.ConvertToStringInfo(InputManagementSystem.strCur, out _);
            expression = Expression.Create(strInf);
            expression.CreateUI();
            EquationHistory.DockAt(expression.DockPanel, Dock.Top);
        }
    }



    public enum Design
    {
        Visible,
        Hidden,
        Ghost,
        Auto
    }

    public class CharInfo
    {
        public char C { get; set; }
        public Design Design { get; set; }

        public CharInfo() { }
        public CharInfo(char c, Design design = Design.Visible)
        {
            C = c;
            Design = design;
        }
        static public implicit operator CharInfo(char c)
        {
            return new CharInfo(c);
        }

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
            */
            
            Expression summe = Expression.Create("(1+1)/1");
            summe.CreateUI();
            EquationHistory.DockAt(summe.DockPanel, Dock.Top);
            
            // TestLabel.Content = summe[3].DockPanel.Width;
            
        }
        public StringInfo(string input) : base(input.Length)
        {
            for (int i = 0; i < input.Length; i++) Add(new CharInfo(input[i]));
        }
        static public implicit operator StringInfo(string input)
        {
            return new StringInfo(input);
        }
        public StringInfo(IEnumerable<CharInfo> charInfos) : base(charInfos) { }

        static public StringInfo operator +(StringInfo stringInfo, CharInfo ci)
        {
            StringInfo output = new StringInfo(stringInfo);
            output.Add(ci);
            return output;
        }
        static public StringInfo operator +(CharInfo ci, StringInfo stringInfo)
        {
            StringInfo output = new StringInfo(stringInfo);
            output.Insert(0, ci);
            return output;
        }
        static public StringInfo operator +(StringInfo stringInfo0, StringInfo stringInfo1)
        {
            StringInfo output = new StringInfo(stringInfo0);
            output.AddRange(stringInfo1);
            return output;
        }
        static public StringInfo operator *(StringInfo stringInfo, int factor)
        {
            if (factor < 0) throw new ArgumentException();
            StringInfo output = new StringInfo();
            for (; 0 < factor; factor--) output.AddRange(stringInfo);
            return output;
        }
        static public StringInfo operator *(int factor, StringInfo stringInfo)
        {
            if (factor < 0) throw new ArgumentException();
            StringInfo output = new StringInfo();
            for (; 0 < factor; factor--) output.AddRange(stringInfo);
            return output;
        }




        private void ScrollViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(TestLabel.Content) == 1)
            {
                TestLabel.Content = 0;
            }
            else
            {
                String = String.Substring(Cursor);
                Cursor = 0;
            }
        }

        public void Clear()
        {
            Cursor = 0;
            String = "";
        }
        public void WriteNew(string str)
        {
            String = str;
            Cursor = Length;
        }

        public void Write(char c)
        {
            String = String.Insert(Cursor, c.ToString());
        }
        public void Write(string str)
        {
            String = String.Insert(Cursor, str);
        }
        public void OverWrite(char c)
        {
            Delete();
            Write(c);
        }
        public void Overwrite(string str)
        {
            Delete(str.Length);
            Write(str);
        }

        public void Insert(int pos, char c)
        {
            String = String.Insert(pos, c.ToString());
            if (pos <= Cursor) Cursor++;
        }
        public void Insert(int pos, string str)
        {
            String = String.Insert(pos, str);
            if (pos <= Cursor) Cursor += str.Length;
        }

        public new string ToString()
        {
            return String + " " + Cursor + " (" + CurentChar + ")";
        }
    }

    static public class InputManagementSystem
    {
        static public StringCursor strCur = new StringCursor();

        static public bool AddInput(Key key, char ch)
        {
            switch (key)
            {
                case Key.Left: strCur.GoLeft(); break;
                case Key.Right: strCur.GoRight(); break;
                case Key.Up: strCur.GoStart(); break;
                case Key.Down: strCur.GoEnd(); break;

                case Key.Back: strCur.Backspace(); break;
                case Key.Delete: strCur.Delete(); break;

                case Key.Enter: return true;

                default:
                    {
                        switch (ch)
                        {
                            case char c when '0' <= c && c <= '9' || 'a' <= c && c <= 'z' || 'A' <= c && c <= 'Z':
                            case '+':
                            case '-':
                            case '*':
                            case '/':
                            case '(':
                            case ')':
                                {
                                    strCur.Write(ch);
                                    break;
                                }
                        }
                        break;
                    }
            }
            return false;
        }
        static private bool AddInput(StringCursor strCur, ConsoleKeyInfo cki)
        {
            switch (cki.Key)
            {
                case ConsoleKey.LeftArrow: strCur.GoLeft(); break;
                case ConsoleKey.RightArrow: strCur.GoRight(); break;
                case ConsoleKey.UpArrow: strCur.GoStart(); break;
                case ConsoleKey.DownArrow: strCur.GoEnd(); break;

                case ConsoleKey.Backspace: strCur.Backspace(); break;
                case ConsoleKey.Delete: strCur.Delete(); break;

                case ConsoleKey.Enter: return true;

                default:
                    {
                        switch (cki.KeyChar)
                        {
                            case char c when '0' <= c && c <= '9' || 'a' <= c && c <= 'z' || 'A' <= c && c <= 'Z':
                            case '+':
                            case '-':
                            case '*':
                            case '/':
                            case '(':
                            case ')':
                                {
                                    strCur.Write(cki.KeyChar);
                                    break;
                                }
                        }
                        break;
                    }
            }
            return false;
        }
        static public StringInfo ConvertToStringInfo(StringCursor strCur, out int cursor)
        {
            StringInfo result = strCur.String;
            cursor = strCur.Cursor;
            int bracketCounter = 0, bracketsOpen = 0;

            //counts how many brackets should be added
            //and puts hidden Multiplications
            bool number = false, variable = false, bracket = false;
            for (int i = 0; i < strCur.Length; i++)
            {
                switch (strCur[i])
                {
                    case char c when Number.IsDigit(c):
                        {
                            if (variable||bracket)
                            {
                                result.Insert(i, new CharInfo('*', Design.Hidden));
                                if (i++ <= cursor) cursor++;
                                variable = false;
                                bracket = false;
                            }
                            number = true;
                            break;
                        }
                    case char c when Variable.IsLetter(c):
                        {
                            if (number || bracket)
                            {
                                result.Insert(i, new CharInfo('*', Design.Hidden));
                                if (i++ <= cursor) cursor++;
                                number = false;
                                bracket = false;
                            }
                            variable = true;
                            break;
                        }
                    case '(':
                        {
                            bracketCounter++;
                            if (number || bracket)
                            {
                                result.Insert(i, new CharInfo('*', Design.Hidden));
                                if (i++ <= cursor) cursor++;
                                number = false;
                                bracket = false;
                            }
                            variable = false;
                            break;
                        }
                    case ')':
                        {
                            bracketCounter--;
                            number = false;
                            variable = false;
                            bracket = true;
                            break;
                        }
                    case '*':
                        {
                            if (number || variable || bracket)
                            {
                                number = false;
                                variable = false;
                                bracket = false;
                            }
                            else
                            {
                                result.Insert(i, new CharInfo(' ', Design.Hidden));
                                if (i++ <= cursor) cursor++;
                            }
                            break;
                        }
                    default:
                        {
                            number = false;
                            variable = false;
                            bracket = false;
                            break;
                        }
                }
                if (bracketCounter < 0)
                {
                    bracketCounter++;
                    bracketsOpen++;
                }
            }

            EquationHistory.DockAt(Expression.Create(inputString).CreateUI(), Dock.Top);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TestLabel.Content = "abcdefg";
        }
    }

    public class Expression
    {
        // its own UI element
        private ExtendedDockPanel dockPanel;
        private bool? visible;

        public ExtendedDockPanel DockPanel
        {
            get { return dockPanel; }
            protected set { dockPanel = value; }
        }
        public Expression BackPointer { get; set; }
        public bool? Visible
        {
            get { return visible; }
            set
            {
                visible = value;
                for (int i = 0; i < Length; i++) this[i].Visible = value;
            }
        }
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

        static public Expression Create(StringInfo input, Expression backPointer = null)
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
            expression.visible = true;
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
                newChild = this[i].CreateUI(fontSize);
                DockPanel.DockAt(newChild, Dock.Left);
            }

            return DockPanel;
        }

        public override string ToString()
        {
            return "#";
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

        static public NullExpression Create(StringInfo input)
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


        public ExtendedDockPanel CreateLabelUI(string name)
        {
            DockPanel = new ExtendedDockPanel(fontSize, Dock.Left, new ExtendedLabel(name));

            return DockPanel;
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

        static public UnitExpression Create(StringInfo input)
        {
            UnitExpression unitExpression;
            StringInfo nextInput;
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

        static public DoubleExpression Create(StringInfo input)
        {
            DoubleExpression doubleExpression;
            StringInfo nextInputA, nextInputB;
            while (true)
            {
                doubleExpression = Fraction.Create(input, out nextInputA, out nextInputB);
                if (doubleExpression != null) break;
                doubleExpression = Power.Create(input, out nextInputA, out nextInputB);
                if (doubleExpression != null) break;
                return null;
            }
            doubleExpression.ExpressionA = Expression.Create(nextInputA, doubleExpression);
            doubleExpression.ExpressionB = Expression.Create(nextInputB, doubleExpression);
            return doubleExpression;
        }
    }

    public class RepeatedExpression : Expression
    {
        public List<Expression> Expressions { get; set; }
        public override int Length
        {
            get { return Expressions.Count; }
        }
        public override Expression this[int index]
        {
            get { return Expressions[index]; }
            set { Expressions[index] = value; }
        }

        static public RepeatedExpression Create(StringInfo input)
        {
            RepeatedExpression repeatedExpression;
            List<StringInfo> nextInputs;

            while (true)
            {
                repeatedExpression = Sum.Create(input, out nextInputs);
                if (repeatedExpression != null) break;
                repeatedExpression = Product.Create(input, out nextInputs);
                if (repeatedExpression != null) break;
                return null;
            }
            repeatedExpression.Expressions = new List<Expression>(nextInputs.Count);
            for (int i = 0; i < nextInputs.Count; i++)
            {
                repeatedExpression.Expressions.Add(Expression.Create(nextInputs[i], repeatedExpression));
            }
            return repeatedExpression;
        }
    }


    public class EmptyExpression : NullExpression
    {
        static public char Symbol = ' ';

        static public new EmptyExpression Create(StringInfo input)
        {
            switch (input.ToString())
            {
                case "":
                case " ": return new EmptyExpression();
                default: return null;
            }
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

        static public new Number Create(StringInfo input)
        {
            if (input.Count == 0) return null;
            Number number = new Number();
            int index = 0;
            GoBehindNumber(input, ref index);
            if (index < input.Count) return null;
            number.Num = Convert.ToInt64(input.ToString());
            return number;
        }

        public override ExtendedDockPanel CreateUI() 
        {
            DockPanel = CreateLabelUI(fontSize, Convert.ToString(Num));
            return DockPanel;
        }
        static public bool IsDigit(char c)
        {
            return '0' <= c && c <= '9';
        }

        static public void GoBehindNumber(StringInfo input, ref int index)
        {
            while (index < input.Count && IsDigit(input[index].C)) index++;
        }

        public override string ToString()
        {
            return Num.ToString();
        }
    }

    public class Variable : NullExpression
    {
        public string Name { get; set; }

        static public new Variable Create(StringInfo input)
        {
            if (input.Count == 0) return null;
            Variable variable = new Variable();
            int index = 0;
            GoBehindVariable(input, ref index);
            if (index < input.Count) return null;
            variable.Name = input.ToString();
            return variable;
        }

        static public bool IsLetter(char c)
        {
            return 'a' <= c && c <= 'z' || 'A' <= c && c <= 'Z';
        }

        static public void GoBehindVariable(StringInfo input, ref int index)
        {
            while (index < input.Count && IsLetter(input[index].C)) index++;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class OperationPart : UnitExpression
    {
        static Brush operationColor = Brushes.Blue;

        public CharInfo Operation { get; set; }

        public OperationPart() : base()
        {
            Operation = new CharInfo();
        }

        static public OperationPart Create(StringInfo input, out StringInfo nextInput)
        {
            OperationPart operationPart = new OperationPart();
            nextInput = null;
            if (input.Count == 0) return null;
            int index = 0;
            switch (input[index++].C)
            {
                case '+':
                case '-':
                    {
                        while (index < input.Count)
                        {
                            switch (input[index++].C)
                            {
                                case '+':
                                case '-': return null;
                                case '*':
                                case '/':
                                case '^': break;
                                case '(':
                                    {
                                        Function.GoBehindBracket(input, ref index);
                                        if (index < 0) throw new ArgumentException();
                                        break;
                                    }
                            }
                        }
                        break;
                    }
                case '*':
                    {

                        while (index < input.Count)
                        {
                            switch (input[index++].C)
                            {
                                case '+':
                                case '-':
                                case '*': return null;
                                case '/':
                                case '^': break;
                                case '(':
                                    {
                                        Function.GoBehindBracket(input, ref index);
                                        if (index < 0) throw new ArgumentException();
                                        break;
                                    }
                            }
                        }
                        break;
                    }
                default: return null;
            }
            operationPart.Operation.C = input[0].C;
            nextInput = input.Substring(1);
            return operationPart;
        }

        public override ExtendedDockPanel CreateUI(double fontSize)
        {
            // create the dockpanel
            DockPanel = new ExtendedDockPanel(
                fontSize,
                Dock.Left,
                new ExtendedLabel(operationColor, Operation), Expression.CreateUI()
            );
            return DockPanel;
        }

        static public bool IsOperation(char c)
        {
            switch (c)
            {
                case '+':
                case '-':
                case '*':
                case '/':
                case '^': return true;
                default: return false;
            }
        }

        public override string ToString()
        {
            return Operation.C + Expression.ToString();
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

        static public Function Create(StringInfo input, out StringInfo nextInput)
        {
            Function funktion = new Function();
            nextInput = null;
            if (input.Count < 2) return null;
            int index = 0;
            switch (input[index++].C)
            {
                case '(':
                    {
                        GoBehindBracket(input, ref index);
                        if (index < 0) throw new ArgumentOutOfRangeException();
                        if (index < input.Count) return null;
                        funktion.Name = "";
                        break;
                    }
                case char c when Variable.IsLetter(c):
                    {
                        Variable.GoBehindVariable(input, ref index);
                        if (index == input.Count || input[index].C != '(') return null;
                        funktion.Name = input.Remove(index++).ToString();
                        GoBehindBracket(input, ref index);
                        if (index < 0) throw new ArgumentOutOfRangeException();
                        if (index < input.Count) return null;
                        break;
                    }
                default: return null;
            }
            int bracketStart = funktion.Name.Length;
            int bracketEnd = input.Count - 1;
            funktion.BracketOpen = input[bracketStart++];
            funktion.BracketClose = input[bracketEnd];
            nextInput = input.Substring(bracketStart, bracketEnd - bracketStart);
            return funktion;
        }

        public override ExtendedDockPanel CreateUI()
        {
            // create the dockpanel
            DockPanel = new ExtendedDockPanel(
                Dock.Left,
                new ExtendedLabel(functionNameColor, Name), new ExtendedLabel(BracketOpen.C), Expression.CreateUI(), new ExtendedLabel(BracketClose.C)
            );
            return DockPanel;
        }

        /// <summary>
        /// searches for the correct bracketClose
        /// </summary>
        /// <param name="input"></param>
        /// <param name="index">this index starts inside of the brackets, returns -1 when not found</param>
        /// <param name="bracketCounter"> '(' --> ++;     ')' --> --; </param>
        static public void GoBehindBracket(StringInfo input, ref int index, int bracketCounter = 1)
        {
            while (bracketCounter != 0 && index < input.Count)
            {
                switch (input[index++].C)
                {
                    case '(': bracketCounter++; break;
                    case ')': bracketCounter--; break;
                }
            }
            if (bracketCounter != 0) index = -1;
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
            while (bracketCounter != 0 && 0 <= index)
            {
                switch (input[index--].C)
                {
                    case '(': bracketCounter++; break;
                    case ')': bracketCounter--; break;
                }
            }
            if (bracketCounter != 0) index = -1;
            else index++;
            return;
        }

        public override string ToString()
        {
            return Name + "(" + Expression.ToString() + ")";
        }
    }

    public class Fraction : DoubleExpression
    {
        static public List<Fraction> needsHeightUpdate = new List<Fraction>(2);

        static public Fraction Create(StringInfo input, out StringInfo nextInputA, out StringInfo nextInputB)
        {
            Fraction fraction = new Fraction();
            nextInputA = null;
            nextInputB = null;
            int index = 0;
            while (true)
            {
                switch (input[index++].C)
                {
                    case '/':
                        {
                            index--;
                            break;
                        }
                    case '(':
                        {
                            Function.GoBehindBracket(input, ref index);
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
                            if (index == input.Count) return null;
                            if (input[index].C == '(')
                            {
                                index++;
                                Function.GoBehindBracket(input, ref index);
                                if (index < 0) throw new ArgumentOutOfRangeException();
                            }
                            break;
                        }
                    default: return null;
                }
                if (index == input.Count) return null;
                switch (input[index].C)
                {
                    case '/': break;
                    case '^': continue;
                    default: return null;
                }
                break;
            }
            while (true)
            {
                nextInputA = input.Remove(index++);
                nextInputB = input.Substring(index);
                if (index == input.Count) return fraction;
                while (true)
                {
                    switch (input[index++].C)
                    {
                        case '/':
                            {
                                index--;
                                break;
                            }
                        case '(':
                            {
                                Function.GoBehindBracket(input, ref index);
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
                                if (index == input.Count) return fraction;
                                if (input[index].C == '(')
                                {
                                    index++;
                                    Function.GoBehindBracket(input, ref index);
                                    if (index < 0) throw new ArgumentOutOfRangeException();
                                }
                                break;
                            }
                        default: return null;
                    }
                    if (index == input.Count) return fraction;
                    if (input[index].C == '/') break;
                    if (input[index++].C == '^') continue;
                    return null;
                }
            }
        }

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

        public override string ToString()
        {
            return ExpressionA.ToString() + "/" + ExpressionB.ToString();
        }
    }

    public class Power : DoubleExpression
    {
        static public Power Create(StringInfo input, out StringInfo nextInputA, out StringInfo nextInputB)
        {
            Power power = new Power();
            nextInputA = null;
            nextInputB = null;
            int index = 0;
            switch (input[index++].C)
            {
                case '^':
                    {
                        index--;
                        break;
                    }
                case '(':
                    {
                        Function.GoBehindBracket(input, ref index);
                        if (index < 0) throw new ArgumentException();
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
                        if (index == input.Count) return null;
                        if (input[index].C == '(')
                        {
                            index++;
                            Function.GoBehindBracket(input, ref index);
                            if (index < 0) throw new ArgumentException();
                        }
                        break;
                    }
            }
            nextInputA = input.Remove(index);
            if (index == input.Count || input[index++].C != '^') return null;
            nextInputB = input.Substring(index);
            if (index == input.Count) return power;
            while (true)
            {
                switch (input[index++].C)
                {
                    case '(':
                        {
                            Function.GoBehindBracket(input, ref index);
                            if (index < 0) throw new ArgumentException();
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
                            if (index == input.Count) return power;
                            if (input[index].C == '(')
                            {
                                index++;
                                Function.GoBehindBracket(input, ref index);
                                if (index < 0) throw new ArgumentException();
                            }
                            break;
                        }
                }
                if (index != input.Count)
                {
                    if (input[index++].C == '^') continue;
                    return null;
                }
                return power;
            }
        }

        public override ExtendedDockPanel CreateUI(double fontSize)
        {
            DockPanel = new ExtendedDockPanel(
                fontSize, Dock.Left, 
                ExpressionA.CreateUI(fontSize),
                new ExtendedLabel(""),
                new ExtendedDockPanel(fontSize / 1.5, Dock.Top, ExpressionB.CreateUI(fontSize / 1.5), new ExtendedLabel(" ")));
            // ExpressionB.DockPanel.SetSideMargin();
            return DockPanel;
        }

        public override string ToString()
        {
            return ExpressionA.ToString() + "^" + ExpressionB.ToString();
        }
    }

    public class Sum : RepeatedExpression
    {
        /*
        * Only for specific methods that are only possible with sums.
        * Here to group additions AND SUBTRACTIONS together
        * THE SUM DOES NOT ADD THE + AND - SIGNS, FUNCTION IS RESPONSIBLE FOR THIS!!!!
        */
        static public Sum Create(StringInfo input, out List<StringInfo> nextInputs)
        {
            Sum sum = new Sum();
            nextInputs = new List<StringInfo>(2);
            int index = 0;
            if (input.Count == 0) return null;
            if (input[index].C == '+' || input[index].C == '-') index++;
            while (index < input.Count)
            {
                switch (input[index++].C)
                {
                    case '+':
                    case '-':
                        {
                            nextInputs.Add(input.Remove(--index));
                            input = input.Substring(index);
                            index = 1;
                            break;
                        }
                    case '(':
                        {
                            Function.GoBehindBracket(input, ref index);
                            if (index < 0) throw new ArgumentOutOfRangeException();
                            break;
                        }
                    default: break;
                }
            }
            nextInputs.Add(input);
            if (nextInputs.Count < 2) return null;
            return sum;
        }

        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < Length; i++) output += this[i].ToString();
            return output;
        }
    }

    public class Product : RepeatedExpression
    {
        static public Product Create(StringInfo input, out List<StringInfo> nextInputs)
        {
            Product product = new Product();
            nextInputs = new List<StringInfo>(2);
            int index = 0;
            while (index < input.Count)
            {
                switch (input[index++].C)
                {
                    case '+':
                    case '-': return null;
                    case '*':
                        {
                            nextInputs.Add(input.Remove(--index));
                            input = input.Substring(index);
                            index = 1;
                            break;
                        }
                    case '(':
                        {
                            Function.GoBehindBracket(input, ref index);
                            if (index < 0) throw new ArgumentOutOfRangeException();
                            break;
                        }
                    default: break;
                }
            }
            nextInputs.Add(input);
            if (nextInputs.Count < 2) return null;
            return product;
        }

        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < Length; i++) output += this[i].ToString();
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

        public void Clear()
        {
            for(int i = 0; i < Children.Count; i++)
            {
                Children.RemoveAt(i);
            }
        }
    }
}
