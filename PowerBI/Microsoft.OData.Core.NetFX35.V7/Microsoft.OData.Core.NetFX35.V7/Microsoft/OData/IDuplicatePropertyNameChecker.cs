using System;

namespace Microsoft.OData
{
	// Token: 0x020000BF RID: 191
	internal interface IDuplicatePropertyNameChecker
	{
		// Token: 0x0600076A RID: 1898
		void ValidatePropertyUniqueness(ODataProperty property);

		// Token: 0x0600076B RID: 1899
		void ValidatePropertyUniqueness(ODataNestedResourceInfo property);

		// Token: 0x0600076C RID: 1900
		void ValidatePropertyOpenForAssociationLink(string propertyName);

		// Token: 0x0600076D RID: 1901
		void Reset();
	}
}
