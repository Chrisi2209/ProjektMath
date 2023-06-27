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
            //InitializeComponent();
            //KeyDown += ScrollViewer_KeyDown;

            /*
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
            */

            //Expression summe = Expression.Create(new StringInfo());
            //summe.CreateUI();
            //EquationHistory.DockAt(summe.DockPanel, Dock.Top);

            // TestLabel.Content = summe[3].DockPanel.Width;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Update heights on fractions so that the fraction bars are centered
            //Fraction.updateHeightsAndLineWidths();
        }


        /*
        private void ScrollViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(TestLabel.Content) == 1)
            {
                TestLabel.Content = 0;
            }
            else
            {
                TestLabel.Content = 1;
            }


            if (e.Key == Key.Oem5)
            {
                AddInput('^');
                e.Handled = true;
            }
            else if (e.Key == Key.Left)
            {
                TestLabel.Content = 100000000;
                AddInput(Convert.ToChar(1));
            }
            else if (e.Key == Key.Right)
            {
                TestLabel.Content = 200000000;
                AddInput(Convert.ToChar(2));
            }
        }

        private void ScrollViewer_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char input = e.Text[0];
            // 59 = ; | 63 = ? | 64 = @ | 92 = \ | 96 = ' | 127 = DEL
            if (input == '!' || input == 8 || (input >= 40 && input != 59 && input != 63 && input != 64 && input != 92 && input != 96 && input < 127))
            {
                AddInput(e.Text[0]);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TestLabel.Content = "abcdefg";
        }
        */



        StringInfo input = new StringInfo();
        int cursorPosition;

        public bool AddInput_Simon(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    {
                        if (cursorPosition == 0) break;

                        //input[cp0]
                        if (cursorPosition < input.Length && input[cursorPosition].C == '(' && input[cursorPosition].Design == Design.Ghost)
                        {
                            int index = cursorPosition + 1;
                            Function.GoToBracketEnd(input, ref index);
                            input[index].Design = Design.Hidden;
                            input[cursorPosition].Design = Design.Hidden;
                        }

                        //input[cp0 - 1]
                        if (input[--cursorPosition].C == '(' && input[cursorPosition].Design == Design.AutoBracket)
                        {
                            int index = cursorPosition + 1;
                            Function.GoToBracketEnd(input, ref index);
                            input[cursorPosition].Design = Design.Visible;
                        }

                        //input[cp0 - 2]
                        if (0 < cursorPosition && input[--cursorPosition].Design == Design.Hidden && input[cursorPosition].C == ')')
                        {
                            input[cursorPosition].Design = Design.Ghost;
                            int index = cursorPosition - 2;
                            Function.GoToBracketStart(input, ref index);
                            input[index].Design = Design.Ghost;
                        }

                        cursorPosition++;

                        break;
                    }
                case Key.Right:
                    {
                        if (cursorPosition == input.Length) break;

                        //input[cp0 - 1]
                        if (0 < cursorPosition-- && input[cursorPosition].C == ')')
                        {
                            int index = cursorPosition - 1;
                            Function.GoToBracketStart(input, ref index);
                            switch (input[cursorPosition].Design)
                            {
                                case Design.Visible: input[index].Design = Design.Visible; break;
                                case Design.Ghost:
                                    {
                                        input[index].Design = Design.Hidden;
                                        input[cursorPosition].Design = Design.Hidden;
                                        break;
                                    }
                                default: throw new ArgumentException();
                            }
                        }

                        //input[cp0]
                        if (input[++cursorPosition].C == ')' && input[cursorPosition].Design == Design.AutoBracket) input[cursorPosition].Design = Design.Visible;

                        //input[cp0 + 1]
                        if (input[++cursorPosition].Design == Design.Hidden)
                        {
                            switch (input[cursorPosition].C)
                            {
                                case '(':
                                    {
                                        input[cursorPosition].Design = Design.Ghost;
                                        int index = cursorPosition + 1;
                                        Function.GoToBracketEnd(input, ref index);
                                        input[index].Design = Design.Ghost;
                                        break;
                                    }
                                case ')':
                                    {
                                        input[cursorPosition].Design = Design.Ghost;
                                        int index = cursorPosition - 1;
                                        Function.GoToBracketStart(input, ref index);
                                        input[index].Design = Design.Ghost;
                                        break;
                                    }
                            }
                        }

                        break;
                    }
                case Key.Up:
                case Key.Down:
                    {
                        //input[cp0]
                        FarFrom(cursorPosition);

                        //input[cp0 - 1]
                        if (0 < cursorPosition) FarFrom(cursorPosition - 1);

                        switch (e.Key)
                        {
                            case Key.Up:
                                {
                                    cursorPosition = 0;
                                    if (input[cursorPosition].C == '(')
                                    {
                                        switch (input[cursorPosition].Design)
                                        {
                                            case Design.Hidden:
                                                {
                                                    input[cursorPosition].Design = Design.Ghost;
                                                    int index = cursorPosition + 1;
                                                    Function.GoToBracketEnd(input, ref index);
                                                    input[index].Design = Design.Ghost;
                                                    break;
                                                }
                                            case Design.AutoBracket:
                                                {
                                                    int index = cursorPosition;
                                                    do input[index].Design = Design.Visible; while (input[++index].Design == Design.AutoBracket);
                                                    break;
                                                }
                                        }
                                    }
                                    break;
                                }
                            case Key.Down:
                                {
                                    cursorPosition = input.Length - 1;
                                    if (input[cursorPosition].C == ')')
                                    {
                                        switch (input[cursorPosition].Design)
                                        {
                                            case Design.Hidden:
                                                {
                                                    input[cursorPosition].Design = Design.Ghost;
                                                    int index = cursorPosition - 1;
                                                    Function.GoToBracketStart(input, ref index);
                                                    input[index].Design = Design.Ghost;
                                                    break;
                                                }
                                            case Design.AutoBracket:
                                                {
                                                    int index = cursorPosition;
                                                    do input[index].Design = Design.Visible; while (input[--index].Design == Design.AutoBracket);
                                                    break;
                                                }
                                        }
                                    }
                                    cursorPosition++;
                                    break;
                                }
                        }

                        break;
                    }

                case Key.Back:
                    {
                        if (cursorPosition == 0) break;

                        switch (input[--cursorPosition].C)
                        {
                            case '(':
                            case ')':

                            case char c when OperationPart.IsOperation(c):
                                {
                                    input.Remove1(cursorPosition);

                                    break;
                                }

                            case char c when Number.IsDigit(c):
                                {
                                    break;
                                }

                            case char c when Variable.IsLetter(c):
                                {
                                    break;
                                }

                            default: throw new ArgumentException();
                        }

                        break;
                    }
                case Key.Delete:
                    {
                        
                        break;
                    }
                case Key.Enter: return true;

                case Key.Oem5:
                    {
                        e.Handled = true;
                        goto default;
                    }
                default:
                    {

                        break;
                    }
            }
            return false;
        }
        public void FarFrom(int cursorIndex)
        {
            switch (input[cursorIndex].C)
            {
                case '(':
                case ')':
                    {
                        if (input[cursorIndex].Design != Design.Ghost) break;
                        int index = cursorIndex;
                        if (input[cursorIndex].C == '(')
                        {
                            index++;
                            Function.GoToBracketEnd(input, ref index);
                        }
                        else
                        {
                            index--;
                            Function.GoToBracketStart(input, ref index);
                        }
                        input[cursorIndex].Design = Design.Hidden;
                        input[index].Design = Design.Hidden;
                        break;
                    }
            }
        }

        /*
        private void AddInput(char newChar)
        {
             // int values 1 and 2 are reserved for Left and Right arrows
             
            if (newChar == 1)
            {
                TestLabel.Content = "1aaaa";
                CursorPosition--;
            }
            else if (newChar == 2)
            {
                TestLabel.Content = "1aaaa";
                CursorPosition++;
            }
            else if (newChar == 8)  // Backspace
            {
                if (CursorPosition != 0)  // Exception is raised if not checked
                {
                    inputString = inputString.Remove(--CursorPosition);
                }
            }
            else
            {
                inputString = inputString.Insert(CursorPosition++, "" + newChar);  // "" + newChar to convert into string
            }
            EquationHistory.Clear();

            EquationHistory.DockAt(Expression.Create(inputString).CreateUI(), Dock.Top);
        }
        */
    }



    public enum Design
    {
        Visible,
        Hidden,
        Ghost,
        AutoBracket
    }

    public class CharInfo
    {
        public char C { get; set; }
        public Design Design { get; set; }

        public CharInfo(char c, Design design = Design.Visible)
        {
            C = c;
            Design = design;
        }
        static public implicit operator CharInfo(char c)
        {
            return new CharInfo(c);
        }

        static public bool operator <(CharInfo ci0, CharInfo ci1)
        {
            return ci0.C < ci1.C;
        }
        static public bool operator <=(CharInfo ci0, CharInfo ci1)
        {
            return ci0.C <= ci1.C;
        }
        static public bool operator >=(CharInfo ci0, CharInfo ci1)
        {
            return ci0.C >= ci1.C;
        }
        static public bool operator >(CharInfo ci0, CharInfo ci1)
        {
            return ci0.C > ci1.C;
        }

        public bool EqualTo(CharInfo ci)
        {
            return C == ci.C && Design == ci.Design;
        }
    }

    public class StringInfo : BinArray<CharInfo>
    {
        public StringInfo() : base(0) { }
        public StringInfo(string input) : base(input.Length)
        {
            for (int i = 0; i < Length; i++) this[i] = new CharInfo(input[i]);
        }
        static public implicit operator StringInfo(string input)
        {
            return new StringInfo(input);
        }
        public StringInfo(StringInfo stringInfo) : base(stringInfo.Length)
        {
            for (int i = 0; i < Length; i++) this[i] = stringInfo[i];
        }
        public StringInfo(int length) : base(length) { }

        static public StringInfo operator +(StringInfo stringInfo, CharInfo ci)
        {
            StringInfo output = new StringInfo(stringInfo);
            output.Append(ci);
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
            output.Append(stringInfo1);
            return output;
        }
        static public StringInfo operator *(StringInfo stringInfo, uint factor)
        {
            StringInfo output = new StringInfo();
            for (; 0 < factor; factor--) output.Append(stringInfo);
            return output;
        }

        public StringInfo Substring(int startIndex)
        {
            StringInfo output = new StringInfo(Length - startIndex);
            for (int i = startIndex; i < Length; i++) output[i] = this[i];
            return output;
        }
        public StringInfo Substring(int startIndex, int length)
        {
            StringInfo output = new StringInfo(length);
            for (int i = startIndex; i < length; i++) output[i] = this[i];
            return output;
        }

        public new StringInfo Remove1(int index)
        {
            StringInfo output = new StringInfo(this);
            output.BaseRemove1(index);
            return output;
        }
        public void BaseRemove1(int index)
        {
            base.Remove1(index);
        }
        public new StringInfo Remove(int startIndex)
        {
            StringInfo output = new StringInfo(this);
            output.BaseRemove(startIndex);
            return output;
        }
        public void BaseRemove(int startIndex)
        {
            base.Remove(startIndex);
        }
        public new StringInfo Remove(int startIndex, int count)
        {
            StringInfo output = new StringInfo(this);
            output.BaseRemove(startIndex, count);
            return output;
        }
        public void BaseRemove(int startIndex, int count)
        {
            base.Remove(startIndex, count);
        }

        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < Length; i++) output += this[i].C;
            return output;
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
                newChild = this[i].CreateUI();
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
            DockPanel = new ExtendedDockPanel(Dock.Left, new ExtendedLabel(name));

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

        static public RepeatedExpression Create(StringInfo input)
        {
            RepeatedExpression repeatedExpression;
            BinArray<StringInfo> nextInputs;

            while (true)
            {
                repeatedExpression = Sum.Create(input, out nextInputs);
                if (repeatedExpression != null) break;
                repeatedExpression = Product.Create(input, out nextInputs);
                if (repeatedExpression != null) break;
                return null;
            }
            repeatedExpression.Expressions = new BinArray<Expression>(nextInputs.Length);
            for (int i = 0; i < nextInputs.Length; i++)
            {
                repeatedExpression.Expressions[i] = Expression.Create(nextInputs[i], repeatedExpression);
            }
            return repeatedExpression;
        }
    }


    public class EmptyExpression : NullExpression
    {
        static public new EmptyExpression Create(StringInfo input)
        {
            if (input.Length == 0) return new EmptyExpression();
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

        static public new Number Create(StringInfo input)
        {
            Number number = new Number();
            if (input.Length == 0 || input[0] < '0' || '9' < input[0]) return null;
            // using data field for number.num because property can't be an out parameter
            if (!long.TryParse(input.ToString(), out number.num)) number = null;
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

        static public void GoBehindNumber(StringInfo input, ref int index)
        {
            while (index < input.Length && IsDigit(input[index].C)) index++;
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
            Variable variable = new Variable();
            for (int i = 0; i < input.Length; i++)
            {
                if ((input[i] < 'a' || 'z' < input[i]) && (input[i] < 'A' || 'Z' < input[i])) return null;
            }
            variable.Name = input.ToString();
            return variable;
        }

        static public bool IsLetter(char c)
        {
            return 'a' <= c && c <= 'z' || 'A' <= c && c <= 'Z';
        }

        static public void GoBehindVariable(StringInfo input, ref int index)
        {
            while (index < input.Length && IsLetter(input[index].C)) index++;
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

        static public OperationPart Create(StringInfo input, out StringInfo nextInput)
        {
            OperationPart operationPart = new OperationPart();
            nextInput = null;
            if (input.Length == 0) return null;
            int index = 0;
            switch (input[index++].C)
            {
                case '+':
                case '-':
                    {
                        while (index < input.Length)
                        {
                            switch (input[index++].C)
                            {
                                case '+':
                                case '-': return null;
                                case '*':
                                case '/':
                                case '^': break;
                                case '(': Function.GoToBracketEnd(input, ref index); break;
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
                            switch (input[index++].C)
                            {
                                case '+':
                                case '-':
                                case '*':
                                case '/': return null;
                                case '^': break;
                                case '(': Function.GoToBracketEnd(input, ref index); break;
                                case char c when Number.IsDigit(c): Number.GoBehindNumber(input, ref index); break;
                                case char c when Variable.IsLetter(c): Variable.GoBehindVariable(input, ref index); break;
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

        public override ExtendedDockPanel CreateUI()
        {
            // create the dockpanel
            DockPanel = new ExtendedDockPanel(
                Dock.Left,
                new ExtendedLabel(operationColor, Operation.C), Expression.CreateUI()
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
        public CharInfo BracketOpen { get; set; }
        public CharInfo BracketClose { get; set; }

        static public Function Create(StringInfo input, out StringInfo nextInput)
        {
            Function funktion = new Function();
            nextInput = null;
            if (input.Length < 2) return null;
            int index = 0;
            switch (input[index++].C)
            {
                case '(':
                    {
                        GoToBracketEnd(input, ref index);
                        if (index < 0) throw new ArgumentOutOfRangeException();
                        if (++index < input.Length) return null;
                        funktion.Name = "";
                        break;
                    }
                case char c when Variable.IsLetter(c):
                    {
                        Variable.GoBehindVariable(input, ref index);
                        if (index == input.Length || input[index++].C != '(') return null;
                        funktion.Name = input.Remove(index - 1).ToString();
                        GoToBracketEnd(input, ref index);
                        if (++index < input.Length) return null;
                        break;
                    }
                default: return null;
            }
            int bracketStart = funktion.Name.Length;
            int bracketEnd = input.Length - funktion.Name.Length - 1;
            funktion.BracketOpen = input[bracketStart++];
            funktion.BracketClose = input[bracketEnd--];
            nextInput = input.Substring(bracketStart, bracketEnd);
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
        static public void GoToBracketEnd(StringInfo input, ref int index, int bracketCounter = 1)
        {
            while (bracketCounter != 0 && index < input.Length)
            {
                switch (input[index++].C)
                {
                    case '(': bracketCounter++; break;
                    case ')': bracketCounter--; break;
                }
            }
            if (bracketCounter != 0) index = -1;
            else index--;
            return;
        }
        static public void GoToBracketStart(StringInfo input, ref int index, int bracketCounter = -1)
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
    }

    public class Fraction : DoubleExpression
    {
        static public BinArray<Fraction> needsHeightUpdate = new BinArray<Fraction>(2);

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
                    case '(':
                        {
                            Function.GoToBracketEnd(input, ref index);
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
                            if (index != input.Length && input[index].C == '(')
                            {
                                index++;
                                Function.GoToBracketEnd(input, ref index);
                            }
                            break;
                        }
                    default: return null;
                }
                if (++index == input.Length) return null;
                switch (input[index].C)
                {
                    case '/': break;
                    case '^':
                        {
                            index++;
                            continue;
                        }
                    default: return null;
                }
                break;
            }
            nextInputA = input.Remove(index++);
            nextInputB = input.Substring(index);
            if (index == input.Length) return fraction;
            while (true)
            {
                switch (input[index++].C)
                {
                    case '(':
                        {
                            Function.GoToBracketEnd(input, ref index);
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
                            if (index != input.Length && input[index].C == '(')
                            {
                                index++;
                                Function.GoToBracketEnd(input, ref index);
                            }
                            break;
                        }
                    default: return null;
                }
                if (++index == input.Length) break;
                if (input[index++].C == '^') continue;
                return null;
            }
            return fraction;
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
            return "(" + ExpressionA.ToString() + "/" + ExpressionB.ToString() + ")";
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
                case '(':
                    {
                        Function.GoToBracketEnd(input, ref index);
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
                        if (index != input.Length && input[index].C == '(')
                        {
                            index++;
                            Function.GoToBracketEnd(input, ref index);
                        }
                        break;
                    }
            }
            nextInputA = input.Remove(++index);
            if (index == input.Length || input[index++].C != '^') return null;
            nextInputB = input.Substring(index);
            while (true)
            {
                switch (input[index++].C)
                {
                    case '(':
                        {
                            Function.GoToBracketEnd(input, ref index);
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
                            if (index != input.Length && input[index].C == '(')
                            {
                                index++;
                                Function.GoToBracketEnd(input, ref index);
                            }
                            break;
                        }
                }
                if (++index != input.Length)
                {
                    if (input[index++].C == '^') continue;
                    return null;
                }
                break;
            }
            return power;
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
        static public Sum Create(StringInfo input, out BinArray<StringInfo> nextInputs)
        {
            Sum sum = new Sum();
            nextInputs = new BinArray<StringInfo>(0);
            int index = 0;
            if (input.Length == 0) return null;
            if (input[index].C == '+' || input[index].C == '-') index++;
            while (index < input.Length)
            {
                switch (input[index++].C)
                {
                    case '+':
                    case '-':
                        {
                            nextInputs.Append(input.Remove(--index));
                            input = input.Substring(index);
                            index = 1;
                            break;
                        }
                    case '(':
                        {
                            Function.GoToBracketEnd(input, ref index);
                            if (index < 0) throw new ArgumentOutOfRangeException();
                            break;
                        }
                    default: break;
                }
            }
            nextInputs.Append(input);
            if (nextInputs.Length < 2) return null;
            return sum;
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
        static public Product Create(StringInfo input, out BinArray<StringInfo> nextInputs)
        {
            Product product = new Product();
            nextInputs = new BinArray<StringInfo>(0);
            int index = 0;
            while (index < input.Length)
            {
                switch (input[index++].C)
                {
                    case '+':
                    case '-': return null;
                    case '*':
                        {
                            nextInputs.Append(input.Remove(--index));
                            input = input.Substring(index);
                            index = 1;
                            break;
                        }
                    case '(':
                        {
                            Function.GoToBracketEnd(input, ref index);
                            if (index < 0) throw new ArgumentOutOfRangeException();
                            break;
                        }
                    default: break;
                }
            }
            nextInputs.Append(input);
            if (nextInputs.Length < 2) return null;
            return product;
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

    public class ExtendedDockPanel : DockPanel
    {
        public ExtendedDockPanel(Dock dockDirection, params UIElement[] elements) : base()
        {
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
            for (int i = 0; i < Children.Count; i++)
            {
                Children.RemoveAt(i);
            }
        }
    }
}
