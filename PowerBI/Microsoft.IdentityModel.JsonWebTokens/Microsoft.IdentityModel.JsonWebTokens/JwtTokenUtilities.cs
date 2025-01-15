using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Json.Linq;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.IdentityModel.JsonWebTokens
{
	// Token: 0x0200000B RID: 11
	public class JwtTokenUtilities
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00006A04 File Offset: 0x00004C04
		public static string CreateEncodedSignature(string input, SigningCredentials signingCredentials)
		{
			if (input == null)
			{
				throw LogHelper.LogArgumentNullException("input");
			}
			if (signingCredentials == null)
			{
				throw LogHelper.LogArgumentNullException("signingCredentials");
			}
			CryptoProviderFactory cryptoProviderFactory = signingCredentials.CryptoProviderFactory ?? signingCredentials.Key.CryptoProviderFactory;
			SignatureProvider signatureProvider = cryptoProviderFactory.CreateForSigning(signingCredentials.Key, signingCredentials.Algorithm);
			if (signatureProvider == null)
			{
				throw LogHelper.LogExceptionMessage(new InvalidOperationException(LogHelper.FormatInvariant("IDX10637: CryptoProviderFactory.CreateForSigning returned null for key: '{0}', signatureAlgorithm: '{1}'.", new object[]
				{
					(signingCredentials.Key == null) ? "Null" : signingCredentials.Key.ToString(),
					LogHelper.MarkAsNonPII(signingCredentials.Algorithm)
				})));
			}
			string text;
			try
			{
				LogHelper.LogVerbose("IDX14200: Creating raw signature using the signature credentials.", Array.Empty<object>());
				text = Base64UrlEncoder.Encode(signatureProvider.Sign(Encoding.UTF8.GetBytes(input)));
			}
			finally
			{
				cryptoProviderFactory.ReleaseSignatureProvider(signatureProvider);
			}
			return text;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00006AE4 File Offset: 0x00004CE4
		public static string CreateEncodedSignature(string input, SigningCredentials signingCredentials, bool cacheProvider)
		{
			if (input == null)
			{
				throw LogHelper.LogArgumentNullException("input");
			}
			if (signingCredentials == null)
			{
				throw LogHelper.LogArgumentNullException("signingCredentials");
			}
			CryptoProviderFactory cryptoProviderFactory = signingCredentials.CryptoProviderFactory ?? signingCredentials.Key.CryptoProviderFactory;
			SignatureProvider signatureProvider = cryptoProviderFactory.CreateForSigning(signingCredentials.Key, signingCredentials.Algorithm, cacheProvider);
			if (signatureProvider == null)
			{
				throw LogHelper.LogExceptionMessage(new InvalidOperationException(LogHelper.FormatInvariant("IDX10637: CryptoProviderFactory.CreateForSigning returned null for key: '{0}', signatureAlgorithm: '{1}'.", new object[]
				{
					(signingCredentials.Key == null) ? "Null" : signingCredentials.Key.ToString(),
					LogHelper.MarkAsNonPII(signingCredentials.Algorithm)
				})));
			}
			string text;
			try
			{
				LogHelper.LogVerbose(LogHelper.FormatInvariant("IDX14201: Creating raw signature using the signature credentials. Caching SignatureProvider: '{0}'.", new object[] { LogHelper.MarkAsNonPII(cacheProvider) }), Array.Empty<object>());
				text = Base64UrlEncoder.Encode(signatureProvider.Sign(Encoding.UTF8.GetBytes(input)));
			}
			finally
			{
				cryptoProviderFactory.ReleaseSignatureProvider(signatureProvider);
			}
			return text;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00006BDC File Offset: 0x00004DDC
		internal static string DecompressToken(byte[] tokenBytes, string algorithm, int maximumDeflateSize)
		{
			if (tokenBytes == null)
			{
				throw LogHelper.LogArgumentNullException("tokenBytes");
			}
			if (string.IsNullOrEmpty(algorithm))
			{
				throw LogHelper.LogArgumentNullException("algorithm");
			}
			if (!CompressionProviderFactory.Default.IsSupportedAlgorithm(algorithm))
			{
				throw LogHelper.LogExceptionMessage(new NotSupportedException(LogHelper.FormatInvariant("IDX10682: Compression algorithm '{0}' is not supported.", new object[] { LogHelper.MarkAsNonPII(algorithm) })));
			}
			byte[] array = CompressionProviderFactory.Default.CreateCompressionProvider(algorithm, maximumDeflateSize).Decompress(tokenBytes);
			if (array == null)
			{
				throw LogHelper.LogExceptionMessage(new SecurityTokenDecompressionFailedException(LogHelper.FormatInvariant("IDX10679: Failed to decompress using algorithm '{0}'.", new object[] { LogHelper.MarkAsNonPII(algorithm) })));
			}
			return Encoding.UTF8.GetString(array);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00006C80 File Offset: 0x00004E80
		internal static string DecryptJwtToken(SecurityToken securityToken, TokenValidationParameters validationParameters, JwtTokenDecryptionParameters decryptionParameters)
		{
			if (validationParameters == null)
			{
				throw LogHelper.LogArgumentNullException("validationParameters");
			}
			if (decryptionParameters == null)
			{
				throw LogHelper.LogArgumentNullException("decryptionParameters");
			}
			bool flag = false;
			bool flag2 = false;
			byte[] array = null;
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			string text = null;
			foreach (SecurityKey securityKey in decryptionParameters.Keys)
			{
				CryptoProviderFactory cryptoProviderFactory = validationParameters.CryptoProviderFactory ?? securityKey.CryptoProviderFactory;
				if (cryptoProviderFactory == null)
				{
					LogHelper.LogWarning("IDX10607: Decryption skipping key: '{0}', both validationParameters.CryptoProviderFactory and key.CryptoProviderFactory are null.", new object[] { securityKey });
				}
				else
				{
					try
					{
						JsonWebToken jsonWebToken = securityToken as JsonWebToken;
						if (jsonWebToken != null)
						{
							if (!cryptoProviderFactory.IsSupportedAlgorithm(jsonWebToken.Enc, securityKey))
							{
								flag2 = true;
								LogHelper.LogWarning("IDX10611: Decryption failed. Encryption is not supported for: Algorithm: '{0}', SecurityKey: '{1}'.", new object[]
								{
									LogHelper.MarkAsNonPII(decryptionParameters.Enc),
									securityKey
								});
								continue;
							}
							Validators.ValidateAlgorithm(jsonWebToken.Enc, securityKey, securityToken, validationParameters);
							array = JwtTokenUtilities.DecryptToken(cryptoProviderFactory, securityKey, jsonWebToken.Enc, jsonWebToken.CipherTextBytes, jsonWebToken.HeaderAsciiBytes, jsonWebToken.InitializationVectorBytes, jsonWebToken.AuthenticationTagBytes);
							text = jsonWebToken.Zip;
							flag = true;
							break;
						}
						else
						{
							if (!cryptoProviderFactory.IsSupportedAlgorithm(decryptionParameters.Enc, securityKey))
							{
								flag2 = true;
								LogHelper.LogWarning("IDX10611: Decryption failed. Encryption is not supported for: Algorithm: '{0}', SecurityKey: '{1}'.", new object[]
								{
									LogHelper.MarkAsNonPII(decryptionParameters.Enc),
									securityKey
								});
								continue;
							}
							Validators.ValidateAlgorithm(decryptionParameters.Enc, securityKey, securityToken, validationParameters);
							array = JwtTokenUtilities.DecryptToken(cryptoProviderFactory, securityKey, decryptionParameters.Enc, decryptionParameters.CipherTextBytes, decryptionParameters.HeaderAsciiBytes, decryptionParameters.InitializationVectorBytes, decryptionParameters.AuthenticationTagBytes);
							text = decryptionParameters.Zip;
							flag = true;
							break;
						}
					}
					catch (Exception ex)
					{
						stringBuilder.AppendLine(ex.ToString());
					}
					if (securityKey != null)
					{
						stringBuilder2.AppendLine(securityKey.ToString());
					}
				}
			}
			JwtTokenUtilities.ValidateDecryption(decryptionParameters, flag, flag2, stringBuilder, stringBuilder2);
			string text2;
			try
			{
				if (string.IsNullOrEmpty(text))
				{
					text2 = Encoding.UTF8.GetString(array);
				}
				else
				{
					text2 = decryptionParameters.DecompressionFunction(array, text, decryptionParameters.MaximumDeflateSize);
				}
			}
			catch (Exception ex2)
			{
				throw LogHelper.LogExceptionMessage(new SecurityTokenDecompressionFailedException(LogHelper.FormatInvariant("IDX10679: Failed to decompress using algorithm '{0}'.", new object[] { text }), ex2));
			}
			return text2;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00006F0C File Offset: 0x0000510C
		private static void ValidateDecryption(JwtTokenDecryptionParameters decryptionParameters, bool decryptionSucceeded, bool algorithmNotSupportedByCryptoProvider, StringBuilder exceptionStrings, StringBuilder keysAttempted)
		{
			if (!decryptionSucceeded && keysAttempted.Length > 0)
			{
				throw LogHelper.LogExceptionMessage(new SecurityTokenDecryptionFailedException(LogHelper.FormatInvariant("IDX10603: Decryption failed. Keys tried: '{0}'.\nExceptions caught:\n '{1}'.\ntoken: '{2}'", new object[]
				{
					keysAttempted,
					exceptionStrings,
					LogHelper.MarkAsSecurityArtifact(decryptionParameters.EncodedToken, new Func<object, string>(JwtTokenUtilities.SafeLogJwtToken))
				})));
			}
			if (!decryptionSucceeded && algorithmNotSupportedByCryptoProvider)
			{
				throw LogHelper.LogExceptionMessage(new SecurityTokenDecryptionFailedException(LogHelper.FormatInvariant("IDX10619: Decryption failed. Algorithm: '{0}'. Either the Encryption Algorithm: '{1}' or none of the Security Keys are supported by the CryptoProviderFactory.", new object[]
				{
					LogHelper.MarkAsNonPII(decryptionParameters.Alg),
					LogHelper.MarkAsNonPII(decryptionParameters.Enc)
				})));
			}
			if (!decryptionSucceeded)
			{
				throw LogHelper.LogExceptionMessage(new SecurityTokenDecryptionFailedException(LogHelper.FormatInvariant("IDX10609: Decryption failed. No Keys tried: token: '{0}'.", new object[] { LogHelper.MarkAsSecurityArtifact(decryptionParameters.EncodedToken, new Func<object, string>(JwtTokenUtilities.SafeLogJwtToken)) })));
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00006FDC File Offset: 0x000051DC
		private static byte[] DecryptToken(CryptoProviderFactory cryptoProviderFactory, SecurityKey key, string encAlg, byte[] ciphertext, byte[] headerAscii, byte[] initializationVector, byte[] authenticationTag)
		{
			byte[] array;
			using (AuthenticatedEncryptionProvider authenticatedEncryptionProvider = cryptoProviderFactory.CreateAuthenticatedEncryptionProvider(key, encAlg))
			{
				if (authenticatedEncryptionProvider == null)
				{
					throw LogHelper.LogExceptionMessage(new InvalidOperationException(LogHelper.FormatInvariant("IDX10610: Decryption failed. Could not create decryption provider. Key: '{0}', Algorithm: '{1}'.", new object[]
					{
						key,
						LogHelper.MarkAsNonPII(encAlg)
					})));
				}
				array = authenticatedEncryptionProvider.Decrypt(ciphertext, headerAscii, initializationVector, authenticationTag);
			}
			return array;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00007048 File Offset: 0x00005248
		public static byte[] GenerateKeyBytes(int sizeInBits)
		{
			byte[] array = null;
			if (sizeInBits != 256 && sizeInBits != 384 && sizeInBits != 512)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException("IDX10401: Invalid requested key size. Valid key sizes are: 256, 384, and 512.", "sizeInBits"));
			}
			using (Aes aes = Aes.Create())
			{
				int num = sizeInBits >> 4;
				array = new byte[num << 1];
				aes.KeySize = sizeInBits >> 1;
				aes.GenerateKey();
				Array.Copy(aes.Key, array, num);
				aes.GenerateKey();
				Array.Copy(aes.Key, 0, array, num, num);
			}
			return array;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000070E8 File Offset: 0x000052E8
		internal static SecurityKey GetSecurityKey(EncryptingCredentials encryptingCredentials, CryptoProviderFactory cryptoProviderFactory, IDictionary<string, object> additionalHeaderClaims, out byte[] wrappedKey)
		{
			wrappedKey = null;
			SecurityKey securityKey;
			if ("dir".Equals(encryptingCredentials.Alg))
			{
				if (!cryptoProviderFactory.IsSupportedAlgorithm(encryptingCredentials.Enc, encryptingCredentials.Key))
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenEncryptionFailedException(LogHelper.FormatInvariant("IDX10615: Encryption failed. No support for: Algorithm: '{0}', SecurityKey: '{1}'.", new object[]
					{
						LogHelper.MarkAsNonPII(encryptingCredentials.Enc),
						encryptingCredentials.Key
					})));
				}
				securityKey = encryptingCredentials.Key;
			}
			else
			{
				if (!cryptoProviderFactory.IsSupportedAlgorithm(encryptingCredentials.Alg, encryptingCredentials.Key))
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenEncryptionFailedException(LogHelper.FormatInvariant("IDX10615: Encryption failed. No support for: Algorithm: '{0}', SecurityKey: '{1}'.", new object[]
					{
						LogHelper.MarkAsNonPII(encryptingCredentials.Alg),
						encryptingCredentials.Key
					})));
				}
				if ("A128CBC-HS256".Equals(encryptingCredentials.Enc))
				{
					securityKey = new SymmetricSecurityKey(JwtTokenUtilities.GenerateKeyBytes(256));
				}
				else if ("A192CBC-HS384".Equals(encryptingCredentials.Enc))
				{
					securityKey = new SymmetricSecurityKey(JwtTokenUtilities.GenerateKeyBytes(384));
				}
				else
				{
					if (!"A256CBC-HS512".Equals(encryptingCredentials.Enc))
					{
						throw LogHelper.LogExceptionMessage(new SecurityTokenEncryptionFailedException(LogHelper.FormatInvariant("IDX10617: Encryption failed. Keywrap is only supported for: '{0}', '{1}' and '{2}'. The content encryption specified is: '{3}'.", new object[]
						{
							LogHelper.MarkAsNonPII("A128CBC-HS256"),
							LogHelper.MarkAsNonPII("A192CBC-HS384"),
							LogHelper.MarkAsNonPII("A256CBC-HS512"),
							LogHelper.MarkAsNonPII(encryptingCredentials.Enc)
						})));
					}
					securityKey = new SymmetricSecurityKey(JwtTokenUtilities.GenerateKeyBytes(512));
				}
				KeyWrapProvider keyWrapProvider = cryptoProviderFactory.CreateKeyWrapProvider(encryptingCredentials.Key, encryptingCredentials.Alg);
				wrappedKey = keyWrapProvider.WrapKey(((SymmetricSecurityKey)securityKey).Key);
			}
			return securityKey;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000728C File Offset: 0x0000548C
		public static IEnumerable<SecurityKey> GetAllDecryptionKeys(TokenValidationParameters validationParameters)
		{
			if (validationParameters == null)
			{
				throw new ArgumentNullException("validationParameters");
			}
			Collection<SecurityKey> collection = new Collection<SecurityKey>();
			if (validationParameters.TokenDecryptionKey != null)
			{
				collection.Add(validationParameters.TokenDecryptionKey);
			}
			if (validationParameters.TokenDecryptionKeys != null)
			{
				foreach (SecurityKey securityKey in validationParameters.TokenDecryptionKeys)
				{
					collection.Add(securityKey);
				}
			}
			return collection;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000730C File Offset: 0x0000550C
		internal static DateTime GetDateTime(string key, JObject payload)
		{
			JToken jtoken;
			if (!payload.TryGetValue(key, out jtoken))
			{
				return DateTime.MinValue;
			}
			return EpochTime.DateTime(Convert.ToInt64(Math.Truncate(Convert.ToDouble(JwtTokenUtilities.ParseTimeValue(jtoken, key), CultureInfo.InvariantCulture))));
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00007350 File Offset: 0x00005550
		private static long ParseTimeValue(JToken jToken, string claimName)
		{
			if (jToken.Type == JTokenType.Integer || jToken.Type == JTokenType.Float)
			{
				return (long)jToken;
			}
			if (jToken.Type == JTokenType.String)
			{
				long num;
				if (long.TryParse((string)jToken, out num))
				{
					return num;
				}
				float num2;
				if (float.TryParse((string)jToken, out num2))
				{
					return (long)num2;
				}
				double num3;
				if (double.TryParse((string)jToken, out num3))
				{
					return (long)num3;
				}
			}
			throw LogHelper.LogExceptionMessage(new FormatException(LogHelper.FormatInvariant("IDX14300: Could not parse '{0}' : '{1}' as a '{2}'.", new object[]
			{
				LogHelper.MarkAsNonPII(claimName),
				jToken.ToString(),
				LogHelper.MarkAsNonPII(typeof(long))
			})));
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000073F4 File Offset: 0x000055F4
		internal static string SafeLogJwtToken(object obj)
		{
			if (obj == null)
			{
				return string.Empty;
			}
			string text = obj as string;
			if (text == null)
			{
				return obj.GetType().ToString();
			}
			int num = text.LastIndexOf(".");
			if (num == -1)
			{
				return "UnrecognizedEncodedToken";
			}
			return text.Substring(0, num);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000743E File Offset: 0x0000563E
		internal static SecurityKey ResolveTokenSigningKey(string kid, string x5t, TokenValidationParameters validationParameters, BaseConfiguration configuration)
		{
			return JwtTokenUtilities.ResolveTokenSigningKey(kid, x5t, (configuration != null) ? configuration.SigningKeys : null) ?? JwtTokenUtilities.ResolveTokenSigningKey(kid, x5t, JwtTokenUtilities.ConcatSigningKeys(validationParameters));
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00007464 File Offset: 0x00005664
		internal static SecurityKey ResolveTokenSigningKey(string kid, string x5t, IEnumerable<SecurityKey> signingKeys)
		{
			if (signingKeys == null)
			{
				return null;
			}
			foreach (SecurityKey securityKey in signingKeys)
			{
				if (securityKey != null)
				{
					X509SecurityKey x509SecurityKey = securityKey as X509SecurityKey;
					if (x509SecurityKey != null)
					{
						if ((!string.IsNullOrEmpty(kid) && string.Equals(securityKey.KeyId, kid, StringComparison.OrdinalIgnoreCase)) || (!string.IsNullOrEmpty(x5t) && string.Equals(x509SecurityKey.X5t, x5t, StringComparison.OrdinalIgnoreCase)))
						{
							return securityKey;
						}
					}
					else if (!string.IsNullOrEmpty(securityKey.KeyId) && (string.Equals(securityKey.KeyId, kid) || string.Equals(securityKey.KeyId, x5t)))
					{
						return securityKey;
					}
				}
			}
			return null;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000751C File Offset: 0x0000571C
		internal static IEnumerable<SecurityKey> ConcatSigningKeys(TokenValidationParameters tvp)
		{
			if (tvp == null)
			{
				yield break;
			}
			yield return tvp.IssuerSigningKey;
			if (tvp.IssuerSigningKeys != null)
			{
				foreach (SecurityKey securityKey in tvp.IssuerSigningKeys)
				{
					yield return securityKey;
				}
				IEnumerator<SecurityKey> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000752C File Offset: 0x0000572C
		internal static JsonDocument ParseDocument(byte[] bytes, int length)
		{
			JsonDocument jsonDocument;
			using (MemoryStream memoryStream = new MemoryStream(bytes, 0, length))
			{
				jsonDocument = JsonDocument.Parse(memoryStream, default(JsonDocumentOptions));
			}
			return jsonDocument;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00007570 File Offset: 0x00005770
		internal static JsonDocument GetJsonDocumentFromBase64UrlEncodedString(string rawString, int startIndex, int length)
		{
			return Base64UrlEncoding.Decode<JsonDocument>(rawString, startIndex, length, new Func<byte[], int, JsonDocument>(JwtTokenUtilities.ParseDocument));
		}

		// Token: 0x04000084 RID: 132
		private const string _unrecognizedEncodedToken = "UnrecognizedEncodedToken";

		// Token: 0x04000085 RID: 133
		public static Regex RegexJws = new Regex("^[A-Za-z0-9-_]+\\.[A-Za-z0-9-_]+\\.[A-Za-z0-9-_]*$", RegexOptions.Compiled | RegexOptions.CultureInvariant, TimeSpan.FromMilliseconds(100.0));

		// Token: 0x04000086 RID: 134
		public static Regex RegexJwe = new Regex("^[A-Za-z0-9-_]+\\.[A-Za-z0-9-_]*\\.[A-Za-z0-9-_]+\\.[A-Za-z0-9-_]+\\.[A-Za-z0-9-_]+$", RegexOptions.Compiled | RegexOptions.CultureInvariant, TimeSpan.FromMilliseconds(100.0));

		// Token: 0x04000087 RID: 135
		internal static IList<string> DefaultHeaderParameters = new List<string> { "alg", "kid", "x5t", "enc", "zip" };
	}
}
