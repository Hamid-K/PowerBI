using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003C5 RID: 965
	[Serializable]
	internal class QueryParenthesisExpression : QueryExpression
	{
		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06002EE8 RID: 12008 RVA: 0x0016CD18 File Offset: 0x0016AF18
		// (set) Token: 0x06002EE9 RID: 12009 RVA: 0x0016CD20 File Offset: 0x0016AF20
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

		// Token: 0x06002EEA RID: 12010 RVA: 0x0016CD30 File Offset: 0x0016AF30
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002EEB RID: 12011 RVA: 0x0016CD3C File Offset: 0x0016AF3C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.QueryExpression != null)
			{
				this.QueryExpression.Accept(visitor);
			}
		}

		// Token: 0x04001DC3 RID: 7619
		private QueryExpression _queryExpression;
	}
}
