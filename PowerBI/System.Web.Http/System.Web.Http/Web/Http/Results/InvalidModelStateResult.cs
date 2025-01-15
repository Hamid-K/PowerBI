using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace System.Web.Http.Results
{
	// Token: 0x020000B8 RID: 184
	public class InvalidModelStateResult : IHttpActionResult
	{
		// Token: 0x06000474 RID: 1140 RVA: 0x0000C91E File Offset: 0x0000AB1E
		public InvalidModelStateResult(ModelStateDictionary modelState, bool includeErrorDetail, IContentNegotiator contentNegotiator, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
			: this(modelState, new ExceptionResult.DirectDependencyProvider(includeErrorDetail, contentNegotiator, request, formatters))
		{
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000C932 File Offset: 0x0000AB32
		public InvalidModelStateResult(ModelStateDictionary modelState, ApiController controller)
			: this(modelState, new ExceptionResult.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000C941 File Offset: 0x0000AB41
		private InvalidModelStateResult(ModelStateDictionary modelState, ExceptionResult.IDependencyProvider dependencies)
		{
			if (modelState == null)
			{
				throw new ArgumentNullException("modelState");
			}
			this._modelState = modelState;
			this._dependencies = dependencies;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0000C965 File Offset: 0x0000AB65
		public ModelStateDictionary ModelState
		{
			get
			{
				return this._modelState;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0000C96D File Offset: 0x0000AB6D
		public bool IncludeErrorDetail
		{
			get
			{
				return this._dependencies.IncludeErrorDetail;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0000C97A File Offset: 0x0000AB7A
		public IContentNegotiator ContentNegotiator
		{
			get
			{
				return this._dependencies.ContentNegotiator;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x0000C987 File Offset: 0x0000AB87
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0000C994 File Offset: 0x0000AB94
		public IEnumerable<MediaTypeFormatter> Formatters
		{
			get
			{
				return this._dependencies.Formatters;
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000C9A1 File Offset: 0x0000ABA1
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(this.Execute());
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0000C9B0 File Offset: 0x0000ABB0
		private HttpResponseMessage Execute()
		{
			HttpError httpError = new HttpError(this._modelState, this._dependencies.IncludeErrorDetail);
			return NegotiatedContentResult<HttpError>.Execute(HttpStatusCode.BadRequest, httpError, this._dependencies.ContentNegotiator, this._dependencies.Request, this._dependencies.Formatters);
		}

		// Token: 0x0400011C RID: 284
		private readonly ModelStateDictionary _modelState;

		// Token: 0x0400011D RID: 285
		private readonly ExceptionResult.IDependencyProvider _dependencies;
	}
}
