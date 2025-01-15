using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000284 RID: 644
	[Serializable]
	internal class LockEscalationTableOption : TableOption
	{
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06002722 RID: 10018 RVA: 0x00164C1C File Offset: 0x00162E1C
		// (set) Token: 0x06002723 RID: 10019 RVA: 0x00164C24 File Offset: 0x00162E24
		public LockEscalationMethod Value
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

		// Token: 0x06002724 RID: 10020 RVA: 0x00164C2D File Offset: 0x00162E2D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002725 RID: 10021 RVA: 0x00164C39 File Offset: 0x00162E39
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B82 RID: 7042
		private LockEscalationMethod _value;
	}
}
