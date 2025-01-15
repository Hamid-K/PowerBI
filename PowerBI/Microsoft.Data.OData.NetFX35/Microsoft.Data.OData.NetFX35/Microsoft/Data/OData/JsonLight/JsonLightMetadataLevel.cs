using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Evaluation;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x0200010C RID: 268
	internal abstract class JsonLightMetadataLevel
	{
		// Token: 0x0600071F RID: 1823 RVA: 0x000186A0 File Offset: 0x000168A0
		internal static JsonLightMetadataLevel Create(MediaType mediaType, Uri metadataDocumentUri, IEdmModel model, bool writingResponse)
		{
			if (writingResponse && mediaType.Parameters != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in mediaType.Parameters)
				{
					if (HttpUtils.CompareMediaTypeParameterNames(keyValuePair.Key, "odata"))
					{
						if (string.Compare(keyValuePair.Value, "minimalmetadata", 5) == 0)
						{
							return new JsonMinimalMetadataLevel();
						}
						if (string.Compare(keyValuePair.Value, "fullmetadata", 5) == 0)
						{
							return new JsonFullMetadataLevel(metadataDocumentUri, model);
						}
						if (string.Compare(keyValuePair.Value, "nometadata", 5) == 0)
						{
							return new JsonNoMetadataLevel();
						}
					}
				}
			}
			return new JsonMinimalMetadataLevel();
		}

		// Token: 0x06000720 RID: 1824
		internal abstract JsonLightTypeNameOracle GetTypeNameOracle(bool autoComputePayloadMetadataInJson);

		// Token: 0x06000721 RID: 1825
		internal abstract bool ShouldWriteODataMetadataUri();

		// Token: 0x06000722 RID: 1826
		internal abstract ODataEntityMetadataBuilder CreateEntityMetadataBuilder(ODataEntry entry, IODataFeedAndEntryTypeContext typeContext, ODataFeedAndEntrySerializationInfo serializationInfo, IEdmEntityType actualEntityType, SelectedPropertiesNode selectedProperties, bool isResponse, bool? keyAsSegment);

		// Token: 0x06000723 RID: 1827
		internal abstract void InjectMetadataBuilder(ODataEntry entry, ODataEntityMetadataBuilder builder);
	}
}
