using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.AspNet.OData.Adapters;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Results
{
	// Token: 0x020000A3 RID: 163
	public class UpdatedODataResult<T> : IHttpActionResult
	{
		// Token: 0x0600057A RID: 1402 RVA: 0x00012F9D File Offset: 0x0001119D
		public UpdatedODataResult(T entity, ApiController controller)
			: this(new NegotiatedContentResult<T>(HttpStatusCode.OK, UpdatedODataResult<T>.CheckNull(entity), controller))
		{
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00012FB6 File Offset: 0x000111B6
		public UpdatedODataResult(T entity, IContentNegotiator contentNegotiator, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
			: this(new NegotiatedContentResult<T>(HttpStatusCode.OK, UpdatedODataResult<T>.CheckNull(entity), contentNegotiator, request, formatters))
		{
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00012FD2 File Offset: 0x000111D2
		private UpdatedODataResult(NegotiatedContentResult<T> innerResult)
		{
			this._innerResult = innerResult;
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x00012FE1 File Offset: 0x000111E1
		public T Entity
		{
			get
			{
				return this._innerResult.Content;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x00012FEE File Offset: 0x000111EE
		public IContentNegotiator ContentNegotiator
		{
			get
			{
				return this._innerResult.ContentNegotiator;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x00012FFB File Offset: 0x000111FB
		public HttpRequestMessage Request
		{
			get
			{
				return this._innerResult.Request;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x00013008 File Offset: 0x00011208
		public IEnumerable<MediaTypeFormatter> Formatters
		{
			get
			{
				return this._innerResult.Formatters;
			}
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00013018 File Offset: 0x00011218
		public virtual async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			HttpResponseMessage httpResponseMessage = await this.GetInnerActionResult().ExecuteAsync(cancellationToken);
			ResultHelpers.AddServiceVersion(httpResponseMessage, () => ODataUtils.ODataVersionToString(ResultHelpers.GetODataResponseVersion(this.Request)));
			return httpResponseMessage;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00013065 File Offset: 0x00011265
		internal IHttpActionResult GetInnerActionResult()
		{
			if (RequestPreferenceHelpers.RequestPrefersReturnContent(new WebApiRequestHeaders(this._innerResult.Request.Headers)))
			{
				return this._innerResult;
			}
			return new StatusCodeResult(HttpStatusCode.NoContent, this._innerResult.Request);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0001309F File Offset: 0x0001129F
		private static T CheckNull(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			return entity;
		}

		// Token: 0x04000135 RID: 309
		private readonly NegotiatedContentResult<T> _innerResult;
	}
}
