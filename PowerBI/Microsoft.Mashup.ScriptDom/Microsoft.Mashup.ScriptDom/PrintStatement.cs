using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000261 RID: 609
	[Serializable]
	internal class PrintStatement : TSqlStatement
	{
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06002664 RID: 9828 RVA: 0x00163FDD File Offset: 0x001621DD
		// (set) Token: 0x06002665 RID: 9829 RVA: 0x00163FE5 File Offset: 0x001621E5
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

		// Token: 0x06002666 RID: 9830 RVA: 0x00163FF5 File Offset: 0x001621F5
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x00164001 File Offset: 0x00162201
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Expression != null)
			{
				this.Expression.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B51 RID: 6993
		private ScalarExpression _expression;
	}
}
