using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Properties;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x020000D5 RID: 213
	public static class ExceptionHandlerExtensions
	{
		// Token: 0x0600058A RID: 1418 RVA: 0x0000E1F0 File Offset: 0x0000C3F0
		public static Task<HttpResponseMessage> HandleAsync(this IExceptionHandler handler, ExceptionContext context, CancellationToken cancellationToken)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			ExceptionHandlerContext exceptionHandlerContext = new ExceptionHandlerContext(context);
			return ExceptionHandlerExtensions.HandleAsyncCore(handler, exceptionHandlerContext, cancellationToken);
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0000E228 File Offset: 0x0000C428
		private static async Task<HttpResponseMessage> HandleAsyncCore(IExceptionHandler handler, ExceptionHandlerContext context, CancellationToken cancellationToken)
		{
			await handler.HandleAsync(context, cancellationToken);
			IHttpActionResult result = context.Result;
			HttpResponseMessage httpResponseMessage;
			if (result == null)
			{
				httpResponseMessage = null;
			}
			else
			{
				HttpResponseMessage httpResponseMessage2 = await result.ExecuteAsync(cancellationToken);
				if (httpResponseMessage2 == null)
				{
					throw new InvalidOperationException(Error.Format(SRResources.TypeMethodMustNotReturnNull, new object[]
					{
						typeof(IHttpActionResult).Name,
						"ExecuteAsync"
					}));
				}
				httpResponseMessage = httpResponseMessage2;
			}
			return httpResponseMessage;
		}
	}
}
