using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.Caching;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000025 RID: 37
	internal class AzureAttestationEnclaveProvider : EnclaveProviderBase
	{
		// Token: 0x0600069C RID: 1692 RVA: 0x0000CCF4 File Offset: 0x0000AEF4
		internal override void GetEnclaveSession(EnclaveSessionParameters enclaveSessionParameters, bool generateCustomData, bool isRetry, out SqlEnclaveSession sqlEnclaveSession, out long counter, out byte[] customData, out int customDataLength)
		{
			base.GetEnclaveSessionHelper(enclaveSessionParameters, generateCustomData, isRetry, out sqlEnclaveSession, out counter, out customData, out customDataLength);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0000CD08 File Offset: 0x0000AF08
		internal override SqlEnclaveAttestationParameters GetAttestationParameters(string attestationUrl, byte[] customData, int customDataLength)
		{
			ECDiffieHellman ecdiffieHellman = KeyConverter.CreateECDiffieHellman(384);
			byte[] array = this.PrepareAttestationParameters(attestationUrl, customData, customDataLength);
			return new SqlEnclaveAttestationParameters(1, array, ecdiffieHellman);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0000CD34 File Offset: 0x0000AF34
		internal override void CreateEnclaveSession(byte[] attestationInfo, ECDiffieHellman clientDHKey, EnclaveSessionParameters enclaveSessionParameters, byte[] customData, int customDataLength, out SqlEnclaveSession sqlEnclaveSession, out long counter)
		{
			sqlEnclaveSession = null;
			counter = 0L;
			try
			{
				EnclaveProviderBase.ThreadRetryCache.Remove(Thread.CurrentThread.ManagedThreadId.ToString(), null);
				sqlEnclaveSession = base.GetEnclaveSessionFromCache(enclaveSessionParameters, out counter);
				if (sqlEnclaveSession == null)
				{
					if (string.IsNullOrEmpty(enclaveSessionParameters.AttestationUrl) || customData == null || customDataLength <= 0)
					{
						throw SQL.AttestationFailed(Strings.FailToCreateEnclaveSession, null);
					}
					IdentityModelEventSource.ShowPII = true;
					AzureAttestationEnclaveProvider.AzureAttestationInfo azureAttestationInfo = new AzureAttestationEnclaveProvider.AzureAttestationInfo(attestationInfo);
					this.VerifyAzureAttestationInfo(enclaveSessionParameters.AttestationUrl, azureAttestationInfo.EnclaveType, azureAttestationInfo.AttestationToken.AttestationToken, azureAttestationInfo.Identity, customData);
					byte[] sharedSecret = this.GetSharedSecret(azureAttestationInfo.Identity, customData, azureAttestationInfo.EnclaveType, azureAttestationInfo.EnclaveDHInfo, clientDHKey);
					sqlEnclaveSession = base.AddEnclaveSessionToCache(enclaveSessionParameters, sharedSecret, azureAttestationInfo.SessionId, out counter);
				}
			}
			finally
			{
				base.UpdateEnclaveSessionLockStatus(sqlEnclaveSession);
			}
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0000CE20 File Offset: 0x0000B020
		internal override void InvalidateEnclaveSession(EnclaveSessionParameters enclaveSessionParameters, SqlEnclaveSession enclaveSessionToInvalidate)
		{
			base.InvalidateEnclaveSessionHelper(enclaveSessionParameters, enclaveSessionToInvalidate);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0000CE2C File Offset: 0x0000B02C
		internal byte[] PrepareAttestationParameters(string attestationUrl, byte[] attestNonce, int attestNonceLength)
		{
			if (!string.IsNullOrEmpty(attestationUrl) && attestNonce != null && attestNonceLength > 0)
			{
				string text = attestationUrl + "\0";
				byte[] bytes = Encoding.Unicode.GetBytes(text);
				byte[] bytes2 = BitConverter.GetBytes(bytes.Length);
				byte[] bytes3 = BitConverter.GetBytes(attestNonceLength);
				int num = bytes.Length + bytes2.Length + attestNonce.Length + bytes3.Length;
				int num2 = 0;
				byte[] array = new byte[num];
				Buffer.BlockCopy(bytes2, 0, array, num2, bytes2.Length);
				num2 += bytes2.Length;
				Buffer.BlockCopy(bytes, 0, array, num2, bytes.Length);
				num2 += bytes.Length;
				Buffer.BlockCopy(bytes3, 0, array, num2, bytes3.Length);
				num2 += bytes3.Length;
				Buffer.BlockCopy(attestNonce, 0, array, num2, attestNonce.Length);
				num2 += attestNonce.Length;
				return array;
			}
			throw SQL.AttestationFailed(Strings.FailToCreateEnclaveSession, null);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0000CF04 File Offset: 0x0000B104
		private void VerifyAzureAttestationInfo(string attestationUrl, EnclaveType enclaveType, string attestationToken, EnclavePublicKey enclavePublicKey, byte[] nonce)
		{
			bool flag = false;
			string attestationInstanceUrl = this.GetAttestationInstanceUrl(attestationUrl);
			string empty = string.Empty;
			bool flag2;
			bool flag3;
			do
			{
				flag2 = false;
				OpenIdConnectConfiguration openIdConfigForSigningKeys = this.GetOpenIdConfigForSigningKeys(attestationInstanceUrl, flag);
				bool flag4;
				flag3 = this.VerifyTokenSignature(attestationToken, attestationInstanceUrl, openIdConfigForSigningKeys.SigningKeys, out flag4, out empty);
				if (!flag3 && flag4 && !flag)
				{
					flag = true;
					flag2 = true;
				}
			}
			while (flag2);
			if (!flag3)
			{
				throw SQL.AttestationFailed(string.Format(Strings.AttestationTokenSignatureValidationFailed, empty), null);
			}
			this.ValidateAttestationClaims(enclaveType, attestationToken, enclavePublicKey, nonce);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0000CF78 File Offset: 0x0000B178
		private static string GetInnerMostExceptionMessage(Exception exception)
		{
			Exception ex = exception;
			while (ex.InnerException != null)
			{
				ex = ex.InnerException;
			}
			return ex.Message;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0000CFA0 File Offset: 0x0000B1A0
		private OpenIdConnectConfiguration GetOpenIdConfigForSigningKeys(string url, bool forceUpdate)
		{
			OpenIdConnectConfiguration openIdConnectConfiguration = AzureAttestationEnclaveProvider.OpenIdConnectConfigurationCache[url] as OpenIdConnectConfiguration;
			if (forceUpdate || openIdConnectConfiguration == null)
			{
				string text = url + "/.well-known/openid-configuration";
				try
				{
					IConfigurationManager<OpenIdConnectConfiguration> configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(text, new OpenIdConnectConfigurationRetriever());
					openIdConnectConfiguration = configurationManager.GetConfigurationAsync(CancellationToken.None).Result;
				}
				catch (Exception ex)
				{
					throw SQL.AttestationFailed(string.Format(Strings.GetAttestationTokenSigningKeysFailed, AzureAttestationEnclaveProvider.GetInnerMostExceptionMessage(ex)), ex);
				}
				AzureAttestationEnclaveProvider.OpenIdConnectConfigurationCache.Add(url, openIdConnectConfiguration, DateTime.UtcNow.AddDays(1.0), null);
			}
			return openIdConnectConfiguration;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0000D044 File Offset: 0x0000B244
		private string GetAttestationInstanceUrl(string attestationUrl)
		{
			Uri uri = new Uri(attestationUrl);
			return uri.GetLeftPart(UriPartial.Authority);
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0000D060 File Offset: 0x0000B260
		private static ICollection<string> GenerateListOfIssuers(string tokenIssuerUrl)
		{
			List<string> list = new List<string>();
			Uri uri = new Uri(tokenIssuerUrl);
			int port = uri.Port;
			bool isDefaultPort = uri.IsDefaultPort;
			string leftPart = uri.GetLeftPart(UriPartial.Authority);
			list.Add(leftPart);
			if (isDefaultPort)
			{
				list.Add(leftPart + ":" + port.ToString());
			}
			return list;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0000D0B8 File Offset: 0x0000B2B8
		private bool VerifyTokenSignature(string attestationToken, string tokenIssuerUrl, ICollection<SecurityKey> issuerSigningKeys, out bool isKeySigningExpired, out string exceptionMessage)
		{
			exceptionMessage = string.Empty;
			bool flag = false;
			isKeySigningExpired = false;
			TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
			{
				RequireExpirationTime = true,
				ValidateLifetime = true,
				ValidateIssuer = true,
				ValidateAudience = false,
				RequireSignedTokens = true,
				ValidIssuers = AzureAttestationEnclaveProvider.GenerateListOfIssuers(tokenIssuerUrl),
				IssuerSigningKeys = issuerSigningKeys
			};
			try
			{
				JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
				SecurityToken securityToken;
				ClaimsPrincipal claimsPrincipal = jwtSecurityTokenHandler.ValidateToken(attestationToken, tokenValidationParameters, out securityToken);
				flag = true;
			}
			catch (SecurityTokenExpiredException ex)
			{
				throw SQL.AttestationFailed(Strings.ExpiredAttestationToken, ex);
			}
			catch (SecurityTokenValidationException ex2)
			{
				isKeySigningExpired = true;
				Thread.Sleep(3000);
				exceptionMessage = AzureAttestationEnclaveProvider.GetInnerMostExceptionMessage(ex2);
			}
			catch (Exception ex3)
			{
				throw SQL.AttestationFailed(string.Format(Strings.InvalidAttestationToken, AzureAttestationEnclaveProvider.GetInnerMostExceptionMessage(ex3)), null);
			}
			return flag;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0000D194 File Offset: 0x0000B394
		private byte[] ComputeSHA256(byte[] data)
		{
			byte[] array = null;
			try
			{
				using (SHA256 sha = SHA256.Create())
				{
					array = sha.ComputeHash(data);
				}
			}
			catch (Exception ex)
			{
				throw SQL.AttestationFailed(Strings.InvalidArgumentToSHA256, ex);
			}
			return array;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0000D1EC File Offset: 0x0000B3EC
		private void ValidateAttestationClaims(EnclaveType enclaveType, string attestationToken, EnclavePublicKey enclavePublicKey, byte[] nonce)
		{
			JsonWebToken jsonWebToken;
			try
			{
				JsonWebTokenHandler jsonWebTokenHandler = new JsonWebTokenHandler();
				jsonWebToken = jsonWebTokenHandler.ReadJsonWebToken(attestationToken);
			}
			catch (ArgumentException ex)
			{
				throw SQL.AttestationFailed(string.Format(Strings.FailToParseAttestationToken, ex.Message), null);
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (Claim claim in jsonWebToken.Claims.ToList<Claim>())
			{
				dictionary.Add(claim.Type, claim.Value);
			}
			this.ValidateClaim(dictionary, "aas-ehd", enclavePublicKey.PublicKey);
			if (enclaveType == EnclaveType.Vbs)
			{
				this.ValidateClaim(dictionary, "rp_data", nonce);
			}
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0000D2B4 File Offset: 0x0000B4B4
		private void ValidateClaim(Dictionary<string, string> claims, string claimName, byte[] actualData)
		{
			string text;
			if (!claims.TryGetValue(claimName, out text))
			{
				throw SQL.AttestationFailed(string.Format(Strings.MissingClaimInAttestationToken, claimName), null);
			}
			string text2 = string.Empty;
			try
			{
				text2 = Base64UrlEncoder.Encode(actualData);
			}
			catch (Exception)
			{
				throw SQL.AttestationFailed(Strings.InvalidArgumentToBase64UrlDecoder, null);
			}
			if (!string.Equals(text2, text, StringComparison.Ordinal))
			{
				throw SQL.AttestationFailed(string.Format(Strings.InvalidClaimInAttestationToken, claimName, text), null);
			}
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0000D32C File Offset: 0x0000B52C
		private byte[] GetSharedSecret(EnclavePublicKey enclavePublicKey, byte[] nonce, EnclaveType enclaveType, EnclaveDiffieHellmanInfo enclaveDHInfo, ECDiffieHellman clientDHKey)
		{
			byte[] publicKey = enclavePublicKey.PublicKey;
			if (enclaveType == EnclaveType.Sgx)
			{
				for (int i = 0; i < publicKey.Length; i++)
				{
					publicKey[i] ^= nonce[i % nonce.Length];
				}
			}
			using (RSA rsa = KeyConverter.CreateRSAFromPublicKeyBlob(publicKey))
			{
				if (!rsa.VerifyData(enclaveDHInfo.PublicKey, enclaveDHInfo.PublicKeySignature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1))
				{
					throw new ArgumentException(Strings.GetSharedSecretFailed);
				}
			}
			byte[] array;
			using (ECDiffieHellman ecdiffieHellman = KeyConverter.CreateECDiffieHellmanFromPublicKeyBlob(enclaveDHInfo.PublicKey))
			{
				array = KeyConverter.DeriveKey(clientDHKey, ecdiffieHellman.PublicKey);
			}
			return array;
		}

		// Token: 0x04000076 RID: 118
		private const int DiffieHellmanKeySize = 384;

		// Token: 0x04000077 RID: 119
		private const int AzureBasedAttestationProtocolId = 1;

		// Token: 0x04000078 RID: 120
		private const int SigningKeyRetryInSec = 3;

		// Token: 0x04000079 RID: 121
		private const string AttestationUrlSuffix = "/.well-known/openid-configuration";

		// Token: 0x0400007A RID: 122
		private static readonly MemoryCache OpenIdConnectConfigurationCache = new MemoryCache("OpenIdConnectConfigurationCache", null);

		// Token: 0x020001A6 RID: 422
		internal class AzureAttestationInfo
		{
			// Token: 0x17000A25 RID: 2597
			// (get) Token: 0x06001D68 RID: 7528 RVA: 0x0007983A File Offset: 0x00077A3A
			// (set) Token: 0x06001D69 RID: 7529 RVA: 0x00079842 File Offset: 0x00077A42
			public uint TotalSize { get; set; }

			// Token: 0x17000A26 RID: 2598
			// (get) Token: 0x06001D6A RID: 7530 RVA: 0x0007984B File Offset: 0x00077A4B
			// (set) Token: 0x06001D6B RID: 7531 RVA: 0x00079853 File Offset: 0x00077A53
			public EnclavePublicKey Identity { get; set; }

			// Token: 0x17000A27 RID: 2599
			// (get) Token: 0x06001D6C RID: 7532 RVA: 0x0007985C File Offset: 0x00077A5C
			// (set) Token: 0x06001D6D RID: 7533 RVA: 0x00079864 File Offset: 0x00077A64
			public AzureAttestationEnclaveProvider.AzureAttestationToken AttestationToken { get; set; }

			// Token: 0x17000A28 RID: 2600
			// (get) Token: 0x06001D6E RID: 7534 RVA: 0x0007986D File Offset: 0x00077A6D
			// (set) Token: 0x06001D6F RID: 7535 RVA: 0x00079875 File Offset: 0x00077A75
			public long SessionId { get; set; }

			// Token: 0x17000A29 RID: 2601
			// (get) Token: 0x06001D70 RID: 7536 RVA: 0x0007987E File Offset: 0x00077A7E
			// (set) Token: 0x06001D71 RID: 7537 RVA: 0x00079886 File Offset: 0x00077A86
			public EnclaveType EnclaveType { get; set; }

			// Token: 0x17000A2A RID: 2602
			// (get) Token: 0x06001D72 RID: 7538 RVA: 0x0007988F File Offset: 0x00077A8F
			// (set) Token: 0x06001D73 RID: 7539 RVA: 0x00079897 File Offset: 0x00077A97
			public EnclaveDiffieHellmanInfo EnclaveDHInfo { get; set; }

			// Token: 0x06001D74 RID: 7540 RVA: 0x000798A0 File Offset: 0x00077AA0
			public AzureAttestationInfo(byte[] attestationInfo)
			{
				try
				{
					int num = 0;
					this.TotalSize = BitConverter.ToUInt32(attestationInfo, num);
					num += 4;
					int num2 = BitConverter.ToInt32(attestationInfo, num);
					num += 4;
					int num3 = BitConverter.ToInt32(attestationInfo, num);
					num += 4;
					int num4 = BitConverter.ToInt32(attestationInfo, num);
					this.EnclaveType = (EnclaveType)num4;
					num += 4;
					byte[] array = attestationInfo.Skip(num).Take(num2).ToArray<byte>();
					this.Identity = new EnclavePublicKey(array);
					num += num2;
					byte[] array2 = attestationInfo.Skip(num).Take(num3).ToArray<byte>();
					this.AttestationToken = new AzureAttestationEnclaveProvider.AzureAttestationToken(array2);
					num += num3;
					uint num5 = BitConverter.ToUInt32(attestationInfo, num);
					num += 4;
					this.SessionId = BitConverter.ToInt64(attestationInfo, num);
					num += 8;
					int num6 = Convert.ToInt32(num5) - 4;
					byte[] array3 = attestationInfo.Skip(num).Take(num6).ToArray<byte>();
					this.EnclaveDHInfo = new EnclaveDiffieHellmanInfo(array3);
					num += Convert.ToInt32(this.EnclaveDHInfo.Size);
				}
				catch (Exception ex)
				{
					throw SQL.AttestationFailed(string.Format(Strings.FailToParseAttestationInfo, ex.Message), null);
				}
			}
		}

		// Token: 0x020001A7 RID: 423
		internal class AzureAttestationToken
		{
			// Token: 0x17000A2B RID: 2603
			// (get) Token: 0x06001D75 RID: 7541 RVA: 0x000799C8 File Offset: 0x00077BC8
			// (set) Token: 0x06001D76 RID: 7542 RVA: 0x000799D0 File Offset: 0x00077BD0
			public string AttestationToken { get; set; }

			// Token: 0x06001D77 RID: 7543 RVA: 0x000799DC File Offset: 0x00077BDC
			public AzureAttestationToken(byte[] payload)
			{
				string @string = Encoding.Default.GetString(payload);
				this.AttestationToken = @string.Trim().Trim(new char[] { '"' });
			}
		}
	}
}
