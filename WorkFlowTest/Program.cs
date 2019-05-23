using System;
using Microsoft.Extensions.DependencyInjection;
using WorkflowCore.Interface;
using WorkFlowTest._01;
using WorkFlowTest._02;

namespace WorkFlowTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Work Flow!");
            IServiceCollection services = new ServiceCollection();         

            services.AddLogging();
            //Use the AddWorkflow extension method for IServiceCollection to configure the workflow host upon startup of your application. 
            //By default, it is configured with MemoryPersistenceProvider
            //and SingleNodeConcurrencyProvider for testing purposes.
            //You can also configure a DB persistence provider at this point.
            services.AddWorkflow();
            var serviceProvider = services.BuildServiceProvider();
            var host = serviceProvider.GetRequiredService<IWorkflowHost>();

            // 注册工作流
            host.RegisterWorkflow<HelloWorldWorkflow>();

            host.RegisterWorkflow<SimpleDecisionWorkflow>();
            // 启动工作引擎
            host.Start();
            // 开始执行HelloWorld工作流
            //host.StartWorkflow("HelloWorld", 1, null);           
            host.StartWorkflow("Simple Decision Workflow");
         

            Console.ReadLine();
            host.Stop();
        }
    }
}
