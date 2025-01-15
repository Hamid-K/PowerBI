using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x02000258 RID: 600
	internal sealed class AddMissingItemsRollupBuilder : AddMissingItemsGroupBuilderBase, IAddMissingItemsRollupBuilder
	{
		// Token: 0x06001A1B RID: 6683 RVA: 0x00047FBA File Offset: 0x000461BA
		internal AddMissingItemsRollupBuilder(QueryTable inputTable)
			: base(inputTable)
		{
			this._groupBuilders = new List<AddMissingItemsGroupBuilder>();
			this._contextTables = new List<QueryTable>();
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x00047FDC File Offset: 0x000461DC
		public IAddMissingItemsGroupBuilder AddRollupGroup(QdmTableColumnReferenceExpression subtotalIndicatorColumnRef)
		{
			AddMissingItemsGroupBuilder addMissingItemsGroupBuilder = new AddMissingItemsGroupBuilder(base.InputTable, subtotalIndicatorColumnRef);
			this._groupBuilders.Add(addMissingItemsGroupBuilder);
			return addMissingItemsGroupBuilder;
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x00048003 File Offset: 0x00046203
		public void AddContextTable(QueryTable table)
		{
			this._contextTables.Add(table);
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x00048011 File Offset: 0x00046211
		public override void RemoveDuplicateKeys(HashSet<QdmTableColumnReferenceExpression> existingColumns)
		{
			this._groupBuilders.RemoveDuplicatedColumnsAndEmptyGroups(existingColumns);
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06001A1F RID: 6687 RVA: 0x0004801F File Offset: 0x0004621F
		public override bool IsEmpty
		{
			get
			{
				return this._groupBuilders.IsNullOrEmpty<AddMissingItemsGroupBuilder>();
			}
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x0004802C File Offset: 0x0004622C
		public void AddContextTable(QueryExpression expression)
		{
			base.AddContextTableExpression(expression);
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x00048038 File Offset: 0x00046238
		internal override IAddMissingItemsGroupItem ToGroupItem(QueryVariableReferenceExpression variableRef)
		{
			if (this._groupBuilders.Count == 0)
			{
				throw new InvalidOperationException(DevErrors.AddMissingItemsTableBuilder.RollupMustHaveAtLeastOneGroup);
			}
			return new AddMissingItemsRollup(this._groupBuilders.Select((AddMissingItemsGroupBuilder b) => (AddMissingItemsGroupWithSubtotal)b.ToGroupItem(variableRef)), base.ContextTables);
		}

		// Token: 0x04000E7F RID: 3711
		private readonly List<AddMissingItemsGroupBuilder> _groupBuilders;

		// Token: 0x04000E80 RID: 3712
		private readonly List<QueryTable> _contextTables;
	}
}
