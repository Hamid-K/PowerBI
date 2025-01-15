using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Identity.Client.AppConfig;
using Microsoft.Identity.Client.Cache.Items;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.Utils;
using Microsoft.Identity.Json.Linq;

namespace Microsoft.Identity.Client.AuthScheme.PoP
{
	// Token: 0x020002C6 RID: 710
	internal class PopAuthenticationScheme : IAuthenticationScheme
	{
		// Token: 0x06001A96 RID: 6806 RVA: 0x00056838 File Offset: 0x00054A38
		public PopAuthenticationScheme(PoPAuthenticationConfiguration popAuthenticationConfiguration, IServiceBundle serviceBundle)
		{
			if (serviceBundle == null)
			{
				throw new ArgumentNullException("serviceBundle");
			}
			if (popAuthenticationConfiguration == null)
			{
				throw new ArgumentNullException("popAuthenticationConfiguration");
			}
			this._popAuthenticationConfiguration = popAuthenticationConfiguration;
			this._popCryptoProvider = this._popAuthenticationConfiguration.PopCryptoProvider ?? serviceBundle.PlatformProxy.GetDefaultPoPCryptoProvider();
			byte[] array = PopAuthenticationScheme.ComputeThumbprint(this._popCryptoProvider.CannonicalPublicKeyJwk);
			this.KeyId = Base64UrlHelpers.Encode(array);
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001A97 RID: 6807 RVA: 0x000568AC File Offset: 0x00054AAC
		public TokenType TelemetryTokenType
		{
			get
			{
				return TokenType.Pop;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001A98 RID: 6808 RVA: 0x000568AF File Offset: 0x00054AAF
		public string AuthorizationHeaderPrefix
		{
			get
			{
				return "PoP";
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001A99 RID: 6809 RVA: 0x000568B6 File Offset: 0x00054AB6
		public string AccessTokenType
		{
			get
			{
				return "pop";
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001A9A RID: 6810 RVA: 0x000568BD File Offset: 0x00054ABD
		public string KeyId { get; }

		// Token: 0x06001A9B RID: 6811 RVA: 0x000568C5 File Offset: 0x00054AC5
		public IReadOnlyDictionary<string, string> GetTokenRequestParams()
		{
			return new Dictionary<string, string>
			{
				{ "token_type", "pop" },
				{
					"req_cnf",
					this.ComputeReqCnf()
				}
			};
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x000568F0 File Offset: 0x00054AF0
		public string FormatAccessToken(MsalAccessTokenCacheItem msalAccessTokenCacheItem)
		{
			if (!this._popAuthenticationConfiguration.SignHttpRequest)
			{
				return msalAccessTokenCacheItem.Secret;
			}
			JObject jobject = new JObject();
			jobject["alg"] = this._popCryptoProvider.CryptographicAlgorithm;
			jobject["kid"] = this.KeyId;
			jobject["typ"] = "pop";
			JObject jobject2 = this.CreateBody(msalAccessTokenCacheItem);
			return this.CreateJWS(JsonHelper.JsonObjectToString(jobject2), JsonHelper.JsonObjectToString(jobject));
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x00056978 File Offset: 0x00054B78
		private JObject CreateBody(MsalAccessTokenCacheItem msalAccessTokenCacheItem)
		{
			JToken jtoken = JToken.Parse(this._popCryptoProvider.CannonicalPublicKeyJwk);
			JObject jobject = new JObject();
			string text = "cnf";
			JObject jobject2 = new JObject();
			jobject2["jwk"] = jtoken;
			jobject[text] = jobject2;
			jobject["ts"] = DateTimeHelpers.CurrDateTimeInUnixTimestamp();
			jobject["at"] = msalAccessTokenCacheItem.Secret;
			jobject["nonce"] = this._popAuthenticationConfiguration.Nonce ?? PopAuthenticationScheme.CreateSimpleNonce();
			JObject jobject3 = jobject;
			if (this._popAuthenticationConfiguration.HttpMethod != null)
			{
				JObject jobject4 = jobject3;
				string text2 = "m";
				HttpMethod httpMethod = this._popAuthenticationConfiguration.HttpMethod;
				jobject4[text2] = ((httpMethod != null) ? httpMethod.ToString() : null);
			}
			if (!string.IsNullOrEmpty(this._popAuthenticationConfiguration.HttpHost))
			{
				jobject3["u"] = this._popAuthenticationConfiguration.HttpHost;
			}
			if (!string.IsNullOrEmpty(this._popAuthenticationConfiguration.HttpPath))
			{
				jobject3["p"] = this._popAuthenticationConfiguration.HttpPath;
			}
			return jobject3;
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x00056A9C File Offset: 0x00054C9C
		private static string CreateSimpleNonce()
		{
			return Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture);
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x00056AC0 File Offset: 0x00054CC0
		private string ComputeReqCnf()
		{
			return Base64UrlHelpers.Encode("{\"kid\":\"" + this.KeyId + "\"}");
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x00056ADC File Offset: 0x00054CDC
		private static byte[] ComputeThumbprint(string canonicalJwk)
		{
			byte[] array;
			using (SHA256 sha = SHA256.Create())
			{
				array = sha.ComputeHash(Encoding.UTF8.GetBytes(canonicalJwk));
			}
			return array;
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x00056B20 File Offset: 0x00054D20
		private string CreateJWS(string payload, string header)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(Base64UrlHelpers.Encode(Encoding.UTF8.GetBytes(header)));
			stringBuilder.Append('.');
			stringBuilder.Append(Base64UrlHelpers.Encode(payload));
			string text = stringBuilder.ToString();
			stringBuilder.Append('.');
			stringBuilder.Append(Base64UrlHelpers.Encode(this._popCryptoProvider.Sign(Encoding.UTF8.GetBytes(text))));
			return stringBuilder.ToString();
		}

		// Token: 0x04000C1A RID: 3098
		private readonly PoPAuthenticationConfiguration _popAuthenticationConfiguration;

		// Token: 0x04000C1B RID: 3099
		private readonly IPoPCryptoProvider _popCryptoProvider;
	}
}
