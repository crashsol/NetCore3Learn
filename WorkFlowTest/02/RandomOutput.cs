using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkFlowTest._02
{
    class RandomOutput : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Random rnd = new Random();
            int rndValue = rnd.Next(2);
            Console.WriteLine($"Generated random value: {rndValue}");

            //  下一个流程传递数据
            return ExecutionResult.Outcome(rndValue);
        }
    }
}
