using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDBDemo.Wrappers
{
    public class OperationResult<T>
    {
        public OperationResult(T result)
        {
            Result = result;
            Status = true;
        }
        public OperationResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
            Status = false;
        }
        public OperationResult(string errorMessage, bool status)
        {
            ErrorMessage = errorMessage;
            Status = status;
        }
        public OperationResult(T result,bool status)
        {
            Result = result;
            Status = status;
        }
        public OperationResult(T result, bool status,string errorMessage)
        {
            Result = result;
            Status = status;
            ErrorMessage = errorMessage;
        }
        public T Result { get; set; }
        public bool Status { get; set; }
        public string ErrorMessage { get; set; }
    }
}
