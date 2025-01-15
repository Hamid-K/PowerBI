using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200001E RID: 30
	internal class ActiveDirectoryAuthenticationTimeoutRetryHelper
	{
		// Token: 0x06000677 RID: 1655 RVA: 0x0000C54A File Offset: 0x0000A74A
		public ActiveDirectoryAuthenticationTimeoutRetryHelper()
		{
			this._typeName = base.GetType().Name;
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x0000C56E File Offset: 0x0000A76E
		// (set) Token: 0x06000679 RID: 1657 RVA: 0x0000C578 File Offset: 0x0000A778
		public ActiveDirectoryAuthenticationTimeoutRetryState State
		{
			get
			{
				return this._state;
			}
			set
			{
				switch (this._state)
				{
				case ActiveDirectoryAuthenticationTimeoutRetryState.NotStarted:
					if (value != ActiveDirectoryAuthenticationTimeoutRetryState.Retrying && value != ActiveDirectoryAuthenticationTimeoutRetryState.HasLoggedIn)
					{
						throw new InvalidOperationException(string.Format("Cannot transit from {0} to {1}.", this._state, value));
					}
					break;
				case ActiveDirectoryAuthenticationTimeoutRetryState.Retrying:
					if (value != ActiveDirectoryAuthenticationTimeoutRetryState.HasLoggedIn)
					{
						throw new InvalidOperationException(string.Format("Cannot transit from {0} to {1}.", this._state, value));
					}
					break;
				case ActiveDirectoryAuthenticationTimeoutRetryState.HasLoggedIn:
					throw new InvalidOperationException(string.Format("Cannot transit from {0} to {1}.", this._state, value));
				default:
					throw new InvalidOperationException(string.Format("Unsupported state: {0}.", value));
				}
				if (this._sqlAuthLogger.IsLoggingEnabled)
				{
					this._sqlAuthLogger.LogInfo(this._typeName, "SetState", string.Format("State changed from {0} to {1}.", this._state, value));
				}
				this._state = value;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x0000C670 File Offset: 0x0000A870
		// (set) Token: 0x0600067B RID: 1659 RVA: 0x0000C6C0 File Offset: 0x0000A8C0
		public SqlFedAuthToken CachedToken
		{
			get
			{
				if (this._sqlAuthLogger.IsLoggingEnabled)
				{
					this._sqlAuthLogger.LogInfo(this._typeName, "GetCachedToken", "Retrieved cached token " + ActiveDirectoryAuthenticationTimeoutRetryHelper.GetTokenHash(this._token) + ".");
				}
				return this._token;
			}
			set
			{
				if (this._sqlAuthLogger.IsLoggingEnabled)
				{
					this._sqlAuthLogger.LogInfo(this._typeName, "SetCachedToken", string.Concat(new string[]
					{
						"CachedToken changed from ",
						ActiveDirectoryAuthenticationTimeoutRetryHelper.GetTokenHash(this._token),
						" to ",
						ActiveDirectoryAuthenticationTimeoutRetryHelper.GetTokenHash(value),
						"."
					}));
				}
				this._token = value;
			}
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0000C734 File Offset: 0x0000A934
		public bool CanRetryWithSqlException(SqlException sqlex)
		{
			string text = "CheckCanRetry";
			if (this._sqlAuthLogger.LogAssert(this._state == ActiveDirectoryAuthenticationTimeoutRetryState.NotStarted, this._typeName, text, string.Format("Cannot retry due to state == {0}.", this._state)) && this._sqlAuthLogger.LogAssert(this.CachedToken != null, this._typeName, text, "Cannot retry when cached token is null.") && this._sqlAuthLogger.LogAssert(ActiveDirectoryAuthenticationTimeoutRetryHelper.IsConnectTimeoutError(sqlex), this._typeName, text, "Cannot retry when exception is not timeout."))
			{
				this._sqlAuthLogger.LogInfo(this._typeName, text, "All checks passed.");
				return true;
			}
			return false;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0000C7D4 File Offset: 0x0000A9D4
		private static bool IsConnectTimeoutError(SqlException sqlex)
		{
			Win32Exception ex = sqlex.InnerException as Win32Exception;
			return ex != null && (ex.NativeErrorCode == 10054 || ex.NativeErrorCode == 258);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0000C810 File Offset: 0x0000AA10
		private static string GetTokenHash(SqlFedAuthToken token)
		{
			if (token == null)
			{
				return "null";
			}
			string text = SqlAuthenticationToken.AccessTokenStringFromBytes(token.accessToken);
			byte[] bytes = Encoding.UTF8.GetBytes(text);
			string text2;
			using (SHA256 sha = SHA256.Create())
			{
				byte[] array = sha.ComputeHash(bytes);
				text2 = Convert.ToBase64String(array);
			}
			return text2;
		}

		// Token: 0x04000058 RID: 88
		private ActiveDirectoryAuthenticationTimeoutRetryState _state;

		// Token: 0x04000059 RID: 89
		private SqlFedAuthToken _token;

		// Token: 0x0400005A RID: 90
		private readonly string _typeName;

		// Token: 0x0400005B RID: 91
		private readonly SqlClientLogger _sqlAuthLogger = new SqlClientLogger();
	}
}
