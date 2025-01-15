using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Properties;
using System.Web.Http.Results;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x020000D7 RID: 215
	internal class DefaultExceptionHandler : IExceptionHandler
	{
		// Token: 0x06000591 RID: 1425 RVA: 0x0000E33D File Offset: 0x0000C53D
		public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
		{
			DefaultExceptionHandler.Handle(context);
			return TaskHelpers.Completed();
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0000E34C File Offset: 0x0000C54C
		private static void Handle(ExceptionHandlerContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			ExceptionContext exceptionContext = context.ExceptionContext;
			Exception exception = exceptionContext.Exception;
			HttpRequestMessage request = exceptionContext.Request;
			if (request == null)
			{
				throw new ArgumentException(Error.Format(SRResources.TypePropertyMustNotBeNull, new object[]
				{
					typeof(ExceptionContext).Name,
					"Request"
				}), "context");
			}
			if (exceptionContext.CatchBlock == ExceptionCatchBlocks.IExceptionFilter)
			{
				return;
			}
			context.Result = new ResponseMessageResult(request.CreateErrorResponse(HttpStatusCode.InternalServerError, exception));
		}
	}
}
