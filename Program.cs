using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FigArea
{
    class Figure
    {
        protected int N;
        protected double L;
        protected string Str="";

        public Figure(int n, double l)
        {
            N = n;
            L = l;
        }
       
        public virtual double GetArea()
        {
            Console.WriteLine("Area of  " + Str + "  is eq =  ");
            return 0;
        }
    }

    class Rectangle : Figure
    {
        string str = "Rectangle"; 
        public Rectangle(int n, double l) : base(n, l) { }

        public override double GetArea()
        {
            
            double area;
            Str = str;
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

    class Obj : Figure
    {

        public Obj(int N, double L) : base(N, L) { }

        public static void Choose(int N, double L)
        {

            switch (N)
            {
                case 3:
                    Figure triangle = new Triangle(N,L);
                    triangle.GetArea();
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

        public static void Coord(double[] x, double[] y, int N, double L)
        {
            double deg;
            int i;
            
            deg = 2.0 * Math.PI / N;

            for (i = 0; i < N; i++)
            {
                x[i] = L * Math.Cos(i * deg);
                y[i] = L * Math.Sin(i * deg);
            }



        }
    }

    class Program
    {
        public static int NAxal;
        public static double Lenght;
        public static bool Write=false;
        public static string char1;
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
                Console.WriteLine(e);
            }

            try
            {
                Lenght = Convert.ToDouble(args[1]);
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine(e);
            }

/*****************************************************/
            Obj.Choose(NAxal, Lenght);
/*******************************************************/
            try  //save?
            {
                char1 = Convert.ToString(args[2]);
                Write = true;
                Console.WriteLine("Result has been saved!");
                System.IO.StreamWriter file = new System.IO.StreamWriter("data.txt");
                Obj.Coord(x, y, NAxal, Lenght);
                for (int i = 0; i < NAxal; i++)
                {
                    char1 = x[i].ToString() + "     " + y[i].ToString();
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
