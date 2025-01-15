using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003C4 RID: 964
	[Serializable]
	internal abstract class QueryExpression : TSqlFragment
	{
		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06002EE0 RID: 12000 RVA: 0x0016CC75 File Offset: 0x0016AE75
		// (set) Token: 0x06002EE1 RID: 12001 RVA: 0x0016CC7D File Offset: 0x0016AE7D
		public OrderByClause OrderByClause
		{
			get
			{
				return this._orderByClause;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._orderByClause = value;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06002EE2 RID: 12002 RVA: 0x0016CC8D File Offset: 0x0016AE8D
		// (set) Token: 0x06002EE3 RID: 12003 RVA: 0x0016CC95 File Offset: 0x0016AE95
		public OffsetClause OffsetClause
		{
			get
			{
				return this._offsetClause;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._offsetClause = value;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06002EE4 RID: 12004 RVA: 0x0016CCA5 File Offset: 0x0016AEA5
		// (set) Token: 0x06002EE5 RID: 12005 RVA: 0x0016CCAD File Offset: 0x0016AEAD
		public ForClause ForClause
		{
			get
			{
				return this._forClause;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._forClause = value;
			}
		}

		// Token: 0x06002EE6 RID: 12006 RVA: 0x0016CCC0 File Offset: 0x0016AEC0
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.OrderByClause != null)
			{
				this.OrderByClause.Accept(visitor);
			}
			if (this.OffsetClause != null)
			{
				this.OffsetClause.Accept(visitor);
			}
			if (this.ForClause != null)
			{
				this.ForClause.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DC0 RID: 7616
		private OrderByClause _orderByClause;

		// Token: 0x04001DC1 RID: 7617
		private OffsetClause _offsetClause;

		// Token: 0x04001DC2 RID: 7618
		private ForClause _forClause;
	}
}
