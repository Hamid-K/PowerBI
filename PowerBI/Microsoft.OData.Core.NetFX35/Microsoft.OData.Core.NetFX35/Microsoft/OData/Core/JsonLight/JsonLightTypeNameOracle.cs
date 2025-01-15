using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000AE RID: 174
	internal abstract class JsonLightTypeNameOracle : TypeNameOracle
	{
		// Token: 0x0600065A RID: 1626
		internal abstract string GetEntryTypeNameForWriting(string expectedTypeName, ODataEntry entry);

		// Token: 0x0600065B RID: 1627
		internal abstract string GetValueTypeNameForWriting(ODataValue value, IEdmTypeReference typeReferenceFromMetadata, IEdmTypeReference typeReferenceFromValue, bool isOpenProperty);
	}
}
