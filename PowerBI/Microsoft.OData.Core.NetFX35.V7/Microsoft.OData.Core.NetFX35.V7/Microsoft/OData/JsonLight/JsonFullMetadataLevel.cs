using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.Evaluation;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x020001F4 RID: 500
	internal sealed class JsonFullMetadataLevel : JsonLightMetadataLevel
	{
		// Token: 0x06001393 RID: 5011 RVA: 0x00038219 File Offset: 0x00036419
		internal JsonFullMetadataLevel(Uri metadataDocumentUri, IEdmModel model)
		{
			this.metadataDocumentUri = metadataDocumentUri;
			this.model = model;
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001394 RID: 5012 RVA: 0x0002900C File Offset: 0x0002720C
		internal override ODataContextUrlLevel ContextUrlLevel
		{
			get
			{
				return ODataContextUrlLevel.Full;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001395 RID: 5013 RVA: 0x0003822F File Offset: 0x0003642F
		private Uri NonNullMetadataDocumentUri
		{
			get
			{
				if (this.metadataDocumentUri == null)
				{
					throw new ODataException(Strings.ODataOutputContext_MetadataDocumentUriMissing);
				}
				return this.metadataDocumentUri;
			}
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x00038250 File Offset: 0x00036450
		internal override JsonLightTypeNameOracle GetTypeNameOracle()
		{
			return new JsonFullMetadataTypeNameOracle();
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x00038258 File Offset: 0x00036458
		internal override ODataResourceMetadataBuilder CreateResourceMetadataBuilder(ODataResource resource, IODataResourceTypeContext typeContext, ODataResourceSerializationInfo serializationInfo, IEdmStructuredType actualResourceType, SelectedPropertiesNode selectedProperties, bool isResponse, bool keyAsSegment, ODataUri odataUri)
		{
			IODataMetadataContext iodataMetadataContext = new ODataMetadataContext(isResponse, this.model, this.NonNullMetadataDocumentUri, odataUri);
			ODataConventionalUriBuilder odataConventionalUriBuilder = new ODataConventionalUriBuilder(iodataMetadataContext.ServiceBaseUri, keyAsSegment ? ODataUrlKeyDelimiter.Slash : ODataUrlKeyDelimiter.Parentheses);
			IODataResourceMetadataContext iodataResourceMetadataContext = ODataResourceMetadataContext.Create(resource, typeContext, serializationInfo, actualResourceType, iodataMetadataContext, selectedProperties);
			if ((actualResourceType != null && actualResourceType.TypeKind == EdmTypeKind.Entity) || (actualResourceType == null && typeContext.NavigationSourceKind != EdmNavigationSourceKind.None))
			{
				return new ODataConventionalEntityMetadataBuilder(iodataResourceMetadataContext, iodataMetadataContext, odataConventionalUriBuilder);
			}
			return new ODataConventionalResourceMetadataBuilder(iodataResourceMetadataContext, iodataMetadataContext, odataConventionalUriBuilder);
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x000382D0 File Offset: 0x000364D0
		internal override void InjectMetadataBuilder(ODataResource resource, ODataResourceMetadataBuilder builder)
		{
			base.InjectMetadataBuilder(resource, builder);
			ODataStreamReferenceValue nonComputedMediaResource = resource.NonComputedMediaResource;
			if (nonComputedMediaResource != null)
			{
				nonComputedMediaResource.SetMetadataBuilder(builder, null);
			}
			if (resource.NonComputedProperties != null)
			{
				foreach (ODataProperty odataProperty in resource.NonComputedProperties)
				{
					ODataStreamReferenceValue odataStreamReferenceValue = odataProperty.ODataValue as ODataStreamReferenceValue;
					if (odataStreamReferenceValue != null)
					{
						odataStreamReferenceValue.SetMetadataBuilder(builder, odataProperty.Name);
					}
				}
			}
			IEnumerable<ODataOperation> enumerable = ODataUtilsInternal.ConcatEnumerables<ODataOperation>((IEnumerable<ODataOperation>)resource.NonComputedActions, (IEnumerable<ODataOperation>)resource.NonComputedFunctions);
			if (enumerable != null)
			{
				foreach (ODataOperation odataOperation in enumerable)
				{
					odataOperation.SetMetadataBuilder(builder, this.NonNullMetadataDocumentUri);
				}
			}
		}

		// Token: 0x040009C8 RID: 2504
		private readonly IEdmModel model;

		// Token: 0x040009C9 RID: 2505
		private readonly Uri metadataDocumentUri;
	}
}
