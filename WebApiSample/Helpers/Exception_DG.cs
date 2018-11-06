using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSample.Helpers
{
    public class Exception_DG : ApplicationException
    {
        /// <summary>
        /// Exception_DG
        /// </summary>
        /// <param name="message">Error Message</param>
        public Exception_DG(string message) : base(message) { }

        public Exception_DG(string message, int errorCode) : base(message) { this.ErrorCode = errorCode; }

        public Exception_DG(string message, int errorCode, int errorLevel) : base(message) { this.ErrorCode = errorCode; this.ErrorLevel = errorLevel; }

        /// <summary>
        /// Exception_DG
        /// </summary>
        /// <param name="arguments">Error Arguments</param>
        /// <param name="message">Error Message</param>
        public Exception_DG(string arguments, string message) : base(message) { this.Arguments = arguments; }

        public Exception_DG(string arguments, string message, int errorCode) : base(message) { this.Arguments = arguments; this.ErrorCode = errorCode; }

        public Exception_DG(string arguments, string message, int errorCode, int errorLevel) : base(message) { this.Arguments = arguments; this.ErrorCode = errorCode; this.ErrorLevel = errorLevel; }


        /// <summary>
        /// Error Arguments
        /// </summary>
        public string Arguments { get; set; }

        /// <summary>
        /// Error Message
        /// </summary>
        public override string Message
        {
            get
            {
                return base.Message;
            }
        }

        /// <summary>
        /// Error Code
        /// </summary>
        public int ErrorCode { get; set; } = 0;

        /// <summary>
        /// Error Level
        /// </summary>
        public int ErrorLevel { get; set; } = 0;
    }
}