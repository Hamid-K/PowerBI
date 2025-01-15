using System;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Salesforce
{
	// Token: 0x020001D8 RID: 472
	internal class SalesforceLinkInfo
	{
		// Token: 0x06000957 RID: 2391 RVA: 0x00012DC0 File Offset: 0x00010FC0
		public SalesforceLinkInfo(string name, Keys sourceKeys, int[] sourceColumns, SalesforceObjectHeader targetTable, Keys targetKeys, bool singleTarget, Func<SalesforceObjectHeader, TableValue> loader)
		{
			this.originalName = name;
			this.Name = name;
			this.sourceKeys = sourceKeys;
			this.sourceColumns = sourceColumns;
			this.targetTable = targetTable;
			this.targetKeys = targetKeys;
			this.singleTarget = singleTarget;
			this.linkTable = new LinkTableFunctionValue(() => loader(targetTable));
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x00012E38 File Offset: 0x00011038
		public string OriginalName
		{
			get
			{
				return this.originalName;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x00012E40 File Offset: 0x00011040
		// (set) Token: 0x0600095A RID: 2394 RVA: 0x00012E48 File Offset: 0x00011048
		public string Name { get; set; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x00012E51 File Offset: 0x00011051
		public Keys SourceKeys
		{
			get
			{
				return this.sourceKeys;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x00012E59 File Offset: 0x00011059
		public int[] SourceColumns
		{
			get
			{
				return this.sourceColumns;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x00012E61 File Offset: 0x00011061
		public SalesforceObjectHeader TargetTable
		{
			get
			{
				return this.targetTable;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x00012E69 File Offset: 0x00011069
		public Keys TargetKeys
		{
			get
			{
				return this.targetKeys;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x00012E71 File Offset: 0x00011071
		public bool SingleTarget
		{
			get
			{
				return this.singleTarget;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x00012E79 File Offset: 0x00011079
		public Value LinkTable
		{
			get
			{
				return this.linkTable;
			}
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00012E84 File Offset: 0x00011084
		public static int Comparison(SalesforceLinkInfo x, SalesforceLinkInfo y)
		{
			int num = string.CompareOrdinal(x.Name, y.Name);
			if (num == 0)
			{
				num = string.CompareOrdinal(x.TargetTable.Name, y.TargetTable.Name);
			}
			if (num == 0)
			{
				num = string.CompareOrdinal(x.TargetKeys[0], y.TargetKeys[0]);
			}
			return num;
		}

		// Token: 0x04000556 RID: 1366
		private readonly string originalName;

		// Token: 0x04000557 RID: 1367
		private readonly Keys sourceKeys;

		// Token: 0x04000558 RID: 1368
		private readonly int[] sourceColumns;

		// Token: 0x04000559 RID: 1369
		private readonly SalesforceObjectHeader targetTable;

		// Token: 0x0400055A RID: 1370
		private readonly Keys targetKeys;

		// Token: 0x0400055B RID: 1371
		private readonly bool singleTarget;

		// Token: 0x0400055C RID: 1372
		private readonly Value linkTable;
	}
}
