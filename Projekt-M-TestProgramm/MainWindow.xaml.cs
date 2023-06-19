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
        }
    }



    public class Expression
    {
        public DockPanel DockPanel { get; set; }
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
                expression = RepetedExpression.Create(input);
                if (expression != null) break;
                throw new Exception();
            }
            expression.BackPointer = backPointer;
            return expression;
        }
    }


    public class NullExpression : Expression
    {
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
                unitExpression = Funktion.Create(input, out nextInput);
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
            string nextInputA, nextInputB;
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

    public class RepetedExpression : Expression
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

        static public RepetedExpression Create(string input)
        {
            RepetedExpression repetedExpression;
            BinArray<string> nextInputs;
            while (true)
            {
                repetedExpression = Sum.Create(input, out nextInputs);
                if (repetedExpression != null) break;
                repetedExpression = Product.Create(input, out nextInputs);
                if (repetedExpression != null) break;
                return null;
            }
            repetedExpression.Expressions = new BinArray<Expression>(nextInputs.Length);
            for (int i = 0; i < nextInputs.Length; i++)
            {
                repetedExpression.Expressions[i] = Expression.Create(input, repetedExpression);
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
            if (!long.TryParse(input, out number.num)) number = null;
            return number;
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
        public char Operation { get; set; }

        static public OperationPart Create(string input, out string nextInput)
        {
            OperationPart operationPart = new OperationPart();
            nextInput = null;
            if (input.Length == 0) return null;
            int index = 0;
            switch (input[index++])
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
                                case '(': Funktion.GoOutOfBracket(input, ref index); break;
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
                                case '(': Funktion.GoOutOfBracket(input, ref index); break;
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
    }

    public class Funktion : UnitExpression
    {
        public string Name { get; set; }
        public bool? VisibleOpen { get; set; }
        public char BracketOpen { get; set; }
        public bool? VisibleClose { get; set; }
        public char BracketClose { get; set; }

        static public Funktion Create(string input, out string nextInput)
        {
            Funktion funktion = new Funktion();
            nextInput = null;
            if (input.Length < 2) return null;
            int index = 0;
            switch (input[index++])
            {
                case '(':
                    {
                        GoOutOfBracket(input, ref index);
                        if (index < 0) throw new ArgumentOutOfRangeException();
                        if (index < input.Length) return null;
                        funktion.Name = "";
                        break;
                    }
                case char c when Variable.IsLetter(c):
                    {
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
                            Funktion.GoOutOfBracket(input, ref index);
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
                            if (index != input.Length && input[index] == '(')
                            {
                                index++;
                                Funktion.GoOutOfBracket(input, ref index);
                            }
                            break;
                        }
                    default: return null;
                }
                if (index == input.Length) return null;
                switch (input[index])
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
            if (index == input.Length) return null;
            nextInputB = input.Substring(index);
            while (true)
            {
                switch (input[index++])
                {
                    case '(':
                        {
                            Funktion.GoOutOfBracket(input, ref index);
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
                            if (index != input.Length && input[index] == '(')
                            {
                                index++;
                                Funktion.GoOutOfBracket(input, ref index);
                            }
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
        static public Power Create(string input, out string nextInputA, out string nextInputB)
        {
            Power power = new Power();
            nextInputA = null;
            nextInputB = null;
            int index = 0;
            switch (input[index++])
            {
                case '(':
                    {
                        Funktion.GoOutOfBracket(input, ref index);
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
                        if (index!=input.Length&&input[index]=='(')
                        {
                            index++;
                            Funktion.GoOutOfBracket(input, ref index);
                        }
                        break;
                    }
            }
            nextInputA = input.Remove(index);
            if (index == input.Length || input[index++] != '^') return null;
            nextInputB = input.Substring(index);
            while (true)
            {
                switch (input[index++])
                {
                    case '(':
                        {
                            Funktion.GoOutOfBracket(input, ref index);
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
                            if (index != input.Length && input[index] == '(')
                            {
                                index++;
                                Funktion.GoOutOfBracket(input, ref index);
                            }
                            break;
                        }
                }
                if (index != input.Length)
                {
                    if (input[index++] == '^') continue;
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

    public class Sum : RepetedExpression
    {
        static public Sum Create(string input, out BinArray<string> nextInputs)
        {
            Sum sum = new Sum();
            nextInputs = new BinArray<string>(0);
            int index = 0;
            if (input.Length == 0) return null;
            if (input[index] == '+' || input[index] == '-') index++;
            while (index < input.Length)
            {
                switch (input[index++])
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
                            Funktion.GoOutOfBracket(input, ref index);
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

    public class Product : RepetedExpression
    {
        static public Product Create(string input, out BinArray<string> nextInputs)
        {
            Product product = new Product();
            nextInputs = new BinArray<string>(0);
            int index = 0;
            while (index < input.Length)
            {
                switch (input[index++])
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
                            Funktion.GoOutOfBracket(input, ref index);
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
}
