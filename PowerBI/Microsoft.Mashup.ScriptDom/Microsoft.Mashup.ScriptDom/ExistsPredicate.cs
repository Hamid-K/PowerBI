using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001E9 RID: 489
	[Serializable]
	internal class ExistsPredicate : BooleanExpression
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06002362 RID: 9058 RVA: 0x001607AD File Offset: 0x0015E9AD
		// (set) Token: 0x06002363 RID: 9059 RVA: 0x001607B5 File Offset: 0x0015E9B5
		public ScalarSubquery Subquery
		{
			get
			{
				return this._subquery;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._subquery = value;
			}
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x001607C5 File Offset: 0x0015E9C5
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x001607D1 File Offset: 0x0015E9D1
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Subquery != null)
			{
				this.Subquery.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A6A RID: 6762
		private ScalarSubquery _subquery;
	}
}
