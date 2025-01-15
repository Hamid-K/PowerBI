using System;
using System.Collections.Generic;

namespace Azure.Identity
{
	// Token: 0x02000061 RID: 97
	public static class IdentityModelFactory
	{
		// Token: 0x0600037A RID: 890 RVA: 0x0000AA2A File Offset: 0x00008C2A
		public static AuthenticationRecord AuthenticationRecord(string username, string authority, string homeAccountId, string tenantId, string clientId)
		{
			return new AuthenticationRecord(username, authority, homeAccountId, tenantId, clientId);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000AA37 File Offset: 0x00008C37
		public static DeviceCodeInfo DeviceCodeInfo(string userCode, string deviceCode, Uri verificationUri, DateTimeOffset expiresOn, string message, string clientId, IReadOnlyCollection<string> scopes)
		{
			return new DeviceCodeInfo(userCode, deviceCode, verificationUri, expiresOn, message, clientId, scopes);
		}
	}
}
