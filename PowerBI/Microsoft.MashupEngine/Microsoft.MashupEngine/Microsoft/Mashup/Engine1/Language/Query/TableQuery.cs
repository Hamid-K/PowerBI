using System;
using System.Collections.Generic;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001805 RID: 6149
	internal class TableQuery : DataSourceQuery
	{
		// Token: 0x06009B7C RID: 39804 RVA: 0x00201B84 File Offset: 0x001FFD84
		public TableQuery(TableValue table, IEngineHost engineHost)
			: this(table, null, engineHost)
		{
		}

		// Token: 0x06009B7D RID: 39805 RVA: 0x00201B8F File Offset: 0x001FFD8F
		public TableQuery(TableValue table, int[] expandColumns, IEngineHost engineHost)
		{
			this.engineHost = engineHost;
			this.table = table;
			this.expandColumns = expandColumns;
		}

		// Token: 0x17002808 RID: 10248
		// (get) Token: 0x06009B7E RID: 39806 RVA: 0x00201BAC File Offset: 0x001FFDAC
		public override Keys Columns
		{
			get
			{
				return this.table.Columns;
			}
		}

		// Token: 0x06009B7F RID: 39807 RVA: 0x00201BB9 File Offset: 0x001FFDB9
		public override TypeValue GetColumnType(int column)
		{
			return this.table.GetColumnType(column);
		}

		// Token: 0x17002809 RID: 10249
		// (get) Token: 0x06009B80 RID: 39808 RVA: 0x00201BC7 File Offset: 0x001FFDC7
		public override IList<TableKey> TableKeys
		{
			get
			{
				return this.table.TableKeys;
			}
		}

		// Token: 0x1700280A RID: 10250
		// (get) Token: 0x06009B81 RID: 39809 RVA: 0x00201BD4 File Offset: 0x001FFDD4
		public override IList<ComputedColumn> ComputedColumns
		{
			get
			{
				return this.table.ComputedColumns;
			}
		}

		// Token: 0x1700280B RID: 10251
		// (get) Token: 0x06009B82 RID: 39810 RVA: 0x00201BE1 File Offset: 0x001FFDE1
		public override RowCount RowCount
		{
			get
			{
				return new RowCount(this.table.LargeCount);
			}
		}

		// Token: 0x1700280C RID: 10252
		// (get) Token: 0x06009B83 RID: 39811 RVA: 0x00201BF3 File Offset: 0x001FFDF3
		public override TableSortOrder SortOrder
		{
			get
			{
				return this.table.SortOrder;
			}
		}

		// Token: 0x1700280D RID: 10253
		// (get) Token: 0x06009B84 RID: 39812 RVA: 0x00201C00 File Offset: 0x001FFE00
		public TableValue Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x1700280E RID: 10254
		// (get) Token: 0x06009B85 RID: 39813 RVA: 0x00201C08 File Offset: 0x001FFE08
		public int[] ExpandColumnns
		{
			get
			{
				return this.expandColumns;
			}
		}

		// Token: 0x1700280F RID: 10255
		// (get) Token: 0x06009B86 RID: 39814 RVA: 0x00201C10 File Offset: 0x001FFE10
		public override IEngineHost EngineHost
		{
			get
			{
				return this.engineHost;
			}
		}

		// Token: 0x06009B87 RID: 39815 RVA: 0x00201C18 File Offset: 0x001FFE18
		public override Query SelectColumns(ColumnSelection columnSelection)
		{
			TableValue tableValue;
			if (this.table.TrySelectColumns(columnSelection, out tableValue))
			{
				return tableValue.Query;
			}
			return base.SelectColumns(columnSelection);
		}

		// Token: 0x06009B88 RID: 39816 RVA: 0x00201C43 File Offset: 0x001FFE43
		public override Query Unordered()
		{
			if (this.table.SortOrder == TableSortOrder.None)
			{
				return this;
			}
			return new TableQuery(this.table.Unordered(), this.engineHost);
		}

		// Token: 0x17002810 RID: 10256
		// (get) Token: 0x06009B89 RID: 39817 RVA: 0x00201C6F File Offset: 0x001FFE6F
		public override IQueryDomain QueryDomain
		{
			get
			{
				return this.table.QueryDomain;
			}
		}

		// Token: 0x06009B8A RID: 39818 RVA: 0x00201C7C File Offset: 0x001FFE7C
		public override bool TryGetReader(out IPageReader reader)
		{
			reader = this.table.GetReader();
			return true;
		}

		// Token: 0x06009B8B RID: 39819 RVA: 0x00201C00 File Offset: 0x001FFE00
		public sealed override IEnumerable<IValueReference> GetRows()
		{
			return this.table;
		}

		// Token: 0x06009B8C RID: 39820 RVA: 0x00201C8C File Offset: 0x001FFE8C
		public override bool TryGetExpression(out IExpression expression)
		{
			if (this.table is IOptimizedValue)
			{
				expression = this.table.Expression;
				return true;
			}
			return base.TryGetExpression(out expression);
		}

		// Token: 0x06009B8D RID: 39821 RVA: 0x00201CB1 File Offset: 0x001FFEB1
		public override bool TryInvokeAsArgument(FunctionValue function, Value[] arguments, int index, out Value result)
		{
			return this.table.TryInvokeAsArgument(function, arguments, index, out result);
		}

		// Token: 0x04005221 RID: 21025
		private readonly IEngineHost engineHost;

		// Token: 0x04005222 RID: 21026
		private readonly TableValue table;

		// Token: 0x04005223 RID: 21027
		private readonly int[] expandColumns;
	}
}
