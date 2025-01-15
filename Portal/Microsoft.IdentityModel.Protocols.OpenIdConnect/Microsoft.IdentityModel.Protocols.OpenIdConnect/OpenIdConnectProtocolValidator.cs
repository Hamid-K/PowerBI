using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.IdentityModel.Protocols.OpenIdConnect
{
	// Token: 0x02000011 RID: 17
	public class OpenIdConnectProtocolValidator
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x00003180 File Offset: 0x00001380
		public OpenIdConnectProtocolValidator()
		{
			this.RequireAcr = false;
			this.RequireAmr = false;
			this.RequireAuthTime = false;
			this.RequireAzp = false;
			this.RequireNonce = true;
			this.RequireState = true;
			this.RequireTimeStampInNonce = true;
			this.RequireStateValidation = true;
			this._cryptoProviderFactory = new CryptoProviderFactory(CryptoProviderFactory.Default);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000331C File Offset: 0x0000151C
		public virtual string GenerateNonce()
		{
			LogHelper.LogVerbose("IDX21328: Generating nonce for openIdConnect message.", Array.Empty<object>());
			string text = Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString() + Guid.NewGuid().ToString()));
			if (this.RequireTimeStampInNonce)
			{
				return DateTime.UtcNow.Ticks.ToString(CultureInfo.InvariantCulture) + "." + text;
			}
			return text;
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000033A2 File Offset: 0x000015A2
		public IDictionary<string, string> HashAlgorithmMap
		{
			get
			{
				return this._hashAlgorithmMap;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x000033AA File Offset: 0x000015AA
		// (set) Token: 0x060000FA RID: 250 RVA: 0x000033B4 File Offset: 0x000015B4
		public TimeSpan NonceLifetime
		{
			get
			{
				return this._nonceLifetime;
			}
			set
			{
				if (value <= TimeSpan.Zero)
				{
					throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("value", LogHelper.FormatInvariant("IDX21105: NonceLifetime must be greater than zero. value: '{0}'", new object[] { LogHelper.MarkAsNonPII(value) })));
				}
				this._nonceLifetime = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00003403 File Offset: 0x00001603
		// (set) Token: 0x060000FC RID: 252 RVA: 0x0000340B File Offset: 0x0000160B
		[DefaultValue(false)]
		public bool RequireAcr { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00003414 File Offset: 0x00001614
		// (set) Token: 0x060000FE RID: 254 RVA: 0x0000341C File Offset: 0x0000161C
		[DefaultValue(false)]
		public bool RequireAmr { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00003425 File Offset: 0x00001625
		// (set) Token: 0x06000100 RID: 256 RVA: 0x0000342D File Offset: 0x0000162D
		[DefaultValue(false)]
		public bool RequireAuthTime { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00003436 File Offset: 0x00001636
		// (set) Token: 0x06000102 RID: 258 RVA: 0x0000343E File Offset: 0x0000163E
		[DefaultValue(false)]
		public bool RequireAzp { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00003447 File Offset: 0x00001647
		// (set) Token: 0x06000104 RID: 260 RVA: 0x0000344F File Offset: 0x0000164F
		[DefaultValue(true)]
		public bool RequireNonce { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00003458 File Offset: 0x00001658
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00003460 File Offset: 0x00001660
		[DefaultValue(true)]
		public bool RequireState { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00003469 File Offset: 0x00001669
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00003471 File Offset: 0x00001671
		[DefaultValue(true)]
		public bool RequireStateValidation { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000109 RID: 265 RVA: 0x0000347A File Offset: 0x0000167A
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00003482 File Offset: 0x00001682
		[DefaultValue(true)]
		public bool RequireSub { get; set; } = OpenIdConnectProtocolValidator.RequireSubByDefault;

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000348B File Offset: 0x0000168B
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00003492 File Offset: 0x00001692
		public static bool RequireSubByDefault { get; set; } = true;

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000349A File Offset: 0x0000169A
		// (set) Token: 0x0600010E RID: 270 RVA: 0x000034A2 File Offset: 0x000016A2
		[DefaultValue(true)]
		public bool RequireTimeStampInNonce { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600010F RID: 271 RVA: 0x000034AB File Offset: 0x000016AB
		// (set) Token: 0x06000110 RID: 272 RVA: 0x000034B3 File Offset: 0x000016B3
		public IdTokenValidator IdTokenValidator { get; set; }

		// Token: 0x06000111 RID: 273 RVA: 0x000034BC File Offset: 0x000016BC
		public virtual void ValidateAuthenticationResponse(OpenIdConnectProtocolValidationContext validationContext)
		{
			if (validationContext == null)
			{
				throw LogHelper.LogArgumentNullException("validationContext");
			}
			if (validationContext.ProtocolMessage == null)
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException("IDX21333: OpenIdConnectProtocolValidationContext.ProtocolMessage is null, there is no OpenIdConnect Response to validate."));
			}
			if (string.IsNullOrEmpty(validationContext.ProtocolMessage.IdToken))
			{
				if (string.IsNullOrEmpty(validationContext.ProtocolMessage.Code))
				{
					throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException("IDX21334: Both 'id_token' and 'code' are null in OpenIdConnectProtocolValidationContext.ProtocolMessage received from Authorization Endpoint. Cannot process the message."));
				}
				this.ValidateState(validationContext);
				return;
			}
			else
			{
				if (validationContext.ValidatedIdToken == null)
				{
					throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException("IDX21332: OpenIdConnectProtocolValidationContext.ValidatedIdToken is null. There is no 'id_token' to validate against."));
				}
				if (!string.IsNullOrEmpty(validationContext.ProtocolMessage.RefreshToken))
				{
					throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException("IDX21335: 'refresh_token' cannot be present in a response message received from Authorization Endpoint."));
				}
				this.ValidateState(validationContext);
				this.ValidateIdToken(validationContext);
				this.ValidateNonce(validationContext);
				this.ValidateCHash(validationContext);
				this.ValidateAtHash(validationContext);
				return;
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00003588 File Offset: 0x00001788
		public virtual void ValidateTokenResponse(OpenIdConnectProtocolValidationContext validationContext)
		{
			if (validationContext == null)
			{
				throw LogHelper.LogArgumentNullException("validationContext");
			}
			if (validationContext.ProtocolMessage == null)
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException("IDX21333: OpenIdConnectProtocolValidationContext.ProtocolMessage is null, there is no OpenIdConnect Response to validate."));
			}
			if (string.IsNullOrEmpty(validationContext.ProtocolMessage.IdToken) || string.IsNullOrEmpty(validationContext.ProtocolMessage.AccessToken))
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException("IDX21336: Both 'id_token' and 'access_token' should be present in OpenIdConnectProtocolValidationContext.ProtocolMessage received from Token Endpoint. Cannot process the message."));
			}
			if (validationContext.ValidatedIdToken == null)
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException("IDX21332: OpenIdConnectProtocolValidationContext.ValidatedIdToken is null. There is no 'id_token' to validate against."));
			}
			this.ValidateIdToken(validationContext);
			this.ValidateNonce(validationContext);
			object obj;
			if (validationContext.ValidatedIdToken.Payload.TryGetValue("at_hash", out obj))
			{
				this.ValidateAtHash(validationContext);
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00003638 File Offset: 0x00001838
		public virtual void ValidateUserInfoResponse(OpenIdConnectProtocolValidationContext validationContext)
		{
			if (validationContext == null)
			{
				throw LogHelper.LogArgumentNullException("validationContext");
			}
			if (string.IsNullOrEmpty(validationContext.UserInfoEndpointResponse))
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException("IDX21337: OpenIdConnectProtocolValidationContext.UserInfoEndpointResponse is null or empty, there is no OpenIdConnect Response to validate."));
			}
			if (validationContext.ValidatedIdToken == null)
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException("IDX21332: OpenIdConnectProtocolValidationContext.ValidatedIdToken is null. There is no 'id_token' to validate against."));
			}
			string text = string.Empty;
			try
			{
				JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
				if (jwtSecurityTokenHandler.CanReadToken(validationContext.UserInfoEndpointResponse))
				{
					text = (jwtSecurityTokenHandler.ReadToken(validationContext.UserInfoEndpointResponse) as JwtSecurityToken).Payload.Sub;
				}
				else
				{
					text = JwtPayload.Deserialize(validationContext.UserInfoEndpointResponse).Sub;
				}
			}
			catch (Exception ex)
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant("IDX21343: Unable to parse response from UserInfo endpoint: '{0}'", new object[] { validationContext.UserInfoEndpointResponse }), ex));
			}
			if (string.IsNullOrEmpty(text))
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException("IDX21345: OpenIdConnectProtocolValidationContext.UserInfoEndpointResponse does not contain a 'sub' claim, cannot validate."));
			}
			if (string.IsNullOrEmpty(validationContext.ValidatedIdToken.Payload.Sub))
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException("IDX21346: OpenIdConnectProtocolValidationContext.ValidatedIdToken does not contain a 'sub' claim, cannot validate."));
			}
			if (!string.Equals(validationContext.ValidatedIdToken.Payload.Sub, text))
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant("IDX21338: Subject claim present in 'id_token': '{0}' does not match the claim received from UserInfo Endpoint: '{1}'.", new object[]
				{
					validationContext.ValidatedIdToken.Payload.Sub,
					text
				})));
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00003794 File Offset: 0x00001994
		protected virtual void ValidateIdToken(OpenIdConnectProtocolValidationContext validationContext)
		{
			if (validationContext == null)
			{
				throw LogHelper.LogArgumentNullException("validationContext");
			}
			if (validationContext.ValidatedIdToken == null)
			{
				throw LogHelper.LogArgumentNullException("validationContext.ValidatedIdToken");
			}
			if (this.IdTokenValidator != null)
			{
				try
				{
					this.IdTokenValidator(validationContext.ValidatedIdToken, validationContext);
				}
				catch (Exception ex)
				{
					throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant("IDX21313: The id_token: '{0}' is not valid. Delegate threw exception, see inner exception for more details.", new object[] { validationContext.ValidatedIdToken }), ex));
				}
				return;
			}
			JwtSecurityToken validatedIdToken = validationContext.ValidatedIdToken;
			if (validatedIdToken.Payload.Aud.Count == 0)
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant("IDX21314: OpenIdConnectProtocol requires the jwt token to have an '{0}' claim. The jwt did not contain an '{0}' claim, jwt: '{1}'.", new object[]
				{
					LogHelper.MarkAsNonPII("aud".ToLowerInvariant()),
					validatedIdToken
				})));
			}
			if (validatedIdToken.Payload.Exp == null)
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant("IDX21314: OpenIdConnectProtocol requires the jwt token to have an '{0}' claim. The jwt did not contain an '{0}' claim, jwt: '{1}'.", new object[]
				{
					LogHelper.MarkAsNonPII("exp".ToLowerInvariant()),
					validatedIdToken
				})));
			}
			if (validatedIdToken.Payload.Iat == null)
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant("IDX21314: OpenIdConnectProtocol requires the jwt token to have an '{0}' claim. The jwt did not contain an '{0}' claim, jwt: '{1}'.", new object[]
				{
					LogHelper.MarkAsNonPII("iat".ToLowerInvariant()),
					validatedIdToken
				})));
			}
			if (validatedIdToken.Payload.Iss == null)
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant("IDX21314: OpenIdConnectProtocol requires the jwt token to have an '{0}' claim. The jwt did not contain an '{0}' claim, jwt: '{1}'.", new object[]
				{
					LogHelper.MarkAsNonPII("iss".ToLowerInvariant()),
					validatedIdToken
				})));
			}
			if (this.RequireSub && string.IsNullOrWhiteSpace(validatedIdToken.Payload.Sub))
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant("IDX21314: OpenIdConnectProtocol requires the jwt token to have an '{0}' claim. The jwt did not contain an '{0}' claim, jwt: '{1}'.", new object[]
				{
					LogHelper.MarkAsNonPII("sub".ToLowerInvariant()),
					validatedIdToken
				})));
			}
			if (this.RequireAcr && string.IsNullOrWhiteSpace(validatedIdToken.Payload.Acr))
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant("IDX21315: RequireAcr is 'true' (default is 'false') but jwt.PayLoad.Acr is 'null or whitespace', jwt: '{0}'.", new object[] { validatedIdToken })));
			}
			if (this.RequireAmr && validatedIdToken.Payload.Amr.Count == 0)
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant("IDX21316: RequireAmr is 'true' (default is 'false') but jwt.PayLoad.Amr is 'null or whitespace', jwt: '{0}'.", new object[] { validatedIdToken })));
			}
			if (this.RequireAuthTime && validatedIdToken.Payload.AuthTime == null)
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant("IDX21317: RequireAuthTime is 'true' (default is 'false') but jwt.PayLoad.AuthTime is 'null or whitespace', jwt: '{0}'.", new object[] { validatedIdToken })));
			}
			if (this.RequireAzp && string.IsNullOrWhiteSpace(validatedIdToken.Payload.Azp))
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant("IDX21318: RequireAzp is 'true' (default is 'false') but jwt.PayLoad.Azp is 'null or whitespace', jwt: '{0}'.", new object[] { validatedIdToken })));
			}
			if (validatedIdToken.Payload.Aud.Count > 1 && string.IsNullOrEmpty(validatedIdToken.Payload.Azp))
			{
				LogHelper.LogWarning("IDX21339: The 'id_token' contains multiple audiences but 'azp' claim is missing.", Array.Empty<object>());
			}
			if (!string.IsNullOrEmpty(validatedIdToken.Payload.Azp))
			{
				if (string.IsNullOrEmpty(validationContext.ClientId))
				{
					throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException("IDX21308: 'azp' claim exists in the 'id_token' but 'client_id' is null. Cannot validate the 'azp' claim."));
				}
				if (!string.Equals(validatedIdToken.Payload.Azp, validationContext.ClientId))
				{
					throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant("IDX21340: The 'id_token' contains 'azp' claim but its value is not equal to Client Id. 'azp': '{0}'. clientId: '{1}'.", new object[]
					{
						validatedIdToken.Payload.Azp,
						validationContext.ClientId
					})));
				}
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00003B08 File Offset: 0x00001D08
		public virtual HashAlgorithm GetHashAlgorithm(string algorithm)
		{
			if (string.IsNullOrEmpty(algorithm))
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException("IDX21350: The algorithm specified in the jwt header is null or empty."));
			}
			HashAlgorithm hashAlgorithm;
			try
			{
				string text;
				if (!this.HashAlgorithmMap.TryGetValue(algorithm, out text))
				{
					text = algorithm;
				}
				hashAlgorithm = this.CryptoProviderFactory.CreateHashAlgorithm(text);
			}
			catch (Exception ex)
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant("IDX21301: The algorithm: '{0}' specified in the jwt header could not be used to create a '{1}'. See inner exception for details.", new object[]
				{
					LogHelper.MarkAsNonPII(algorithm),
					LogHelper.MarkAsNonPII(typeof(HashAlgorithm))
				}), ex));
			}
			return hashAlgorithm;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00003B98 File Offset: 0x00001D98
		// (set) Token: 0x06000117 RID: 279 RVA: 0x00003BA0 File Offset: 0x00001DA0
		public CryptoProviderFactory CryptoProviderFactory
		{
			get
			{
				return this._cryptoProviderFactory;
			}
			set
			{
				if (value == null)
				{
					throw LogHelper.LogArgumentNullException("value");
				}
				this._cryptoProviderFactory = value;
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00003BB8 File Offset: 0x00001DB8
		private void ValidateHash(string expectedValue, string hashItem, string algorithm)
		{
			LogHelper.LogInformation("IDX21303: Validating hash of OIDC protocol message. Expected: '{0}'.", new object[] { expectedValue });
			HashAlgorithm hashAlgorithm = null;
			try
			{
				hashAlgorithm = this.GetHashAlgorithm(algorithm);
				OpenIdConnectProtocolValidator.CheckHash(hashAlgorithm, expectedValue, hashItem, algorithm);
			}
			finally
			{
				this.CryptoProviderFactory.ReleaseHashAlgorithm(hashAlgorithm);
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00003C0C File Offset: 0x00001E0C
		private static void CheckHash(HashAlgorithm hashAlgorithm, string expectedValue, string hashItem, string algorithm)
		{
			byte[] array = hashAlgorithm.ComputeHash(Encoding.ASCII.GetBytes(hashItem));
			string text = Base64UrlEncoder.Encode(array, 0, array.Length / 2);
			if (!string.Equals(expectedValue, text))
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException(LogHelper.FormatInvariant("IDX21300: The hash claim: '{0}' in the id_token did not validate with against: '{1}', algorithm: '{2}'.", new object[]
				{
					expectedValue,
					hashItem,
					LogHelper.MarkAsNonPII(algorithm)
				})));
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00003C70 File Offset: 0x00001E70
		protected virtual void ValidateCHash(OpenIdConnectProtocolValidationContext validationContext)
		{
			LogHelper.LogVerbose("IDX21304: Validating 'c_hash' using id_token and code.", Array.Empty<object>());
			if (validationContext == null)
			{
				throw LogHelper.LogArgumentNullException("validationContext");
			}
			if (validationContext.ValidatedIdToken == null)
			{
				throw LogHelper.LogArgumentNullException("ValidatedIdToken");
			}
			if (validationContext.ProtocolMessage == null)
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException("IDX21333: OpenIdConnectProtocolValidationContext.ProtocolMessage is null, there is no OpenIdConnect Response to validate."));
			}
			if (string.IsNullOrEmpty(validationContext.ProtocolMessage.Code))
			{
				LogHelper.LogInformation("IDX21305: OpenIdConnectProtocolValidationContext.ProtocolMessage.Code is null, there is no 'code' in the OpenIdConnect Response to validate.", Array.Empty<object>());
				return;
			}
			object obj;
			if (!validationContext.ValidatedIdToken.Payload.TryGetValue("c_hash", out obj))
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolInvalidCHashException(LogHelper.FormatInvariant("IDX21307: The 'c_hash' claim was not found in the id_token, but a 'code' was in the OpenIdConnectMessage, id_token: '{0}'", new object[] { validationContext.ValidatedIdToken })));
			}
			string text = obj as string;
			if (text == null)
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolInvalidCHashException(LogHelper.FormatInvariant("IDX21306: The 'c_hash' claim was not a string in the 'id_token', but a 'code' was in the OpenIdConnectMessage, 'id_token': '{0}'.", new object[] { validationContext.ValidatedIdToken })));
			}
			JwtSecurityToken validatedIdToken = validationContext.ValidatedIdToken;
			string text2 = ((validatedIdToken.InnerToken != null) ? validatedIdToken.InnerToken.Header.Alg : validatedIdToken.Header.Alg);
			try
			{
				this.ValidateHash(text, validationContext.ProtocolMessage.Code, text2);
			}
			catch (OpenIdConnectProtocolException ex)
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolInvalidCHashException("IDX21347: Validating the 'c_hash' failed, see inner exception.", ex));
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00003DB8 File Offset: 0x00001FB8
		protected virtual void ValidateAtHash(OpenIdConnectProtocolValidationContext validationContext)
		{
			LogHelper.LogVerbose("IDX21309: Validating 'at_hash' using id_token and access_token.", Array.Empty<object>());
			if (validationContext == null)
			{
				throw LogHelper.LogArgumentNullException("validationContext");
			}
			if (validationContext.ValidatedIdToken == null)
			{
				throw LogHelper.LogArgumentNullException("validationContext.ValidatedIdToken");
			}
			if (validationContext.ProtocolMessage == null)
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException("IDX21333: OpenIdConnectProtocolValidationContext.ProtocolMessage is null, there is no OpenIdConnect Response to validate."));
			}
			if (string.IsNullOrEmpty(validationContext.ProtocolMessage.AccessToken))
			{
				LogHelper.LogInformation("IDX21310: OpenIdConnectProtocolValidationContext.ProtocolMessage.AccessToken is null, there is no 'token' in the OpenIdConnect Response to validate.", Array.Empty<object>());
				return;
			}
			object obj;
			if (!validationContext.ValidatedIdToken.Payload.TryGetValue("at_hash", out obj))
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolInvalidAtHashException(LogHelper.FormatInvariant("IDX21312: The 'at_hash' claim was not found in the 'id_token', but a 'access_token' was in the OpenIdConnectMessage, 'id_token': '{0}'.", new object[] { validationContext.ValidatedIdToken })));
			}
			string text = obj as string;
			if (text == null)
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolInvalidAtHashException(LogHelper.FormatInvariant("IDX21311: The 'at_hash' claim was not a string in the 'id_token', but an 'access_token' was in the OpenIdConnectMessage, 'id_token': '{0}'.", new object[] { validationContext.ValidatedIdToken })));
			}
			JwtSecurityToken validatedIdToken = validationContext.ValidatedIdToken;
			string text2 = ((validatedIdToken.InnerToken != null) ? validatedIdToken.InnerToken.Header.Alg : validatedIdToken.Header.Alg);
			try
			{
				this.ValidateHash(text, validationContext.ProtocolMessage.AccessToken, text2);
			}
			catch (OpenIdConnectProtocolException ex)
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolInvalidAtHashException("IDX21348: Validating the 'at_hash' failed, see inner exception.", ex));
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00003F00 File Offset: 0x00002100
		protected virtual void ValidateNonce(OpenIdConnectProtocolValidationContext validationContext)
		{
			LogHelper.LogVerbose("IDX21319: Validating the nonce claim found in the id_token.", Array.Empty<object>());
			if (validationContext == null)
			{
				throw LogHelper.LogArgumentNullException("validationContext");
			}
			if (validationContext.ValidatedIdToken == null)
			{
				throw LogHelper.LogArgumentNullException("ValidatedIdToken");
			}
			string nonce = validationContext.ValidatedIdToken.Payload.Nonce;
			if (!this.RequireNonce && string.IsNullOrEmpty(validationContext.Nonce) && string.IsNullOrEmpty(nonce))
			{
				LogHelper.LogInformation("IDX21322: RequireNonce is false, validationContext.Nonce is null and there is no 'nonce' in the OpenIdConnect Response to validate.", Array.Empty<object>());
				return;
			}
			if (string.IsNullOrEmpty(validationContext.Nonce) && string.IsNullOrEmpty(nonce))
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolInvalidNonceException(LogHelper.FormatInvariant("IDX21320: RequireNonce is '{0}'. OpenIdConnectProtocolValidationContext.Nonce and OpenIdConnectProtocol.ValidatedIdToken.Nonce are both null or empty. The nonce cannot be validated. If you don't need to check the nonce, set OpenIdConnectProtocolValidator.RequireNonce to 'false'.", new object[] { LogHelper.MarkAsNonPII(this.RequireNonce) })));
			}
			if (string.IsNullOrEmpty(validationContext.Nonce))
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolInvalidNonceException(LogHelper.FormatInvariant("IDX21323: RequireNonce is '{0}'. OpenIdConnectProtocolValidationContext.Nonce was null, OpenIdConnectProtocol.ValidatedIdToken.Payload.Nonce was not null. The nonce cannot be validated. If you don't need to check the nonce, set OpenIdConnectProtocolValidator.RequireNonce to 'false'. Note if a 'nonce' is found it will be evaluated.", new object[] { LogHelper.MarkAsNonPII(this.RequireNonce) })));
			}
			if (string.IsNullOrEmpty(nonce))
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolInvalidNonceException(LogHelper.FormatInvariant("IDX21349: RequireNonce is '{0}'. OpenIdConnectProtocolValidationContext.Nonce was not null, OpenIdConnectProtocol.ValidatedIdToken.Payload.Nonce was null or empty. The nonce cannot be validated. If you don't need to check the nonce, set OpenIdConnectProtocolValidator.RequireNonce to 'false'. Note if a 'nonce' is found it will be evaluated.", new object[] { LogHelper.MarkAsNonPII(this.RequireNonce) })));
			}
			if (!string.Equals(nonce, validationContext.Nonce))
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolInvalidNonceException(LogHelper.FormatInvariant("IDX21321: The 'nonce' found in the jwt token did not match the expected nonce.\nexpected: '{0}'\nfound in jwt: '{1}'.\njwt: '{2}'.", new object[] { validationContext.Nonce, nonce, validationContext.ValidatedIdToken })));
			}
			if (this.RequireTimeStampInNonce)
			{
				int num = nonce.IndexOf('.');
				if (num == -1)
				{
					throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolInvalidNonceException(LogHelper.FormatInvariant("IDX21325: The 'nonce' did not contain a timestamp: '{0}'.\nFormat expected is: <epochtime>.<noncedata>.", new object[] { nonce })));
				}
				string text = nonce.Substring(0, num);
				DateTime dateTime = new DateTime(1979, 1, 1);
				long num2 = -1L;
				try
				{
					num2 = Convert.ToInt64(text, CultureInfo.InvariantCulture);
				}
				catch (Exception ex)
				{
					throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolInvalidNonceException(LogHelper.FormatInvariant("IDX21326: The 'nonce' timestamp could not be converted to a positive integer (greater than 0).\ntimestamp: '{0}'\nnonce: '{1}'.", new object[]
					{
						LogHelper.MarkAsNonPII(text),
						nonce
					}), ex));
				}
				if (num2 <= 0L)
				{
					throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolInvalidNonceException(LogHelper.FormatInvariant("IDX21326: The 'nonce' timestamp could not be converted to a positive integer (greater than 0).\ntimestamp: '{0}'\nnonce: '{1}'.", new object[]
					{
						LogHelper.MarkAsNonPII(text),
						nonce
					})));
				}
				try
				{
					dateTime = DateTime.FromBinary(num2);
				}
				catch (Exception ex2)
				{
					throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolInvalidNonceException(LogHelper.FormatInvariant("IDX21327: The 'nonce' timestamp: '{0}', could not be converted to a DateTime using DateTime.FromBinary({0}).\nThe value must be between: '{1}' and '{2}'.", new object[]
					{
						LogHelper.MarkAsNonPII(text),
						LogHelper.MarkAsNonPII(DateTime.MinValue.Ticks.ToString(CultureInfo.InvariantCulture)),
						LogHelper.MarkAsNonPII(DateTime.MaxValue.Ticks.ToString(CultureInfo.InvariantCulture))
					}), ex2));
				}
				DateTime utcNow = DateTime.UtcNow;
				if (dateTime + this.NonceLifetime < utcNow)
				{
					throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolInvalidNonceException(LogHelper.FormatInvariant("IDX21324: The 'nonce' has expired: '{0}'. Time from 'nonce' (UTC): '{1}', Current Time (UTC): '{2}'. NonceLifetime is: '{3}'.", new object[]
					{
						nonce,
						LogHelper.MarkAsNonPII(dateTime.ToString(CultureInfo.InvariantCulture)),
						LogHelper.MarkAsNonPII(utcNow.ToString(CultureInfo.InvariantCulture)),
						LogHelper.MarkAsNonPII(this.NonceLifetime.ToString("c", CultureInfo.InvariantCulture))
					})));
				}
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004244 File Offset: 0x00002444
		protected virtual void ValidateState(OpenIdConnectProtocolValidationContext validationContext)
		{
			if (!this.RequireStateValidation)
			{
				LogHelper.LogVerbose("IDX21342: 'RequireStateValidation' = false, not validating the state.", Array.Empty<object>());
				return;
			}
			if (validationContext == null)
			{
				throw LogHelper.LogArgumentNullException("validationContext");
			}
			if (validationContext.ProtocolMessage == null)
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolException("IDX21333: OpenIdConnectProtocolValidationContext.ProtocolMessage is null, there is no OpenIdConnect Response to validate."));
			}
			if (!this.RequireState && string.IsNullOrEmpty(validationContext.State) && string.IsNullOrEmpty(validationContext.ProtocolMessage.State))
			{
				LogHelper.LogInformation("IDX21341: 'RequireState' = false, OpenIdConnectProtocolValidationContext.State is null and there is no 'state' in the OpenIdConnect response to validate.", Array.Empty<object>());
				return;
			}
			if (string.IsNullOrEmpty(validationContext.State))
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolInvalidStateException(LogHelper.FormatInvariant("IDX21329: RequireState is '{0}' but the OpenIdConnectProtocolValidationContext.State is null. State cannot be validated.", new object[] { LogHelper.MarkAsNonPII(this.RequireState) })));
			}
			if (string.IsNullOrEmpty(validationContext.ProtocolMessage.State))
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolInvalidStateException(LogHelper.FormatInvariant("IDX21330: RequireState is '{0}', the OpenIdConnect Request contained 'state', but the Response does not contain 'state'.", new object[] { LogHelper.MarkAsNonPII(this.RequireState) })));
			}
			if (!string.Equals(validationContext.State, validationContext.ProtocolMessage.State))
			{
				throw LogHelper.LogExceptionMessage(new OpenIdConnectProtocolInvalidStateException(LogHelper.FormatInvariant("IDX21331: The 'state' parameter in the message: '{0}', does not equal the 'state' in the context: '{1}'.", new object[]
				{
					validationContext.State,
					validationContext.ProtocolMessage.State
				})));
			}
		}

		// Token: 0x040000B1 RID: 177
		private IDictionary<string, string> _hashAlgorithmMap = new Dictionary<string, string>
		{
			{ "ES256", "SHA256" },
			{ "http://www.w3.org/2001/04/xmldsig-more#ecdsa-sha256", "SHA256" },
			{ "HS256", "SHA256" },
			{ "RS256", "SHA256" },
			{ "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256", "SHA256" },
			{ "PS256", "SHA256" },
			{ "ES384", "SHA384" },
			{ "http://www.w3.org/2001/04/xmldsig-more#ecdsa-sha384", "SHA384" },
			{ "HS384", "SHA384" },
			{ "RS384", "SHA384" },
			{ "http://www.w3.org/2001/04/xmldsig-more#rsa-sha384", "SHA384" },
			{ "PS384", "SHA384" },
			{ "ES512", "SHA512" },
			{ "http://www.w3.org/2001/04/xmldsig-more#ecdsa-sha512", "SHA512" },
			{ "HS512", "SHA512" },
			{ "RS512", "SHA512" },
			{ "http://www.w3.org/2001/04/xmldsig-more#rsa-sha512", "SHA512" },
			{ "PS512", "SHA512" }
		};

		// Token: 0x040000B2 RID: 178
		private TimeSpan _nonceLifetime = OpenIdConnectProtocolValidator.DefaultNonceLifetime;

		// Token: 0x040000B3 RID: 179
		private CryptoProviderFactory _cryptoProviderFactory;

		// Token: 0x040000B4 RID: 180
		public static readonly TimeSpan DefaultNonceLifetime = TimeSpan.FromMinutes(60.0);
	}
}
