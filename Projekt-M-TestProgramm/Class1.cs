using System;
using System.Collections;

namespace Simon
{
    public class Bruch
    {
        public long Zähler { get; private set; }
        public long Nenner { get; private set; }

        public Bruch(long zahl)
        {
            Zähler = zahl;
            Nenner = 1;
        }
        static public implicit operator Bruch(long zahl)
        {
            return new Bruch(zahl);
        }
        public Bruch(long zähler, long nenner)
        {
            Zähler = zähler;
            Nenner = nenner;
        }

        public void Kürzen()
        {
            long gcd = Gcd(Zähler, Nenner);
            if (Nenner < 0 ^ gcd < 0) gcd *= -1;
            Zähler /= gcd;
            Nenner /= gcd;
        }
        static public long Gcd(long a, long b)
        {
            long c;
            while (b != 0)
            {
                c = a;
                a = b;
                b = c - c / b * b;
            }
            return a;
        }
        static public long Scm(long a, long b)
        {
            return a / Gcd(a, b) * b;
        }

        static public Bruch Pos(Bruch bruch)
        {
            return new Bruch(bruch.Zähler, bruch.Nenner);
        }
        static public Bruch Neg(Bruch bruch)
        {
            return new Bruch(-bruch.Zähler, bruch.Nenner);
        }
        static public Bruch Inc(Bruch bruch)
        {
            return new Bruch(bruch.Zähler + bruch.Nenner, bruch.Nenner);
        }
        static public Bruch Dec(Bruch bruch)
        {
            return new Bruch(bruch.Zähler - bruch.Nenner, bruch.Nenner);
        }
        static public Bruch Add(Bruch bruch0, Bruch bruch1)
        {
            return new Bruch(bruch0.Zähler * bruch1.Nenner + bruch0.Nenner * bruch1.Zähler, bruch0.Nenner * bruch1.Nenner);
        }
        static public Bruch Sub(Bruch bruch0, Bruch bruch1)
        {
            return new Bruch(bruch0.Zähler * bruch1.Nenner - bruch0.Nenner * bruch1.Zähler, bruch0.Nenner * bruch1.Nenner);
        }
        static public Bruch Mult(Bruch bruch0, Bruch bruch1)
        {
            return new Bruch(bruch0.Zähler * bruch1.Zähler, bruch0.Nenner * bruch1.Nenner);
        }
        static public Bruch Div(Bruch bruch0, Bruch bruch1)
        {
            return new Bruch(bruch0.Zähler * bruch1.Nenner, bruch0.Nenner * bruch1.Zähler);
        }
        static public Bruch Kehrwert(Bruch bruch)
        {
            return new Bruch(bruch.Nenner, bruch.Zähler);
        }
        static public long Pow(long basis, int exponennte)
        {
            if (exponennte < 0) throw new Exception("Negative Exponennte");
            string exponennteString = Convert.ToString(exponennte, 2);
            long ergebnis = 1;
            for (int i = 0; i < exponennteString.Length; i++)
            {
                ergebnis *= ergebnis;
                if (exponennteString[i] == '1') ergebnis *= basis;
            }
            return ergebnis;
        }
        static public Bruch Pow(Bruch bruch, int exponennte)
        {
            Bruch ergebnis;
            if (exponennte < 0)
            {
                exponennte *= -1;
                ergebnis = Kehrwert(bruch);
            }
            else ergebnis = Pos(bruch);
            ergebnis.Zähler = Pow(ergebnis.Zähler, exponennte);
            ergebnis.Nenner = Pow(ergebnis.Nenner, exponennte);
            return ergebnis;
        }

        static public Bruch operator ++(Bruch bruch)
        {
            return Inc(bruch);
        }
        static public Bruch operator +(Bruch bruch)
        {
            return Pos(bruch);
        }
        static public Bruch operator +(Bruch bruch, long zahl)
        {
            return new Bruch(bruch.Zähler + zahl * bruch.Nenner, bruch.Nenner);
        }
        static public Bruch operator +(long zahl, Bruch bruch)
        {
            return new Bruch(zahl * bruch.Nenner + bruch.Zähler, bruch.Nenner);
        }
        static public Bruch operator +(Bruch bruch0, Bruch bruch1)
        {
            Bruch sum = Add(bruch0, bruch1);
            sum.Kürzen();
            return sum;
        }

        static public Bruch operator --(Bruch bruch)
        {
            return Dec(bruch);
        }
        static public Bruch operator -(Bruch bruch)
        {
            return Neg(bruch);
        }
        static public Bruch operator -(Bruch bruch, long zahl)
        {
            return new Bruch(bruch.Zähler - zahl * bruch.Nenner, bruch.Nenner);
        }
        static public Bruch operator -(long zahl, Bruch bruch)
        {
            return new Bruch(zahl * bruch.Nenner - bruch.Zähler, bruch.Nenner);
        }
        static public Bruch operator -(Bruch bruch0, Bruch bruch1)
        {
            Bruch sum = Sub(bruch0, bruch1);
            sum.Kürzen();
            return sum;
        }

        static public Bruch operator *(Bruch bruch, long zahl)
        {
            long gcd = Gcd(bruch.Nenner, zahl);
            return new Bruch(bruch.Zähler * (zahl / gcd), bruch.Nenner / gcd);
        }
        static public Bruch operator *(long zahl, Bruch bruch)
        {
            long gcd = Gcd(zahl, bruch.Nenner);
            return new Bruch((zahl / gcd) * bruch.Zähler, bruch.Nenner / gcd);
        }
        static public Bruch operator *(Bruch bruch0, Bruch bruch1)
        {
            Bruch ergebnis = Mult(bruch0, bruch1);
            ergebnis.Kürzen();
            return ergebnis;
        }

        static public Bruch operator /(Bruch bruch, long zahl)
        {
            long gcd = Gcd(bruch.Zähler, zahl);
            return new Bruch(bruch.Zähler / gcd, bruch.Nenner * (zahl / gcd));
        }
        static public Bruch operator /(long zahl, Bruch bruch)
        {
            long gcd = Gcd(zahl, bruch.Nenner);
            return new Bruch((zahl / gcd) * bruch.Nenner, bruch.Zähler / gcd);
        }
        static public Bruch operator /(Bruch bruch0, Bruch bruch1)
        {
            Bruch ergebnis = Div(bruch0, bruch1);
            ergebnis.Kürzen();
            return ergebnis;
        }

        static public Bruch operator ^(Bruch bruch, int exponennte)
        {
            Bruch ergebnis = Pow(bruch, exponennte);
            if (ergebnis.Nenner < 0)
            {
                ergebnis.Zähler *= -1;
                ergebnis.Nenner *= -1;
            }
            return ergebnis;
        }


        public new string ToString()
        {
            return Zähler + (Nenner == 1 ? "" : "/" + Nenner);
        }
    }

    public class C
    {
        static public readonly C prozent = 0.01;
        static public readonly C promill = 0.001;
        static public readonly C ppm = 0.000001;
        static public readonly C grad = new C(Math.PI / 180);
        static public readonly C pi = new C(Math.PI);
        static public readonly C e = new C(Math.E);
        static public readonly C i = new C(0, 1);

        public double x;
        public double y;

        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    default: throw new Exception("Der Index ist auserhalb des definierten Bereiches!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    default: throw new Exception("Der Index ist auserhalb des definierten Bereiches!");
                }
            }
        }

        public C() { }
        public C(double x)
        {
            this.x = x;
        }
        public static implicit operator C(double x)
        {
            return new C(x);
        }
        public C(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public C(C z)
        {
            x = z.x;
            y = z.y;
        }

        static public C Polar(double abs, double arc)
        {
            return abs * new C(Math.Cos(arc), Math.Sin(arc));
        }

        public void Inc()
        {
            x++;
        }
        public void Dec()
        {
            x--;
        }
        public double Abs2()
        {
            return x * x + y * y;
        }
        public double Abs()
        {
            return Math.Sqrt(Abs2());
        }
        public double Arc()
        {
            return Math.Atan2(y, x);
        }

        static public C Inc(C z)
        {
            return new C(z.x + 1, z.y);
        }
        static public C Dec(C z)
        {
            return new C(z.x - 1, z.y);
        }
        static public bool EQ(C z0, C z1)
        {
            if (z0.x == z1.x && z0.y == z1.y) return true;
            return false;
        }
        static public C Re(C z)
        {
            return new C(z.x);
        }
        static public C Im(C z)
        {
            return new C(z.y);
        }
        static public C Abs2(C z)
        {
            return new C(z.Abs2());
        }
        static public C Abs(C z)
        {
            return new C(z.Abs());
        }
        static public C Arc(C z)
        {
            return new C(z.Arc());
        }
        static public C Conj(C z)
        {
            return new C(z.x, -z.y);
        }


        static public C operator ++(C z)
        {
            if (z == null) return null;
            return new C(z.x + 1, z.y);
        }
        static public C operator +(C z)
        {
            return new C(z);
        }
        static public C operator +(C z, double r)
        {
            if (z == null) return null;
            return new C(z.x + r, z.y);
        }
        static public C operator +(double r, C z)
        {
            if (z == null) return null;
            return new C(r + z.x, z.y);
        }
        static public C operator +(C z0, C z1)
        {
            if (z0 == null || z1 == null) return null;
            return new C(z0.x + z1.x, z0.y + z1.y);
        }

        static public C operator --(C z)
        {
            if (z == null) return null;
            return new C(z.x - 1, z.y);
        }
        static public C operator -(C z)
        {
            if (z == null) return null;
            return new C(-z.x, -z.y);
        }
        static public C operator -(C z, double r)
        {
            if (z == null) return null;
            return new C(z.x - r, z.y);
        }
        static public C operator -(double r, C z)
        {
            return new C(r - z.x, -z.y);
        }
        static public C operator -(C z0, C z1)
        {
            if (z0 == null || z1 == null) return null;
            return new C(z0.x - z1.x, z0.y - z1.y);
        }

        static public C operator *(C z, double r)
        {
            if (z == null) return null;
            return new C(z.x * r, z.y * r);
        }
        static public C operator *(double r, C z)
        {
            if (z == null) return null;
            return new C(r * z.x, r * z.y);
        }
        static public C operator *(C z0, C z1)
        {
            if (z0 == null || z1 == null) return null;
            return new C(
                z0.x * z1.x - z0.y * z1.y,
                z0.x * z1.y + z0.y * z1.x);
        }

        static public C operator /(C z, double r)
        {
            if (z == null) return null;
            return new C(z.x / r, z.y / r);
        }
        static public C operator /(double r, C z)
        {
            if (z == null) return null;
            return r / z.Abs2() * Conj(z);
        }
        static public C operator /(C z0, C z1)
        {
            if (z0 == null || z1 == null) return null;
            return z0 * (1 / z1);
        }

        static public C operator ^(C z, double r)
        {
            return Exp(Ln(z) * r);
        }
        static public C operator ^(double r, C z)
        {
            return Exp(Math.Log(r) * z);
        }
        static public C operator ^(C z0, C z1)
        {
            return Exp(Ln(z0) * z1);
        }


        static public bool GültigeFunktion(string funktionsName)
        {
            switch (funktionsName)
            {
                case "":
                case "inc":
                case "dec":
                case "abs":
                case "arc":

                case "sqar":
                case "cube":
                case "sqrt":
                case "cbrt":
                case "exp":
                case "ln":
                case "lg":
                case "lb":

                case "sin":
                case "cos":
                case "tan":
                case "cot":
                case "sec":
                case "csc":

                case "asin":
                case "arcsin":
                case "acos":
                case "arccos":
                case "atan":
                case "arctan":
                case "acot":
                case "arccot":
                case "asec":
                case "arcsec":
                case "acsc":
                case "arccsc":

                case "sinh":
                case "cosh":
                case "tanh":
                case "coth":
                case "sech":
                case "csch":

                case "asinh":
                case "arcsinh":
                case "acosh":
                case "arccosh":
                case "atanh":
                case "arctanh":
                case "acoth":
                case "arccoth":
                case "asech":
                case "arcsech":
                case "acsch":
                case "arccsch":
                    return true;
                default: return false;
            }
        }
        static public C FunktionBerechnen(string funktionsName, C z)
        {
            switch (funktionsName)
            {
                case "": return z;
                case "inc": return Inc(z);
                case "dec": return Dec(z);
                case "abs": return Abs(z);
                case "arc": return Arc(z);

                case "sqar": return Sqare(z);
                case "cube": return Cube(z);
                case "sqrt": return Sqrt(z);
                case "cbrt": return Cbrt(z);
                case "exp": return Exp(z);
                case "ln": return Ln(z);
                case "lg": return Lg(z);
                case "lb": return Lb(z);

                case "sin": return Sin(z);
                case "cos": return Cos(z);
                case "tan": return Tan(z);
                case "cot": return Cot(z);
                case "sec": return Sec(z);
                case "csc": return Csc(z);

                case "asin":
                case "arcsin": return ArcSin(z);
                case "acos":
                case "arccos": return ArcCos(z);
                case "atan":
                case "arctan": return ArcTan(z);
                case "acot":
                case "arccot": return ArcCot(z);
                case "asec":
                case "arcsec": return ArcSec(z);
                case "acsc":
                case "arccsc": return ArcCsc(z);

                case "sinh": return Sinh(z);
                case "cosh": return Cosh(z);
                case "tanh": return Tanh(z);
                case "coth": return Coth(z);
                case "sech": return Sech(z);
                case "csch": return Csch(z);

                case "asinh":
                case "arcsinh": return ArcSinh(z);
                case "acosh":
                case "arccosh": return ArcCosh(z);
                case "atanh":
                case "arctanh": return ArcTanh(z);
                case "acoth":
                case "arccoth": return ArcCoth(z);
                case "asech":
                case "arcsech": return ArcSech(z);
                case "acsch":
                case "arccsch": return ArcCsch(z);

                default: return null;
            }
        }
        public static C Berechnen(string rechnung)
        {
            string[] ausdruck;
            C[] zahlen;

            //aufteilen
            if (!Normieren(rechnung, out ausdruck, out zahlen)) return null;

            //berechnen
            return Berechnen(ausdruck, zahlen);
        }
        public static bool Normieren(string rechnung, out string[] ausdruck, out C[] zahlen)
        {
            rechnung = rechnung.ToLower();
            int indexRechnung = 0;
            int indexAusdruck = 0;
            int indexZahlen = 0;

            //lehre Zeichenkette
            if (rechnung.Length == 0)
            {
                ausdruck = new string[0];
                zahlen = new C[0];
                return false;
            }
            //klammern vervollständigen
            int klammerZähler = 0;
            for (int i = 0; i < rechnung.Length; i++)
            {
                switch (rechnung[i])
                {
                    case '(': klammerZähler++; break;
                    case ')':
                        {
                            if (klammerZähler == 0)
                            {
                                rechnung = "(" + rechnung;
                                i++;
                            }
                            else klammerZähler--;
                            break;
                        }
                }
            }
            for (int i = 0; i < klammerZähler; i++) rechnung += ")";
            //² --> ^2, ³ --> ^3
            rechnung = rechnung.Replace("²", "^2");
            rechnung = rechnung.Replace("³", "^3");
            //* plazieren
            if (rechnung[0] == '+' || rechnung[0] == '-') rechnung = "0" + rechnung;
            bool? ziffer_buchstabe = null;
            for (int i = 0; i < rechnung.Length; i++)
            {
                switch (rechnung[i])
                {
                    case char c when '0' <= c && c <= '9':
                        {
                            ziffer_buchstabe = true;
                            break;
                        }
                    case '(':
                        {
                            if (ziffer_buchstabe == true) rechnung = rechnung.Insert(i++, "*");
                            ziffer_buchstabe = null;
                            if (rechnung[++i] == '+' || rechnung[i] == '-')
                            {
                                rechnung = rechnung.Insert(i++, "0");
                                ziffer_buchstabe = true;
                            }
                            break;
                        }
                    case ')':
                        {
                            if (++i < rechnung.Length && ('0' <= rechnung[i] && rechnung[i] <= '9' || 'a' <= rechnung[i] && rechnung[i] <= 'z' || rechnung[i] == '('))
                                rechnung = rechnung.Insert(i, "*");
                            ziffer_buchstabe = null;
                            break;
                        }
                    case char c when 'a' <= c && c <= 'z':
                        {
                            if (ziffer_buchstabe == true) rechnung = rechnung.Insert(i++, "*");
                            ziffer_buchstabe = false;
                            break;
                        }
                    default:
                        {
                            ziffer_buchstabe = null;
                            break;
                        }
                }
            }
            //rt --> r, log --> l
            rechnung = rechnung.Replace("*log", "l");
            rechnung = rechnung.Replace("*rt", "r");

            //Konvertierung
            ausdruck = new string[rechnung.Length];
            zahlen = new C[(rechnung.Length + 1) / 2];
            string SymbolFürZahl = "x";
            bool ziffer = true, komma, buchstabe = true, operation = false, klammerAuf = true, klammerZu = false;
            while (indexRechnung < ausdruck.Length)
            {
                switch (rechnung[indexRechnung])
                {
                    //Ziffer
                    case 'p':
                    case 'e':
                    case 'i':
                    case char c when '0' <= c && c <= '9':
                        {
                            if (rechnung[indexRechnung] == 'p')
                            {
                                if (indexRechnung + 1 == rechnung.Length) return false;
                                if (rechnung[indexRechnung + 1] != 'i' || indexRechnung + 2 < rechnung.Length && 'a' <= rechnung[indexRechnung + 2] && rechnung[indexRechnung + 2] <= 'z') goto default;
                                ausdruck[indexAusdruck++] = SymbolFürZahl;
                                zahlen[indexZahlen++] = pi;
                                indexRechnung += 2;
                            }
                            else if (rechnung[indexRechnung] == 'e')
                            {
                                if (indexRechnung + 1 != rechnung.Length && 'a' <= rechnung[indexRechnung + 1] && rechnung[indexRechnung + 1] <= 'z') goto default;
                                ausdruck[indexAusdruck++] = SymbolFürZahl;
                                zahlen[indexZahlen++] = e;
                                indexRechnung++;
                            }
                            else if (rechnung[indexRechnung] == 'i')
                            {
                                if (indexRechnung + 1 != rechnung.Length && 'a' <= rechnung[indexRechnung + 1] && rechnung[indexRechnung + 1] <= 'z') goto default;
                                ausdruck[indexAusdruck++] = SymbolFürZahl;
                                zahlen[indexZahlen++] = i;
                                indexRechnung++;
                            }
                            else
                            {
                                if (!ziffer) return false;
                                komma = true;
                                string zahl = "";
                                while (indexRechnung < rechnung.Length)
                                {
                                    if ('0' <= rechnung[indexRechnung] && rechnung[indexRechnung] <= '9')
                                    {
                                        zahl += rechnung[indexRechnung++];
                                    }
                                    else if (rechnung[indexRechnung] == ',')
                                    {
                                        zahl += rechnung[indexRechnung++];
                                        if (komma == false) return false;
                                        komma = false;
                                    }
                                    else break;
                                }
                                ausdruck[indexAusdruck++] = SymbolFürZahl;
                                zahlen[indexZahlen++] = Convert.ToDouble(zahl);
                            }
                            if (indexRechnung < rechnung.Length)
                            {
                                if (rechnung[indexRechnung] == '%')
                                {
                                    zahlen[indexZahlen - 1] /= 100;
                                    indexRechnung++;
                                }
                                else if (rechnung[indexRechnung] == '°')
                                {
                                    zahlen[indexZahlen - 1] *= grad;
                                    indexRechnung++;
                                }
                            }

                            ziffer = false;
                            buchstabe = false;
                            operation = true;
                            klammerAuf = false;
                            klammerZu = true;
                            break;
                        }

                    //Komma
                    case ',': return false;

                    //Operation
                    case '+':
                    case '-':
                    case '*':
                    case '/':
                    case '^':
                    case 'r':
                    case 'l':
                        {
                            if (indexRechnung + 1 == rechnung.Length) return false;
                            if ((rechnung[indexRechnung] == 'r' || rechnung[indexRechnung] == 'l') && 'a' <= rechnung[indexRechnung + 1] && rechnung[indexRechnung + 1] <= 'z') goto default;

                            if (!operation) return false;
                            ausdruck[indexAusdruck++] = rechnung[indexRechnung++].ToString();

                            ziffer = true;
                            buchstabe = true;
                            operation = false;
                            klammerAuf = true;
                            klammerZu = false;
                            break;
                        }

                    //Klammer auf
                    case '(':
                        {
                            if (!klammerAuf) return false;
                            ausdruck[indexAusdruck++] = rechnung[indexRechnung++].ToString();

                            ziffer = true;
                            buchstabe = true;
                            operation = false;
                            klammerAuf = true;
                            klammerZu = false;
                            break;
                        }

                    //Klammer zu
                    case ')':
                        {
                            if (!klammerZu) return false;
                            ausdruck[indexAusdruck++] = rechnung[indexRechnung++].ToString();

                            ziffer = false;
                            buchstabe = false;
                            operation = true;
                            klammerAuf = false;
                            klammerZu = true;
                            break;
                        }

                    //Buchstabe
                    default:
                        {
                            if (!buchstabe) return false;
                            string funktion = "";
                            bool ende = false;
                            while (indexRechnung < rechnung.Length && !ende)
                            {
                                switch (rechnung[indexRechnung])
                                {
                                    case '(': ende = true; break;
                                    default:
                                        {
                                            funktion += rechnung[indexRechnung++];
                                            break;
                                        }
                                }
                            }
                            if (indexRechnung == rechnung.Length || !GültigeFunktion(funktion)) return false;
                            ausdruck[indexAusdruck++] = funktion + rechnung[indexRechnung++];

                            ziffer = true;
                            buchstabe = true;
                            operation = false;
                            klammerAuf = true;
                            klammerZu = false;
                            break;
                        }
                }
            }
            if (ziffer || buchstabe || !operation || klammerAuf || !klammerZu) return false;
            //resizen
            Array.Resize(ref ausdruck, indexAusdruck);
            Array.Resize(ref zahlen, indexZahlen);
            return true;
        }
        public static C Berechnen(string[] ausdruck, C[] zahlen, int mode = 0)
        {
            if (ausdruck.Length == 1) return zahlen[0];
            string[] rechenzeichen;
            string[][] teilAusdrücke;
            C[][] zahlenKetten;
            switch (mode)
            {
                case 0:
                    {
                        //aufteilen
                        if (!Aufteilen(ausdruck, zahlen, mode, out rechenzeichen, out teilAusdrücke, out zahlenKetten)) return null;

                        //berechnen
                        C summe = Berechnen(teilAusdrücke[0], zahlenKetten[0], mode + 1);
                        for (int i = 1; i < teilAusdrücke.Length; i++)
                        {
                            switch (rechenzeichen[i - 1])
                            {
                                case "+": summe += Berechnen(teilAusdrücke[i], zahlenKetten[i], mode + 1); break;
                                case "-": summe -= Berechnen(teilAusdrücke[i], zahlenKetten[i], mode + 1); break;
                                default: throw new Exception();
                            }
                        }
                        return summe;
                    }
                case 1:
                    {
                        //aufteilen
                        if (!Aufteilen(ausdruck, zahlen, mode, out rechenzeichen, out teilAusdrücke, out zahlenKetten)) return null;

                        //berechnen
                        C produkt = Berechnen(teilAusdrücke[0], zahlenKetten[0], mode + 1);
                        for (int i = 1; i < teilAusdrücke.Length; i++)
                        {
                            switch (rechenzeichen[i - 1])
                            {
                                case "*": produkt *= Berechnen(teilAusdrücke[i], zahlenKetten[i], mode + 1); break;
                                case "/": produkt /= Berechnen(teilAusdrücke[i], zahlenKetten[i], mode + 1); break;
                                default: throw new Exception();
                            }
                        }
                        return produkt;
                    }
                case 2:
                    {
                        //aufteilen
                        if (!Aufteilen(ausdruck, zahlen, mode, out rechenzeichen, out teilAusdrücke, out zahlenKetten)) return null;

                        //berechnen
                        C potenz = Berechnen(teilAusdrücke[teilAusdrücke.Length - 1], zahlenKetten[zahlenKetten.Length - 1], mode + 1);
                        for (int i = teilAusdrücke.Length - 2; 0 <= i; i--)
                        {
                            switch (rechenzeichen[i])
                            {
                                case "^": potenz = Berechnen(teilAusdrücke[i], zahlenKetten[i], mode + 1) ^ potenz; break;
                                case "r": potenz = Rt(potenz, Berechnen(teilAusdrücke[i], zahlenKetten[i], mode + 1)); break;
                                case "l": potenz = Log(Berechnen(teilAusdrücke[i], zahlenKetten[i], mode + 1), potenz); break;
                            }
                        }
                        return potenz;
                    }
                case 3:
                    {
                        string funktion = ausdruck[0].Remove(ausdruck[0].Length - 1);
                        Array.Copy(ausdruck, 1, ausdruck, 0, ausdruck.Length - 2);
                        Array.Resize(ref ausdruck, ausdruck.Length - 2);
                        return FunktionBerechnen(funktion, Berechnen(ausdruck, zahlen));
                    }
                default: return null;
            }
        }
        public static bool Aufteilen(string[] ausdruck, C[] zahlen, int mode, out string[] rechenzeichen, out string[][] teilAusdrücke, out C[][] zahlenKetten)
        {
            //rechenzeichenEbenen berechnen
            string[] möglicheRechenzeichen;
            switch (mode)
            {
                case 0: möglicheRechenzeichen = new string[] { "+", "-" }; break;
                case 1: möglicheRechenzeichen = new string[] { "*", "/" }; break;
                case 2: möglicheRechenzeichen = new string[] { "^", "r", "l" }; break;
                default:
                    {
                        rechenzeichen = null;
                        teilAusdrücke = null;
                        zahlenKetten = null;
                        return false;
                    }
            }

            //aufteilen
            rechenzeichen = new string[ausdruck.Length / 2];
            teilAusdrücke = new string[rechenzeichen.Length + 1][];
            teilAusdrücke[0] = new string[ausdruck.Length];
            zahlenKetten = new C[teilAusdrücke.Length][];
            zahlenKetten[0] = new C[zahlen.Length];
            int indexZahlen = 0;
            int indexRechenzeichen = 0, indexTeilAusdrücke = 0, indexZahlenKetten = 0;
            int subIndexTeilAusdrücke = 0, subIndexZahlenKetten = 0;
            int klammernzähler = 0;
            for (int i = 0; i < ausdruck.Length; i++)
            {
                if (klammernzähler == 0 && Array.IndexOf(möglicheRechenzeichen, ausdruck[i]) != -1)
                {
                    rechenzeichen[indexRechenzeichen++] = ausdruck[i];
                    Array.Resize(ref teilAusdrücke[indexTeilAusdrücke++], subIndexTeilAusdrücke);
                    teilAusdrücke[indexTeilAusdrücke] = new string[ausdruck.Length - i - 1];
                    subIndexTeilAusdrücke = 0;
                    Array.Resize(ref zahlenKetten[indexZahlenKetten++], subIndexZahlenKetten);
                    zahlenKetten[indexZahlenKetten] = new C[zahlen.Length - indexZahlen];
                    subIndexZahlenKetten = 0;
                }
                else
                {
                    teilAusdrücke[indexTeilAusdrücke][subIndexTeilAusdrücke++] = ausdruck[i];
                    if (ausdruck[i][ausdruck[i].Length - 1] == '(') klammernzähler++;
                    else if (ausdruck[i] == ")") klammernzähler--;
                    else if (ausdruck[i] == "x") zahlenKetten[indexZahlenKetten][subIndexZahlenKetten++] = zahlen[indexZahlen++];
                }
            }
            Array.Resize(ref rechenzeichen, indexRechenzeichen);
            Array.Resize(ref teilAusdrücke, indexTeilAusdrücke + 1);
            Array.Resize(ref zahlenKetten, indexZahlenKetten + 1);
            return true;
        }


        static public C Sqare(C z)
        {
            return z * z;
        }
        static public C Cube(C z)
        {
            return z * z * z;
        }
        static public C Sqrt(C z)
        {
            return new C(
                Math.Sqrt((z.Abs() + z.x) / 2),
                Math.Sign(z.y) * Math.Sqrt((z.Abs() - z.x) / 2));
        }
        static public C Cbrt(C z)
        {
            return z ^ 1.0 / 3;
        }
        static public C Rt(C zahl, C exponennte)
        {
            return zahl ^ 1 / exponennte;
        }
        static public C Exp(C z)
        {
            if (z == null) return null;
            return Math.Exp(z.x) * new C(Math.Cos(z.y), Math.Sin(z.y));
        }
        static public C Ln(C z)
        {
            if (z == null) return null;
            return new C(Math.Log(z.Abs2()) / 2, z.Arc());
        }
        static public C Lg(C z)
        {
            return Ln(z) / Math.Log(10);
        }
        static public C Lb(C z)
        {
            return Ln(z) / Math.Log(2);
        }
        static public C Log(C basis, C zahl)
        {
            return Ln(zahl) / Ln(basis);
        }
        static public C nPr(C z, int n)
        {
            if (z == null) return null;
            if (n < 0) return 1 / nPr(z - n, -n);
            if (z.y == 0 && z.x < n) return new C();
            C zCopy = z;
            C zOut = new C(1);
            while (n-- != 0) zOut *= zCopy--;
            return zOut;
        }
        static public C nCr(C z, int n)
        {
            if (z == null) return null;
            if (n < 0 || z.y == 0 && z.x < n) return new C();
            if (n == 0) return new C(1);
            C zCopy = z;
            C zOut = z / n;
            while (--n != 0) zOut *= --zCopy / n;
            return zOut;
        }


        static public C Sin(C z)
        {
            /*
             * sin(z) = (exp(i*z)-exp(-i*z))/(2i)
             * = (exp(i*(x+i*y))-exp(-i*(x+i*y)))/(2i)
             * = (exp(-y+i*x)-exp(y-i*x))/(2i)
             * = ((cosh(y)-sinh(y))(cos(x)+sin(x)i)-(cosh(y)+sinh(y))(cos(x)-sin(x)i))/(2i)
             * = (cos(x)(cosh(y)-sinh(y))+sin(x)(cosh(y)-sinh(y))i-cos(x)(cosh(y)+sinh(y))+sin(x)(cosh(y)+sinh(y))i)/(2i)
             * = (-2cos(x)sinh(y)+2sin(x)cosh(y)i)/(2i)
             * = sin(x)cosh(y) + cos(x)sinh(y)i
             */
            return new C(Math.Sin(z.x) * Math.Cosh(z.y), Math.Cos(z.x) * Math.Sinh(z.y));
        }
        static public C Cos(C z)
        {
            /*
             * cos(z) = (exp(i*z)+exp(-i*z))/2
             * = (exp(i*(x+y*i))+exp(-i*(x+y*i)))/2
             * = (exp(-y+x*i)+exp(y-x*i))/2
             * = ((cosh(y)-sinh(y))(cos(x)+sin(x)i)+(cosh(y)+sinh(y))(cos(x)-sin(x)i))/2
             * = (cos(x)(cosh(y)-sinh(y))+sin(x)(cosh(y)-sinh(y))i+cos(x)(cosh(y)+sinh(y))-sin(x)(cosh(y)+sinh(y))i)/2
             * = (2cos(x)cosh(y)-2sin(x)sinh(y)i)/2
             * = cos(x)cosh(y) - sin(x)sinh(y)i
             */
            return new C(Math.Cos(z.x) * Math.Cosh(z.y), -Math.Sin(z.x) * Math.Sinh(z.y));
        }
        static public C Tan(C z)
        {
            return Sin(z) / Cos(z);
        }
        static public C Cot(C z)
        {
            return Cos(z) / Sin(z);
        }
        static public C Sec(C z)
        {
            return 1 / Cos(z);
        }
        static public C Csc(C z)
        {
            return 1 / Sin(z);
        }

        static public C ArcSin(C z)
        {
            return -i * Ln(Sqrt(1 - Sqare(z)) + z * i);
        }
        static public C ArcCos(C z)
        {
            return -i * Ln(z + Sqrt(1 - Sqare(z)) * i);
        }
        static public C ArcTan(C z)
        {
            return -i * Ln((1 + z * i) / (1 - z * i)) / 2;
        }
        static public C ArcCot(C z)
        {
            return -i * Ln((z + i) / (z - i)) / 2;
        }
        static public C ArcSec(C z)
        {
            return -i * Ln((1 + Sqrt(Sqare(z) - 1) * i) / (1 - Sqrt(Sqare(z) - 1) * i)) / 2;
        }
        static public C ArcCsc(C z)
        {
            return -i * Ln((Sqrt(Sqare(z) - 1) + i) / (Sqrt(Sqare(z) - 1) - i)) / 2;
        }

        static public C Sinh(C z)
        {
            /*
             * sinh(z) = (exp(z)-exp(-z))/2
             * = (exp(x+y*i)-exp(-x-y*i))/2
             * = ((cosh(x)+sinh(x))(cos(y)+sin(y)i)-(cosh(x)-sinh(x))(cos(y)-sin(y)i))/2
             * = (cos(y)(cosh(x)+sinh(x))+sin(y)(cosh(x)+sinh(x))i-cos(y)(cosh(x)-sinh(x))+sin(y)(cosh(x)-sinh(x))i)/2
             * = (2cos(y)sinh(x)+2sin(y)cosh(x)i)/2
             * = cos(y)sinh(x)+sin(y)cosh(x)i
             * = sinh(x)cos(y) + cosh(x)sin(y)i
             */
            return new C(Math.Sinh(z.x) * Math.Cos(z.y), Math.Cosh(z.x) * Math.Sin(z.y));
        }
        static public C Cosh(C z)
        {
            /*
             * cosh(z) = (exp(z)+exp(-z))/2
             * = (exp(x+y*i)+exp(-x-y*i))/2
             * = ((cosh(x)+sinh(x))(cos(y)+sin(y)i)+(cosh(x)-sinh(x))(cos(y)-sin(y)i))/2
             * = (cos(y)(cosh(x)+sinh(x))+sin(y)(cosh(x)+sinh(x))i+cos(y)(cosh(x)-sinh(x))-sin(y)(cosh(x)-sinh(x))i)/2
             * = (2cos(y)cosh(x)+2sin(y)sinh(x)i)/2
             * = cos(y)cosh(x)+sin(y)sinh(x)i
             * = cosh(x)cos(y) + sinh(x)sin(y)i
             */
            return new C(Math.Cosh(z.x) * Math.Cos(z.y), Math.Sinh(z.x) * Math.Sin(z.y));
        }
        static public C Tanh(C z)
        {
            return Sinh(z) / Cosh(z);
        }
        static public C Coth(C z)
        {
            return Cosh(z) / Sinh(z);
        }
        static public C Sech(C z)
        {
            return 1 / Cosh(z);
        }
        static public C Csch(C z)
        {
            return 1 / Sinh(z);
        }

        static public C ArcSinh(C z)
        {
            /*
             * sinh(z) = (exp(z)-exp(-z))/2
             * z = (exp(f)-exp(-f))/2       |*2exp(f)
             * 2z*exp(f) = exp(2f)-1        |+1-2z*exp(f)
             * 1 = exp(2f)-2z*exp(f)
             * exp(2f) -2z*exp(f) = 1
             * exp(f) = sqrt(1+z²)+z
             * f = ln(sqrt(1+z²)+z)
             * f = ln(sqrt(z²+1)+z)
             */
            return Ln(Sqrt(Sqare(z) + 1) + z);
        }
        static public C ArcCosh(C z)
        {
            /*
             * cosh(z) = (exp(z)+exp(-z))/2
             * z = (exp(f)+exp(-f))/2       |*2exp(f)
             * 2z*exp(f) = exp(2f)+1        |-1-2z*exp(f)
             * -1 = exp(2f)-2z*exp(f)
             * exp(2f)-2z*exp(f) = -1
             * exp(f) = sqrt(-1+z²)+z
             * f = ln(sqrt(-1+z²)+z)
             * f = ln(sqrt(z²-1)+z)
             */
            return Ln(Sqrt(Sqare(z) - 1) + z);
        }
        static public C ArcTanh(C z)
        {
            /*
             * tanh(z) = sinh(z)/cosh(z)
             * tanh(z) = (exp(z)-exp(-z))/(exp(z)+exp(-z))
             * z = (exp(f)-exp(-f))/(exp(f)+exp(-f))        |*exp(f)/exp(f)
             * z = (exp(2f)-1)/(exp(2f)+1)                  |*(exp(2f)+1)
             * z*exp(2f)+z = exp(2f)-1                      |-z-exp(2f)
             * (z-1)*exp(2f) = -z-1                         |/(z-1)
             * exp(2f) = -(z+1)/(z-1)                       | ln( )
             * 2f = ln((1+z)/(1-z))                         |/2
             * f = ln((1+z)/(1-z))/2
             */
            return Ln((1 + z) / (1 - z)) / 2;
        }
        static public C ArcCoth(C z)
        {
            /*
             * coth(z) = 1/tanh(z)
             * z = 1/tanh(f)
             * 1/z = tanh(f)
             * f = arctanh(1/z)
             * f = ln((1+1/z)/(1-1/z))/2
             * f = ln((z+1)/(z-1))/2
             */
            return Ln((z + 1) / (z - 1)) / 2;
        }
        static public C ArcSech(C z)
        {
            /*
             * sech(z) = 1/cosh(z)
             * z = 1/cosh(f)
             * 1/z = cosh(f)
             * f = arccosh(1/z)
             * f = ln(sqrt((1/z)²-1)+1/z)
             * f = ln(sqrt((1-z²)/z²)+1/z)
             * f = ln(sqrt(1-z²)/z+1/z)
             * f = ln((sqrt(1-z²)+1)/z)
             */
            return Ln((Sqrt(1 - Sqare(z)) + 1) / z);
        }
        static public C ArcCsch(C z)
        {
            /*
             * sech(z) = 1/sinh(z)
             * z = 1/sinh(f)
             * 1/z = sinh(f)
             * f = arcsinh(1/z)
             * f = ln(sqrt((1/z)²+1)+1/z)
             * f = ln((sqrt(1+z²)+1)/z)
             */
            return Ln((Sqrt(1 + Sqare(z)) + 1) / z);
        }


        static public double Runden(double zahl, int anzahlGültigeStellen)
        {
            if (zahl == 0) return zahl;
            double faktor = Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(zahl))));
            return Math.Round(zahl / faktor, anzahlGültigeStellen) * faktor;
        }
        static public C Runden(C z, int anzhalGültigeStellen)
        {
            if (z == null) return null;
            double abs = z.Abs();
            if (abs == 0) return new C();
            return new C(
                Runden(Math.Round(z.x / abs, anzhalGültigeStellen) * abs, anzhalGültigeStellen),
                Runden(Math.Round(z.y / abs, anzhalGültigeStellen) * abs, anzhalGültigeStellen));
        }
        public void Runden(int anzhalGültigeStellen)
        {
            double abs = Abs();
            if (abs == 0) return;
            x = Runden(Math.Round(x / abs, anzhalGültigeStellen) * abs, anzhalGültigeStellen);
            y = Runden(Math.Round(y / abs, anzhalGültigeStellen) * abs, anzhalGültigeStellen);
        }
        public new string ToString()
        {
            if (double.IsNaN(x) || double.IsNaN(y)) return "1/0";
            string zeichenkette = "";
            if (x != 0) zeichenkette += x;
            if (y != 0)
            {
                if (zeichenkette != "" && 0 < y) zeichenkette += "+";
                switch (y.ToString())
                {
                    case "1": zeichenkette += "i"; break;
                    case "-1": zeichenkette += "-i"; break;
                    default: zeichenkette += y + "*i"; break;
                }
            }
            if (zeichenkette == "") zeichenkette = "0";
            return zeichenkette;
        }
        public string ToString(bool mitKlammern)
        {
            if (mitKlammern)
            {
                switch (y)
                {
                    case 0:
                        {
                            if (0 <= x) mitKlammern = false;
                            break;
                        }
                    case 1:
                        {
                            if (x == 0) mitKlammern = false;
                            break;
                        }
                    default: break;
                }
            }
            string zeichenkette = ToString();
            if (mitKlammern) return "(" + zeichenkette + ")";
            return zeichenkette;
        }
        public string ToString(int anzahlGültigeStellen)
        {
            C zahl = this;
            zahl = Runden(zahl, anzahlGültigeStellen);
            return zahl.ToString();
        }
        public string ToString(bool mitKlammern, int anzahlGültigeStellen)
        {
            C zahl = this;
            zahl = Runden(zahl, anzahlGültigeStellen);
            return zahl.ToString(mitKlammern);
        }
    }

    public class BinArray<T>
    {
        private T[] array;
        private int length;


        /// <summary>
        /// Gibt die Anzahl der Werte im BinArray an.
        /// </summary>
        public int Length
        {
            get
            {
                return length;
            }
            private set
            {
                length = 0 <= value ? value : 0;
            }
        }

        /// <summary>
        /// Gibt die Länge des Arrays an.
        /// </summary>
        public int BinLength
        {
            get
            {
                return array.Length;
            }
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || Length <= index) throw new IndexOutOfRangeException();
                return array[index];
            }
            set
            {
                if (index < 0 || Length <= index) throw new IndexOutOfRangeException();
                array[index] = value;
            }
        }



        /// <summary>
        /// Erstellt ein BinArray mit einem Array der länge 2.
        /// </summary>
        public BinArray()
        {
            array = new T[2];
        }

        /// <summary>
        /// Erstellt ein BinArray mit der länge <paramref name="length"/>.
        /// </summary>
        /// <param name="length">gibt die länge des Arrays an</param>
        public BinArray(int length)
        {
            array = new T[NextBin(length)];
            Length = length;
        }

        /// <summary>
        /// Erstellt ein BinArray mit den Werten vom Array.
        /// </summary>
        /// <param name="array">gibt die Werte im BinArrays an</param>
        public BinArray(params T[] array)
        {
            Length = array.Length;
            this.array = new T[NextBin(Length)];
            Array.Copy(array, this.array, Length);
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < Length; ++i)
                yield return this[i];
        }



        /// <summary>
        /// gibt die kleinste 2erPotzenz die größer als <paramref name="number"/> ist zurück. 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        static public int NextBin(int number)
        {
            if (number <= 0) return 1;
            return 1 << Convert.ToString(number, 2).Length;
        }


        /// <summary>
        /// Fügt den Wert <paramref name="value"/> ans BinArray hinten an. 
        /// </summary>
        /// <param name="value"></param>
        public void Append(T value)
        {
            if (array.Length == Length) Array.Resize(ref array, Length * 2);
            array[Length++] = value;
        }

        /// <summary>
        /// Fügt alle Werte von <paramref name="array"/> ans BinArray hinten an. 
        /// </summary>
        /// <param name="array"></param>
        public void Append(params T[] array)
        {
            Length += array.Length;
            if (this.array.Length < Length) Array.Resize(ref this.array, NextBin(Length));
            Array.Copy(array, 0, this.array, Length - array.Length, array.Length);
        }

        /// <summary>
        /// Fügt alle Werte von <paramref name="binArray"/> ans BinArray hinten an. 
        /// </summary>
        /// <param name="binArray"></param>
        public void Append(BinArray<T> binArray)
        {
            Length += binArray.Length;
            if (array.Length < Length) Array.Resize(ref array, NextBin(Length));
            Array.Copy(binArray.array, 0, array, Length - binArray.Length, binArray.Length);
        }


        /// <summary>
        /// Fügt <paramref name="value"/> an der Stelle <paramref name="index"/> ins BinArray ein.
        /// </summary>
        /// <param name="index">Darf einen Wert von 0 bis 'Anzahl' haben.</param>
        /// <param name="value">BinArray[stelle] == wert</param>
        public void Insert(int index, T value)
        {
            if (index < 0 || Length < index) throw new IndexOutOfRangeException();
            if (Length == array.Length) Array.Resize(ref array, Length * 2);
            Array.Copy(array, index, array, index + 1, Length++ - index);
            array[index] = value;
        }

        /// <summary>
        /// Fügt die Werte von <paramref name="array"/> an der der Stelle <paramref name="index"/> ein. 
        /// </summary>
        /// <param name="index">Kann den Wert von 0 bis 'Anzahl' haben.</param>
        /// <param name="array">BinArray[stelle] == array[0]</param>
        public void Insert(int index, params T[] array)
        {
            Length += array.Length;
            if (this.array.Length < Length) Array.Resize(ref this.array, NextBin(Length));
            Array.Copy(this.array, index, this.array, index + array.Length, Length - array.Length - index);
            Array.Copy(array, 0, this.array, index, array.Length);
        }

        /// <summary>
        /// Fügt die Werte von <paramref name="binArray"/> an der Stelle <paramref name="index"/> ein. 
        /// </summary>
        /// <param name="index">Kann den Wert von 0 bis 'Anzahl' haben.</param>
        /// <param name="binArray">BinArray[stelle] == bArray[0]</param>
        public void Insert(int index, BinArray<T> binArray)
        {
            Length += binArray.Length;
            if (array.Length < Length) Array.Resize(ref array, NextBin(Length));
            Array.Copy(array, index, array, index + binArray.Length, Length - binArray.Length - index);
            Array.Copy(binArray.array, 0, array, index, binArray.Length);
        }


        /// <summary>
        /// Entfernt den ersten Wert. 
        /// </summary>
        public void RemoveFirst()
        {
            Array.Copy(array, 1, array, 0, --Length);
        }

        /// <summary>
        /// Entfernt die ersten <paramref name="length"/> Werte. 
        /// </summary>
        /// <param name="length"></param>
        public void RemoveFirst(int length)
        {
            Length -= length;
            Array.Copy(array, length, array, 0, Length);
        }

        /// <summary>
        /// Entfernt den Wert an der Stelle <paramref name="index"/>.
        /// </summary>
        /// <param name="index">Kann den Wert von 0 bis 'Anzahl'-1 haben.</param>
        public void Remove(int index)
        {
            if (index < 0 || Length <= index) throw new ArgumentOutOfRangeException();
            Array.Copy(array, index + 1, array, index, --Length - index);
        }

        /// <summary>
        /// Entfernt <paramref name="length"/> Werte vom BinArray, begonnen bei <paramref name="index"/>.
        /// </summary>
        /// <param name="index">Kann einen Wert von 0 bis 'Anzahl'-'länge' haben.</param>
        /// <param name="length">Gibt an wie viele Werte vom BinArray entfernt wereden.</param>
        public void Remove(int index, int length)
        {
            if (index < 0 || length < 0 || Length < index + length) throw new ArgumentOutOfRangeException();
            Length -= length;
            Array.Copy(array, index + length, array, index, Length - index);
        }

        /// <summary>
        /// Entfernt das letzte Ellement.
        /// </summary>
        public void RemoveLast()
        {
            Length--;
        }

        /// <summary>
        /// Entfernt die letzten <paramref name="length"/> werte des Arrays. 
        /// </summary>
        /// <param name="length"></param>
        public void RemoveLast(int length)
        {
            Length -= length;
        }

        /// <summary>
        /// Entfernt den ersten Wert und gibt in zurück. 
        /// </summary>
        /// <returns></returns>
        public T PopFirst()
        {
            T temp = this[0];
            Array.Copy(array, 1, array, 0, --Length);
            return temp;
        }

        /// <summary>
        /// Entfernt den letzten Wert und gibt in zurück. 
        /// </summary>
        /// <returns></returns>
        public T PopLast()
        {
            return array[--Length];
        }


        /// <summary>
        /// Dreht das gesammte Array um.
        /// </summary>
        public void Reverse()
        {
            Array.Reverse(array, 0, Length);
        }

        /// <summary>
        /// Dreht den angegebenen Bereich um.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="length"></param>
        public void Reverse(int index, int length)
        {
            if (Length < index + length) throw new ArgumentOutOfRangeException();
            Array.Reverse(array, index, length);
        }


        /// <summary>
        /// Setzt die Größe des Arrays auf die kleinste 2erPotenz, 
        /// welche größer als 'Anzahl' ist. 
        /// </summary>
        public void UpdateArraySize()
        {
            Array.Resize(ref array, NextBin(Length));
        }

        /// <summary>
        /// Löscht das BinArray. 
        /// </summary>
        /// <param name="clearBaseArray">Gibt an, ob das Array auch gelöscht werden soll.</param>
        public void Clear(bool clearBaseArray = false)
        {
            if (clearBaseArray) array = new T[1];
            Length = 0;
        }

        /// <summary>
        /// Gibt dier Zugang auf das gesammte Array. 
        /// </summary>
        public void GetAccessToArray()
        {
            Length = array.Length;
        }


        /// <summary>
        /// Gibt eine Kopie des BinArrays zurück.
        /// </summary>
        /// <returns></returns>
        public BinArray<T> Copy()
        {
            BinArray<T> copy = new BinArray<T>(Length);
            for (int i = 0; i < Length; i++) copy[i] = this[i];
            return copy;
        }

        /// <summary>
        /// Kopiert die Werte von <paramref name="source"/> in <paramref name="destination"/>. 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="length">Gibt an wie viele Werte kopiert werden sollen. (begonnen bei 0)</param>
        static public void Copy(BinArray<T> source, BinArray<T> destination, int length)
        {
            if (length < 0 || source.Length < length) throw new ArgumentOutOfRangeException();
            if (destination.Length < length)
            {
                destination.Length = length;
                if (destination.array.Length < destination.Length) Array.Resize(ref destination.array, NextBin(destination.Length));
            }
            Array.Copy(source.array, destination.array, length);
        }

        /// <summary>
        /// Kopiert die Werte von <paramref name="source"/> in <paramref name="destination"/>. 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="length">Gibt an wie viele Werte kopiert werden sollen. (begonnen bei 0)</param>
        static public void Copy(T[] source, BinArray<T> destination, int length)
        {
            if (length < 0 || source.Length < length) throw new ArgumentOutOfRangeException();
            if (destination.Length < length)
            {
                destination.Length = length;
                if (destination.array.Length < destination.Length) Array.Resize(ref destination.array, NextBin(destination.Length));
            }
            Array.Copy(source, destination.array, length);
        }

        /// <summary>
        /// Kopiert die Werte von <paramref name="source"/> in <paramref name="destination"/>. 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="sourceIndex">Gibt an von wo die Datenkopierung gebinnen soll.</param>
        /// <param name="destination"></param>
        /// <param name="destinationIndex">Gibt an wo die Datenkopierung beginnen soll.</param>
        /// <param name="length">Gibt an wie viele Daten kopiert werden sollen.</param>
        static public void Copy(BinArray<T> source, int sourceIndex, BinArray<T> destination, int destinationIndex, int length)
        {
            if (sourceIndex < 0 || length < 0 || source.Length < sourceIndex + length ||
                destinationIndex < 0 || destination.Length < destinationIndex) throw new ArgumentOutOfRangeException();
            if (destination.Length < destinationIndex + length)
            {
                destination.Length = destinationIndex + length;
                if (destination.array.Length < destination.Length) Array.Resize(ref destination.array, NextBin(destination.Length));
            }
            Array.Copy(source.array, sourceIndex, destination.array, destinationIndex, length);
        }

        /// <summary>
        /// Kopiert die Werte von <paramref name="source"/> in <paramref name="destination"/>. 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="sourceIndex">Gibt an von wo die Datenkopierung gebinnen soll.</param>
        /// <param name="destination"></param>
        /// <param name="destinationIndex">Gibt an wo die Datenkopierung beginnen soll.</param>
        /// <param name="length">Gibt an wie viele Daten kopiert werden sollen.</param>
        static public void Copy(T[] source, int sourceIndex, BinArray<T> destination, int destinationIndex, int length)
        {
            if (sourceIndex < 0 || length < 0 || source.Length < sourceIndex + length ||
                destinationIndex < 0 || destination.Length < destinationIndex) throw new ArgumentOutOfRangeException();
            if (destination.Length < destinationIndex + length)
            {
                destination.Length = destinationIndex + length;
                if (destination.array.Length < destination.Length) Array.Resize(ref destination.array, NextBin(destination.Length));
            }
            Array.Copy(source, sourceIndex, destination.array, destinationIndex, length);
        }


        /// <summary>
        /// Gibt den ersten index zurück, bei dem gilt: BinArray['i'] = <paramref name="value"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns>'i'</returns>
        public int IndexOf(T value)
        {
            for (int i = 0; i < Length; i++) if (value.Equals(this[i])) return i;
            return -1;
        }

        /// <summary>
        /// Gibt den letzten Index zurück bei dem gilt: BinArray['i'] = <paramref name="value"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns>'i'</returns>
        public int LastIndexOf(T value)
        {
            return Array.IndexOf(array, value);
        }


        public new string ToString()
        {
            if (Length == 0) return "{ }";
            string zeichenkette = "(" + Length + ") { " + this[0].ToString();
            for (int i = 1; i < Length; i++) zeichenkette += "; " + this[i].ToString();
            zeichenkette += " }";
            return zeichenkette;
        }
    }

    public class Vektor
    {
        private C[] werte;

        public int Dimension
        {
            get { return werte.Length; }
        }

        public C this[int index]
        {
            get { return werte[index]; }
            set { werte[index] = value; }
        }

        public Vektor(int dimension)
        {
            werte = new C[dimension];
        }
        public Vektor(params C[] werte)
        {
            this.werte = werte;
        }


        static public Vektor operator +(Vektor v)
        {
            Vektor vektor = new Vektor(v.Dimension);
            for (int i = 0; i < vektor.Dimension; i++) vektor[i] = v[i];
            return vektor;
        }
        static public Vektor operator +(Vektor v, Vektor w)
        {
            if (v.Dimension != w.Dimension) throw new Exception("Dimensionen müssen gleich sein!");
            Vektor vektor = new Vektor(v.Dimension);
            for (int i = 0; i < vektor.Dimension; i++) vektor[i] = v[i] + w[i];
            return vektor;
        }
        static public Vektor operator -(Vektor v)
        {
            Vektor vektor = new Vektor(v.Dimension);
            for (int i = 0; i < vektor.Dimension; i++) vektor[i] = -v[i];
            return vektor;
        }
        static public Vektor operator -(Vektor v, Vektor w)
        {
            if (v.Dimension != w.Dimension) throw new Exception("Dimensionen müssen gleich sein!");
            Vektor vektor = new Vektor(v.Dimension);
            for (int i = 0; i < vektor.Dimension; i++) vektor[i] = v[i] - w[i];
            return vektor;
        }

        static public Vektor operator *(Vektor v, C faktor)
        {
            Vektor vektor = new Vektor(v.Dimension);
            for (int i = 0; i < v.Dimension; i++) vektor[i] = v[i] * faktor;
            return vektor;
        }
        static public Vektor operator *(C faktor, Vektor v)
        {
            Vektor vektor = new Vektor(v.Dimension);
            for (int i = 0; i < v.Dimension; i++) vektor[i] = faktor * v[i];
            return vektor;
        }
        static public C operator *(Vektor v, Vektor w)
        {
            if (v.Dimension != w.Dimension) throw new Exception("Dimensionen müssen gleich sein!");
            C prod = 0;
            for (int i = 0; i < v.Dimension; i++) prod += v[i] * w[i];
            return prod;
        }
        static public Vektor operator /(Vektor v, C divisor)
        {
            Vektor vektor = new Vektor(v.Dimension);
            for (int i = 0; i < v.Dimension; i++) vektor[i] = v[i] / divisor;
            return vektor;
        }


        public new string ToString()
        {
            if (Dimension == 0) return "( )";
            string zeichenkette = "( " + this[0];
            for (int i = 1; i < Dimension; i++) zeichenkette +="; "+ this[i];
            zeichenkette += " )";
            return zeichenkette;
        }
    }

    public class Matrix
    {
        private C[,] werte;

        public int ZeilenDimension
        {
            get { return werte.GetLength(0); }
        }
        public int SpaltenDimension
        {
            get { return werte.GetLength(1); }
        }

        public C this[int zeile, int spalte]
        {
            get { return werte[zeile, spalte]; }
            set { werte[zeile, spalte] = value; }
        }

        public Matrix(int zeilenDimension, int spaltenDimension)
        {
            werte = new C[zeilenDimension, spaltenDimension];
        }

        public Vektor ZeilenVektor(int zeile)
        {
            Vektor vektor = new Vektor(SpaltenDimension);
            for (int i = 0; i < vektor.Dimension; i++) vektor[i] = this[zeile, i];
            return vektor;
        }
        public Vektor[] ZeilenVektoren()
        {
            Vektor[] vektors = new Vektor[ZeilenDimension];
            for (int i = 0; i < vektors.Length; i++) vektors[i] = ZeilenVektor(i);
            return vektors;
        }
        public Vektor SpaltenVektor(int spalte)
        {
            Vektor vektor = new Vektor(ZeilenDimension);
            for (int i = 0; i < vektor.Dimension; i++) vektor[i] = this[i, spalte];
            return vektor;
        }
        public Vektor[] SpaltenVektoren()
        {
            Vektor[] vektors = new Vektor[SpaltenDimension];
            for (int i = 0; i < vektors.Length; i++) vektors[i] = SpaltenVektor(i);
            return vektors;
        }


        static public Matrix operator +(Matrix matrix)
        {
            Matrix ergebnis = new Matrix(matrix.ZeilenDimension, matrix.SpaltenDimension);
            for (int i = 0; i < matrix.ZeilenDimension; i++)
            {
                for (int j = 0; j < matrix.SpaltenDimension; j++)
                {
                    ergebnis[i, j] = matrix[i, j];
                }
            }
            return ergebnis;
        }
        static public Matrix operator +(Matrix matrix, C z)
        {
            Matrix ergebnis = new Matrix(matrix.ZeilenDimension, matrix.SpaltenDimension);
            for (int i = 0; i < matrix.ZeilenDimension; i++)
            {
                for (int j = 0; j < matrix.SpaltenDimension; j++)
                {
                    ergebnis[i, j] = (i == j ? matrix[i, j] + z : matrix[i, j]);
                }
            }
            return ergebnis;
        }
        static public Matrix operator +(C z, Matrix matrix)
        {
            Matrix ergebnis = new Matrix(matrix.ZeilenDimension, matrix.SpaltenDimension);
            for (int i = 0; i < matrix.ZeilenDimension; i++)
            {
                for (int j = 0; j < matrix.SpaltenDimension; j++)
                {
                    ergebnis[i, j] = (i == j ? z + matrix[i, j] : matrix[i, j]);
                }
            }
            return ergebnis;
        }
        static public Matrix operator +(Matrix matrix0, Matrix matrix1)
        {
            if (matrix0.ZeilenDimension != matrix1.ZeilenDimension || matrix0.SpaltenDimension != matrix1.SpaltenDimension) throw new Exception("Dimensionen passen nicht überein!");
            Matrix ergebnis = new Matrix(matrix0.ZeilenDimension, matrix0.SpaltenDimension);
            for (int i = 0; i < matrix0.ZeilenDimension; i++)
            {
                for (int j = 0; j < matrix0.SpaltenDimension; j++)
                {
                    ergebnis[i, j] = matrix0[i, j] + matrix1[i, j];
                }
            }
            return ergebnis;
        }
        static public Matrix operator -(Matrix matrix)
        {
            Matrix ergebnis = new Matrix(matrix.ZeilenDimension, matrix.SpaltenDimension);
            for (int i = 0; i < matrix.ZeilenDimension; i++)
            {
                for (int j = 0; j < matrix.SpaltenDimension; j++)
                {
                    ergebnis[i, j] = -matrix[i, j];
                }
            }
            return ergebnis;
        }
        static public Matrix operator -(Matrix matrix, C z)
        {
            Matrix ergebnis = new Matrix(matrix.ZeilenDimension, matrix.SpaltenDimension);
            for (int i = 0; i < matrix.ZeilenDimension; i++)
            {
                for (int j = 0; j < matrix.SpaltenDimension; j++)
                {
                    ergebnis[i, j] = (i == j ? matrix[i, j] - z : matrix[i, j]);
                }
            }
            return ergebnis;
        }
        static public Matrix operator -(C z, Matrix matrix)
        {
            Matrix ergebnis = new Matrix(matrix.ZeilenDimension, matrix.SpaltenDimension);
            for (int i = 0; i < matrix.ZeilenDimension; i++)
            {
                for (int j = 0; j < matrix.SpaltenDimension; j++)
                {
                    ergebnis[i, j] = (i == j ? z - matrix[i, j] : -matrix[i, j]);
                }
            }
            return ergebnis;
        }
        static public Matrix operator -(Matrix matrix0, Matrix matrix1)
        {
            if (matrix0.ZeilenDimension != matrix1.ZeilenDimension || matrix0.SpaltenDimension != matrix1.SpaltenDimension) throw new Exception("Dimensionen passen nicht überein!");
            Matrix ergebnis = new Matrix(matrix0.ZeilenDimension, matrix0.SpaltenDimension);
            for (int i = 0; i < matrix0.ZeilenDimension; i++)
            {
                for (int j = 0; j < matrix0.SpaltenDimension; j++)
                {
                    ergebnis[i, j] = matrix0[i, j] - matrix1[i, j];
                }
            }
            return ergebnis;
        }

        static public Matrix operator *(Matrix matrix, C z)
        {
            Matrix ergebnis = new Matrix(matrix.ZeilenDimension, matrix.SpaltenDimension);
            for (int i = 0; i < matrix.ZeilenDimension; i++)
            {
                for (int j = 0; j < matrix.SpaltenDimension; j++)
                {
                    ergebnis[i, j] = matrix[i, j] * z;
                }
            }
            return ergebnis;
        }
        static public Matrix operator *(C z, Matrix matrix)
        {
            Matrix ergebnis = new Matrix(matrix.ZeilenDimension, matrix.SpaltenDimension);
            for (int i = 0; i < matrix.ZeilenDimension; i++)
            {
                for (int j = 0; j < matrix.SpaltenDimension; j++)
                {
                    ergebnis[i, j] = z * matrix[i, j];
                }
            }
            return ergebnis;
        }
        static public Vektor operator *(Matrix matrix, Vektor v)
        {
            if (matrix.SpaltenDimension != v.Dimension) throw new Exception("Dimensionen passen nicht überein!");
            Vektor ergebnis = new Vektor(matrix.ZeilenDimension);
            for (int i = 0; i < matrix.ZeilenDimension; i++)
            {
                ergebnis[i] = 0;
                for (int j = 0; j < matrix.SpaltenDimension; j++)
                {
                    ergebnis[i] += matrix[i, j] * v[j];
                }
            }
            return ergebnis;
        }
        static public Matrix operator *(Matrix matrix0, Matrix matrix1)
        {
            if (matrix0.SpaltenDimension != matrix1.ZeilenDimension) throw new Exception("Dimensionen passen nicht überein!");
            Matrix ergebnis = new Matrix(matrix0.ZeilenDimension, matrix1.SpaltenDimension);
            for (int i = 0; i < ergebnis.ZeilenDimension; i++)
            {
                for (int j = 0; j < ergebnis.SpaltenDimension; j++)
                {
                    ergebnis[i, j] = 0;
                    for (int k = 0; k < matrix0.SpaltenDimension; k++)
                    {
                        ergebnis[i, j] += matrix0[i, k] * matrix1[k, j];
                    }
                }
            }
            return ergebnis;
        }
        static public Matrix operator /(Matrix matrix, C z)
        {
            Matrix ergebnis = new Matrix(matrix.ZeilenDimension, matrix.SpaltenDimension);
            for (int i = 0; i < matrix.ZeilenDimension; i++)
            {
                for (int j = 0; j < matrix.SpaltenDimension; j++)
                {
                    ergebnis[i, j] = matrix[i, j] / z;
                }
            }
            return ergebnis;
        }
    }
}
