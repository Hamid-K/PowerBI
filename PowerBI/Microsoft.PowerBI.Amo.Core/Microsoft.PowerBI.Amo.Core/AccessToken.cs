using System;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000018 RID: 24
	public struct AccessToken
	{
		// Token: 0x0600008F RID: 143 RVA: 0x00005614 File Offset: 0x00003814
		public AccessToken(string token, DateTimeOffset expirationTime, object userContext = null)
		{
			this.token = token;
			this.expirationTime = expirationTime;
			this.userContext = userContext;
			AccessToken.ValidateTokenInput(this);
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00005636 File Offset: 0x00003836
		public string Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000563E File Offset: 0x0000383E
		public DateTimeOffset ExpirationTime
		{
			get
			{
				return this.expirationTime;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00005646 File Offset: 0x00003846
		public object UserContext
		{
			get
			{
				return this.userContext;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000564E File Offset: 0x0000384E
		public bool IsExpired
		{
			get
			{
				return DateTimeOffset.Now >= this.expirationTime;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00005660 File Offset: 0x00003860
		internal bool IsValid
		{
			get
			{
				return !string.IsNullOrEmpty(this.Token);
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00005670 File Offset: 0x00003870
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

		// Token: 0x06000096 RID: 150 RVA: 0x0000569A File Offset: 0x0000389A
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

		// Token: 0x04000076 RID: 118
		private readonly string token;

		// Token: 0x04000077 RID: 119
		private readonly DateTimeOffset expirationTime;

		// Token: 0x04000078 RID: 120
		private readonly object userContext;
	}
}
