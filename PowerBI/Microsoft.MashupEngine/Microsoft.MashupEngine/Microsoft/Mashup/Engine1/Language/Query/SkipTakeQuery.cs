using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001813 RID: 6163
	internal class SkipTakeQuery : Query
	{
		// Token: 0x06009C07 RID: 39943 RVA: 0x00203425 File Offset: 0x00201625
		public static Query New(RowRange rowRange, Query innerQuery, bool floating = false)
		{
			if (rowRange.IsAll)
			{
				return innerQuery;
			}
			if (rowRange.IsNone && !floating)
			{
				return new TableQuery(ListValue.Empty.ToTable(QueryTableValue.NewTableType(innerQuery)), innerQuery.GetEngineHost());
			}
			return new SkipTakeQuery(rowRange, innerQuery, floating);
		}

		// Token: 0x06009C08 RID: 39944 RVA: 0x00203462 File Offset: 0x00201662
		public SkipTakeQuery(RowRange rowRange, Query innerQuery, bool floating = false)
		{
			this.rowRange = rowRange;
			this.innerQuery = innerQuery;
			this.floating = floating;
		}

		// Token: 0x17002833 RID: 10291
		// (get) Token: 0x06009C09 RID: 39945 RVA: 0x0000244F File Offset: 0x0000064F
		public override QueryKind Kind
		{
			get
			{
				return QueryKind.SkipTake;
			}
		}

		// Token: 0x17002834 RID: 10292
		// (get) Token: 0x06009C0A RID: 39946 RVA: 0x0020347F File Offset: 0x0020167F
		public Query InnerQuery
		{
			get
			{
				return this.innerQuery;
			}
		}

		// Token: 0x17002835 RID: 10293
		// (get) Token: 0x06009C0B RID: 39947 RVA: 0x00203487 File Offset: 0x00201687
		public bool Floating
		{
			get
			{
				return this.floating;
			}
		}

		// Token: 0x17002836 RID: 10294
		// (get) Token: 0x06009C0C RID: 39948 RVA: 0x0020348F File Offset: 0x0020168F
		public override Keys Columns
		{
			get
			{
				return this.innerQuery.Columns;
			}
		}

		// Token: 0x06009C0D RID: 39949 RVA: 0x0020349C File Offset: 0x0020169C
		public override TypeValue GetColumnType(int column)
		{
			return this.innerQuery.GetColumnType(column);
		}

		// Token: 0x17002837 RID: 10295
		// (get) Token: 0x06009C0E RID: 39950 RVA: 0x002034AA File Offset: 0x002016AA
		public override IList<TableKey> TableKeys
		{
			get
			{
				return this.innerQuery.TableKeys;
			}
		}

		// Token: 0x17002838 RID: 10296
		// (get) Token: 0x06009C0F RID: 39951 RVA: 0x002034B7 File Offset: 0x002016B7
		public override IList<ComputedColumn> ComputedColumns
		{
			get
			{
				return this.innerQuery.ComputedColumns;
			}
		}

		// Token: 0x17002839 RID: 10297
		// (get) Token: 0x06009C10 RID: 39952 RVA: 0x002034C4 File Offset: 0x002016C4
		public override TableSortOrder SortOrder
		{
			get
			{
				return this.innerQuery.SortOrder;
			}
		}

		// Token: 0x1700283A RID: 10298
		// (get) Token: 0x06009C11 RID: 39953 RVA: 0x002034D1 File Offset: 0x002016D1
		public RowRange RowRange
		{
			get
			{
				return this.rowRange;
			}
		}

		// Token: 0x06009C12 RID: 39954 RVA: 0x002034D9 File Offset: 0x002016D9
		public override TableValue GetPartitionTable(int[] columns)
		{
			return this.innerQuery.GetPartitionTable(columns);
		}

		// Token: 0x06009C13 RID: 39955 RVA: 0x002034E7 File Offset: 0x002016E7
		public override Query Skip(RowCount count)
		{
			return SkipTakeQuery.New(this.rowRange.Skip(count), this.innerQuery, false);
		}

		// Token: 0x06009C14 RID: 39956 RVA: 0x00203501 File Offset: 0x00201701
		public override Query Take(RowCount count)
		{
			return SkipTakeQuery.New(this.rowRange.Take(count), this.innerQuery, false);
		}

		// Token: 0x06009C15 RID: 39957 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override Query Unordered()
		{
			return this;
		}

		// Token: 0x1700283B RID: 10299
		// (get) Token: 0x06009C16 RID: 39958 RVA: 0x0020351B File Offset: 0x0020171B
		public override IQueryDomain QueryDomain
		{
			get
			{
				return this.innerQuery.QueryDomain;
			}
		}

		// Token: 0x06009C17 RID: 39959 RVA: 0x00203528 File Offset: 0x00201728
		public override IEnumerable<IValueReference> GetRows()
		{
			return new SkipTakeEnumerable(this.innerQuery.GetRows(), this.rowRange);
		}

		// Token: 0x0400523F RID: 21055
		private Query innerQuery;

		// Token: 0x04005240 RID: 21056
		private RowRange rowRange;

		// Token: 0x04005241 RID: 21057
		private bool floating;
	}
}
