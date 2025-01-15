using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x0200180C RID: 6156
	internal class RenameReorderColumnsQuery : ProjectColumnsQuery
	{
		// Token: 0x06009BD2 RID: 39890 RVA: 0x00202BFA File Offset: 0x00200DFA
		public new static Query New(ColumnSelection columnSelection, Query innerQuery)
		{
			if (innerQuery.Columns.Equals(columnSelection.Keys) && columnSelection.Ordered)
			{
				return innerQuery;
			}
			return new RenameReorderColumnsQuery(columnSelection, innerQuery);
		}

		// Token: 0x06009BD3 RID: 39891 RVA: 0x00202B2E File Offset: 0x00200D2E
		private RenameReorderColumnsQuery(ColumnSelection columnSelection, Query innerQuery)
			: base(columnSelection, innerQuery)
		{
		}

		// Token: 0x17002826 RID: 10278
		// (get) Token: 0x06009BD4 RID: 39892 RVA: 0x00002139 File Offset: 0x00000339
		public override bool RenameReorder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002827 RID: 10279
		// (get) Token: 0x06009BD5 RID: 39893 RVA: 0x00002105 File Offset: 0x00000305
		public override bool FloatingSelect
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06009BD6 RID: 39894 RVA: 0x00202C20 File Offset: 0x00200E20
		public override Query RenameReorderColumns(ColumnSelection columnSelection)
		{
			return base.InnerQuery.RenameReorderColumns(base.ColumnSelection.SelectColumns(columnSelection));
		}

		// Token: 0x06009BD7 RID: 39895 RVA: 0x00202C3C File Offset: 0x00200E3C
		public override Query SelectColumns(ColumnSelection columnSelection)
		{
			if (columnSelection.Keys.Length == base.ColumnSelection.Keys.Length)
			{
				return this;
			}
			ColumnSelection columnSelection2;
			ColumnSelection columnSelection3;
			base.ColumnSelection.SelectColumns(columnSelection).Split(base.InnerQuery.Columns, out columnSelection2, out columnSelection3);
			return base.InnerQuery.SelectColumns(columnSelection2).RenameReorderColumns(columnSelection3);
		}

		// Token: 0x06009BD8 RID: 39896 RVA: 0x00202C9A File Offset: 0x00200E9A
		public override Query Take(RowCount count)
		{
			return base.InnerQuery.Take(count).RenameReorderColumns(base.ColumnSelection);
		}

		// Token: 0x06009BD9 RID: 39897 RVA: 0x00202CB3 File Offset: 0x00200EB3
		public override Query Skip(RowCount count)
		{
			return base.InnerQuery.Skip(count).RenameReorderColumns(base.ColumnSelection);
		}

		// Token: 0x06009BDA RID: 39898 RVA: 0x00202CCC File Offset: 0x00200ECC
		public override bool TryExpandListColumn(int columnIndex, bool singleOrDefault, out Query query)
		{
			if (base.InnerQuery.TryExpandListColumn(base.ColumnSelection.GetColumn(columnIndex), singleOrDefault, out query))
			{
				query = query.RenameReorderColumns(base.ColumnSelection);
				return true;
			}
			query = null;
			return false;
		}

		// Token: 0x06009BDB RID: 39899 RVA: 0x00202D00 File Offset: 0x00200F00
		protected override Query CreateProjectColumns(Query innerQuery, ColumnSelection columnProjection)
		{
			ColumnSelection columnSelection;
			ColumnSelection columnSelection2;
			columnProjection.Split(innerQuery.Columns, out columnSelection, out columnSelection2);
			return innerQuery.SelectColumns(columnSelection).RenameReorderColumns(columnSelection2);
		}
	}
}
