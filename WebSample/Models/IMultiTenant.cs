﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSample.Models
{
   public interface IMultiTenant
    {
        Guid? TenantId { get; set; }
    }
}
