using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace WebApiSample.Helpers
{
    public abstract class Return_Helper
    {
        public static object IsSuccess_Msg_Data_HttpCode(bool isSuccess, string msg, dynamic data, HttpStatusCode httpCode = HttpStatusCode.OK)
        {
            return new
            {
                isSuccess = isSuccess,
                msg = msg,
                httpCode = httpCode,
                data = data

            };
        }
        public static object Success_Msg_Data_DCount_HttpCode(string msg, dynamic data = null, int dataCount = 0, HttpStatusCode httpCode = HttpStatusCode.OK)
        {
            return new { isSuccess = true, msg = msg, httpCode = httpCode, data = data, dataCount = dataCount };
        }

        public static object Error_Msg_Ecode_Elevel_HttpCode(string msg, int errorCode = 0, int errorLevel = 0, HttpStatusCode httpCode = HttpStatusCode.InternalServerError)
        {
            return new { isSuccess = false, msg = msg, httpCode = httpCode, errorCode = errorCode, errorLevel = errorLevel };
        }
    }
}