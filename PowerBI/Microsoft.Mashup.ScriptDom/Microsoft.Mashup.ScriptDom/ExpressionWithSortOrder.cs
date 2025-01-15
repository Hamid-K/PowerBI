using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003B3 RID: 947
	[Serializable]
	internal class ExpressionWithSortOrder : TSqlFragment
	{
		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06002E87 RID: 11911 RVA: 0x0016C5EB File Offset: 0x0016A7EB
		// (set) Token: 0x06002E88 RID: 11912 RVA: 0x0016C5F3 File Offset: 0x0016A7F3
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

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06002E89 RID: 11913 RVA: 0x0016C5FC File Offset: 0x0016A7FC
		// (set) Token: 0x06002E8A RID: 11914 RVA: 0x0016C604 File Offset: 0x0016A804
		public ScalarExpression Expression
		{
			get
			{
				return this._expression;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._expression = value;
			}
		}

		// Token: 0x06002E8B RID: 11915 RVA: 0x0016C614 File Offset: 0x0016A814
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x0016C620 File Offset: 0x0016A820
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Expression != null)
			{
				this.Expression.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DA8 RID: 7592
		private SortOrder _sortOrder;

		// Token: 0x04001DA9 RID: 7593
		private ScalarExpression _expression;
	}
}
