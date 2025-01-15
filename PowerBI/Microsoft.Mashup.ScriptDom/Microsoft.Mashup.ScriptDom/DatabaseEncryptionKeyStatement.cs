using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000454 RID: 1108
	[Serializable]
	internal abstract class DatabaseEncryptionKeyStatement : TSqlStatement
	{
		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x0600320F RID: 12815 RVA: 0x0016FCDF File Offset: 0x0016DEDF
		// (set) Token: 0x06003210 RID: 12816 RVA: 0x0016FCE7 File Offset: 0x0016DEE7
		public CryptoMechanism Encryptor
		{
			get
			{
				return this._encryptor;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._encryptor = value;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06003211 RID: 12817 RVA: 0x0016FCF7 File Offset: 0x0016DEF7
		// (set) Token: 0x06003212 RID: 12818 RVA: 0x0016FCFF File Offset: 0x0016DEFF
		public DatabaseEncryptionKeyAlgorithm Algorithm
		{
			get
			{
				return this._algorithm;
			}
			set
			{
				this._algorithm = value;
			}
		}

		// Token: 0x06003213 RID: 12819 RVA: 0x0016FD08 File Offset: 0x0016DF08
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Encryptor != null)
			{
				this.Encryptor.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E94 RID: 7828
		private CryptoMechanism _encryptor;

		// Token: 0x04001E95 RID: 7829
		private DatabaseEncryptionKeyAlgorithm _algorithm;
	}
}
