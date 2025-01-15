using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200044E RID: 1102
	[Serializable]
	internal class StateAuditOption : AuditOption
	{
		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060031EC RID: 12780 RVA: 0x0016FB53 File Offset: 0x0016DD53
		// (set) Token: 0x060031ED RID: 12781 RVA: 0x0016FB5B File Offset: 0x0016DD5B
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

		// Token: 0x060031EE RID: 12782 RVA: 0x0016FB64 File Offset: 0x0016DD64
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060031EF RID: 12783 RVA: 0x0016FB70 File Offset: 0x0016DD70
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E8B RID: 7819
		private OptionState _value;
	}
}
