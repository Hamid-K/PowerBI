using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003FF RID: 1023
	[Serializable]
	internal class CertificateCreateLoginSource : CreateLoginSource
	{
		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06003031 RID: 12337 RVA: 0x0016E0BD File Offset: 0x0016C2BD
		// (set) Token: 0x06003032 RID: 12338 RVA: 0x0016E0C5 File Offset: 0x0016C2C5
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

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06003033 RID: 12339 RVA: 0x0016E0D5 File Offset: 0x0016C2D5
		// (set) Token: 0x06003034 RID: 12340 RVA: 0x0016E0DD File Offset: 0x0016C2DD
		public Identifier Credential
		{
			get
			{
				return this._credential;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._credential = value;
			}
		}

		// Token: 0x06003035 RID: 12341 RVA: 0x0016E0ED File Offset: 0x0016C2ED
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003036 RID: 12342 RVA: 0x0016E0F9 File Offset: 0x0016C2F9
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Certificate != null)
			{
				this.Certificate.Accept(visitor);
			}
			if (this.Credential != null)
			{
				this.Credential.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E17 RID: 7703
		private Identifier _certificate;

		// Token: 0x04001E18 RID: 7704
		private Identifier _credential;
	}
}
