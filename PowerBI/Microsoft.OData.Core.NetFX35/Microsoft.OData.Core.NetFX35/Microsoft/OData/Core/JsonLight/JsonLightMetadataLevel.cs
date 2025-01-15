using System;
using System.Collections.Generic;
using Microsoft.OData.Core.Evaluation;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000AC RID: 172
	internal abstract class JsonLightMetadataLevel
	{
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600064E RID: 1614
		internal abstract ODataContextUrlLevel ContextUrlLevel { get; }

		// Token: 0x0600064F RID: 1615 RVA: 0x00016094 File Offset: 0x00014294
		internal static JsonLightMetadataLevel Create(ODataMediaType mediaType, Uri metadataDocumentUri, IEdmModel model, bool writingResponse)
		{
			if (writingResponse && mediaType.Parameters != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in mediaType.Parameters)
				{
					if (HttpUtils.CompareMediaTypeParameterNames(keyValuePair.Key, "odata.metadata"))
					{
						if (string.Compare(keyValuePair.Value, "minimal", 5) == 0)
						{
							return new JsonMinimalMetadataLevel();
						}
						if (string.Compare(keyValuePair.Value, "full", 5) == 0)
						{
							return new JsonFullMetadataLevel(metadataDocumentUri, model);
						}
						if (string.Compare(keyValuePair.Value, "none", 5) == 0)
						{
							return new JsonNoMetadataLevel();
						}
					}
				}
			}
			return new JsonMinimalMetadataLevel();
		}

		// Token: 0x06000650 RID: 1616
		internal abstract JsonLightTypeNameOracle GetTypeNameOracle(bool autoComputePayloadMetadataInJson);

		// Token: 0x06000651 RID: 1617
		internal abstract ODataEntityMetadataBuilder CreateEntityMetadataBuilder(ODataEntry entry, IODataFeedAndEntryTypeContext typeContext, ODataFeedAndEntrySerializationInfo serializationInfo, IEdmEntityType actualEntityType, SelectedPropertiesNode selectedProperties, bool isResponse, bool? keyAsSegment, ODataUri odataUri);

		// Token: 0x06000652 RID: 1618 RVA: 0x0001615C File Offset: 0x0001435C
		internal virtual void InjectMetadataBuilder(ODataEntry entry, ODataEntityMetadataBuilder builder)
		{
			entry.MetadataBuilder = builder;
		}
	}
}
