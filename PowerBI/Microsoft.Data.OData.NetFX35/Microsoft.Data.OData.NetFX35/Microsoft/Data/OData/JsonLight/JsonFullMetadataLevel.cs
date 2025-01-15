using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Evaluation;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x0200010D RID: 269
	internal sealed class JsonFullMetadataLevel : JsonLightMetadataLevel
	{
		// Token: 0x06000725 RID: 1829 RVA: 0x00018770 File Offset: 0x00016970
		internal JsonFullMetadataLevel(Uri metadataDocumentUri, IEdmModel model)
		{
			this.metadataDocumentUri = metadataDocumentUri;
			this.model = model;
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x00018786 File Offset: 0x00016986
		private Uri NonNullMetadataDocumentUri
		{
			get
			{
				if (this.metadataDocumentUri == null)
				{
					throw new ODataException(Strings.ODataJsonLightOutputContext_MetadataDocumentUriMissing);
				}
				return this.metadataDocumentUri;
			}
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x000187A7 File Offset: 0x000169A7
		internal override JsonLightTypeNameOracle GetTypeNameOracle(bool autoComputePayloadMetadataInJson)
		{
			if (autoComputePayloadMetadataInJson)
			{
				return new JsonFullMetadataTypeNameOracle();
			}
			return new JsonMinimalMetadataTypeNameOracle();
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x000187B7 File Offset: 0x000169B7
		internal override bool ShouldWriteODataMetadataUri()
		{
			return true;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x000187BC File Offset: 0x000169BC
		internal override ODataEntityMetadataBuilder CreateEntityMetadataBuilder(ODataEntry entry, IODataFeedAndEntryTypeContext typeContext, ODataFeedAndEntrySerializationInfo serializationInfo, IEdmEntityType actualEntityType, SelectedPropertiesNode selectedProperties, bool isResponse, bool? keyAsSegment)
		{
			IODataMetadataContext iodataMetadataContext = new ODataMetadataContext(isResponse, this.model, this.NonNullMetadataDocumentUri);
			UrlConvention urlConvention = UrlConvention.ForUserSettingAndTypeContext(keyAsSegment, typeContext);
			ODataConventionalUriBuilder odataConventionalUriBuilder = new ODataConventionalUriBuilder(iodataMetadataContext.ServiceBaseUri, urlConvention);
			IODataEntryMetadataContext iodataEntryMetadataContext = ODataEntryMetadataContext.Create(entry, typeContext, serializationInfo, actualEntityType, iodataMetadataContext, selectedProperties);
			return new ODataConventionalEntityMetadataBuilder(iodataEntryMetadataContext, iodataMetadataContext, odataConventionalUriBuilder);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0001880C File Offset: 0x00016A0C
		internal override void InjectMetadataBuilder(ODataEntry entry, ODataEntityMetadataBuilder builder)
		{
			entry.MetadataBuilder = builder;
			ODataStreamReferenceValue nonComputedMediaResource = entry.NonComputedMediaResource;
			if (nonComputedMediaResource != null)
			{
				nonComputedMediaResource.SetMetadataBuilder(builder, null);
			}
			if (entry.NonComputedProperties != null)
			{
				foreach (ODataProperty odataProperty in entry.NonComputedProperties)
				{
					ODataStreamReferenceValue odataStreamReferenceValue = odataProperty.ODataValue as ODataStreamReferenceValue;
					if (odataStreamReferenceValue != null)
					{
						odataStreamReferenceValue.SetMetadataBuilder(builder, odataProperty.Name);
					}
				}
			}
			IEnumerable<ODataOperation> enumerable = ODataUtilsInternal.ConcatEnumerables<ODataOperation>((IEnumerable<ODataOperation>)entry.NonComputedActions, (IEnumerable<ODataOperation>)entry.NonComputedFunctions);
			if (enumerable != null)
			{
				foreach (ODataOperation odataOperation in enumerable)
				{
					odataOperation.SetMetadataBuilder(builder, this.NonNullMetadataDocumentUri);
				}
			}
		}

		// Token: 0x040002C2 RID: 706
		private readonly IEdmModel model;

		// Token: 0x040002C3 RID: 707
		private readonly Uri metadataDocumentUri;
	}
}
