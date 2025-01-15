using System;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x0200180A RID: 6154
	internal class SelectColumnsQuery : ProjectColumnsQuery
	{
		// Token: 0x06009BC5 RID: 39877 RVA: 0x00202AE8 File Offset: 0x00200CE8
		public new static Query New(ColumnSelection columnSelection, Query innerQuery)
		{
			if (!SelectColumnsQuery.NewQueryRequired(columnSelection, innerQuery))
			{
				return innerQuery;
			}
			if (columnSelection.Keys.Length == 0)
			{
				return FloatingSelectColumnsQuery.New(columnSelection, innerQuery);
			}
			return new SelectColumnsQuery(columnSelection, innerQuery);
		}

		// Token: 0x06009BC6 RID: 39878 RVA: 0x00202B11 File Offset: 0x00200D11
		public static bool NewQueryRequired(ColumnSelection columnSelection, Query innerQuery)
		{
			return columnSelection.Keys.Length != innerQuery.Columns.Length;
		}

		// Token: 0x06009BC7 RID: 39879 RVA: 0x00202B2E File Offset: 0x00200D2E
		protected SelectColumnsQuery(ColumnSelection columnSelection, Query innerQuery)
			: base(columnSelection, innerQuery)
		{
		}

		// Token: 0x17002823 RID: 10275
		// (get) Token: 0x06009BC8 RID: 39880 RVA: 0x00002105 File Offset: 0x00000305
		public override bool RenameReorder
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002824 RID: 10276
		// (get) Token: 0x06009BC9 RID: 39881 RVA: 0x00002105 File Offset: 0x00000305
		public override bool FloatingSelect
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06009BCA RID: 39882 RVA: 0x00202B38 File Offset: 0x00200D38
		public override Query SelectColumns(ColumnSelection columnSelection)
		{
			return base.InnerQuery.SelectColumns(base.ColumnSelection.SelectColumns(columnSelection));
		}

		// Token: 0x06009BCB RID: 39883 RVA: 0x00202B54 File Offset: 0x00200D54
		protected override Query CreateProjectColumns(Query innerQuery, ColumnSelection columnProjection)
		{
			ColumnSelection columnSelection;
			ColumnSelection columnSelection2;
			columnProjection.Split(innerQuery.Columns, out columnSelection, out columnSelection2);
			return innerQuery.SelectColumns(columnSelection).RenameReorderColumns(columnSelection2);
		}
	}
}
