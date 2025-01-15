using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002DE RID: 734
	[Serializable]
	internal class CryptoMechanism : TSqlFragment
	{
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06002937 RID: 10551 RVA: 0x00166F36 File Offset: 0x00165136
		// (set) Token: 0x06002938 RID: 10552 RVA: 0x00166F3E File Offset: 0x0016513E
		public CryptoMechanismType CryptoMechanismType
		{
			get
			{
				return this._cryptoMechanismType;
			}
			set
			{
				this._cryptoMechanismType = value;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06002939 RID: 10553 RVA: 0x00166F47 File Offset: 0x00165147
		// (set) Token: 0x0600293A RID: 10554 RVA: 0x00166F4F File Offset: 0x0016514F
		public Identifier Identifier
		{
			get
			{
				return this._identifier;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._identifier = value;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x0600293B RID: 10555 RVA: 0x00166F5F File Offset: 0x0016515F
		// (set) Token: 0x0600293C RID: 10556 RVA: 0x00166F67 File Offset: 0x00165167
		public Literal PasswordOrSignature
		{
			get
			{
				return this._passwordOrSignature;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._passwordOrSignature = value;
			}
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x00166F77 File Offset: 0x00165177
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x00166F83 File Offset: 0x00165183
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Identifier != null)
			{
				this.Identifier.Accept(visitor);
			}
			if (this.PasswordOrSignature != null)
			{
				this.PasswordOrSignature.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C18 RID: 7192
		private CryptoMechanismType _cryptoMechanismType;

		// Token: 0x04001C19 RID: 7193
		private Identifier _identifier;

		// Token: 0x04001C1A RID: 7194
		private Literal _passwordOrSignature;
	}
}
