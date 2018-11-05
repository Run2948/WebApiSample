/* ==============================================================================
* 命名空间：MyApiClient
* 类 名 称：MyMessageHandler
* 创 建 者：Qing
* 创建时间：2018-11-05 13:50:04
* CLR 版本：4.0.30319.42000
* 保存的文件名：MyMessageHandler
* 文件版本：V1.0.0.0
*
* 功能描述：N/A 
*
* 修改历史：
*
*
* ==============================================================================
*         CopyRight @ 班纳工作室 2018. All rights reserved
* ==============================================================================*/



using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyApiClient
{
    public class MyMessageHandler:DelegatingHandler
    {
        /// <summary>
        /// api请求计数器
        /// </summary>
        private static int _counter = 0;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("x-custom-header", $"{++_counter}");
            return base.SendAsync(request, cancellationToken);
        }
    }
}
