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

        static public Expression Create(string input, Expression backPointer = null)
        {

        }
    }


    public class NullExpression : Expression
    {
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
    }


    public class Number : NullExpression
    {
        public long num;
        public long Num
        {
            get { return num; }
            set { num = value; }
        }

        static public Number Create(string input)
        {
            Number number = new Number();
            if (!long.TryParse(input, out number.num)) number = null;
            return number;
        }

        public override string ToString()
        {
            return Num.ToString();
        }
    }

    public class Variable : NullExpression
    {
        public string Name { get; set; }

        static new public Variable Create(string input)
        {
            Variable variable = new Variable();

        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Funktion : UnitExpression
    {
        public string Name { get; set; }
        public string BracketOpen { get; set; }
        public string BracketClose { get; set; }
    }

    public class Fraction : DoubleExpression
    {
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

    public class Sum : RepetedExpression
    {
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
