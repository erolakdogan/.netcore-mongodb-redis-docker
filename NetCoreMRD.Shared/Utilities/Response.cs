using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetCoreMRD.Shared.Utilities
{
    public class Response
    {
        public string Message { get; set; }
    }

    public class ErrorResponse
    {
        public string Message { get; set; }
    }
}
