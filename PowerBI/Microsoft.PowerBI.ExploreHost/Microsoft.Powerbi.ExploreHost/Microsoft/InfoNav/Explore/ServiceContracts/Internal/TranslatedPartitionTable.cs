using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Explore.ServiceContracts.Internal
{
	// Token: 0x02000009 RID: 9
	public sealed class TranslatedPartitionTable
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002177 File Offset: 0x00000377
		public TranslatedPartitionTable(string tableExpression, string columnExpression, string partitionIdColumn, PartitionTableResult tableResult)
		{
			this.TableExpression = tableExpression;
			this.ColumnExpression = columnExpression;
			this.PartitionIdColumn = partitionIdColumn;
			this.TableResult = tableResult;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000219C File Offset: 0x0000039C
		public TranslatedPartitionTable(string tableExpression, string columnExpression, string partitionIdColumn)
		{
			this.TableExpression = tableExpression;
			this.ColumnExpression = columnExpression;
			this.PartitionIdColumn = partitionIdColumn;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000021B9 File Offset: 0x000003B9
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000021C1 File Offset: 0x000003C1
		public string TableExpression { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000021CA File Offset: 0x000003CA
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000021D2 File Offset: 0x000003D2
		public string ColumnExpression { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000021DB File Offset: 0x000003DB
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000021E3 File Offset: 0x000003E3
		public string PartitionIdColumn { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000021EC File Offset: 0x000003EC
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000021F4 File Offset: 0x000003F4
		public PartitionTableResult TableResult { get; private set; }
	}
}
