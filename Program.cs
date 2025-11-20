using System;

namespace LabWork
{
    /// <summary>
    /// Базовий абстрактний клас для дробових функцій.
    /// Містить методи обчислення та виведення.
    /// </summary>
    public abstract class RationalFunction : IEvaluable, IPrintable
    {
        public abstract void SetCoefficients(params double[] c);
        public abstract double Evaluate(double x);
        public abstract void PrintCoefficients();
        public abstract override string ToString();

        /// <summary>
        /// Перевірка знаменника перед діленням.
        /// </summary>
        protected void CheckDenominator(double val)
        {
            if (Math.Abs(val) < 1e-12)
                throw new DivideByZeroException("Знаменник = 0. Вертикальна асимптота!");
        }

        ~RationalFunction()
        {
            Console.WriteLine("Finalize RationalFunction (демонстрація)");
        }
    }

    /// <summary>Інтерфейс обчислення функції.</summary>
    public interface IEvaluable
    {
        double Evaluate(double x);
    }

    /// <summary>Інтерфейс виведення коефіцієнтів.</summary>
    public interface IPrintable
    {
        void PrintCoefficients();
    }

    /// <summary>Дробово-лінійна функція (a1x + a0) / (b1x + b0)</summary>
    public class FractionLinear : RationalFunction
    {
        private double _a1, _a0, _b1, _b0;

        public FractionLinear(double a1, double a0, double b1, double b0)
        {
            SetCoefficients(a1, a0, b1, b0);
        }

        public override void SetCoefficients(params double[] c)
        {
            if (c.Length != 4) throw new ArgumentException("Очікується 4 коефіцієнти.");
            (_a1, _a0, _b1, _b0) = (c[0], c[1], c[2], c[3]);
        }

        public override double Evaluate(double x)
        {
            double denom = _b1 * x + _b0;
            CheckDenominator(denom);
            return (_a1 * x + _a0) / denom;
        }

        public override void PrintCoefficients() => Console.WriteLine(ToString());
        public override string ToString() => $"f(x) = ({_a1}x + {_a0}) / ({_b1}x + {_b0})";
    }

    /// <summary>Дробова квадратична функція (a2x² + a1x + a0) / (b2x² + b1x + b0)</summary>
    public class FractionQuadratic : RationalFunction
    {
        private double _a2, _a1, _a0, _b2, _b1, _b0;

        public FractionQuadratic(double a2, double a1, double a0, double b2, double b1, double b0)
        {
            SetCoefficients(a2, a1, a0, b2, b1, b0);
        }

        public override void SetCoefficients(params double[] c)
        {
            if (c.Length != 6) throw new ArgumentException("Очікується 6 коефіцієнтів.");
            (_a2, _a1, _a0, _b2, _b1, _b0) = (c[0], c[1], c[2], c[3], c[4], c[5]);
        }

        public override double Evaluate(double x)
        {
            double denom = _b2 * x * x + _b1 * x + _b0;
            CheckDenominator(denom);
            return (_a2 * x * x + _a1 * x + _a0) / denom;
        }

        public override void PrintCoefficients() => Console.WriteLine(ToString());
        public override string ToString() => $"g(x) = ({_a2}x² + {_a1}x + {_a0}) / ({_b2}x² + {_b1}x + {_b0})";
    }

    // ---- Головна програма ----
    public class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            IEvaluable func; // поліморфізм
            Console.WriteLine("Оберіть функцію: 1 - лінійна, 2 - квадратична");

            func = Console.ReadLine() == "1"
                ? new FractionLinear(2, 3, 4, 5)
                : new FractionQuadratic(1, 2, 3, 1, 1, 2);

            Console.Write("Введіть x: ");
            if (!double.TryParse(Console.ReadLine(), out double x))
            {
                Console.WriteLine("Помилка вводу.");
                return;
            }

            try
            {
                Console.WriteLine($"Результат: {func.Evaluate(x):F4}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

