using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Formatter.Serialization
{
	// Token: 0x02000197 RID: 407
	public class DefaultODataSerializerProvider : ODataSerializerProvider
	{
		// Token: 0x06000D63 RID: 3427 RVA: 0x0003606C File Offset: 0x0003426C
		public override ODataSerializer GetODataPayloadSerializer(Type type, HttpRequestMessage request)
		{
			return this.GetODataPayloadSerializerImpl(type, () => request.GetModel(), request.ODataProperties().Path, typeof(HttpError));
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x000360B3 File Offset: 0x000342B3
		public DefaultODataSerializerProvider(IServiceProvider rootContainer)
		{
			if (rootContainer == null)
			{
				throw Error.ArgumentNull("rootContainer");
			}
			this._rootContainer = rootContainer;
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x000360D0 File Offset: 0x000342D0
		public override ODataEdmTypeSerializer GetEdmTypeSerializer(IEdmTypeReference edmType)
		{
			if (edmType == null)
			{
				throw Error.ArgumentNull("edmType");
			}
			switch (edmType.TypeKind())
			{
			case EdmTypeKind.Primitive:
				return ServiceProviderServiceExtensions.GetRequiredService<ODataPrimitiveSerializer>(this._rootContainer);
			case EdmTypeKind.Entity:
			case EdmTypeKind.Complex:
				return ServiceProviderServiceExtensions.GetRequiredService<ODataResourceSerializer>(this._rootContainer);
			case EdmTypeKind.Collection:
			{
				IEdmCollectionTypeReference edmCollectionTypeReference = edmType.AsCollection();
				if (edmCollectionTypeReference.Definition.IsDeltaFeed())
				{
					return ServiceProviderServiceExtensions.GetRequiredService<ODataDeltaFeedSerializer>(this._rootContainer);
				}
				if (edmCollectionTypeReference.ElementType().IsEntity() || edmCollectionTypeReference.ElementType().IsComplex())
				{
					return ServiceProviderServiceExtensions.GetRequiredService<ODataResourceSetSerializer>(this._rootContainer);
				}
				return ServiceProviderServiceExtensions.GetRequiredService<ODataCollectionSerializer>(this._rootContainer);
			}
			case EdmTypeKind.Enum:
				return ServiceProviderServiceExtensions.GetRequiredService<ODataEnumSerializer>(this._rootContainer);
			}
			return null;
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0003618C File Offset: 0x0003438C
		internal ODataSerializer GetODataPayloadSerializerImpl(Type type, Func<IEdmModel> modelFunction, Microsoft.AspNet.OData.Routing.ODataPath path, Type errorType)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (modelFunction == null)
			{
				throw Error.ArgumentNull("modelFunction");
			}
			if (type == typeof(ODataServiceDocument))
			{
				return ServiceProviderServiceExtensions.GetRequiredService<ODataServiceDocumentSerializer>(this._rootContainer);
			}
			if (type == typeof(Uri) || type == typeof(ODataEntityReferenceLink))
			{
				return ServiceProviderServiceExtensions.GetRequiredService<ODataEntityReferenceLinkSerializer>(this._rootContainer);
			}
			if (TypeHelper.IsTypeAssignableFrom(typeof(IEnumerable<Uri>), type) || type == typeof(ODataEntityReferenceLinks))
			{
				return ServiceProviderServiceExtensions.GetRequiredService<ODataEntityReferenceLinksSerializer>(this._rootContainer);
			}
			if (type == typeof(ODataError) || type == errorType)
			{
				return ServiceProviderServiceExtensions.GetRequiredService<ODataErrorSerializer>(this._rootContainer);
			}
			if (TypeHelper.IsTypeAssignableFrom(typeof(IEdmModel), type))
			{
				return ServiceProviderServiceExtensions.GetRequiredService<ODataMetadataSerializer>(this._rootContainer);
			}
			IEdmModel edmModel = modelFunction();
			IEdmTypeReference edmType = edmModel.GetTypeMappingCache().GetEdmType(type, edmModel);
			if (edmType == null)
			{
				return null;
			}
			bool flag = path != null && path.Segments.LastOrDefault<ODataPathSegment>() is CountSegment;
			bool flag2 = path != null && path.Segments.LastOrDefault<ODataPathSegment>() is ValueSegment;
			if (((edmType.IsPrimitive() || edmType.IsEnum()) && flag2) || flag)
			{
				return ServiceProviderServiceExtensions.GetRequiredService<ODataRawValueSerializer>(this._rootContainer);
			}
			return this.GetEdmTypeSerializer(edmType);
		}

		// Token: 0x040003D3 RID: 979
		private readonly IServiceProvider _rootContainer;
	}
}
