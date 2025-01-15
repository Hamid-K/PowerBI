using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.Evaluation;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000230 RID: 560
	internal abstract class JsonLightMetadataLevel
	{
		// Token: 0x06001870 RID: 6256 RVA: 0x00046244 File Offset: 0x00044444
		internal static JsonLightMetadataLevel Create(ODataMediaType mediaType, Uri metadataDocumentUri, IEdmModel model, bool writingResponse)
		{
			if (writingResponse && mediaType.Parameters != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in mediaType.Parameters)
				{
					if (HttpUtils.IsMetadataParameter(keyValuePair.Key))
					{
						if (string.Compare(keyValuePair.Value, "minimal", StringComparison.OrdinalIgnoreCase) == 0)
						{
							return new JsonMinimalMetadataLevel();
						}
						if (string.Compare(keyValuePair.Value, "full", StringComparison.OrdinalIgnoreCase) == 0)
						{
							return new JsonFullMetadataLevel(metadataDocumentUri, model);
						}
						if (string.Compare(keyValuePair.Value, "none", StringComparison.OrdinalIgnoreCase) == 0)
						{
							return new JsonNoMetadataLevel();
						}
					}
				}
			}
			return new JsonMinimalMetadataLevel();
		}

		// Token: 0x06001871 RID: 6257
		internal abstract JsonLightTypeNameOracle GetTypeNameOracle();

		// Token: 0x06001872 RID: 6258
		internal abstract ODataResourceMetadataBuilder CreateResourceMetadataBuilder(ODataResourceBase resource, IODataResourceTypeContext typeContext, ODataResourceSerializationInfo serializationInfo, IEdmStructuredType actualResourceType, SelectedPropertiesNode selectedProperties, bool isResponse, bool keyAsSegment, ODataUri odataUri, ODataMessageWriterSettings settings);

		// Token: 0x06001873 RID: 6259 RVA: 0x00046308 File Offset: 0x00044508
		internal virtual void InjectMetadataBuilder(ODataResourceBase resource, ODataResourceMetadataBuilder builder)
		{
			resource.MetadataBuilder = builder;
		}
	}
}
