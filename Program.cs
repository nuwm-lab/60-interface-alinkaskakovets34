using System;

// ===== ІНТЕРФЕЙСИ =====
interface IPrintable
{
    void PrintCoefficients();
}

interface IEvaluable
{
    double Evaluate(double x);
}

// ===== АБСТРАКТНИЙ КЛАС =====
abstract class RationalFunction : IEvaluable, IPrintable
{
    public abstract void SetCoefficients(params double[] c);
    public abstract double Evaluate(double x);
    public abstract void PrintCoefficients();

    protected void CheckDenominator(double val)
    {
        if (Math.Abs(val) < 1e-12)
            throw new DivideByZeroException("Помилка: знаменник = 0 (вертикальна асимптота)!");
    }
}

// ===== ЛІНІЙНА ДРОБОВА ФУНКЦІЯ =====
class FractionLinear : RationalFunction
{
    private double A1, A0, B1, B0;

    public override void SetCoefficients(params double[] c)
    {
        if (c.Length != 4) throw new ArgumentException("Потрібно 4 коефіцієнти!");
        (A1, A0, B1, B0) = (c[0], c[1], c[2], c[3]);
    }

    public override void PrintCoefficients()
    {
        Console.WriteLine($"f(x) = ({A1}x + {A0}) / ({B1}x + {B0})");
    }

    public override double Evaluate(double x)
    {
        double denom = B1 * x + B0;
        CheckDenominator(denom);
        return (A1 * x + A0) / denom;
    }
}

// ===== КВАДРАТИЧНА ДРОБОВА ФУНКЦІЯ =====
class FractionQuadratic : RationalFunction
{
    private double A2, A1, A0, B2, B1, B0;

    public override void SetCoefficients(params double[] c)
    {
        if (c.Length != 6) throw new ArgumentException("Потрібно 6 коефіцієнтів!");
        (A2, A1, A0, B2, B1, B0) = (c[0], c[1], c[2], c[3], c[4], c[5]);
    }

    public override void PrintCoefficients()
    {
        Console.WriteLine($"g(x) = ({A2}x² + {A1}x + {A0}) / ({B2}x² + {B1}x + {B0})");
    }

    public override double Evaluate(double x)
    {
        double denom = B2 * x * x + B1 * x + B0;
        CheckDenominator(denom);
        return (A2 * x * x + A1 * x + A0) / denom;
    }
}

// ===== MAIN =====
class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        IEvaluable func; // Поліморфізм ✔
        Console.WriteLine("Оберіть тип функції: 1 - Лінійна, 2 - Квадратична");

        string? choice = Console.ReadLine();
        func = choice == "1" ? new FractionLinear() : new FractionQuadratic();

        try
        {
            if (func is FractionLinear fl)
            {
                fl.SetCoefficients(2, 3, 4, 5);
                fl.PrintCoefficients();
            }
            else if (func is FractionQuadratic fq)
            {
                fq.SetCoefficients(1, 2, 3, 1, 1, 2);
                fq.PrintCoefficients();
            }

            Console.Write("\nВведіть x0 = ");
            if (double.TryParse(Console.ReadLine(), out double x))
            {
                Console.WriteLine($"Результат: {func.Evaluate(x):F4}");
            }
            else Console.WriteLine("Некоректне значення x!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

