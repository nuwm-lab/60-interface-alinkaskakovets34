using System;

// Клас дробово-лінійної функції (a1*x + a0)/(b1*x + b0)
class FractionLinear
{
    protected double a1, a0, b1, b0;

    public void SetCoefficients(double a1, double a0, double b1, double b0)
    {
        this.a1 = a1;
        this.a0 = a0;
        this.b1 = b1;
        this.b0 = b0;
    }

    public virtual void Display()
    {
        Console.WriteLine($"f(x) = ({a1}x + {a0}) / ({b1}x + {b0})");
    }

    public virtual double Value(double x)
    {
        return (a1 * x + a0) / (b1 * x + b0);
    }
}


// Похідний клас дробової функції (a2*x^2 + a1*x + a0)/(b2*x^2 + b1*x + b0)
class FractionQuadratic : FractionLinear
{
    private double a2, b2;

    public void SetCoefficients(double a2, double a1, double a0,
                                double b2, double b1, double b0)
    {
        this.a2 = a2;
        this.a1 = a1;
        this.a0 = a0;
        this.b2 = b2;
        this.b1 = b1;
        this.b0 = b0;
    }

    public override void Display()
    {
        Console.WriteLine(
            $"g(x) = ({a2}x² + {a1}x + {a0}) / ({b2}x² + {b1}x + {b0})");
    }

    public override double Value(double x)
    {
        return (a2 * x * x + a1 * x + a0) /
               (b2 * x * x + b1 * x + b0);
    }
}


// Демонстрація
class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // 1) Об’єкт лінійної дробової функції
        FractionLinear f = new FractionLinear();
        f.SetCoefficients(2, 3, 4, 5);  // (2x+3)/(4x+5)

        Console.WriteLine("=== Лінійна дробова функція ===");
        f.Display();
        Console.WriteLine($"f(1) = {f.Value(1):F4}");

        // 2) Об’єкт дробової квадратичної функції
        FractionQuadratic g = new FractionQuadratic();
        g.SetCoefficients(1, 2, 3, 1, 1, 2); // (x²+2x+3)/(x²+x+2)

        Console.WriteLine("\n=== Квадратична дробова функція ===");
        g.Display();
        Console.WriteLine($"g(1) = {g.Value(1):F4}");
    }
}

        Shape obj;


        Console.WriteLine("Choose object: 0 - Circle, 1 - Sphere");
        int choice = Convert.ToInt32(Console.ReadLine());


        if (choice == 0)
        {
            obj = new Circle(0, 0, 5);
        }
        else
        {
            obj = new Sphere(0, 0, 10, 5);
        }


        Console.WriteLine("\n--- Object Information ---");
        obj.Display();
        Console.WriteLine($"Area: {obj.Area():F2}");
    }
}
