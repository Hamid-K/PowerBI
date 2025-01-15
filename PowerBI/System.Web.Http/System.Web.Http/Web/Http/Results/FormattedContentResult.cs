using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Results
{
	// Token: 0x020000B9 RID: 185
	public class FormattedContentResult<T> : IHttpActionResult
	{
		// Token: 0x0600047E RID: 1150 RVA: 0x0000CA00 File Offset: 0x0000AC00
		public FormattedContentResult(HttpStatusCode statusCode, T content, MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType, HttpRequestMessage request)
			: this(statusCode, content, formatter, mediaType, new StatusCodeResult.DirectDependencyProvider(request))
		{
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000CA14 File Offset: 0x0000AC14
		public FormattedContentResult(HttpStatusCode statusCode, T content, MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType, ApiController controller)
			: this(statusCode, content, formatter, mediaType, new StatusCodeResult.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000CA28 File Offset: 0x0000AC28
		private FormattedContentResult(HttpStatusCode statusCode, T content, MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType, StatusCodeResult.IDependencyProvider dependencies)
		{
			if (formatter == null)
			{
				throw new ArgumentNullException("formatter");
			}
			this._statusCode = statusCode;
			this._content = content;
			this._formatter = formatter;
			this._mediaType = mediaType;
			this._dependencies = dependencies;
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0000CA63 File Offset: 0x0000AC63
		public HttpStatusCode StatusCode
		{
			get
			{
				return this._statusCode;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0000CA6B File Offset: 0x0000AC6B
		public T Content
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0000CA73 File Offset: 0x0000AC73
		public MediaTypeFormatter Formatter
		{
			get
			{
				return this._formatter;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0000CA7B File Offset: 0x0000AC7B
		public MediaTypeHeaderValue MediaType
		{
			get
			{
				return this._mediaType;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0000CA83 File Offset: 0x0000AC83
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000CA90 File Offset: 0x0000AC90
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(this.Execute());
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000CA9D File Offset: 0x0000AC9D
		private HttpResponseMessage Execute()
		{
			return FormattedContentResult<T>.Execute(this._statusCode, this._content, this._formatter, this._mediaType, this._dependencies.Request);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000CAC8 File Offset: 0x0000ACC8
		internal static HttpResponseMessage Execute(HttpStatusCode statusCode, T content, MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType, HttpRequestMessage request)
		{
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage(statusCode);
			try
			{
				httpResponseMessage.Content = new ObjectContent<T>(content, formatter, mediaType);
				httpResponseMessage.RequestMessage = request;
			}
			catch
			{
				httpResponseMessage.Dispose();
				throw;
			}
			return httpResponseMessage;
		}

		// Token: 0x0400011E RID: 286
		private readonly HttpStatusCode _statusCode;

		// Token: 0x0400011F RID: 287
		private readonly T _content;

		// Token: 0x04000120 RID: 288
		private readonly MediaTypeFormatter _formatter;

		// Token: 0x04000121 RID: 289
		private readonly MediaTypeHeaderValue _mediaType;

		// Token: 0x04000122 RID: 290
		private readonly StatusCodeResult.IDependencyProvider _dependencies;
	}
}
