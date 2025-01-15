using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.Evaluation;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x0200022D RID: 557
	internal sealed class JsonFullMetadataLevel : JsonLightMetadataLevel
	{
		// Token: 0x06001866 RID: 6246 RVA: 0x00045FD1 File Offset: 0x000441D1
		internal JsonFullMetadataLevel(Uri metadataDocumentUri, IEdmModel model)
		{
			this.metadataDocumentUri = metadataDocumentUri;
			this.model = model;
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001867 RID: 6247 RVA: 0x00045FE7 File Offset: 0x000441E7
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

		// Token: 0x06001868 RID: 6248 RVA: 0x00046008 File Offset: 0x00044208
		internal override JsonLightTypeNameOracle GetTypeNameOracle()
		{
			return new JsonFullMetadataTypeNameOracle();
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x00046010 File Offset: 0x00044210
		internal override ODataResourceMetadataBuilder CreateResourceMetadataBuilder(ODataResourceBase resource, IODataResourceTypeContext typeContext, ODataResourceSerializationInfo serializationInfo, IEdmStructuredType actualResourceType, SelectedPropertiesNode selectedProperties, bool isResponse, bool keyAsSegment, ODataUri odataUri, ODataMessageWriterSettings settings)
		{
			IODataMetadataContext iodataMetadataContext = new ODataMetadataContext(isResponse, this.model, this.NonNullMetadataDocumentUri, odataUri);
			ODataConventionalUriBuilder odataConventionalUriBuilder = new ODataConventionalUriBuilder(iodataMetadataContext.ServiceBaseUri, keyAsSegment ? ODataUrlKeyDelimiter.Slash : ODataUrlKeyDelimiter.Parentheses);
			IODataResourceMetadataContext iodataResourceMetadataContext = ODataResourceMetadataContext.Create(resource, typeContext, serializationInfo, actualResourceType, iodataMetadataContext, selectedProperties, (settings != null) ? settings.MetadataSelector : null);
			if ((actualResourceType != null && actualResourceType.TypeKind == EdmTypeKind.Entity) || (actualResourceType == null && typeContext.NavigationSourceKind != EdmNavigationSourceKind.None))
			{
				return new ODataConventionalEntityMetadataBuilder(iodataResourceMetadataContext, iodataMetadataContext, odataConventionalUriBuilder);
			}
			return new ODataConventionalResourceMetadataBuilder(iodataResourceMetadataContext, iodataMetadataContext, odataConventionalUriBuilder);
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x00046098 File Offset: 0x00044298
		internal override void InjectMetadataBuilder(ODataResourceBase resource, ODataResourceMetadataBuilder builder)
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
			IEnumerable<ODataOperation> enumerable = ODataUtilsInternal.ConcatEnumerables<ODataOperation>(resource.NonComputedActions, resource.NonComputedFunctions);
			if (enumerable != null)
			{
				foreach (ODataOperation odataOperation in enumerable)
				{
					odataOperation.SetMetadataBuilder(builder, this.NonNullMetadataDocumentUri);
				}
			}
		}

		// Token: 0x04000ADE RID: 2782
		private readonly IEdmModel model;

		// Token: 0x04000ADF RID: 2783
		private readonly Uri metadataDocumentUri;
	}
}
