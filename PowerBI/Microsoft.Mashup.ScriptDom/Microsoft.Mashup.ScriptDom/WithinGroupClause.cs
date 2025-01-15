using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020004A5 RID: 1189
	[Serializable]
	internal class WithinGroupClause : TSqlFragment
	{
		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060033E7 RID: 13287 RVA: 0x00171A56 File Offset: 0x0016FC56
		// (set) Token: 0x060033E8 RID: 13288 RVA: 0x00171A5E File Offset: 0x0016FC5E
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

		// Token: 0x060033E9 RID: 13289 RVA: 0x00171A6E File Offset: 0x0016FC6E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060033EA RID: 13290 RVA: 0x00171A7A File Offset: 0x0016FC7A
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.OrderByClause != null)
			{
				this.OrderByClause.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001F17 RID: 7959
		private OrderByClause _orderByClause;
	}
}
