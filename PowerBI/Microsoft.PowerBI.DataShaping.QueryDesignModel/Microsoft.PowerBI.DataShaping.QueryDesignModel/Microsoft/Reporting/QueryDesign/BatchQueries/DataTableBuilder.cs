using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x02000264 RID: 612
	internal sealed class DataTableBuilder
	{
		// Token: 0x06001A8C RID: 6796 RVA: 0x00049958 File Offset: 0x00047B58
		internal DataTableBuilder(int columnCapacity = 0)
		{
			this._rows = new List<QueryTupleExpression>();
			this._queryColumns = new List<QueryTableColumn>(columnCapacity);
			this._columnNames = new List<string>(columnCapacity);
			this._conceptualColumnTypes = new List<ConceptualPrimitiveResultType>(columnCapacity);
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x0004998F File Offset: 0x00047B8F
		internal DataTableBuilder(IReadOnlyList<ConceptualPrimitiveResultType> conceptualColumnTypes)
			: this(conceptualColumnTypes.Count)
		{
			this.SetupColumns(conceptualColumnTypes);
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x000499A4 File Offset: 0x00047BA4
		public QueryTable ToQueryTable()
		{
			QueryDataTableExpression queryDataTableExpression = QueryExpressionBuilder.DataTable(this._rows, this._columnNames, this._conceptualColumnTypes);
			return new QueryTableDefinition(this._queryColumns, queryDataTableExpression, "DataTable");
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x000499DA File Offset: 0x00047BDA
		public void AddRow(IReadOnlyList<QueryExpression> columns)
		{
			this._rows.Add(QueryExpressionBuilder.Tuple(columns, this._columnNames));
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x000499F4 File Offset: 0x00047BF4
		public QueryTableColumn AddColumn(ConceptualPrimitiveResultType conceptualColumnType)
		{
			string text = DataTableBuilder.AutoGenColumnName(this._columnNames.Count);
			this._conceptualColumnTypes.Add(conceptualColumnType);
			return this.AddDataTableColumn(text, conceptualColumnType);
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x00049A28 File Offset: 0x00047C28
		public static List<string> AutoGenQueryTupleNames(int count)
		{
			List<string> list = new List<string>(count);
			if (count == 1)
			{
				list.Add("Value");
			}
			else
			{
				for (int i = 0; i < count; i++)
				{
					list.Add(DataTableBuilder.AutoGenColumnName(i));
				}
			}
			return list;
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x00049A66 File Offset: 0x00047C66
		private static string AutoGenColumnName(int columnIndex)
		{
			return "Value" + (columnIndex + 1);
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x00049A7C File Offset: 0x00047C7C
		private void SetupColumns(IReadOnlyList<ConceptualPrimitiveResultType> conceptualColumnTypes)
		{
			List<string> list = DataTableBuilder.AutoGenQueryTupleNames(conceptualColumnTypes.Count);
			this._conceptualColumnTypes.AddRange(conceptualColumnTypes);
			for (int i = 0; i < conceptualColumnTypes.Count; i++)
			{
				this.AddDataTableColumn(list[i], conceptualColumnTypes[i]);
			}
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x00049AC8 File Offset: 0x00047CC8
		private QueryTableColumn AddDataTableColumn(string name, ConceptualPrimitiveResultType conceptualResultType)
		{
			this._columnNames.Add(name);
			QueryTableColumn queryTableColumn = new QueryTableColumn(name, conceptualResultType);
			this._queryColumns.Add(queryTableColumn);
			return queryTableColumn;
		}

		// Token: 0x04000EAA RID: 3754
		private const string AutoGenColumnNamePrefix = "Value";

		// Token: 0x04000EAB RID: 3755
		private readonly List<QueryTupleExpression> _rows;

		// Token: 0x04000EAC RID: 3756
		private readonly List<QueryTableColumn> _queryColumns;

		// Token: 0x04000EAD RID: 3757
		private readonly List<string> _columnNames;

		// Token: 0x04000EAE RID: 3758
		private readonly List<ConceptualPrimitiveResultType> _conceptualColumnTypes;
	}
}
