using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000178 RID: 376
	internal static class SupportedAlgorithms
	{
		// Token: 0x06001101 RID: 4353 RVA: 0x00040EA4 File Offset: 0x0003F0A4
		internal static HashAlgorithmName GetHashAlgorithmName(string algorithm)
		{
			if (string.IsNullOrWhiteSpace(algorithm))
			{
				throw LogHelper.LogArgumentNullException("algorithm");
			}
			if (algorithm != null)
			{
				int length = algorithm.Length;
				if (length <= 49)
				{
					if (length != 5)
					{
						if (length != 49)
						{
							goto IL_0217;
						}
						switch (algorithm[46])
						{
						case '2':
							if (!(algorithm == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"))
							{
								goto IL_0217;
							}
							break;
						case '3':
							if (!(algorithm == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha384"))
							{
								goto IL_0217;
							}
							goto IL_020B;
						case '4':
							goto IL_0217;
						case '5':
							if (!(algorithm == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha512"))
							{
								goto IL_0217;
							}
							goto IL_0211;
						default:
							goto IL_0217;
						}
					}
					else
					{
						char c = algorithm[0];
						if (c != 'E')
						{
							if (c != 'P')
							{
								if (c != 'R')
								{
									goto IL_0217;
								}
								if (!(algorithm == "RS256"))
								{
									if (algorithm == "RS384")
									{
										goto IL_020B;
									}
									if (!(algorithm == "RS512"))
									{
										goto IL_0217;
									}
									goto IL_0211;
								}
							}
							else if (!(algorithm == "PS256"))
							{
								if (algorithm == "PS384")
								{
									goto IL_020B;
								}
								if (!(algorithm == "PS512"))
								{
									goto IL_0217;
								}
								goto IL_0211;
							}
						}
						else if (!(algorithm == "ES256"))
						{
							if (algorithm == "ES384")
							{
								goto IL_020B;
							}
							if (!(algorithm == "ES512"))
							{
								goto IL_0217;
							}
							goto IL_0211;
						}
					}
				}
				else if (length != 51)
				{
					if (length != 54)
					{
						goto IL_0217;
					}
					switch (algorithm[42])
					{
					case '2':
						if (!(algorithm == "http://www.w3.org/2007/05/xmldsig-more#sha256-rsa-MGF1"))
						{
							goto IL_0217;
						}
						break;
					case '3':
						if (!(algorithm == "http://www.w3.org/2007/05/xmldsig-more#sha384-rsa-MGF1"))
						{
							goto IL_0217;
						}
						goto IL_020B;
					case '4':
						goto IL_0217;
					case '5':
						if (!(algorithm == "http://www.w3.org/2007/05/xmldsig-more#sha512-rsa-MGF1"))
						{
							goto IL_0217;
						}
						goto IL_0211;
					default:
						goto IL_0217;
					}
				}
				else
				{
					switch (algorithm[48])
					{
					case '2':
						if (!(algorithm == "http://www.w3.org/2001/04/xmldsig-more#ecdsa-sha256"))
						{
							goto IL_0217;
						}
						break;
					case '3':
						if (!(algorithm == "http://www.w3.org/2001/04/xmldsig-more#ecdsa-sha384"))
						{
							goto IL_0217;
						}
						goto IL_020B;
					case '4':
						goto IL_0217;
					case '5':
						if (!(algorithm == "http://www.w3.org/2001/04/xmldsig-more#ecdsa-sha512"))
						{
							goto IL_0217;
						}
						goto IL_0211;
					default:
						goto IL_0217;
					}
				}
				return HashAlgorithmName.SHA256;
				IL_020B:
				return HashAlgorithmName.SHA384;
				IL_0211:
				return HashAlgorithmName.SHA512;
			}
			IL_0217:
			throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("algorithm", LogHelper.FormatInvariant("IDX10652: The algorithm '{0}' is not supported.", new object[] { LogHelper.MarkAsNonPII(algorithm) })));
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x000410F0 File Offset: 0x0003F2F0
		internal static string GetDigestFromSignatureAlgorithm(string algorithm)
		{
			if (string.IsNullOrWhiteSpace(algorithm))
			{
				throw LogHelper.LogArgumentNullException("algorithm");
			}
			if (algorithm != null)
			{
				int length = algorithm.Length;
				if (length != 5)
				{
					switch (length)
					{
					case 49:
						switch (algorithm[46])
						{
						case '2':
							if (!(algorithm == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"))
							{
								goto IL_02E7;
							}
							break;
						case '3':
							if (!(algorithm == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha384"))
							{
								goto IL_02E7;
							}
							goto IL_02D5;
						case '4':
							goto IL_02E7;
						case '5':
							if (!(algorithm == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha512"))
							{
								goto IL_02E7;
							}
							goto IL_02E1;
						default:
							goto IL_02E7;
						}
						break;
					case 50:
						switch (algorithm[47])
						{
						case '2':
							if (!(algorithm == "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256"))
							{
								goto IL_02E7;
							}
							break;
						case '3':
							if (!(algorithm == "http://www.w3.org/2001/04/xmldsig-more#hmac-sha384"))
							{
								goto IL_02E7;
							}
							goto IL_02D5;
						case '4':
							goto IL_02E7;
						case '5':
							if (!(algorithm == "http://www.w3.org/2001/04/xmldsig-more#hmac-sha512"))
							{
								goto IL_02E7;
							}
							goto IL_02E1;
						default:
							goto IL_02E7;
						}
						break;
					case 51:
						switch (algorithm[48])
						{
						case '2':
							if (!(algorithm == "http://www.w3.org/2001/04/xmldsig-more#ecdsa-sha256"))
							{
								goto IL_02E7;
							}
							break;
						case '3':
							if (!(algorithm == "http://www.w3.org/2001/04/xmldsig-more#ecdsa-sha384"))
							{
								goto IL_02E7;
							}
							goto IL_02D5;
						case '4':
							goto IL_02E7;
						case '5':
							if (!(algorithm == "http://www.w3.org/2001/04/xmldsig-more#ecdsa-sha512"))
							{
								goto IL_02E7;
							}
							goto IL_02E1;
						default:
							goto IL_02E7;
						}
						break;
					case 52:
					case 53:
						goto IL_02E7;
					case 54:
						switch (algorithm[42])
						{
						case '2':
							if (!(algorithm == "http://www.w3.org/2007/05/xmldsig-more#sha256-rsa-MGF1"))
							{
								goto IL_02E7;
							}
							break;
						case '3':
							if (!(algorithm == "http://www.w3.org/2007/05/xmldsig-more#sha384-rsa-MGF1"))
							{
								goto IL_02E7;
							}
							goto IL_02D5;
						case '4':
							goto IL_02E7;
						case '5':
							if (!(algorithm == "http://www.w3.org/2007/05/xmldsig-more#sha512-rsa-MGF1"))
							{
								goto IL_02E7;
							}
							goto IL_02E1;
						default:
							goto IL_02E7;
						}
						break;
					default:
						goto IL_02E7;
					}
					return "http://www.w3.org/2001/04/xmlenc#sha256";
					IL_02D5:
					return "http://www.w3.org/2001/04/xmldsig-more#sha384";
					IL_02E1:
					return "http://www.w3.org/2001/04/xmlenc#sha512";
				}
				char c = algorithm[0];
				if (c <= 'H')
				{
					if (c != 'E')
					{
						if (c != 'H')
						{
							goto IL_02E7;
						}
						if (!(algorithm == "HS256"))
						{
							if (algorithm == "HS384")
							{
								goto IL_02CF;
							}
							if (!(algorithm == "HS512"))
							{
								goto IL_02E7;
							}
							goto IL_02DB;
						}
					}
					else if (!(algorithm == "ES256"))
					{
						if (algorithm == "ES384")
						{
							goto IL_02CF;
						}
						if (!(algorithm == "ES512"))
						{
							goto IL_02E7;
						}
						goto IL_02DB;
					}
				}
				else if (c != 'P')
				{
					if (c != 'R')
					{
						goto IL_02E7;
					}
					if (!(algorithm == "RS256"))
					{
						if (algorithm == "RS384")
						{
							goto IL_02CF;
						}
						if (!(algorithm == "RS512"))
						{
							goto IL_02E7;
						}
						goto IL_02DB;
					}
				}
				else if (!(algorithm == "PS256"))
				{
					if (algorithm == "PS384")
					{
						goto IL_02CF;
					}
					if (!(algorithm == "PS512"))
					{
						goto IL_02E7;
					}
					goto IL_02DB;
				}
				return "SHA256";
				IL_02CF:
				return "SHA384";
				IL_02DB:
				return "SHA512";
			}
			IL_02E7:
			throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10652: The algorithm '{0}' is not supported.", new object[] { LogHelper.MarkAsNonPII(algorithm) }), "algorithm"));
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x0004140C File Offset: 0x0003F60C
		public static bool IsSupportedAlgorithm(string algorithm, SecurityKey key)
		{
			if (key is RsaSecurityKey)
			{
				return SupportedAlgorithms.IsSupportedRsaAlgorithm(algorithm, key);
			}
			X509SecurityKey x509SecurityKey = key as X509SecurityKey;
			if (x509SecurityKey != null)
			{
				return x509SecurityKey.PublicKey is RSA && SupportedAlgorithms.IsSupportedRsaAlgorithm(algorithm, key);
			}
			JsonWebKey jsonWebKey = key as JsonWebKey;
			if (jsonWebKey != null)
			{
				if ("RSA".Equals(jsonWebKey.Kty))
				{
					return SupportedAlgorithms.IsSupportedRsaAlgorithm(algorithm, key);
				}
				if ("EC".Equals(jsonWebKey.Kty))
				{
					return SupportedAlgorithms.IsSupportedEcdsaAlgorithm(algorithm);
				}
				return "oct".Equals(jsonWebKey.Kty) && SupportedAlgorithms.IsSupportedSymmetricAlgorithm(algorithm);
			}
			else
			{
				if (key is ECDsaSecurityKey)
				{
					return SupportedAlgorithms.IsSupportedEcdsaAlgorithm(algorithm);
				}
				return key is SymmetricSecurityKey && SupportedAlgorithms.IsSupportedSymmetricAlgorithm(algorithm);
			}
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x000414C4 File Offset: 0x0003F6C4
		internal static bool IsSupportedEncryptionAlgorithm(string algorithm, SecurityKey key)
		{
			if (key == null)
			{
				return false;
			}
			if (string.IsNullOrEmpty(algorithm))
			{
				return false;
			}
			if (!SupportedAlgorithms.IsAesCbc(algorithm) && !SupportedAlgorithms.IsAesGcm(algorithm))
			{
				return false;
			}
			if (key is SymmetricSecurityKey)
			{
				return true;
			}
			JsonWebKey jsonWebKey = key as JsonWebKey;
			return jsonWebKey != null && jsonWebKey.K != null && jsonWebKey.Kty == "oct";
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00041522 File Offset: 0x0003F722
		internal static bool IsAesGcm(string algorithm)
		{
			return !string.IsNullOrEmpty(algorithm) && (algorithm.Equals("A128GCM") || algorithm.Equals("A192GCM") || algorithm.Equals("A256GCM"));
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x00041555 File Offset: 0x0003F755
		internal static bool IsAesCbc(string algorithm)
		{
			return !string.IsNullOrEmpty(algorithm) && (algorithm.Equals("A128CBC-HS256") || algorithm.Equals("A192CBC-HS384") || algorithm.Equals("A256CBC-HS512"));
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x00041588 File Offset: 0x0003F788
		private static bool IsSupportedEcdsaAlgorithm(string algorithm)
		{
			return SupportedAlgorithms.EcdsaSigningAlgorithms.Contains(algorithm);
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x00041595 File Offset: 0x0003F795
		internal static bool IsSupportedHashAlgorithm(string algorithm)
		{
			return SupportedAlgorithms.HashAlgorithms.Contains(algorithm);
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x000415A4 File Offset: 0x0003F7A4
		internal static bool IsSupportedRsaKeyWrap(string algorithm, SecurityKey key)
		{
			if (key == null)
			{
				return false;
			}
			if (string.IsNullOrEmpty(algorithm))
			{
				return false;
			}
			if (!SupportedAlgorithms.RsaEncryptionAlgorithms.Contains(algorithm))
			{
				return false;
			}
			if (!(key is RsaSecurityKey) && !(key is X509SecurityKey))
			{
				JsonWebKey jsonWebKey = key as JsonWebKey;
				if (jsonWebKey == null || !(jsonWebKey.Kty == "RSA"))
				{
					return false;
				}
			}
			return key.KeySize >= 2048;
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00041610 File Offset: 0x0003F810
		internal static bool IsSupportedSymmetricKeyWrap(string algorithm, SecurityKey key)
		{
			if (key == null)
			{
				return false;
			}
			if (string.IsNullOrEmpty(algorithm))
			{
				return false;
			}
			if (!SupportedAlgorithms.SymmetricKeyWrapAlgorithms.Contains(algorithm))
			{
				return false;
			}
			if (!(key is SymmetricSecurityKey))
			{
				JsonWebKey jsonWebKey = key as JsonWebKey;
				return jsonWebKey != null && jsonWebKey.Kty == "oct";
			}
			return true;
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x00041661 File Offset: 0x0003F861
		internal static bool IsSupportedRsaAlgorithm(string algorithm, SecurityKey key)
		{
			return SupportedAlgorithms.RsaSigningAlgorithms.Contains(algorithm) || SupportedAlgorithms.RsaEncryptionAlgorithms.Contains(algorithm) || (SupportedAlgorithms.RsaPssSigningAlgorithms.Contains(algorithm) && SupportedAlgorithms.IsSupportedRsaPss(key));
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x00041694 File Offset: 0x0003F894
		private static bool IsSupportedRsaPss(SecurityKey key)
		{
			RsaSecurityKey rsaSecurityKey = key as RsaSecurityKey;
			if (rsaSecurityKey != null && rsaSecurityKey.Rsa is RSACryptoServiceProvider)
			{
				LogHelper.LogInformation("IDX10693: RSACryptoServiceProvider doesn't support the RSASSA-PSS signature algorithm. The list of supported algorithms is available here: https://aka.ms/IdentityModel/supported-algorithms", Array.Empty<object>());
				return false;
			}
			X509SecurityKey x509SecurityKey = key as X509SecurityKey;
			if (x509SecurityKey != null && x509SecurityKey.PublicKey is RSACryptoServiceProvider)
			{
				LogHelper.LogInformation("IDX10693: RSACryptoServiceProvider doesn't support the RSASSA-PSS signature algorithm. The list of supported algorithms is available here: https://aka.ms/IdentityModel/supported-algorithms", Array.Empty<object>());
				return false;
			}
			return true;
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x000416F2 File Offset: 0x0003F8F2
		internal static bool IsSupportedSymmetricAlgorithm(string algorithm)
		{
			return SupportedAlgorithms.SymmetricEncryptionAlgorithms.Contains(algorithm) || SupportedAlgorithms.SymmetricKeyWrapAlgorithms.Contains(algorithm) || SupportedAlgorithms.SymmetricSigningAlgorithms.Contains(algorithm);
		}

		// Token: 0x0400067A RID: 1658
		private const int RsaMinKeySize = 2048;

		// Token: 0x0400067B RID: 1659
		internal static readonly ICollection<string> EcdsaSigningAlgorithms = new Collection<string> { "ES256", "http://www.w3.org/2001/04/xmldsig-more#ecdsa-sha256", "ES384", "http://www.w3.org/2001/04/xmldsig-more#ecdsa-sha384", "ES512", "http://www.w3.org/2001/04/xmldsig-more#ecdsa-sha512" };

		// Token: 0x0400067C RID: 1660
		internal static readonly ICollection<string> HashAlgorithms = new Collection<string> { "SHA256", "http://www.w3.org/2001/04/xmlenc#sha256", "SHA384", "http://www.w3.org/2001/04/xmldsig-more#sha384", "SHA512", "http://www.w3.org/2001/04/xmlenc#sha512" };

		// Token: 0x0400067D RID: 1661
		internal static readonly ICollection<string> RsaEncryptionAlgorithms = new Collection<string> { "RSA-OAEP", "RSA1_5", "http://www.w3.org/2001/04/xmlenc#rsa-oaep" };

		// Token: 0x0400067E RID: 1662
		internal static readonly ICollection<string> RsaSigningAlgorithms = new Collection<string> { "RS256", "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256", "RS384", "http://www.w3.org/2001/04/xmldsig-more#rsa-sha384", "RS512", "http://www.w3.org/2001/04/xmldsig-more#rsa-sha512" };

		// Token: 0x0400067F RID: 1663
		internal static readonly ICollection<string> RsaPssSigningAlgorithms = new Collection<string> { "PS256", "http://www.w3.org/2007/05/xmldsig-more#sha256-rsa-MGF1", "PS384", "http://www.w3.org/2007/05/xmldsig-more#sha384-rsa-MGF1", "PS512", "http://www.w3.org/2007/05/xmldsig-more#sha512-rsa-MGF1" };

		// Token: 0x04000680 RID: 1664
		internal static readonly ICollection<string> SymmetricEncryptionAlgorithms = new Collection<string> { "A128CBC-HS256", "A192CBC-HS384", "A256CBC-HS512", "A128GCM", "A192GCM", "A256GCM" };

		// Token: 0x04000681 RID: 1665
		internal static readonly ICollection<string> SymmetricKeyWrapAlgorithms = new Collection<string> { "A128KW", "http://www.w3.org/2001/04/xmlenc#kw-aes128", "A192KW", "http://www.w3.org/2001/04/xmlenc#kw-aes192", "A256KW", "http://www.w3.org/2001/04/xmlenc#kw-aes256", "ECDH-ES+A128KW", "ECDH-ES+A192KW", "ECDH-ES+A256KW" };

		// Token: 0x04000682 RID: 1666
		internal static readonly ICollection<string> SymmetricSigningAlgorithms = new Collection<string> { "HS256", "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256", "HS384", "http://www.w3.org/2001/04/xmldsig-more#hmac-sha384", "HS512", "http://www.w3.org/2001/04/xmldsig-more#hmac-sha512" };

		// Token: 0x04000683 RID: 1667
		internal static readonly ICollection<string> EcdsaWrapAlgorithms = new Collection<string> { "ECDH-ES+A128KW", "ECDH-ES+A192KW", "ECDH-ES+A256KW" };
	}
}
