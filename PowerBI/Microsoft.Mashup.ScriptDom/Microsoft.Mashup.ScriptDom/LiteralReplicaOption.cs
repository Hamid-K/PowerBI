using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000491 RID: 1169
	[Serializable]
	internal class LiteralReplicaOption : AvailabilityReplicaOption
	{
		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06003366 RID: 13158 RVA: 0x001712AB File Offset: 0x0016F4AB
		// (set) Token: 0x06003367 RID: 13159 RVA: 0x001712B3 File Offset: 0x0016F4B3
		public Literal Value
		{
			get
			{
				return this._value;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._value = value;
			}
		}

		// Token: 0x06003368 RID: 13160 RVA: 0x001712C3 File Offset: 0x0016F4C3
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003369 RID: 13161 RVA: 0x001712CF File Offset: 0x0016F4CF
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
		}

		// Token: 0x04001EF2 RID: 7922
		private Literal _value;
	}
}
