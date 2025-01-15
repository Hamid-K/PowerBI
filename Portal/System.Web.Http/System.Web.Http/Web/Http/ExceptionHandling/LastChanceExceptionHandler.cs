using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Results;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x020000E2 RID: 226
	internal class LastChanceExceptionHandler : IExceptionHandler
	{
		// Token: 0x060005CA RID: 1482 RVA: 0x0000E857 File Offset: 0x0000CA57
		public LastChanceExceptionHandler(IExceptionHandler innerHandler)
		{
			if (innerHandler == null)
			{
				throw new ArgumentNullException("innerHandler");
			}
			this._innerHandler = innerHandler;
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x0000E874 File Offset: 0x0000CA74
		public IExceptionHandler InnerHandler
		{
			get
			{
				return this._innerHandler;
			}
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0000E87C File Offset: 0x0000CA7C
		public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
		{
			if (context != null)
			{
				ExceptionContext exceptionContext = context.ExceptionContext;
				ExceptionContextCatchBlock catchBlock = exceptionContext.CatchBlock;
				if (catchBlock != null && catchBlock.IsTopLevel)
				{
					context.Result = LastChanceExceptionHandler.CreateDefaultLastChanceResult(exceptionContext);
				}
			}
			return this._innerHandler.HandleAsync(context, cancellationToken);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0000E8C0 File Offset: 0x0000CAC0
		private static IHttpActionResult CreateDefaultLastChanceResult(ExceptionContext context)
		{
			Exception exception = context.Exception;
			if (exception == null)
			{
				return null;
			}
			HttpRequestMessage request = context.Request;
			if (request == null)
			{
				return null;
			}
			HttpRequestContext requestContext = context.RequestContext;
			if (requestContext == null)
			{
				return null;
			}
			HttpConfiguration configuration = requestContext.Configuration;
			if (configuration == null)
			{
				return null;
			}
			IContentNegotiator contentNegotiator = configuration.Services.GetContentNegotiator();
			if (contentNegotiator == null)
			{
				return null;
			}
			IEnumerable<MediaTypeFormatter> formatters = configuration.Formatters;
			return new ExceptionResult(exception, requestContext.IncludeErrorDetail, contentNegotiator, request, formatters);
		}

		// Token: 0x04000150 RID: 336
		private readonly IExceptionHandler _innerHandler;
	}
}
