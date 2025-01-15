using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Evaluation;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000112 RID: 274
	internal sealed class JsonNoMetadataLevel : JsonLightMetadataLevel
	{
		// Token: 0x06000739 RID: 1849 RVA: 0x00018A2B File Offset: 0x00016C2B
		internal override JsonLightTypeNameOracle GetTypeNameOracle(bool autoComputePayloadMetadataInJson)
		{
			if (autoComputePayloadMetadataInJson)
			{
				return new JsonNoMetadataTypeNameOracle();
			}
			return new JsonMinimalMetadataTypeNameOracle();
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00018A3B File Offset: 0x00016C3B
		internal override bool ShouldWriteODataMetadataUri()
		{
			return false;
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00018A3E File Offset: 0x00016C3E
		internal override ODataEntityMetadataBuilder CreateEntityMetadataBuilder(ODataEntry entry, IODataFeedAndEntryTypeContext typeContext, ODataFeedAndEntrySerializationInfo serializationInfo, IEdmEntityType actualEntityType, SelectedPropertiesNode selectedProperties, bool isResponse, bool? keyAsSegment)
		{
			return ODataEntityMetadataBuilder.Null;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00018A45 File Offset: 0x00016C45
		internal override void InjectMetadataBuilder(ODataEntry entry, ODataEntityMetadataBuilder builder)
		{
			entry.MetadataBuilder = builder;
		}
	}
}
