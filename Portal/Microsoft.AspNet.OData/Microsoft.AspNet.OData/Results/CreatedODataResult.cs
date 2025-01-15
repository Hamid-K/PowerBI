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
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Results
{
	// Token: 0x020000A1 RID: 161
	public class CreatedODataResult<T> : IHttpActionResult
	{
		// Token: 0x06000564 RID: 1380 RVA: 0x00012ABA File Offset: 0x00010CBA
		public CreatedODataResult(T entity, ApiController controller)
			: this(new NegotiatedContentResult<T>(HttpStatusCode.Created, CreatedODataResult<T>.CheckNull(entity), controller))
		{
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00012AD3 File Offset: 0x00010CD3
		public CreatedODataResult(T entity, IContentNegotiator contentNegotiator, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters, Uri locationHeader)
			: this(new NegotiatedContentResult<T>(HttpStatusCode.Created, CreatedODataResult<T>.CheckNull(entity), contentNegotiator, request, formatters))
		{
			if (locationHeader == null)
			{
				throw Error.ArgumentNull("locationHeader");
			}
			this._locationHeader = locationHeader;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00012B0C File Offset: 0x00010D0C
		private CreatedODataResult(NegotiatedContentResult<T> innerResult)
		{
			this._innerResult = innerResult;
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x00012B1B File Offset: 0x00010D1B
		public T Entity
		{
			get
			{
				return this._innerResult.Content;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00012B28 File Offset: 0x00010D28
		public IContentNegotiator ContentNegotiator
		{
			get
			{
				return this._innerResult.ContentNegotiator;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x00012B35 File Offset: 0x00010D35
		public HttpRequestMessage Request
		{
			get
			{
				return this._innerResult.Request;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00012B42 File Offset: 0x00010D42
		public IEnumerable<MediaTypeFormatter> Formatters
		{
			get
			{
				return this._innerResult.Formatters;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x00012B4F File Offset: 0x00010D4F
		public Uri LocationHeader
		{
			get
			{
				this._locationHeader = this._locationHeader ?? this.GenerateLocationHeader(this.Request);
				return this._locationHeader;
			}
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00012B74 File Offset: 0x00010D74
		public virtual async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			HttpResponseMessage httpResponseMessage = await this.GetInnerActionResult(this.Request).ExecuteAsync(cancellationToken);
			httpResponseMessage.Headers.Location = this.LocationHeader;
			ResultHelpers.AddEntityId(httpResponseMessage, () => this.GenerateEntityId(this.Request));
			ResultHelpers.AddServiceVersion(httpResponseMessage, () => ODataUtils.ODataVersionToString(ResultHelpers.GetODataResponseVersion(this.Request)));
			return httpResponseMessage;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00012BC1 File Offset: 0x00010DC1
		internal IHttpActionResult GetInnerActionResult(HttpRequestMessage request)
		{
			if (RequestPreferenceHelpers.RequestPrefersReturnNoContent(new WebApiRequestHeaders(request.Headers)))
			{
				return new StatusCodeResult(HttpStatusCode.NoContent, request);
			}
			return this._innerResult;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00012BE7 File Offset: 0x00010DE7
		internal Uri GenerateEntityId(HttpRequestMessage request)
		{
			return ResultHelpers.GenerateODataLink(request, this.Entity, true);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00012BFB File Offset: 0x00010DFB
		internal Uri GenerateLocationHeader(HttpRequestMessage request)
		{
			return ResultHelpers.GenerateODataLink(request, this.Entity, false);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00012C0F File Offset: 0x00010E0F
		private static T CheckNull(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			return entity;
		}

		// Token: 0x04000132 RID: 306
		private readonly NegotiatedContentResult<T> _innerResult;

		// Token: 0x04000133 RID: 307
		private Uri _locationHeader;
	}
}
