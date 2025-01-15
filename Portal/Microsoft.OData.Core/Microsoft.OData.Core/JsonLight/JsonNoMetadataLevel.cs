using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Evaluation;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000235 RID: 565
	internal sealed class JsonNoMetadataLevel : JsonLightMetadataLevel
	{
		// Token: 0x0600188A RID: 6282 RVA: 0x000465CA File Offset: 0x000447CA
		internal override JsonLightTypeNameOracle GetTypeNameOracle()
		{
			return new JsonNoMetadataTypeNameOracle();
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x000465D1 File Offset: 0x000447D1
		internal override ODataResourceMetadataBuilder CreateResourceMetadataBuilder(ODataResourceBase resource, IODataResourceTypeContext typeContext, ODataResourceSerializationInfo serializationInfo, IEdmStructuredType actualResourceType, SelectedPropertiesNode selectedProperties, bool isResponse, bool keyAsSegment, ODataUri odataUri, ODataMessageWriterSettings settings)
		{
			return ODataResourceMetadataBuilder.Null;
		}
	}
}
