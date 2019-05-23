using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkFlowTest.Steps;

namespace WorkFlowTest._02
{
    class SimpleDecisionWorkflow : IWorkflow

    {
        public string Id => "Simple Decision Workflow";

        public int Version => 1;

        public void Build(IWorkflowBuilder<object> builder)
        {



            builder.StartWith<HelloWorld>()
                .Then<RandomOutput>(randOutput =>
               {
                   randOutput.If(data => (int)data == 0)
                   .Do(then => then.StartWith<CustomerMessage>()
                                       .Name("Print custom message")
                                       .Input(step => step.Message, data => "Looping back....")
                                   ).Then(randOutput);

                   randOutput.If(data => (int)data == 1)
                     .Do(then => then.StartWith<GoodByeWorld>());
                   

               });
             
                  
                //.Then<RandomOutput>(randomOutput =>
                //{
                //    //如果输入结果是 0
                //    //randomOutput.When(0).Then<CustomerMessage>(cm =>
                //    //{
                //    //    cm.Name("Print custom message");
                //    //    cm.Input(step => step.Message, data => "Looping back....");
                //    //}).Then(randomOutput); //loop back to randomOutput    

                //    //randomOutput.When(1).Then<GoodByeWorld>();  
               
                //});


        
        }
    }
}
