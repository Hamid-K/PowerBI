using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200041A RID: 1050
	[Serializable]
	internal class AlterAsymmetricKeyStatement : TSqlStatement, IPasswordChangeOption
	{
		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060030D4 RID: 12500 RVA: 0x0016EAD7 File Offset: 0x0016CCD7
		// (set) Token: 0x060030D5 RID: 12501 RVA: 0x0016EADF File Offset: 0x0016CCDF
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

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060030D6 RID: 12502 RVA: 0x0016EAEF File Offset: 0x0016CCEF
		// (set) Token: 0x060030D7 RID: 12503 RVA: 0x0016EAF7 File Offset: 0x0016CCF7
		public Literal AttestedBy
		{
			get
			{
				return this._attestedBy;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._attestedBy = value;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060030D8 RID: 12504 RVA: 0x0016EB07 File Offset: 0x0016CD07
		// (set) Token: 0x060030D9 RID: 12505 RVA: 0x0016EB0F File Offset: 0x0016CD0F
		public AlterCertificateStatementKind Kind
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

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060030DA RID: 12506 RVA: 0x0016EB18 File Offset: 0x0016CD18
		// (set) Token: 0x060030DB RID: 12507 RVA: 0x0016EB20 File Offset: 0x0016CD20
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

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060030DC RID: 12508 RVA: 0x0016EB30 File Offset: 0x0016CD30
		// (set) Token: 0x060030DD RID: 12509 RVA: 0x0016EB38 File Offset: 0x0016CD38
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

		// Token: 0x060030DE RID: 12510 RVA: 0x0016EB48 File Offset: 0x0016CD48
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x0016EB54 File Offset: 0x0016CD54
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.AttestedBy != null)
			{
				this.AttestedBy.Accept(visitor);
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

		// Token: 0x04001E44 RID: 7748
		private Identifier _name;

		// Token: 0x04001E45 RID: 7749
		private Literal _attestedBy;

		// Token: 0x04001E46 RID: 7750
		private AlterCertificateStatementKind _kind;

		// Token: 0x04001E47 RID: 7751
		private Literal _encryptionPassword;

		// Token: 0x04001E48 RID: 7752
		private Literal _decryptionPassword;
	}
}
