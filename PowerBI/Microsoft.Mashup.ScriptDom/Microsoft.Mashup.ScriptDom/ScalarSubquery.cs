using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200021E RID: 542
	[Serializable]
	internal class ScalarSubquery : PrimaryExpression
	{
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060024D4 RID: 9428 RVA: 0x0016238B File Offset: 0x0016058B
		// (set) Token: 0x060024D5 RID: 9429 RVA: 0x00162393 File Offset: 0x00160593
		public QueryExpression QueryExpression
		{
			get
			{
				return this._queryExpression;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._queryExpression = value;
			}
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x001623A3 File Offset: 0x001605A3
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x001623AF File Offset: 0x001605AF
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.QueryExpression != null)
			{
				this.QueryExpression.Accept(visitor);
			}
		}

		// Token: 0x04001AE1 RID: 6881
		private QueryExpression _queryExpression;
	}
}
