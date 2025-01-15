using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Hosting;
using System.Web.Http.ModelBinding;
using System.Web.Http.Properties;
using System.Web.Http.Results;
using System.Web.Http.Routing;

namespace System.Net.Http
{
	// Token: 0x02000008 RID: 8
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpRequestMessageExtensions
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000025C4 File Offset: 0x000007C4
		public static HttpConfiguration GetConfiguration(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			HttpRequestContext requestContext = request.GetRequestContext();
			if (requestContext != null)
			{
				return requestContext.Configuration;
			}
			return request.LegacyGetConfiguration();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000025F6 File Offset: 0x000007F6
		internal static HttpConfiguration LegacyGetConfiguration(this HttpRequestMessage request)
		{
			return request.GetProperty(HttpPropertyKeys.HttpConfigurationKey);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002604 File Offset: 0x00000804
		public static void SetConfiguration(this HttpRequestMessage request, HttpConfiguration configuration)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			HttpRequestContext requestContext = request.GetRequestContext();
			if (requestContext != null)
			{
				requestContext.Configuration = configuration;
			}
			request.Properties[HttpPropertyKeys.HttpConfigurationKey] = configuration;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002650 File Offset: 0x00000850
		public static IDependencyScope GetDependencyScope(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			IDependencyScope dependencyScope;
			if (!request.Properties.TryGetValue(HttpPropertyKeys.DependencyScope, out dependencyScope))
			{
				IDependencyResolver dependencyResolver = request.GetConfiguration().DependencyResolver;
				dependencyScope = dependencyResolver.BeginScope();
				if (dependencyScope == null)
				{
					throw Error.InvalidOperation(SRResources.DependencyResolver_BeginScopeReturnsNull, new object[] { dependencyResolver.GetType().Name });
				}
				request.Properties[HttpPropertyKeys.DependencyScope] = dependencyScope;
				request.RegisterForDispose(dependencyScope);
			}
			return dependencyScope;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000026CD File Offset: 0x000008CD
		public static HttpRequestContext GetRequestContext(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return request.GetProperty(HttpPropertyKeys.RequestContextKey);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000026E8 File Offset: 0x000008E8
		public static void SetRequestContext(this HttpRequestMessage request, HttpRequestContext context)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (context == null)
			{
				throw Error.ArgumentNull("context");
			}
			request.Properties[HttpPropertyKeys.RequestContextKey] = context;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002717 File Offset: 0x00000917
		public static SynchronizationContext GetSynchronizationContext(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return request.GetProperty(HttpPropertyKeys.SynchronizationContextKey);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002732 File Offset: 0x00000932
		internal static void SetSynchronizationContext(this HttpRequestMessage request, SynchronizationContext synchronizationContext)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			request.Properties[HttpPropertyKeys.SynchronizationContextKey] = synchronizationContext;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002754 File Offset: 0x00000954
		public static X509Certificate2 GetClientCertificate(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			HttpRequestContext requestContext = request.GetRequestContext();
			if (requestContext != null)
			{
				return requestContext.ClientCertificate;
			}
			return request.LegacyGetClientCertificate();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002788 File Offset: 0x00000988
		internal static X509Certificate2 LegacyGetClientCertificate(this HttpRequestMessage request)
		{
			X509Certificate2 x509Certificate = null;
			Func<HttpRequestMessage, X509Certificate2> func;
			if (!request.Properties.TryGetValue(HttpPropertyKeys.ClientCertificateKey, out x509Certificate) && request.Properties.TryGetValue(HttpPropertyKeys.RetrieveClientCertificateDelegateKey, out func))
			{
				x509Certificate = func(request);
				if (x509Certificate != null)
				{
					request.Properties.Add(HttpPropertyKeys.ClientCertificateKey, x509Certificate);
				}
			}
			return x509Certificate;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000027DC File Offset: 0x000009DC
		public static IHttpRouteData GetRouteData(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			HttpRequestContext requestContext = request.GetRequestContext();
			if (requestContext != null)
			{
				return requestContext.RouteData;
			}
			return request.LegacyGetRouteData();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000280E File Offset: 0x00000A0E
		internal static IHttpRouteData LegacyGetRouteData(this HttpRequestMessage request)
		{
			return request.GetProperty(HttpPropertyKeys.HttpRouteDataKey);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000281C File Offset: 0x00000A1C
		public static void SetRouteData(this HttpRequestMessage request, IHttpRouteData routeData)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (routeData == null)
			{
				throw Error.ArgumentNull("routeData");
			}
			HttpRequestContext requestContext = request.GetRequestContext();
			if (requestContext != null)
			{
				requestContext.RouteData = routeData;
			}
			request.Properties[HttpPropertyKeys.HttpRouteDataKey] = routeData;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002867 File Offset: 0x00000A67
		public static HttpActionDescriptor GetActionDescriptor(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return request.GetProperty(HttpPropertyKeys.HttpActionDescriptorKey);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002882 File Offset: 0x00000A82
		internal static void SetActionDescriptor(this HttpRequestMessage request, HttpActionDescriptor actionDescriptor)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			request.Properties[HttpPropertyKeys.HttpActionDescriptorKey] = actionDescriptor;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000028A4 File Offset: 0x00000AA4
		private static T GetProperty<T>(this HttpRequestMessage request, string key)
		{
			T t;
			request.Properties.TryGetValue(key, out t);
			return t;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000028C1 File Offset: 0x00000AC1
		public static HttpResponseMessage CreateErrorResponse(this HttpRequestMessage request, InvalidByteRangeException invalidByteRangeException)
		{
			if (invalidByteRangeException == null)
			{
				throw Error.ArgumentNull("invalidByteRangeException");
			}
			HttpResponseMessage httpResponseMessage = request.CreateErrorResponse(HttpStatusCode.RequestedRangeNotSatisfiable, invalidByteRangeException);
			httpResponseMessage.Content.Headers.ContentRange = invalidByteRangeException.ContentRange;
			return httpResponseMessage;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000028F3 File Offset: 0x00000AF3
		public static HttpResponseMessage CreateErrorResponse(this HttpRequestMessage request, HttpStatusCode statusCode, string message)
		{
			return request.CreateErrorResponse(statusCode, new HttpError(message));
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002904 File Offset: 0x00000B04
		internal static HttpResponseMessage CreateErrorResponse(this HttpRequestMessage request, HttpStatusCode statusCode, string message, string messageDetail)
		{
			return request.CreateErrorResponse(statusCode, delegate(bool includeErrorDetail)
			{
				if (!includeErrorDetail)
				{
					return new HttpError(message);
				}
				return new HttpError(message, messageDetail);
			});
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002938 File Offset: 0x00000B38
		public static HttpResponseMessage CreateErrorResponse(this HttpRequestMessage request, HttpStatusCode statusCode, string message, Exception exception)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return request.CreateErrorResponse(statusCode, (bool includeErrorDetail) => new HttpError(exception, includeErrorDetail)
			{
				Message = message
			});
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000297C File Offset: 0x00000B7C
		public static HttpResponseMessage CreateErrorResponse(this HttpRequestMessage request, HttpStatusCode statusCode, Exception exception)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return request.CreateErrorResponse(statusCode, (bool includeErrorDetail) => new HttpError(exception, includeErrorDetail));
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000029B8 File Offset: 0x00000BB8
		public static HttpResponseMessage CreateErrorResponse(this HttpRequestMessage request, HttpStatusCode statusCode, ModelStateDictionary modelState)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return request.CreateErrorResponse(statusCode, (bool includeErrorDetail) => new HttpError(modelState, includeErrorDetail));
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000029F4 File Offset: 0x00000BF4
		public static HttpResponseMessage CreateErrorResponse(this HttpRequestMessage request, HttpStatusCode statusCode, HttpError error)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return request.CreateErrorResponse(statusCode, (bool includeErrorDetail) => error);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002A30 File Offset: 0x00000C30
		private static HttpResponseMessage CreateErrorResponse(this HttpRequestMessage request, HttpStatusCode statusCode, Func<bool, HttpError> errorCreator)
		{
			HttpConfiguration configuration = request.GetConfiguration();
			HttpError httpError = errorCreator(request.ShouldIncludeErrorDetail());
			if (configuration == null)
			{
				using (HttpConfiguration httpConfiguration = new HttpConfiguration())
				{
					return request.CreateResponse(statusCode, httpError, httpConfiguration);
				}
			}
			return request.CreateResponse(statusCode, httpError, configuration);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002A8C File Offset: 0x00000C8C
		public static HttpResponseMessage CreateResponse<T>(this HttpRequestMessage request, T value)
		{
			return request.CreateResponse(HttpStatusCode.OK, value, null);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002A9B File Offset: 0x00000C9B
		public static HttpResponseMessage CreateResponse<T>(this HttpRequestMessage request, HttpStatusCode statusCode, T value)
		{
			return request.CreateResponse(statusCode, value, null);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002AA8 File Offset: 0x00000CA8
		public static HttpResponseMessage CreateResponse<T>(this HttpRequestMessage request, HttpStatusCode statusCode, T value, HttpConfiguration configuration)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			configuration = configuration ?? request.GetConfiguration();
			if (configuration == null)
			{
				throw Error.InvalidOperation(SRResources.HttpRequestMessageExtensions_NoConfiguration, new object[0]);
			}
			IContentNegotiator contentNegotiator = configuration.Services.GetContentNegotiator();
			if (contentNegotiator == null)
			{
				throw Error.InvalidOperation(SRResources.HttpRequestMessageExtensions_NoContentNegotiator, new object[] { typeof(IContentNegotiator).FullName });
			}
			IEnumerable<MediaTypeFormatter> formatters = configuration.Formatters;
			return NegotiatedContentResult<T>.Execute(statusCode, value, contentNegotiator, request, formatters);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002B27 File Offset: 0x00000D27
		public static HttpResponseMessage CreateResponse<T>(this HttpRequestMessage request, HttpStatusCode statusCode, T value, string mediaType)
		{
			return request.CreateResponse(statusCode, value, new MediaTypeHeaderValue(mediaType));
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002B38 File Offset: 0x00000D38
		public static HttpResponseMessage CreateResponse<T>(this HttpRequestMessage request, HttpStatusCode statusCode, T value, MediaTypeHeaderValue mediaType)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (mediaType == null)
			{
				throw Error.ArgumentNull("mediaType");
			}
			HttpConfiguration configuration = request.GetConfiguration();
			if (configuration == null)
			{
				throw Error.InvalidOperation(SRResources.HttpRequestMessageExtensions_NoConfiguration, new object[0]);
			}
			MediaTypeFormatter mediaTypeFormatter = configuration.Formatters.FindWriter(typeof(T), mediaType);
			if (mediaTypeFormatter == null)
			{
				throw Error.InvalidOperation(SRResources.HttpRequestMessageExtensions_NoMatchingFormatter, new object[]
				{
					mediaType,
					typeof(T).Name
				});
			}
			return request.CreateResponse(statusCode, value, mediaTypeFormatter, mediaType);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002BC7 File Offset: 0x00000DC7
		public static HttpResponseMessage CreateResponse<T>(this HttpRequestMessage request, HttpStatusCode statusCode, T value, MediaTypeFormatter formatter)
		{
			return request.CreateResponse(statusCode, value, formatter, null);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002BD4 File Offset: 0x00000DD4
		public static HttpResponseMessage CreateResponse<T>(this HttpRequestMessage request, HttpStatusCode statusCode, T value, MediaTypeFormatter formatter, string mediaType)
		{
			MediaTypeHeaderValue mediaTypeHeaderValue = ((mediaType != null) ? new MediaTypeHeaderValue(mediaType) : null);
			return request.CreateResponse(statusCode, value, formatter, mediaTypeHeaderValue);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002BFA File Offset: 0x00000DFA
		public static HttpResponseMessage CreateResponse<T>(this HttpRequestMessage request, HttpStatusCode statusCode, T value, MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (formatter == null)
			{
				throw Error.ArgumentNull("formatter");
			}
			return FormattedContentResult<T>.Execute(statusCode, value, formatter, mediaType, request);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002C23 File Offset: 0x00000E23
		public static void RegisterForDispose(this HttpRequestMessage request, IDisposable resource)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (resource == null)
			{
				return;
			}
			HttpRequestMessageExtensions.GetRegisteredResourcesForDispose(request).Add(resource);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002C44 File Offset: 0x00000E44
		public static void RegisterForDispose(this HttpRequestMessage request, IEnumerable<IDisposable> resources)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (resources == null)
			{
				throw Error.ArgumentNull("resources");
			}
			List<IDisposable> registeredResourcesForDispose = HttpRequestMessageExtensions.GetRegisteredResourcesForDispose(request);
			foreach (IDisposable disposable in resources)
			{
				if (disposable != null)
				{
					registeredResourcesForDispose.Add(disposable);
				}
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002CB4 File Offset: 0x00000EB4
		public static void DisposeRequestResources(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			List<IDisposable> list;
			if (request.Properties.TryGetValue(HttpPropertyKeys.DisposableRequestResourcesKey, out list))
			{
				foreach (IDisposable disposable in list)
				{
					try
					{
						disposable.Dispose();
					}
					catch
					{
					}
				}
				list.Clear();
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002D3C File Offset: 0x00000F3C
		public static Guid GetCorrelationId(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			Guid guid;
			if (!request.Properties.TryGetValue(HttpPropertyKeys.RequestCorrelationKey, out guid))
			{
				guid = Trace.CorrelationManager.ActivityId;
				if (guid == Guid.Empty)
				{
					guid = Guid.NewGuid();
				}
				request.Properties.Add(HttpPropertyKeys.RequestCorrelationKey, guid);
			}
			return guid;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002DA0 File Offset: 0x00000FA0
		public static IEnumerable<KeyValuePair<string, string>> GetQueryNameValuePairs(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			Uri requestUri = request.RequestUri;
			if (requestUri == null || string.IsNullOrEmpty(requestUri.Query))
			{
				return Enumerable.Empty<KeyValuePair<string, string>>();
			}
			IEnumerable<KeyValuePair<string, string>> enumerable;
			request.Properties.TryGetValue(HttpPropertyKeys.RequestQueryNameValuePairsKey, out enumerable);
			string text;
			request.Properties.TryGetValue(HttpPropertyKeys.CachedRequestQueryKey, out text);
			if (enumerable == null || (text != null && text != (requestUri.Query ?? string.Empty)))
			{
				enumerable = new FormDataCollection(requestUri).GetJQueryNameValuePairs().ToArray<KeyValuePair<string, string>>();
				request.Properties[HttpPropertyKeys.RequestQueryNameValuePairsKey] = enumerable;
				request.Properties[HttpPropertyKeys.CachedRequestQueryKey] = requestUri.Query ?? string.Empty;
			}
			return enumerable;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002E60 File Offset: 0x00001060
		public static UrlHelper GetUrlHelper(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			HttpRequestContext requestContext = request.GetRequestContext();
			if (requestContext != null)
			{
				return requestContext.Url;
			}
			return new UrlHelper(request);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002E94 File Offset: 0x00001094
		public static bool IsLocal(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			HttpRequestContext requestContext = request.GetRequestContext();
			if (requestContext != null)
			{
				return requestContext.IsLocal;
			}
			return request.LegacyIsLocal();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002EC8 File Offset: 0x000010C8
		internal static bool LegacyIsLocal(this HttpRequestMessage request)
		{
			Lazy<bool> property = request.GetProperty(HttpPropertyKeys.IsLocalKey);
			return property != null && property.Value;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002EEC File Offset: 0x000010EC
		public static bool IsBatchRequest(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return request.GetProperty(HttpPropertyKeys.IsBatchRequest);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002F08 File Offset: 0x00001108
		public static bool ShouldIncludeErrorDetail(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			HttpRequestContext requestContext = request.GetRequestContext();
			if (requestContext != null)
			{
				return requestContext.IncludeErrorDetail;
			}
			return request.LegacyShouldIncludeErrorDetail();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002F3C File Offset: 0x0000113C
		internal static bool LegacyShouldIncludeErrorDetail(this HttpRequestMessage request)
		{
			HttpConfiguration configuration = request.GetConfiguration();
			IncludeErrorDetailPolicy includeErrorDetailPolicy = IncludeErrorDetailPolicy.Default;
			if (configuration != null)
			{
				includeErrorDetailPolicy = configuration.IncludeErrorDetailPolicy;
			}
			switch (includeErrorDetailPolicy)
			{
			case IncludeErrorDetailPolicy.Default:
			{
				Lazy<bool> property = request.GetProperty(HttpPropertyKeys.IncludeErrorDetailKey);
				if (property != null)
				{
					return property.Value;
				}
				break;
			}
			case IncludeErrorDetailPolicy.LocalOnly:
				break;
			case IncludeErrorDetailPolicy.Always:
				return true;
			case IncludeErrorDetailPolicy.Never:
				return false;
			default:
				return false;
			}
			return request.IsLocal();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002F94 File Offset: 0x00001194
		public static IEnumerable<IDisposable> GetResourcesForDisposal(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return HttpRequestMessageExtensions.GetRegisteredResourcesForDispose(request);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002FAC File Offset: 0x000011AC
		private static List<IDisposable> GetRegisteredResourcesForDispose(HttpRequestMessage request)
		{
			List<IDisposable> list;
			if (!request.Properties.TryGetValue(HttpPropertyKeys.DisposableRequestResourcesKey, out list))
			{
				list = new List<IDisposable>();
				request.Properties[HttpPropertyKeys.DisposableRequestResourcesKey] = list;
			}
			return list;
		}
	}
}
