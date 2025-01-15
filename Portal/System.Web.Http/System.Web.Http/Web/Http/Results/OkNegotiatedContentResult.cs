using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Results
{
	// Token: 0x020000B7 RID: 183
	public class OkNegotiatedContentResult<T> : IHttpActionResult
	{
		// Token: 0x0600046C RID: 1132 RVA: 0x0000C880 File Offset: 0x0000AA80
		public OkNegotiatedContentResult(T content, IContentNegotiator contentNegotiator, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
			: this(content, new NegotiatedContentResult<T>.DirectDependencyProvider(contentNegotiator, request, formatters))
		{
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000C892 File Offset: 0x0000AA92
		public OkNegotiatedContentResult(T content, ApiController controller)
			: this(content, new NegotiatedContentResult<T>.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000C8A1 File Offset: 0x0000AAA1
		private OkNegotiatedContentResult(T content, NegotiatedContentResult<T>.IDependencyProvider dependencies)
		{
			this._content = content;
			this._dependencies = dependencies;
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0000C8B7 File Offset: 0x0000AAB7
		public T Content
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x0000C8BF File Offset: 0x0000AABF
		public IContentNegotiator ContentNegotiator
		{
			get
			{
				return this._dependencies.ContentNegotiator;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0000C8CC File Offset: 0x0000AACC
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0000C8D9 File Offset: 0x0000AAD9
		public IEnumerable<MediaTypeFormatter> Formatters
		{
			get
			{
				return this._dependencies.Formatters;
			}
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000C8E6 File Offset: 0x0000AAE6
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(NegotiatedContentResult<T>.Execute(HttpStatusCode.OK, this._content, this._dependencies.ContentNegotiator, this._dependencies.Request, this._dependencies.Formatters));
		}

		// Token: 0x0400011A RID: 282
		private readonly T _content;

		// Token: 0x0400011B RID: 283
		private readonly NegotiatedContentResult<T>.IDependencyProvider _dependencies;
	}
}
