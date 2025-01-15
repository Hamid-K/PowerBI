using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Properties;

namespace System.Web.Http.Results
{
	// Token: 0x020000B2 RID: 178
	public class ExceptionResult : IHttpActionResult
	{
		// Token: 0x06000449 RID: 1097 RVA: 0x0000C54C File Offset: 0x0000A74C
		public ExceptionResult(Exception exception, bool includeErrorDetail, IContentNegotiator contentNegotiator, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
			: this(exception, new ExceptionResult.DirectDependencyProvider(includeErrorDetail, contentNegotiator, request, formatters))
		{
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000C560 File Offset: 0x0000A760
		public ExceptionResult(Exception exception, ApiController controller)
			: this(exception, new ExceptionResult.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000C56F File Offset: 0x0000A76F
		private ExceptionResult(Exception exception, ExceptionResult.IDependencyProvider dependencies)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			this._exception = exception;
			this._dependencies = dependencies;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0000C593 File Offset: 0x0000A793
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x0000C59B File Offset: 0x0000A79B
		public bool IncludeErrorDetail
		{
			get
			{
				return this._dependencies.IncludeErrorDetail;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0000C5A8 File Offset: 0x0000A7A8
		public IContentNegotiator ContentNegotiator
		{
			get
			{
				return this._dependencies.ContentNegotiator;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0000C5B5 File Offset: 0x0000A7B5
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0000C5C2 File Offset: 0x0000A7C2
		public IEnumerable<MediaTypeFormatter> Formatters
		{
			get
			{
				return this._dependencies.Formatters;
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000C5CF File Offset: 0x0000A7CF
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(this.Execute());
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000C5DC File Offset: 0x0000A7DC
		private HttpResponseMessage Execute()
		{
			HttpError httpError = new HttpError(this._exception, this._dependencies.IncludeErrorDetail);
			return NegotiatedContentResult<HttpError>.Execute(HttpStatusCode.InternalServerError, httpError, this._dependencies.ContentNegotiator, this._dependencies.Request, this._dependencies.Formatters);
		}

		// Token: 0x04000112 RID: 274
		private readonly Exception _exception;

		// Token: 0x04000113 RID: 275
		private readonly ExceptionResult.IDependencyProvider _dependencies;

		// Token: 0x020001D2 RID: 466
		internal interface IDependencyProvider
		{
			// Token: 0x17000306 RID: 774
			// (get) Token: 0x06000B25 RID: 2853
			bool IncludeErrorDetail { get; }

			// Token: 0x17000307 RID: 775
			// (get) Token: 0x06000B26 RID: 2854
			IContentNegotiator ContentNegotiator { get; }

			// Token: 0x17000308 RID: 776
			// (get) Token: 0x06000B27 RID: 2855
			HttpRequestMessage Request { get; }

			// Token: 0x17000309 RID: 777
			// (get) Token: 0x06000B28 RID: 2856
			IEnumerable<MediaTypeFormatter> Formatters { get; }
		}

		// Token: 0x020001D3 RID: 467
		internal sealed class DirectDependencyProvider : ExceptionResult.IDependencyProvider
		{
			// Token: 0x06000B29 RID: 2857 RVA: 0x0001CA08 File Offset: 0x0001AC08
			public DirectDependencyProvider(bool includeErrorDetail, IContentNegotiator contentNegotiator, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
			{
				if (contentNegotiator == null)
				{
					throw new ArgumentNullException("contentNegotiator");
				}
				if (request == null)
				{
					throw new ArgumentNullException("request");
				}
				if (formatters == null)
				{
					throw new ArgumentNullException("formatters");
				}
				this._includeErrorDetail = includeErrorDetail;
				this._contentNegotiator = contentNegotiator;
				this._request = request;
				this._formatters = formatters;
			}

			// Token: 0x1700030A RID: 778
			// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0001CA63 File Offset: 0x0001AC63
			public bool IncludeErrorDetail
			{
				get
				{
					return this._includeErrorDetail;
				}
			}

			// Token: 0x1700030B RID: 779
			// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0001CA6B File Offset: 0x0001AC6B
			public IContentNegotiator ContentNegotiator
			{
				get
				{
					return this._contentNegotiator;
				}
			}

			// Token: 0x1700030C RID: 780
			// (get) Token: 0x06000B2C RID: 2860 RVA: 0x0001CA73 File Offset: 0x0001AC73
			public HttpRequestMessage Request
			{
				get
				{
					return this._request;
				}
			}

			// Token: 0x1700030D RID: 781
			// (get) Token: 0x06000B2D RID: 2861 RVA: 0x0001CA7B File Offset: 0x0001AC7B
			public IEnumerable<MediaTypeFormatter> Formatters
			{
				get
				{
					return this._formatters;
				}
			}

			// Token: 0x0400038D RID: 909
			private readonly bool _includeErrorDetail;

			// Token: 0x0400038E RID: 910
			private readonly IContentNegotiator _contentNegotiator;

			// Token: 0x0400038F RID: 911
			private readonly HttpRequestMessage _request;

			// Token: 0x04000390 RID: 912
			private readonly IEnumerable<MediaTypeFormatter> _formatters;
		}

		// Token: 0x020001D4 RID: 468
		internal sealed class ApiControllerDependencyProvider : ExceptionResult.IDependencyProvider
		{
			// Token: 0x06000B2E RID: 2862 RVA: 0x0001CA83 File Offset: 0x0001AC83
			public ApiControllerDependencyProvider(ApiController controller)
			{
				if (controller == null)
				{
					throw new ArgumentNullException("controller");
				}
				this._controller = controller;
			}

			// Token: 0x1700030E RID: 782
			// (get) Token: 0x06000B2F RID: 2863 RVA: 0x0001CAA0 File Offset: 0x0001ACA0
			public bool IncludeErrorDetail
			{
				get
				{
					this.EnsureResolved();
					return this._resolvedDependencies.IncludeErrorDetail;
				}
			}

			// Token: 0x1700030F RID: 783
			// (get) Token: 0x06000B30 RID: 2864 RVA: 0x0001CAB3 File Offset: 0x0001ACB3
			public IContentNegotiator ContentNegotiator
			{
				get
				{
					this.EnsureResolved();
					return this._resolvedDependencies.ContentNegotiator;
				}
			}

			// Token: 0x17000310 RID: 784
			// (get) Token: 0x06000B31 RID: 2865 RVA: 0x0001CAC6 File Offset: 0x0001ACC6
			public HttpRequestMessage Request
			{
				get
				{
					this.EnsureResolved();
					return this._resolvedDependencies.Request;
				}
			}

			// Token: 0x17000311 RID: 785
			// (get) Token: 0x06000B32 RID: 2866 RVA: 0x0001CAD9 File Offset: 0x0001ACD9
			public IEnumerable<MediaTypeFormatter> Formatters
			{
				get
				{
					this.EnsureResolved();
					return this._resolvedDependencies.Formatters;
				}
			}

			// Token: 0x06000B33 RID: 2867 RVA: 0x0001CAEC File Offset: 0x0001ACEC
			private void EnsureResolved()
			{
				if (this._resolvedDependencies == null)
				{
					bool includeErrorDetail = this._controller.RequestContext.IncludeErrorDetail;
					HttpConfiguration configuration = this._controller.Configuration;
					if (configuration == null)
					{
						throw new InvalidOperationException(SRResources.HttpControllerContext_ConfigurationMustNotBeNull);
					}
					IContentNegotiator contentNegotiator = configuration.Services.GetContentNegotiator();
					if (contentNegotiator == null)
					{
						throw new InvalidOperationException(Error.Format(SRResources.HttpRequestMessageExtensions_NoContentNegotiator, new object[] { typeof(IContentNegotiator) }));
					}
					HttpRequestMessage request = this._controller.Request;
					if (request == null)
					{
						throw new InvalidOperationException(SRResources.ApiController_RequestMustNotBeNull);
					}
					IEnumerable<MediaTypeFormatter> formatters = configuration.Formatters;
					this._resolvedDependencies = new ExceptionResult.DirectDependencyProvider(includeErrorDetail, contentNegotiator, request, formatters);
				}
			}

			// Token: 0x04000391 RID: 913
			private readonly ApiController _controller;

			// Token: 0x04000392 RID: 914
			private ExceptionResult.IDependencyProvider _resolvedDependencies;
		}
	}
}
