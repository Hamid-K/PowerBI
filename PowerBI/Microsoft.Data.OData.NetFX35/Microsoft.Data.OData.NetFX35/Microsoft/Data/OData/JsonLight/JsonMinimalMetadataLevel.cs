using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Evaluation;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000110 RID: 272
	internal sealed class JsonMinimalMetadataLevel : JsonLightMetadataLevel
	{
		// Token: 0x06000731 RID: 1841 RVA: 0x00018970 File Offset: 0x00016B70
		internal override JsonLightTypeNameOracle GetTypeNameOracle(bool autoComputePayloadMetadataInJson)
		{
			return new JsonMinimalMetadataTypeNameOracle();
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00018977 File Offset: 0x00016B77
		internal override bool ShouldWriteODataMetadataUri()
		{
			return true;
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0001897A File Offset: 0x00016B7A
		internal override ODataEntityMetadataBuilder CreateEntityMetadataBuilder(ODataEntry entry, IODataFeedAndEntryTypeContext typeContext, ODataFeedAndEntrySerializationInfo serializationInfo, IEdmEntityType actualEntityType, SelectedPropertiesNode selectedProperties, bool isResponse, bool? keyAsSegment)
		{
			return null;
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0001897D File Offset: 0x00016B7D
		internal override void InjectMetadataBuilder(ODataEntry entry, ODataEntityMetadataBuilder builder)
		{
		}
	}
}
