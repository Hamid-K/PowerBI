using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000057 RID: 87
	internal abstract class SqlClientEncryptionAlgorithm
	{
		// Token: 0x0600085A RID: 2138
		internal abstract byte[] EncryptData(byte[] plainText);

		// Token: 0x0600085B RID: 2139
		internal abstract byte[] DecryptData(byte[] cipherText);
	}
}
