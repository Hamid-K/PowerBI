using System;

namespace Microsoft.OData
{
	// Token: 0x02000016 RID: 22
	internal interface IDuplicatePropertyNameChecker
	{
		// Token: 0x060000FE RID: 254
		void ValidatePropertyUniqueness(ODataPropertyInfo property);

		// Token: 0x060000FF RID: 255
		void ValidatePropertyUniqueness(ODataNestedResourceInfo property);

		// Token: 0x06000100 RID: 256
		void ValidatePropertyOpenForAssociationLink(string propertyName);

		// Token: 0x06000101 RID: 257
		void Reset();
	}
}
