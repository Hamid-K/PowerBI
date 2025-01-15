using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000411 RID: 1041
	[Serializable]
	internal class DropEventNotificationStatement : TSqlStatement
	{
		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x0600308B RID: 12427 RVA: 0x0016E59B File Offset: 0x0016C79B
		public IList<Identifier> Notifications
		{
			get
			{
				return this._notifications;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x0600308C RID: 12428 RVA: 0x0016E5A3 File Offset: 0x0016C7A3
		// (set) Token: 0x0600308D RID: 12429 RVA: 0x0016E5AB File Offset: 0x0016C7AB
		public EventNotificationObjectScope Scope
		{
			get
			{
				return this._scope;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._scope = value;
			}
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x0016E5BB File Offset: 0x0016C7BB
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600308F RID: 12431 RVA: 0x0016E5C8 File Offset: 0x0016C7C8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Notifications.Count;
			while (i < count)
			{
				this.Notifications[i].Accept(visitor);
				i++;
			}
			if (this.Scope != null)
			{
				this.Scope.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E2B RID: 7723
		private List<Identifier> _notifications = new List<Identifier>();

		// Token: 0x04001E2C RID: 7724
		private EventNotificationObjectScope _scope;
	}
}
