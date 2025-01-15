using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Json.Linq;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200017D RID: 381
	internal class TokenUtilities
	{
		// Token: 0x06001133 RID: 4403 RVA: 0x00042410 File Offset: 0x00040610
		internal static IDictionary<string, object> CreateDictionaryFromClaims(IEnumerable<Claim> claims)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (claims == null)
			{
				return dictionary;
			}
			foreach (Claim claim in claims)
			{
				if (claim != null)
				{
					string type = claim.Type;
					object obj = (claim.ValueType.Equals("http://www.w3.org/2001/XMLSchema#string") ? claim.Value : TokenUtilities.GetClaimValueUsingValueType(claim));
					object obj2;
					if (dictionary.TryGetValue(type, out obj2))
					{
						IList<object> list = obj2 as IList<object>;
						if (list == null)
						{
							list = new List<object>();
							list.Add(obj2);
							dictionary[type] = list;
						}
						list.Add(obj);
					}
					else
					{
						dictionary[type] = obj;
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x000424D0 File Offset: 0x000406D0
		internal static object GetClaimValueUsingValueType(Claim claim)
		{
			if (claim.ValueType == "http://www.w3.org/2001/XMLSchema#string")
			{
				return claim.Value;
			}
			bool flag;
			if (claim.ValueType == "http://www.w3.org/2001/XMLSchema#boolean" && bool.TryParse(claim.Value, out flag))
			{
				return flag;
			}
			double num;
			if (claim.ValueType == "http://www.w3.org/2001/XMLSchema#double" && double.TryParse(claim.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out num))
			{
				return num;
			}
			int num2;
			if ((claim.ValueType == "http://www.w3.org/2001/XMLSchema#integer" || claim.ValueType == "http://www.w3.org/2001/XMLSchema#integer32") && int.TryParse(claim.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out num2))
			{
				return num2;
			}
			long num3;
			if (claim.ValueType == "http://www.w3.org/2001/XMLSchema#integer64" && long.TryParse(claim.Value, out num3))
			{
				return num3;
			}
			DateTime dateTime;
			if (claim.ValueType == "http://www.w3.org/2001/XMLSchema#dateTime" && DateTime.TryParse(claim.Value, out dateTime))
			{
				return dateTime;
			}
			if (claim.ValueType == "JSON")
			{
				return JObject.Parse(claim.Value);
			}
			if (claim.ValueType == "JSON_ARRAY")
			{
				return JArray.Parse(claim.Value);
			}
			if (claim.ValueType == "JSON_NULL")
			{
				return string.Empty;
			}
			return claim.Value;
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x0004263F File Offset: 0x0004083F
		internal static IEnumerable<SecurityKey> GetAllSigningKeys(TokenValidationParameters validationParameters)
		{
			LogHelper.LogInformation("IDX10243: Reading issuer signing keys from validation parameters.", Array.Empty<object>());
			if (validationParameters.IssuerSigningKey != null)
			{
				yield return validationParameters.IssuerSigningKey;
			}
			if (validationParameters.IssuerSigningKeys != null)
			{
				foreach (SecurityKey securityKey in validationParameters.IssuerSigningKeys)
				{
					yield return securityKey;
				}
				IEnumerator<SecurityKey> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x0004264F File Offset: 0x0004084F
		internal static IEnumerable<SecurityKey> GetAllSigningKeys(BaseConfiguration configuration)
		{
			LogHelper.LogInformation("IDX10265: Reading issuer signing keys from configuration.", Array.Empty<object>());
			if (((configuration != null) ? configuration.SigningKeys : null) != null)
			{
				foreach (SecurityKey securityKey in configuration.SigningKeys)
				{
					yield return securityKey;
				}
				IEnumerator<SecurityKey> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x0004265F File Offset: 0x0004085F
		internal static IEnumerable<SecurityKey> GetAllSigningKeys(TokenValidationParameters validationParameters, BaseConfiguration configuration)
		{
			LogHelper.LogInformation("IDX10264: Reading issuer signing keys from validation parameters and configuration.", Array.Empty<object>());
			return TokenUtilities.GetAllSigningKeys(configuration).Concat(TokenUtilities.GetAllSigningKeys(validationParameters));
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x00042684 File Offset: 0x00040884
		internal static IEnumerable<Claim> MergeClaims(IEnumerable<Claim> claims, IEnumerable<Claim> subjectClaims)
		{
			if (claims == null)
			{
				return subjectClaims;
			}
			if (subjectClaims == null)
			{
				return claims;
			}
			List<Claim> list = claims.ToList<Claim>();
			using (IEnumerator<Claim> enumerator = subjectClaims.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Claim claim = enumerator.Current;
					if (!claims.Any((Claim i) => i.Type == claim.Type))
					{
						list.Add(claim);
					}
				}
			}
			return list;
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x00042704 File Offset: 0x00040904
		internal static bool IsRecoverableException(Exception exception)
		{
			return exception is SecurityTokenInvalidSignatureException || exception is SecurityTokenInvalidIssuerException || exception is SecurityTokenSignatureKeyNotFoundException;
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x00042724 File Offset: 0x00040924
		internal static bool IsRecoverableConfiguration(string kid, BaseConfiguration currentConfiguration, BaseConfiguration lkgConfiguration, Exception currentException)
		{
			Func<SecurityKey, bool> <>9__1;
			Lazy<bool> lazy = new Lazy<bool>(delegate
			{
				IEnumerable<SecurityKey> signingKeys = lkgConfiguration.SigningKeys;
				Func<SecurityKey, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (SecurityKey signingKey) => signingKey.KeyId == kid);
				}
				return signingKeys.Any(func);
			});
			if (currentException is SecurityTokenInvalidIssuerException)
			{
				return currentConfiguration.Issuer != lkgConfiguration.Issuer;
			}
			if (currentException is SecurityTokenSignatureKeyNotFoundException)
			{
				return lazy.Value;
			}
			if (!(currentException is SecurityTokenInvalidSignatureException))
			{
				return false;
			}
			SecurityKey securityKey = currentConfiguration.SigningKeys.FirstOrDefault((SecurityKey x) => x.KeyId == kid);
			if (securityKey == null)
			{
				return lazy.Value;
			}
			SecurityKey securityKey2 = lkgConfiguration.SigningKeys.FirstOrDefault((SecurityKey signingKey) => signingKey.KeyId == kid);
			return securityKey2 != null && securityKey.InternalId != securityKey2.InternalId;
		}

		// Token: 0x0400068F RID: 1679
		internal const string Json = "JSON";

		// Token: 0x04000690 RID: 1680
		internal const string JsonArray = "JSON_ARRAY";

		// Token: 0x04000691 RID: 1681
		internal const string JsonNull = "JSON_NULL";
	}
}
