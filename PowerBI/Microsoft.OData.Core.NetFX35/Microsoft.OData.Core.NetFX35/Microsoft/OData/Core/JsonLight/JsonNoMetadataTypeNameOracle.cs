using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000B5 RID: 181
	internal sealed class JsonNoMetadataTypeNameOracle : JsonLightTypeNameOracle
	{
		// Token: 0x06000674 RID: 1652 RVA: 0x0001653B File Offset: 0x0001473B
		internal override string GetEntryTypeNameForWriting(string expectedTypeName, ODataEntry entry)
		{
			return null;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001653E File Offset: 0x0001473E
		internal override string GetValueTypeNameForWriting(ODataValue value, IEdmTypeReference typeReferenceFromMetadata, IEdmTypeReference typeReferenceFromValue, bool isOpenProperty)
		{
			return null;
		}
	}
}
