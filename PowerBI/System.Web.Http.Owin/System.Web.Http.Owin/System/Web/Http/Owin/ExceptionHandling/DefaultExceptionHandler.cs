using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Owin.Properties;
using System.Web.Http.Results;

namespace System.Web.Http.Owin.ExceptionHandling
{
	// Token: 0x02000016 RID: 22
	internal class DefaultExceptionHandler : IExceptionHandler
	{
		// Token: 0x060000BC RID: 188 RVA: 0x00003916 File Offset: 0x00001B16
		public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
		{
			DefaultExceptionHandler.Handle(context);
			return TaskHelpers.Completed();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003924 File Offset: 0x00001B24
		private static void Handle(ExceptionHandlerContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			ExceptionContext exceptionContext = context.ExceptionContext;
			HttpRequestMessage request = exceptionContext.Request;
			if (request == null)
			{
				throw new ArgumentException(Error.Format(OwinResources.TypePropertyMustNotBeNull, new object[]
				{
					typeof(ExceptionContext).Name,
					"Request"
				}), "context");
			}
			context.Result = new ResponseMessageResult(request.CreateErrorResponse(HttpStatusCode.InternalServerError, exceptionContext.Exception));
		}
	}
}
