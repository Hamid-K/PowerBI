using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000361 RID: 865
	[Serializable]
	internal class CreateAsymmetricKeyStatement : TSqlStatement, IAuthorization
	{
		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06002C6B RID: 11371 RVA: 0x0016A1DE File Offset: 0x001683DE
		// (set) Token: 0x06002C6C RID: 11372 RVA: 0x0016A1E6 File Offset: 0x001683E6
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

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06002C6D RID: 11373 RVA: 0x0016A1F6 File Offset: 0x001683F6
		// (set) Token: 0x06002C6E RID: 11374 RVA: 0x0016A1FE File Offset: 0x001683FE
		public EncryptionSource KeySource
		{
			get
			{
				return this._keySource;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._keySource = value;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06002C6F RID: 11375 RVA: 0x0016A20E File Offset: 0x0016840E
		// (set) Token: 0x06002C70 RID: 11376 RVA: 0x0016A216 File Offset: 0x00168416
		public EncryptionAlgorithm EncryptionAlgorithm
		{
			get
			{
				return this._encryptionAlgorithm;
			}
			set
			{
				this._encryptionAlgorithm = value;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06002C71 RID: 11377 RVA: 0x0016A21F File Offset: 0x0016841F
		// (set) Token: 0x06002C72 RID: 11378 RVA: 0x0016A227 File Offset: 0x00168427
		public Literal Password
		{
			get
			{
				return this._password;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._password = value;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06002C73 RID: 11379 RVA: 0x0016A237 File Offset: 0x00168437
		// (set) Token: 0x06002C74 RID: 11380 RVA: 0x0016A23F File Offset: 0x0016843F
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

		// Token: 0x06002C75 RID: 11381 RVA: 0x0016A24F File Offset: 0x0016844F
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002C76 RID: 11382 RVA: 0x0016A25C File Offset: 0x0016845C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.KeySource != null)
			{
				this.KeySource.Accept(visitor);
			}
			if (this.Password != null)
			{
				this.Password.Accept(visitor);
			}
			if (this.Owner != null)
			{
				this.Owner.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D04 RID: 7428
		private Identifier _name;

		// Token: 0x04001D05 RID: 7429
		private EncryptionSource _keySource;

		// Token: 0x04001D06 RID: 7430
		private EncryptionAlgorithm _encryptionAlgorithm;

		// Token: 0x04001D07 RID: 7431
		private Literal _password;

		// Token: 0x04001D08 RID: 7432
		private Identifier _owner;
	}
}
