using System;
using System.Text;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000050 RID: 80
	public class SqlAuthenticationToken
	{
		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x00012AAC File Offset: 0x00010CAC
		public DateTimeOffset ExpiresOn { get; }

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x00012AB4 File Offset: 0x00010CB4
		public string AccessToken { get; }

		// Token: 0x06000813 RID: 2067 RVA: 0x00012ABC File Offset: 0x00010CBC
		public SqlAuthenticationToken(string accessToken, DateTimeOffset expiresOn)
		{
			if (string.IsNullOrEmpty(accessToken))
			{
				throw SQL.ParameterCannotBeEmpty("AccessToken");
			}
			this.AccessToken = accessToken;
			this.ExpiresOn = expiresOn;
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00012AE5 File Offset: 0x00010CE5
		internal SqlAuthenticationToken(byte[] accessToken, DateTimeOffset expiresOn)
			: this(SqlAuthenticationToken.AccessTokenStringFromBytes(accessToken), expiresOn)
		{
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00012AF4 File Offset: 0x00010CF4
		internal SqlFedAuthToken ToSqlFedAuthToken()
		{
			byte[] array = SqlAuthenticationToken.AccessTokenBytesFromString(this.AccessToken);
			return new SqlFedAuthToken
			{
				accessToken = array,
				dataLen = (uint)array.Length,
				expirationFileTime = this.ExpiresOn.ToFileTime()
			};
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00012B36 File Offset: 0x00010D36
		internal static string AccessTokenStringFromBytes(byte[] bytes)
		{
			return Encoding.Unicode.GetString(bytes);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00012B43 File Offset: 0x00010D43
		internal static byte[] AccessTokenBytesFromString(string token)
		{
			return Encoding.Unicode.GetBytes(token);
		}
	}
}
