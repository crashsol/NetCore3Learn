using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkFlowTest._03
{
    public class AddNumbers : StepBody
    {
        public int Input1 { get; set; }

        public int Input2 { get; set; }

        public int OutPut { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            OutPut = (Input1 + Input2);
            return ExecutionResult.Next();
        }
    }
}
