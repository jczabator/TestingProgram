using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FigureArea // Use full names instead of short name FigArea
{
    class Figure
    {
        protected int N; // Provide funn names according to C# naming conventions https://www.dofactory.com/reference/csharp-coding-standards
        protected double L;
        protected string Str=""; // Do not use Str name. Use something like FigureName. 
        // When adding default value to 'Str' use string.Empty instead of ""

        public Figure(int n, double l) // Change names of parameters to for example sideNumber sideLength
        {
            N = n;
            L = l;
        }
       
        public virtual double GetArea()
        {
            Console.WriteLine("Area of  " + Str + "  is eq =  "); // Do not use concatenation with string with + use string.Format() method
            return 0;                                             // Be aware of memory leaks when using + to join string (use StringBuilder instead)
        }
    }

    class Rectangle : Figure // Each class should be in seperate file for example Rectangle.cs
    {
        string str = "Rectangle"; // Why create new string str when Str from base class is visible here. Use FigureName(renamed from Str)
        public Rectangle(int n, double l)
            : base(n, l) 
        {
        } // <-- Change formatting to this

        public override double GetArea()
        {
                            // <-- Not needed blank line
            double area;
            Str = str; // Do not assign Str from base class here. Do it in constructor. You can assign value with reflection: FigureName = this.GetType().Name 
            base.GetArea();
            area = L * L;
            Console.WriteLine(area.ToString());
            return area;
           
        }
    }

    class Triangle : Figure
    {
        string str = "Triangle";

        public Triangle(int n, double l) : base(n, l) { }

        public override double GetArea()
        {
            Str = str;
            double area;

            area = 0.5 * L * L;
            base.GetArea();
            Console.WriteLine(area.ToString());
            return area;
        }
    }

    class Obj : Figure  // this class(factory pattern) do not need to derive from Figure
    {

        public Obj(int N, double L) : base(N, L) { } // N and L in base class are not used

        public static void Choose(int N, double L)
        {

            switch (N)
            {
                case 3:
                    Figure triangle = new Triangle(N,L); // this class should only return new objects(create them)
                    triangle.GetArea();                  // area should be calculated outside of class
                break;

                case 4:
                    Figure Rectangle = new Rectangle(N, L);
                    Rectangle.GetArea();
                break;

                default:
                    Console.WriteLine("Area not implementet yet");
                    Console.WriteLine("but points of polynomial might be calculated");
                break;
                    
            }


        }

        // Naming and return value
        public static void Coord(double[] x, double[] y, int N, double L) // this method should be in other class for example Helper class
        {
            double deg;
            int i;
            
            deg = 2.0 * Math.PI / N;

            for (i = 0; i < N; i++)
            {
                x[i] = L * Math.Cos(i * deg);   // You are passing by value two arrays x and y. When method will end these arrays will have no data. Read about passing arguments by value or by reference in c#.
                y[i] = L * Math.Sin(i * deg);
            }



        }
    }

    class Program
    {
        public static int NAxal; // Naming. NumberOfVertex?       
        public static double Lenght;
        public static bool Write=false;
        public static string char1; // Naming
        public static double[] x = new double[10];
        public static double[] y = new double[10];

        static void Main(string[] args)
        {


            try
            {
                NAxal = Convert.ToInt32(args[0]);
                if (NAxal <= 2)
                {
                    Console.WriteLine("Set correct values. Must be Greather then 2");
                    Console.ReadKey();
                    return;
                }

            }
            catch (InvalidCastException e)
            {
                Console.WriteLine(e); // e.Message
            }

            try                         // Use only one try catch for all operations
            {
                Lenght = Convert.ToDouble(args[1]);
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine(e);
            }

/*****************************************************/
            Obj.Choose(NAxal, Lenght);                  //Naming - use for example FigureFactory. And return object as FigureClass(for example Triangle returned as Figure)
/*******************************************************/
            try  //save?
            {
                char1 = Convert.ToString(args[2]);
                Write = true;
                Console.WriteLine("Result has been saved!");
                System.IO.StreamWriter file = new System.IO.StreamWriter("data.txt"); // try with using statement https://stackoverflow.com/questions/16499767/streamwriter-opening-and-closing
                Obj.Coord(x, y, NAxal, Lenght);
                for (int i = 0; i < NAxal; i++)
                {
                    char1 = x[i].ToString() + "     " + y[i].ToString(); // use string.Format
                    file.WriteLine(char1);

                }
                file.Close(); 

            }
            catch (IndexOutOfRangeException)
            {
                
                Console.WriteLine("Results has not been saved!");
            }


            Console.ReadKey();
        }
    }
}
