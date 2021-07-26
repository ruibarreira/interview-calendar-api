using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DataTransferObject
    {
        #nullable enable
        public object? data { get; set; }
        public string? message { get; set; }
    }
}
