using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001E5 RID: 485
	[Serializable]
	internal class QueryDerivedTable : TableReferenceWithAliasAndColumns
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600234C RID: 9036 RVA: 0x00160656 File Offset: 0x0015E856
		// (set) Token: 0x0600234D RID: 9037 RVA: 0x0016065E File Offset: 0x0015E85E
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

		// Token: 0x0600234E RID: 9038 RVA: 0x0016066E File Offset: 0x0015E86E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600234F RID: 9039 RVA: 0x0016067A File Offset: 0x0015E87A
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.QueryExpression != null)
			{
				this.QueryExpression.Accept(visitor);
			}
		}

		// Token: 0x04001A64 RID: 6756
		private QueryExpression _queryExpression;
	}
}
