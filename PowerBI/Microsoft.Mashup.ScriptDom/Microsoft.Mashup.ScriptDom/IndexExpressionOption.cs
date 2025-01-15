using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002AE RID: 686
	[Serializable]
	internal class IndexExpressionOption : IndexOption
	{
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06002827 RID: 10279 RVA: 0x00165E0C File Offset: 0x0016400C
		// (set) Token: 0x06002828 RID: 10280 RVA: 0x00165E14 File Offset: 0x00164014
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

		// Token: 0x06002829 RID: 10281 RVA: 0x00165E24 File Offset: 0x00164024
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x00165E30 File Offset: 0x00164030
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Expression != null)
			{
				this.Expression.Accept(visitor);
			}
		}

		// Token: 0x04001BCE RID: 7118
		private ScalarExpression _expression;
	}
}
