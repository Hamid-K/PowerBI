using System;

namespace Microsoft.OData
{
	// Token: 0x020000BC RID: 188
	internal class NullDuplicatePropertyNameChecker : IDuplicatePropertyNameChecker
	{
		// Token: 0x06000761 RID: 1889 RVA: 0x0000250D File Offset: 0x0000070D
		public void ValidatePropertyUniqueness(ODataProperty property)
		{
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0000250D File Offset: 0x0000070D
		public void ValidatePropertyUniqueness(ODataNestedResourceInfo property)
		{
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0000250D File Offset: 0x0000070D
		public void ValidatePropertyOpenForAssociationLink(string propertyName)
		{
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0000250D File Offset: 0x0000070D
		public void Reset()
		{
		}
	}
}
