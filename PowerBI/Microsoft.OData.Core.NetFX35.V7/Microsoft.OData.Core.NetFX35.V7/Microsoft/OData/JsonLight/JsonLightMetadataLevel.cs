using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.Evaluation;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x020001F7 RID: 503
	internal abstract class JsonLightMetadataLevel
	{
		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x0600139E RID: 5022
		internal abstract ODataContextUrlLevel ContextUrlLevel { get; }

		// Token: 0x0600139F RID: 5023 RVA: 0x00038490 File Offset: 0x00036690
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

		// Token: 0x060013A0 RID: 5024
		internal abstract JsonLightTypeNameOracle GetTypeNameOracle();

		// Token: 0x060013A1 RID: 5025
		internal abstract ODataResourceMetadataBuilder CreateResourceMetadataBuilder(ODataResource resource, IODataResourceTypeContext typeContext, ODataResourceSerializationInfo serializationInfo, IEdmStructuredType actualResourceType, SelectedPropertiesNode selectedProperties, bool isResponse, bool keyAsSegment, ODataUri odataUri);

		// Token: 0x060013A2 RID: 5026 RVA: 0x00038558 File Offset: 0x00036758
		internal virtual void InjectMetadataBuilder(ODataResource resource, ODataResourceMetadataBuilder builder)
		{
			resource.MetadataBuilder = builder;
		}
	}
}
