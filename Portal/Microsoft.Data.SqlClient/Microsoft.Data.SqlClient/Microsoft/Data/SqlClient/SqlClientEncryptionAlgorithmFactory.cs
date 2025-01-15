using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000058 RID: 88
	internal abstract class SqlClientEncryptionAlgorithmFactory
	{
		// Token: 0x0600085D RID: 2141
		internal abstract SqlClientEncryptionAlgorithm Create(SqlClientSymmetricKey encryptionKey, SqlClientEncryptionType encryptionType, string encryptionAlgorithm);
	}
}
