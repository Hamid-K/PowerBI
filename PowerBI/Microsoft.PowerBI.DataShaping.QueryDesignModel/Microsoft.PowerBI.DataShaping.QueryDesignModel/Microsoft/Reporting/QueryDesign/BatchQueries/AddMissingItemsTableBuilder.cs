using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav.Utils;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x02000253 RID: 595
	internal sealed class AddMissingItemsTableBuilder
	{
		// Token: 0x06001A01 RID: 6657 RVA: 0x00047C9C File Offset: 0x00045E9C
		internal AddMissingItemsTableBuilder(QueryTable inputTable)
		{
			this._inputTable = inputTable;
			this._showAllColumns = new List<QdmTableColumnReferenceExpression>();
			this._groupBuilders = new List<AddMissingItemsGroupBuilderBase>();
			this._contextTables = new List<QueryExpression>();
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x00047CCC File Offset: 0x00045ECC
		public void AddShowAllColumn(QdmTableColumnReferenceExpression columnRef)
		{
			if (this._showAllColumns.Contains(columnRef))
			{
				return;
			}
			if (!this._inputTable.Columns.Contains(columnRef.Target))
			{
				throw new InvalidOperationException(DevErrors.AddMissingItemsTableBuilder.ColumnIsNotInInputTable(columnRef.Target.Name.MarkAsModelInfo()));
			}
			this._showAllColumns.Add(columnRef);
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x00047D27 File Offset: 0x00045F27
		public void AddContextTable(QueryTable table)
		{
			this._contextTables.Add(table.Expression);
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x00047D3C File Offset: 0x00045F3C
		public IAddMissingItemsGroupBuilder AddGroup()
		{
			AddMissingItemsGroupBuilder addMissingItemsGroupBuilder = new AddMissingItemsGroupBuilder(this._inputTable);
			this._groupBuilders.Add(addMissingItemsGroupBuilder);
			return addMissingItemsGroupBuilder;
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x00047D64 File Offset: 0x00045F64
		public IAddMissingItemsRollupBuilder AddRollup()
		{
			if (this._groupBuilders.OfType<AddMissingItemsRollupBuilder>().Any<AddMissingItemsRollupBuilder>())
			{
				throw new InvalidOperationException(DevErrors.AddMissingItemsTableBuilder.DuplicateRollup);
			}
			AddMissingItemsRollupBuilder addMissingItemsRollupBuilder = new AddMissingItemsRollupBuilder(this._inputTable);
			this._groupBuilders.Add(addMissingItemsRollupBuilder);
			return addMissingItemsRollupBuilder;
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x00047DA8 File Offset: 0x00045FA8
		public QueryTable ToQueryTable()
		{
			this.RemoveDuplicateColumnsInGroups();
			QueryExpressionBinding inputTableBinding = this._inputTable.ToBinding();
			IEnumerable<QueryExpression> enumerable = this._showAllColumns.Select((QdmTableColumnReferenceExpression c) => c.RewriteColumnReferences(this._inputTable.Columns, inputTableBinding.Variable));
			IEnumerable<IAddMissingItemsGroupItem> enumerable2 = this._groupBuilders.Select((AddMissingItemsGroupBuilderBase b) => b.ToGroupItem(inputTableBinding.Variable));
			QueryAddMissingItemsExpression queryAddMissingItemsExpression = QueryExpressionBuilder.AddMissingItems(enumerable, inputTableBinding, enumerable2, this._contextTables);
			return new QueryTableDefinition(this._inputTable.Columns, queryAddMissingItemsExpression, QdmNames.AddMissingItems(this._inputTable.BindingVariableNameSuggestion));
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x00047E3C File Offset: 0x0004603C
		private void RemoveDuplicateColumnsInGroups()
		{
			this._groupBuilders.RemoveDuplicatedColumnsAndEmptyGroups(null);
		}

		// Token: 0x04000E77 RID: 3703
		private readonly QueryTable _inputTable;

		// Token: 0x04000E78 RID: 3704
		private readonly List<QdmTableColumnReferenceExpression> _showAllColumns;

		// Token: 0x04000E79 RID: 3705
		private readonly List<AddMissingItemsGroupBuilderBase> _groupBuilders;

		// Token: 0x04000E7A RID: 3706
		private readonly List<QueryExpression> _contextTables;
	}
}
