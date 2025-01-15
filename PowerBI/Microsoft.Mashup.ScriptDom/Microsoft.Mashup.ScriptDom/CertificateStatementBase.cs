using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200036F RID: 879
	[Serializable]
	internal abstract class CertificateStatementBase : TSqlStatement, IPasswordChangeOption
	{
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06002CC4 RID: 11460 RVA: 0x0016A819 File Offset: 0x00168A19
		// (set) Token: 0x06002CC5 RID: 11461 RVA: 0x0016A821 File Offset: 0x00168A21
		public Identifier Name
		{
			get
			{
				return this._name;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._name = value;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06002CC6 RID: 11462 RVA: 0x0016A831 File Offset: 0x00168A31
		// (set) Token: 0x06002CC7 RID: 11463 RVA: 0x0016A839 File Offset: 0x00168A39
		public OptionState ActiveForBeginDialog
		{
			get
			{
				return this._activeForBeginDialog;
			}
			set
			{
				this._activeForBeginDialog = value;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06002CC8 RID: 11464 RVA: 0x0016A842 File Offset: 0x00168A42
		// (set) Token: 0x06002CC9 RID: 11465 RVA: 0x0016A84A File Offset: 0x00168A4A
		public Literal PrivateKeyPath
		{
			get
			{
				return this._privateKeyPath;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._privateKeyPath = value;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06002CCA RID: 11466 RVA: 0x0016A85A File Offset: 0x00168A5A
		// (set) Token: 0x06002CCB RID: 11467 RVA: 0x0016A862 File Offset: 0x00168A62
		public Literal EncryptionPassword
		{
			get
			{
				return this._encryptionPassword;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._encryptionPassword = value;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06002CCC RID: 11468 RVA: 0x0016A872 File Offset: 0x00168A72
		// (set) Token: 0x06002CCD RID: 11469 RVA: 0x0016A87A File Offset: 0x00168A7A
		public Literal DecryptionPassword
		{
			get
			{
				return this._decryptionPassword;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._decryptionPassword = value;
			}
		}

		// Token: 0x06002CCE RID: 11470 RVA: 0x0016A88C File Offset: 0x00168A8C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.PrivateKeyPath != null)
			{
				this.PrivateKeyPath.Accept(visitor);
			}
			if (this.EncryptionPassword != null)
			{
				this.EncryptionPassword.Accept(visitor);
			}
			if (this.DecryptionPassword != null)
			{
				this.DecryptionPassword.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D1F RID: 7455
		private Identifier _name;

		// Token: 0x04001D20 RID: 7456
		private OptionState _activeForBeginDialog;

		// Token: 0x04001D21 RID: 7457
		private Literal _privateKeyPath;

		// Token: 0x04001D22 RID: 7458
		private Literal _encryptionPassword;

		// Token: 0x04001D23 RID: 7459
		private Literal _decryptionPassword;
	}
}
