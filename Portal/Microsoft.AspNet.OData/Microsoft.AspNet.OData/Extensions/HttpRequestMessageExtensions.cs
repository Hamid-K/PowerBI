using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNet.OData.Formatter.Deserialization;
using Microsoft.AspNet.OData.Formatter.Serialization;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNet.OData.Routing.Conventions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Extensions
{
	// Token: 0x020001C5 RID: 453
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpRequestMessageExtensions
	{
		// Token: 0x06000EEF RID: 3823 RVA: 0x0003D9A4 File Offset: 0x0003BBA4
		public static HttpRequestMessageProperties ODataProperties(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			object obj;
			HttpRequestMessageProperties httpRequestMessageProperties;
			if (request.Properties.TryGetValue("Microsoft.AspNet.OData.Properties", out obj))
			{
				httpRequestMessageProperties = obj as HttpRequestMessageProperties;
			}
			else
			{
				httpRequestMessageProperties = new HttpRequestMessageProperties(request);
				request.Properties["Microsoft.AspNet.OData.Properties"] = httpRequestMessageProperties;
			}
			return httpRequestMessageProperties;
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0003D9F5 File Offset: 0x0003BBF5
		public static HttpResponseMessage CreateErrorResponse(this HttpRequestMessage request, HttpStatusCode statusCode, ODataError oDataError)
		{
			if (HttpRequestMessageExtensions.ShouldIncludeErrorDetail(request))
			{
				return HttpRequestMessageExtensions.CreateResponse<ODataError>(request, statusCode, oDataError);
			}
			return HttpRequestMessageExtensions.CreateResponse<ODataError>(request, statusCode, new ODataError
			{
				ErrorCode = oDataError.ErrorCode,
				Message = oDataError.Message
			});
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0003DA2C File Offset: 0x0003BC2C
		public static ETag GetETag(this HttpRequestMessage request, EntityTagHeaderValue entityTagHeaderValue)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (entityTagHeaderValue != null)
			{
				if (entityTagHeaderValue.Equals(EntityTagHeaderValue.Any))
				{
					return new ETag
					{
						IsAny = true
					};
				}
				HttpConfiguration configuration = HttpRequestMessageExtensions.GetConfiguration(request);
				if (configuration == null)
				{
					throw Error.InvalidOperation(SRResources.RequestMustContainConfiguration, new object[0]);
				}
				IList<object> list = (configuration.GetETagHandler().ParseETag(entityTagHeaderValue) ?? new Dictionary<string, object>()).Select((KeyValuePair<string, object> property) => property.Value).AsList<object>();
				ODataPath path = request.ODataProperties().Path;
				IEdmModel model = request.GetModel();
				IEdmNavigationSource navigationSource = path.NavigationSource;
				if (model != null && navigationSource != null)
				{
					IList<IEdmStructuralProperty> list2 = model.GetConcurrencyProperties(navigationSource).ToList<IEdmStructuralProperty>();
					IList<string> list3 = (from c in list2
						orderby c.Name
						select c.Name).AsList<string>();
					ETag etag = new ETag();
					if (list.Count != list3.Count)
					{
						etag.IsWellFormed = false;
					}
					using (IEnumerator<KeyValuePair<string, object>> enumerator = list3.Zip(list, (string name, object value) => new KeyValuePair<string, object>(name, value)).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<string, object> nameValue = enumerator.Current;
							Type clrType = EdmLibHelpers.GetClrType(list2.SingleOrDefault((IEdmStructuralProperty e) => e.Name == nameValue.Key).Type, model);
							if (nameValue.Value != null)
							{
								Type type = nameValue.Value.GetType();
								etag[nameValue.Key] = ((type != clrType) ? Convert.ChangeType(nameValue.Value, clrType, CultureInfo.InvariantCulture) : nameValue.Value);
							}
							else
							{
								etag[nameValue.Key] = nameValue.Value;
							}
						}
					}
					return etag;
				}
			}
			return null;
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0003DC80 File Offset: 0x0003BE80
		public static ETag<TEntity> GetETag<TEntity>(this HttpRequestMessage request, EntityTagHeaderValue entityTagHeaderValue)
		{
			ETag etag = request.GetETag(entityTagHeaderValue);
			if (etag == null)
			{
				return null;
			}
			return new ETag<TEntity>
			{
				ConcurrencyProperties = etag.ConcurrencyProperties,
				IsWellFormed = etag.IsWellFormed,
				IsAny = etag.IsAny
			};
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0003DCC3 File Offset: 0x0003BEC3
		public static Uri GetNextPageLink(this HttpRequestMessage request, int pageSize)
		{
			return request.GetNextPageLink(pageSize, null, null);
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0003DCD0 File Offset: 0x0003BED0
		public static Uri GetNextPageLink(this HttpRequestMessage request, int pageSize, object instance, Func<object, string> objToSkipTokenValue)
		{
			if (request == null || request.RequestUri == null)
			{
				throw Error.ArgumentNull("request");
			}
			CompatibilityOptions compatibilityOptions = request.GetCompatibilityOptions();
			return GetNextPageHelper.GetNextPageLink(request.RequestUri, HttpRequestMessageExtensions.GetQueryNameValuePairs(request), pageSize, instance, objToSkipTokenValue, compatibilityOptions);
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x0003DD18 File Offset: 0x0003BF18
		public static IServiceProvider GetRequestContainer(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			object obj;
			if (request.Properties.TryGetValue("Microsoft.AspNet.OData.RequestContainer", out obj))
			{
				return (IServiceProvider)obj;
			}
			return request.CreateRequestContainer(null);
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0003DD58 File Offset: 0x0003BF58
		public static IServiceProvider CreateRequestContainer(this HttpRequestMessage request, string routeName)
		{
			if (request.Properties.ContainsKey("Microsoft.AspNet.OData.RequestContainer"))
			{
				throw Error.InvalidOperation(SRResources.RequestContainerAlreadyExists, new object[0]);
			}
			IServiceScope serviceScope = request.CreateRequestScope(routeName);
			IServiceProvider serviceProvider = serviceScope.ServiceProvider;
			request.Properties["Microsoft.AspNet.OData.RequestScope"] = serviceScope;
			request.Properties["Microsoft.AspNet.OData.RequestContainer"] = serviceProvider;
			return serviceProvider;
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0003DDBC File Offset: 0x0003BFBC
		public static void DeleteRequestContainer(this HttpRequestMessage request, bool dispose)
		{
			object obj;
			if (request.Properties.TryGetValue("Microsoft.AspNet.OData.RequestScope", out obj))
			{
				IServiceScope serviceScope = (IServiceScope)obj;
				request.Properties.Remove("Microsoft.AspNet.OData.RequestScope");
				request.Properties.Remove("Microsoft.AspNet.OData.RequestContainer");
				if (dispose)
				{
					serviceScope.Dispose();
				}
			}
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0003DE0F File Offset: 0x0003C00F
		public static IEdmModel GetModel(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return ServiceProviderServiceExtensions.GetRequiredService<IEdmModel>(request.GetRequestContainer());
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0003DE2A File Offset: 0x0003C02A
		public static ODataMessageWriterSettings GetWriterSettings(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return ServiceProviderServiceExtensions.GetRequiredService<ODataMessageWriterSettings>(request.GetRequestContainer());
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0003DE45 File Offset: 0x0003C045
		public static ODataMessageReaderSettings GetReaderSettings(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return ServiceProviderServiceExtensions.GetRequiredService<ODataMessageReaderSettings>(request.GetRequestContainer());
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0003DE60 File Offset: 0x0003C060
		public static IODataPathHandler GetPathHandler(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return ServiceProviderServiceExtensions.GetRequiredService<IODataPathHandler>(request.GetRequestContainer());
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x0003DE7B File Offset: 0x0003C07B
		public static ODataSerializerProvider GetSerializerProvider(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return ServiceProviderServiceExtensions.GetRequiredService<ODataSerializerProvider>(request.GetRequestContainer());
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x0003DE96 File Offset: 0x0003C096
		public static ODataDeserializerProvider GetDeserializerProvider(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return ServiceProviderServiceExtensions.GetRequiredService<ODataDeserializerProvider>(request.GetRequestContainer());
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x0003DEB1 File Offset: 0x0003C0B1
		public static IEnumerable<IODataRoutingConvention> GetRoutingConventions(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return ServiceProviderServiceExtensions.GetServices<IODataRoutingConvention>(request.GetRequestContainer());
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x0003DECC File Offset: 0x0003C0CC
		internal static CompatibilityOptions GetCompatibilityOptions(this HttpRequestMessage request)
		{
			HttpConfiguration configuration = HttpRequestMessageExtensions.GetConfiguration(request);
			if (configuration == null)
			{
				return CompatibilityOptions.None;
			}
			return configuration.GetCompatibilityOptions();
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0003DEEC File Offset: 0x0003C0EC
		private static IServiceScope CreateRequestScope(this HttpRequestMessage request, string routeName)
		{
			HttpConfiguration configuration = HttpRequestMessageExtensions.GetConfiguration(request);
			if (configuration == null)
			{
				throw Error.Argument("request", SRResources.RequestMustContainConfiguration, new object[0]);
			}
			IServiceScope serviceScope = ServiceProviderServiceExtensions.GetRequiredService<IServiceScopeFactory>(configuration.GetODataRootContainer(routeName)).CreateScope();
			if (routeName != null)
			{
				ServiceProviderServiceExtensions.GetRequiredService<HttpRequestScope>(serviceScope.ServiceProvider).HttpRequest = request;
			}
			return serviceScope;
		}

		// Token: 0x04000422 RID: 1058
		private const string PropertiesKey = "Microsoft.AspNet.OData.Properties";

		// Token: 0x04000423 RID: 1059
		private const string RequestContainerKey = "Microsoft.AspNet.OData.RequestContainer";

		// Token: 0x04000424 RID: 1060
		private const string RequestScopeKey = "Microsoft.AspNet.OData.RequestScope";
	}
}
