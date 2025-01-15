using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Results
{
	// Token: 0x020000AE RID: 174
	public class BadRequestErrorMessageResult : IHttpActionResult
	{
		// Token: 0x0600042A RID: 1066 RVA: 0x0000C140 File Offset: 0x0000A340
		public BadRequestErrorMessageResult(string message, IContentNegotiator contentNegotiator, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
			: this(message, new NegotiatedContentResult<HttpError>.DirectDependencyProvider(contentNegotiator, request, formatters))
		{
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000C152 File Offset: 0x0000A352
		public BadRequestErrorMessageResult(string message, ApiController controller)
			: this(message, new NegotiatedContentResult<HttpError>.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000C161 File Offset: 0x0000A361
		private BadRequestErrorMessageResult(string message, NegotiatedContentResult<HttpError>.IDependencyProvider dependencies)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			this._message = message;
			this._dependencies = dependencies;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x0000C185 File Offset: 0x0000A385
		public string Message
		{
			get
			{
				return this._message;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x0000C18D File Offset: 0x0000A38D
		public IContentNegotiator ContentNegotiator
		{
			get
			{
				return this._dependencies.ContentNegotiator;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0000C19A File Offset: 0x0000A39A
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x0000C1A7 File Offset: 0x0000A3A7
		public IEnumerable<MediaTypeFormatter> Formatters
		{
			get
			{
				return this._dependencies.Formatters;
			}
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000C1B4 File Offset: 0x0000A3B4
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(this.Execute());
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000C1C4 File Offset: 0x0000A3C4
		private HttpResponseMessage Execute()
		{
			HttpError httpError = new HttpError(this._message);
			return NegotiatedContentResult<HttpError>.Execute(HttpStatusCode.BadRequest, httpError, this._dependencies.ContentNegotiator, this._dependencies.Request, this._dependencies.Formatters);
		}

		// Token: 0x04000109 RID: 265
		private readonly string _message;

		// Token: 0x0400010A RID: 266
		private readonly NegotiatedContentResult<HttpError>.IDependencyProvider _dependencies;
	}
}
