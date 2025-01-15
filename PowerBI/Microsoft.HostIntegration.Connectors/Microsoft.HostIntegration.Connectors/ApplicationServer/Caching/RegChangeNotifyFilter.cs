using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200036B RID: 875
	[Flags]
	internal enum RegChangeNotifyFilter
	{
		// Token: 0x0400116A RID: 4458
		Key = 1,
		// Token: 0x0400116B RID: 4459
		Attribute = 2,
		// Token: 0x0400116C RID: 4460
		Value = 4,
		// Token: 0x0400116D RID: 4461
		Security = 8,
		// Token: 0x0400116E RID: 4462
		All = 15
	}
}
