using System;
using System.Collections.Generic;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200003A RID: 58
	[Serializable]
	public sealed class SqlRetryLogicOption
	{
		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x0000EEAD File Offset: 0x0000D0AD
		// (set) Token: 0x0600073B RID: 1851 RVA: 0x0000EEB5 File Offset: 0x0000D0B5
		public int NumberOfTries { get; set; }

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x0000EEBE File Offset: 0x0000D0BE
		// (set) Token: 0x0600073D RID: 1853 RVA: 0x0000EEC6 File Offset: 0x0000D0C6
		public TimeSpan DeltaTime { get; set; }

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x0000EECF File Offset: 0x0000D0CF
		// (set) Token: 0x0600073F RID: 1855 RVA: 0x0000EED7 File Offset: 0x0000D0D7
		public TimeSpan MinTimeInterval { get; set; } = TimeSpan.Zero;

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x0000EEE0 File Offset: 0x0000D0E0
		// (set) Token: 0x06000741 RID: 1857 RVA: 0x0000EEE8 File Offset: 0x0000D0E8
		public TimeSpan MaxTimeInterval { get; set; }

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x0000EEF1 File Offset: 0x0000D0F1
		// (set) Token: 0x06000743 RID: 1859 RVA: 0x0000EEF9 File Offset: 0x0000D0F9
		public IEnumerable<int> TransientErrors { get; set; }

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x0000EF02 File Offset: 0x0000D102
		// (set) Token: 0x06000745 RID: 1861 RVA: 0x0000EF0A File Offset: 0x0000D10A
		public Predicate<string> AuthorizedSqlCondition { get; set; }
	}
}
