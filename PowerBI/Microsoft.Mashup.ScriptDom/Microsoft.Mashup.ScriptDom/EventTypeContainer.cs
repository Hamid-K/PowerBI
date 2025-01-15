using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002B7 RID: 695
	[Serializable]
	internal class EventTypeContainer : EventTypeGroupContainer
	{
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06002864 RID: 10340 RVA: 0x001661D5 File Offset: 0x001643D5
		// (set) Token: 0x06002865 RID: 10341 RVA: 0x001661DD File Offset: 0x001643DD
		public EventNotificationEventType EventType
		{
			get
			{
				return this._eventType;
			}
			set
			{
				this._eventType = value;
			}
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x001661E6 File Offset: 0x001643E6
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002867 RID: 10343 RVA: 0x001661F2 File Offset: 0x001643F2
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BE1 RID: 7137
		private EventNotificationEventType _eventType;
	}
}
