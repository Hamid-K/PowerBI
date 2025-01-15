using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003AF RID: 943
	[Serializable]
	internal class BooleanParenthesisExpression : BooleanExpression
	{
		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06002E69 RID: 11881 RVA: 0x0016C43C File Offset: 0x0016A63C
		// (set) Token: 0x06002E6A RID: 11882 RVA: 0x0016C444 File Offset: 0x0016A644
		public BooleanExpression Expression
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

		// Token: 0x06002E6B RID: 11883 RVA: 0x0016C454 File Offset: 0x0016A654
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x0016C460 File Offset: 0x0016A660
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Expression != null)
			{
				this.Expression.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D9F RID: 7583
		private BooleanExpression _expression;
	}
}
