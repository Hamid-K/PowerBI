using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002D7 RID: 727
	internal class TokenValidator
	{
		// Token: 0x06001AED RID: 6893 RVA: 0x000519AC File Offset: 0x0004FBAC
		public TokenValidator(string trustedIssuer, string trustedAudienceValue, params string[] signingKeys)
		{
			this.trustedSigningKeys = new List<byte[]>(signingKeys.Length);
			for (int i = 0; i < signingKeys.Length; i++)
			{
				if (!string.IsNullOrEmpty(signingKeys[i]))
				{
					this.trustedSigningKeys.Add(Convert.FromBase64String(signingKeys[i]));
				}
			}
			this.trustedTokenIssuer = trustedIssuer;
			this.trustedAudienceValue = trustedAudienceValue;
			if (!Uri.TryCreate(trustedIssuer, UriKind.Absolute, out this.trustedTokenIssuerUri))
			{
				this.stripSchemeFromTokenIssuer = true;
			}
			if (!Uri.TryCreate(trustedAudienceValue, UriKind.Absolute, out this.trustedAudienceValueUri))
			{
				this.stripSchemeFromAudienceValue = true;
			}
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x00051A32 File Offset: 0x0004FC32
		public bool Validate(Token token)
		{
			return TokenValidator.IsHMACValid(token, this.trustedSigningKeys) && !TokenValidator.IsExpired(token) && this.IsIssuerTrusted(token) && this.IsAudienceTrusted(token);
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x00051A65 File Offset: 0x0004FC65
		public bool ValidateEx(Token token, out ErrStatus err)
		{
			if (!this.IsIssuerTrusted(token) || !this.IsAudienceTrusted(token))
			{
				err = ErrStatus.AUTH_HEADER_INVALID;
				return false;
			}
			if (!TokenValidator.IsHMACValid(token, this.trustedSigningKeys) || TokenValidator.IsExpired(token))
			{
				err = ErrStatus.AUTH_HEADER_EXPIRED;
				return false;
			}
			err = ErrStatus.UNINITIALIZED_ERROR;
			return true;
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x00051AA0 File Offset: 0x0004FCA0
		private bool IsAudienceTrusted(Token token)
		{
			string audience = token.Audience;
			if (!string.IsNullOrEmpty(audience))
			{
				Uri uri = new Uri(audience);
				if (this.stripSchemeFromAudienceValue)
				{
					if (uri.DnsSafeHost.Equals(this.trustedAudienceValue, StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
				}
				else if (uri.Equals(this.trustedAudienceValueUri))
				{
					return true;
				}
			}
			if (Provider.IsEnabled(TraceLevel.Error))
			{
				EventLogWriter.WriteError("DistributedCache.AcsTokenValidator", "Token failed Audience Check. trustedAudienceValue = {0}, audienceValueinToken = {1}", new object[] { this.trustedAudienceValue, audience });
			}
			return false;
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x00051B20 File Offset: 0x0004FD20
		private bool IsIssuerTrusted(Token token)
		{
			string issuer = token.Issuer;
			if (!string.IsNullOrEmpty(issuer))
			{
				Uri uri = new Uri(issuer);
				if (this.stripSchemeFromTokenIssuer)
				{
					if (uri.DnsSafeHost.Equals(this.trustedTokenIssuer, StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
				}
				else if (uri.Equals(this.trustedTokenIssuerUri))
				{
					return true;
				}
			}
			if (Provider.IsEnabled(TraceLevel.Error))
			{
				EventLogWriter.WriteError("DistributedCache.AcsTokenValidator", "Token failed Issuer validation. TrustedIssuer = {0} IssuerInToken = {1}", new object[] { this.trustedTokenIssuer, issuer });
			}
			return false;
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x00051BA0 File Offset: 0x0004FDA0
		private static bool IsHMACValid(Token token, IList<byte[]> sha256HMACKeys)
		{
			string[] array = token.TokenStr.Split(new string[] { "&HMACSHA256=" }, StringSplitOptions.None);
			if (array == null || array.Length != 2)
			{
				return false;
			}
			for (int i = 0; i < sha256HMACKeys.Count; i++)
			{
				using (HMACSHA256 hmacsha = new HMACSHA256(sha256HMACKeys[i]))
				{
					byte[] array2 = hmacsha.ComputeHash(Encoding.ASCII.GetBytes(array[0]));
					string text = HttpUtility.UrlEncode(Convert.ToBase64String(array2));
					if (text == array[1])
					{
						return true;
					}
				}
			}
			if (Provider.IsEnabled(TraceLevel.Error))
			{
				EventLogWriter.WriteError("DistributedCache.AcsTokenValidator", "Token failed signature validation. Token = {0}, checked {1} keys.", new object[]
				{
					array[0],
					sha256HMACKeys.Count
				});
			}
			return false;
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x00051C80 File Offset: 0x0004FE80
		internal static bool IsExpired(Token token)
		{
			return TokenValidator.IsExpired(token, DateTime.UtcNow);
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x00051C8D File Offset: 0x0004FE8D
		internal static bool IsExpired(Token token, DateTime timeToCompare)
		{
			return timeToCompare > token.Expiry;
		}

		// Token: 0x04000E49 RID: 3657
		private const string logSource = "DistributedCache.AcsTokenValidator";

		// Token: 0x04000E4A RID: 3658
		private IList<byte[]> trustedSigningKeys;

		// Token: 0x04000E4B RID: 3659
		private string trustedTokenIssuer;

		// Token: 0x04000E4C RID: 3660
		private string trustedAudienceValue;

		// Token: 0x04000E4D RID: 3661
		private Uri trustedTokenIssuerUri;

		// Token: 0x04000E4E RID: 3662
		private Uri trustedAudienceValueUri;

		// Token: 0x04000E4F RID: 3663
		private bool stripSchemeFromTokenIssuer;

		// Token: 0x04000E50 RID: 3664
		private bool stripSchemeFromAudienceValue;
	}
}
