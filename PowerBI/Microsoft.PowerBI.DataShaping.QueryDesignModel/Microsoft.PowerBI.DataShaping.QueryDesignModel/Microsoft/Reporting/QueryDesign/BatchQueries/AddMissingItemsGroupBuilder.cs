using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x02000257 RID: 599
	internal class AddMissingItemsGroupBuilder : AddMissingItemsGroupBuilderBase, IAddMissingItemsGroupBuilder
	{
		// Token: 0x06001A14 RID: 6676 RVA: 0x00047EAC File Offset: 0x000460AC
		internal AddMissingItemsGroupBuilder(QueryTable inputTable)
			: base(inputTable)
		{
			this._groupKeys = new List<QdmTableColumnReferenceExpression>(1);
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x00047EC1 File Offset: 0x000460C1
		internal AddMissingItemsGroupBuilder(QueryTable inputTable, QdmTableColumnReferenceExpression subtotalIndicatorColumnRef)
			: this(inputTable)
		{
			base.CheckColumnExistsInInput(subtotalIndicatorColumnRef);
			this._subtotalIndicatorColumnRef = subtotalIndicatorColumnRef;
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x00047ED8 File Offset: 0x000460D8
		public void AddGroupKey(QdmTableColumnReferenceExpression columnRef)
		{
			if (this._groupKeys.Contains(columnRef))
			{
				return;
			}
			base.CheckColumnExistsInInput(columnRef);
			this._groupKeys.Add(columnRef);
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x00047EFC File Offset: 0x000460FC
		public void AddContextTable(QueryExpression expression)
		{
			base.AddContextTableExpression(expression);
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06001A18 RID: 6680 RVA: 0x00047F05 File Offset: 0x00046105
		public override bool IsEmpty
		{
			get
			{
				return this._groupKeys.IsNullOrEmpty<QdmTableColumnReferenceExpression>() && this._subtotalIndicatorColumnRef == null;
			}
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x00047F1F File Offset: 0x0004611F
		public override void RemoveDuplicateKeys(HashSet<QdmTableColumnReferenceExpression> existingColumns)
		{
			QueryTableUtils.RemoveDuplicateKeys<QdmTableColumnReferenceExpression>(this._groupKeys, existingColumns);
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x00047F30 File Offset: 0x00046130
		internal override IAddMissingItemsGroupItem ToGroupItem(QueryVariableReferenceExpression variableRef)
		{
			if (this._groupKeys.Count == 0)
			{
				throw new InvalidOperationException(DevErrors.AddMissingItemsTableBuilder.GroupMustHaveAtLeastOneKey);
			}
			AddMissingItemsGroup addMissingItemsGroup = new AddMissingItemsGroup(this._groupKeys.Select((QdmTableColumnReferenceExpression k) => k.RewriteColumnReferences(this.InputTable.Columns, variableRef)));
			if (this._subtotalIndicatorColumnRef == null)
			{
				return addMissingItemsGroup;
			}
			QueryExpression queryExpression = this._subtotalIndicatorColumnRef.RewriteColumnReferences(base.InputTable.Columns, variableRef);
			return new AddMissingItemsGroupWithSubtotal(addMissingItemsGroup, queryExpression, base.ContextTables);
		}

		// Token: 0x04000E7D RID: 3709
		private readonly List<QdmTableColumnReferenceExpression> _groupKeys;

		// Token: 0x04000E7E RID: 3710
		private readonly QdmTableColumnReferenceExpression _subtotalIndicatorColumnRef;
	}
}
