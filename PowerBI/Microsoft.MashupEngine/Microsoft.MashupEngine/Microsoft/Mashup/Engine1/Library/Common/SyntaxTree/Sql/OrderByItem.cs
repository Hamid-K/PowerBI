using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011E5 RID: 4581
	internal sealed class OrderByItem : IScriptable
	{
		// Token: 0x060078D1 RID: 30929 RVA: 0x001A2323 File Offset: 0x001A0523
		public OrderByItem(ColumnReference sortColumn, OrderByOption order)
		{
			this.order = order;
			this.sortColumn = sortColumn;
		}

		// Token: 0x1700210C RID: 8460
		// (get) Token: 0x060078D2 RID: 30930 RVA: 0x001A2339 File Offset: 0x001A0539
		public OrderByOption Order
		{
			get
			{
				return this.order;
			}
		}

		// Token: 0x1700210D RID: 8461
		// (get) Token: 0x060078D3 RID: 30931 RVA: 0x001A2341 File Offset: 0x001A0541
		public ColumnReference SortColumn
		{
			get
			{
				return this.sortColumn;
			}
		}

		// Token: 0x060078D4 RID: 30932 RVA: 0x001A2349 File Offset: 0x001A0549
		public void WriteCreateScript(ScriptWriter writer)
		{
			writer.Indent();
			this.SortColumn.WriteCreateScript(writer);
			if (this.Order == OrderByOption.Descending)
			{
				writer.WriteSpaceBefore(SqlLanguageStrings.DescSqlString);
			}
			writer.Unindent();
		}

		// Token: 0x040041CF RID: 16847
		private readonly OrderByOption order;

		// Token: 0x040041D0 RID: 16848
		private readonly ColumnReference sortColumn;
	}
}
