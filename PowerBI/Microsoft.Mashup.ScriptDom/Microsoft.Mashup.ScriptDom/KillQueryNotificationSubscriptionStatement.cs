using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002FD RID: 765
	[Serializable]
	internal class KillQueryNotificationSubscriptionStatement : TSqlStatement
	{
		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060029DA RID: 10714 RVA: 0x00167957 File Offset: 0x00165B57
		// (set) Token: 0x060029DB RID: 10715 RVA: 0x0016795F File Offset: 0x00165B5F
		public Literal SubscriptionId
		{
			get
			{
				return this._subscriptionId;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._subscriptionId = value;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060029DC RID: 10716 RVA: 0x0016796F File Offset: 0x00165B6F
		// (set) Token: 0x060029DD RID: 10717 RVA: 0x00167977 File Offset: 0x00165B77
		public bool All
		{
			get
			{
				return this._all;
			}
			set
			{
				this._all = value;
			}
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x00167980 File Offset: 0x00165B80
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x0016798C File Offset: 0x00165B8C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.SubscriptionId != null)
			{
				this.SubscriptionId.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C3F RID: 7231
		private Literal _subscriptionId;

		// Token: 0x04001C40 RID: 7232
		private bool _all;
	}
}
