using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000391 RID: 913
	[Serializable]
	internal class AuthenticationPayloadOption : PayloadOption
	{
		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06002DA9 RID: 11689 RVA: 0x0016B693 File Offset: 0x00169893
		// (set) Token: 0x06002DAA RID: 11690 RVA: 0x0016B69B File Offset: 0x0016989B
		public AuthenticationProtocol Protocol
		{
			get
			{
				return this._protocol;
			}
			set
			{
				this._protocol = value;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06002DAB RID: 11691 RVA: 0x0016B6A4 File Offset: 0x001698A4
		// (set) Token: 0x06002DAC RID: 11692 RVA: 0x0016B6AC File Offset: 0x001698AC
		public Identifier Certificate
		{
			get
			{
				return this._certificate;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._certificate = value;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06002DAD RID: 11693 RVA: 0x0016B6BC File Offset: 0x001698BC
		// (set) Token: 0x06002DAE RID: 11694 RVA: 0x0016B6C4 File Offset: 0x001698C4
		public bool TryCertificateFirst
		{
			get
			{
				return this._tryCertificateFirst;
			}
			set
			{
				this._tryCertificateFirst = value;
			}
		}

		// Token: 0x06002DAF RID: 11695 RVA: 0x0016B6CD File Offset: 0x001698CD
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002DB0 RID: 11696 RVA: 0x0016B6D9 File Offset: 0x001698D9
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Certificate != null)
			{
				this.Certificate.Accept(visitor);
			}
		}

		// Token: 0x04001D64 RID: 7524
		private AuthenticationProtocol _protocol;

		// Token: 0x04001D65 RID: 7525
		private Identifier _certificate;

		// Token: 0x04001D66 RID: 7526
		private bool _tryCertificateFirst;
	}
}
