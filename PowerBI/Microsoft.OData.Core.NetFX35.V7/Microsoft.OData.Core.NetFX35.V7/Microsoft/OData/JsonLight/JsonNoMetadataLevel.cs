using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Evaluation;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x020001FC RID: 508
	internal sealed class JsonNoMetadataLevel : JsonLightMetadataLevel
	{
		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x00002500 File Offset: 0x00000700
		internal override ODataContextUrlLevel ContextUrlLevel
		{
			get
			{
				return ODataContextUrlLevel.None;
			}
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x00038806 File Offset: 0x00036A06
		internal override JsonLightTypeNameOracle GetTypeNameOracle()
		{
			return new JsonNoMetadataTypeNameOracle();
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x0003880D File Offset: 0x00036A0D
		internal override ODataResourceMetadataBuilder CreateResourceMetadataBuilder(ODataResource resource, IODataResourceTypeContext typeContext, ODataResourceSerializationInfo serializationInfo, IEdmStructuredType actualResourceType, SelectedPropertiesNode selectedProperties, bool isResponse, bool keyAsSegment, ODataUri odataUri)
		{
			return ODataResourceMetadataBuilder.Null;
		}
	}
}
