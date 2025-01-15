using System;
using System.Security;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000106 RID: 262
	internal sealed class SqlLogin
	{
		// Token: 0x04000862 RID: 2146
		internal SqlAuthenticationMethod authentication;

		// Token: 0x04000863 RID: 2147
		internal int timeout;

		// Token: 0x04000864 RID: 2148
		internal bool userInstance;

		// Token: 0x04000865 RID: 2149
		internal string hostName = "";

		// Token: 0x04000866 RID: 2150
		internal string userName = "";

		// Token: 0x04000867 RID: 2151
		internal string password = "";

		// Token: 0x04000868 RID: 2152
		internal string applicationName = "";

		// Token: 0x04000869 RID: 2153
		internal string serverName = "";

		// Token: 0x0400086A RID: 2154
		internal string language = "";

		// Token: 0x0400086B RID: 2155
		internal string database = "";

		// Token: 0x0400086C RID: 2156
		internal string attachDBFilename = "";

		// Token: 0x0400086D RID: 2157
		internal string newPassword = "";

		// Token: 0x0400086E RID: 2158
		internal bool useReplication;

		// Token: 0x0400086F RID: 2159
		internal bool useSSPI;

		// Token: 0x04000870 RID: 2160
		internal int packetSize = 8000;

		// Token: 0x04000871 RID: 2161
		internal bool readOnlyIntent;

		// Token: 0x04000872 RID: 2162
		internal SqlCredential credential;

		// Token: 0x04000873 RID: 2163
		internal SecureString newSecurePassword;
	}
}
