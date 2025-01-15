using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000453 RID: 1107
	[Serializable]
	internal class OnOffAuditTargetOption : AuditTargetOption
	{
		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x0600320A RID: 12810 RVA: 0x0016FCB1 File Offset: 0x0016DEB1
		// (set) Token: 0x0600320B RID: 12811 RVA: 0x0016FCB9 File Offset: 0x0016DEB9
		public OptionState Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x0600320C RID: 12812 RVA: 0x0016FCC2 File Offset: 0x0016DEC2
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600320D RID: 12813 RVA: 0x0016FCCE File Offset: 0x0016DECE
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E93 RID: 7827
		private OptionState _value;
	}
}
