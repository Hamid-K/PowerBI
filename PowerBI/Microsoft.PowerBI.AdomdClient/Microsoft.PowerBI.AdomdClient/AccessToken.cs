using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000004 RID: 4
	public struct AccessToken
	{
		// Token: 0x06000005 RID: 5 RVA: 0x0000215C File Offset: 0x0000035C
		public AccessToken(string token, DateTimeOffset expirationTime, object userContext = null)
		{
			this.token = token;
			this.expirationTime = expirationTime;
			this.userContext = userContext;
			AccessToken.ValidateTokenInput(this);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000217E File Offset: 0x0000037E
		public string Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002186 File Offset: 0x00000386
		public DateTimeOffset ExpirationTime
		{
			get
			{
				return this.expirationTime;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000218E File Offset: 0x0000038E
		public object UserContext
		{
			get
			{
				return this.userContext;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002196 File Offset: 0x00000396
		public bool IsExpired
		{
			get
			{
				return DateTimeOffset.Now >= this.expirationTime;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021A8 File Offset: 0x000003A8
		internal bool IsValid
		{
			get
			{
				return !string.IsNullOrEmpty(this.Token);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021B8 File Offset: 0x000003B8
		internal static bool TryRefreshToken(AccessToken oldToken, Func<AccessToken, AccessToken> onAccessTokenExpired, out AccessToken newToken)
		{
			if (onAccessTokenExpired == null)
			{
				newToken = default(AccessToken);
				return !oldToken.IsExpired;
			}
			newToken = onAccessTokenExpired(oldToken);
			return newToken.IsValid;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021E2 File Offset: 0x000003E2
		internal static void ValidateTokenInput(AccessToken token)
		{
			if (!token.IsValid)
			{
				throw new ArgumentException(RuntimeSR.AccessToken_Invalid);
			}
			if (token.ExpirationTime < DateTimeOffset.Now)
			{
				throw new ArgumentException(RuntimeSR.Token_Expired);
			}
		}

		// Token: 0x0400002A RID: 42
		private readonly string token;

		// Token: 0x0400002B RID: 43
		private readonly DateTimeOffset expirationTime;

		// Token: 0x0400002C RID: 44
		private readonly object userContext;
	}
}
