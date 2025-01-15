using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002D5 RID: 725
	[Serializable]
	internal class ReturnStatement : TSqlStatement
	{
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060028FD RID: 10493 RVA: 0x00166B9E File Offset: 0x00164D9E
		// (set) Token: 0x060028FE RID: 10494 RVA: 0x00166BA6 File Offset: 0x00164DA6
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

		// Token: 0x060028FF RID: 10495 RVA: 0x00166BB6 File Offset: 0x00164DB6
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002900 RID: 10496 RVA: 0x00166BC2 File Offset: 0x00164DC2
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Expression != null)
			{
				this.Expression.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C07 RID: 7175
		private ScalarExpression _expression;
	}
}
