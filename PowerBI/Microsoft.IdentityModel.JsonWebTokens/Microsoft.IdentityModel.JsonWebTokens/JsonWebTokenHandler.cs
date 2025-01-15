using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Json;
using Microsoft.IdentityModel.Json.Linq;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.IdentityModel.JsonWebTokens
{
	// Token: 0x02000005 RID: 5
	public class JsonWebTokenHandler : TokenHandler
	{
		// Token: 0x06000054 RID: 84 RVA: 0x000035F0 File Offset: 0x000017F0
		public JsonWebTokenHandler()
		{
			if (this._mapInboundClaims)
			{
				this._inboundClaimTypeMap = new Dictionary<string, string>(JsonWebTokenHandler.DefaultInboundClaimTypeMap);
				return;
			}
			this._inboundClaimTypeMap = new Dictionary<string, string>();
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003627 File Offset: 0x00001827
		public Type TokenType
		{
			get
			{
				return typeof(JsonWebToken);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00003633 File Offset: 0x00001833
		// (set) Token: 0x06000057 RID: 87 RVA: 0x0000363A File Offset: 0x0000183A
		public static string ShortClaimTypeProperty
		{
			get
			{
				return JsonWebTokenHandler._shortClaimType;
			}
			set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw LogHelper.LogArgumentNullException("value");
				}
				JsonWebTokenHandler._shortClaimType = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003655 File Offset: 0x00001855
		// (set) Token: 0x06000059 RID: 89 RVA: 0x0000365D File Offset: 0x0000185D
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
					this._inboundClaimTypeMap = new Dictionary<string, string>(JsonWebTokenHandler.DefaultInboundClaimTypeMap);
				}
				this._mapInboundClaims = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003690 File Offset: 0x00001890
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00003698 File Offset: 0x00001898
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

		// Token: 0x0600005C RID: 92 RVA: 0x000036B0 File Offset: 0x000018B0
		internal static IDictionary<string, object> AddCtyClaimDefaultValue(IDictionary<string, object> additionalClaims, bool setDefaultCtyClaim)
		{
			if (!setDefaultCtyClaim)
			{
				return additionalClaims;
			}
			object obj;
			if (additionalClaims == null)
			{
				additionalClaims = new Dictionary<string, object> { { "cty", "JWT" } };
			}
			else if (!additionalClaims.TryGetValue("cty", out obj))
			{
				additionalClaims.Add("cty", "JWT");
			}
			return additionalClaims;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003700 File Offset: 0x00001900
		public virtual bool CanReadToken(string token)
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
			LogHelper.LogInformation("IDX14107: Token string does not match the token formats: JWE (header.encryptedKey.iv.ciphertext.tag) or JWS (header.payload.signature)", Array.Empty<object>());
			return false;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000037A4 File Offset: 0x000019A4
		public virtual bool CanValidateToken
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000037A8 File Offset: 0x000019A8
		private static JObject CreateDefaultJWEHeader(EncryptingCredentials encryptingCredentials, string compressionAlgorithm, string tokenType)
		{
			JObject jobject = new JObject();
			jobject.Add("alg", encryptingCredentials.Alg);
			jobject.Add("enc", encryptingCredentials.Enc);
			if (!string.IsNullOrEmpty(encryptingCredentials.Key.KeyId))
			{
				jobject.Add("kid", encryptingCredentials.Key.KeyId);
			}
			if (!string.IsNullOrEmpty(compressionAlgorithm))
			{
				jobject.Add("zip", compressionAlgorithm);
			}
			if (string.IsNullOrEmpty(tokenType))
			{
				jobject.Add("typ", "JWT");
			}
			else
			{
				jobject.Add("typ", tokenType);
			}
			return jobject;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003860 File Offset: 0x00001A60
		private static JObject CreateDefaultJWSHeader(SigningCredentials signingCredentials, string tokenType)
		{
			JObject jobject;
			if (signingCredentials == null)
			{
				jobject = new JObject { { "alg", "none" } };
			}
			else
			{
				jobject = new JObject { { "alg", signingCredentials.Algorithm } };
				if (signingCredentials.Key.KeyId != null)
				{
					jobject.Add("kid", signingCredentials.Key.KeyId);
				}
				X509SecurityKey x509SecurityKey = signingCredentials.Key as X509SecurityKey;
				if (x509SecurityKey != null)
				{
					jobject["x5t"] = x509SecurityKey.X5t;
				}
			}
			if (string.IsNullOrEmpty(tokenType))
			{
				jobject.Add("typ", "JWT");
			}
			else
			{
				jobject.Add("typ", tokenType);
			}
			return jobject;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003929 File Offset: 0x00001B29
		public virtual string CreateToken(string payload)
		{
			if (string.IsNullOrEmpty(payload))
			{
				throw LogHelper.LogArgumentNullException("payload");
			}
			return this.CreateTokenPrivate(payload, null, null, null, null, null, null);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000394B File Offset: 0x00001B4B
		public virtual string CreateToken(string payload, IDictionary<string, object> additionalHeaderClaims)
		{
			if (string.IsNullOrEmpty(payload))
			{
				throw LogHelper.LogArgumentNullException("payload");
			}
			if (additionalHeaderClaims == null)
			{
				throw LogHelper.LogArgumentNullException("additionalHeaderClaims");
			}
			return this.CreateTokenPrivate(payload, null, null, null, additionalHeaderClaims, null, null);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000397B File Offset: 0x00001B7B
		public virtual string CreateToken(string payload, SigningCredentials signingCredentials)
		{
			if (string.IsNullOrEmpty(payload))
			{
				throw LogHelper.LogArgumentNullException("payload");
			}
			if (signingCredentials == null)
			{
				throw LogHelper.LogArgumentNullException("signingCredentials");
			}
			return this.CreateTokenPrivate(payload, signingCredentials, null, null, null, null, null);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000039AB File Offset: 0x00001BAB
		public virtual string CreateToken(string payload, SigningCredentials signingCredentials, IDictionary<string, object> additionalHeaderClaims)
		{
			if (string.IsNullOrEmpty(payload))
			{
				throw LogHelper.LogArgumentNullException("payload");
			}
			if (signingCredentials == null)
			{
				throw LogHelper.LogArgumentNullException("signingCredentials");
			}
			if (additionalHeaderClaims == null)
			{
				throw LogHelper.LogArgumentNullException("additionalHeaderClaims");
			}
			return this.CreateTokenPrivate(payload, signingCredentials, null, null, additionalHeaderClaims, null, null);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000039EC File Offset: 0x00001BEC
		public virtual string CreateToken(SecurityTokenDescriptor tokenDescriptor)
		{
			if (tokenDescriptor == null)
			{
				throw LogHelper.LogArgumentNullException("tokenDescriptor");
			}
			if ((tokenDescriptor.Subject == null || !tokenDescriptor.Subject.Claims.Any<Claim>()) && (tokenDescriptor.Claims == null || !tokenDescriptor.Claims.Any<KeyValuePair<string, object>>()))
			{
				LogHelper.LogWarning("IDX14114: Both '{0}.{1}' and '{0}.{2}' are null or empty.", new object[]
				{
					LogHelper.MarkAsNonPII("SecurityTokenDescriptor"),
					LogHelper.MarkAsNonPII("Subject"),
					LogHelper.MarkAsNonPII("Claims")
				});
			}
			JObject jobject;
			if (tokenDescriptor.Subject != null)
			{
				jobject = JObject.FromObject(TokenUtilities.CreateDictionaryFromClaims(tokenDescriptor.Subject.Claims));
			}
			else
			{
				jobject = new JObject();
			}
			if (tokenDescriptor.Claims != null && tokenDescriptor.Claims.Count > 0)
			{
				jobject.Merge(JObject.FromObject(tokenDescriptor.Claims), new JsonMergeSettings
				{
					MergeArrayHandling = MergeArrayHandling.Replace
				});
			}
			if (tokenDescriptor.Audience != null)
			{
				if (jobject.ContainsKey("aud"))
				{
					LogHelper.LogInformation(LogHelper.FormatInvariant("IDX14113: A duplicate value for 'SecurityTokenDescriptor.{0}' exists in 'SecurityTokenDescriptor.Claims'. \nThe value of 'SecurityTokenDescriptor.{0}' is used.", new object[] { LogHelper.MarkAsNonPII("Audience") }), Array.Empty<object>());
				}
				jobject["aud"] = tokenDescriptor.Audience;
			}
			if (tokenDescriptor.Expires != null)
			{
				if (jobject.ContainsKey("exp"))
				{
					LogHelper.LogInformation(LogHelper.FormatInvariant("IDX14113: A duplicate value for 'SecurityTokenDescriptor.{0}' exists in 'SecurityTokenDescriptor.Claims'. \nThe value of 'SecurityTokenDescriptor.{0}' is used.", new object[] { LogHelper.MarkAsNonPII("Expires") }), Array.Empty<object>());
				}
				jobject["exp"] = EpochTime.GetIntDate(tokenDescriptor.Expires.Value);
			}
			if (tokenDescriptor.Issuer != null)
			{
				if (jobject.ContainsKey("iss"))
				{
					LogHelper.LogInformation(LogHelper.FormatInvariant("IDX14113: A duplicate value for 'SecurityTokenDescriptor.{0}' exists in 'SecurityTokenDescriptor.Claims'. \nThe value of 'SecurityTokenDescriptor.{0}' is used.", new object[] { LogHelper.MarkAsNonPII("Issuer") }), Array.Empty<object>());
				}
				jobject["iss"] = tokenDescriptor.Issuer;
			}
			if (tokenDescriptor.IssuedAt != null)
			{
				if (jobject.ContainsKey("iat"))
				{
					LogHelper.LogInformation(LogHelper.FormatInvariant("IDX14113: A duplicate value for 'SecurityTokenDescriptor.{0}' exists in 'SecurityTokenDescriptor.Claims'. \nThe value of 'SecurityTokenDescriptor.{0}' is used.", new object[] { LogHelper.MarkAsNonPII("IssuedAt") }), Array.Empty<object>());
				}
				jobject["iat"] = EpochTime.GetIntDate(tokenDescriptor.IssuedAt.Value);
			}
			if (tokenDescriptor.NotBefore != null)
			{
				if (jobject.ContainsKey("nbf"))
				{
					LogHelper.LogInformation(LogHelper.FormatInvariant("IDX14113: A duplicate value for 'SecurityTokenDescriptor.{0}' exists in 'SecurityTokenDescriptor.Claims'. \nThe value of 'SecurityTokenDescriptor.{0}' is used.", new object[] { LogHelper.MarkAsNonPII("NotBefore") }), Array.Empty<object>());
				}
				jobject["nbf"] = EpochTime.GetIntDate(tokenDescriptor.NotBefore.Value);
			}
			return this.CreateTokenPrivate(jobject.ToString(Formatting.None, Array.Empty<JsonConverter>()), tokenDescriptor.SigningCredentials, tokenDescriptor.EncryptingCredentials, tokenDescriptor.CompressionAlgorithm, tokenDescriptor.AdditionalHeaderClaims, tokenDescriptor.AdditionalInnerHeaderClaims, tokenDescriptor.TokenType);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003CD5 File Offset: 0x00001ED5
		public virtual string CreateToken(string payload, EncryptingCredentials encryptingCredentials)
		{
			if (string.IsNullOrEmpty(payload))
			{
				throw LogHelper.LogArgumentNullException("payload");
			}
			if (encryptingCredentials == null)
			{
				throw LogHelper.LogArgumentNullException("encryptingCredentials");
			}
			return this.CreateTokenPrivate(payload, null, encryptingCredentials, null, null, null, null);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003D05 File Offset: 0x00001F05
		public virtual string CreateToken(string payload, EncryptingCredentials encryptingCredentials, IDictionary<string, object> additionalHeaderClaims)
		{
			if (string.IsNullOrEmpty(payload))
			{
				throw LogHelper.LogArgumentNullException("payload");
			}
			if (encryptingCredentials == null)
			{
				throw LogHelper.LogArgumentNullException("encryptingCredentials");
			}
			if (additionalHeaderClaims == null)
			{
				throw LogHelper.LogArgumentNullException("additionalHeaderClaims");
			}
			return this.CreateTokenPrivate(payload, null, encryptingCredentials, null, additionalHeaderClaims, null, null);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003D43 File Offset: 0x00001F43
		public virtual string CreateToken(string payload, SigningCredentials signingCredentials, EncryptingCredentials encryptingCredentials)
		{
			if (string.IsNullOrEmpty(payload))
			{
				throw LogHelper.LogArgumentNullException("payload");
			}
			if (signingCredentials == null)
			{
				throw LogHelper.LogArgumentNullException("signingCredentials");
			}
			if (encryptingCredentials == null)
			{
				throw LogHelper.LogArgumentNullException("encryptingCredentials");
			}
			return this.CreateTokenPrivate(payload, signingCredentials, encryptingCredentials, null, null, null, null);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003D84 File Offset: 0x00001F84
		public virtual string CreateToken(string payload, SigningCredentials signingCredentials, EncryptingCredentials encryptingCredentials, IDictionary<string, object> additionalHeaderClaims)
		{
			if (string.IsNullOrEmpty(payload))
			{
				throw LogHelper.LogArgumentNullException("payload");
			}
			if (signingCredentials == null)
			{
				throw LogHelper.LogArgumentNullException("signingCredentials");
			}
			if (encryptingCredentials == null)
			{
				throw LogHelper.LogArgumentNullException("encryptingCredentials");
			}
			if (additionalHeaderClaims == null)
			{
				throw LogHelper.LogArgumentNullException("additionalHeaderClaims");
			}
			return this.CreateTokenPrivate(payload, signingCredentials, encryptingCredentials, null, additionalHeaderClaims, null, null);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003DE0 File Offset: 0x00001FE0
		public virtual string CreateToken(string payload, EncryptingCredentials encryptingCredentials, string compressionAlgorithm)
		{
			if (string.IsNullOrEmpty(payload))
			{
				throw LogHelper.LogArgumentNullException("payload");
			}
			if (encryptingCredentials == null)
			{
				throw LogHelper.LogArgumentNullException("encryptingCredentials");
			}
			if (string.IsNullOrEmpty(compressionAlgorithm))
			{
				throw LogHelper.LogArgumentNullException("compressionAlgorithm");
			}
			return this.CreateTokenPrivate(payload, null, encryptingCredentials, compressionAlgorithm, null, null, null);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003E30 File Offset: 0x00002030
		public virtual string CreateToken(string payload, SigningCredentials signingCredentials, EncryptingCredentials encryptingCredentials, string compressionAlgorithm)
		{
			if (string.IsNullOrEmpty(payload))
			{
				throw LogHelper.LogArgumentNullException("payload");
			}
			if (signingCredentials == null)
			{
				throw LogHelper.LogArgumentNullException("signingCredentials");
			}
			if (encryptingCredentials == null)
			{
				throw LogHelper.LogArgumentNullException("encryptingCredentials");
			}
			if (string.IsNullOrEmpty(compressionAlgorithm))
			{
				throw LogHelper.LogArgumentNullException("compressionAlgorithm");
			}
			return this.CreateTokenPrivate(payload, signingCredentials, encryptingCredentials, compressionAlgorithm, null, null, null);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003E90 File Offset: 0x00002090
		public virtual string CreateToken(string payload, SigningCredentials signingCredentials, EncryptingCredentials encryptingCredentials, string compressionAlgorithm, IDictionary<string, object> additionalHeaderClaims, IDictionary<string, object> additionalInnerHeaderClaims)
		{
			if (string.IsNullOrEmpty(payload))
			{
				throw LogHelper.LogArgumentNullException("payload");
			}
			if (signingCredentials == null)
			{
				throw LogHelper.LogArgumentNullException("signingCredentials");
			}
			if (encryptingCredentials == null)
			{
				throw LogHelper.LogArgumentNullException("encryptingCredentials");
			}
			if (string.IsNullOrEmpty(compressionAlgorithm))
			{
				throw LogHelper.LogArgumentNullException("compressionAlgorithm");
			}
			if (additionalHeaderClaims == null)
			{
				throw LogHelper.LogArgumentNullException("additionalHeaderClaims");
			}
			if (additionalInnerHeaderClaims == null)
			{
				throw LogHelper.LogArgumentNullException("additionalInnerHeaderClaims");
			}
			return this.CreateTokenPrivate(payload, signingCredentials, encryptingCredentials, compressionAlgorithm, additionalHeaderClaims, additionalInnerHeaderClaims, null);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003F10 File Offset: 0x00002110
		public virtual string CreateToken(string payload, SigningCredentials signingCredentials, EncryptingCredentials encryptingCredentials, string compressionAlgorithm, IDictionary<string, object> additionalHeaderClaims)
		{
			if (string.IsNullOrEmpty(payload))
			{
				throw LogHelper.LogArgumentNullException("payload");
			}
			if (signingCredentials == null)
			{
				throw LogHelper.LogArgumentNullException("signingCredentials");
			}
			if (encryptingCredentials == null)
			{
				throw LogHelper.LogArgumentNullException("encryptingCredentials");
			}
			if (string.IsNullOrEmpty(compressionAlgorithm))
			{
				throw LogHelper.LogArgumentNullException("compressionAlgorithm");
			}
			if (additionalHeaderClaims == null)
			{
				throw LogHelper.LogArgumentNullException("additionalHeaderClaims");
			}
			return this.CreateTokenPrivate(payload, signingCredentials, encryptingCredentials, compressionAlgorithm, additionalHeaderClaims, null, null);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003F80 File Offset: 0x00002180
		private string CreateTokenPrivate(string payload, SigningCredentials signingCredentials, EncryptingCredentials encryptingCredentials, string compressionAlgorithm, IDictionary<string, object> additionalHeaderClaims, IDictionary<string, object> additionalInnerHeaderClaims, string tokenType)
		{
			if (additionalHeaderClaims != null && additionalHeaderClaims.Count > 0 && additionalHeaderClaims.Keys.Intersect(JwtTokenUtilities.DefaultHeaderParameters, StringComparer.OrdinalIgnoreCase).Any<string>())
			{
				throw LogHelper.LogExceptionMessage(new SecurityTokenException(LogHelper.FormatInvariant("IDX14116: '{0}' cannot contain the following claims: '{1}'. These values are added by default (if necessary) during security token creation.", new object[]
				{
					LogHelper.MarkAsNonPII("additionalHeaderClaims"),
					LogHelper.MarkAsNonPII(string.Join(", ", JwtTokenUtilities.DefaultHeaderParameters))
				})));
			}
			if (additionalInnerHeaderClaims != null && additionalInnerHeaderClaims.Count > 0 && additionalInnerHeaderClaims.Keys.Intersect(JwtTokenUtilities.DefaultHeaderParameters, StringComparer.OrdinalIgnoreCase).Any<string>())
			{
				throw LogHelper.LogExceptionMessage(new SecurityTokenException(LogHelper.FormatInvariant("IDX14116: '{0}' cannot contain the following claims: '{1}'. These values are added by default (if necessary) during security token creation.", new object[]
				{
					"additionalInnerHeaderClaims",
					string.Join(", ", JwtTokenUtilities.DefaultHeaderParameters)
				})));
			}
			JObject jobject = JsonWebTokenHandler.CreateDefaultJWSHeader(signingCredentials, tokenType);
			if (encryptingCredentials == null && additionalHeaderClaims != null && additionalHeaderClaims.Count > 0)
			{
				jobject.Merge(JObject.FromObject(additionalHeaderClaims));
			}
			if (additionalInnerHeaderClaims != null && additionalInnerHeaderClaims.Count > 0)
			{
				jobject.Merge(JObject.FromObject(additionalInnerHeaderClaims));
			}
			string text = Base64UrlEncoder.Encode(Encoding.UTF8.GetBytes(jobject.ToString(Formatting.None, Array.Empty<JsonConverter>())));
			JObject jobject2 = null;
			try
			{
				if (base.SetDefaultTimesOnTokenCreation)
				{
					jobject2 = JObject.Parse(payload);
					if (jobject2 != null)
					{
						long intDate = EpochTime.GetIntDate(DateTime.UtcNow);
						JToken jtoken;
						if (!jobject2.TryGetValue("exp", out jtoken))
						{
							jobject2.Add("exp", intDate + (long)(base.TokenLifetimeInMinutes * 60));
						}
						if (!jobject2.TryGetValue("iat", out jtoken))
						{
							jobject2.Add("iat", intDate);
						}
						if (!jobject2.TryGetValue("nbf", out jtoken))
						{
							jobject2.Add("nbf", intDate);
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.LogExceptionMessage(new SecurityTokenException("IDX14307: JWE header is missing.", ex));
			}
			payload = ((jobject2 != null) ? jobject2.ToString(Formatting.None, Array.Empty<JsonConverter>()) : payload);
			string text2 = Base64UrlEncoder.Encode(Encoding.UTF8.GetBytes(payload));
			string text3 = text + "." + text2;
			string text4 = ((signingCredentials == null) ? string.Empty : JwtTokenUtilities.CreateEncodedSignature(text3, signingCredentials));
			if (encryptingCredentials != null)
			{
				additionalHeaderClaims = JsonWebTokenHandler.AddCtyClaimDefaultValue(additionalHeaderClaims, encryptingCredentials.SetDefaultCtyClaim);
				return JsonWebTokenHandler.EncryptTokenPrivate(text3 + "." + text4, encryptingCredentials, compressionAlgorithm, additionalHeaderClaims, tokenType);
			}
			return text3 + "." + text4;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000041F0 File Offset: 0x000023F0
		private static byte[] CompressToken(string token, string compressionAlgorithm)
		{
			if (token == null)
			{
				throw LogHelper.LogArgumentNullException("token");
			}
			if (string.IsNullOrEmpty(compressionAlgorithm))
			{
				throw LogHelper.LogArgumentNullException("compressionAlgorithm");
			}
			if (!CompressionProviderFactory.Default.IsSupportedAlgorithm(compressionAlgorithm))
			{
				throw LogHelper.LogExceptionMessage(new NotSupportedException(LogHelper.FormatInvariant("IDX10682: Compression algorithm '{0}' is not supported.", new object[] { LogHelper.MarkAsNonPII(compressionAlgorithm) })));
			}
			byte[] array = CompressionProviderFactory.Default.CreateCompressionProvider(compressionAlgorithm).Compress(Encoding.UTF8.GetBytes(token));
			if (array == null)
			{
				throw LogHelper.LogExceptionMessage(new InvalidOperationException(LogHelper.FormatInvariant("IDX10680: Failed to compress using algorithm '{0}'.", new object[] { LogHelper.MarkAsNonPII(compressionAlgorithm) })));
			}
			return array;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004292 File Offset: 0x00002492
		private static StringComparison GetStringComparisonRuleIf509(SecurityKey securityKey)
		{
			if (!(securityKey is X509SecurityKey))
			{
				return StringComparison.Ordinal;
			}
			return StringComparison.OrdinalIgnoreCase;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000429F File Offset: 0x0000249F
		private static StringComparison GetStringComparisonRuleIf509OrECDsa(SecurityKey securityKey)
		{
			if (!(securityKey is X509SecurityKey) && !(securityKey is ECDsaSecurityKey))
			{
				return StringComparison.Ordinal;
			}
			return StringComparison.OrdinalIgnoreCase;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000042B4 File Offset: 0x000024B4
		protected virtual ClaimsIdentity CreateClaimsIdentity(JsonWebToken jwtToken, TokenValidationParameters validationParameters)
		{
			if (jwtToken == null)
			{
				throw LogHelper.LogArgumentNullException("jwtToken");
			}
			return this.CreateClaimsIdentityPrivate(jwtToken, validationParameters, JsonWebTokenHandler.GetActualIssuer(jwtToken));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000042D2 File Offset: 0x000024D2
		protected virtual ClaimsIdentity CreateClaimsIdentity(JsonWebToken jwtToken, TokenValidationParameters validationParameters, string issuer)
		{
			if (jwtToken == null)
			{
				throw LogHelper.LogArgumentNullException("jwtToken");
			}
			if (string.IsNullOrWhiteSpace(issuer))
			{
				issuer = JsonWebTokenHandler.GetActualIssuer(jwtToken);
			}
			if (this.MapInboundClaims)
			{
				return this.CreateClaimsIdentityWithMapping(jwtToken, validationParameters, issuer);
			}
			return this.CreateClaimsIdentityPrivate(jwtToken, validationParameters, issuer);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004310 File Offset: 0x00002510
		private ClaimsIdentity CreateClaimsIdentityWithMapping(JsonWebToken jwtToken, TokenValidationParameters validationParameters, string issuer)
		{
			if (validationParameters == null)
			{
				throw LogHelper.LogArgumentNullException("validationParameters");
			}
			ClaimsIdentity claimsIdentity = validationParameters.CreateClaimsIdentity(jwtToken, issuer);
			foreach (Claim claim in jwtToken.Claims)
			{
				string type;
				bool flag = this._inboundClaimTypeMap.TryGetValue(claim.Type, out type);
				if (!flag)
				{
					type = claim.Type;
				}
				if (type == "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor")
				{
					if (claimsIdentity.Actor != null)
					{
						throw LogHelper.LogExceptionMessage(new InvalidOperationException(LogHelper.FormatInvariant("IDX14112: Only a single 'Actor' is supported. Found second claim of type: '{0}'", new object[]
						{
							LogHelper.MarkAsNonPII("actort"),
							claim.Value
						})));
					}
					if (this.CanReadToken(claim.Value))
					{
						JsonWebToken jsonWebToken = this.ReadToken(claim.Value) as JsonWebToken;
						claimsIdentity.Actor = this.CreateClaimsIdentity(jsonWebToken, validationParameters);
					}
				}
				if (flag)
				{
					Claim claim2 = new Claim(type, claim.Value, claim.ValueType, issuer, issuer, claimsIdentity);
					if (claim.Properties.Count > 0)
					{
						foreach (KeyValuePair<string, string> keyValuePair in claim.Properties)
						{
							claim2.Properties[keyValuePair.Key] = keyValuePair.Value;
						}
					}
					claim2.Properties[JsonWebTokenHandler.ShortClaimTypeProperty] = claim.Type;
					claimsIdentity.AddClaim(claim2);
				}
				else
				{
					claimsIdentity.AddClaim(claim);
				}
			}
			return claimsIdentity;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000044CC File Offset: 0x000026CC
		internal override ClaimsIdentity CreateClaimsIdentityInternal(SecurityToken securityToken, TokenValidationParameters tokenValidationParameters, string issuer)
		{
			return this.CreateClaimsIdentity(securityToken as JsonWebToken, tokenValidationParameters, issuer);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000044DC File Offset: 0x000026DC
		private static string GetActualIssuer(JsonWebToken jwtToken)
		{
			string text = jwtToken.Issuer;
			if (string.IsNullOrWhiteSpace(text))
			{
				LogHelper.LogVerbose("IDX10244: Issuer is null or empty. Using runtime default for creating claims '{0}'.", new object[] { "LOCAL AUTHORITY" });
				text = "LOCAL AUTHORITY";
			}
			return text;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004518 File Offset: 0x00002718
		private ClaimsIdentity CreateClaimsIdentityPrivate(JsonWebToken jwtToken, TokenValidationParameters validationParameters, string issuer)
		{
			if (validationParameters == null)
			{
				throw LogHelper.LogArgumentNullException("validationParameters");
			}
			ClaimsIdentity claimsIdentity = validationParameters.CreateClaimsIdentity(jwtToken, issuer);
			foreach (Claim claim in jwtToken.Claims)
			{
				string type = claim.Type;
				if (type == "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor")
				{
					if (claimsIdentity.Actor != null)
					{
						throw LogHelper.LogExceptionMessage(new InvalidOperationException(LogHelper.FormatInvariant("IDX14112: Only a single 'Actor' is supported. Found second claim of type: '{0}'", new object[]
						{
							LogHelper.MarkAsNonPII("actort"),
							claim.Value
						})));
					}
					if (this.CanReadToken(claim.Value))
					{
						JsonWebToken jsonWebToken = this.ReadToken(claim.Value) as JsonWebToken;
						claimsIdentity.Actor = this.CreateClaimsIdentity(jsonWebToken, validationParameters, issuer);
					}
				}
				if (claim.Properties.Count == 0)
				{
					claimsIdentity.AddClaim(new Claim(type, claim.Value, claim.ValueType, issuer, issuer, claimsIdentity));
				}
				else
				{
					Claim claim2 = new Claim(type, claim.Value, claim.ValueType, issuer, issuer, claimsIdentity);
					foreach (KeyValuePair<string, string> keyValuePair in claim.Properties)
					{
						claim2.Properties[keyValuePair.Key] = keyValuePair.Value;
					}
					claimsIdentity.AddClaim(claim2);
				}
			}
			return claimsIdentity;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000046B0 File Offset: 0x000028B0
		public string DecryptToken(JsonWebToken jwtToken, TokenValidationParameters validationParameters)
		{
			return this.DecryptToken(jwtToken, validationParameters, null);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000046BC File Offset: 0x000028BC
		private string DecryptToken(JsonWebToken jwtToken, TokenValidationParameters validationParameters, BaseConfiguration configuration)
		{
			if (jwtToken == null)
			{
				throw LogHelper.LogArgumentNullException("jwtToken");
			}
			if (validationParameters == null)
			{
				throw LogHelper.LogArgumentNullException("validationParameters");
			}
			if (string.IsNullOrEmpty(jwtToken.Enc))
			{
				throw LogHelper.LogExceptionMessage(new SecurityTokenException(LogHelper.FormatInvariant("IDX10612: Decryption failed. Header.Enc is null or empty, it must be specified.", Array.Empty<object>())));
			}
			IEnumerable<SecurityKey> contentEncryptionKeys = this.GetContentEncryptionKeys(jwtToken, validationParameters, configuration);
			return JwtTokenUtilities.DecryptJwtToken(jwtToken, validationParameters, new JwtTokenDecryptionParameters
			{
				DecompressionFunction = new Func<byte[], string, int, string>(JwtTokenUtilities.DecompressToken),
				Keys = contentEncryptionKeys,
				MaximumDeflateSize = this.MaximumTokenSizeInBytes
			});
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004747 File Offset: 0x00002947
		public string EncryptToken(string innerJwt, EncryptingCredentials encryptingCredentials)
		{
			if (string.IsNullOrEmpty(innerJwt))
			{
				throw LogHelper.LogArgumentNullException("innerJwt");
			}
			if (encryptingCredentials == null)
			{
				throw LogHelper.LogArgumentNullException("encryptingCredentials");
			}
			return JsonWebTokenHandler.EncryptTokenPrivate(innerJwt, encryptingCredentials, null, null, null);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004774 File Offset: 0x00002974
		public string EncryptToken(string innerJwt, EncryptingCredentials encryptingCredentials, IDictionary<string, object> additionalHeaderClaims)
		{
			if (string.IsNullOrEmpty(innerJwt))
			{
				throw LogHelper.LogArgumentNullException("innerJwt");
			}
			if (encryptingCredentials == null)
			{
				throw LogHelper.LogArgumentNullException("encryptingCredentials");
			}
			if (additionalHeaderClaims == null)
			{
				throw LogHelper.LogArgumentNullException("additionalHeaderClaims");
			}
			return JsonWebTokenHandler.EncryptTokenPrivate(innerJwt, encryptingCredentials, null, additionalHeaderClaims, null);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000047AF File Offset: 0x000029AF
		public string EncryptToken(string innerJwt, EncryptingCredentials encryptingCredentials, string algorithm)
		{
			if (string.IsNullOrEmpty(innerJwt))
			{
				throw LogHelper.LogArgumentNullException("innerJwt");
			}
			if (encryptingCredentials == null)
			{
				throw LogHelper.LogArgumentNullException("encryptingCredentials");
			}
			if (string.IsNullOrEmpty(algorithm))
			{
				throw LogHelper.LogArgumentNullException("algorithm");
			}
			return JsonWebTokenHandler.EncryptTokenPrivate(innerJwt, encryptingCredentials, algorithm, null, null);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000047F0 File Offset: 0x000029F0
		public string EncryptToken(string innerJwt, EncryptingCredentials encryptingCredentials, string algorithm, IDictionary<string, object> additionalHeaderClaims)
		{
			if (string.IsNullOrEmpty(innerJwt))
			{
				throw LogHelper.LogArgumentNullException("innerJwt");
			}
			if (encryptingCredentials == null)
			{
				throw LogHelper.LogArgumentNullException("encryptingCredentials");
			}
			if (string.IsNullOrEmpty(algorithm))
			{
				throw LogHelper.LogArgumentNullException("algorithm");
			}
			if (additionalHeaderClaims == null)
			{
				throw LogHelper.LogArgumentNullException("additionalHeaderClaims");
			}
			return JsonWebTokenHandler.EncryptTokenPrivate(innerJwt, encryptingCredentials, algorithm, additionalHeaderClaims, null);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000484C File Offset: 0x00002A4C
		private static string EncryptTokenPrivate(string innerJwt, EncryptingCredentials encryptingCredentials, string compressionAlgorithm, IDictionary<string, object> additionalHeaderClaims, string tokenType)
		{
			CryptoProviderFactory cryptoProviderFactory = encryptingCredentials.CryptoProviderFactory ?? encryptingCredentials.Key.CryptoProviderFactory;
			if (cryptoProviderFactory == null)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException("IDX10620: Unable to obtain a CryptoProviderFactory, both EncryptingCredentials.CryptoProviderFactory and EncryptingCredentials.Key.CrypoProviderFactory are null."));
			}
			byte[] array = null;
			SecurityKey securityKey = JwtTokenUtilities.GetSecurityKey(encryptingCredentials, cryptoProviderFactory, additionalHeaderClaims, out array);
			string text2;
			using (AuthenticatedEncryptionProvider authenticatedEncryptionProvider = cryptoProviderFactory.CreateAuthenticatedEncryptionProvider(securityKey, encryptingCredentials.Enc))
			{
				if (authenticatedEncryptionProvider == null)
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenEncryptionFailedException("IDX14103: Failed to create the token encryption provider."));
				}
				JObject jobject = JsonWebTokenHandler.CreateDefaultJWEHeader(encryptingCredentials, compressionAlgorithm, tokenType);
				if (additionalHeaderClaims != null)
				{
					jobject.Merge(JObject.FromObject(additionalHeaderClaims));
				}
				byte[] array2;
				if (!string.IsNullOrEmpty(compressionAlgorithm))
				{
					try
					{
						array2 = JsonWebTokenHandler.CompressToken(innerJwt, compressionAlgorithm);
						goto IL_00BA;
					}
					catch (Exception ex)
					{
						throw LogHelper.LogExceptionMessage(new SecurityTokenCompressionFailedException(LogHelper.FormatInvariant("IDX10680: Failed to compress using algorithm '{0}'.", new object[] { LogHelper.MarkAsNonPII(compressionAlgorithm) }), ex));
					}
				}
				array2 = Encoding.UTF8.GetBytes(innerJwt);
				IL_00BA:
				try
				{
					string text = Base64UrlEncoder.Encode(Encoding.UTF8.GetBytes(jobject.ToString(Formatting.None, Array.Empty<JsonConverter>())));
					AuthenticatedEncryptionResult authenticatedEncryptionResult = authenticatedEncryptionProvider.Encrypt(array2, Encoding.ASCII.GetBytes(text));
					text2 = ("dir".Equals(encryptingCredentials.Alg) ? string.Join(".", new string[]
					{
						text,
						string.Empty,
						Base64UrlEncoder.Encode(authenticatedEncryptionResult.IV),
						Base64UrlEncoder.Encode(authenticatedEncryptionResult.Ciphertext),
						Base64UrlEncoder.Encode(authenticatedEncryptionResult.AuthenticationTag)
					}) : string.Join(".", new string[]
					{
						text,
						Base64UrlEncoder.Encode(array),
						Base64UrlEncoder.Encode(authenticatedEncryptionResult.IV),
						Base64UrlEncoder.Encode(authenticatedEncryptionResult.Ciphertext),
						Base64UrlEncoder.Encode(authenticatedEncryptionResult.AuthenticationTag)
					}));
				}
				catch (Exception ex2)
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenEncryptionFailedException(LogHelper.FormatInvariant("IDX10616: Encryption failed. EncryptionProvider failed for: Algorithm: '{0}', SecurityKey: '{1}'. See inner exception.", new object[]
					{
						LogHelper.MarkAsNonPII(encryptingCredentials.Enc),
						encryptingCredentials.Key
					}), ex2));
				}
			}
			return text2;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004A84 File Offset: 0x00002C84
		private static SecurityKey ResolveTokenDecryptionKeyFromConfig(JsonWebToken jwtToken, BaseConfiguration configuration)
		{
			if (jwtToken == null)
			{
				throw LogHelper.LogArgumentNullException("jwtToken");
			}
			if (!string.IsNullOrEmpty(jwtToken.Kid) && configuration.TokenDecryptionKeys != null)
			{
				foreach (SecurityKey securityKey in configuration.TokenDecryptionKeys)
				{
					if (securityKey != null && string.Equals(securityKey.KeyId, jwtToken.Kid, JsonWebTokenHandler.GetStringComparisonRuleIf509OrECDsa(securityKey)))
					{
						return securityKey;
					}
				}
			}
			if (!string.IsNullOrEmpty(jwtToken.X5t) && configuration.TokenDecryptionKeys != null)
			{
				foreach (SecurityKey securityKey2 in configuration.TokenDecryptionKeys)
				{
					if (securityKey2 != null && string.Equals(securityKey2.KeyId, jwtToken.X5t, JsonWebTokenHandler.GetStringComparisonRuleIf509(securityKey2)))
					{
						return securityKey2;
					}
					X509SecurityKey x509SecurityKey = securityKey2 as X509SecurityKey;
					if (x509SecurityKey != null && string.Equals(x509SecurityKey.X5t, jwtToken.X5t, StringComparison.OrdinalIgnoreCase))
					{
						return securityKey2;
					}
				}
			}
			return null;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004BA4 File Offset: 0x00002DA4
		internal IEnumerable<SecurityKey> GetContentEncryptionKeys(JsonWebToken jwtToken, TokenValidationParameters validationParameters, BaseConfiguration configuration)
		{
			IEnumerable<SecurityKey> enumerable = null;
			if (validationParameters.TokenDecryptionKeyResolver != null)
			{
				enumerable = validationParameters.TokenDecryptionKeyResolver(jwtToken.EncodedToken, jwtToken, jwtToken.Kid, validationParameters);
			}
			else
			{
				SecurityKey securityKey = this.ResolveTokenDecryptionKey(jwtToken.EncodedToken, jwtToken, validationParameters);
				if (securityKey != null)
				{
					LogHelper.LogInformation("IDX10904: Token decryption key : '{0}' found in TokenValidationParameters.", new object[] { securityKey });
				}
				else if (configuration != null)
				{
					securityKey = JsonWebTokenHandler.ResolveTokenDecryptionKeyFromConfig(jwtToken, configuration);
					if (securityKey != null)
					{
						LogHelper.LogInformation("IDX10905: Token decryption key : '{0}' found in Configuration/Metadata.", new object[] { securityKey });
					}
				}
				if (securityKey != null)
				{
					enumerable = new List<SecurityKey> { securityKey };
				}
			}
			if (enumerable == null)
			{
				enumerable = JwtTokenUtilities.GetAllDecryptionKeys(validationParameters);
				if (configuration != null)
				{
					IEnumerable<SecurityKey> enumerable2;
					if (enumerable != null)
					{
						enumerable2 = enumerable.Concat(configuration.TokenDecryptionKeys);
					}
					else
					{
						IEnumerable<SecurityKey> tokenDecryptionKeys = configuration.TokenDecryptionKeys;
						enumerable2 = tokenDecryptionKeys;
					}
					enumerable = enumerable2;
				}
			}
			if (jwtToken.Alg.Equals("dir", StringComparison.Ordinal) || jwtToken.Alg.Equals("ECDH-ES", StringComparison.Ordinal))
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
					if (securityKey2.CryptoProviderFactory.IsSupportedAlgorithm(jwtToken.Alg, securityKey2))
					{
						byte[] array = securityKey2.CryptoProviderFactory.CreateKeyWrapProviderForUnwrap(securityKey2, jwtToken.Alg).UnwrapKey(jwtToken.EncryptedKeyBytes);
						list.Add(new SymmetricSecurityKey(array));
					}
				}
				catch (Exception ex)
				{
					stringBuilder.AppendLine(ex.ToString());
				}
				stringBuilder2.AppendLine(securityKey2.ToString());
			}
			if (list.Count > 0 && stringBuilder.Length == 0)
			{
				return list;
			}
			throw LogHelper.LogExceptionMessage(new SecurityTokenKeyWrapException(LogHelper.FormatInvariant("IDX10618: Key unwrap failed using decryption Keys: '{0}'.\nExceptions caught:\n '{1}'.\ntoken: '{2}'.", new object[] { stringBuilder2, stringBuilder, jwtToken })));
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004D84 File Offset: 0x00002F84
		protected virtual SecurityKey ResolveTokenDecryptionKey(string token, JsonWebToken jwtToken, TokenValidationParameters validationParameters)
		{
			if (jwtToken == null)
			{
				throw LogHelper.LogArgumentNullException("jwtToken");
			}
			if (validationParameters == null)
			{
				throw LogHelper.LogArgumentNullException("validationParameters");
			}
			StringComparison stringComparisonRuleIf509OrECDsa = JsonWebTokenHandler.GetStringComparisonRuleIf509OrECDsa(validationParameters.TokenDecryptionKey);
			if (!string.IsNullOrEmpty(jwtToken.Kid))
			{
				if (validationParameters.TokenDecryptionKey != null && string.Equals(validationParameters.TokenDecryptionKey.KeyId, jwtToken.Kid, stringComparisonRuleIf509OrECDsa))
				{
					return validationParameters.TokenDecryptionKey;
				}
				if (validationParameters.TokenDecryptionKeys != null)
				{
					foreach (SecurityKey securityKey in validationParameters.TokenDecryptionKeys)
					{
						if (securityKey != null && string.Equals(securityKey.KeyId, jwtToken.Kid, JsonWebTokenHandler.GetStringComparisonRuleIf509OrECDsa(securityKey)))
						{
							return securityKey;
						}
					}
				}
			}
			if (!string.IsNullOrEmpty(jwtToken.X5t))
			{
				if (validationParameters.TokenDecryptionKey != null)
				{
					if (string.Equals(validationParameters.TokenDecryptionKey.KeyId, jwtToken.X5t, stringComparisonRuleIf509OrECDsa))
					{
						return validationParameters.TokenDecryptionKey;
					}
					X509SecurityKey x509SecurityKey = validationParameters.TokenDecryptionKey as X509SecurityKey;
					if (x509SecurityKey != null && string.Equals(x509SecurityKey.X5t, jwtToken.X5t, StringComparison.OrdinalIgnoreCase))
					{
						return validationParameters.TokenDecryptionKey;
					}
				}
				if (validationParameters.TokenDecryptionKeys != null)
				{
					foreach (SecurityKey securityKey2 in validationParameters.TokenDecryptionKeys)
					{
						if (securityKey2 != null && string.Equals(securityKey2.KeyId, jwtToken.X5t, JsonWebTokenHandler.GetStringComparisonRuleIf509(securityKey2)))
						{
							return securityKey2;
						}
						X509SecurityKey x509SecurityKey2 = securityKey2 as X509SecurityKey;
						if (x509SecurityKey2 != null && string.Equals(x509SecurityKey2.X5t, jwtToken.X5t, StringComparison.OrdinalIgnoreCase))
						{
							return securityKey2;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004F44 File Offset: 0x00003144
		public virtual JsonWebToken ReadJsonWebToken(string token)
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
			return new JsonWebToken(token);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004FB9 File Offset: 0x000031B9
		public override SecurityToken ReadToken(string token)
		{
			return this.ReadJsonWebToken(token);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004FC4 File Offset: 0x000031C4
		public virtual TokenValidationResult ValidateToken(string token, TokenValidationParameters validationParameters)
		{
			return this.ValidateTokenAsync(token, validationParameters).ConfigureAwait(false).GetAwaiter()
				.GetResult();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004FF0 File Offset: 0x000031F0
		public override async Task<TokenValidationResult> ValidateTokenAsync(string token, TokenValidationParameters validationParameters)
		{
			TokenValidationResult tokenValidationResult;
			if (string.IsNullOrEmpty(token))
			{
				tokenValidationResult = new TokenValidationResult
				{
					Exception = LogHelper.LogArgumentNullException("token"),
					IsValid = false
				};
			}
			else if (validationParameters == null)
			{
				tokenValidationResult = new TokenValidationResult
				{
					Exception = LogHelper.LogArgumentNullException("validationParameters"),
					IsValid = false
				};
			}
			else if (token.Length > this.MaximumTokenSizeInBytes)
			{
				tokenValidationResult = new TokenValidationResult
				{
					Exception = LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10209: Token has length: '{0}' which is larger than the MaximumTokenSizeInBytes: '{1}'.", new object[]
					{
						LogHelper.MarkAsNonPII(token.Length),
						LogHelper.MarkAsNonPII(this.MaximumTokenSizeInBytes)
					}))),
					IsValid = false
				};
			}
			else
			{
				try
				{
					TokenValidationResult tokenValidationResult2 = JsonWebTokenHandler.ReadToken(token, validationParameters);
					if (tokenValidationResult2.IsValid)
					{
						tokenValidationResult = await this.ValidateTokenAsync(tokenValidationResult2.SecurityToken, validationParameters).ConfigureAwait(false);
					}
					else
					{
						tokenValidationResult = tokenValidationResult2;
					}
				}
				catch (Exception ex)
				{
					tokenValidationResult = new TokenValidationResult
					{
						Exception = ex,
						IsValid = false
					};
				}
			}
			return tokenValidationResult;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00005044 File Offset: 0x00003244
		public override async Task<TokenValidationResult> ValidateTokenAsync(SecurityToken token, TokenValidationParameters validationParameters)
		{
			if (token == null)
			{
				throw LogHelper.LogArgumentNullException("token");
			}
			TokenValidationResult tokenValidationResult;
			if (validationParameters == null)
			{
				tokenValidationResult = new TokenValidationResult
				{
					Exception = LogHelper.LogArgumentNullException("validationParameters"),
					IsValid = false
				};
			}
			else
			{
				JsonWebToken jsonWebToken = token as JsonWebToken;
				if (jsonWebToken == null)
				{
					tokenValidationResult = new TokenValidationResult
					{
						Exception = LogHelper.LogExceptionMessage(new SecurityTokenMalformedException("IDX14100: JWT is not well formed, there are no dots (.).\nThe token needs to be in JWS or JWE Compact Serialization Format. (JWS): 'EncodedHeader.EndcodedPayload.EncodedSignature'. (JWE): 'EncodedProtectedHeader.EncodedEncryptedKey.EncodedInitializationVector.EncodedCiphertext.EncodedAuthenticationTag'.")),
						IsValid = false
					};
				}
				else
				{
					try
					{
						tokenValidationResult = await this.ValidateTokenAsync(jsonWebToken, validationParameters).ConfigureAwait(false);
					}
					catch (Exception ex)
					{
						tokenValidationResult = new TokenValidationResult
						{
							Exception = ex,
							IsValid = false
						};
					}
				}
			}
			return tokenValidationResult;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00005098 File Offset: 0x00003298
		private static TokenValidationResult ReadToken(string token, TokenValidationParameters validationParameters)
		{
			JsonWebToken jsonWebToken = null;
			if (validationParameters.TokenReader != null)
			{
				SecurityToken securityToken = validationParameters.TokenReader(token, validationParameters);
				if (securityToken == null)
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenMalformedException(LogHelper.FormatInvariant("IDX10510: Token validation failed. The user defined 'Delegate' set on TokenValidationParameters.TokenReader returned null when reading token: '{0}'.", new object[] { LogHelper.MarkAsSecurityArtifact(token, new Func<object, string>(JwtTokenUtilities.SafeLogJwtToken)) })));
				}
				jsonWebToken = securityToken as JsonWebToken;
				if (jsonWebToken == null)
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenMalformedException(LogHelper.FormatInvariant("IDX10509: Token validation failed. The user defined 'Delegate' set on TokenValidationParameters.TokenReader did not return a '{0}', but returned a '{1}' when reading token: '{2}'.", new object[]
					{
						typeof(JsonWebToken),
						securityToken.GetType(),
						LogHelper.MarkAsSecurityArtifact(token, new Func<object, string>(JwtTokenUtilities.SafeLogJwtToken))
					})));
				}
			}
			else
			{
				try
				{
					jsonWebToken = new JsonWebToken(token);
				}
				catch (Exception ex)
				{
					return new TokenValidationResult
					{
						Exception = LogHelper.LogExceptionMessage(new SecurityTokenMalformedException("IDX14100: JWT is not well formed, there are no dots (.).\nThe token needs to be in JWS or JWE Compact Serialization Format. (JWS): 'EncodedHeader.EndcodedPayload.EncodedSignature'. (JWE): 'EncodedProtectedHeader.EncodedEncryptedKey.EncodedInitializationVector.EncodedCiphertext.EncodedAuthenticationTag'.", ex)),
						IsValid = false
					};
				}
			}
			return new TokenValidationResult
			{
				SecurityToken = jsonWebToken,
				IsValid = true
			};
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00005198 File Offset: 0x00003398
		private async Task<TokenValidationResult> ValidateTokenAsync(JsonWebToken jsonWebToken, TokenValidationParameters validationParameters)
		{
			BaseConfiguration currentConfiguration = null;
			if (validationParameters.ConfigurationManager != null)
			{
				try
				{
					BaseConfiguration baseConfiguration = await validationParameters.ConfigurationManager.GetBaseConfigurationAsync(CancellationToken.None).ConfigureAwait(false);
					currentConfiguration = baseConfiguration;
				}
				catch (Exception ex)
				{
					LogHelper.LogWarning(LogHelper.FormatInvariant("IDX10261: Unable to retrieve configuration from authority: '{0}'. \nProceeding with token validation in case the relevant properties have been set manually on the TokenValidationParameters. Exception caught: \n {1}. See https://aka.ms/validate-using-configuration-manager for additional information.", new object[]
					{
						validationParameters.ConfigurationManager.MetadataAddress,
						ex.ToString()
					}), Array.Empty<object>());
				}
			}
			TokenValidationResult tokenValidationResult = await this.ValidateTokenAsync(jsonWebToken, validationParameters, currentConfiguration).ConfigureAwait(false);
			if (validationParameters.ConfigurationManager != null)
			{
				if (tokenValidationResult.IsValid)
				{
					if (currentConfiguration != null)
					{
						validationParameters.ConfigurationManager.LastKnownGoodConfiguration = currentConfiguration;
					}
					return tokenValidationResult;
				}
				if (TokenUtilities.IsRecoverableException(tokenValidationResult.Exception))
				{
					if (currentConfiguration != null)
					{
						validationParameters.ConfigurationManager.RequestRefresh();
						validationParameters.RefreshBeforeValidation = true;
						BaseConfiguration lastConfig = currentConfiguration;
						BaseConfiguration baseConfiguration = await validationParameters.ConfigurationManager.GetBaseConfigurationAsync(CancellationToken.None).ConfigureAwait(false);
						currentConfiguration = baseConfiguration;
						if (lastConfig != currentConfiguration)
						{
							tokenValidationResult = await this.ValidateTokenAsync(jsonWebToken, validationParameters, currentConfiguration).ConfigureAwait(false);
							if (tokenValidationResult.IsValid)
							{
								validationParameters.ConfigurationManager.LastKnownGoodConfiguration = currentConfiguration;
								return tokenValidationResult;
							}
						}
						lastConfig = null;
					}
					if (validationParameters.ConfigurationManager.UseLastKnownGoodConfiguration)
					{
						validationParameters.RefreshBeforeValidation = false;
						validationParameters.ValidateWithLKG = true;
						Exception recoverableException = tokenValidationResult.Exception;
						foreach (BaseConfiguration baseConfiguration2 in validationParameters.ConfigurationManager.GetValidLkgConfigurations())
						{
							if (!baseConfiguration2.Equals(currentConfiguration) && TokenUtilities.IsRecoverableConfiguration(jsonWebToken.Kid, currentConfiguration, baseConfiguration2, recoverableException))
							{
								tokenValidationResult = await this.ValidateTokenAsync(jsonWebToken, validationParameters, baseConfiguration2).ConfigureAwait(false);
								if (tokenValidationResult.IsValid)
								{
									return tokenValidationResult;
								}
							}
						}
						IEnumerator<BaseConfiguration> enumerator = null;
						recoverableException = null;
					}
				}
			}
			return tokenValidationResult;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000051EC File Offset: 0x000033EC
		private async Task<TokenValidationResult> ValidateTokenAsync(JsonWebToken jsonWebToken, TokenValidationParameters validationParameters, BaseConfiguration configuration)
		{
			TokenValidationResult tokenValidationResult;
			if (jsonWebToken.IsEncrypted)
			{
				tokenValidationResult = await this.ValidateJWEAsync(jsonWebToken, validationParameters, configuration).ConfigureAwait(false);
			}
			else
			{
				tokenValidationResult = await this.ValidateJWSAsync(jsonWebToken, validationParameters, configuration).ConfigureAwait(false);
			}
			return tokenValidationResult;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00005248 File Offset: 0x00003448
		private async Task<TokenValidationResult> ValidateJWSAsync(JsonWebToken jsonWebToken, TokenValidationParameters validationParameters, BaseConfiguration configuration)
		{
			TokenValidationResult tokenValidationResult2;
			try
			{
				if (validationParameters.TransformBeforeSignatureValidation != null)
				{
					jsonWebToken = validationParameters.TransformBeforeSignatureValidation(jsonWebToken, validationParameters) as JsonWebToken;
				}
				TokenValidationResult tokenValidationResult;
				if (validationParameters.SignatureValidator != null || validationParameters.SignatureValidatorUsingConfiguration != null)
				{
					JsonWebToken validatedToken = JsonWebTokenHandler.ValidateSignatureUsingDelegates(jsonWebToken, validationParameters, configuration);
					tokenValidationResult = await this.ValidateTokenPayloadAsync(validatedToken, validationParameters, configuration).ConfigureAwait(false);
					Validators.ValidateIssuerSecurityKey(validatedToken.SigningKey, validatedToken, validationParameters, configuration);
					validatedToken = null;
				}
				else if (validationParameters.ValidateSignatureLast)
				{
					tokenValidationResult = await this.ValidateTokenPayloadAsync(jsonWebToken, validationParameters, configuration).ConfigureAwait(false);
					if (tokenValidationResult.IsValid)
					{
						tokenValidationResult.SecurityToken = JsonWebTokenHandler.ValidateSignatureAndIssuerSecurityKey(jsonWebToken, validationParameters, configuration);
					}
				}
				else
				{
					tokenValidationResult = await this.ValidateTokenPayloadAsync(JsonWebTokenHandler.ValidateSignatureAndIssuerSecurityKey(jsonWebToken, validationParameters, configuration), validationParameters, configuration).ConfigureAwait(false);
				}
				tokenValidationResult2 = tokenValidationResult;
			}
			catch (Exception ex)
			{
				tokenValidationResult2 = new TokenValidationResult
				{
					Exception = ex,
					IsValid = false,
					TokenOnFailedValidation = (validationParameters.IncludeTokenOnFailedValidation ? jsonWebToken : null)
				};
			}
			return tokenValidationResult2;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000052A4 File Offset: 0x000034A4
		private async Task<TokenValidationResult> ValidateJWEAsync(JsonWebToken jwtToken, TokenValidationParameters validationParameters, BaseConfiguration configuration)
		{
			TokenValidationResult tokenValidationResult2;
			try
			{
				TokenValidationResult tokenValidationResult = JsonWebTokenHandler.ReadToken(this.DecryptToken(jwtToken, validationParameters, configuration), validationParameters);
				if (!tokenValidationResult.IsValid)
				{
					tokenValidationResult2 = tokenValidationResult;
				}
				else
				{
					tokenValidationResult = await this.ValidateJWSAsync(tokenValidationResult.SecurityToken as JsonWebToken, validationParameters, configuration).ConfigureAwait(false);
					if (!tokenValidationResult.IsValid)
					{
						tokenValidationResult2 = tokenValidationResult;
					}
					else
					{
						jwtToken.InnerToken = tokenValidationResult.SecurityToken as JsonWebToken;
						jwtToken.Payload = (tokenValidationResult.SecurityToken as JsonWebToken).Payload;
						tokenValidationResult2 = new TokenValidationResult
						{
							SecurityToken = jwtToken,
							ClaimsIdentity = tokenValidationResult.ClaimsIdentity,
							IsValid = true,
							TokenType = tokenValidationResult.TokenType
						};
					}
				}
			}
			catch (Exception ex)
			{
				tokenValidationResult2 = new TokenValidationResult
				{
					Exception = ex,
					IsValid = false,
					TokenOnFailedValidation = (validationParameters.IncludeTokenOnFailedValidation ? jwtToken : null)
				};
			}
			return tokenValidationResult2;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00005300 File Offset: 0x00003500
		private static JsonWebToken ValidateSignatureUsingDelegates(JsonWebToken jsonWebToken, TokenValidationParameters validationParameters, BaseConfiguration configuration)
		{
			if (validationParameters.SignatureValidatorUsingConfiguration != null)
			{
				SecurityToken securityToken = validationParameters.SignatureValidatorUsingConfiguration(jsonWebToken.EncodedToken, validationParameters, configuration);
				if (securityToken == null)
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidSignatureException(LogHelper.FormatInvariant("IDX10505: Signature validation failed. The user defined 'Delegate' specified on TokenValidationParameters returned null when validating token: '{0}'.", new object[] { jsonWebToken })));
				}
				JsonWebToken jsonWebToken2 = securityToken as JsonWebToken;
				if (jsonWebToken2 == null)
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidSignatureException(LogHelper.FormatInvariant("IDX10506: Signature validation failed. The user defined 'Delegate' specified on TokenValidationParameters did not return a '{0}', but returned a '{1}' when validating token: '{2}'.", new object[]
					{
						LogHelper.MarkAsNonPII(typeof(JsonWebToken)),
						LogHelper.MarkAsNonPII(securityToken.GetType()),
						jsonWebToken
					})));
				}
				return jsonWebToken2;
			}
			else
			{
				if (validationParameters.SignatureValidator == null)
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidSignatureException(LogHelper.FormatInvariant("IDX10505: Signature validation failed. The user defined 'Delegate' specified on TokenValidationParameters returned null when validating token: '{0}'.", new object[] { jsonWebToken })));
				}
				SecurityToken securityToken2 = validationParameters.SignatureValidator(jsonWebToken.EncodedToken, validationParameters);
				if (securityToken2 == null)
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidSignatureException(LogHelper.FormatInvariant("IDX10505: Signature validation failed. The user defined 'Delegate' specified on TokenValidationParameters returned null when validating token: '{0}'.", new object[] { jsonWebToken })));
				}
				JsonWebToken jsonWebToken3 = securityToken2 as JsonWebToken;
				if (jsonWebToken3 == null)
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidSignatureException(LogHelper.FormatInvariant("IDX10506: Signature validation failed. The user defined 'Delegate' specified on TokenValidationParameters did not return a '{0}', but returned a '{1}' when validating token: '{2}'.", new object[]
					{
						LogHelper.MarkAsNonPII(typeof(JsonWebToken)),
						LogHelper.MarkAsNonPII(securityToken2.GetType()),
						jsonWebToken
					})));
				}
				return jsonWebToken3;
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00005442 File Offset: 0x00003642
		private static JsonWebToken ValidateSignatureAndIssuerSecurityKey(JsonWebToken jsonWebToken, TokenValidationParameters validationParameters, BaseConfiguration configuration)
		{
			JsonWebToken jsonWebToken2 = JsonWebTokenHandler.ValidateSignature(jsonWebToken, validationParameters, configuration);
			Validators.ValidateIssuerSecurityKey(jsonWebToken2.SigningKey, jsonWebToken, validationParameters, configuration);
			return jsonWebToken2;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000545C File Offset: 0x0000365C
		private async Task<TokenValidationResult> ValidateTokenPayloadAsync(JsonWebToken jsonWebToken, TokenValidationParameters validationParameters, BaseConfiguration configuration)
		{
			DateTime? expires = (jsonWebToken.HasPayloadClaim("exp") ? new DateTime?(jsonWebToken.ValidTo) : null);
			Validators.ValidateLifetime(jsonWebToken.HasPayloadClaim("nbf") ? new DateTime?(jsonWebToken.ValidFrom) : null, expires, jsonWebToken, validationParameters);
			Validators.ValidateAudience(jsonWebToken.Audiences, jsonWebToken, validationParameters);
			string text = await Validators.ValidateIssuerAsync(jsonWebToken.Issuer, jsonWebToken, validationParameters, configuration).ConfigureAwait(false);
			string issuer = text;
			Validators.ValidateTokenReplay(expires, jsonWebToken.EncodedToken, validationParameters);
			if (validationParameters.ValidateActor && !string.IsNullOrWhiteSpace(jsonWebToken.Actor))
			{
				TokenValidationResult tokenValidationResult = await this.ValidateTokenAsync(jsonWebToken.Actor, validationParameters.ActorValidationParameters ?? validationParameters).ConfigureAwait(false);
				if (!tokenValidationResult.IsValid)
				{
					return tokenValidationResult;
				}
			}
			string text2 = Validators.ValidateTokenType(jsonWebToken.Typ, jsonWebToken, validationParameters);
			return new TokenValidationResult(jsonWebToken, this, validationParameters.Clone(), issuer)
			{
				IsValid = true,
				TokenType = text2
			};
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000054B8 File Offset: 0x000036B8
		private static JsonWebToken ValidateSignature(JsonWebToken jwtToken, TokenValidationParameters validationParameters, BaseConfiguration configuration)
		{
			bool flag = false;
			IEnumerable<SecurityKey> enumerable = null;
			if (!jwtToken.IsSigned)
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
					enumerable = validationParameters.IssuerSigningKeyResolverUsingConfiguration(jwtToken.EncodedToken, jwtToken, jwtToken.Kid, validationParameters, configuration);
				}
				else if (validationParameters.IssuerSigningKeyResolver != null)
				{
					enumerable = validationParameters.IssuerSigningKeyResolver(jwtToken.EncodedToken, jwtToken, jwtToken.Kid, validationParameters);
				}
				else
				{
					SecurityKey securityKey = JwtTokenUtilities.ResolveTokenSigningKey(jwtToken.Kid, jwtToken.X5t, validationParameters, configuration);
					if (securityKey != null)
					{
						flag = true;
						enumerable = new List<SecurityKey> { securityKey };
					}
				}
				if (validationParameters.TryAllIssuerSigningKeys && enumerable.IsNullOrEmpty<SecurityKey>())
				{
					enumerable = TokenUtilities.GetAllSigningKeys(validationParameters, configuration);
				}
				StringBuilder stringBuilder = new StringBuilder();
				StringBuilder stringBuilder2 = new StringBuilder();
				bool flag2 = !string.IsNullOrEmpty(jwtToken.Kid);
				if (enumerable != null)
				{
					foreach (SecurityKey securityKey2 in enumerable)
					{
						try
						{
							if (JsonWebTokenHandler.ValidateSignature(jwtToken, securityKey2, validationParameters))
							{
								LogHelper.LogInformation("IDX10242: Security token: '{0}' has a valid signature.", new object[] { jwtToken });
								jwtToken.SigningKey = securityKey2;
								return jwtToken;
							}
						}
						catch (Exception ex)
						{
							stringBuilder.AppendLine(ex.ToString());
						}
						if (securityKey2 != null)
						{
							stringBuilder2.Append(securityKey2.ToString()).Append(" , KeyId: ").AppendLine(securityKey2.KeyId);
							if (flag2 && !flag && securityKey2.KeyId != null)
							{
								flag = jwtToken.Kid.Equals(securityKey2.KeyId, (securityKey2 is X509SecurityKey) ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
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
						string text = (allSigningKeys.Any((SecurityKey x) => x.KeyId.Equals(jwtToken.Kid)) ? "TokenValidationParameters" : "Configuration");
						throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidSignatureException(LogHelper.FormatInvariant("IDX10511: Signature validation failed. Keys tried: '{0}'. \nNumber of keys in TokenValidationParameters: '{1}'. \nNumber of keys in Configuration: '{2}'. \nMatched key was in '{3}'. \nkid: '{4}'. \nExceptions caught:\n '{5}'.\ntoken: '{6}'. See https://aka.ms/IDX10511 for details.", new object[]
						{
							stringBuilder2,
							LogHelper.MarkAsNonPII(num),
							LogHelper.MarkAsNonPII(num2),
							LogHelper.MarkAsNonPII(text),
							LogHelper.MarkAsNonPII(jwtToken.Kid),
							stringBuilder,
							jwtToken
						})));
					}
					Claim claim;
					DateTime? dateTime = (jwtToken.TryGetClaim("exp", out claim) ? new DateTime?(jwtToken.ValidTo) : null);
					DateTime? dateTime2 = (jwtToken.TryGetClaim("nbf", out claim) ? new DateTime?(jwtToken.ValidFrom) : null);
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

		// Token: 0x06000090 RID: 144 RVA: 0x0000589C File Offset: 0x00003A9C
		internal static bool ValidateSignature(byte[] encodedBytes, byte[] signature, SecurityKey key, string algorithm, SecurityToken securityToken, TokenValidationParameters validationParameters)
		{
			CryptoProviderFactory cryptoProviderFactory = validationParameters.CryptoProviderFactory ?? key.CryptoProviderFactory;
			if (!cryptoProviderFactory.IsSupportedAlgorithm(algorithm, key))
			{
				LogHelper.LogInformation("IDX14000: Signature validation of this JWT is not supported for: Algorithm: '{0}', SecurityKey: '{1}'.", new object[]
				{
					LogHelper.MarkAsNonPII(algorithm),
					key
				});
				return false;
			}
			Validators.ValidateAlgorithm(algorithm, key, securityToken, validationParameters);
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

		// Token: 0x06000091 RID: 145 RVA: 0x00005958 File Offset: 0x00003B58
		internal static bool IsSignatureValid(byte[] signatureBytes, int signatureBytesLength, SignatureProvider signatureProvider, byte[] dataToVerify, int dataToVerifyLength)
		{
			if (signatureProvider is SymmetricSignatureProvider)
			{
				return signatureProvider.Verify(dataToVerify, 0, dataToVerifyLength, signatureBytes, 0, signatureBytesLength);
			}
			if (signatureBytes.Length == signatureBytesLength)
			{
				return signatureProvider.Verify(dataToVerify, 0, dataToVerifyLength, signatureBytes, 0, signatureBytesLength);
			}
			byte[] array = new byte[signatureBytesLength];
			Array.Copy(signatureBytes, 0, array, 0, signatureBytesLength);
			return signatureProvider.Verify(dataToVerify, 0, dataToVerifyLength, array, 0, signatureBytesLength);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000059AD File Offset: 0x00003BAD
		internal static bool ValidateSignature(byte[] bytes, int len, string stringWithSignature, int signatureStartIndex, SignatureProvider signatureProvider)
		{
			return Base64UrlEncoding.Decode<bool, SignatureProvider, byte[], int>(stringWithSignature, signatureStartIndex + 1, stringWithSignature.Length - signatureStartIndex - 1, signatureProvider, bytes, len, new Func<byte[], int, SignatureProvider, byte[], int, bool>(JsonWebTokenHandler.IsSignatureValid));
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000059D4 File Offset: 0x00003BD4
		internal static bool ValidateSignature(JsonWebToken jsonWebToken, SecurityKey key, TokenValidationParameters validationParameters)
		{
			CryptoProviderFactory cryptoProviderFactory = validationParameters.CryptoProviderFactory ?? key.CryptoProviderFactory;
			if (!cryptoProviderFactory.IsSupportedAlgorithm(jsonWebToken.Alg, key))
			{
				LogHelper.LogInformation("IDX14000: Signature validation of this JWT is not supported for: Algorithm: '{0}', SecurityKey: '{1}'.", new object[]
				{
					LogHelper.MarkAsNonPII(jsonWebToken.Alg),
					key
				});
				return false;
			}
			Validators.ValidateAlgorithm(jsonWebToken.Alg, key, jsonWebToken, validationParameters);
			SignatureProvider signatureProvider = cryptoProviderFactory.CreateForVerifying(key, jsonWebToken.Alg);
			bool flag;
			try
			{
				if (signatureProvider == null)
				{
					throw LogHelper.LogExceptionMessage(new InvalidOperationException(LogHelper.FormatInvariant("IDX10636: CryptoProviderFactory.CreateForVerifying returned null for key: '{0}', signatureAlgorithm: '{1}'.", new object[]
					{
						(key == null) ? "Null" : key.ToString(),
						LogHelper.MarkAsNonPII(jsonWebToken.Alg)
					})));
				}
				flag = EncodingUtils.PerformEncodingDependentOperation<bool, string, int, SignatureProvider>(jsonWebToken.EncodedToken, 0, jsonWebToken.Dot2, Encoding.UTF8, jsonWebToken.EncodedToken, jsonWebToken.Dot2, signatureProvider, new Func<byte[], int, string, int, SignatureProvider, bool>(JsonWebTokenHandler.ValidateSignature));
			}
			finally
			{
				cryptoProviderFactory.ReleaseSignatureProvider(signatureProvider);
			}
			return flag;
		}

		// Token: 0x04000035 RID: 53
		private IDictionary<string, string> _inboundClaimTypeMap;

		// Token: 0x04000036 RID: 54
		private const string _namespace = "http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties";

		// Token: 0x04000037 RID: 55
		private static string _shortClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/ShortTypeName";

		// Token: 0x04000038 RID: 56
		private bool _mapInboundClaims = JsonWebTokenHandler.DefaultMapInboundClaims;

		// Token: 0x04000039 RID: 57
		public static IDictionary<string, string> DefaultInboundClaimTypeMap = new Dictionary<string, string>(ClaimTypeMapping.InboundClaimTypeMap);

		// Token: 0x0400003A RID: 58
		public static bool DefaultMapInboundClaims = false;

		// Token: 0x0400003B RID: 59
		public const string Base64UrlEncodedUnsignedJWSHeader = "eyJhbGciOiJub25lIn0";
	}
}
