using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.IdentityModel.Json;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200015F RID: 351
	[JsonObject]
	public class JsonWebKeySet
	{
		// Token: 0x06001056 RID: 4182 RVA: 0x0003F89C File Offset: 0x0003DA9C
		public static JsonWebKeySet Create(string json)
		{
			if (string.IsNullOrEmpty(json))
			{
				throw LogHelper.LogArgumentNullException("json");
			}
			return new JsonWebKeySet(json);
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0003F8B7 File Offset: 0x0003DAB7
		public JsonWebKeySet()
		{
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0003F8E0 File Offset: 0x0003DAE0
		public JsonWebKeySet(string json)
		{
			if (string.IsNullOrEmpty(json))
			{
				throw LogHelper.LogArgumentNullException("json");
			}
			try
			{
				LogHelper.LogVerbose("IDX10806: Deserializing json: '{0}' into '{1}'.", new object[]
				{
					json,
					LogHelper.MarkAsNonPII("Microsoft.IdentityModel.Tokens.JsonWebKeySet")
				});
				JsonConvert.PopulateObject(json, this);
			}
			catch (Exception ex)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10805: Error deserializing json: '{0}' into '{1}'.", new object[]
				{
					json,
					LogHelper.MarkAsNonPII("Microsoft.IdentityModel.Tokens.JsonWebKeySet")
				}), ex));
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06001059 RID: 4185 RVA: 0x0003F990 File Offset: 0x0003DB90
		[JsonExtensionData]
		public virtual IDictionary<string, object> AdditionalData { get; } = new Dictionary<string, object>();

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x0600105A RID: 4186 RVA: 0x0003F998 File Offset: 0x0003DB98
		// (set) Token: 0x0600105B RID: 4187 RVA: 0x0003F9A0 File Offset: 0x0003DBA0
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "keys", Required = Required.Default)]
		public IList<JsonWebKey> Keys { get; private set; } = new List<JsonWebKey>();

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x0003F9A9 File Offset: 0x0003DBA9
		// (set) Token: 0x0600105D RID: 4189 RVA: 0x0003F9B1 File Offset: 0x0003DBB1
		[DefaultValue(true)]
		public bool SkipUnresolvedJsonWebKeys { get; set; } = JsonWebKeySet.DefaultSkipUnresolvedJsonWebKeys;

		// Token: 0x0600105E RID: 4190 RVA: 0x0003F9BC File Offset: 0x0003DBBC
		public IList<SecurityKey> GetSigningKeys()
		{
			List<SecurityKey> list = new List<SecurityKey>();
			foreach (JsonWebKey jsonWebKey in this.Keys)
			{
				if (!string.IsNullOrEmpty(jsonWebKey.Use) && !jsonWebKey.Use.Equals("sig"))
				{
					string text = LogHelper.FormatInvariant("IDX10808: The 'use' parameter of a JsonWebKey: '{0}' was expected to be 'sig' or empty, but was '{1}'.", new object[] { jsonWebKey, jsonWebKey.Use });
					jsonWebKey.ConvertKeyInfo = text;
					LogHelper.LogInformation(text, Array.Empty<object>());
					if (!this.SkipUnresolvedJsonWebKeys)
					{
						list.Add(jsonWebKey);
					}
				}
				else if ("RSA".Equals(jsonWebKey.Kty))
				{
					bool flag = true;
					if ((jsonWebKey.X5c == null || jsonWebKey.X5c.Count == 0) && string.IsNullOrEmpty(jsonWebKey.E) && string.IsNullOrEmpty(jsonWebKey.N))
					{
						List<string> list2 = new List<string> { "x5c", "e", "n" };
						string text2 = LogHelper.FormatInvariant("IDX10814: Unable to create a {0} from the properties found in the JsonWebKey: '{1}'. Missing: '{2}'.", new object[]
						{
							LogHelper.MarkAsNonPII(typeof(RsaSecurityKey)),
							jsonWebKey,
							LogHelper.MarkAsNonPII(string.Join(", ", list2))
						});
						jsonWebKey.ConvertKeyInfo = text2;
						LogHelper.LogInformation(text2, Array.Empty<object>());
						flag = false;
					}
					else
					{
						if (JsonWebKeySet.IsValidX509SecurityKey(jsonWebKey))
						{
							SecurityKey securityKey;
							if (JsonWebKeyConverter.TryConvertToX509SecurityKey(jsonWebKey, out securityKey))
							{
								list.Add(securityKey);
							}
							else
							{
								flag = false;
							}
						}
						if (JsonWebKeySet.IsValidRsaSecurityKey(jsonWebKey))
						{
							SecurityKey securityKey2;
							if (JsonWebKeyConverter.TryCreateToRsaSecurityKey(jsonWebKey, out securityKey2))
							{
								list.Add(securityKey2);
							}
							else
							{
								flag = false;
							}
						}
					}
					if (!flag && !this.SkipUnresolvedJsonWebKeys)
					{
						list.Add(jsonWebKey);
					}
				}
				else if ("EC".Equals(jsonWebKey.Kty))
				{
					SecurityKey securityKey3;
					if (JsonWebKeyConverter.TryConvertToECDsaSecurityKey(jsonWebKey, out securityKey3))
					{
						list.Add(securityKey3);
					}
					else if (!this.SkipUnresolvedJsonWebKeys)
					{
						list.Add(jsonWebKey);
					}
				}
				else
				{
					string text3 = LogHelper.FormatInvariant("IDX10810: Unable to convert the JsonWebKey: '{0}' to a X509SecurityKey, RsaSecurityKey or ECDSASecurityKey.", new object[] { jsonWebKey });
					jsonWebKey.ConvertKeyInfo = text3;
					LogHelper.LogInformation(text3, Array.Empty<object>());
					if (!this.SkipUnresolvedJsonWebKeys)
					{
						list.Add(jsonWebKey);
					}
				}
			}
			return list;
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0003FC14 File Offset: 0x0003DE14
		private static bool IsValidX509SecurityKey(JsonWebKey webKey)
		{
			if (webKey.X5c == null || webKey.X5c.Count == 0)
			{
				webKey.ConvertKeyInfo = LogHelper.FormatInvariant("IDX10814: Unable to create a {0} from the properties found in the JsonWebKey: '{1}'. Missing: '{2}'.", new object[]
				{
					LogHelper.MarkAsNonPII(typeof(X509SecurityKey)),
					webKey,
					LogHelper.MarkAsNonPII("x5c")
				});
				return false;
			}
			return true;
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0003FC74 File Offset: 0x0003DE74
		private static bool IsValidRsaSecurityKey(JsonWebKey webKey)
		{
			List<string> list = new List<string>();
			if (string.IsNullOrWhiteSpace(webKey.E))
			{
				list.Add("e");
			}
			if (string.IsNullOrWhiteSpace(webKey.N))
			{
				list.Add("n");
			}
			if (list.Count > 0)
			{
				string text = LogHelper.FormatInvariant("IDX10814: Unable to create a {0} from the properties found in the JsonWebKey: '{1}'. Missing: '{2}'.", new object[]
				{
					LogHelper.MarkAsNonPII(typeof(RsaSecurityKey)),
					webKey,
					LogHelper.MarkAsNonPII(string.Join(", ", list))
				});
				if (string.IsNullOrEmpty(webKey.ConvertKeyInfo))
				{
					webKey.ConvertKeyInfo = text;
				}
				else
				{
					webKey.ConvertKeyInfo += text;
				}
			}
			return list.Count == 0;
		}

		// Token: 0x0400055A RID: 1370
		private const string _className = "Microsoft.IdentityModel.Tokens.JsonWebKeySet";

		// Token: 0x0400055D RID: 1373
		[DefaultValue(true)]
		public static bool DefaultSkipUnresolvedJsonWebKeys = true;
	}
}
