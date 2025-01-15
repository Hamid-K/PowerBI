using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000113 RID: 275
	internal sealed class JsonNoMetadataTypeNameOracle : JsonLightTypeNameOracle
	{
		// Token: 0x0600073E RID: 1854 RVA: 0x00018A56 File Offset: 0x00016C56
		internal override string GetEntryTypeNameForWriting(string expectedTypeName, ODataEntry entry)
		{
			return null;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00018A59 File Offset: 0x00016C59
		internal override string GetValueTypeNameForWriting(ODataValue value, IEdmTypeReference typeReferenceFromMetadata, IEdmTypeReference typeReferenceFromValue, bool isOpenProperty)
		{
			return null;
		}
	}
}
