using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000268 RID: 616
	[Serializable]
	internal class ParenthesisExpression : PrimaryExpression
	{
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600268D RID: 9869 RVA: 0x0016422A File Offset: 0x0016242A
		// (set) Token: 0x0600268E RID: 9870 RVA: 0x00164232 File Offset: 0x00162432
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

		// Token: 0x0600268F RID: 9871 RVA: 0x00164242 File Offset: 0x00162442
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x0016424E File Offset: 0x0016244E
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Expression != null)
			{
				this.Expression.Accept(visitor);
			}
		}

		// Token: 0x04001B5B RID: 7003
		private ScalarExpression _expression;
	}
}
