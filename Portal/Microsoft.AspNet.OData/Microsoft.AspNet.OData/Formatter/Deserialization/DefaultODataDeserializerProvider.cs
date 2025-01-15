using System;
using System.Net.Http;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Formatter.Deserialization
{
	// Token: 0x020001AE RID: 430
	public class DefaultODataDeserializerProvider : ODataDeserializerProvider
	{
		// Token: 0x06000E46 RID: 3654 RVA: 0x0003A79C File Offset: 0x0003899C
		public override ODataDeserializer GetODataDeserializer(Type type, HttpRequestMessage request)
		{
			return this.GetODataDeserializerImpl(type, () => request.GetModel());
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0003A7C9 File Offset: 0x000389C9
		public DefaultODataDeserializerProvider(IServiceProvider rootContainer)
		{
			if (rootContainer == null)
			{
				throw Error.ArgumentNull("rootContainer");
			}
			this._rootContainer = rootContainer;
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0003A7E8 File Offset: 0x000389E8
		public override ODataEdmTypeDeserializer GetEdmTypeDeserializer(IEdmTypeReference edmType)
		{
			if (edmType == null)
			{
				throw Error.ArgumentNull("edmType");
			}
			switch (edmType.TypeKind())
			{
			case EdmTypeKind.Primitive:
				return ServiceProviderServiceExtensions.GetRequiredService<ODataPrimitiveDeserializer>(this._rootContainer);
			case EdmTypeKind.Entity:
			case EdmTypeKind.Complex:
				return ServiceProviderServiceExtensions.GetRequiredService<ODataResourceDeserializer>(this._rootContainer);
			case EdmTypeKind.Collection:
			{
				IEdmCollectionTypeReference edmCollectionTypeReference = edmType.AsCollection();
				if (edmCollectionTypeReference.ElementType().IsEntity() || edmCollectionTypeReference.ElementType().IsComplex())
				{
					return ServiceProviderServiceExtensions.GetRequiredService<ODataResourceSetDeserializer>(this._rootContainer);
				}
				return ServiceProviderServiceExtensions.GetRequiredService<ODataCollectionDeserializer>(this._rootContainer);
			}
			case EdmTypeKind.Enum:
				return ServiceProviderServiceExtensions.GetRequiredService<ODataEnumDeserializer>(this._rootContainer);
			}
			return null;
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0003A88C File Offset: 0x00038A8C
		internal ODataDeserializer GetODataDeserializerImpl(Type type, Func<IEdmModel> modelFunction)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (modelFunction == null)
			{
				throw Error.ArgumentNull("modelFunction");
			}
			if (type == typeof(Uri))
			{
				return ServiceProviderServiceExtensions.GetRequiredService<ODataEntityReferenceLinkDeserializer>(this._rootContainer);
			}
			if (type == typeof(ODataActionParameters) || type == typeof(ODataUntypedActionParameters))
			{
				return ServiceProviderServiceExtensions.GetRequiredService<ODataActionPayloadDeserializer>(this._rootContainer);
			}
			IEdmModel edmModel = modelFunction();
			IEdmTypeReference edmType = edmModel.GetTypeMappingCache().GetEdmType(type, edmModel);
			if (edmType == null)
			{
				return null;
			}
			return this.GetEdmTypeDeserializer(edmType);
		}

		// Token: 0x04000400 RID: 1024
		private readonly IServiceProvider _rootContainer;
	}
}
