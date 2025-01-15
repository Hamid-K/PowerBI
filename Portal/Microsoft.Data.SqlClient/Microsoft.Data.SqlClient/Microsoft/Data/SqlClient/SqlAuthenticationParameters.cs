using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200004D RID: 77
	public class SqlAuthenticationParameters
	{
		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x00010FA6 File Offset: 0x0000F1A6
		public SqlAuthenticationMethod AuthenticationMethod { get; }

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x00010FAE File Offset: 0x0000F1AE
		public string Resource { get; }

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x00010FB6 File Offset: 0x0000F1B6
		public string Authority { get; }

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x00010FBE File Offset: 0x0000F1BE
		public string UserId { get; }

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00010FC6 File Offset: 0x0000F1C6
		public string Password { get; }

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x00010FCE File Offset: 0x0000F1CE
		public Guid ConnectionId { get; }

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00010FD6 File Offset: 0x0000F1D6
		public string ServerName { get; }

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x00010FDE File Offset: 0x0000F1DE
		public string DatabaseName { get; }

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x00010FE6 File Offset: 0x0000F1E6
		public int ConnectionTimeout { get; } = 15;

		// Token: 0x060007B7 RID: 1975 RVA: 0x00010FF0 File Offset: 0x0000F1F0
		protected SqlAuthenticationParameters(SqlAuthenticationMethod authenticationMethod, string serverName, string databaseName, string resource, string authority, string userId, string password, Guid connectionId, int connectionTimeout)
		{
			this.AuthenticationMethod = authenticationMethod;
			this.ServerName = serverName;
			this.DatabaseName = databaseName;
			this.Resource = resource;
			this.Authority = authority;
			this.UserId = userId;
			this.Password = password;
			this.ConnectionId = connectionId;
			this.ConnectionTimeout = connectionTimeout;
		}

		// Token: 0x020001B1 RID: 433
		internal class Builder
		{
			// Token: 0x06001D8D RID: 7565 RVA: 0x0007A0B4 File Offset: 0x000782B4
			public static implicit operator SqlAuthenticationParameters(SqlAuthenticationParameters.Builder builder)
			{
				return new SqlAuthenticationParameters(builder._authenticationMethod, builder._serverName, builder._databaseName, builder._resource, builder._authority, builder._userId, builder._password, builder._connectionId, builder._connectionTimeout);
			}

			// Token: 0x06001D8E RID: 7566 RVA: 0x0007A0FC File Offset: 0x000782FC
			public SqlAuthenticationParameters.Builder WithUserId(string userId)
			{
				this._userId = userId;
				return this;
			}

			// Token: 0x06001D8F RID: 7567 RVA: 0x0007A106 File Offset: 0x00078306
			public SqlAuthenticationParameters.Builder WithPassword(string password)
			{
				this._password = password;
				return this;
			}

			// Token: 0x06001D90 RID: 7568 RVA: 0x0007A110 File Offset: 0x00078310
			public SqlAuthenticationParameters.Builder WithPassword(SecureString password)
			{
				IntPtr intPtr = IntPtr.Zero;
				try
				{
					intPtr = Marshal.SecureStringToGlobalAllocUnicode(password);
					this._password = Marshal.PtrToStringUni(intPtr);
				}
				finally
				{
					Marshal.ZeroFreeGlobalAllocUnicode(intPtr);
				}
				return this;
			}

			// Token: 0x06001D91 RID: 7569 RVA: 0x0007A150 File Offset: 0x00078350
			public SqlAuthenticationParameters.Builder WithConnectionId(Guid connectionId)
			{
				this._connectionId = connectionId;
				return this;
			}

			// Token: 0x06001D92 RID: 7570 RVA: 0x0007A15A File Offset: 0x0007835A
			public SqlAuthenticationParameters.Builder WithConnectionTimeout(int timeout)
			{
				this._connectionTimeout = timeout;
				return this;
			}

			// Token: 0x06001D93 RID: 7571 RVA: 0x0007A164 File Offset: 0x00078364
			internal Builder(SqlAuthenticationMethod authenticationMethod, string resource, string authority, string serverName, string databaseName)
			{
				this._authenticationMethod = authenticationMethod;
				this._serverName = serverName;
				this._databaseName = databaseName;
				this._resource = resource;
				this._authority = authority;
			}

			// Token: 0x040012D2 RID: 4818
			private readonly SqlAuthenticationMethod _authenticationMethod;

			// Token: 0x040012D3 RID: 4819
			private readonly string _serverName;

			// Token: 0x040012D4 RID: 4820
			private readonly string _databaseName;

			// Token: 0x040012D5 RID: 4821
			private readonly string _resource;

			// Token: 0x040012D6 RID: 4822
			private readonly string _authority;

			// Token: 0x040012D7 RID: 4823
			private string _userId;

			// Token: 0x040012D8 RID: 4824
			private string _password;

			// Token: 0x040012D9 RID: 4825
			private Guid _connectionId = Guid.NewGuid();

			// Token: 0x040012DA RID: 4826
			private int _connectionTimeout = 15;
		}
	}
}
