using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200044B RID: 1099
	[Serializable]
	internal class QueueDelayAuditOption : AuditOption
	{
		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060031DD RID: 12765 RVA: 0x0016FA93 File Offset: 0x0016DC93
		// (set) Token: 0x060031DE RID: 12766 RVA: 0x0016FA9B File Offset: 0x0016DC9B
		public Literal Delay
		{
			get
			{
				return this._delay;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._delay = value;
			}
		}

		// Token: 0x060031DF RID: 12767 RVA: 0x0016FAAB File Offset: 0x0016DCAB
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x0016FAB7 File Offset: 0x0016DCB7
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Delay != null)
			{
				this.Delay.Accept(visitor);
			}
		}

		// Token: 0x04001E88 RID: 7816
		private Literal _delay;
	}
}
