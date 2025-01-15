using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200044C RID: 1100
	[Serializable]
	internal class AuditGuidAuditOption : AuditOption
	{
		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060031E2 RID: 12770 RVA: 0x0016FADC File Offset: 0x0016DCDC
		// (set) Token: 0x060031E3 RID: 12771 RVA: 0x0016FAE4 File Offset: 0x0016DCE4
		public Literal Guid
		{
			get
			{
				return this._guid;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._guid = value;
			}
		}

		// Token: 0x060031E4 RID: 12772 RVA: 0x0016FAF4 File Offset: 0x0016DCF4
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060031E5 RID: 12773 RVA: 0x0016FB00 File Offset: 0x0016DD00
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Guid != null)
			{
				this.Guid.Accept(visitor);
			}
		}

		// Token: 0x04001E89 RID: 7817
		private Literal _guid;
	}
}
