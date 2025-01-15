using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace System.IdentityModel.Tokens.Jwt
{
	// Token: 0x0200000C RID: 12
	public class JwtSecurityTokenHandler : SecurityTokenHandler
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00003A7C File Offset: 0x00001C7C
		public JwtSecurityTokenHandler()
		{
			if (this._mapInboundClaims)
			{
				this._inboundClaimTypeMap = new Dictionary<string, string>(JwtSecurityTokenHandler.DefaultInboundClaimTypeMap);
			}
			else
			{
				this._inboundClaimTypeMap = new Dictionary<string, string>();
			}
			this._outboundClaimTypeMap = new Dictionary<string, string>(JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap);
			this._inboundClaimFilter = new HashSet<string>(JwtSecurityTokenHandler.DefaultInboundClaimFilter);
			this._outboundAlgorithmMap = new Dictionary<string, string>(JwtSecurityTokenHandler.DefaultOutboundAlgorithmMap);
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003AEF File Offset: 0x00001CEF
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00003AF7 File Offset: 0x00001CF7
		public bool MapInboundClaims
		{
			get
			{
				return this._mapInboundClaims;
			}
			set
			{
				if (!this._mapInboundClaims && value && this._inboundClaimTypeMap.Count == 0)
				{
					this._inboundClaimTypeMap = new Dictionary<string, string>(JwtSecurityTokenHandler.DefaultInboundClaimTypeMap);
				}
				this._mapInboundClaims = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003B2A File Offset: 0x00001D2A
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00003B32 File Offset: 0x00001D32
		public IDictionary<string, string> InboundClaimTypeMap
		{
			get
			{
				return this._inboundClaimTypeMap;
			}
			set
			{
				if (value == null)
				{
					throw LogHelper.LogArgumentNullException("value");
				}
				this._inboundClaimTypeMap = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003B4A File Offset: 0x00001D4A
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00003B52 File Offset: 0x00001D52
		public IDictionary<string, string> OutboundClaimTypeMap
		{
			get
			{
				return this._outboundClaimTypeMap;
			}
			set
			{
				if (value == null)
				{
					throw LogHelper.LogArgumentNullException("value");
				}
				this._outboundClaimTypeMap = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003B69 File Offset: 0x00001D69
		public IDictionary<string, string> OutboundAlgorithmMap
		{
			get
			{
				return this._outboundAlgorithmMap;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003B71 File Offset: 0x00001D71
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00003B79 File Offset: 0x00001D79
		public ISet<string> InboundClaimFilter
		{
			get
			{
				return this._inboundClaimFilter;
			}
			set
			{
				if (value == null)
				{
					throw LogHelper.LogArgumentNullException("value");
				}
				this._inboundClaimFilter = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003B90 File Offset: 0x00001D90
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00003B97 File Offset: 0x00001D97
		public static string ShortClaimTypeProperty
		{
			get
			{
				return JwtSecurityTokenHandler._shortClaimType;
			}
			set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw LogHelper.LogArgumentNullException("value");
				}
				JwtSecurityTokenHandler._shortClaimType = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003BB2 File Offset: 0x00001DB2
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00003BB9 File Offset: 0x00001DB9
		public static string JsonClaimTypeProperty
		{
			get
			{
				return JwtSecurityTokenHandler._jsonClaimType;
			}
			set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw LogHelper.LogArgumentNullException("value");
				}
				JwtSecurityTokenHandler._jsonClaimType = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003BD4 File Offset: 0x00001DD4
		public override bool CanValidateToken
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003BD7 File Offset: 0x00001DD7
		public override bool CanWriteToken
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003BDA File Offset: 0x00001DDA
		public override Type TokenType
		{
			get
			{
				return typeof(JwtSecurityToken);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override bool CanReadToken(string token)
		{
			if (string.IsNullOrWhiteSpace(token))
			{
				return false;
			}
			if (token.Length > this.MaximumTokenSizeInBytes)
			{
				LogHelper.LogInformation("IDX10209: Token has length: '{0}' which is larger than the MaximumTokenSizeInBytes: '{1}'.", new object[]
				{
					LogHelper.MarkAsNonPII(token.Length),
					LogHelper.MarkAsNonPII(this.MaximumTokenSizeInBytes)
				});
				return false;
			}
			string[] array = token.Split(new char[] { '.' }, 6);
			if (array.Length == 3)
			{
				return JwtTokenUtilities.RegexJws.IsMatch(token);
			}
			if (array.Length == 5)
			{
				return JwtTokenUtilities.RegexJwe.IsMatch(token);
			}
			LogHelper.LogInformation("IDX12720: Token string does not match the token formats: JWE (header.encryptedKey.iv.ciphertext.tag) or JWS (header.payload.signature)", Array.Empty<object>());
			return false;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003C8C File Offset: 0x00001E8C
		public virtual string CreateEncodedJwt(SecurityTokenDescriptor tokenDescriptor)
		{
			if (tokenDescriptor == null)
			{
				throw LogHelper.LogArgumentNullException("tokenDescriptor");
			}
			return this.CreateJwtSecurityToken(tokenDescriptor).RawData;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003CA8 File Offset: 0x00001EA8
		public virtual string CreateEncodedJwt(string issuer, string audience, ClaimsIdentity subject, DateTime? notBefore, DateTime? expires, DateTime? issuedAt, SigningCredentials signingCredentials)
		{
			return this.CreateJwtSecurityTokenPrivate(issuer, audience, subject, notBefore, expires, issuedAt, signingCredentials, null, null, null, null, null).RawData;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003CD0 File Offset: 0x00001ED0
		public virtual string CreateEncodedJwt(string issuer, string audience, ClaimsIdentity subject, DateTime? notBefore, DateTime? expires, DateTime? issuedAt, SigningCredentials signingCredentials, EncryptingCredentials encryptingCredentials)
		{
			return this.CreateJwtSecurityTokenPrivate(issuer, audience, subject, notBefore, expires, issuedAt, signingCredentials, encryptingCredentials, null, null, null, null).RawData;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003CFC File Offset: 0x00001EFC
		public virtual string CreateEncodedJwt(string issuer, string audience, ClaimsIdentity subject, DateTime? notBefore, DateTime? expires, DateTime? issuedAt, SigningCredentials signingCredentials, EncryptingCredentials encryptingCredentials, IDictionary<string, object> claimCollection)
		{
			return this.CreateJwtSecurityTokenPrivate(issuer, audience, subject, notBefore, expires, issuedAt, signingCredentials, encryptingCredentials, claimCollection, null, null, null).RawData;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003D28 File Offset: 0x00001F28
		public virtual JwtSecurityToken CreateJwtSecurityToken(SecurityTokenDescriptor tokenDescriptor)
		{
			if (tokenDescriptor == null)
			{
				throw LogHelper.LogArgumentNullException("tokenDescriptor");
			}
			return this.CreateJwtSecurityTokenPrivate(tokenDescriptor.Issuer, tokenDescriptor.Audience, tokenDescriptor.Subject, tokenDescriptor.NotBefore, tokenDescriptor.Expires, tokenDescriptor.IssuedAt, tokenDescriptor.SigningCredentials, tokenDescriptor.EncryptingCredentials, tokenDescriptor.Claims, tokenDescriptor.TokenType, tokenDescriptor.AdditionalHeaderClaims, tokenDescriptor.AdditionalInnerHeaderClaims);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003D94 File Offset: 0x00001F94
		public virtual JwtSecurityToken CreateJwtSecurityToken(string issuer, string audience, ClaimsIdentity subject, DateTime? notBefore, DateTime? expires, DateTime? issuedAt, SigningCredentials signingCredentials, EncryptingCredentials encryptingCredentials)
		{
			return this.CreateJwtSecurityTokenPrivate(issuer, audience, subject, notBefore, expires, issuedAt, signingCredentials, encryptingCredentials, null, null, null, null);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003DB8 File Offset: 0x00001FB8
		public virtual JwtSecurityToken CreateJwtSecurityToken(string issuer, string audience, ClaimsIdentity subject, DateTime? notBefore, DateTime? expires, DateTime? issuedAt, SigningCredentials signingCredentials, EncryptingCredentials encryptingCredentials, IDictionary<string, object> claimCollection)
		{
			return this.CreateJwtSecurityTokenPrivate(issuer, audience, subject, notBefore, expires, issuedAt, signingCredentials, encryptingCredentials, claimCollection, null, null, null);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003DE0 File Offset: 0x00001FE0
		public virtual JwtSecurityToken CreateJwtSecurityToken(string issuer = null, string audience = null, ClaimsIdentity subject = null, DateTime? notBefore = null, DateTime? expires = null, DateTime? issuedAt = null, SigningCredentials signingCredentials = null)
		{
			return this.CreateJwtSecurityTokenPrivate(issuer, audience, subject, notBefore, expires, issuedAt, signingCredentials, null, null, null, null, null);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003E04 File Offset: 0x00002004
		public override SecurityToken CreateToken(SecurityTokenDescriptor tokenDescriptor)
		{
			if (tokenDescriptor == null)
			{
				throw LogHelper.LogArgumentNullException("tokenDescriptor");
			}
			return this.CreateJwtSecurityTokenPrivate(tokenDescriptor.Issuer, tokenDescriptor.Audience, tokenDescriptor.Subject, tokenDescriptor.NotBefore, tokenDescriptor.Expires, tokenDescriptor.IssuedAt, tokenDescriptor.SigningCredentials, tokenDescriptor.EncryptingCredentials, tokenDescriptor.Claims, tokenDescriptor.TokenType, tokenDescriptor.AdditionalHeaderClaims, tokenDescriptor.AdditionalInnerHeaderClaims);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003E70 File Offset: 0x00002070
		private JwtSecurityToken CreateJwtSecurityTokenPrivate(string issuer, string audience, ClaimsIdentity subject, DateTime? notBefore, DateTime? expires, DateTime? issuedAt, SigningCredentials signingCredentials, EncryptingCredentials encryptingCredentials, IDictionary<string, object> claimCollection, string tokenType, IDictionary<string, object> additionalHeaderClaims, IDictionary<string, object> additionalInnerHeaderClaims)
		{
			if (base.SetDefaultTimesOnTokenCreation && (expires == null || issuedAt == null || notBefore == null))
			{
				DateTime utcNow = DateTime.UtcNow;
				if (expires == null)
				{
					expires = new DateTime?(utcNow + TimeSpan.FromMinutes((double)base.TokenLifetimeInMinutes));
				}
				if (issuedAt == null)
				{
					issuedAt = new DateTime?(utcNow);
				}
				if (notBefore == null)
				{
					notBefore = new DateTime?(utcNow);
				}
			}
			LogHelper.LogVerbose("IDX12721: Creating JwtSecurityToken: Issuer: '{0}', Audience: '{1}'", new object[]
			{
				LogHelper.MarkAsNonPII(issuer ?? "null"),
				LogHelper.MarkAsNonPII(audience ?? "null")
			});
			JwtPayload jwtPayload = new JwtPayload(issuer, audience, (subject == null) ? null : this.OutboundClaimTypeTransform(subject.Claims), (claimCollection == null) ? null : this.OutboundClaimTypeTransform(claimCollection), notBefore, expires, issuedAt);
			JwtHeader jwtHeader = new JwtHeader(signingCredentials, this.OutboundAlgorithmMap, tokenType, additionalInnerHeaderClaims);
			if (((subject != null) ? subject.Actor : null) != null)
			{
				jwtPayload.AddClaim(new Claim("actort", this.CreateActorValue(subject.Actor)));
			}
			string text = jwtHeader.Base64UrlEncode();
			string text2 = jwtPayload.Base64UrlEncode();
			string text3 = jwtHeader.Base64UrlEncode() + "." + jwtPayload.Base64UrlEncode();
			string text4 = ((signingCredentials == null) ? string.Empty : JwtTokenUtilities.CreateEncodedSignature(text3, signingCredentials));
			LogHelper.LogInformation("IDX12722: Creating security token from the header: '{0}', payload: '{1}'.", new object[] { text, text2 });
			if (encryptingCredentials != null)
			{
				return this.EncryptToken(new JwtSecurityToken(jwtHeader, jwtPayload, text, text2, text4), encryptingCredentials, tokenType, additionalHeaderClaims);
			}
			return new JwtSecurityToken(jwtHeader, jwtPayload, text, text2, text4);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004008 File Offset: 0x00002208
		private JwtSecurityToken EncryptToken(JwtSecurityToken innerJwt, EncryptingCredentials encryptingCredentials, string tokenType, IDictionary<string, object> additionalHeaderClaims)
		{
			if (encryptingCredentials == null)
			{
				throw LogHelper.LogArgumentNullException("encryptingCredentials");
			}
			CryptoProviderFactory cryptoProviderFactory = encryptingCredentials.CryptoProviderFactory ?? encryptingCredentials.Key.CryptoProviderFactory;
			if (cryptoProviderFactory == null)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException("IDX10620: Unable to obtain a CryptoProviderFactory, both EncryptingCredentials.CryptoProviderFactory and EncryptingCredentials.Key.CrypoProviderFactory are null."));
			}
			byte[] array;
			SecurityKey securityKey = JwtTokenUtilities.GetSecurityKey(encryptingCredentials, cryptoProviderFactory, additionalHeaderClaims, out array);
			JwtSecurityToken jwtSecurityToken;
			using (AuthenticatedEncryptionProvider authenticatedEncryptionProvider = cryptoProviderFactory.CreateAuthenticatedEncryptionProvider(securityKey, encryptingCredentials.Enc))
			{
				if (authenticatedEncryptionProvider == null)
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenEncryptionFailedException("IDX12730: Failed to create the token encryption provider."));
				}
				try
				{
					JwtHeader jwtHeader = new JwtHeader(encryptingCredentials, this.OutboundAlgorithmMap, tokenType, additionalHeaderClaims);
					AuthenticatedEncryptionResult authenticatedEncryptionResult = authenticatedEncryptionProvider.Encrypt(Encoding.UTF8.GetBytes(innerJwt.RawData), Encoding.ASCII.GetBytes(jwtHeader.Base64UrlEncode()));
					jwtSecurityToken = ("dir".Equals(encryptingCredentials.Alg) ? new JwtSecurityToken(jwtHeader, innerJwt, jwtHeader.Base64UrlEncode(), string.Empty, Base64UrlEncoder.Encode(authenticatedEncryptionResult.IV), Base64UrlEncoder.Encode(authenticatedEncryptionResult.Ciphertext), Base64UrlEncoder.Encode(authenticatedEncryptionResult.AuthenticationTag)) : new JwtSecurityToken(jwtHeader, innerJwt, jwtHeader.Base64UrlEncode(), Base64UrlEncoder.Encode(array), Base64UrlEncoder.Encode(authenticatedEncryptionResult.IV), Base64UrlEncoder.Encode(authenticatedEncryptionResult.Ciphertext), Base64UrlEncoder.Encode(authenticatedEncryptionResult.AuthenticationTag)));
				}
				catch (Exception ex)
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenEncryptionFailedException(LogHelper.FormatInvariant("IDX10616: Encryption failed. EncryptionProvider failed for: Algorithm: '{0}', SecurityKey: '{1}'. See inner exception.", new object[]
					{
						LogHelper.MarkAsNonPII(encryptingCredentials.Enc),
						encryptingCredentials.Key
					}), ex));
				}
			}
			return jwtSecurityToken;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000041B4 File Offset: 0x000023B4
		private IEnumerable<Claim> OutboundClaimTypeTransform(IEnumerable<Claim> claims)
		{
			foreach (Claim claim in claims)
			{
				string text = null;
				if (this._outboundClaimTypeMap.TryGetValue(claim.Type, out text))
				{
					yield return new Claim(text, claim.Value, claim.ValueType, claim.Issuer, claim.OriginalIssuer, claim.Subject);
				}
				else
				{
					yield return claim;
				}
			}
			IEnumerator<Claim> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000041CC File Offset: 0x000023CC
		private IDictionary<string, object> OutboundClaimTypeTransform(IDictionary<string, object> claimCollection)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (string text in claimCollection.Keys)
			{
				string text2;
				if (this._outboundClaimTypeMap.TryGetValue(text, out text2))
				{
					dictionary[text2] = claimCollection[text];
				}
				else
				{
					dictionary[text] = claimCollection[text];
				}
			}
			return dictionary;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004248 File Offset: 0x00002448
		public JwtSecurityToken ReadJwtToken(string token)
		{
			if (string.IsNullOrEmpty(token))
			{
				throw LogHelper.LogArgumentNullException("token");
			}
			if (token.Length > this.MaximumTokenSizeInBytes)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10209: Token has length: '{0}' which is larger than the MaximumTokenSizeInBytes: '{1}'.", new object[]
				{
					LogHelper.MarkAsNonPII(token.Length),
					LogHelper.MarkAsNonPII(this.MaximumTokenSizeInBytes)
				})));
			}
			if (!this.CanReadToken(token))
			{
				throw LogHelper.LogExceptionMessage(new SecurityTokenMalformedException("IDX12709: CanReadToken() returned false. JWT is not well formed.\nThe token needs to be in JWS or JWE Compact Serialization Format. (JWS): 'EncodedHeader.EndcodedPayload.EncodedSignature'. (JWE): 'EncodedProtectedHeader.EncodedEncryptedKey.EncodedInitializationVector.EncodedCiphertext.EncodedAuthenticationTag'."));
			}
			JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(null, null, null, null, null, null);
			jwtSecurityToken.Decode(token.Split(new char[] { '.' }), token);
			return jwtSecurityToken;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004305 File Offset: 0x00002505
		public override SecurityToken ReadToken(string token)
		{
			return this.ReadJwtToken(token);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000430E File Offset: 0x0000250E
		public override SecurityToken ReadToken(XmlReader reader, TokenValidationParameters validationParameters)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004318 File Offset: 0x00002518
		public override ClaimsPrincipal ValidateToken(string token, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
		{
			if (string.IsNullOrWhiteSpace(token))
			{
				throw LogHelper.LogArgumentNullException("token");
			}
			if (validationParameters == null)
			{
				throw LogHelper.LogArgumentNullException("validationParameters");
			}
			if (token.Length > this.MaximumTokenSizeInBytes)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10209: Token has length: '{0}' which is larger than the MaximumTokenSizeInBytes: '{1}'.", new object[]
				{
					LogHelper.MarkAsNonPII(token.Length),
					LogHelper.MarkAsNonPII(this.MaximumTokenSizeInBytes)
				})));
			}
			string[] array = token.Split(new char[] { '.' }, 6);
			if (array.Length != 3 && array.Length != 5)
			{
				throw LogHelper.LogExceptionMessage(new SecurityTokenMalformedException("IDX12741: JWT must have three segments (JWS) or five segments (JWE)."));
			}
			if (array.Length == 5)
			{
				JwtSecurityToken jwtSecurityToken = this.ReadJwtToken(token);
				string text = this.DecryptToken(jwtSecurityToken, validationParameters);
				return this.ValidateToken(text, jwtSecurityToken, validationParameters, out validatedToken);
			}
			return this.ValidateToken(token, null, validationParameters, out validatedToken);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000043F0 File Offset: 0x000025F0
		private ClaimsPrincipal ValidateToken(string token, JwtSecurityToken outerToken, TokenValidationParameters validationParameters, out SecurityToken signatureValidatedToken)
		{
			BaseConfiguration baseConfiguration = null;
			if (validationParameters.ConfigurationManager != null)
			{
				try
				{
					baseConfiguration = validationParameters.ConfigurationManager.GetBaseConfigurationAsync(CancellationToken.None).ConfigureAwait(false).GetAwaiter()
						.GetResult();
				}
				catch (Exception ex)
				{
					LogHelper.LogWarning(LogHelper.FormatInvariant("IDX10261: Unable to retrieve configuration from authority: '{0}'. \nProceeding with token validation in case the relevant properties have been set manually on the TokenValidationParameters. Exception caught: \n {1}. See https://aka.ms/validate-using-configuration-manager for additional information.", new object[]
					{
						LogHelper.MarkAsNonPII(validationParameters.ConfigurationManager.MetadataAddress),
						ex.ToString()
					}), Array.Empty<object>());
				}
			}
			ExceptionDispatchInfo exceptionDispatchInfo;
			ClaimsPrincipal claimsPrincipal = ((outerToken != null) ? this.ValidateJWE(token, outerToken, validationParameters, baseConfiguration, out signatureValidatedToken, out exceptionDispatchInfo) : this.ValidateJWS(token, validationParameters, baseConfiguration, out signatureValidatedToken, out exceptionDispatchInfo));
			if (validationParameters.ConfigurationManager != null)
			{
				if (claimsPrincipal != null)
				{
					if (baseConfiguration != null)
					{
						validationParameters.ConfigurationManager.LastKnownGoodConfiguration = baseConfiguration;
					}
					return claimsPrincipal;
				}
				if (TokenUtilities.IsRecoverableException(exceptionDispatchInfo.SourceException))
				{
					if (baseConfiguration != null)
					{
						validationParameters.ConfigurationManager.RequestRefresh();
						validationParameters.RefreshBeforeValidation = true;
						BaseConfiguration baseConfiguration2 = baseConfiguration;
						baseConfiguration = validationParameters.ConfigurationManager.GetBaseConfigurationAsync(CancellationToken.None).GetAwaiter().GetResult();
						if (baseConfiguration2 != baseConfiguration)
						{
							claimsPrincipal = ((outerToken != null) ? this.ValidateJWE(token, outerToken, validationParameters, baseConfiguration, out signatureValidatedToken, out exceptionDispatchInfo) : this.ValidateJWS(token, validationParameters, baseConfiguration, out signatureValidatedToken, out exceptionDispatchInfo));
							if (claimsPrincipal != null)
							{
								validationParameters.ConfigurationManager.LastKnownGoodConfiguration = baseConfiguration;
								return claimsPrincipal;
							}
						}
					}
					if (validationParameters.ConfigurationManager.UseLastKnownGoodConfiguration)
					{
						validationParameters.RefreshBeforeValidation = false;
						validationParameters.ValidateWithLKG = true;
						Exception sourceException = exceptionDispatchInfo.SourceException;
						string text = ((outerToken != null) ? outerToken.Header.Kid : (JwtSecurityTokenHandler.ValidateSignatureUsingDelegates(token, validationParameters, null) ?? this.GetJwtSecurityTokenFromToken(token, validationParameters)).Header.Kid);
						foreach (BaseConfiguration baseConfiguration3 in validationParameters.ConfigurationManager.GetValidLkgConfigurations())
						{
							if (!baseConfiguration3.Equals(baseConfiguration) && TokenUtilities.IsRecoverableConfiguration(text, baseConfiguration, baseConfiguration3, sourceException))
							{
								claimsPrincipal = ((outerToken != null) ? this.ValidateJWE(token, outerToken, validationParameters, baseConfiguration3, out signatureValidatedToken, out exceptionDispatchInfo) : this.ValidateJWS(token, validationParameters, baseConfiguration3, out signatureValidatedToken, out exceptionDispatchInfo));
								if (claimsPrincipal != null)
								{
									return claimsPrincipal;
								}
							}
						}
					}
				}
			}
			if (claimsPrincipal != null)
			{
				return claimsPrincipal;
			}
			exceptionDispatchInfo.Throw();
			return null;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004620 File Offset: 0x00002820
		private ClaimsPrincipal ValidateJWE(string decryptedJwt, JwtSecurityToken outerToken, TokenValidationParameters validationParameters, BaseConfiguration currentConfiguration, out SecurityToken signatureValidatedToken, out ExceptionDispatchInfo exceptionThrown)
		{
			exceptionThrown = null;
			ClaimsPrincipal claimsPrincipal2;
			try
			{
				SecurityToken securityToken;
				ClaimsPrincipal claimsPrincipal = this.ValidateJWS(decryptedJwt, validationParameters, currentConfiguration, out securityToken, out exceptionThrown);
				outerToken.InnerToken = securityToken as JwtSecurityToken;
				signatureValidatedToken = ((exceptionThrown == null) ? outerToken : null);
				claimsPrincipal2 = claimsPrincipal;
			}
			catch (Exception ex)
			{
				exceptionThrown = ExceptionDispatchInfo.Capture(ex);
				signatureValidatedToken = null;
				claimsPrincipal2 = null;
			}
			return claimsPrincipal2;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004680 File Offset: 0x00002880
		private ClaimsPrincipal ValidateJWS(string token, TokenValidationParameters validationParameters, BaseConfiguration currentConfiguration, out SecurityToken signatureValidatedToken, out ExceptionDispatchInfo exceptionThrown)
		{
			exceptionThrown = null;
			ClaimsPrincipal claimsPrincipal2;
			try
			{
				ClaimsPrincipal claimsPrincipal;
				if (validationParameters.SignatureValidator != null || validationParameters.SignatureValidatorUsingConfiguration != null)
				{
					signatureValidatedToken = JwtSecurityTokenHandler.ValidateSignatureUsingDelegates(token, validationParameters, currentConfiguration);
					claimsPrincipal = this.ValidateTokenPayload(signatureValidatedToken as JwtSecurityToken, validationParameters, currentConfiguration);
					if (currentConfiguration == null)
					{
						this.ValidateIssuerSecurityKey(signatureValidatedToken.SigningKey, signatureValidatedToken as JwtSecurityToken, validationParameters);
					}
					else
					{
						Validators.ValidateIssuerSecurityKey(signatureValidatedToken.SigningKey, signatureValidatedToken, validationParameters, currentConfiguration);
					}
				}
				else
				{
					JwtSecurityToken jwtSecurityToken = this.GetJwtSecurityTokenFromToken(token, validationParameters);
					if (validationParameters.ValidateSignatureLast)
					{
						claimsPrincipal = this.ValidateTokenPayload(jwtSecurityToken, validationParameters, currentConfiguration);
						jwtSecurityToken = this.ValidateSignatureAndIssuerSecurityKey(token, jwtSecurityToken, validationParameters, currentConfiguration);
						signatureValidatedToken = jwtSecurityToken;
					}
					else
					{
						signatureValidatedToken = this.ValidateSignatureAndIssuerSecurityKey(token, jwtSecurityToken, validationParameters, currentConfiguration);
						claimsPrincipal = this.ValidateTokenPayload(signatureValidatedToken as JwtSecurityToken, validationParameters, currentConfiguration);
					}
				}
				claimsPrincipal2 = claimsPrincipal;
			}
			catch (Exception ex)
			{
				exceptionThrown = ExceptionDispatchInfo.Capture(ex);
				signatureValidatedToken = null;
				claimsPrincipal2 = null;
			}
			return claimsPrincipal2;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004760 File Offset: 0x00002960
		private static JwtSecurityToken ValidateSignatureUsingDelegates(string token, TokenValidationParameters validationParameters, BaseConfiguration configuration)
		{
			if (validationParameters == null)
			{
				throw LogHelper.LogArgumentNullException("validationParameters");
			}
			if (validationParameters.SignatureValidatorUsingConfiguration != null)
			{
				SecurityToken securityToken = validationParameters.SignatureValidatorUsingConfiguration(token, validationParameters, configuration);
				if (securityToken == null)
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidSignatureException(LogHelper.FormatInvariant("IDX10505: Signature validation failed. The user defined 'Delegate' specified on TokenValidationParameters returned null when validating token: '{0}'.", new object[] { LogHelper.MarkAsSecurityArtifact(token, new Func<object, string>(JwtTokenUtilities.SafeLogJwtToken)) })));
				}
				JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
				if (jwtSecurityToken == null)
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidSignatureException(LogHelper.FormatInvariant("IDX10506: Signature validation failed. The user defined 'Delegate' specified on TokenValidationParameters did not return a '{0}', but returned a '{1}' when validating token: '{2}'.", new object[]
					{
						LogHelper.MarkAsNonPII(typeof(JwtSecurityToken)),
						LogHelper.MarkAsNonPII(securityToken.GetType()),
						LogHelper.MarkAsSecurityArtifact(token, new Func<object, string>(JwtTokenUtilities.SafeLogJwtToken))
					})));
				}
				return jwtSecurityToken;
			}
			else
			{
				if (validationParameters.SignatureValidator == null)
				{
					return null;
				}
				SecurityToken securityToken2 = validationParameters.SignatureValidator(token, validationParameters);
				if (securityToken2 == null)
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidSignatureException(LogHelper.FormatInvariant("IDX10505: Signature validation failed. The user defined 'Delegate' specified on TokenValidationParameters returned null when validating token: '{0}'.", new object[] { LogHelper.MarkAsSecurityArtifact(token, new Func<object, string>(JwtTokenUtilities.SafeLogJwtToken)) })));
				}
				JwtSecurityToken jwtSecurityToken2 = securityToken2 as JwtSecurityToken;
				if (jwtSecurityToken2 == null)
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidSignatureException(LogHelper.FormatInvariant("IDX10506: Signature validation failed. The user defined 'Delegate' specified on TokenValidationParameters did not return a '{0}', but returned a '{1}' when validating token: '{2}'.", new object[]
					{
						LogHelper.MarkAsNonPII(typeof(JwtSecurityToken)),
						LogHelper.MarkAsNonPII(securityToken2.GetType()),
						LogHelper.MarkAsSecurityArtifact(token, new Func<object, string>(JwtTokenUtilities.SafeLogJwtToken))
					})));
				}
				return jwtSecurityToken2;
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000048CD File Offset: 0x00002ACD
		private JwtSecurityToken ValidateSignatureAndIssuerSecurityKey(string token, JwtSecurityToken jwtToken, TokenValidationParameters validationParameters, BaseConfiguration configuration)
		{
			JwtSecurityToken jwtSecurityToken = this.ValidateSignature(token, jwtToken, validationParameters, configuration);
			if (configuration == null)
			{
				this.ValidateIssuerSecurityKey(jwtToken.SigningKey, jwtToken, validationParameters);
				return jwtSecurityToken;
			}
			Validators.ValidateIssuerSecurityKey(jwtToken.SigningKey, jwtToken, validationParameters, configuration);
			return jwtSecurityToken;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000048FC File Offset: 0x00002AFC
		private JwtSecurityToken GetJwtSecurityTokenFromToken(string token, TokenValidationParameters validationParameters)
		{
			if (validationParameters.TokenReader == null)
			{
				return this.ReadJwtToken(token);
			}
			SecurityToken securityToken = validationParameters.TokenReader(token, validationParameters);
			if (securityToken == null)
			{
				throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidSignatureException(LogHelper.FormatInvariant("IDX10510: Token validation failed. The user defined 'Delegate' set on TokenValidationParameters.TokenReader returned null when reading token: '{0}'.", new object[] { LogHelper.MarkAsSecurityArtifact(token, new Func<object, string>(JwtTokenUtilities.SafeLogJwtToken)) })));
			}
			JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
			if (jwtSecurityToken == null)
			{
				throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidSignatureException(LogHelper.FormatInvariant("IDX10509: Token validation failed. The user defined 'Delegate' set on TokenValidationParameters.TokenReader did not return a '{0}', but returned a '{1}' when reading token: '{2}'.", new object[]
				{
					LogHelper.MarkAsNonPII(typeof(JsonWebToken)),
					LogHelper.MarkAsNonPII(securityToken.GetType()),
					LogHelper.MarkAsSecurityArtifact(token, new Func<object, string>(JwtTokenUtilities.SafeLogJwtToken))
				})));
			}
			return jwtSecurityToken;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000049B8 File Offset: 0x00002BB8
		protected ClaimsPrincipal ValidateTokenPayload(JwtSecurityToken jwtToken, TokenValidationParameters validationParameters)
		{
			return this.ValidateTokenPayload(jwtToken, validationParameters, null);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000049C4 File Offset: 0x00002BC4
		private ClaimsPrincipal ValidateTokenPayload(JwtSecurityToken jwtToken, TokenValidationParameters validationParameters, BaseConfiguration configuration)
		{
			if (jwtToken == null)
			{
				throw LogHelper.LogArgumentNullException("jwtToken");
			}
			if (validationParameters == null)
			{
				throw LogHelper.LogArgumentNullException("validationParameters");
			}
			DateTime? dateTime = ((jwtToken.Payload.Exp == null) ? null : new DateTime?(jwtToken.ValidTo));
			DateTime? dateTime2 = ((jwtToken.Payload.Nbf == null) ? null : new DateTime?(jwtToken.ValidFrom));
			this.ValidateLifetime(dateTime2, dateTime, jwtToken, validationParameters);
			this.ValidateAudience(jwtToken.Audiences, jwtToken, validationParameters);
			string text = ((configuration == null) ? this.ValidateIssuer(jwtToken.Issuer, jwtToken, validationParameters) : Validators.ValidateIssuer(jwtToken.Issuer, jwtToken, validationParameters, configuration));
			this.ValidateTokenReplay(dateTime, jwtToken.RawData, validationParameters);
			if (validationParameters.ValidateActor && !string.IsNullOrWhiteSpace(jwtToken.Actor))
			{
				SecurityToken securityToken;
				this.ValidateToken(jwtToken.Actor, validationParameters.ActorValidationParameters ?? validationParameters, out securityToken);
			}
			Validators.ValidateTokenType(jwtToken.Header.Typ, jwtToken, validationParameters);
			ClaimsIdentity claimsIdentity = this.CreateClaimsIdentity(jwtToken, text, validationParameters);
			if (validationParameters.SaveSigninToken)
			{
				claimsIdentity.BootstrapContext = jwtToken.RawData;
			}
			LogHelper.LogInformation("IDX10241: Security token validated. token: '{0}'.", new object[] { jwtToken });
			return new ClaimsPrincipal(claimsIdentity);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004B0C File Offset: 0x00002D0C
		private ClaimsPrincipal CreateClaimsPrincipalFromToken(JwtSecurityToken jwtToken, string issuer, TokenValidationParameters validationParameters)
		{
			ClaimsIdentity claimsIdentity = this.CreateClaimsIdentity(jwtToken, issuer, validationParameters);
			if (validationParameters.SaveSigninToken)
			{
				claimsIdentity.BootstrapContext = jwtToken.RawData;
			}
			LogHelper.LogInformation("IDX10241: Security token validated. token: '{0}'.", new object[] { jwtToken });
			return new ClaimsPrincipal(claimsIdentity);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004B54 File Offset: 0x00002D54
		public override string WriteToken(SecurityToken token)
		{
			if (token == null)
			{
				throw LogHelper.LogArgumentNullException("token");
			}
			JwtSecurityToken jwtSecurityToken = token as JwtSecurityToken;
			if (jwtSecurityToken == null)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX12706: '{0}' can only write SecurityTokens of type: '{1}', 'token' type is: '{2}'.", new object[]
				{
					LogHelper.MarkAsNonPII("System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler"),
					LogHelper.MarkAsNonPII(typeof(JwtSecurityToken)),
					LogHelper.MarkAsNonPII(token.GetType())
				}), "token"));
			}
			string encodedPayload = jwtSecurityToken.EncodedPayload;
			string text = string.Empty;
			string text2 = string.Empty;
			if (jwtSecurityToken.InnerToken != null)
			{
				if (jwtSecurityToken.SigningCredentials != null)
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenEncryptionFailedException("IDX12736: JwtSecurityToken.SigningCredentials is not supported when JwtSecurityToken.InnerToken is set."));
				}
				if (jwtSecurityToken.InnerToken.Header.EncryptingCredentials != null)
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenEncryptionFailedException("IDX12737: EncryptingCredentials set on JwtSecurityToken.InnerToken is not supported."));
				}
				if (jwtSecurityToken.Header.EncryptingCredentials == null)
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenEncryptionFailedException("IDX12735: If JwtSecurityToken.InnerToken != null, then JwtSecurityToken.Header.EncryptingCredentials must be set."));
				}
				if (jwtSecurityToken.InnerToken.SigningCredentials != null)
				{
					text = JwtTokenUtilities.CreateEncodedSignature(jwtSecurityToken.InnerToken.EncodedHeader + "." + jwtSecurityToken.EncodedPayload, jwtSecurityToken.InnerToken.SigningCredentials);
				}
				return this.EncryptToken(new JwtSecurityToken(jwtSecurityToken.InnerToken.Header, jwtSecurityToken.InnerToken.Payload, jwtSecurityToken.InnerToken.EncodedHeader, encodedPayload, text), jwtSecurityToken.EncryptingCredentials, jwtSecurityToken.InnerToken.Header.Typ, null).RawData;
			}
			else
			{
				JwtHeader jwtHeader = ((jwtSecurityToken.EncryptingCredentials == null) ? jwtSecurityToken.Header : new JwtHeader(jwtSecurityToken.SigningCredentials));
				text2 = jwtHeader.Base64UrlEncode();
				if (jwtSecurityToken.SigningCredentials != null)
				{
					text = JwtTokenUtilities.CreateEncodedSignature(text2 + "." + encodedPayload, jwtSecurityToken.SigningCredentials);
				}
				if (jwtSecurityToken.EncryptingCredentials != null)
				{
					return this.EncryptToken(new JwtSecurityToken(jwtHeader, jwtSecurityToken.Payload, text2, encodedPayload, text), jwtSecurityToken.EncryptingCredentials, jwtSecurityToken.Header.Typ, null).RawData;
				}
				return string.Concat(new string[] { text2, ".", encodedPayload, ".", text });
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004D64 File Offset: 0x00002F64
		private static bool ValidateSignature(byte[] encodedBytes, byte[] signature, SecurityKey key, string algorithm, SecurityToken securityToken, TokenValidationParameters validationParameters)
		{
			Validators.ValidateAlgorithm(algorithm, key, securityToken, validationParameters);
			CryptoProviderFactory cryptoProviderFactory = validationParameters.CryptoProviderFactory ?? key.CryptoProviderFactory;
			SignatureProvider signatureProvider = cryptoProviderFactory.CreateForVerifying(key, algorithm);
			if (signatureProvider == null)
			{
				throw LogHelper.LogExceptionMessage(new InvalidOperationException(LogHelper.FormatInvariant("IDX10636: CryptoProviderFactory.CreateForVerifying returned null for key: '{0}', signatureAlgorithm: '{1}'.", new object[]
				{
					(key == null) ? "Null" : key.ToString(),
					LogHelper.MarkAsNonPII(algorithm)
				})));
			}
			bool flag;
			try
			{
				flag = signatureProvider.Verify(encodedBytes, signature);
			}
			finally
			{
				cryptoProviderFactory.ReleaseSignatureProvider(signatureProvider);
			}
			return flag;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004DF8 File Offset: 0x00002FF8
		protected virtual JwtSecurityToken ValidateSignature(string token, TokenValidationParameters validationParameters)
		{
			JwtSecurityToken jwtSecurityToken = JwtSecurityTokenHandler.ValidateSignatureUsingDelegates(token, validationParameters, null);
			JwtSecurityToken jwtSecurityTokenFromToken = this.GetJwtSecurityTokenFromToken(token, validationParameters);
			return this.ValidateSignature(token, jwtSecurityToken ?? jwtSecurityTokenFromToken, validationParameters, null);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004E28 File Offset: 0x00003028
		private JwtSecurityToken ValidateSignature(string token, JwtSecurityToken jwtToken, TokenValidationParameters validationParameters, BaseConfiguration configuration)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(jwtToken.RawHeader + "." + jwtToken.RawPayload);
			bool flag = false;
			IEnumerable<SecurityKey> enumerable = null;
			if (string.IsNullOrEmpty(jwtToken.RawSignature))
			{
				if (validationParameters.RequireSignedTokens)
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidSignatureException(LogHelper.FormatInvariant("IDX10504: Unable to validate signature, token does not have a signature: '{0}'.", new object[] { jwtToken })));
				}
				return jwtToken;
			}
			else
			{
				if (validationParameters.IssuerSigningKeyResolverUsingConfiguration != null)
				{
					enumerable = validationParameters.IssuerSigningKeyResolverUsingConfiguration(token, jwtToken, jwtToken.Header.Kid, validationParameters, configuration);
				}
				if (validationParameters.IssuerSigningKeyResolver != null)
				{
					enumerable = validationParameters.IssuerSigningKeyResolver(token, jwtToken, jwtToken.Header.Kid, validationParameters);
				}
				else
				{
					SecurityKey securityKey = ((configuration == null) ? this.ResolveIssuerSigningKey(token, jwtToken, validationParameters) : JwtTokenUtilities.ResolveTokenSigningKey(jwtToken.Header.Kid, jwtToken.Header.X5t, validationParameters, configuration));
					if (securityKey != null)
					{
						flag = true;
						enumerable = new List<SecurityKey> { securityKey };
					}
				}
				if (enumerable == null && validationParameters.TryAllIssuerSigningKeys)
				{
					enumerable = TokenUtilities.GetAllSigningKeys(validationParameters, configuration);
				}
				StringBuilder stringBuilder = new StringBuilder();
				StringBuilder stringBuilder2 = new StringBuilder();
				bool flag2 = !string.IsNullOrEmpty(jwtToken.Header.Kid);
				byte[] array;
				try
				{
					array = Base64UrlEncoder.DecodeBytes(jwtToken.RawSignature);
				}
				catch (FormatException ex)
				{
					throw new SecurityTokenInvalidSignatureException("IDX10508: Signature validation failed. Signature is improperly formatted.", ex);
				}
				if (enumerable != null)
				{
					foreach (SecurityKey securityKey2 in enumerable)
					{
						try
						{
							if (JwtSecurityTokenHandler.ValidateSignature(bytes, array, securityKey2, jwtToken.Header.Alg, jwtToken, validationParameters))
							{
								LogHelper.LogInformation("IDX10242: Security token: '{0}' has a valid signature.", new object[] { jwtToken });
								jwtToken.SigningKey = securityKey2;
								return jwtToken;
							}
						}
						catch (Exception ex2)
						{
							stringBuilder.AppendLine(ex2.ToString());
						}
						if (securityKey2 != null)
						{
							stringBuilder2.Append(securityKey2.ToString()).Append(" , KeyId: ").AppendLine(securityKey2.KeyId);
							if (flag2 && !flag && securityKey2.KeyId != null)
							{
								flag = jwtToken.Header.Kid.Equals(securityKey2.KeyId, (securityKey2 is X509SecurityKey) ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
							}
						}
					}
				}
				IEnumerable<SecurityKey> allSigningKeys = TokenUtilities.GetAllSigningKeys(validationParameters);
				IEnumerable<SecurityKey> allSigningKeys2 = TokenUtilities.GetAllSigningKeys(configuration);
				int num = allSigningKeys.Count<SecurityKey>();
				int num2 = allSigningKeys2.Count<SecurityKey>();
				if (flag2)
				{
					if (flag)
					{
						string text = (allSigningKeys.Any((SecurityKey x) => x.KeyId.Equals(jwtToken.Header.Kid)) ? "TokenValidationParameters" : "Configuration");
						throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidSignatureException(LogHelper.FormatInvariant("IDX10511: Signature validation failed. Keys tried: '{0}'. \nNumber of keys in TokenValidationParameters: '{1}'. \nNumber of keys in Configuration: '{2}'. \nMatched key was in '{3}'. \nkid: '{4}'. \nExceptions caught:\n '{5}'.\ntoken: '{6}'. See https://aka.ms/IDX10511 for details.", new object[]
						{
							stringBuilder2,
							LogHelper.MarkAsNonPII(num),
							LogHelper.MarkAsNonPII(num2),
							LogHelper.MarkAsNonPII(text),
							LogHelper.MarkAsNonPII(jwtToken.Header.Kid),
							stringBuilder,
							jwtToken
						})));
					}
					DateTime? dateTime = ((jwtToken.Payload.Exp == null) ? null : new DateTime?(jwtToken.ValidTo));
					DateTime? dateTime2 = ((jwtToken.Payload.Nbf == null) ? null : new DateTime?(jwtToken.ValidFrom));
					if (!validationParameters.ValidateSignatureLast)
					{
						InternalValidators.ValidateAfterSignatureFailed(jwtToken, dateTime2, dateTime, jwtToken.Audiences, validationParameters, configuration);
					}
				}
				if (stringBuilder2.Length > 0)
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenSignatureKeyNotFoundException(LogHelper.FormatInvariant("IDX10503: Signature validation failed. Token does not have a kid. Keys tried: '{0}'. Number of keys in TokenValidationParameters: '{1}'. \nNumber of keys in Configuration: '{2}'. \nExceptions caught:\n '{3}'.\ntoken: '{4}'. See https://aka.ms/IDX10503 for details.", new object[]
					{
						stringBuilder2,
						LogHelper.MarkAsNonPII(num),
						LogHelper.MarkAsNonPII(num2),
						stringBuilder,
						jwtToken
					})));
				}
				throw LogHelper.LogExceptionMessage(new SecurityTokenSignatureKeyNotFoundException("IDX10500: Signature validation failed. No security keys were provided to validate the signature."));
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000052B0 File Offset: 0x000034B0
		private static IEnumerable<SecurityKey> GetAllDecryptionKeys(TokenValidationParameters validationParameters)
		{
			if (validationParameters.TokenDecryptionKey != null)
			{
				yield return validationParameters.TokenDecryptionKey;
			}
			if (validationParameters.TokenDecryptionKeys != null)
			{
				foreach (SecurityKey securityKey in validationParameters.TokenDecryptionKeys)
				{
					yield return securityKey;
				}
				IEnumerator<SecurityKey> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000052C0 File Offset: 0x000034C0
		protected virtual ClaimsIdentity CreateClaimsIdentity(JwtSecurityToken jwtToken, string issuer, TokenValidationParameters validationParameters)
		{
			if (jwtToken == null)
			{
				throw LogHelper.LogArgumentNullException("jwtToken");
			}
			if (validationParameters == null)
			{
				throw LogHelper.LogArgumentNullException("validationParameters");
			}
			string text = issuer;
			if (string.IsNullOrWhiteSpace(issuer))
			{
				LogHelper.LogVerbose("IDX10244: Issuer is null or empty. Using runtime default for creating claims '{0}'.", new object[] { LogHelper.MarkAsNonPII("LOCAL AUTHORITY") });
				text = "LOCAL AUTHORITY";
			}
			if (!this.MapInboundClaims)
			{
				return this.CreateClaimsIdentityWithoutMapping(jwtToken, text, validationParameters);
			}
			return this.CreateClaimsIdentityWithMapping(jwtToken, text, validationParameters);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005334 File Offset: 0x00003534
		private ClaimsIdentity CreateClaimsIdentityWithMapping(JwtSecurityToken jwtToken, string actualIssuer, TokenValidationParameters validationParameters)
		{
			ClaimsIdentity claimsIdentity = validationParameters.CreateClaimsIdentity(jwtToken, actualIssuer);
			foreach (Claim claim in jwtToken.Claims)
			{
				if (!this._inboundClaimFilter.Contains(claim.Type))
				{
					bool flag = true;
					string type;
					if (!this._inboundClaimTypeMap.TryGetValue(claim.Type, out type))
					{
						type = claim.Type;
						flag = false;
					}
					if (type == "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor")
					{
						if (claimsIdentity.Actor != null)
						{
							throw LogHelper.LogExceptionMessage(new InvalidOperationException(LogHelper.FormatInvariant("IDX12710: Only a single 'Actor' is supported. Found second claim of type: '{0}', value: '{1}'", new object[]
							{
								LogHelper.MarkAsNonPII("actort"),
								LogHelper.MarkAsSecurityArtifact(claim.Value, new Func<object, string>(JwtTokenUtilities.SafeLogJwtToken))
							})));
						}
						if (this.CanReadToken(claim.Value))
						{
							JwtSecurityToken jwtSecurityToken = this.ReadToken(claim.Value) as JwtSecurityToken;
							claimsIdentity.Actor = this.CreateClaimsIdentity(jwtSecurityToken, actualIssuer, validationParameters);
						}
					}
					Claim claim2 = new Claim(type, claim.Value, claim.ValueType, actualIssuer, actualIssuer, claimsIdentity);
					if (claim.Properties.Count > 0)
					{
						foreach (KeyValuePair<string, string> keyValuePair in claim.Properties)
						{
							claim2.Properties[keyValuePair.Key] = keyValuePair.Value;
						}
					}
					if (flag)
					{
						claim2.Properties[JwtSecurityTokenHandler.ShortClaimTypeProperty] = claim.Type;
					}
					claimsIdentity.AddClaim(claim2);
				}
			}
			return claimsIdentity;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00005500 File Offset: 0x00003700
		private ClaimsIdentity CreateClaimsIdentityWithoutMapping(JwtSecurityToken jwtToken, string actualIssuer, TokenValidationParameters validationParameters)
		{
			ClaimsIdentity claimsIdentity = validationParameters.CreateClaimsIdentity(jwtToken, actualIssuer);
			foreach (Claim claim in jwtToken.Claims)
			{
				if (!this._inboundClaimFilter.Contains(claim.Type))
				{
					string type = claim.Type;
					if (type == "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor")
					{
						if (claimsIdentity.Actor != null)
						{
							throw LogHelper.LogExceptionMessage(new InvalidOperationException(LogHelper.FormatInvariant("IDX12710: Only a single 'Actor' is supported. Found second claim of type: '{0}', value: '{1}'", new object[]
							{
								LogHelper.MarkAsNonPII("actort"),
								LogHelper.MarkAsSecurityArtifact(claim.Value, new Func<object, string>(JwtTokenUtilities.SafeLogJwtToken))
							})));
						}
						if (this.CanReadToken(claim.Value))
						{
							JwtSecurityToken jwtSecurityToken = this.ReadToken(claim.Value) as JwtSecurityToken;
							claimsIdentity.Actor = this.CreateClaimsIdentity(jwtSecurityToken, actualIssuer, validationParameters);
						}
					}
					Claim claim2 = new Claim(type, claim.Value, claim.ValueType, actualIssuer, actualIssuer, claimsIdentity);
					if (claim.Properties.Count > 0)
					{
						foreach (KeyValuePair<string, string> keyValuePair in claim.Properties)
						{
							claim2.Properties[keyValuePair.Key] = keyValuePair.Value;
						}
					}
					claimsIdentity.AddClaim(claim2);
				}
			}
			return claimsIdentity;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005698 File Offset: 0x00003898
		protected virtual string CreateActorValue(ClaimsIdentity actor)
		{
			if (actor == null)
			{
				throw LogHelper.LogArgumentNullException("actor");
			}
			if (actor.BootstrapContext != null)
			{
				string text = actor.BootstrapContext as string;
				if (text != null)
				{
					LogHelper.LogVerbose("IDX12713: Creating actor value using actor.BootstrapContext(as string)", Array.Empty<object>());
					return text;
				}
				JwtSecurityToken jwtSecurityToken = actor.BootstrapContext as JwtSecurityToken;
				if (jwtSecurityToken != null)
				{
					if (jwtSecurityToken.RawData != null)
					{
						LogHelper.LogVerbose("IDX12714: Creating actor value using actor.BootstrapContext.rawData", Array.Empty<object>());
						return jwtSecurityToken.RawData;
					}
					LogHelper.LogVerbose("IDX12715: Creating actor value by writing the JwtSecurityToken created from actor.BootstrapContext", Array.Empty<object>());
					return this.WriteToken(jwtSecurityToken);
				}
				else
				{
					LogHelper.LogVerbose("IDX12711: actor.BootstrapContext is not a string AND actor.BootstrapContext is not a JWT", Array.Empty<object>());
				}
			}
			LogHelper.LogVerbose("IDX12712: actor.BootstrapContext is null. Creating the token using actor.Claims.", Array.Empty<object>());
			return this.WriteToken(new JwtSecurityToken(null, null, actor.Claims, null, null, null));
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005763 File Offset: 0x00003963
		protected virtual void ValidateAudience(IEnumerable<string> audiences, JwtSecurityToken jwtToken, TokenValidationParameters validationParameters)
		{
			Validators.ValidateAudience(audiences, jwtToken, validationParameters);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000576D File Offset: 0x0000396D
		protected virtual void ValidateLifetime(DateTime? notBefore, DateTime? expires, JwtSecurityToken jwtToken, TokenValidationParameters validationParameters)
		{
			Validators.ValidateLifetime(notBefore, expires, jwtToken, validationParameters);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00005779 File Offset: 0x00003979
		protected virtual string ValidateIssuer(string issuer, JwtSecurityToken jwtToken, TokenValidationParameters validationParameters)
		{
			return Validators.ValidateIssuer(issuer, jwtToken, validationParameters);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00005783 File Offset: 0x00003983
		protected virtual void ValidateTokenReplay(DateTime? expires, string securityToken, TokenValidationParameters validationParameters)
		{
			Validators.ValidateTokenReplay(expires, securityToken, validationParameters);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000578D File Offset: 0x0000398D
		protected virtual SecurityKey ResolveIssuerSigningKey(string token, JwtSecurityToken jwtToken, TokenValidationParameters validationParameters)
		{
			if (validationParameters == null)
			{
				throw LogHelper.LogArgumentNullException("validationParameters");
			}
			if (jwtToken == null)
			{
				throw LogHelper.LogArgumentNullException("jwtToken");
			}
			return JwtTokenUtilities.ResolveTokenSigningKey(jwtToken.Header.Kid, jwtToken.Header.X5t, validationParameters, null);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000057C8 File Offset: 0x000039C8
		protected virtual SecurityKey ResolveTokenDecryptionKey(string token, JwtSecurityToken jwtToken, TokenValidationParameters validationParameters)
		{
			if (jwtToken == null)
			{
				throw LogHelper.LogArgumentNullException("jwtToken");
			}
			if (validationParameters == null)
			{
				throw LogHelper.LogArgumentNullException("validationParameters");
			}
			if (!string.IsNullOrEmpty(jwtToken.Header.Kid))
			{
				if (validationParameters.TokenDecryptionKey != null && string.Equals(validationParameters.TokenDecryptionKey.KeyId, jwtToken.Header.Kid, (validationParameters.TokenDecryptionKey is X509SecurityKey) ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
				{
					return validationParameters.TokenDecryptionKey;
				}
				if (validationParameters.TokenDecryptionKeys != null)
				{
					foreach (SecurityKey securityKey in validationParameters.TokenDecryptionKeys)
					{
						if (securityKey != null && string.Equals(securityKey.KeyId, jwtToken.Header.Kid, (securityKey is X509SecurityKey) ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
						{
							return securityKey;
						}
					}
				}
			}
			if (!string.IsNullOrEmpty(jwtToken.Header.X5t))
			{
				if (validationParameters.TokenDecryptionKey != null)
				{
					if (string.Equals(validationParameters.TokenDecryptionKey.KeyId, jwtToken.Header.X5t, (validationParameters.TokenDecryptionKey is X509SecurityKey) ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
					{
						return validationParameters.TokenDecryptionKey;
					}
					X509SecurityKey x509SecurityKey = validationParameters.TokenDecryptionKey as X509SecurityKey;
					if (x509SecurityKey != null && string.Equals(x509SecurityKey.X5t, jwtToken.Header.X5t, StringComparison.OrdinalIgnoreCase))
					{
						return validationParameters.TokenDecryptionKey;
					}
				}
				if (validationParameters.TokenDecryptionKeys != null)
				{
					foreach (SecurityKey securityKey2 in validationParameters.TokenDecryptionKeys)
					{
						if (securityKey2 != null && string.Equals(securityKey2.KeyId, jwtToken.Header.X5t, (securityKey2 is X509SecurityKey) ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
						{
							return securityKey2;
						}
						X509SecurityKey x509SecurityKey2 = securityKey2 as X509SecurityKey;
						if (x509SecurityKey2 != null && string.Equals(x509SecurityKey2.X5t, jwtToken.Header.X5t, StringComparison.OrdinalIgnoreCase))
						{
							return securityKey2;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000059D4 File Offset: 0x00003BD4
		protected string DecryptToken(JwtSecurityToken jwtToken, TokenValidationParameters validationParameters)
		{
			if (jwtToken == null)
			{
				throw LogHelper.LogArgumentNullException("jwtToken");
			}
			if (validationParameters == null)
			{
				throw LogHelper.LogArgumentNullException("validationParameters");
			}
			if (string.IsNullOrEmpty(jwtToken.Header.Enc))
			{
				throw LogHelper.LogExceptionMessage(new SecurityTokenException(LogHelper.FormatInvariant("IDX10612: Decryption failed. Header.Enc is null or empty, it must be specified.", Array.Empty<object>())));
			}
			IEnumerable<SecurityKey> contentEncryptionKeys = this.GetContentEncryptionKeys(jwtToken, validationParameters);
			return JwtTokenUtilities.DecryptJwtToken(jwtToken, validationParameters, new JwtTokenDecryptionParameters
			{
				Alg = jwtToken.Header.Alg,
				AuthenticationTagBytes = Base64UrlEncoder.DecodeBytes(jwtToken.RawAuthenticationTag),
				CipherTextBytes = Base64UrlEncoder.DecodeBytes(jwtToken.RawCiphertext),
				DecompressionFunction = new Func<byte[], string, int, string>(JwtTokenUtilities.DecompressToken),
				Enc = jwtToken.Header.Enc,
				EncodedToken = jwtToken.RawData,
				HeaderAsciiBytes = Encoding.ASCII.GetBytes(jwtToken.EncodedHeader),
				InitializationVectorBytes = Base64UrlEncoder.DecodeBytes(jwtToken.RawInitializationVector),
				MaximumDeflateSize = this.MaximumTokenSizeInBytes,
				Keys = contentEncryptionKeys,
				Zip = jwtToken.Header.Zip
			});
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005AEC File Offset: 0x00003CEC
		internal IEnumerable<SecurityKey> GetContentEncryptionKeys(JwtSecurityToken jwtToken, TokenValidationParameters validationParameters)
		{
			IEnumerable<SecurityKey> enumerable = null;
			if (validationParameters.TokenDecryptionKeyResolver != null)
			{
				enumerable = validationParameters.TokenDecryptionKeyResolver(jwtToken.RawData, jwtToken, jwtToken.Header.Kid, validationParameters);
			}
			else
			{
				SecurityKey securityKey = this.ResolveTokenDecryptionKey(jwtToken.RawData, jwtToken, validationParameters);
				if (securityKey != null)
				{
					enumerable = new List<SecurityKey> { securityKey };
				}
			}
			if (enumerable == null)
			{
				enumerable = JwtSecurityTokenHandler.GetAllDecryptionKeys(validationParameters);
			}
			if (jwtToken.Header.Alg.Equals("dir"))
			{
				return enumerable;
			}
			List<SecurityKey> list = new List<SecurityKey>();
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			foreach (SecurityKey securityKey2 in enumerable)
			{
				try
				{
					if (securityKey2.CryptoProviderFactory.IsSupportedAlgorithm(jwtToken.Header.Alg, securityKey2))
					{
						byte[] array = securityKey2.CryptoProviderFactory.CreateKeyWrapProviderForUnwrap(securityKey2, jwtToken.Header.Alg).UnwrapKey(Base64UrlEncoder.DecodeBytes(jwtToken.RawEncryptedKey));
						list.Add(new SymmetricSecurityKey(array));
					}
				}
				catch (Exception ex)
				{
					stringBuilder.AppendLine(ex.ToString());
				}
				stringBuilder2.AppendLine(securityKey2.ToString());
			}
			if (list.Count > 0 || stringBuilder.Length == 0)
			{
				return list;
			}
			throw LogHelper.LogExceptionMessage(new SecurityTokenKeyWrapException(LogHelper.FormatInvariant("IDX10618: Key unwrap failed using decryption Keys: '{0}'.\nExceptions caught:\n '{1}'.\ntoken: '{2}'.", new object[] { stringBuilder2, stringBuilder, jwtToken })));
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005C74 File Offset: 0x00003E74
		private static byte[] GetSymmetricSecurityKey(SecurityKey key)
		{
			if (key == null)
			{
				throw LogHelper.LogArgumentNullException("key");
			}
			SymmetricSecurityKey symmetricSecurityKey = key as SymmetricSecurityKey;
			if (symmetricSecurityKey != null)
			{
				return symmetricSecurityKey.Key;
			}
			JsonWebKey jsonWebKey = key as JsonWebKey;
			if (jsonWebKey != null && jsonWebKey.K != null)
			{
				return Base64UrlEncoder.DecodeBytes(jsonWebKey.K);
			}
			return null;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005CBF File Offset: 0x00003EBF
		protected virtual void ValidateIssuerSecurityKey(SecurityKey key, JwtSecurityToken securityToken, TokenValidationParameters validationParameters)
		{
			Validators.ValidateIssuerSecurityKey(key, securityToken, validationParameters);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00005CC9 File Offset: 0x00003EC9
		public override void WriteToken(XmlWriter writer, SecurityToken token)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00005CD0 File Offset: 0x00003ED0
		public override Task<TokenValidationResult> ValidateTokenAsync(string token, TokenValidationParameters validationParameters)
		{
			Task<TokenValidationResult> task;
			try
			{
				SecurityToken securityToken;
				ClaimsPrincipal claimsPrincipal = this.ValidateToken(token, validationParameters, out securityToken);
				task = Task.FromResult<TokenValidationResult>(new TokenValidationResult
				{
					SecurityToken = securityToken,
					ClaimsIdentity = (((claimsPrincipal != null) ? claimsPrincipal.Identity : null) as ClaimsIdentity),
					IsValid = true
				});
			}
			catch (Exception ex)
			{
				task = Task.FromResult<TokenValidationResult>(new TokenValidationResult
				{
					IsValid = false,
					Exception = ex
				});
			}
			return task;
		}

		// Token: 0x04000049 RID: 73
		private ISet<string> _inboundClaimFilter;

		// Token: 0x0400004A RID: 74
		private IDictionary<string, string> _inboundClaimTypeMap;

		// Token: 0x0400004B RID: 75
		private static string _jsonClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/json_type";

		// Token: 0x0400004C RID: 76
		private const string _namespace = "http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties";

		// Token: 0x0400004D RID: 77
		private const string _className = "System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler";

		// Token: 0x0400004E RID: 78
		private IDictionary<string, string> _outboundClaimTypeMap;

		// Token: 0x0400004F RID: 79
		private IDictionary<string, string> _outboundAlgorithmMap;

		// Token: 0x04000050 RID: 80
		private static string _shortClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/ShortTypeName";

		// Token: 0x04000051 RID: 81
		private bool _mapInboundClaims = JwtSecurityTokenHandler.DefaultMapInboundClaims;

		// Token: 0x04000052 RID: 82
		public static IDictionary<string, string> DefaultInboundClaimTypeMap = new Dictionary<string, string>(ClaimTypeMapping.InboundClaimTypeMap);

		// Token: 0x04000053 RID: 83
		public static bool DefaultMapInboundClaims = true;

		// Token: 0x04000054 RID: 84
		public static IDictionary<string, string> DefaultOutboundClaimTypeMap = new Dictionary<string, string>(ClaimTypeMapping.OutboundClaimTypeMap);

		// Token: 0x04000055 RID: 85
		public static ISet<string> DefaultInboundClaimFilter = ClaimTypeMapping.InboundClaimFilter;

		// Token: 0x04000056 RID: 86
		public static IDictionary<string, string> DefaultOutboundAlgorithmMap = new Dictionary<string, string>
		{
			{ "http://www.w3.org/2001/04/xmldsig-more#ecdsa-sha256", "ES256" },
			{ "http://www.w3.org/2001/04/xmldsig-more#ecdsa-sha384", "ES384" },
			{ "http://www.w3.org/2001/04/xmldsig-more#ecdsa-sha512", "ES512" },
			{ "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256", "HS256" },
			{ "http://www.w3.org/2001/04/xmldsig-more#hmac-sha384", "HS384" },
			{ "http://www.w3.org/2001/04/xmldsig-more#hmac-sha512", "HS512" },
			{ "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256", "RS256" },
			{ "http://www.w3.org/2001/04/xmldsig-more#rsa-sha384", "RS384" },
			{ "http://www.w3.org/2001/04/xmldsig-more#rsa-sha512", "RS512" }
		};

		// Token: 0x0200000E RID: 14
		// (Invoke) Token: 0x060000CE RID: 206
		private delegate bool CertMatcher(X509Certificate2 cert);
	}
}
