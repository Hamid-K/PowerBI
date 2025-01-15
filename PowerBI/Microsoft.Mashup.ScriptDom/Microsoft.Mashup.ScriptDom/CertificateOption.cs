using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000372 RID: 882
	[Serializable]
	internal class CertificateOption : TSqlFragment
	{
		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06002CDF RID: 11487 RVA: 0x0016AA5A File Offset: 0x00168C5A
		// (set) Token: 0x06002CE0 RID: 11488 RVA: 0x0016AA62 File Offset: 0x00168C62
		public CertificateOptionKinds Kind
		{
			get
			{
				return this._kind;
			}
			set
			{
				this._kind = value;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06002CE1 RID: 11489 RVA: 0x0016AA6B File Offset: 0x00168C6B
		// (set) Token: 0x06002CE2 RID: 11490 RVA: 0x0016AA73 File Offset: 0x00168C73
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

		// Token: 0x06002CE3 RID: 11491 RVA: 0x0016AA83 File Offset: 0x00168C83
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002CE4 RID: 11492 RVA: 0x0016AA8F File Offset: 0x00168C8F
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D29 RID: 7465
		private CertificateOptionKinds _kind;

		// Token: 0x04001D2A RID: 7466
		private Literal _value;
	}
}
