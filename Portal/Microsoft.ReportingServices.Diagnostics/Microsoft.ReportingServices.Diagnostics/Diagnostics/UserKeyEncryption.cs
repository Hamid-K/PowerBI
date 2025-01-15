using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200006A RID: 106
	internal sealed class UserKeyEncryption : Encryption
	{
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000DD7A File Offset: 0x0000BF7A
		public static Encryption Instance
		{
			get
			{
				return UserKeyEncryption._instance;
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000B311 File Offset: 0x00009511
		private UserKeyEncryption()
		{
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000DD81 File Offset: 0x0000BF81
		protected override bool IsEncryptionChecked()
		{
			return this._encryptionChecked;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000DD89 File Offset: 0x0000BF89
		protected override void SetEncryptionChecked()
		{
			this._encryptionChecked = true;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000DD92 File Offset: 0x0000BF92
		protected override byte[] EncryptInternal(byte[] data)
		{
			return Encryption.EncryptNative(data, 1);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000DD9B File Offset: 0x0000BF9B
		public override byte[] Decrypt(byte[] data, bool useSalt = false)
		{
			return Encryption.DecryptNative(data, 1);
		}

		// Token: 0x04000337 RID: 823
		private const int UserKeyCryptoFlags = 1;

		// Token: 0x04000338 RID: 824
		private static readonly Encryption _instance = new UserKeyEncryption();

		// Token: 0x04000339 RID: 825
		private bool _encryptionChecked;
	}
}
