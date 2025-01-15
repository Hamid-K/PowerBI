using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003AE RID: 942
	[Serializable]
	internal class BooleanNotExpression : BooleanExpression
	{
		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06002E64 RID: 11876 RVA: 0x0016C3F3 File Offset: 0x0016A5F3
		// (set) Token: 0x06002E65 RID: 11877 RVA: 0x0016C3FB File Offset: 0x0016A5FB
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

		// Token: 0x06002E66 RID: 11878 RVA: 0x0016C40B File Offset: 0x0016A60B
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002E67 RID: 11879 RVA: 0x0016C417 File Offset: 0x0016A617
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Expression != null)
			{
				this.Expression.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D9E RID: 7582
		private BooleanExpression _expression;
	}
}
