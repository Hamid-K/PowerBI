using System;
using System.Collections.Generic;
using Microsoft.OData.Core.Evaluation;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000AD RID: 173
	internal sealed class JsonFullMetadataLevel : JsonLightMetadataLevel
	{
		// Token: 0x06000654 RID: 1620 RVA: 0x0001616D File Offset: 0x0001436D
		internal JsonFullMetadataLevel(Uri metadataDocumentUri, IEdmModel model)
		{
			this.metadataDocumentUri = metadataDocumentUri;
			this.model = model;
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x00016183 File Offset: 0x00014383
		internal override ODataContextUrlLevel ContextUrlLevel
		{
			get
			{
				return ODataContextUrlLevel.Full;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x00016186 File Offset: 0x00014386
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

		// Token: 0x06000657 RID: 1623 RVA: 0x000161A7 File Offset: 0x000143A7
		internal override JsonLightTypeNameOracle GetTypeNameOracle(bool autoComputePayloadMetadataInJson)
		{
			if (autoComputePayloadMetadataInJson)
			{
				return new JsonFullMetadataTypeNameOracle();
			}
			return new JsonMinimalMetadataTypeNameOracle();
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x000161B8 File Offset: 0x000143B8
		internal override ODataEntityMetadataBuilder CreateEntityMetadataBuilder(ODataEntry entry, IODataFeedAndEntryTypeContext typeContext, ODataFeedAndEntrySerializationInfo serializationInfo, IEdmEntityType actualEntityType, SelectedPropertiesNode selectedProperties, bool isResponse, bool? keyAsSegment, ODataUri odataUri)
		{
			IODataMetadataContext iodataMetadataContext = new ODataMetadataContext(isResponse, this.model, this.NonNullMetadataDocumentUri, odataUri);
			UrlConvention urlConvention = UrlConvention.ForUserSettingAndTypeContext(keyAsSegment, typeContext);
			ODataConventionalUriBuilder odataConventionalUriBuilder = new ODataConventionalUriBuilder(iodataMetadataContext.ServiceBaseUri, urlConvention);
			IODataEntryMetadataContext iodataEntryMetadataContext = ODataEntryMetadataContext.Create(entry, typeContext, serializationInfo, actualEntityType, iodataMetadataContext, selectedProperties);
			return new ODataConventionalEntityMetadataBuilder(iodataEntryMetadataContext, iodataMetadataContext, odataConventionalUriBuilder);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00016208 File Offset: 0x00014408
		internal override void InjectMetadataBuilder(ODataEntry entry, ODataEntityMetadataBuilder builder)
		{
			base.InjectMetadataBuilder(entry, builder);
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

		// Token: 0x040002DD RID: 733
		private readonly IEdmModel model;

		// Token: 0x040002DE RID: 734
		private readonly Uri metadataDocumentUri;
	}
}
