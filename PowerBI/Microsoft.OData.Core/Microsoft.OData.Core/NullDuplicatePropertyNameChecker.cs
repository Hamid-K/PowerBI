using System;

namespace Microsoft.OData
{
	// Token: 0x02000021 RID: 33
	internal class NullDuplicatePropertyNameChecker : IDuplicatePropertyNameChecker
	{
		// Token: 0x06000159 RID: 345 RVA: 0x0000239D File Offset: 0x0000059D
		public void ValidatePropertyUniqueness(ODataPropertyInfo property)
		{
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000239D File Offset: 0x0000059D
		public void ValidatePropertyUniqueness(ODataNestedResourceInfo property)
		{
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000239D File Offset: 0x0000059D
		public void ValidatePropertyOpenForAssociationLink(string propertyName)
		{
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000239D File Offset: 0x0000059D
		public void Reset()
		{
		}
	}
}
