using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Routing;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Builder.Conventions;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNet.OData.Formatter.Serialization;
using Microsoft.AspNet.OData.Routing;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Results
{
	// Token: 0x020000A2 RID: 162
	internal static class ResultHelpers
	{
		// Token: 0x06000573 RID: 1395 RVA: 0x00012C48 File Offset: 0x00010E48
		public static Uri GenerateODataLink(HttpRequestMessage request, object entity, bool isEntityId)
		{
			IEdmModel model = request.GetModel();
			if (model == null)
			{
				throw new InvalidOperationException(SRResources.RequestMustHaveModel);
			}
			Microsoft.AspNet.OData.Routing.ODataPath path = request.ODataProperties().Path;
			if (path == null)
			{
				throw new InvalidOperationException(SRResources.ODataPathMissing);
			}
			IEdmNavigationSource navigationSource = path.NavigationSource;
			if (navigationSource == null)
			{
				throw new InvalidOperationException(SRResources.NavigationSourceMissingDuringSerialization);
			}
			ODataSerializerContext odataSerializerContext = new ODataSerializerContext();
			odataSerializerContext.NavigationSource = navigationSource;
			odataSerializerContext.Model = model;
			odataSerializerContext.Url = HttpRequestMessageExtensions.GetUrlHelper(request) ?? new UrlHelper(request);
			odataSerializerContext.MetadataLevel = ODataMetadataLevel.FullMetadata;
			odataSerializerContext.Request = request;
			odataSerializerContext.Path = path;
			IEdmEntityTypeReference entityType = ResultHelpers.GetEntityType(model, entity);
			return ResultHelpers.GenerateODataLink(new ResourceContext(odataSerializerContext, entityType, entity), isEntityId);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00012CEC File Offset: 0x00010EEC
		public static void AddEntityId(HttpResponseMessage response, Func<Uri> entityId)
		{
			if (response.StatusCode == HttpStatusCode.NoContent)
			{
				response.Headers.TryAddWithoutValidation("OData-EntityId", entityId().ToString());
			}
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00012D17 File Offset: 0x00010F17
		public static void AddServiceVersion(HttpResponseMessage response, Func<string> version)
		{
			if (response.StatusCode == HttpStatusCode.NoContent)
			{
				response.Headers.TryAddWithoutValidation("OData-Version", version());
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00012D40 File Offset: 0x00010F40
		internal static ODataVersion GetODataResponseVersion(HttpRequestMessage request)
		{
			if (request == null)
			{
				return ODataVersion.V4;
			}
			HttpRequestMessageProperties httpRequestMessageProperties = request.ODataProperties();
			ODataVersion? odataMaxServiceVersion = httpRequestMessageProperties.ODataMaxServiceVersion;
			if (odataMaxServiceVersion != null)
			{
				return odataMaxServiceVersion.GetValueOrDefault();
			}
			ODataVersion? odataMinServiceVersion = httpRequestMessageProperties.ODataMinServiceVersion;
			if (odataMinServiceVersion == null)
			{
				return httpRequestMessageProperties.ODataServiceVersion.GetValueOrDefault();
			}
			return odataMinServiceVersion.GetValueOrDefault();
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00012D98 File Offset: 0x00010F98
		public static Uri GenerateODataLink(ResourceContext resourceContext, bool isEntityId)
		{
			if (resourceContext.NavigationSource.NavigationSourceKind() == EdmNavigationSourceKind.ContainedEntitySet)
			{
				return ResultHelpers.GenerateContainmentODataPathSegments(resourceContext, isEntityId);
			}
			NavigationSourceLinkBuilderAnnotation navigationSourceLinkBuilder = resourceContext.EdmModel.GetNavigationSourceLinkBuilder(resourceContext.NavigationSource);
			Uri uri = navigationSourceLinkBuilder.BuildIdLink(resourceContext);
			if (isEntityId)
			{
				if (uri == null)
				{
					throw Error.InvalidOperation(SRResources.IdLinkNullForEntityIdHeader, new object[] { resourceContext.NavigationSource.Name });
				}
				return uri;
			}
			else
			{
				Uri uri2 = navigationSourceLinkBuilder.BuildEditLink(resourceContext);
				if (!(uri2 == null))
				{
					return uri2;
				}
				if (uri != null)
				{
					return uri;
				}
				throw Error.InvalidOperation(SRResources.EditLinkNullForLocationHeader, new object[] { resourceContext.NavigationSource.Name });
			}
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00012E40 File Offset: 0x00011040
		private static Uri GenerateContainmentODataPathSegments(ResourceContext resourceContext, bool isEntityId)
		{
			Microsoft.AspNet.OData.Routing.ODataPath odataPath = resourceContext.InternalRequest.Context.Path;
			if (odataPath == null)
			{
				throw Error.InvalidOperation(SRResources.ODataPathMissing, new object[0]);
			}
			odataPath = new ContainmentPathBuilder().TryComputeCanonicalContainingPath(odataPath);
			List<ODataPathSegment> list = odataPath.Segments.ToList<ODataPathSegment>();
			IEdmEntitySet edmEntitySet = resourceContext.NavigationSource as IEdmEntitySet;
			if (edmEntitySet == null)
			{
				edmEntitySet = new EdmEntitySet(new EdmEntityContainer("NS", "Default"), resourceContext.NavigationSource.Name, resourceContext.NavigationSource.EntityType());
			}
			list.Add(new EntitySetSegment(edmEntitySet));
			list.Add(new KeySegment(ConventionsHelpers.GetEntityKey(resourceContext), resourceContext.StructuredType as IEdmEntityType, resourceContext.NavigationSource));
			if (!isEntityId && resourceContext.StructuredType != resourceContext.NavigationSource.EntityType())
			{
				list.Add(new TypeSegment(resourceContext.StructuredType, resourceContext.NavigationSource));
			}
			string text = resourceContext.InternalUrlHelper.CreateODataLink(list);
			if (text != null)
			{
				return new Uri(text);
			}
			return null;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00012F3C File Offset: 0x0001113C
		private static IEdmEntityTypeReference GetEntityType(IEdmModel model, object entity)
		{
			Type type = entity.GetType();
			IEdmTypeReference edmTypeReference = model.GetEdmTypeReference(type);
			if (edmTypeReference == null)
			{
				throw Error.InvalidOperation(SRResources.ResourceTypeNotInModel, new object[] { type.FullName });
			}
			if (!edmTypeReference.IsEntity())
			{
				throw Error.InvalidOperation(SRResources.TypeMustBeEntity, new object[] { edmTypeReference.FullName() });
			}
			return edmTypeReference.AsEntity();
		}

		// Token: 0x04000134 RID: 308
		public const string EntityIdHeaderName = "OData-EntityId";
	}
}
