using System;

namespace Microsoft.InfoNav.Explore.ServiceContracts.Internal
{
	// Token: 0x0200000A RID: 10
	public sealed class TranslatedPartitionColumn
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000021FD File Offset: 0x000003FD
		public TranslatedPartitionColumn(string columnExpression)
		{
			this.ColumnExpression = columnExpression;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000220C File Offset: 0x0000040C
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002214 File Offset: 0x00000414
		public string ColumnExpression { get; private set; }
	}
}
