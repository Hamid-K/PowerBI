using System;
using System.Globalization;
using System.Security;
using System.Security.Principal;

namespace Microsoft.PowerBI.ReportServer.ExploreHost
{
	// Token: 0x0200000E RID: 14
	public class RSDataSourceConnection
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002B31 File Offset: 0x00000D31
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002B39 File Offset: 0x00000D39
		public string ClientSessionId { get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002B42 File Offset: 0x00000D42
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002B4A File Offset: 0x00000D4A
		public string ServerName { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002B53 File Offset: 0x00000D53
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002B5B File Offset: 0x00000D5B
		public string CubeName { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002B64 File Offset: 0x00000D64
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002B6C File Offset: 0x00000D6C
		public string DataBaseName { get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002B75 File Offset: 0x00000D75
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002B7D File Offset: 0x00000D7D
		public int PortNumber { get; private set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002B86 File Offset: 0x00000D86
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002B8E File Offset: 0x00000D8E
		public string EffectiveUserName { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002B97 File Offset: 0x00000D97
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002B9F File Offset: 0x00000D9F
		public IIdentity IdentityToImpersonate { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002BA8 File Offset: 0x00000DA8
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002BB0 File Offset: 0x00000DB0
		public string CustomData { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002BB9 File Offset: 0x00000DB9
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002BC1 File Offset: 0x00000DC1
		public string Roles { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002BCA File Offset: 0x00000DCA
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002BD2 File Offset: 0x00000DD2
		public string ActAsUser { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002BDB File Offset: 0x00000DDB
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002BE3 File Offset: 0x00000DE3
		public RSDataSourceConnection.ConnectionCredential Credential { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002BEC File Offset: 0x00000DEC
		public RSConnectionSecurity ConnectionSecurity
		{
			get
			{
				return this.Credential.CredentialSecurity;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002BFC File Offset: 0x00000DFC
		public RSDataSourceConnection(string clientSessionId, string serverName, string databaseName, string cubeName, int portNumber, string effectiveUserName, IIdentity identityToImpersonate, string customData, RSDataSourceConnection.ConnectionCredential credential, string roles = null, string actAsUser = null)
		{
			this.ClientSessionId = clientSessionId;
			this.ServerName = serverName;
			this.DataBaseName = databaseName;
			this.CubeName = cubeName;
			this.PortNumber = portNumber;
			this.EffectiveUserName = effectiveUserName;
			this.IdentityToImpersonate = identityToImpersonate;
			this.CustomData = customData;
			this.Credential = credential;
			this.Roles = roles;
			this.ActAsUser = actAsUser;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002C64 File Offset: 0x00000E64
		public string GetConnectionKey()
		{
			string text = "\0";
			string[] array = new string[11];
			array[0] = this.ClientSessionId;
			array[1] = this.ServerName;
			array[2] = this.DataBaseName;
			array[3] = this.CubeName;
			array[4] = this.PortNumber.ToString(CultureInfo.InvariantCulture);
			array[5] = this.EffectiveUserName;
			int num = 6;
			IIdentity identityToImpersonate = this.IdentityToImpersonate;
			array[num] = ((identityToImpersonate != null) ? identityToImpersonate.Name : null);
			array[7] = this.CustomData ?? string.Empty;
			array[8] = this.Roles ?? string.Empty;
			array[9] = this.Credential.GetCredentialKey();
			array[10] = this.ActAsUser ?? string.Empty;
			return string.Join(text, array);
		}

		// Token: 0x0400003F RID: 63
		public const string ConnectionKeySeparator = "\0";

		// Token: 0x02000026 RID: 38
		public class ConnectionCredential
		{
			// Token: 0x1700003F RID: 63
			// (get) Token: 0x06000101 RID: 257 RVA: 0x00004176 File Offset: 0x00002376
			// (set) Token: 0x06000102 RID: 258 RVA: 0x0000417E File Offset: 0x0000237E
			public string UserName { get; private set; }

			// Token: 0x17000040 RID: 64
			// (get) Token: 0x06000103 RID: 259 RVA: 0x00004187 File Offset: 0x00002387
			// (set) Token: 0x06000104 RID: 260 RVA: 0x0000418F File Offset: 0x0000238F
			public SecureString Password { get; private set; }

			// Token: 0x17000041 RID: 65
			// (get) Token: 0x06000105 RID: 261 RVA: 0x00004198 File Offset: 0x00002398
			// (set) Token: 0x06000106 RID: 262 RVA: 0x000041A0 File Offset: 0x000023A0
			public bool UseAsWindowsCredentials { get; private set; }

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x06000107 RID: 263 RVA: 0x000041A9 File Offset: 0x000023A9
			// (set) Token: 0x06000108 RID: 264 RVA: 0x000041B1 File Offset: 0x000023B1
			public RSConnectionSecurity CredentialSecurity { get; private set; }

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x06000109 RID: 265 RVA: 0x000041BA File Offset: 0x000023BA
			public bool IsIntegratedCredential
			{
				get
				{
					return this.CredentialSecurity == RSConnectionSecurity.Integrated;
				}
			}

			// Token: 0x0600010A RID: 266 RVA: 0x000041C5 File Offset: 0x000023C5
			public ConnectionCredential(string userName, SecureString password, bool useAsWindowsCredentials, RSConnectionSecurity credentialSecurity)
			{
				this.UserName = userName;
				this.Password = password;
				this.UseAsWindowsCredentials = useAsWindowsCredentials;
				this.CredentialSecurity = credentialSecurity;
			}

			// Token: 0x0600010B RID: 267 RVA: 0x000041EC File Offset: 0x000023EC
			public string GetCredentialKey()
			{
				return string.Join("\0", new object[]
				{
					this.UserName ?? string.Empty,
					this.UseAsWindowsCredentials,
					this.CredentialSecurity.ToString()
				});
			}
		}
	}
}
