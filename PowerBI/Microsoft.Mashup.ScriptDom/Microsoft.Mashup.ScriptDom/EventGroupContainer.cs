using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002B8 RID: 696
	[Serializable]
	internal class EventGroupContainer : EventTypeGroupContainer
	{
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06002869 RID: 10345 RVA: 0x00166203 File Offset: 0x00164403
		// (set) Token: 0x0600286A RID: 10346 RVA: 0x0016620B File Offset: 0x0016440B
		public EventNotificationEventGroup EventGroup
		{
			get
			{
				return this._eventGroup;
			}
			set
			{
				this._eventGroup = value;
			}
		}

		// Token: 0x0600286B RID: 10347 RVA: 0x00166214 File Offset: 0x00164414
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600286C RID: 10348 RVA: 0x00166220 File Offset: 0x00164420
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BE2 RID: 7138
		private EventNotificationEventGroup _eventGroup;
	}
}
