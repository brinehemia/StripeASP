using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stripe_Test.Models
{
    public class ApiResponse<T>
    {
        public ApiResponse()
        {

        }
        public ApiResponse(T data)
        {
            Data = data;
        }
        public T Data { get; set; }
        public string Status { get; set; }
    }
}
