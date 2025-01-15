using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.QueryDesignModel.QueryDesign.BatchQueries;
using Microsoft.InfoNav.Utils;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x02000256 RID: 598
	internal abstract class AddMissingItemsGroupBuilderBase : IGroupBuilder<QdmTableColumnReferenceExpression>, IGroupBuilder
	{
		// Token: 0x06001A0C RID: 6668 RVA: 0x00047E4A File Offset: 0x0004604A
		protected AddMissingItemsGroupBuilderBase(QueryTable inputTable)
		{
			this._inputTable = inputTable;
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06001A0D RID: 6669 RVA: 0x00047E59 File Offset: 0x00046059
		protected QueryTable InputTable
		{
			get
			{
				return this._inputTable;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06001A0E RID: 6670 RVA: 0x00047E61 File Offset: 0x00046061
		protected List<QueryExpression> ContextTables
		{
			get
			{
				return this._contextTables;
			}
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x00047E69 File Offset: 0x00046069
		protected void CheckColumnExistsInInput(QdmTableColumnReferenceExpression columnRef)
		{
			if (!this._inputTable.Columns.Contains(columnRef.Target))
			{
				throw new InvalidOperationException(DevErrors.AddMissingItemsTableBuilder.ColumnIsNotInInputTable(columnRef.Target.Name.MarkAsModelInfo()));
			}
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x00047E9E File Offset: 0x0004609E
		public void AddContextTableExpression(QueryExpression expression)
		{
			Util.AddToLazyList<QueryExpression>(ref this._contextTables, expression);
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06001A11 RID: 6673
		public abstract bool IsEmpty { get; }

		// Token: 0x06001A12 RID: 6674
		public abstract void RemoveDuplicateKeys(HashSet<QdmTableColumnReferenceExpression> existingColumns);

		// Token: 0x06001A13 RID: 6675
		internal abstract IAddMissingItemsGroupItem ToGroupItem(QueryVariableReferenceExpression variableRef);

		// Token: 0x04000E7B RID: 3707
		private readonly QueryTable _inputTable;

		// Token: 0x04000E7C RID: 3708
		private List<QueryExpression> _contextTables;
	}
}
