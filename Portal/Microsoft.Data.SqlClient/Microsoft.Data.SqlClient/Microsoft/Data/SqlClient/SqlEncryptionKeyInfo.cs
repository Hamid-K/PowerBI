using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000102 RID: 258
	internal class SqlEncryptionKeyInfo
	{
		// Token: 0x0400084F RID: 2127
		internal byte[] encryptedKey;

		// Token: 0x04000850 RID: 2128
		internal int databaseId;

		// Token: 0x04000851 RID: 2129
		internal int cekId;

		// Token: 0x04000852 RID: 2130
		internal int cekVersion;

		// Token: 0x04000853 RID: 2131
		internal byte[] cekMdVersion;

		// Token: 0x04000854 RID: 2132
		internal string keyPath;

		// Token: 0x04000855 RID: 2133
		internal string keyStoreName;

		// Token: 0x04000856 RID: 2134
		internal string algorithmName;

		// Token: 0x04000857 RID: 2135
		internal byte normalizationRuleVersion;
	}
}
