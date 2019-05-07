using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionConsole
{
    /// <summary>
    /// 范型方法
    /// </summary>
   public class GenericMethodTest
    {
        public static void Generic<T>(T dispaly)
        {
            Console.WriteLine("\r\n Here its is{0}",dispaly);
        }
    }
}
