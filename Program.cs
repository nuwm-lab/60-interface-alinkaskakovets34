using System;


// Абстрактний клас — спільний для всіх фігур
abstract class Shape
{
    protected int x0, y0, r;


    public Shape(int x, int y, int radius)
    {
        x0 = x;
        y0 = y;
        r = radius;
        Console.WriteLine("Shape constructor called");
    }


    ~Shape()
    {
        Console.WriteLine("Shape destructor called");
    }


    public virtual void Display()
    {
        Console.WriteLine($"Center: ({x0}, {y0}), Radius: {r}");
    }


    // Абстрактний метод — обов’язково переозначати
    public abstract double Area();
}

//  Клас Circle (коло)
class Circle : Shape
{
    public Circle(int x, int y, int radius)
        : base(x, y, radius)
    {
        Console.WriteLine("Circle constructor called");
    }


    ~Circle()
    {
        Console.WriteLine("Circle destructor called");
    }


    public override double Area()
    {
        return Math.PI * r * r;
    }


    public double Length()
    {
        return 2 * Math.PI * r;
    }


    public override void Display()
    {
        Console.WriteLine($"Circle Center: ({x0}, {y0}), Radius: {r}");
    }
}

//  Клас Sphere (сфера)
class Sphere : Shape
{
    protected int z0;


    public Sphere(int x, int y, int z, int radius)
        : base(x, y, radius)
    {
        z0 = z;
        Console.WriteLine("Sphere constructor called");
    }


    ~Sphere()
    {
        Console.WriteLine("Sphere destructor called");
    }


    public override double Area()
    {
        return 4 * Math.PI * r * r;
    }


    public override void Display()
    {
        Console.WriteLine($"Sphere Center: ({x0}, {y0}, {z0}), Radius: {r}");
    }
}

//  Демонстрація роботи
class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;


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
