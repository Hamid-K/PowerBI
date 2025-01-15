using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000220 RID: 544
	[Serializable]
	internal class ExtractFromExpression : ScalarExpression
	{
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060024E1 RID: 9441 RVA: 0x00162479 File Offset: 0x00160679
		// (set) Token: 0x060024E2 RID: 9442 RVA: 0x00162481 File Offset: 0x00160681
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

		// Token: 0x060024E3 RID: 9443 RVA: 0x00162491 File Offset: 0x00160691
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x0016249D File Offset: 0x0016069D
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Expression != null)
			{
				this.Expression.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001AE5 RID: 6885
		private ScalarExpression _expression;
	}
}
