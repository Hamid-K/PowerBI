using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200015C RID: 348
	public class JsonWebKeyConverter
	{
		// Token: 0x0600104B RID: 4171 RVA: 0x0003F168 File Offset: 0x0003D368
		public static JsonWebKey ConvertFromSecurityKey(SecurityKey key)
		{
			if (key == null)
			{
				throw LogHelper.LogArgumentNullException("key");
			}
			RsaSecurityKey rsaSecurityKey = key as RsaSecurityKey;
			if (rsaSecurityKey != null)
			{
				return JsonWebKeyConverter.ConvertFromRSASecurityKey(rsaSecurityKey);
			}
			SymmetricSecurityKey symmetricSecurityKey = key as SymmetricSecurityKey;
			if (symmetricSecurityKey != null)
			{
				return JsonWebKeyConverter.ConvertFromSymmetricSecurityKey(symmetricSecurityKey);
			}
			X509SecurityKey x509SecurityKey = key as X509SecurityKey;
			if (x509SecurityKey != null)
			{
				return JsonWebKeyConverter.ConvertFromX509SecurityKey(x509SecurityKey);
			}
			throw LogHelper.LogExceptionMessage(new NotSupportedException(LogHelper.FormatInvariant("IDX10674: JsonWebKeyConverter does not support SecurityKey of type: {0}", new object[] { LogHelper.MarkAsNonPII(key.GetType().FullName) })));
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0003F1E4 File Offset: 0x0003D3E4
		public static JsonWebKey ConvertFromRSASecurityKey(RsaSecurityKey key)
		{
			if (key == null)
			{
				throw LogHelper.LogArgumentNullException("key");
			}
			RSAParameters rsaparameters;
			if (key.Rsa != null)
			{
				try
				{
					rsaparameters = key.Rsa.ExportParameters(true);
					goto IL_003C;
				}
				catch
				{
					rsaparameters = key.Rsa.ExportParameters(false);
					goto IL_003C;
				}
			}
			rsaparameters = key.Parameters;
			IL_003C:
			return new JsonWebKey
			{
				N = ((rsaparameters.Modulus != null) ? Base64UrlEncoder.Encode(rsaparameters.Modulus) : null),
				E = ((rsaparameters.Exponent != null) ? Base64UrlEncoder.Encode(rsaparameters.Exponent) : null),
				D = ((rsaparameters.D != null) ? Base64UrlEncoder.Encode(rsaparameters.D) : null),
				P = ((rsaparameters.P != null) ? Base64UrlEncoder.Encode(rsaparameters.P) : null),
				Q = ((rsaparameters.Q != null) ? Base64UrlEncoder.Encode(rsaparameters.Q) : null),
				DP = ((rsaparameters.DP != null) ? Base64UrlEncoder.Encode(rsaparameters.DP) : null),
				DQ = ((rsaparameters.DQ != null) ? Base64UrlEncoder.Encode(rsaparameters.DQ) : null),
				QI = ((rsaparameters.InverseQ != null) ? Base64UrlEncoder.Encode(rsaparameters.InverseQ) : null),
				Kty = "RSA",
				Kid = key.KeyId,
				ConvertedSecurityKey = key
			};
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0003F340 File Offset: 0x0003D540
		public static JsonWebKey ConvertFromX509SecurityKey(X509SecurityKey key)
		{
			if (key == null)
			{
				throw LogHelper.LogArgumentNullException("key");
			}
			JsonWebKey jsonWebKey = new JsonWebKey
			{
				Kty = "RSA",
				Kid = key.KeyId,
				X5t = key.X5t,
				ConvertedSecurityKey = key
			};
			if (key.Certificate.RawData != null)
			{
				jsonWebKey.X5c.Add(Convert.ToBase64String(key.Certificate.RawData));
			}
			return jsonWebKey;
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x0003F3B4 File Offset: 0x0003D5B4
		public static JsonWebKey ConvertFromX509SecurityKey(X509SecurityKey key, bool representAsRsaKey)
		{
			if (!representAsRsaKey)
			{
				return JsonWebKeyConverter.ConvertFromX509SecurityKey(key);
			}
			if (key == null)
			{
				throw LogHelper.LogArgumentNullException("key");
			}
			RSA rsa;
			if (key.PrivateKeyStatus == PrivateKeyStatus.Exists)
			{
				rsa = key.PrivateKey as RSA;
			}
			else
			{
				rsa = key.PublicKey as RSA;
			}
			return JsonWebKeyConverter.ConvertFromRSASecurityKey(new RsaSecurityKey(rsa)
			{
				KeyId = key.KeyId
			});
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x0003F414 File Offset: 0x0003D614
		public static JsonWebKey ConvertFromSymmetricSecurityKey(SymmetricSecurityKey key)
		{
			if (key == null)
			{
				throw LogHelper.LogArgumentNullException("key");
			}
			return new JsonWebKey
			{
				K = Base64UrlEncoder.Encode(key.Key),
				Kid = key.KeyId,
				Kty = "oct",
				ConvertedSecurityKey = key
			};
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x0003F464 File Offset: 0x0003D664
		internal static bool TryConvertToSecurityKey(JsonWebKey webKey, out SecurityKey key)
		{
			if (webKey.ConvertedSecurityKey != null)
			{
				key = webKey.ConvertedSecurityKey;
				return true;
			}
			key = null;
			try
			{
				if ("RSA".Equals(webKey.Kty))
				{
					if (JsonWebKeyConverter.TryConvertToX509SecurityKey(webKey, out key))
					{
						return true;
					}
					if (JsonWebKeyConverter.TryCreateToRsaSecurityKey(webKey, out key))
					{
						return true;
					}
				}
				else
				{
					if ("EC".Equals(webKey.Kty))
					{
						return JsonWebKeyConverter.TryConvertToECDsaSecurityKey(webKey, out key);
					}
					if ("oct".Equals(webKey.Kty))
					{
						return JsonWebKeyConverter.TryConvertToSymmetricSecurityKey(webKey, out key);
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.LogWarning(LogHelper.FormatInvariant("IDX10813: Unable to create a {0} from the properties found in the JsonWebKey: '{1}', Exception '{2}'.", new object[]
				{
					LogHelper.MarkAsNonPII(typeof(SecurityKey)),
					webKey,
					ex
				}), Array.Empty<object>());
			}
			LogHelper.LogWarning(LogHelper.FormatInvariant("IDX10812: Unable to create a {0} from the properties found in the JsonWebKey: '{1}'.", new object[]
			{
				LogHelper.MarkAsNonPII(typeof(SecurityKey)),
				webKey
			}), Array.Empty<object>());
			return false;
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x0003F570 File Offset: 0x0003D770
		internal static bool TryConvertToSymmetricSecurityKey(JsonWebKey webKey, out SecurityKey key)
		{
			if (webKey.ConvertedSecurityKey is SymmetricSecurityKey)
			{
				key = webKey.ConvertedSecurityKey;
				return true;
			}
			key = null;
			if (string.IsNullOrEmpty(webKey.K))
			{
				return false;
			}
			try
			{
				key = new SymmetricSecurityKey(webKey);
				return true;
			}
			catch (Exception ex)
			{
				LogHelper.LogExceptionMessage(new InvalidOperationException(LogHelper.FormatInvariant("IDX10813: Unable to create a {0} from the properties found in the JsonWebKey: '{1}', Exception '{2}'.", new object[]
				{
					LogHelper.MarkAsNonPII(typeof(SymmetricSecurityKey)),
					webKey,
					ex
				}), ex));
			}
			return false;
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0003F600 File Offset: 0x0003D800
		internal static bool TryConvertToX509SecurityKey(JsonWebKey webKey, out SecurityKey key)
		{
			if (webKey.ConvertedSecurityKey is X509SecurityKey)
			{
				key = webKey.ConvertedSecurityKey;
				return true;
			}
			key = null;
			if (webKey.X5c == null || webKey.X5c.Count == 0)
			{
				return false;
			}
			try
			{
				key = new X509SecurityKey(webKey);
				return true;
			}
			catch (Exception ex)
			{
				string text = LogHelper.FormatInvariant("IDX10813: Unable to create a {0} from the properties found in the JsonWebKey: '{1}', Exception '{2}'.", new object[]
				{
					LogHelper.MarkAsNonPII(typeof(X509SecurityKey)),
					webKey,
					ex
				});
				webKey.ConvertKeyInfo = text;
				LogHelper.LogExceptionMessage(new InvalidOperationException(text, ex));
			}
			return false;
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x0003F6A0 File Offset: 0x0003D8A0
		internal static bool TryCreateToRsaSecurityKey(JsonWebKey webKey, out SecurityKey key)
		{
			if (webKey.ConvertedSecurityKey is RsaSecurityKey)
			{
				key = webKey.ConvertedSecurityKey;
				return true;
			}
			key = null;
			if (string.IsNullOrWhiteSpace(webKey.E) || string.IsNullOrWhiteSpace(webKey.N))
			{
				return false;
			}
			try
			{
				key = new RsaSecurityKey(webKey);
				return true;
			}
			catch (Exception ex)
			{
				string text = LogHelper.FormatInvariant("IDX10813: Unable to create a {0} from the properties found in the JsonWebKey: '{1}', Exception '{2}'.", new object[]
				{
					LogHelper.MarkAsNonPII(typeof(RsaSecurityKey)),
					webKey,
					ex
				});
				webKey.ConvertKeyInfo = text;
				LogHelper.LogExceptionMessage(new InvalidOperationException(text, ex));
			}
			return false;
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x0003F748 File Offset: 0x0003D948
		internal static bool TryConvertToECDsaSecurityKey(JsonWebKey webKey, out SecurityKey key)
		{
			if (webKey.ConvertedSecurityKey is ECDsaSecurityKey)
			{
				key = webKey.ConvertedSecurityKey;
				return true;
			}
			key = null;
			if (string.IsNullOrEmpty(webKey.Crv) || string.IsNullOrEmpty(webKey.X) || string.IsNullOrEmpty(webKey.Y))
			{
				List<string> list = new List<string>();
				if (string.IsNullOrEmpty(webKey.Crv))
				{
					list.Add("crv");
				}
				if (string.IsNullOrEmpty(webKey.X))
				{
					list.Add("x");
				}
				if (string.IsNullOrEmpty(webKey.Y))
				{
					list.Add("y");
				}
				webKey.ConvertKeyInfo = LogHelper.FormatInvariant("IDX10814: Unable to create a {0} from the properties found in the JsonWebKey: '{1}'. Missing: '{2}'.", new object[]
				{
					LogHelper.MarkAsNonPII(typeof(ECDsaSecurityKey)),
					webKey,
					string.Join(", ", list)
				});
				return false;
			}
			try
			{
				key = new ECDsaSecurityKey(webKey, !string.IsNullOrEmpty(webKey.D));
				return true;
			}
			catch (Exception ex)
			{
				string text = LogHelper.FormatInvariant("IDX10813: Unable to create a {0} from the properties found in the JsonWebKey: '{1}', Exception '{2}'.", new object[]
				{
					LogHelper.MarkAsNonPII(typeof(ECDsaSecurityKey)),
					webKey,
					ex
				});
				webKey.ConvertKeyInfo = text;
				LogHelper.LogExceptionMessage(new InvalidOperationException(text, ex));
			}
			return false;
		}
	}
}
