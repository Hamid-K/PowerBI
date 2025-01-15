using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002BA RID: 698
	[Serializable]
	internal class EventNotificationObjectScope : TSqlFragment
	{
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x0600287C RID: 10364 RVA: 0x00166359 File Offset: 0x00164559
		// (set) Token: 0x0600287D RID: 10365 RVA: 0x00166361 File Offset: 0x00164561
		public EventNotificationTarget Target
		{
			get
			{
				return this._target;
			}
			set
			{
				this._target = value;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x0600287E RID: 10366 RVA: 0x0016636A File Offset: 0x0016456A
		// (set) Token: 0x0600287F RID: 10367 RVA: 0x00166372 File Offset: 0x00164572
		public SchemaObjectName QueueName
		{
			get
			{
				return this._queueName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._queueName = value;
			}
		}

		// Token: 0x06002880 RID: 10368 RVA: 0x00166382 File Offset: 0x00164582
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x0016638E File Offset: 0x0016458E
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.QueueName != null)
			{
				this.QueueName.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BE9 RID: 7145
		private EventNotificationTarget _target;

		// Token: 0x04001BEA RID: 7146
		private SchemaObjectName _queueName;
	}
}
