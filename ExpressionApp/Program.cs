using System;
using System.Linq.Expressions;
using Snowflake.Core;

namespace ExpressionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var worker = new IdWorker(1, 1);
            for (int i = 0; i < 100; i++)
            {
                long id = worker.NextId();

                Console.WriteLine($"生成的ID为：{id}，他的长度是：{id.ToString().Length}");
              

            }

            Console.ReadKey();


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
