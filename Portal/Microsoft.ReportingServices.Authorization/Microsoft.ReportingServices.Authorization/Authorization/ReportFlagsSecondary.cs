using System;

namespace Microsoft.ReportingServices.Authorization
{
	// Token: 0x0200001E RID: 30
	internal enum ReportFlagsSecondary
	{
		// Token: 0x040000D0 RID: 208
		ListHistory = 1,
		// Token: 0x040000D1 RID: 209
		CreateResource,
		// Token: 0x040000D2 RID: 210
		CreateSnapshot = 4,
		// Token: 0x040000D3 RID: 211
		Execute = 8,
		// Token: 0x040000D4 RID: 212
		CreateLink = 16,
		// Token: 0x040000D5 RID: 213
		Comment = 32,
		// Token: 0x040000D6 RID: 214
		ManageComments = 64
	}
}
