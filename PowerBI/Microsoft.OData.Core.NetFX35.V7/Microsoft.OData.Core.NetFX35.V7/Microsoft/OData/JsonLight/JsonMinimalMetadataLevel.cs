using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Evaluation;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x020001FA RID: 506
	internal sealed class JsonMinimalMetadataLevel : JsonLightMetadataLevel
	{
		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x00002503 File Offset: 0x00000703
		internal override ODataContextUrlLevel ContextUrlLevel
		{
			get
			{
				return ODataContextUrlLevel.OnDemand;
			}
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x0003863E File Offset: 0x0003683E
		internal override JsonLightTypeNameOracle GetTypeNameOracle()
		{
			return new JsonMinimalMetadataTypeNameOracle();
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x0000B41B File Offset: 0x0000961B
		internal override ODataResourceMetadataBuilder CreateResourceMetadataBuilder(ODataResource resource, IODataResourceTypeContext typeContext, ODataResourceSerializationInfo serializationInfo, IEdmStructuredType actualResourceType, SelectedPropertiesNode selectedProperties, bool isResponse, bool keyAsSegment, ODataUri odataUri)
		{
			return null;
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0000250D File Offset: 0x0000070D
		internal override void InjectMetadataBuilder(ODataResource resource, ODataResourceMetadataBuilder builder)
		{
		}
	}
}
