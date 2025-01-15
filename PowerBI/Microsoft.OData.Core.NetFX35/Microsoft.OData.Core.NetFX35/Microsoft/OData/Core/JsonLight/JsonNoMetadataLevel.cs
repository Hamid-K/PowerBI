using System;
using Microsoft.OData.Core.Evaluation;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000B4 RID: 180
	internal sealed class JsonNoMetadataLevel : JsonLightMetadataLevel
	{
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x00016519 File Offset: 0x00014719
		internal override ODataContextUrlLevel ContextUrlLevel
		{
			get
			{
				return ODataContextUrlLevel.None;
			}
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0001651C File Offset: 0x0001471C
		internal override JsonLightTypeNameOracle GetTypeNameOracle(bool autoComputePayloadMetadataInJson)
		{
			if (autoComputePayloadMetadataInJson)
			{
				return new JsonNoMetadataTypeNameOracle();
			}
			return new JsonMinimalMetadataTypeNameOracle();
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001652C File Offset: 0x0001472C
		internal override ODataEntityMetadataBuilder CreateEntityMetadataBuilder(ODataEntry entry, IODataFeedAndEntryTypeContext typeContext, ODataFeedAndEntrySerializationInfo serializationInfo, IEdmEntityType actualEntityType, SelectedPropertiesNode selectedProperties, bool isResponse, bool? keyAsSegment, ODataUri odataUri)
		{
			return ODataEntityMetadataBuilder.Null;
		}
	}
}
