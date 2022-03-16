using System;
using System.Collections.Generic;
using System.Net;

namespace Common.Helper
{
    public class ApiResponse<T>
    {
        public ApiResponse()
        {
            ErrorMessages = new List<string>();
        }
        public bool Success { get; set; }
        public List<string> ErrorMessages { get; set; }
        public int ReferenceId { get; set; }

        public Exception Exception { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public T Content { get; set; }
    }
    public class ApiResponse
    {
        public ApiResponse()
        {
            ErrorMessages = new List<string>();
        }
        public bool Success { get; set; }
        public List<string> ErrorMessages { get; set; }
        public int ReferenceId { get; set; }

        public Exception Exception { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
