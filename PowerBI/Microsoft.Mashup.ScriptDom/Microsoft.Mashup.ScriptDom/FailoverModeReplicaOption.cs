using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000493 RID: 1171
	[Serializable]
	internal class FailoverModeReplicaOption : AvailabilityReplicaOption
	{
		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06003370 RID: 13168 RVA: 0x00171322 File Offset: 0x0016F522
		// (set) Token: 0x06003371 RID: 13169 RVA: 0x0017132A File Offset: 0x0016F52A
		public FailoverModeOptionKind Value
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

		// Token: 0x06003372 RID: 13170 RVA: 0x00171333 File Offset: 0x0016F533
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x0017133F File Offset: 0x0016F53F
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EF4 RID: 7924
		private FailoverModeOptionKind _value;
	}
}
