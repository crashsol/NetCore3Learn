using System;
using System.Linq.Expressions;

namespace ExpressionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Expression<Func<int, int, int>> adder = (x, y) => x + y;
            Console.WriteLine(adder);

            // handwriten
            ParameterExpression xParameter = Expression.Parameter(typeof(int), "x");
            ParameterExpression yParameter = Expression.Parameter(typeof(int), "y");
            Expression body = Expression.Add(xParameter, yParameter);
            ParameterExpression[] parameters = new [] { xParameter, yParameter };
            Expression<Func<int, int, int>> adder2 = Expression.Lambda<Func<int, int, int>>(body, parameters);

            Console.WriteLine(adder2);

            //如果lambad具有语句表达体，则无法转化为Express
            //Expression<Func<int, int, int>> adder3 = (x, y) => { return x + y; };
            
           

            Console.ReadLine();
        }
    }
}
