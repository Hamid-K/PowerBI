using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x02000282 RID: 642
	internal sealed class TreatAsTableBuilder
	{
		// Token: 0x06001B83 RID: 7043 RVA: 0x0004D290 File Offset: 0x0004B490
		internal TreatAsTableBuilder(int initialColumnCapacity = 0)
		{
			this._columns = new List<QueryTableColumn>(initialColumnCapacity);
			this._dataTable = new DataTableBuilder(initialColumnCapacity);
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x0004D2B0 File Offset: 0x0004B4B0
		internal void AddColumn(QueryExpression column)
		{
			QueryExpression queryExpression = column.RewriteEntityPlaceholdersToScalarEntityReferences(null, null);
			QueryTableColumn queryTableColumn = this._dataTable.AddColumn((ConceptualPrimitiveResultType)column.ConceptualResultType);
			this._columns.Add(queryExpression.ToQueryTableColumn(queryTableColumn.Name));
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x0004D2F4 File Offset: 0x0004B4F4
		internal void AddRow(IReadOnlyList<QueryExpression> columns)
		{
			this._dataTable.AddRow(columns);
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x0004D302 File Offset: 0x0004B502
		public QueryTable ToQueryTable()
		{
			return this._dataTable.ToQueryTable().TreatAs(this._columns);
		}

		// Token: 0x04000F0C RID: 3852
		private readonly List<QueryTableColumn> _columns;

		// Token: 0x04000F0D RID: 3853
		private DataTableBuilder _dataTable;
	}
}
