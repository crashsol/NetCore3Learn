using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkFlowTest.Steps;

namespace WorkFlowTest._03
{
    class PassingDataWorkflow2 : IWorkflow<Dictionary<string, int>>
    {
        public string Id => nameof(PassingDataWorkflow2);

        public int Version => 1;

        public void Build(IWorkflowBuilder<Dictionary<string, int>> builder)
        {

            builder
                   .StartWith(context =>
                   {
                       Console.WriteLine("Starting workflow...");
                       return ExecutionResult.Next();
                   })
                   .Then<AddNumbers>()
                       .Input(step => step.Input1, data => data["Value1"])
                       .Input(step => step.Input2, data => data["Value2"])
                       .Output((step, data) => data["Value3"] = step.OutPut)
                   .Then<CustomerMessage>()
                       .Name("Print custom message")
                       .Input(step => step.Message, data => "The answer is " + data["Value3"].ToString())
                   .Then(context =>
                   {
                       Console.WriteLine("Workflow complete");
                       return ExecutionResult.Next();
                   });
        }
    }
}
