using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.Common;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x02000280 RID: 640
	internal sealed class TopNPerLevelTableBuilder
	{
		// Token: 0x06001B76 RID: 7030 RVA: 0x0004CF9C File Offset: 0x0004B19C
		internal TopNPerLevelTableBuilder(QueryTable inputQueryTable, QueryExpression countExpression, string restartIndicatorColumnName)
		{
			this._inputQueryTable = inputQueryTable;
			this._countExpression = countExpression;
			this._restartIndicatorColumnName = restartIndicatorColumnName;
			this._topNPerLevelLevelRows = new List<TopNPerLevelLevelRow>();
			this._windowExpansionBuilder = new TopNPerLevelWindowExpansionBuilder();
			this._inputBinding = this._inputQueryTable.Expression.BindAs(this._inputQueryTable.BindingVariableNameSuggestion);
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x0004CFFC File Offset: 0x0004B1FC
		public QueryTable ToQueryTable()
		{
			QueryTopNPerLevelWindowExpansion queryTopNPerLevelWindowExpansion = this._windowExpansionBuilder.Build();
			QueryTopNPerLevelSampleExpression queryTopNPerLevelSampleExpression = this._inputBinding.TopNPerLevel(this._topNPerLevelLevelRows, this._countExpression, this._restartIndicatorColumnName, queryTopNPerLevelWindowExpansion);
			return new QueryTableDefinition(this._inputQueryTable.Columns, queryTopNPerLevelSampleExpression, QdmNames.Limit(this._inputQueryTable.BindingVariableNameSuggestion));
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x0004D058 File Offset: 0x0004B258
		public void AddLevel(int levelId, QueryExpression subtotalColumnRef, IReadOnlyList<QuerySortClause> sortByReferences, IReadOnlyList<QueryExpression> valueColumns, IReadOnlyList<QueryExpression> windowValueColumns)
		{
			QueryLiteralExpression queryLiteralExpression = QueryExpressionBuilder.Literal(new ScalarValue(levelId));
			List<TopNPerLevelLevelRow> topNPerLevelLevelRows = this._topNPerLevelLevelRows;
			QueryExpression queryExpression = queryLiteralExpression;
			QueryExpression queryExpression2 = ((subtotalColumnRef != null) ? subtotalColumnRef.RewriteColumnReferences(this._inputQueryTable.Columns, this._inputBinding.Variable) : null);
			IReadOnlyList<QuerySortClause> readOnlyList2;
			if (sortByReferences != null)
			{
				IReadOnlyList<QuerySortClause> readOnlyList = QueryTableBuilder.RewriteSortClauses(this._inputQueryTable, this._inputBinding, sortByReferences).ToList<QuerySortClause>();
				readOnlyList2 = readOnlyList;
			}
			else
			{
				readOnlyList2 = sortByReferences;
			}
			topNPerLevelLevelRows.Add(new TopNPerLevelLevelRow(queryExpression, queryExpression2, readOnlyList2, (valueColumns != null) ? valueColumns.Select((QueryExpression c) => c.RewriteColumnReferences(this._inputQueryTable.Columns, this._inputBinding.Variable)).ToList<QueryExpression>() : null, (windowValueColumns != null) ? windowValueColumns.Select((QueryExpression c) => c.RewriteColumnReferences(this._inputQueryTable.Columns, this._inputBinding.Variable)).ToList<QueryExpression>() : null));
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x0004D106 File Offset: 0x0004B306
		public TopNPerLevelWindowExpansionBuilder WithExpansion()
		{
			return this._windowExpansionBuilder;
		}

		// Token: 0x04000F03 RID: 3843
		private readonly QueryTable _inputQueryTable;

		// Token: 0x04000F04 RID: 3844
		private readonly QueryExpression _countExpression;

		// Token: 0x04000F05 RID: 3845
		private readonly QueryExpressionBinding _inputBinding;

		// Token: 0x04000F06 RID: 3846
		private readonly string _restartIndicatorColumnName;

		// Token: 0x04000F07 RID: 3847
		private TopNPerLevelWindowExpansionBuilder _windowExpansionBuilder;

		// Token: 0x04000F08 RID: 3848
		private List<TopNPerLevelLevelRow> _topNPerLevelLevelRows;
	}
}
