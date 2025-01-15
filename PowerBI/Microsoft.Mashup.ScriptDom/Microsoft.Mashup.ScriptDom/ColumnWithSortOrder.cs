using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000227 RID: 551
	[Serializable]
	internal class ColumnWithSortOrder : TSqlFragment
	{
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06002503 RID: 9475 RVA: 0x001626F3 File Offset: 0x001608F3
		// (set) Token: 0x06002504 RID: 9476 RVA: 0x001626FB File Offset: 0x001608FB
		public ColumnReferenceExpression Column
		{
			get
			{
				return this._column;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._column = value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06002505 RID: 9477 RVA: 0x0016270B File Offset: 0x0016090B
		// (set) Token: 0x06002506 RID: 9478 RVA: 0x00162713 File Offset: 0x00160913
		public SortOrder SortOrder
		{
			get
			{
				return this._sortOrder;
			}
			set
			{
				this._sortOrder = value;
			}
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x0016271C File Offset: 0x0016091C
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x00162728 File Offset: 0x00160928
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Column != null)
			{
				this.Column.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001AEC RID: 6892
		private ColumnReferenceExpression _column;

		// Token: 0x04001AED RID: 6893
		private SortOrder _sortOrder;
	}
}
