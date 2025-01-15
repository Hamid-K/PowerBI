using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Results
{
	// Token: 0x020000B6 RID: 182
	public class CreatedNegotiatedContentResult<T> : IHttpActionResult
	{
		// Token: 0x06000462 RID: 1122 RVA: 0x0000C728 File Offset: 0x0000A928
		public CreatedNegotiatedContentResult(Uri location, T content, IContentNegotiator contentNegotiator, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
			: this(location, content, new NegotiatedContentResult<T>.DirectDependencyProvider(contentNegotiator, request, formatters))
		{
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000C73C File Offset: 0x0000A93C
		public CreatedNegotiatedContentResult(Uri location, T content, ApiController controller)
			: this(location, content, new NegotiatedContentResult<T>.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000C74C File Offset: 0x0000A94C
		private CreatedNegotiatedContentResult(Uri location, T content, NegotiatedContentResult<T>.IDependencyProvider dependencies)
		{
			if (location == null)
			{
				throw new ArgumentNullException("location");
			}
			this._location = location;
			this._content = content;
			this._dependencies = dependencies;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0000C77D File Offset: 0x0000A97D
		public Uri Location
		{
			get
			{
				return this._location;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0000C785 File Offset: 0x0000A985
		public T Content
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0000C78D File Offset: 0x0000A98D
		public IContentNegotiator ContentNegotiator
		{
			get
			{
				return this._dependencies.ContentNegotiator;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0000C79A File Offset: 0x0000A99A
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x0000C7A7 File Offset: 0x0000A9A7
		public IEnumerable<MediaTypeFormatter> Formatters
		{
			get
			{
				return this._dependencies.Formatters;
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000C7B4 File Offset: 0x0000A9B4
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(this.Execute());
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000C7C4 File Offset: 0x0000A9C4
		private HttpResponseMessage Execute()
		{
			ContentNegotiationResult contentNegotiationResult = this._dependencies.ContentNegotiator.Negotiate(typeof(T), this._dependencies.Request, this._dependencies.Formatters);
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
			try
			{
				if (contentNegotiationResult == null)
				{
					httpResponseMessage.StatusCode = HttpStatusCode.NotAcceptable;
				}
				else
				{
					httpResponseMessage.StatusCode = HttpStatusCode.Created;
					httpResponseMessage.Headers.Location = this._location;
					httpResponseMessage.Content = new ObjectContent<T>(this._content, contentNegotiationResult.Formatter, contentNegotiationResult.MediaType);
				}
				httpResponseMessage.RequestMessage = this._dependencies.Request;
			}
			catch
			{
				httpResponseMessage.Dispose();
				throw;
			}
			return httpResponseMessage;
		}

		// Token: 0x04000117 RID: 279
		private readonly Uri _location;

		// Token: 0x04000118 RID: 280
		private readonly T _content;

		// Token: 0x04000119 RID: 281
		private readonly NegotiatedContentResult<T>.IDependencyProvider _dependencies;
	}
}
