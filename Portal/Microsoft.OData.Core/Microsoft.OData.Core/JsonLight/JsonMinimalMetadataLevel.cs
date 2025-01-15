using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Evaluation;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000233 RID: 563
	internal sealed class JsonMinimalMetadataLevel : JsonLightMetadataLevel
	{
		// Token: 0x06001881 RID: 6273 RVA: 0x00046402 File Offset: 0x00044602
		internal override JsonLightTypeNameOracle GetTypeNameOracle()
		{
			return new JsonMinimalMetadataTypeNameOracle();
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x0000360D File Offset: 0x0000180D
		internal override ODataResourceMetadataBuilder CreateResourceMetadataBuilder(ODataResourceBase resource, IODataResourceTypeContext typeContext, ODataResourceSerializationInfo serializationInfo, IEdmStructuredType actualResourceType, SelectedPropertiesNode selectedProperties, bool isResponse, bool keyAsSegment, ODataUri odataUri, ODataMessageWriterSettings settings)
		{
			return null;
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x0000239D File Offset: 0x0000059D
		internal override void InjectMetadataBuilder(ODataResourceBase resource, ODataResourceMetadataBuilder builder)
		{
		}
	}
}
