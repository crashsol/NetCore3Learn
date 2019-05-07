using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSample.Models
{
    public class Person:ISoftDelete
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsDelete { get; set; }
    }
}
