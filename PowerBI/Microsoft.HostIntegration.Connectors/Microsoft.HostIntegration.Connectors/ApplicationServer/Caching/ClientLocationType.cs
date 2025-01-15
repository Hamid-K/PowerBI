using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002FB RID: 763
	internal enum ClientLocationType
	{
		// Token: 0x04000F5D RID: 3933
		External,
		// Token: 0x04000F5E RID: 3934
		CrossRegion,
		// Token: 0x04000F5F RID: 3935
		SameRegion,
		// Token: 0x04000F60 RID: 3936
		SameSubRegion,
		// Token: 0x04000F61 RID: 3937
		SameDatacenter,
		// Token: 0x04000F62 RID: 3938
		None
	}
}
