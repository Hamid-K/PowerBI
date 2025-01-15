using System;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x02000670 RID: 1648
	internal sealed class OdbcLinkInfo : IComparable<OdbcLinkInfo>
	{
		// Token: 0x060033DB RID: 13275 RVA: 0x000A63B4 File Offset: 0x000A45B4
		public OdbcLinkInfo(string name, Keys sourceKeys, int[] sourceColumns, OdbcTableInfo targetTable, Keys targetKeys, bool singleTarget, Func<OdbcTableInfo, TableValue> loader)
		{
			this.Name = name;
			this.originalName = name;
			this.sourceKeys = sourceKeys;
			this.sourceColumns = sourceColumns;
			this.targetTable = targetTable;
			this.targetKeys = targetKeys;
			this.singleTarget = singleTarget;
			this.linkTable = new LinkTableFunctionValue(() => loader(targetTable));
		}

		// Token: 0x1700128B RID: 4747
		// (get) Token: 0x060033DC RID: 13276 RVA: 0x000A642C File Offset: 0x000A462C
		public string OriginalName
		{
			get
			{
				return this.originalName;
			}
		}

		// Token: 0x1700128C RID: 4748
		// (get) Token: 0x060033DD RID: 13277 RVA: 0x000A6434 File Offset: 0x000A4634
		// (set) Token: 0x060033DE RID: 13278 RVA: 0x000A643C File Offset: 0x000A463C
		public string Name { get; set; }

		// Token: 0x1700128D RID: 4749
		// (get) Token: 0x060033DF RID: 13279 RVA: 0x000A6445 File Offset: 0x000A4645
		public Keys SourceKeys
		{
			get
			{
				return this.sourceKeys;
			}
		}

		// Token: 0x1700128E RID: 4750
		// (get) Token: 0x060033E0 RID: 13280 RVA: 0x000A644D File Offset: 0x000A464D
		public int[] SourceColumns
		{
			get
			{
				return this.sourceColumns;
			}
		}

		// Token: 0x1700128F RID: 4751
		// (get) Token: 0x060033E1 RID: 13281 RVA: 0x000A6455 File Offset: 0x000A4655
		public OdbcTableInfo TargetTable
		{
			get
			{
				return this.targetTable;
			}
		}

		// Token: 0x17001290 RID: 4752
		// (get) Token: 0x060033E2 RID: 13282 RVA: 0x000A645D File Offset: 0x000A465D
		public Keys TargetKeys
		{
			get
			{
				return this.targetKeys;
			}
		}

		// Token: 0x17001291 RID: 4753
		// (get) Token: 0x060033E3 RID: 13283 RVA: 0x000A6465 File Offset: 0x000A4665
		public bool IsSingleTarget
		{
			get
			{
				return this.singleTarget;
			}
		}

		// Token: 0x17001292 RID: 4754
		// (get) Token: 0x060033E4 RID: 13284 RVA: 0x000A646D File Offset: 0x000A466D
		public Value LinkTable
		{
			get
			{
				return this.linkTable;
			}
		}

		// Token: 0x060033E5 RID: 13285 RVA: 0x000A6478 File Offset: 0x000A4678
		public int CompareTo(OdbcLinkInfo other)
		{
			int num = string.CompareOrdinal(this.Name, other.Name);
			if (num == 0)
			{
				num = this.targetTable.Identifier.CompareTo(other.targetTable.Identifier);
				if (num == 0)
				{
					num = this.Compare(this.targetKeys, other.targetKeys);
				}
			}
			return num;
		}

		// Token: 0x060033E6 RID: 13286 RVA: 0x000A64D0 File Offset: 0x000A46D0
		private int Compare(Keys x, Keys y)
		{
			int num = 0;
			while (num < x.Length && num < y.Length)
			{
				int num2 = string.CompareOrdinal(x[num], y[num]);
				if (num2 != 0)
				{
					return num2;
				}
				num++;
			}
			return x.Length - y.Length;
		}

		// Token: 0x04001726 RID: 5926
		private readonly string originalName;

		// Token: 0x04001727 RID: 5927
		private readonly Keys sourceKeys;

		// Token: 0x04001728 RID: 5928
		private readonly int[] sourceColumns;

		// Token: 0x04001729 RID: 5929
		private readonly OdbcTableInfo targetTable;

		// Token: 0x0400172A RID: 5930
		private readonly Keys targetKeys;

		// Token: 0x0400172B RID: 5931
		private readonly Value linkTable;

		// Token: 0x0400172C RID: 5932
		private readonly bool singleTarget;
	}
}
