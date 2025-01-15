using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000201 RID: 513
	[Flags]
	public enum EffectiveRights
	{
		// Token: 0x04000DCC RID: 3532
		None = 0,
		// Token: 0x04000DCD RID: 3533
		CreateAssociated = 1,
		// Token: 0x04000DCE RID: 3534
		CreateContents = 2,
		// Token: 0x04000DCF RID: 3535
		CreateHierarchy = 4,
		// Token: 0x04000DD0 RID: 3536
		Delete = 8,
		// Token: 0x04000DD1 RID: 3537
		Modify = 16,
		// Token: 0x04000DD2 RID: 3538
		Read = 32,
		// Token: 0x04000DD3 RID: 3539
		ViewPrivateItems = 64
	}
}
