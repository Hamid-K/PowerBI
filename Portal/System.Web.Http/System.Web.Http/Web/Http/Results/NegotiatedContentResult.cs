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
	// Token: 0x020000BA RID: 186
	public class NegotiatedContentResult<T> : IHttpActionResult
	{
		// Token: 0x06000489 RID: 1161 RVA: 0x0000CB10 File Offset: 0x0000AD10
		public NegotiatedContentResult(HttpStatusCode statusCode, T content, IContentNegotiator contentNegotiator, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
			: this(statusCode, content, new NegotiatedContentResult<T>.DirectDependencyProvider(contentNegotiator, request, formatters))
		{
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000CB24 File Offset: 0x0000AD24
		public NegotiatedContentResult(HttpStatusCode statusCode, T content, ApiController controller)
			: this(statusCode, content, new NegotiatedContentResult<T>.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000CB34 File Offset: 0x0000AD34
		private NegotiatedContentResult(HttpStatusCode statusCode, T content, NegotiatedContentResult<T>.IDependencyProvider dependencies)
		{
			this._statusCode = statusCode;
			this._content = content;
			this._dependencies = dependencies;
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x0000CB51 File Offset: 0x0000AD51
		public HttpStatusCode StatusCode
		{
			get
			{
				return this._statusCode;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x0000CB59 File Offset: 0x0000AD59
		public T Content
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x0000CB61 File Offset: 0x0000AD61
		public IContentNegotiator ContentNegotiator
		{
			get
			{
				return this._dependencies.ContentNegotiator;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0000CB6E File Offset: 0x0000AD6E
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x0000CB7B File Offset: 0x0000AD7B
		public IEnumerable<MediaTypeFormatter> Formatters
		{
			get
			{
				return this._dependencies.Formatters;
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000CB88 File Offset: 0x0000AD88
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(this.Execute());
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000CB95 File Offset: 0x0000AD95
		private HttpResponseMessage Execute()
		{
			return NegotiatedContentResult<T>.Execute(this._statusCode, this._content, this._dependencies.ContentNegotiator, this._dependencies.Request, this._dependencies.Formatters);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000CBCC File Offset: 0x0000ADCC
		internal static HttpResponseMessage Execute(HttpStatusCode statusCode, T content, IContentNegotiator contentNegotiator, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
		{
			ContentNegotiationResult contentNegotiationResult = contentNegotiator.Negotiate(typeof(T), request, formatters);
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
			try
			{
				if (contentNegotiationResult == null)
				{
					httpResponseMessage.StatusCode = HttpStatusCode.NotAcceptable;
				}
				else
				{
					httpResponseMessage.StatusCode = statusCode;
					httpResponseMessage.Content = new ObjectContent<T>(content, contentNegotiationResult.Formatter, contentNegotiationResult.MediaType);
				}
				httpResponseMessage.RequestMessage = request;
			}
			catch
			{
				httpResponseMessage.Dispose();
				throw;
			}
			return httpResponseMessage;
		}

		// Token: 0x04000123 RID: 291
		private readonly HttpStatusCode _statusCode;

		// Token: 0x04000124 RID: 292
		private readonly T _content;

		// Token: 0x04000125 RID: 293
		private readonly NegotiatedContentResult<T>.IDependencyProvider _dependencies;

		// Token: 0x020001D5 RID: 469
		internal interface IDependencyProvider
		{
			// Token: 0x17000312 RID: 786
			// (get) Token: 0x06000B34 RID: 2868
			IContentNegotiator ContentNegotiator { get; }

			// Token: 0x17000313 RID: 787
			// (get) Token: 0x06000B35 RID: 2869
			HttpRequestMessage Request { get; }

			// Token: 0x17000314 RID: 788
			// (get) Token: 0x06000B36 RID: 2870
			IEnumerable<MediaTypeFormatter> Formatters { get; }
		}

		// Token: 0x020001D6 RID: 470
		internal sealed class DirectDependencyProvider : NegotiatedContentResult<T>.IDependencyProvider
		{
			// Token: 0x06000B37 RID: 2871 RVA: 0x0001CB94 File Offset: 0x0001AD94
			public DirectDependencyProvider(IContentNegotiator contentNegotiator, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
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
				this._contentNegotiator = contentNegotiator;
				this._request = request;
				this._formatters = formatters;
			}

			// Token: 0x17000315 RID: 789
			// (get) Token: 0x06000B38 RID: 2872 RVA: 0x0001CBE6 File Offset: 0x0001ADE6
			public IContentNegotiator ContentNegotiator
			{
				get
				{
					return this._contentNegotiator;
				}
			}

			// Token: 0x17000316 RID: 790
			// (get) Token: 0x06000B39 RID: 2873 RVA: 0x0001CBEE File Offset: 0x0001ADEE
			public HttpRequestMessage Request
			{
				get
				{
					return this._request;
				}
			}

			// Token: 0x17000317 RID: 791
			// (get) Token: 0x06000B3A RID: 2874 RVA: 0x0001CBF6 File Offset: 0x0001ADF6
			public IEnumerable<MediaTypeFormatter> Formatters
			{
				get
				{
					return this._formatters;
				}
			}

			// Token: 0x04000393 RID: 915
			private readonly IContentNegotiator _contentNegotiator;

			// Token: 0x04000394 RID: 916
			private readonly HttpRequestMessage _request;

			// Token: 0x04000395 RID: 917
			private readonly IEnumerable<MediaTypeFormatter> _formatters;
		}

		// Token: 0x020001D7 RID: 471
		internal sealed class ApiControllerDependencyProvider : NegotiatedContentResult<T>.IDependencyProvider
		{
			// Token: 0x06000B3B RID: 2875 RVA: 0x0001CBFE File Offset: 0x0001ADFE
			public ApiControllerDependencyProvider(ApiController controller)
			{
				if (controller == null)
				{
					throw new ArgumentNullException("controller");
				}
				this._controller = controller;
			}

			// Token: 0x17000318 RID: 792
			// (get) Token: 0x06000B3C RID: 2876 RVA: 0x0001CC1B File Offset: 0x0001AE1B
			public IContentNegotiator ContentNegotiator
			{
				get
				{
					this.EnsureResolved();
					return this._resolvedDependencies.ContentNegotiator;
				}
			}

			// Token: 0x17000319 RID: 793
			// (get) Token: 0x06000B3D RID: 2877 RVA: 0x0001CC2E File Offset: 0x0001AE2E
			public HttpRequestMessage Request
			{
				get
				{
					this.EnsureResolved();
					return this._resolvedDependencies.Request;
				}
			}

			// Token: 0x1700031A RID: 794
			// (get) Token: 0x06000B3E RID: 2878 RVA: 0x0001CC41 File Offset: 0x0001AE41
			public IEnumerable<MediaTypeFormatter> Formatters
			{
				get
				{
					this.EnsureResolved();
					return this._resolvedDependencies.Formatters;
				}
			}

			// Token: 0x06000B3F RID: 2879 RVA: 0x0001CC54 File Offset: 0x0001AE54
			private void EnsureResolved()
			{
				if (this._resolvedDependencies == null)
				{
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
					this._resolvedDependencies = new NegotiatedContentResult<T>.DirectDependencyProvider(contentNegotiator, request, formatters);
				}
			}

			// Token: 0x04000396 RID: 918
			private readonly ApiController _controller;

			// Token: 0x04000397 RID: 919
			private NegotiatedContentResult<T>.IDependencyProvider _resolvedDependencies;
		}
	}
}
