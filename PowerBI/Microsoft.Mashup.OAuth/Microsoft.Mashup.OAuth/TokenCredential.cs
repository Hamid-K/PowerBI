using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x02000028 RID: 40
	public sealed class TokenCredential
	{
		// Token: 0x06000128 RID: 296 RVA: 0x00005F90 File Offset: 0x00004190
		public TokenCredential(string accessToken, string expiresAt, string refreshToken)
			: this(accessToken, expiresAt, refreshToken, null)
		{
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005F9C File Offset: 0x0000419C
		public TokenCredential(string accessToken, string expiresAt, string refreshToken, Dictionary<string, string> properties)
		{
			if (string.IsNullOrEmpty(accessToken))
			{
				throw new ArgumentNullException("accessToken");
			}
			this.accessToken = accessToken;
			this.expiresAt = expiresAt;
			this.refreshToken = refreshToken;
			this.properties = properties ?? new Dictionary<string, string>();
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005FE8 File Offset: 0x000041E8
		public static string GetExpiresAtFromExpiresIn(string expiresIn)
		{
			return Utilities.GetExpiresAtString(expiresIn);
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00005FF0 File Offset: 0x000041F0
		public string AccessToken
		{
			get
			{
				return this.accessToken;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00005FF8 File Offset: 0x000041F8
		public string Expires
		{
			get
			{
				return this.expiresAt;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00006000 File Offset: 0x00004200
		public string RefreshToken
		{
			get
			{
				return this.refreshToken;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00006008 File Offset: 0x00004208
		public Dictionary<string, string> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006010 File Offset: 0x00004210
		public static string EncodeAccessTokenKey(string resource)
		{
			return "AccessToken:" + resource.ToLowerInvariant().TrimEnd(new char[] { '/' });
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006032 File Offset: 0x00004232
		public bool TryGetExpiresIn(out TimeSpan expiresIn)
		{
			return Utilities.TryGetExpiresIn(this.Expires, out expiresIn);
		}

		// Token: 0x040000FC RID: 252
		private readonly string accessToken;

		// Token: 0x040000FD RID: 253
		private readonly string expiresAt;

		// Token: 0x040000FE RID: 254
		private readonly string refreshToken;

		// Token: 0x040000FF RID: 255
		private readonly Dictionary<string, string> properties;
	}
}
