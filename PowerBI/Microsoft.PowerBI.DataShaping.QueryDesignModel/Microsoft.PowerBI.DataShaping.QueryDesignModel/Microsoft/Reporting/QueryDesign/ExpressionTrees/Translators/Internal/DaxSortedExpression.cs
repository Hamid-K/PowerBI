using System;
using System.Collections.Generic;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x02000143 RID: 323
	internal sealed class DaxSortedExpression : DaxExpression
	{
		// Token: 0x0600118F RID: 4495 RVA: 0x00030EDC File Offset: 0x0002F0DC
		internal DaxSortedExpression(string text, IReadOnlyList<DaxResultColumn> resultColumns, IReadOnlyList<DaxSortItem> sortItems)
			: base(text, resultColumns)
		{
			this._sortItems = sortItems;
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x00030EED File Offset: 0x0002F0ED
		internal IReadOnlyList<DaxSortItem> SortItems
		{
			get
			{
				return this._sortItems;
			}
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00030EF8 File Offset: 0x0002F0F8
		protected override QueryResultFieldSortInformation GetSortInfo(DaxResultColumn resultColumn)
		{
			for (int i = 0; i < this._sortItems.Count; i++)
			{
				DaxSortItem daxSortItem = this._sortItems[i];
				if (daxSortItem.Column.Equals(resultColumn.DaxColumnRef))
				{
					return new QueryResultFieldSortInformation(i, daxSortItem.Direction);
				}
			}
			return null;
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00030F4F File Offset: 0x0002F14F
		internal override DaxExpression ReplaceText(string newText)
		{
			return new DaxSortedExpression(newText, base.ResultColumns, this.SortItems);
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00030F63 File Offset: 0x0002F163
		internal override DaxExpression ReplaceResultColumns(IReadOnlyList<DaxResultColumn> newResultColumns)
		{
			return new DaxSortedExpression(base.Text, newResultColumns, this.SortItems);
		}

		// Token: 0x04000ADB RID: 2779
		private readonly IReadOnlyList<DaxSortItem> _sortItems;
	}
}
