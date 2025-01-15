using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000492 RID: 1170
	[Serializable]
	internal class AvailabilityModeReplicaOption : AvailabilityReplicaOption
	{
		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x0600336B RID: 13163 RVA: 0x001712F4 File Offset: 0x0016F4F4
		// (set) Token: 0x0600336C RID: 13164 RVA: 0x001712FC File Offset: 0x0016F4FC
		public AvailabilityModeOptionKind Value
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

		// Token: 0x0600336D RID: 13165 RVA: 0x00171305 File Offset: 0x0016F505
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600336E RID: 13166 RVA: 0x00171311 File Offset: 0x0016F511
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EF3 RID: 7923
		private AvailabilityModeOptionKind _value;
	}
}
