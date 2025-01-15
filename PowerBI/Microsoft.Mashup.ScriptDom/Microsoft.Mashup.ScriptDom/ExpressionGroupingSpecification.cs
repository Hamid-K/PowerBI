using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003B6 RID: 950
	[Serializable]
	internal class ExpressionGroupingSpecification : GroupingSpecification
	{
		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06002E98 RID: 11928 RVA: 0x0016C6DE File Offset: 0x0016A8DE
		// (set) Token: 0x06002E99 RID: 11929 RVA: 0x0016C6E6 File Offset: 0x0016A8E6
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

		// Token: 0x06002E9A RID: 11930 RVA: 0x0016C6F6 File Offset: 0x0016A8F6
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002E9B RID: 11931 RVA: 0x0016C702 File Offset: 0x0016A902
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Expression != null)
			{
				this.Expression.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DAD RID: 7597
		private ScalarExpression _expression;
	}
}
