using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200026E RID: 622
	internal interface ICachedItemBuilder
	{
		// Token: 0x0600165E RID: 5726
		IHierarchicalCachedItem CreateCachedItem(string itemKey, DateTime expirationDate, IHierarchicalCachedItem parent);
	}
}
