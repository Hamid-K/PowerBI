using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x0200180B RID: 6155
	internal class FloatingSelectColumnsQuery : SelectColumnsQuery
	{
		// Token: 0x06009BCC RID: 39884 RVA: 0x00202B7E File Offset: 0x00200D7E
		public new static Query New(ColumnSelection columnSelection, Query innerQuery)
		{
			if (SelectColumnsQuery.NewQueryRequired(columnSelection, innerQuery))
			{
				return new FloatingSelectColumnsQuery(columnSelection, innerQuery);
			}
			return innerQuery;
		}

		// Token: 0x06009BCD RID: 39885 RVA: 0x00202B92 File Offset: 0x00200D92
		protected FloatingSelectColumnsQuery(ColumnSelection columnSelection, Query innerQuery)
			: base(columnSelection, innerQuery)
		{
		}

		// Token: 0x17002825 RID: 10277
		// (get) Token: 0x06009BCE RID: 39886 RVA: 0x00002139 File Offset: 0x00000339
		public override bool FloatingSelect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06009BCF RID: 39887 RVA: 0x00202B9C File Offset: 0x00200D9C
		public override Query Take(RowCount count)
		{
			return FloatingSelectColumnsQuery.New(base.ColumnSelection, base.InnerQuery.Take(count));
		}

		// Token: 0x06009BD0 RID: 39888 RVA: 0x00202BB5 File Offset: 0x00200DB5
		public override Query Skip(RowCount count)
		{
			return FloatingSelectColumnsQuery.New(base.ColumnSelection, base.InnerQuery.Skip(count));
		}

		// Token: 0x06009BD1 RID: 39889 RVA: 0x00202BD0 File Offset: 0x00200DD0
		protected override Query CreateProjectColumns(Query innerQuery, ColumnSelection columnProjection)
		{
			ColumnSelection columnSelection;
			ColumnSelection columnSelection2;
			columnProjection.Split(innerQuery.Columns, out columnSelection, out columnSelection2);
			return FloatingSelectColumnsQuery.New(columnSelection, innerQuery).RenameReorderColumns(columnSelection2);
		}
	}
}
