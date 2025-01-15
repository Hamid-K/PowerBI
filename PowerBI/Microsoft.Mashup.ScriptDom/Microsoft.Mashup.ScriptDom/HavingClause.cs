using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003BE RID: 958
	[Serializable]
	internal class HavingClause : TSqlFragment
	{
		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06002EBB RID: 11963 RVA: 0x0016CA1A File Offset: 0x0016AC1A
		// (set) Token: 0x06002EBC RID: 11964 RVA: 0x0016CA22 File Offset: 0x0016AC22
		public BooleanExpression SearchCondition
		{
			get
			{
				return this._searchCondition;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._searchCondition = value;
			}
		}

		// Token: 0x06002EBD RID: 11965 RVA: 0x0016CA32 File Offset: 0x0016AC32
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002EBE RID: 11966 RVA: 0x0016CA3E File Offset: 0x0016AC3E
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.SearchCondition != null)
			{
				this.SearchCondition.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DB6 RID: 7606
		private BooleanExpression _searchCondition;
	}
}
