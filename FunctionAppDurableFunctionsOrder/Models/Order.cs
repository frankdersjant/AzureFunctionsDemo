using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionAppDurableFunctionsOrder.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public decimal Price { get; set; }
    }
}
