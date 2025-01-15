using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNet.OData.Formatter.Serialization;
using Microsoft.AspNet.OData.Routing;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000004 RID: 4
	public class ETagMessageHandler : DelegatingHandler
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002260 File Offset: 0x00000460
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			HttpConfiguration configuration = HttpRequestMessageExtensions.GetConfiguration(request);
			if (configuration == null)
			{
				throw Error.InvalidOperation(SRResources.RequestMustContainConfiguration, new object[0]);
			}
			HttpResponseMessage httpResponseMessage = await base.SendAsync(request, cancellationToken);
			ObjectContent objectContent = ((httpResponseMessage == null) ? null : (httpResponseMessage.Content as ObjectContent));
			if (objectContent != null)
			{
				EntityTagHeaderValue etag = ETagMessageHandler.GetETag((httpResponseMessage == null) ? null : new int?((int)httpResponseMessage.StatusCode), request.ODataProperties().Path, request.GetModel(), objectContent.Value, configuration.GetETagHandler());
				if (etag != null)
				{
					httpResponseMessage.Headers.ETag = etag;
				}
			}
			return httpResponseMessage;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022B5 File Offset: 0x000004B5
		internal Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
		{
			return this.SendAsync(request, CancellationToken.None);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022C4 File Offset: 0x000004C4
		private static EntityTagHeaderValue GetETag(int? statusCode, Microsoft.AspNet.OData.Routing.ODataPath path, IEdmModel model, object value, IETagHandler etagHandler)
		{
			if (path == null)
			{
				throw Error.ArgumentNull("path");
			}
			if (model == null)
			{
				throw Error.ArgumentNull("model");
			}
			if (etagHandler == null)
			{
				throw Error.ArgumentNull("etagHandler");
			}
			if (statusCode == null || statusCode.Value < 200 || statusCode.Value >= 300 || statusCode.Value == 204)
			{
				return null;
			}
			IEdmEntityType singleEntityEntityType = ETagMessageHandler.GetSingleEntityEntityType(path);
			IEdmEntityTypeReference typeReference = ETagMessageHandler.GetTypeReference(model, singleEntityEntityType, value);
			if (typeReference != null)
			{
				ResourceContext resourceContext = ETagMessageHandler.CreateInstanceContext(model, typeReference, value);
				resourceContext.EdmModel = model;
				resourceContext.NavigationSource = path.NavigationSource;
				return ETagMessageHandler.CreateETag(resourceContext, etagHandler);
			}
			return null;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002368 File Offset: 0x00000568
		private static IEdmEntityTypeReference GetTypeReference(IEdmModel model, IEdmEntityType edmType, object value)
		{
			if (model == null || edmType == null || value == null)
			{
				return null;
			}
			IEdmObject edmObject = value as IEdmEntityObject;
			if (edmObject != null)
			{
				return edmObject.GetEdmType().AsEntity();
			}
			IEdmTypeReference edmTypeReference = model.GetEdmTypeReference(value.GetType());
			if (edmTypeReference != null && edmTypeReference.Definition.IsOrInheritsFrom(edmType))
			{
				return (IEdmEntityTypeReference)edmTypeReference;
			}
			return null;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023BC File Offset: 0x000005BC
		private static EntityTagHeaderValue CreateETag(ResourceContext resourceContext, IETagHandler handler)
		{
			IEdmModel edmModel = resourceContext.EdmModel;
			IEnumerable<IEdmStructuralProperty> enumerable;
			if (edmModel != null && resourceContext.NavigationSource != null)
			{
				enumerable = from c in edmModel.GetConcurrencyProperties(resourceContext.NavigationSource)
					orderby c.Name
					select c;
			}
			else
			{
				enumerable = Enumerable.Empty<IEdmStructuralProperty>();
			}
			IDictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (IEdmStructuralProperty edmStructuralProperty in enumerable)
			{
				dictionary.Add(edmStructuralProperty.Name, resourceContext.GetPropertyValue(edmStructuralProperty.Name));
			}
			return handler.CreateETag(dictionary);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002470 File Offset: 0x00000670
		private static ResourceContext CreateInstanceContext(IEdmModel model, IEdmEntityTypeReference reference, object value)
		{
			return new ResourceContext(new ODataSerializerContext
			{
				Model = model
			}, reference, value);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002488 File Offset: 0x00000688
		internal static IEdmEntityType GetSingleEntityEntityType(Microsoft.AspNet.OData.Routing.ODataPath path)
		{
			if (path == null || path.Segments.Count == 0)
			{
				return null;
			}
			int num = path.Segments.Count - 1;
			while (num >= 0 && path.Segments[num] is TypeSegment)
			{
				num--;
			}
			if (num < 0)
			{
				return null;
			}
			ODataPathSegment odataPathSegment = path.Segments[num];
			if (odataPathSegment is SingletonSegment || odataPathSegment is KeySegment)
			{
				return (IEdmEntityType)path.EdmType;
			}
			NavigationPropertySegment navigationPropertySegment = odataPathSegment as NavigationPropertySegment;
			if (navigationPropertySegment != null && (navigationPropertySegment.NavigationProperty.TargetMultiplicity() == EdmMultiplicity.ZeroOrOne || navigationPropertySegment.NavigationProperty.TargetMultiplicity() == EdmMultiplicity.One))
			{
				return (IEdmEntityType)path.EdmType;
			}
			return null;
		}
	}
}
