﻿using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkFlowTest.Steps;

namespace WorkFlowTest._03
{
    class PassingDataWorkflow : IWorkflow<MyDataClass>
    {
        public string Id => "PassingDataWorkflow";

        public int Version => 1;

        public void Build(IWorkflowBuilder<MyDataClass> builder)
        {
            builder.StartWith(context =>
            {
                Console.WriteLine("PassworkFlow starting..");
                return ExecutionResult.Next();
            }).Then<AddNumbers>()
                         .Input(step => step.Input1, data => data.Value1)
                         .Input(step => step.Input2, data => data.Value2)
                         .Output(data => data.Value3, step => step.OutPut)
              .Then<CustomerMessage>().Input(step => step.Message, data => $"答案是:{data.Value3}")
              .Then(context =>
              {
                  Console.WriteLine("PassworkFlow end");
                  return ExecutionResult.Next();
              });
        }
    }
}
