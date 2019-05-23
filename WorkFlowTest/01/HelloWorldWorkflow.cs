using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using System.Threading;
using WorkFlowTest.Steps;

namespace WorkFlowTest._01
{
    class HelloWorldWorkflow : IWorkflow
    {
        public string Id => "HelloWorld";

        public int Version => 1;

        public void Build(IWorkflowBuilder<object> builder)
        {
            builder.StartWith<HelloWorld>().
                Then(context =>
                {
                   Thread.Sleep(2000);

                    Console.WriteLine("Goodbye World!");
                    return ExecutionResult.Next();
                });
           

        }
    }
}
