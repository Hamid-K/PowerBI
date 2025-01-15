using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000426 RID: 1062
	[Serializable]
	internal class RestoreMasterKeyStatement : BackupRestoreMasterKeyStatementBase
	{
		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06003125 RID: 12581 RVA: 0x0016EFC0 File Offset: 0x0016D1C0
		// (set) Token: 0x06003126 RID: 12582 RVA: 0x0016EFC8 File Offset: 0x0016D1C8
		public bool IsForce
		{
			get
			{
				return this._isForce;
			}
			set
			{
				this._isForce = value;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06003127 RID: 12583 RVA: 0x0016EFD1 File Offset: 0x0016D1D1
		// (set) Token: 0x06003128 RID: 12584 RVA: 0x0016EFD9 File Offset: 0x0016D1D9
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

		// Token: 0x06003129 RID: 12585 RVA: 0x0016EFE9 File Offset: 0x0016D1E9
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600312A RID: 12586 RVA: 0x0016EFF5 File Offset: 0x0016D1F5
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.EncryptionPassword != null)
			{
				this.EncryptionPassword.Accept(visitor);
			}
		}

		// Token: 0x04001E5C RID: 7772
		private bool _isForce;

		// Token: 0x04001E5D RID: 7773
		private Literal _encryptionPassword;
	}
}
