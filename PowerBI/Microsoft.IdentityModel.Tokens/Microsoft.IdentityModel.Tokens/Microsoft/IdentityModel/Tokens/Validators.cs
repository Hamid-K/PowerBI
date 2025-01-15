using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000193 RID: 403
	public static class Validators
	{
		// Token: 0x06001223 RID: 4643 RVA: 0x000436C0 File Offset: 0x000418C0
		public static void ValidateAlgorithm(string algorithm, SecurityKey securityKey, SecurityToken securityToken, TokenValidationParameters validationParameters)
		{
			if (validationParameters == null)
			{
				throw LogHelper.LogArgumentNullException("validationParameters");
			}
			if (validationParameters.AlgorithmValidator != null)
			{
				if (!validationParameters.AlgorithmValidator(algorithm, securityKey, securityToken, validationParameters))
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidAlgorithmException(LogHelper.FormatInvariant("IDX10697: The user defined 'Delegate' AlgorithmValidator specified on TokenValidationParameters returned false when validating Algorithm: '{0}', SecurityKey: '{1}'.", new object[]
					{
						LogHelper.MarkAsNonPII(algorithm),
						securityKey
					}))
					{
						InvalidAlgorithm = algorithm
					});
				}
				return;
			}
			else
			{
				if (validationParameters.ValidAlgorithms != null && validationParameters.ValidAlgorithms.Any<string>() && !validationParameters.ValidAlgorithms.Contains(algorithm, StringComparer.Ordinal))
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidAlgorithmException(LogHelper.FormatInvariant("IDX10696: The algorithm '{0}' is not in the user-defined accepted list of algorithms.", new object[] { LogHelper.MarkAsNonPII(algorithm) }))
					{
						InvalidAlgorithm = algorithm
					});
				}
				return;
			}
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00043778 File Offset: 0x00041978
		public static void ValidateAudience(IEnumerable<string> audiences, SecurityToken securityToken, TokenValidationParameters validationParameters)
		{
			if (validationParameters == null)
			{
				throw LogHelper.LogArgumentNullException("validationParameters");
			}
			if (validationParameters.AudienceValidator != null)
			{
				if (!validationParameters.AudienceValidator(audiences, securityToken, validationParameters))
				{
					string text = "IDX10231: Audience validation failed. Delegate returned false, securitytoken: '{0}'.";
					object[] array = new object[1];
					array[0] = LogHelper.MarkAsUnsafeSecurityArtifact(securityToken, (object t) => t.ToString());
					throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidAudienceException(LogHelper.FormatInvariant(text, array))
					{
						InvalidAudience = Utility.SerializeAsSingleCommaDelimitedString(audiences)
					});
				}
				return;
			}
			else
			{
				if (!validationParameters.ValidateAudience)
				{
					LogHelper.LogWarning("IDX10233: ValidateAudience property on ValidationParameters is set to false. Exiting without validating the audience.", Array.Empty<object>());
					return;
				}
				if (audiences == null)
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidAudienceException("IDX10207: Unable to validate audience. The 'audiences' parameter is null.")
					{
						InvalidAudience = null
					});
				}
				if (string.IsNullOrWhiteSpace(validationParameters.ValidAudience) && validationParameters.ValidAudiences == null)
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidAudienceException("IDX10208: Unable to validate audience. validationParameters.ValidAudience is null or whitespace and validationParameters.ValidAudiences is null.")
					{
						InvalidAudience = Utility.SerializeAsSingleCommaDelimitedString(audiences)
					});
				}
				if (!audiences.Any<string>())
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidAudienceException(LogHelper.FormatInvariant("IDX10206: Unable to validate audience. The 'audiences' parameter is empty.", Array.Empty<object>()))
					{
						InvalidAudience = Utility.SerializeAsSingleCommaDelimitedString(audiences)
					});
				}
				IEnumerable<string> enumerable;
				if (validationParameters.ValidAudiences == null)
				{
					enumerable = new string[] { validationParameters.ValidAudience };
				}
				else if (string.IsNullOrWhiteSpace(validationParameters.ValidAudience))
				{
					enumerable = validationParameters.ValidAudiences;
				}
				else
				{
					enumerable = validationParameters.ValidAudiences.Concat(new string[] { validationParameters.ValidAudience });
				}
				if (Validators.AudienceIsValid(audiences, validationParameters, enumerable))
				{
					return;
				}
				SecurityTokenInvalidAudienceException ex = new SecurityTokenInvalidAudienceException(LogHelper.FormatInvariant("IDX10214: Audience validation failed. Audiences: '{0}'. Did not match: validationParameters.ValidAudience: '{1}' or validationParameters.ValidAudiences: '{2}'.", new object[]
				{
					LogHelper.MarkAsNonPII(Utility.SerializeAsSingleCommaDelimitedString(audiences)),
					LogHelper.MarkAsNonPII(validationParameters.ValidAudience ?? "null"),
					LogHelper.MarkAsNonPII(Utility.SerializeAsSingleCommaDelimitedString(validationParameters.ValidAudiences))
				}))
				{
					InvalidAudience = Utility.SerializeAsSingleCommaDelimitedString(audiences)
				};
				if (!validationParameters.LogValidationExceptions)
				{
					throw ex;
				}
				throw LogHelper.LogExceptionMessage(ex);
			}
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x0004394C File Offset: 0x00041B4C
		private static bool AudienceIsValid(IEnumerable<string> audiences, TokenValidationParameters validationParameters, IEnumerable<string> validationParametersAudiences)
		{
			foreach (string text in audiences)
			{
				if (!string.IsNullOrWhiteSpace(text))
				{
					foreach (string text2 in validationParametersAudiences)
					{
						if (!string.IsNullOrWhiteSpace(text2) && Validators.AudiencesMatch(validationParameters, text, text2))
						{
							LogHelper.LogInformation("IDX10234: Audience Validated.Audience: '{0}'", new object[] { LogHelper.MarkAsNonPII(text) });
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x000439FC File Offset: 0x00041BFC
		private static bool AudiencesMatch(TokenValidationParameters validationParameters, string tokenAudience, string validAudience)
		{
			if (validAudience.Length == tokenAudience.Length)
			{
				if (string.Equals(validAudience, tokenAudience))
				{
					return true;
				}
			}
			else if (validationParameters.IgnoreTrailingSlashWhenValidatingAudience && Validators.AudiencesMatchIgnoringTrailingSlash(tokenAudience, validAudience))
			{
				return true;
			}
			return false;
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00043A2C File Offset: 0x00041C2C
		private static bool AudiencesMatchIgnoringTrailingSlash(string tokenAudience, string validAudience)
		{
			int num = -1;
			if (validAudience.Length == tokenAudience.Length + 1 && validAudience.EndsWith("/", StringComparison.InvariantCulture))
			{
				num = validAudience.Length - 1;
			}
			else if (tokenAudience.Length == validAudience.Length + 1 && tokenAudience.EndsWith("/", StringComparison.InvariantCulture))
			{
				num = tokenAudience.Length - 1;
			}
			if (num == -1)
			{
				return false;
			}
			if (string.CompareOrdinal(validAudience, 0, tokenAudience, 0, num) == 0)
			{
				LogHelper.LogInformation("IDX10234: Audience Validated.Audience: '{0}'", new object[] { LogHelper.MarkAsNonPII(tokenAudience) });
				return true;
			}
			return false;
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00043AB9 File Offset: 0x00041CB9
		public static string ValidateIssuer(string issuer, SecurityToken securityToken, TokenValidationParameters validationParameters)
		{
			return Validators.ValidateIssuer(issuer, securityToken, validationParameters, null);
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x00043AC4 File Offset: 0x00041CC4
		internal static string ValidateIssuer(string issuer, SecurityToken securityToken, TokenValidationParameters validationParameters, BaseConfiguration configuration)
		{
			return Validators.ValidateIssuerAsync(issuer, securityToken, validationParameters, configuration).GetAwaiter().GetResult();
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00043AE8 File Offset: 0x00041CE8
		internal static async Task<string> ValidateIssuerAsync(string issuer, SecurityToken securityToken, TokenValidationParameters validationParameters, BaseConfiguration configuration)
		{
			if (validationParameters == null)
			{
				throw LogHelper.LogArgumentNullException("validationParameters");
			}
			string text;
			if (validationParameters.IssuerValidatorAsync != null)
			{
				text = await validationParameters.IssuerValidatorAsync(issuer, securityToken, validationParameters).ConfigureAwait(false);
			}
			else if (validationParameters.IssuerValidatorUsingConfiguration != null)
			{
				text = validationParameters.IssuerValidatorUsingConfiguration(issuer, securityToken, validationParameters, configuration);
			}
			else if (validationParameters.IssuerValidator != null)
			{
				text = validationParameters.IssuerValidator(issuer, securityToken, validationParameters);
			}
			else if (!validationParameters.ValidateIssuer)
			{
				LogHelper.LogWarning("IDX10235: ValidateIssuer property on ValidationParameters is set to false. Exiting without validating the issuer.", Array.Empty<object>());
				text = issuer;
			}
			else
			{
				if (string.IsNullOrWhiteSpace(issuer))
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidIssuerException("IDX10211: Unable to validate issuer. The 'issuer' parameter is null or whitespace")
					{
						InvalidIssuer = issuer
					});
				}
				if (string.IsNullOrWhiteSpace(validationParameters.ValidIssuer) && validationParameters.ValidIssuers.IsNullOrEmpty<string>() && string.IsNullOrWhiteSpace((configuration != null) ? configuration.Issuer : null))
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidIssuerException("IDX10204: Unable to validate issuer. validationParameters.ValidIssuer is null or whitespace AND validationParameters.ValidIssuers is null or empty.")
					{
						InvalidIssuer = issuer
					});
				}
				if (configuration != null && string.Equals(configuration.Issuer, issuer))
				{
					LogHelper.LogInformation("IDX10236: Issuer Validated.Issuer: '{0}'", new object[] { LogHelper.MarkAsNonPII(issuer) });
					text = issuer;
				}
				else if (string.Equals(validationParameters.ValidIssuer, issuer))
				{
					LogHelper.LogInformation("IDX10236: Issuer Validated.Issuer: '{0}'", new object[] { LogHelper.MarkAsNonPII(issuer) });
					text = issuer;
				}
				else
				{
					if (validationParameters.ValidIssuers != null)
					{
						foreach (string text2 in validationParameters.ValidIssuers)
						{
							if (string.IsNullOrEmpty(text2))
							{
								LogHelper.LogInformation("IDX10262: One of the issuers in TokenValidationParameters.ValidIssuers was null or an empty string. See https://aka.ms/wilson/tokenvalidation for details.", Array.Empty<object>());
							}
							else if (string.Equals(text2, issuer))
							{
								LogHelper.LogInformation("IDX10236: Issuer Validated.Issuer: '{0}'", new object[] { LogHelper.MarkAsNonPII(issuer) });
								return issuer;
							}
						}
					}
					SecurityTokenInvalidIssuerException ex = new SecurityTokenInvalidIssuerException(LogHelper.FormatInvariant("IDX10205: Issuer validation failed. Issuer: '{0}'. Did not match: validationParameters.ValidIssuer: '{1}' or validationParameters.ValidIssuers: '{2}' or validationParameters.ConfigurationManager.CurrentConfiguration.Issuer: '{3}'. For more details, see https://aka.ms/IdentityModel/issuer-validation. ", new object[]
					{
						LogHelper.MarkAsNonPII(issuer),
						LogHelper.MarkAsNonPII(validationParameters.ValidIssuer ?? "null"),
						LogHelper.MarkAsNonPII(Utility.SerializeAsSingleCommaDelimitedString(validationParameters.ValidIssuers)),
						LogHelper.MarkAsNonPII((configuration != null) ? configuration.Issuer : null)
					}))
					{
						InvalidIssuer = issuer
					};
					if (!validationParameters.LogValidationExceptions)
					{
						throw ex;
					}
					throw LogHelper.LogExceptionMessage(ex);
				}
			}
			return text;
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00043B43 File Offset: 0x00041D43
		public static void ValidateIssuerSecurityKey(SecurityKey securityKey, SecurityToken securityToken, TokenValidationParameters validationParameters)
		{
			Validators.ValidateIssuerSecurityKey(securityKey, securityToken, validationParameters, null);
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00043B50 File Offset: 0x00041D50
		internal static void ValidateIssuerSecurityKey(SecurityKey securityKey, SecurityToken securityToken, TokenValidationParameters validationParameters, BaseConfiguration configuration)
		{
			if (validationParameters == null)
			{
				throw LogHelper.LogArgumentNullException("validationParameters");
			}
			if (validationParameters.IssuerSigningKeyValidatorUsingConfiguration != null)
			{
				if (!validationParameters.IssuerSigningKeyValidatorUsingConfiguration(securityKey, securityToken, validationParameters, configuration))
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidSigningKeyException(LogHelper.FormatInvariant("IDX10232: IssuerSigningKey validation failed. Delegate returned false, securityKey: '{0}'.", new object[] { securityKey }))
					{
						SigningKey = securityKey
					});
				}
				return;
			}
			else if (validationParameters.IssuerSigningKeyValidator != null)
			{
				if (!validationParameters.IssuerSigningKeyValidator(securityKey, securityToken, validationParameters))
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidSigningKeyException(LogHelper.FormatInvariant("IDX10232: IssuerSigningKey validation failed. Delegate returned false, securityKey: '{0}'.", new object[] { securityKey }))
					{
						SigningKey = securityKey
					});
				}
				return;
			}
			else
			{
				if (!validationParameters.ValidateIssuerSigningKey)
				{
					LogHelper.LogVerbose("IDX10237: ValidateIssuerSigningKey property on ValidationParameters is set to false. Exiting without validating the issuer signing key.", Array.Empty<object>());
					return;
				}
				if (!validationParameters.RequireSignedTokens && securityKey == null)
				{
					LogHelper.LogInformation("IDX10252: RequireSignedTokens property on ValidationParameters is set to false and the issuer signing key is null. Exiting without validating the issuer signing key.", Array.Empty<object>());
					return;
				}
				if (securityKey == null)
				{
					throw LogHelper.LogExceptionMessage(new ArgumentNullException("securityKey", "IDX10253: RequireSignedTokens property on ValidationParameters is set to true, but the issuer signing key is null."));
				}
				if (securityToken == null)
				{
					throw LogHelper.LogArgumentNullException("securityToken");
				}
				Validators.ValidateIssuerSigningKeyLifeTime(securityKey, validationParameters);
				return;
			}
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00043C4C File Offset: 0x00041E4C
		internal static void ValidateIssuerSigningKeyLifeTime(SecurityKey securityKey, TokenValidationParameters validationParameters)
		{
			X509SecurityKey x509SecurityKey = securityKey as X509SecurityKey;
			X509Certificate2 x509Certificate = ((x509SecurityKey != null) ? x509SecurityKey.Certificate : null);
			if (x509Certificate != null)
			{
				DateTime utcNow = DateTime.UtcNow;
				DateTime dateTime = x509Certificate.NotBefore.ToUniversalTime();
				DateTime dateTime2 = x509Certificate.NotAfter.ToUniversalTime();
				if (dateTime > DateTimeUtil.Add(utcNow, validationParameters.ClockSkew))
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidSigningKeyException(LogHelper.FormatInvariant("IDX10248: X509SecurityKey validation failed. The associated certificate is not yet valid. ValidFrom (UTC): '{0}', Current time (UTC): '{1}'.", new object[]
					{
						LogHelper.MarkAsNonPII(dateTime),
						LogHelper.MarkAsNonPII(utcNow)
					})));
				}
				LogHelper.LogInformation("IDX10250: The associated certificate is valid. ValidFrom (UTC): '{0}', Current time (UTC): '{1}'.", new object[]
				{
					LogHelper.MarkAsNonPII(dateTime),
					LogHelper.MarkAsNonPII(utcNow)
				});
				if (dateTime2 < DateTimeUtil.Add(utcNow, validationParameters.ClockSkew.Negate()))
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidSigningKeyException(LogHelper.FormatInvariant("IDX10249: X509SecurityKey validation failed. The associated certificate has expired. ValidTo (UTC): '{0}', Current time (UTC): '{1}'.", new object[]
					{
						LogHelper.MarkAsNonPII(dateTime2),
						LogHelper.MarkAsNonPII(utcNow)
					})));
				}
				LogHelper.LogInformation("IDX10251: The associated certificate is valid. ValidTo (UTC): '{0}', Current time (UTC): '{1}'.", new object[]
				{
					LogHelper.MarkAsNonPII(dateTime2),
					LogHelper.MarkAsNonPII(utcNow)
				});
			}
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x00043D90 File Offset: 0x00041F90
		public static void ValidateLifetime(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
		{
			if (validationParameters == null)
			{
				throw LogHelper.LogArgumentNullException("validationParameters");
			}
			if (validationParameters.LifetimeValidator != null)
			{
				if (!validationParameters.LifetimeValidator(notBefore, expires, securityToken, validationParameters))
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidLifetimeException(LogHelper.FormatInvariant("IDX10230: Lifetime validation failed. Delegate returned false, securitytoken: '{0}'.", new object[] { securityToken }))
					{
						NotBefore = notBefore,
						Expires = expires
					});
				}
				return;
			}
			else
			{
				if (!validationParameters.ValidateLifetime)
				{
					LogHelper.LogInformation("IDX10238: ValidateLifetime property on ValidationParameters is set to false. Exiting without validating the lifetime.", Array.Empty<object>());
					return;
				}
				if (expires == null && validationParameters.RequireExpirationTime)
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenNoExpirationException(LogHelper.FormatInvariant("IDX10225: Lifetime validation failed. The token is missing an Expiration Time. Tokentype: '{0}'.", new object[] { LogHelper.MarkAsNonPII((securityToken == null) ? "null" : securityToken.GetType().ToString()) })));
				}
				if (notBefore != null && expires != null && notBefore.Value > expires.Value)
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidLifetimeException(LogHelper.FormatInvariant("IDX10224: Lifetime validation failed. The NotBefore (UTC): '{0}' is after Expires (UTC): '{1}'.", new object[]
					{
						LogHelper.MarkAsNonPII(notBefore.Value),
						LogHelper.MarkAsNonPII(expires.Value)
					}))
					{
						NotBefore = notBefore,
						Expires = expires
					});
				}
				DateTime utcNow = DateTime.UtcNow;
				if (notBefore != null && notBefore.Value > DateTimeUtil.Add(utcNow, validationParameters.ClockSkew))
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenNotYetValidException(LogHelper.FormatInvariant("IDX10222: Lifetime validation failed. The token is not yet valid. ValidFrom (UTC): '{0}', Current time (UTC): '{1}'.", new object[]
					{
						LogHelper.MarkAsNonPII(notBefore.Value),
						LogHelper.MarkAsNonPII(utcNow)
					}))
					{
						NotBefore = notBefore.Value
					});
				}
				if (expires != null && expires.Value < DateTimeUtil.Add(utcNow, validationParameters.ClockSkew.Negate()))
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenExpiredException(LogHelper.FormatInvariant("IDX10223: Lifetime validation failed. The token is expired. ValidTo (UTC): '{0}', Current time (UTC): '{1}'.", new object[]
					{
						LogHelper.MarkAsNonPII(expires.Value),
						LogHelper.MarkAsNonPII(utcNow)
					}))
					{
						Expires = expires.Value
					});
				}
				LogHelper.LogInformation("IDX10239: Lifetime of the token is valid.", Array.Empty<object>());
				return;
			}
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00043FC4 File Offset: 0x000421C4
		public static void ValidateTokenReplay(DateTime? expirationTime, string securityToken, TokenValidationParameters validationParameters)
		{
			if (string.IsNullOrWhiteSpace(securityToken))
			{
				throw LogHelper.LogArgumentNullException("securityToken");
			}
			if (validationParameters == null)
			{
				throw LogHelper.LogArgumentNullException("validationParameters");
			}
			if (validationParameters.TokenReplayValidator != null)
			{
				if (!validationParameters.TokenReplayValidator(expirationTime, securityToken, validationParameters))
				{
					string text = "IDX10228: The securityToken has previously been validated, securityToken: '{0}'.";
					object[] array = new object[1];
					array[0] = LogHelper.MarkAsUnsafeSecurityArtifact(securityToken, (object t) => t.ToString());
					throw LogHelper.LogExceptionMessage(new SecurityTokenReplayDetectedException(LogHelper.FormatInvariant(text, array)));
				}
				return;
			}
			else
			{
				if (!validationParameters.ValidateTokenReplay)
				{
					LogHelper.LogVerbose("IDX10246: ValidateTokenReplay property on ValidationParameters is set to false. Exiting without validating the token replay.", Array.Empty<object>());
					return;
				}
				if (validationParameters.TokenReplayCache != null)
				{
					if (expirationTime == null)
					{
						throw LogHelper.LogExceptionMessage(new SecurityTokenNoExpirationException(LogHelper.FormatInvariant("IDX10227: TokenValidationParameters.TokenReplayCache is not null, indicating to check for token replay but the security token has no expiration time: token '{0}'.", new object[] { securityToken })));
					}
					if (validationParameters.TokenReplayCache.TryFind(securityToken))
					{
						throw LogHelper.LogExceptionMessage(new SecurityTokenReplayDetectedException(LogHelper.FormatInvariant("IDX10228: The securityToken has previously been validated, securityToken: '{0}'.", new object[] { securityToken })));
					}
					if (!validationParameters.TokenReplayCache.TryAdd(securityToken, expirationTime.Value))
					{
						throw LogHelper.LogExceptionMessage(new SecurityTokenReplayAddFailedException(LogHelper.FormatInvariant("IDX10229: TokenValidationParameters.TokenReplayCache was unable to add the securityToken: '{0}'.", new object[] { securityToken })));
					}
				}
				LogHelper.LogInformation("IDX10240: No token replay is detected.", Array.Empty<object>());
				return;
			}
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00044109 File Offset: 0x00042309
		public static void ValidateTokenReplay(string securityToken, DateTime? expirationTime, TokenValidationParameters validationParameters)
		{
			Validators.ValidateTokenReplay(expirationTime, securityToken, validationParameters);
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00044114 File Offset: 0x00042314
		public static string ValidateTokenType(string type, SecurityToken securityToken, TokenValidationParameters validationParameters)
		{
			if (securityToken == null)
			{
				throw new ArgumentNullException("securityToken");
			}
			if (validationParameters == null)
			{
				throw LogHelper.LogArgumentNullException("validationParameters");
			}
			if (validationParameters.TypeValidator == null && (validationParameters.ValidTypes == null || !validationParameters.ValidTypes.Any<string>()))
			{
				LogHelper.LogVerbose("IDX10255: TypeValidator property on ValidationParameters is null and ValidTypes is either null or empty. Exiting without validating the token type.", Array.Empty<object>());
				return type;
			}
			if (validationParameters.TypeValidator != null)
			{
				return validationParameters.TypeValidator(type, securityToken, validationParameters);
			}
			if (string.IsNullOrEmpty(type))
			{
				throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidTypeException("IDX10256: Unable to validate the token type. TokenValidationParameters.ValidTypes is set, but the 'typ' header claim is null or empty.")
				{
					InvalidType = null
				});
			}
			if (!validationParameters.ValidTypes.Contains(type, StringComparer.Ordinal))
			{
				throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidTypeException(LogHelper.FormatInvariant("IDX10257: Token type validation failed. Type: '{0}'. Did not match: validationParameters.TokenTypes: '{1}'.", new object[]
				{
					LogHelper.MarkAsNonPII(type),
					Utility.SerializeAsSingleCommaDelimitedString(validationParameters.ValidTypes)
				}))
				{
					InvalidType = type
				});
			}
			LogHelper.LogInformation("IDX10258: Token type validated. Type: '{0}'.", new object[] { LogHelper.MarkAsNonPII(type) });
			return type;
		}
	}
}
