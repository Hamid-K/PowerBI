using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200003F RID: 63
	internal interface ISqlConfigurableRetryConnectionSection
	{
		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06000762 RID: 1890
		// (set) Token: 0x06000763 RID: 1891
		TimeSpan DeltaTime { get; set; }

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06000764 RID: 1892
		// (set) Token: 0x06000765 RID: 1893
		TimeSpan MaxTimeInterval { get; set; }

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06000766 RID: 1894
		// (set) Token: 0x06000767 RID: 1895
		TimeSpan MinTimeInterval { get; set; }

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06000768 RID: 1896
		// (set) Token: 0x06000769 RID: 1897
		int NumberOfTries { get; set; }

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x0600076A RID: 1898
		// (set) Token: 0x0600076B RID: 1899
		string RetryLogicType { get; set; }

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x0600076C RID: 1900
		// (set) Token: 0x0600076D RID: 1901
		string RetryMethod { get; set; }

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x0600076E RID: 1902
		// (set) Token: 0x0600076F RID: 1903
		string TransientErrors { get; set; }
	}
}
