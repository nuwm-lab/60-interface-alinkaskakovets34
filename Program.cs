using System;

// =================== АБСТРАКТНИЙ БАЗОВИЙ КЛАС ===================

abstract class ShapeND
{
    protected double[] coeff; // масив коефіцієнтів

    public ShapeND(int size)
    {
        coeff = new double[size];
        Console.WriteLine("Викликано конструктор ShapeND");
    }

    // Деструктор
    ~ShapeND()
    {
        Console.WriteLine("Викликано деструктор ShapeND");
    }

    // Віртуальний метод для задання коефіцієнтів
    public virtual void Set(params double[] values)
    {
        for (int i = 0; i < coeff.Length && i < values.Length; i++)
            coeff[i] = values[i];
    }

    // Абстрактні методи — обов'язкові у похідних класах
    public abstract void Print();

    public abstract bool Contains(params double[] point);
}


// =================== КЛАС Line (пряма у 2D) ===================

class Line : ShapeND
{
    public Line() : base(3)
    {
        Console.WriteLine("Викликано конструктор Line");
    }

    ~Line()
    {
        Console.WriteLine("Викликано деструктор Line");
    }

    public override void Print()
    {
        Console.WriteLine($"Пряма: {coeff[1]}*x + {coeff[2]}*y + {coeff[0]} = 0");
    }

    public override bool Contains(params double[] p)
    {
        if (p.Length < 2) return false;
        double x = p[0], y = p[1];
        return Math.Abs(coeff[1] * x + coeff[2] * y + coeff[0]) < 1e-9;
    }
}


// =================== КЛАС Hyperplane (гіперплощина у 4D) ===================

class Hyperplane : ShapeND
{
    public Hyperplane() : base(5)
    {
        Console.WriteLine("Викликано конструктор Hyperplane");
    }

    ~Hyperplane()
    {
        Console.WriteLine("Викликано деструктор Hyperplane");
    }

    public override void Print()
    {
        Console.WriteLine($"Гіперплощина: {coeff[4]}*x4 + {coeff[3]}*x3 + {coeff[2]}*x2 + {coeff[1]}*x1 + {coeff[0]} = 0");
    }

    public override bool Contains(params double[] p)
    {
        if (p.Length < 4) return false;
        return Math.Abs(coeff[4] * p[3] + coeff[3] * p[2] + coeff[2] * p[1] + coeff[1] * p[0] + coeff[0]) < 1e-9;
    }
}


// =================== ГОЛОВНА ПРОГРАМА ===================

class Program
{
    static void Main()
    {
        Console.WriteLine("Виберіть режим:");
        Console.WriteLine("1 — працювати з прямою Line");
        Console.WriteLine("2 — працювати з гіперплощиною Hyperplane");

        char choose = Console.ReadKey().KeyChar;
        Console.WriteLine();

        ShapeND obj; // посилання на базовий клас

        if (choose == '1')
        {
            obj = new Line();
            obj.Set(3, 2, -1);  // a0, a1, a2
            Console.WriteLine("Створено об'єкт типу Line\n");
        }
        else
        {
            obj = new Hyperplane();
            obj.Set(1, 2, 3, 4, 5);  // a0..a4
            Console.WriteLine("Створено об'єкт типу Hyperplane\n");
        }

        // Поліморфний виклик віртуальних методів
        Console.WriteLine("=== Вивід коефіцієнтів ===");
        obj.Print();

        Console.WriteLine("\nПеревіряємо точку (1,1,1,1):");
        bool inside = obj.Contains(1, 1, 1, 1);
        Console.WriteLine(inside ? "Точка належить." : "Точка НЕ належить.");

        Console.WriteLine("\nКінець роботи програми.\n");
    }
}
