using System;
using System.Reflection;

namespace ReflectionConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\r\n--- Examine a generic method.");

            Type ex = typeof(GenericMethodTest);

            MethodInfo mi = ex.GetMethod("Generic");

            DispalyGenericMethodInfo(mi);

            object[] shouwParams = { 41 };          


            MethodInfo miConstructed = mi.MakeGenericMethod(typeof(int));

            DispalyGenericMethodInfo(miConstructed);

         
            miConstructed.Invoke(null, shouwParams);


            GenericMethodTest.Generic<double>(0.01);


            MethodInfo miDef = miConstructed.GetGenericMethodDefinition();
            Console.WriteLine("\r\nThe definition is the same: {0}",
                miDef == mi);





            Console.ReadLine();
        }

        private static void DispalyGenericMethodInfo(MethodInfo mi)
        {
            Console.WriteLine("\r\n{0}", mi);

            Console.WriteLine("\tIs this a generic method definition? {0}", mi.IsGenericMethodDefinition);

            Console.WriteLine("\tIs it a generic method? {0}", mi.IsGenericMethod);

            Console.WriteLine("\tDoes it have unassigned generic parameters? {0}",
              mi.ContainsGenericParameters);

            // If this is a generic method, display its type arguments.
            //
            if (mi.IsGenericMethod)
            {
                Type[] typeArguments = mi.GetGenericArguments();

                Console.WriteLine("\tList type arguments ({0}):",
                    typeArguments.Length);

                foreach (Type tParam in typeArguments)
                {
                    // IsGenericParameter is true only for generic type
                    // parameters.
                    //
                    if (tParam.IsGenericParameter)
                    {
                        Console.WriteLine("\t\t{0}  parameter position {1}" +
                            "\n\t\t   declaring method: {2}",
                            tParam,
                            tParam.GenericParameterPosition,
                            tParam.DeclaringMethod);
                    }
                    else
                    {
                        Console.WriteLine("\t\t{0}", tParam);
                    }
                }
            }



        }
    }
}
