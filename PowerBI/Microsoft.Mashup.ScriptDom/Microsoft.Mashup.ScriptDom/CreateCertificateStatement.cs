using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000371 RID: 881
	[Serializable]
	internal class CreateCertificateStatement : CertificateStatementBase, IAuthorization
	{
		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06002CD7 RID: 11479 RVA: 0x0016A952 File Offset: 0x00168B52
		// (set) Token: 0x06002CD8 RID: 11480 RVA: 0x0016A95A File Offset: 0x00168B5A
		public EncryptionSource CertificateSource
		{
			get
			{
				return this._certificateSource;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._certificateSource = value;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06002CD9 RID: 11481 RVA: 0x0016A96A File Offset: 0x00168B6A
		public IList<CertificateOption> CertificateOptions
		{
			get
			{
				return this._certificateOptions;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06002CDA RID: 11482 RVA: 0x0016A972 File Offset: 0x00168B72
		// (set) Token: 0x06002CDB RID: 11483 RVA: 0x0016A97A File Offset: 0x00168B7A
		public Identifier Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._owner = value;
			}
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x0016A98A File Offset: 0x00168B8A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x0016A998 File Offset: 0x00168B98
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (base.Name != null)
			{
				base.Name.Accept(visitor);
			}
			if (this.CertificateSource != null)
			{
				this.CertificateSource.Accept(visitor);
			}
			if (base.PrivateKeyPath != null)
			{
				base.PrivateKeyPath.Accept(visitor);
			}
			int i = 0;
			int count = this.CertificateOptions.Count;
			while (i < count)
			{
				this.CertificateOptions[i].Accept(visitor);
				i++;
			}
			if (base.EncryptionPassword != null)
			{
				base.EncryptionPassword.Accept(visitor);
			}
			if (base.DecryptionPassword != null)
			{
				base.DecryptionPassword.Accept(visitor);
			}
			if (this.Owner != null)
			{
				this.Owner.Accept(visitor);
			}
		}

		// Token: 0x04001D26 RID: 7462
		private EncryptionSource _certificateSource;

		// Token: 0x04001D27 RID: 7463
		private List<CertificateOption> _certificateOptions = new List<CertificateOption>();

		// Token: 0x04001D28 RID: 7464
		private Identifier _owner;
	}
}
