using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using AspectCore.Injector;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft;
using System.ComponentModel;

namespace AopTest.Interceptors
{
    public class CustomInterceptorAttribute : AbstractInterceptorAttribute
    {

        [FromContainer]
        public ILogger<CustomInterceptorAttribute> Logger { get; set; }

        public override async Task Invoke(AspectContext context, AspectDelegate next) 
        {
            try
            {



                var descrption =( context.ServiceMethod.GetCustomAttributes(true).GetValue(0) as DescriptionAttribute ).Description;


                Logger.LogInformation($"Before Exceted Method:{descrption} ,{JsonConvert.SerializeObject(context.Parameters)},");
                await next(context);               
            }
            catch (Exception ex)
            {
                Logger.LogError("Service threw an exception!");
                throw;
            }
            finally
            {
                Logger.LogInformation($"After Exceted Method,{JsonConvert.SerializeObject(context.ReturnValue)}");
            }
        }
    }
}
