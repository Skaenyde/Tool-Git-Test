using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Common.Models
{
    public class GenericResponse<T>
    {
        public GenericResponse(T value)
        {
            Result = value;
        }

        public bool Success  { get; set; }

        public string Message { get; set; }

        public T Result { get; set; } 

    }
}
