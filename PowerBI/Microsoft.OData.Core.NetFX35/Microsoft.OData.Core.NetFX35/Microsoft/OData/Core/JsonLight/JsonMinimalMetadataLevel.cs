using System;
using Microsoft.OData.Core.Evaluation;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000B2 RID: 178
	internal sealed class JsonMinimalMetadataLevel : JsonLightMetadataLevel
	{
		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x00016431 File Offset: 0x00014631
		internal override ODataContextUrlLevel ContextUrlLevel
		{
			get
			{
				return ODataContextUrlLevel.OnDemand;
			}
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00016434 File Offset: 0x00014634
		internal override JsonLightTypeNameOracle GetTypeNameOracle(bool autoComputePayloadMetadataInJson)
		{
			return new JsonMinimalMetadataTypeNameOracle();
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001643B File Offset: 0x0001463B
		internal override ODataEntityMetadataBuilder CreateEntityMetadataBuilder(ODataEntry entry, IODataFeedAndEntryTypeContext typeContext, ODataFeedAndEntrySerializationInfo serializationInfo, IEdmEntityType actualEntityType, SelectedPropertiesNode selectedProperties, bool isResponse, bool? keyAsSegment, ODataUri odataUri)
		{
			return null;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0001643E File Offset: 0x0001463E
		internal override void InjectMetadataBuilder(ODataEntry entry, ODataEntityMetadataBuilder builder)
		{
		}
	}
}
