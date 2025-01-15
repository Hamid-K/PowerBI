using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000216 RID: 534
	[Serializable]
	internal class ExpressionCallTarget : CallTarget
	{
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060024A8 RID: 9384 RVA: 0x00162032 File Offset: 0x00160232
		// (set) Token: 0x060024A9 RID: 9385 RVA: 0x0016203A File Offset: 0x0016023A
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

		// Token: 0x060024AA RID: 9386 RVA: 0x0016204A File Offset: 0x0016024A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x00162056 File Offset: 0x00160256
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Expression != null)
			{
				this.Expression.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001AD5 RID: 6869
		private ScalarExpression _expression;
	}
}
