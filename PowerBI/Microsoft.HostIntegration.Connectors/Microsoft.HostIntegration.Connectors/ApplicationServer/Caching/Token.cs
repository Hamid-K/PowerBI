using System;
using System.Collections.Specialized;
using System.Web;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002D5 RID: 725
	internal sealed class Token
	{
		// Token: 0x06001AE1 RID: 6881 RVA: 0x00051824 File Offset: 0x0004FA24
		public Token(string tokenStr)
		{
			if (string.IsNullOrEmpty(tokenStr))
			{
				throw new DataCacheException("DistributedCache.ACS", 19002, "Empty/Null token");
			}
			this._nameValues = HttpUtility.ParseQueryString(tokenStr);
			this._tokenStr = tokenStr;
			string expirationOn = this.ExpirationOn;
			ulong num;
			if (!string.IsNullOrEmpty(expirationOn) && ulong.TryParse(expirationOn, out num))
			{
				this._tokenExpiry = ConfigManager.UnixEpoch.AddSeconds(num);
				this._tokenRenewal = Token.GetTokenRenewalTime(DateTime.UtcNow, this._tokenExpiry);
				return;
			}
			this._tokenExpiry = DateTime.MinValue;
			this._tokenRenewal = DateTime.MinValue;
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x000518C4 File Offset: 0x0004FAC4
		public Token(string tokenStr, double validityInSeconds)
			: this(tokenStr)
		{
			DateTime dateTime = DateTime.UtcNow.AddSeconds(validityInSeconds);
			if (this._tokenExpiry > dateTime)
			{
				this._tokenExpiry = dateTime;
				this._tokenRenewal = Token.GetTokenRenewalTime(DateTime.UtcNow, dateTime);
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001AE3 RID: 6883 RVA: 0x0005190D File Offset: 0x0004FB0D
		public DateTime Expiry
		{
			get
			{
				return this._tokenExpiry;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001AE4 RID: 6884 RVA: 0x00051915 File Offset: 0x0004FB15
		public string ExpirationOn
		{
			get
			{
				return this._nameValues["ExpiresOn"];
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x00051927 File Offset: 0x0004FB27
		public string Issuer
		{
			get
			{
				return this._nameValues["Issuer"];
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x00051939 File Offset: 0x0004FB39
		public string Audience
		{
			get
			{
				return this._nameValues["Audience"];
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001AE7 RID: 6887 RVA: 0x0005194B File Offset: 0x0004FB4B
		public string TokenStr
		{
			get
			{
				return this._tokenStr;
			}
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x00051953 File Offset: 0x0004FB53
		public bool TryGetClaim(string claimName, out string claimValue)
		{
			claimValue = this._nameValues[claimName];
			return !string.IsNullOrEmpty(claimValue);
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x0005196D File Offset: 0x0004FB6D
		public bool IsDueForRenewal(DateTime now)
		{
			return now >= this._tokenRenewal;
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x0005197B File Offset: 0x0004FB7B
		public bool IsExpired(DateTime now)
		{
			return now >= this._tokenExpiry;
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x00051989 File Offset: 0x0004FB89
		private static DateTime GetTokenRenewalTime(DateTime currentTime, DateTime expiryTime)
		{
			return new DateTime((currentTime.Ticks + expiryTime.Ticks) / 2L, currentTime.Kind);
		}

		// Token: 0x04000E3F RID: 3647
		private readonly NameValueCollection _nameValues;

		// Token: 0x04000E40 RID: 3648
		private readonly string _tokenStr;

		// Token: 0x04000E41 RID: 3649
		private readonly DateTime _tokenExpiry;

		// Token: 0x04000E42 RID: 3650
		private readonly DateTime _tokenRenewal;
	}
}
