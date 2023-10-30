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
            scrollViewer_EquationHistory.Focus();
            /*
            Expression expression;
            StringInfo input = "hallo+-";
            expression = Expression.Create(input);
            input = InputManagementSystem.ConvertToStringInfo(input.ToString(), out _);
            expression = Expression.Create(input);
            expression = InputManagementSystem.GetEmptyExpression(expression);
            while (true) { }
            /**/
        }

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
                EquationHistory.Clear();
                EquationHistory.DockAt(expression.CreateUI(24), Dock.Top);
            }
            else key = e.Key;
        }
        private void ScrollViewer_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (InputManagementSystem.AddInput(key, e.Text[0])) { }
            StringInfo strInf = InputManagementSystem.ConvertToStringInfo(InputManagementSystem.strCur, out _);
            expression = Expression.Create(strInf);
            expression.CreateUI(24);
            EquationHistory.Clear();
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

        static public StringInfo operator +(CharInfo ci0, CharInfo ci1)
        {
            return new StringInfo(ci0) + ci1;
        }
        static public StringInfo operator *(CharInfo ci, int factor)
        {
            if (factor < 0) throw new ArgumentException();
            return new StringInfo(ci) * factor;
        }
        static public StringInfo operator *(int factor, CharInfo ci)
        {
            if (factor < 0) throw new ArgumentException();
            return factor * new StringInfo(ci);
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

        public new string ToString()
        {
            return C + " (" + Design.ToString() + ")";
        }
    }

    public class StringInfo : List<CharInfo>
    {
        public StringInfo() : base(0) { }
        public StringInfo(int length) : base(length) { }
        public StringInfo(CharInfo ci) : base(1)
        {
            Add(ci);
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

        public StringInfo Substring(int startIndex)
        {
            return new StringInfo(GetRange(startIndex, Count - startIndex));
        }
        public StringInfo Substring(int startIndex, int length)
        {
            return new StringInfo(GetRange(startIndex, length));
        }

        public StringInfo Remove(int startIndex)
        {
            StringInfo output = new StringInfo(this);
            output.RemoveRange(startIndex, Count - startIndex);
            return output;
        }
        public StringInfo Remove(int startIndex, int count)
        {
            StringInfo output = new StringInfo(this);
            output.RemoveRange(startIndex, count);
            return output;
        }

        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < Count; i++) output += this[i].C;
            return output;
        }
    }

    public class StringCursor
    {
        public string String { get; private set; }
        public int Length
        {
            get { return String.Length; }
        }
        public int Cursor { get; private set; }
        public char? CurentChar
        {
            get
            {
                if (Cursor == String.Length) return null;
                else return String[Cursor];
            }
        }

        public char this[int i]
        {
            get
            {
                return String[i];
            }
        }

        public StringCursor()
        {
            String = "";
        }
        public StringCursor(string str)
        {
            String = str == null ? "" : str;
        }
        static public implicit operator StringCursor(string str)
        {
            return new StringCursor(str);
        }
        public StringCursor(string str, int cursor)
        {
            String = str == null ? "" : str;
            Cursor = cursor < 0 ? 0 : cursor < String.Length ? cursor : String.Length;
        }

        static public string operator +(StringCursor sc0, StringCursor sc1)
        {
            return sc0.String + sc1.String;
        }
        static public string operator +(StringCursor sc, string str)
        {
            return sc.String + str;
        }
        static public string operator +(string str, StringCursor sc)
        {
            return str + sc.String;
        }

        public void GoLeft()
        {
            if (0 < Cursor) Cursor--;
        }
        public void GoLeft(int count)
        {
            if (count < Cursor) Cursor -= count;
            else Cursor = 0;
        }
        public void GoRight()
        {
            if (Cursor < String.Length) Cursor++;
        }
        public void GoRight(int count)
        {
            Cursor += count;
            if (Length < Cursor) Cursor = Length;
        }
        public void GoStart()
        {
            Cursor = 0;
        }
        public void GoEnd()
        {
            Cursor = String.Length;
        }
        public void GoTo(int pos)
        {
            if (pos < 0 || Length < pos) throw new ArgumentOutOfRangeException();
            Cursor = pos;
        }

        public void Delete()
        {
            if (Cursor < String.Length) String = String.Remove(Cursor, 1);
        }
        public void Delete(int count)
        {
            if (Cursor + count < String.Length) String = String.Remove(Cursor, count);
            else String = String.Remove(Cursor);
        }
        public void Backspace()
        {
            if (0 < Cursor) String = String.Remove(--Cursor, 1);
        }
        public void Backspace(int count)
        {
            if (count < Cursor)
            {
                Cursor -= count;
                String = String.Remove(Cursor, count);
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
            String = String.Insert(Cursor++, c.ToString());
        }
        public void Write(string str)
        {
            String = String.Insert(Cursor, str);
            Cursor += str.Length;
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

            //adds the brackets
            result = new CharInfo('(', Design.Auto) * bracketsOpen + result + new CharInfo(')', Design.Auto) * bracketCounter;
            cursor += bracketsOpen;

            return result;
        }
        static public void GetSubExpression(Expression expression, ref int pointer, out Expression subExpression, out int index)
        {
            switch (expression)
            {
                case Expression e when e is EmptyExpression:
                    {
                        subExpression = null;
                        index = -1;
                        return;
                    }
                case Expression e when e is Number || e is Variable:
                    {
                        if (pointer < expression.ToString().Length)
                        {
                            subExpression = expression;
                            index = pointer;
                        }
                        else
                        {
                            subExpression = null;
                            index = -1;
                            pointer -= expression.ToString().Length;
                        }
                        return;
                    }
                case Expression e when e is OperationPart:
                    {
                        if (pointer == 0)
                        {
                            subExpression = expression;
                            index = 0;
                            return;
                        }
                        GetSubExpression(((OperationPart)expression).Expression, ref pointer, out subExpression, out index);
                        if (subExpression != null) return;
                        index = -1;
                        return;
                    }
                case Expression e when e is Function:
                    {
                        Function function = (Function)expression;
                        if (pointer <= function.Name.Length)
                        {
                            subExpression = expression;
                            index = pointer;
                            return;
                        }
                        GetSubExpression(function.Expression, ref pointer, out subExpression, out index);
                        if (subExpression != null) return;
                        if (pointer == 0)
                        {
                            subExpression = expression;
                            index = pointer;
                        }
                        else pointer--;
                        return;
                    }
                case Expression e when e is Fraction:
                    {
                        Fraction fraction = (Fraction)expression;
                        GetSubExpression(fraction.ExpressionA, ref pointer, out subExpression, out index);
                        if (subExpression != null) return;
                        if (pointer-- == 0)
                        {
                            subExpression = fraction;
                            index = 0;
                            return;
                        }
                        GetSubExpression(fraction.ExpressionB, ref pointer, out subExpression, out index);
                        if (subExpression != null) return;
                        index = -1;
                        return;
                    }
                case Expression e when e is Sum || e is Product:
                    {
                        RepeatedExpression repeatedExpression = (RepeatedExpression)expression;
                        for (int i = 0; i < repeatedExpression.Length; i++)
                        {
                            GetSubExpression(repeatedExpression[i], ref pointer, out subExpression, out index);
                            if (subExpression != null) return;
                        }
                        subExpression = null;
                        index = -1;
                        return;
                    }
                default: throw new ArgumentException("objekt unknown!");
            }
        }
        static public EmptyExpression GetEmptyExpression(Expression expression)
        {
            switch (true)
            {
                case bool _ when expression is NullExpression:
                    {
                        if (expression is EmptyExpression) return (EmptyExpression)expression;
                        return null;
                    }
                case bool _ when expression is UnitExpression:
                    {
                        return GetEmptyExpression(((UnitExpression)expression).Expression);
                    }
                case bool _ when expression is DoubleExpression:
                    {
                        DoubleExpression doubleExpression = (DoubleExpression)expression;
                        EmptyExpression emptyExpression = GetEmptyExpression(doubleExpression.ExpressionA);
                        if (emptyExpression != null) return emptyExpression;
                        return GetEmptyExpression(doubleExpression.ExpressionB);
                    }
                case bool _ when expression is RepeatedExpression:
                    {
                        RepeatedExpression repeatedExpression = (RepeatedExpression)expression;
                        EmptyExpression emptyExpression;
                        for (int i = 0; i < repeatedExpression.Length; i++)
                        {
                            emptyExpression = GetEmptyExpression(repeatedExpression[i]);
                            if (emptyExpression != null) return emptyExpression;
                        }
                        return null;
                    }
                default: throw new ArgumentException();
            } 
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
        

        public virtual ExtendedDockPanel CreateUI(double fontSize)
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


        public ExtendedDockPanel CreateLabelUI(double fontSize, string name)
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

        public override ExtendedDockPanel CreateUI(double fontSize) 
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
        public override ExtendedDockPanel CreateUI(double fontSize)
        {
            DockPanel = CreateLabelUI(fontSize, Name);
            return DockPanel;
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
                new ExtendedLabel(operationColor, Operation.C), Expression.CreateUI(fontSize)
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
        public CharInfo BracketOpen { get; set; }
        public bool? VisibleClose { get; set; }
        public CharInfo BracketClose { get; set; }

        public override ExtendedDockPanel CreateUI(double fontSize)
        {
            // create the dockpanel
            DockPanel = new ExtendedDockPanel(
                fontSize,
                Dock.Left, 
                new ExtendedLabel(functionNameColor, Name), new ExtendedLabel(BracketOpen.C), Expression.CreateUI(fontSize), new ExtendedLabel(BracketClose.C)
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

        public override ExtendedDockPanel CreateUI(double fontSize)
        {
            Rectangle fractionBar = new Rectangle();

            fractionBar.Height = 2;
            fractionBar.Fill = Brushes.White;

            ExtendedDockPanel TopUi = ExpressionA.CreateUI(fontSize);
            ExtendedDockPanel BottomUi = ExpressionB.CreateUI(fontSize);

            // fraction bar should be bigger than content
            TopUi.SetSideMargin();
            BottomUi.SetSideMargin();

            DockPanel = new ExtendedDockPanel(fontSize, Dock.Top, TopUi, fractionBar, BottomUi);

            // just a little distance before and after the fraction bar
            DockPanel.SetSideMargin();
            needsHeightUpdate.Append(this);

            return DockPanel;
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
}
