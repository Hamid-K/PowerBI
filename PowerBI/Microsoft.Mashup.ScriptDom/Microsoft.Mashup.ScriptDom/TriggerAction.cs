using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001BC RID: 444
	[Serializable]
	internal class TriggerAction : TSqlFragment
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600226D RID: 8813 RVA: 0x0015F5B1 File Offset: 0x0015D7B1
		// (set) Token: 0x0600226E RID: 8814 RVA: 0x0015F5B9 File Offset: 0x0015D7B9
		public TriggerActionType TriggerActionType
		{
			get
			{
				return this._triggerActionType;
			}
			set
			{
				this._triggerActionType = value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600226F RID: 8815 RVA: 0x0015F5C2 File Offset: 0x0015D7C2
		// (set) Token: 0x06002270 RID: 8816 RVA: 0x0015F5CA File Offset: 0x0015D7CA
		public EventTypeGroupContainer EventTypeGroup
		{
			get
			{
				return this._eventTypeGroup;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._eventTypeGroup = value;
			}
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x0015F5DA File Offset: 0x0015D7DA
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x0015F5E6 File Offset: 0x0015D7E6
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.EventTypeGroup != null)
			{
				this.EventTypeGroup.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A25 RID: 6693
		private TriggerActionType _triggerActionType;

		// Token: 0x04001A26 RID: 6694
		private EventTypeGroupContainer _eventTypeGroup;
	}
}
