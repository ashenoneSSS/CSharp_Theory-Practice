using System;
using System.Linq;

namespace LinSys4x4
{
    
    public readonly struct Frac : IComparable<Frac>
    {
        public readonly long Num; 
        public readonly long Den; 

        static long Gcd(long a, long b)
        {
            a = Math.Abs(a); b = Math.Abs(b);
            if (a == 0 && b == 0) return 1;
            while (b != 0) { long t = a % b; a = b; b = t; }
            return a == 0 ? 1 : a;
        }

        
        public Frac(long n, long d)
        {
            if (d == 0) { Num = 0; Den = 1; return; }
            if (d < 0) { n = -n; d = -d; }
            long g = Gcd(n, d);
            Num = n / g; Den = d / g;
        }

        public static Frac FromInt(long x) => new Frac(x, 1);

        public static Frac operator +(Frac a, Frac b) => new Frac(a.Num * b.Den + b.Num * a.Den, a.Den * b.Den);
        public static Frac operator -(Frac a, Frac b) => new Frac(a.Num * b.Den - b.Num * a.Den, a.Den * b.Den);
        public static Frac operator *(Frac a, Frac b) => new Frac(a.Num * b.Num, a.Den * b.Den);
        
        public static Frac operator /(Frac a, Frac b)
        {
            if (b.Num == 0) return new Frac(0, 1);
            return new Frac(a.Num * b.Den, a.Den * b.Num);
        }
        public static Frac operator -(Frac a) => new Frac(-a.Num, a.Den);

        public int CompareTo(Frac other)
        {
            decimal left = (decimal)Num * (decimal)other.Den;
            decimal right = (decimal)other.Num * (decimal)Den;
            return left.CompareTo(right);
        }

        public double ToDouble() => (double)Num / (double)Den;
        public Frac Abs() => new Frac(Math.Abs(Num), Den);

        public override string ToString()
        {
            if (Den == 1) return Num.ToString();
            if (Num == 0) return "0";
            return $"{Num}/{Den}";
        }

        public override bool Equals(object obj) => obj is Frac f && f.Num == Num && f.Den == Den;
        public override int GetHashCode() { unchecked { return ((Num.GetHashCode() * 397) ^ Den.GetHashCode()); } }
    }

    
    public static class F
    {
        public static Frac[,] Copy(Frac[,] A)
        {
            int n = A.GetLength(0), m = A.GetLength(1);
            var C = new Frac[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    C[i, j] = A[i, j];
            return C;
        }
        public static Frac[] Copy(Frac[] v)
        {
            var r = new Frac[v.Length];
            for (int i = 0; i < v.Length; i++) r[i] = v[i];
            return r;
        }
        public static Frac[,] I(int n)
        {
            var I = new Frac[n, n];
            for (int i = 0; i < n; i++) I[i, i] = Frac.FromInt(1);
            return I;
        }
        public static void SwapRows(Frac[,] A, int r1, int r2)
        {
            if (r1 == r2) return;
            int m = A.GetLength(1);
            for (int j = 0; j < m; j++) { var t = A[r1, j]; A[r1, j] = A[r2, j]; A[r2, j] = t; }
        }
        public static void SwapCols(Frac[,] A, int c1, int c2)
        {
            if (c1 == c2) return;
            int n = A.GetLength(0);
            for (int i = 0; i < n; i++) { var t = A[i, c1]; A[i, c1] = A[i, c2]; A[i, c2] = t; }
        }
        public static Frac[,] Mul(Frac[,] A, Frac[,] B)
        {
            int n = A.GetLength(0), m = A.GetLength(1), p = B.GetLength(1);
            var C = new Frac[n, p];
            for (int i = 0; i < n; i++)
                for (int k = 0; k < m; k++)
                {
                    var aik = A[i, k];
                    if (aik.Num == 0) continue;
                    for (int j = 0; j < p; j++)
                        C[i, j] = C[i, j] + aik * B[k, j];
                }
            return C;
        }
        public static Frac[] Mul(Frac[,] A, Frac[] x)
        {
            int n = A.GetLength(0), m = A.GetLength(1);
            var y = new Frac[n];
            for (int i = 0; i < n; i++)
            {
                Frac s = new Frac(0, 1);
                for (int j = 0; j < m; j++) s = s + A[i, j] * x[j];
                y[i] = s;
            }
            return y;
        }
        public static void PrintMat(string title, Frac[,] A)
        {
            Console.WriteLine(title);
            int n = A.GetLength(0), m = A.GetLength(1);
            for (int i = 0; i < n; i++)
            {
                Console.Write("  ");
                for (int j = 0; j < m; j++)
                {
                    Console.Write(A[i, j].ToString().PadLeft(10));
                    if (j + 1 < m) Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public static void PrintAug(string title, Frac[,] A, Frac[] b)
        {
            Console.WriteLine(title);
            int n = A.GetLength(0), m = A.GetLength(1);
            for (int i = 0; i < n; i++)
            {
                Console.Write("  ");
                for (int j = 0; j < m; j++)
                {
                    Console.Write(A[i, j].ToString().PadLeft(10));
                    if (j + 1 < m) Console.Write(" ");
                }
                Console.Write("  | ");
                Console.WriteLine(b[i]);
            }
            Console.WriteLine();
        }
        public static Frac Norm1(Frac[,] A)
        {
            int n = A.GetLength(0), m = A.GetLength(1);
            Frac best = new Frac(0, 1);
            for (int j = 0; j < m; j++)
            {
                Frac s = new Frac(0, 1);
                for (int i = 0; i < n; i++) s = s + A[i, j].Abs();
                if (best.CompareTo(s) < 0) best = s;
            }
            return best;
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            var A0 = new Frac[4, 4]
            {
                { new Frac(7,1),  new Frac(2,1),  new Frac(-3,1), new Frac(1,1) },
                { new Frac(-4,1), new Frac(8,1),  new Frac(1,1),  new Frac(-2,1) },
                { new Frac(3,1),  new Frac(-2,1), new Frac(9,1),  new Frac(2,1) },
                { new Frac(1,1),  new Frac(-1,1), new Frac(1,1),  new Frac(4,1) }
            };
            var b0 = new Frac[] { new Frac(-1, 1), new Frac(16, 1), new Frac(-3, 1), new Frac(5, 1) };

            Console.WriteLine("=== МЕТОД ГАУСА (повний вибір головного елемента) ===\n");
            GaussFullPivot(A0, b0, out var detA, out var invA, out var x);

            Console.WriteLine("Розв'язок:");
            for (int i = 0; i < x.Length; i++) Console.WriteLine($"  x{i + 1} = {x[i]}");
            Console.WriteLine();
            Console.WriteLine($"Det(A) = {detA}");
            Console.WriteLine($"||A||_1 = {F.Norm1(A0)}");
            Console.WriteLine($"||A^{-1}||_1 = {F.Norm1(invA)}");
            Console.WriteLine($"cond_1(A) = {F.Norm1(A0) * F.Norm1(invA)}");
        }

        static void GaussFullPivot(Frac[,] Ainit, Frac[] binit,
                                   out Frac detA, out Frac[,] invA, out Frac[] xFinal)
        {
            int n = Ainit.GetLength(0);
            var A = F.Copy(Ainit);
            var b = F.Copy(binit);
            int[] colPerm = Enumerable.Range(0, n).ToArray();
            int rowSwaps = 0, colSwaps = 0;

            F.PrintAug("A0 (розширена):", A, b);

            var pivots = new Frac[n];

            for (int k = 0; k < n; k++)
            {
                
                int pi = k, pj = k;
                Frac best = new Frac(0, 1);
                for (int i = k; i < n; i++)
                    for (int j = k; j < n; j++)
                    {
                        var val = A[i, j].Abs();
                        if (val.CompareTo(best) > 0) { best = val; pi = i; pj = j; }
                    }

                var R = F.I(n);
                if (pi != k) { F.SwapRows(A, pi, k); var tb = b[pi]; b[pi] = b[k]; b[k] = tb; rowSwaps++; R = PermRows(n, pi, k); }
                F.PrintMat($"P{k + 1}", R);
                F.PrintAug($"Після P{k + 1}:", A, b);

                var C = F.I(n);
                if (pj != k) { F.SwapCols(A, pj, k); int t = colPerm[pj]; colPerm[pj] = colPerm[k]; colPerm[k] = t; colSwaps++; C = PermCols(n, pj, k); }
                F.PrintMat($"C{k + 1}", C);
                F.PrintAug($"Після C{k + 1}:", A, b);

                pivots[k] = A[k, k];
                var M = F.I(n);
                for (int i = k + 1; i < n; i++)
                {
                    var m = A[i, k] / A[k, k]; 
                    if (m.Num == 0) continue;
                    M[i, k] = -m;
                }
                F.PrintMat($"M{k + 1}", M);
                A = F.Mul(M, A);
                b = F.Mul(M, b);
                F.PrintAug($"A{k + 1}:", A, b);
            }

            // зворотній хід
            var y = new Frac[n];
            for (int i = n - 1; i >= 0; i--)
            {
                Frac s = new Frac(0, 1);
                for (int j = i + 1; j < n; j++) s = s + A[i, j] * y[j];
                y[i] = (b[i] - s) / A[i, i];
            }

            // повертаємо порядок змінних після перестановок стовпців
            var xTemp = new Frac[n];
            for (int i = 0; i < n; i++) xTemp[i] = y[i];
            var x = new Frac[n];
            for (int j = 0; j < n; j++) x[colPerm[j]] = xTemp[j];
            xFinal = x;

            // детермінант з урахуванням перестановок
            detA = Frac.FromInt(((rowSwaps + colSwaps) % 2 == 0) ? 1 : -1);
            for (int i = 0; i < n; i++) detA = detA * A[i, i];

            // обернена через Гаус–Жордан з частковим вибором
            invA = Inverse(Ainit);
        }

        static Frac[,] Inverse(Frac[,] A)
        {
            int n = A.GetLength(0);
            var M = F.Copy(A);
            var Inv = F.I(n);

            for (int k = 0; k < n; k++)
            {
                int piv = k;
                for (int i = k; i < n; i++)
                    if (M[i, k].Abs().CompareTo(M[piv, k].Abs()) > 0) piv = i;
                if (piv != k) { F.SwapRows(M, piv, k); F.SwapRows(Inv, piv, k); }

                var diag = M[k, k];
                var invDiag = new Frac(1, 1) / diag; // якщо diag==0 -> 0 (але для невиродженої A не трапиться)

                for (int j = 0; j < n; j++) { M[k, j] = M[k, j] * invDiag; Inv[k, j] = Inv[k, j] * invDiag; }

                for (int i = 0; i < n; i++)
                {
                    if (i == k) continue;
                    var m = M[i, k];
                    if (m.Num == 0) continue;
                    for (int j = 0; j < n; j++)
                    {
                        M[i, j] = M[i, j] - m * M[k, j];
                        Inv[i, j] = Inv[i, j] - m * Inv[k, j];
                    }
                }
            }
            return Inv;
        }

        static Frac[,] PermRows(int n, int r1, int r2)
        {
            var P = F.I(n);
            for (int j = 0; j < n; j++) { var t = P[r1, j]; P[r1, j] = P[r2, j]; P[r2, j] = t; }
            return P;
        }

        static Frac[,] PermCols(int n, int c1, int c2)
        {
            var P = F.I(n);
            for (int i = 0; i < n; i++) { var t = P[i, c1]; P[i, c1] = P[i, c2]; P[i, c2] = t; }
            return P;
        }
    }
}
