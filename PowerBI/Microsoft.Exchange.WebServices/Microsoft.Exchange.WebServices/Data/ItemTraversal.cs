using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000215 RID: 533
	public enum ItemTraversal
	{
		// Token: 0x04000E84 RID: 3716
		Shallow,
		// Token: 0x04000E85 RID: 3717
		SoftDeleted,
		// Token: 0x04000E86 RID: 3718
		[RequiredServerVersion(ExchangeVersion.Exchange2010)]
		Associated
	}
}
