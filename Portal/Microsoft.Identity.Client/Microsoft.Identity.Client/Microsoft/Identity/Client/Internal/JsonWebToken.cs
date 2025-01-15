using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Identity.Client.PlatformsCommon.Interfaces;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.Internal
{
	// Token: 0x02000236 RID: 566
	internal class JsonWebToken
	{
		// Token: 0x06001716 RID: 5910 RVA: 0x0004C1B7 File Offset: 0x0004A3B7
		public JsonWebToken(ICryptographyManager cryptographyManager, string clientId, string audience)
		{
			this._cryptographyManager = cryptographyManager;
			this._clientId = clientId;
			this._audience = audience;
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x0004C1D4 File Offset: 0x0004A3D4
		public JsonWebToken(ICryptographyManager cryptographyManager, string clientId, string audience, IDictionary<string, string> claimsToSign, bool appendDefaultClaims = false)
			: this(cryptographyManager, clientId, audience)
		{
			this._claimsToSign = claimsToSign;
			this._appendDefaultClaims = appendDefaultClaims;
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x0004C1F0 File Offset: 0x0004A3F0
		private string CreateJsonPayload()
		{
			long num = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
			long num2 = num + 600L;
			if (this._claimsToSign == null || this._claimsToSign.Count == 0)
			{
				return string.Format("{{\"aud\":\"{0}\",\"iss\":\"{1}\",\"sub\":\"{2}\",\"nbf\":\"{3}\",\"exp\":\"{4}\",\"jti\":\"{5}\"}}", new object[]
				{
					this._audience,
					this._clientId,
					this._clientId,
					num,
					num2,
					Guid.NewGuid()
				});
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (this._appendDefaultClaims)
			{
				string text = string.Format("{{\"aud\":\"{0}\",\"iss\":\"{1}\",\"sub\":\"{2}\",\"nbf\":\"{3}\",\"exp\":\"{4}\",\"jti\":\"{5}\",", new object[]
				{
					this._audience,
					this._clientId,
					this._clientId,
					num,
					num2,
					Guid.NewGuid()
				});
				stringBuilder.Append(text);
			}
			else
			{
				stringBuilder.Append('{');
			}
			int num3 = 0;
			foreach (KeyValuePair<string, string> keyValuePair in this._claimsToSign)
			{
				stringBuilder.Append(string.Concat(new string[] { "\"", keyValuePair.Key, "\":\"", keyValuePair.Value, "\"" }));
				if (num3 != this._claimsToSign.Count - 1)
				{
					num3++;
					stringBuilder.Append(',');
				}
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x0004C390 File Offset: 0x0004A590
		public string Sign(X509Certificate2 certificate, bool sendX5C, bool useSha2AndPss)
		{
			string text = this.CreateJwtHeaderAndBody(certificate, sendX5C, useSha2AndPss);
			if (65536 < text.Length)
			{
				throw new MsalClientException("encoded_token_too_long");
			}
			byte[] array = this._cryptographyManager.SignWithCertificate(text, certificate, useSha2AndPss ? RSASignaturePadding.Pss : RSASignaturePadding.Pkcs1);
			return text + "." + Base64UrlHelpers.Encode(array);
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x0004C3F0 File Offset: 0x0004A5F0
		private static string CreateJsonHeader(X509Certificate2 certificate, bool sendX5C, bool useSha2AndPss)
		{
			string text = JsonWebToken.ComputeCertThumbprint(certificate, useSha2AndPss);
			string text2 = (useSha2AndPss ? "PS256" : "RS256");
			string text3 = (useSha2AndPss ? "x5t#S256" : "x5t");
			string text5;
			if (sendX5C)
			{
				string text4 = Convert.ToBase64String(certificate.GetRawCertData());
				text5 = string.Concat(new string[] { "{\"alg\":\"", text2, "\",\"typ\":\"JWT\",\"", text3, "\":\"", text, "\",\"x5c\":\"", text4, "\"}" });
			}
			else
			{
				text5 = string.Concat(new string[] { "{\"alg\":\"", text2, "\",\"typ\":\"JWT\",\"", text3, "\":\"", text, "\"}" });
			}
			return text5;
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x0004C4B8 File Offset: 0x0004A6B8
		private static string ComputeCertThumbprint(X509Certificate2 certificate, bool useSha2)
		{
			string text = null;
			try
			{
				if (useSha2)
				{
					using (SHA256 sha = SHA256.Create())
					{
						text = Base64UrlHelpers.Encode(sha.ComputeHash(certificate.RawData));
						goto IL_0035;
					}
				}
				text = Base64UrlHelpers.Encode(certificate.GetCertHash());
				IL_0035:;
			}
			catch (CryptographicException ex)
			{
				throw new MsalClientException("cryptographic_error", "A cryptographic exception occurred. Possible cause: the certificate has been disposed. See inner exception for full details.", ex);
			}
			return text;
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x0004C52C File Offset: 0x0004A72C
		private string CreateJwtHeaderAndBody(X509Certificate2 certificate, bool addX5C, bool useSha2AndPss)
		{
			string text = Base64UrlHelpers.EncodeString(JsonWebToken.CreateJsonHeader(certificate, addX5C, useSha2AndPss));
			string text2 = Base64UrlHelpers.EncodeString(this.CreateJsonPayload());
			return text + "." + text2;
		}

		// Token: 0x04000A05 RID: 2565
		private const int MaxTokenLength = 65536;

		// Token: 0x04000A06 RID: 2566
		public const long JwtToAadLifetimeInSeconds = 600L;

		// Token: 0x04000A07 RID: 2567
		private readonly IDictionary<string, string> _claimsToSign;

		// Token: 0x04000A08 RID: 2568
		private readonly ICryptographyManager _cryptographyManager;

		// Token: 0x04000A09 RID: 2569
		private readonly string _clientId;

		// Token: 0x04000A0A RID: 2570
		private readonly string _audience;

		// Token: 0x04000A0B RID: 2571
		private readonly bool _appendDefaultClaims;
	}
}
