using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Properties;
using System.Web.Http.Routing;

namespace System.Web.Http.Results
{
	// Token: 0x020000AD RID: 173
	public class CreatedAtRouteNegotiatedContentResult<T> : IHttpActionResult
	{
		// Token: 0x0600041E RID: 1054 RVA: 0x0000BFA0 File Offset: 0x0000A1A0
		public CreatedAtRouteNegotiatedContentResult(string routeName, IDictionary<string, object> routeValues, T content, UrlHelper urlFactory, IContentNegotiator contentNegotiator, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
			: this(routeName, routeValues, content, new CreatedAtRouteNegotiatedContentResult<T>.DirectDependencyProvider(urlFactory, contentNegotiator, request, formatters))
		{
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000BFB8 File Offset: 0x0000A1B8
		public CreatedAtRouteNegotiatedContentResult(string routeName, IDictionary<string, object> routeValues, T content, ApiController controller)
			: this(routeName, routeValues, content, new CreatedAtRouteNegotiatedContentResult<T>.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000BFCA File Offset: 0x0000A1CA
		private CreatedAtRouteNegotiatedContentResult(string routeName, IDictionary<string, object> routeValues, T content, CreatedAtRouteNegotiatedContentResult<T>.IDependencyProvider dependencies)
		{
			if (routeName == null)
			{
				throw new ArgumentNullException("routeName");
			}
			this._routeName = routeName;
			this._routeValues = routeValues;
			this._content = content;
			this._dependencies = dependencies;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0000BFFD File Offset: 0x0000A1FD
		public string RouteName
		{
			get
			{
				return this._routeName;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0000C005 File Offset: 0x0000A205
		public IDictionary<string, object> RouteValues
		{
			get
			{
				return this._routeValues;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0000C00D File Offset: 0x0000A20D
		public T Content
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0000C015 File Offset: 0x0000A215
		public UrlHelper UrlFactory
		{
			get
			{
				return this._dependencies.UrlFactory;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0000C022 File Offset: 0x0000A222
		public IContentNegotiator ContentNegotiator
		{
			get
			{
				return this._dependencies.ContentNegotiator;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0000C02F File Offset: 0x0000A22F
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0000C03C File Offset: 0x0000A23C
		public IEnumerable<MediaTypeFormatter> Formatters
		{
			get
			{
				return this._dependencies.Formatters;
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000C049 File Offset: 0x0000A249
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(this.Execute());
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000C058 File Offset: 0x0000A258
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
					string text = this._dependencies.UrlFactory.Link(this._routeName, this._routeValues);
					if (text == null)
					{
						throw new InvalidOperationException(SRResources.UrlHelper_LinkMustNotReturnNull);
					}
					httpResponseMessage.Headers.Location = new Uri(text);
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

		// Token: 0x04000105 RID: 261
		private readonly string _routeName;

		// Token: 0x04000106 RID: 262
		private readonly IDictionary<string, object> _routeValues;

		// Token: 0x04000107 RID: 263
		private readonly T _content;

		// Token: 0x04000108 RID: 264
		private readonly CreatedAtRouteNegotiatedContentResult<T>.IDependencyProvider _dependencies;

		// Token: 0x020001CF RID: 463
		private interface IDependencyProvider
		{
			// Token: 0x170002FA RID: 762
			// (get) Token: 0x06000B16 RID: 2838
			UrlHelper UrlFactory { get; }

			// Token: 0x170002FB RID: 763
			// (get) Token: 0x06000B17 RID: 2839
			IContentNegotiator ContentNegotiator { get; }

			// Token: 0x170002FC RID: 764
			// (get) Token: 0x06000B18 RID: 2840
			HttpRequestMessage Request { get; }

			// Token: 0x170002FD RID: 765
			// (get) Token: 0x06000B19 RID: 2841
			IEnumerable<MediaTypeFormatter> Formatters { get; }
		}

		// Token: 0x020001D0 RID: 464
		private sealed class DirectDependencyProvider : CreatedAtRouteNegotiatedContentResult<T>.IDependencyProvider
		{
			// Token: 0x06000B1A RID: 2842 RVA: 0x0001C868 File Offset: 0x0001AA68
			public DirectDependencyProvider(UrlHelper urlFactory, IContentNegotiator contentNegotiator, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
			{
				if (urlFactory == null)
				{
					throw new ArgumentNullException("urlFactory");
				}
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
				this._urlFactory = urlFactory;
				this._contentNegotiator = contentNegotiator;
				this._request = request;
				this._formatters = formatters;
			}

			// Token: 0x170002FE RID: 766
			// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0001C8D1 File Offset: 0x0001AAD1
			public UrlHelper UrlFactory
			{
				get
				{
					return this._urlFactory;
				}
			}

			// Token: 0x170002FF RID: 767
			// (get) Token: 0x06000B1C RID: 2844 RVA: 0x0001C8D9 File Offset: 0x0001AAD9
			public IContentNegotiator ContentNegotiator
			{
				get
				{
					return this._contentNegotiator;
				}
			}

			// Token: 0x17000300 RID: 768
			// (get) Token: 0x06000B1D RID: 2845 RVA: 0x0001C8E1 File Offset: 0x0001AAE1
			public HttpRequestMessage Request
			{
				get
				{
					return this._request;
				}
			}

			// Token: 0x17000301 RID: 769
			// (get) Token: 0x06000B1E RID: 2846 RVA: 0x0001C8E9 File Offset: 0x0001AAE9
			public IEnumerable<MediaTypeFormatter> Formatters
			{
				get
				{
					return this._formatters;
				}
			}

			// Token: 0x04000387 RID: 903
			private readonly UrlHelper _urlFactory;

			// Token: 0x04000388 RID: 904
			private readonly IContentNegotiator _contentNegotiator;

			// Token: 0x04000389 RID: 905
			private readonly HttpRequestMessage _request;

			// Token: 0x0400038A RID: 906
			private readonly IEnumerable<MediaTypeFormatter> _formatters;
		}

		// Token: 0x020001D1 RID: 465
		private sealed class ApiControllerDependencyProvider : CreatedAtRouteNegotiatedContentResult<T>.IDependencyProvider
		{
			// Token: 0x06000B1F RID: 2847 RVA: 0x0001C8F1 File Offset: 0x0001AAF1
			public ApiControllerDependencyProvider(ApiController controller)
			{
				if (controller == null)
				{
					throw new ArgumentNullException("controller");
				}
				this._controller = controller;
			}

			// Token: 0x17000302 RID: 770
			// (get) Token: 0x06000B20 RID: 2848 RVA: 0x0001C90E File Offset: 0x0001AB0E
			public UrlHelper UrlFactory
			{
				get
				{
					this.EnsureResolved();
					return this._resolvedDependencies.UrlFactory;
				}
			}

			// Token: 0x17000303 RID: 771
			// (get) Token: 0x06000B21 RID: 2849 RVA: 0x0001C921 File Offset: 0x0001AB21
			public IContentNegotiator ContentNegotiator
			{
				get
				{
					this.EnsureResolved();
					return this._resolvedDependencies.ContentNegotiator;
				}
			}

			// Token: 0x17000304 RID: 772
			// (get) Token: 0x06000B22 RID: 2850 RVA: 0x0001C934 File Offset: 0x0001AB34
			public HttpRequestMessage Request
			{
				get
				{
					this.EnsureResolved();
					return this._resolvedDependencies.Request;
				}
			}

			// Token: 0x17000305 RID: 773
			// (get) Token: 0x06000B23 RID: 2851 RVA: 0x0001C947 File Offset: 0x0001AB47
			public IEnumerable<MediaTypeFormatter> Formatters
			{
				get
				{
					this.EnsureResolved();
					return this._resolvedDependencies.Formatters;
				}
			}

			// Token: 0x06000B24 RID: 2852 RVA: 0x0001C95C File Offset: 0x0001AB5C
			private void EnsureResolved()
			{
				if (this._resolvedDependencies == null)
				{
					HttpRequestMessage request = this._controller.Request;
					if (request == null)
					{
						throw new InvalidOperationException(SRResources.ApiController_RequestMustNotBeNull);
					}
					UrlHelper urlHelper = this._controller.Url ?? new UrlHelper(request);
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
					IEnumerable<MediaTypeFormatter> formatters = configuration.Formatters;
					this._resolvedDependencies = new CreatedAtRouteNegotiatedContentResult<T>.DirectDependencyProvider(urlHelper, contentNegotiator, request, formatters);
				}
			}

			// Token: 0x0400038B RID: 907
			private readonly ApiController _controller;

			// Token: 0x0400038C RID: 908
			private CreatedAtRouteNegotiatedContentResult<T>.IDependencyProvider _resolvedDependencies;
		}
	}
}
