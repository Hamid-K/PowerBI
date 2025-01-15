using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000064 RID: 100
	public abstract class SqlColumnEncryptionKeyStoreProvider
	{
		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x0001714C File Offset: 0x0001534C
		// (set) Token: 0x06000904 RID: 2308 RVA: 0x00017154 File Offset: 0x00015354
		public virtual TimeSpan? ColumnEncryptionKeyCacheTtl { get; set; } = new TimeSpan?(new TimeSpan(0L));

		// Token: 0x06000905 RID: 2309
		public abstract byte[] DecryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] encryptedColumnEncryptionKey);

		// Token: 0x06000906 RID: 2310
		public abstract byte[] EncryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] columnEncryptionKey);

		// Token: 0x06000907 RID: 2311 RVA: 0x0000E96E File Offset: 0x0000CB6E
		public virtual byte[] SignColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0000E96E File Offset: 0x0000CB6E
		public virtual bool VerifyColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations, byte[] signature)
		{
			throw new NotImplementedException();
		}
	}
}
