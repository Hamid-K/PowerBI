using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002B9 RID: 697
	[Serializable]
	internal class CreateEventNotificationStatement : TSqlStatement
	{
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x0600286E RID: 10350 RVA: 0x00166231 File Offset: 0x00164431
		// (set) Token: 0x0600286F RID: 10351 RVA: 0x00166239 File Offset: 0x00164439
		public Identifier Name
		{
			get
			{
				return this._name;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._name = value;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06002870 RID: 10352 RVA: 0x00166249 File Offset: 0x00164449
		// (set) Token: 0x06002871 RID: 10353 RVA: 0x00166251 File Offset: 0x00164451
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

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06002872 RID: 10354 RVA: 0x00166261 File Offset: 0x00164461
		// (set) Token: 0x06002873 RID: 10355 RVA: 0x00166269 File Offset: 0x00164469
		public bool WithFanIn
		{
			get
			{
				return this._withFanIn;
			}
			set
			{
				this._withFanIn = value;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06002874 RID: 10356 RVA: 0x00166272 File Offset: 0x00164472
		public IList<EventTypeGroupContainer> EventTypeGroups
		{
			get
			{
				return this._eventTypeGroups;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06002875 RID: 10357 RVA: 0x0016627A File Offset: 0x0016447A
		// (set) Token: 0x06002876 RID: 10358 RVA: 0x00166282 File Offset: 0x00164482
		public Literal BrokerService
		{
			get
			{
				return this._brokerService;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._brokerService = value;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06002877 RID: 10359 RVA: 0x00166292 File Offset: 0x00164492
		// (set) Token: 0x06002878 RID: 10360 RVA: 0x0016629A File Offset: 0x0016449A
		public Literal BrokerInstanceSpecifier
		{
			get
			{
				return this._brokerInstanceSpecifier;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._brokerInstanceSpecifier = value;
			}
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x001662AA File Offset: 0x001644AA
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x001662B8 File Offset: 0x001644B8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.Scope != null)
			{
				this.Scope.Accept(visitor);
			}
			int i = 0;
			int count = this.EventTypeGroups.Count;
			while (i < count)
			{
				this.EventTypeGroups[i].Accept(visitor);
				i++;
			}
			if (this.BrokerService != null)
			{
				this.BrokerService.Accept(visitor);
			}
			if (this.BrokerInstanceSpecifier != null)
			{
				this.BrokerInstanceSpecifier.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BE3 RID: 7139
		private Identifier _name;

		// Token: 0x04001BE4 RID: 7140
		private EventNotificationObjectScope _scope;

		// Token: 0x04001BE5 RID: 7141
		private bool _withFanIn;

		// Token: 0x04001BE6 RID: 7142
		private List<EventTypeGroupContainer> _eventTypeGroups = new List<EventTypeGroupContainer>();

		// Token: 0x04001BE7 RID: 7143
		private Literal _brokerService;

		// Token: 0x04001BE8 RID: 7144
		private Literal _brokerInstanceSpecifier;
	}
}
