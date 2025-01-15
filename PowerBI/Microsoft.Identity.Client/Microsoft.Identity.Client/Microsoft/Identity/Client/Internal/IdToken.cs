using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using Microsoft.Identity.Client.Utils;
using Microsoft.Identity.Json;
using Microsoft.Identity.Json.Linq;

namespace Microsoft.Identity.Client.Internal
{
	// Token: 0x02000234 RID: 564
	internal class IdToken
	{
		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x0004B97A File Offset: 0x00049B7A
		// (set) Token: 0x060016F0 RID: 5872 RVA: 0x0004B982 File Offset: 0x00049B82
		public string ObjectId { get; private set; }

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060016F1 RID: 5873 RVA: 0x0004B98B File Offset: 0x00049B8B
		// (set) Token: 0x060016F2 RID: 5874 RVA: 0x0004B993 File Offset: 0x00049B93
		public string Subject { get; private set; }

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x0004B99C File Offset: 0x00049B9C
		// (set) Token: 0x060016F4 RID: 5876 RVA: 0x0004B9A4 File Offset: 0x00049BA4
		public string TenantId { get; private set; }

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x0004B9AD File Offset: 0x00049BAD
		// (set) Token: 0x060016F6 RID: 5878 RVA: 0x0004B9B5 File Offset: 0x00049BB5
		public string PreferredUsername { get; private set; }

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060016F7 RID: 5879 RVA: 0x0004B9BE File Offset: 0x00049BBE
		// (set) Token: 0x060016F8 RID: 5880 RVA: 0x0004B9C6 File Offset: 0x00049BC6
		public string Name { get; private set; }

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060016F9 RID: 5881 RVA: 0x0004B9CF File Offset: 0x00049BCF
		// (set) Token: 0x060016FA RID: 5882 RVA: 0x0004B9D7 File Offset: 0x00049BD7
		public string Email { get; private set; }

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060016FB RID: 5883 RVA: 0x0004B9E0 File Offset: 0x00049BE0
		// (set) Token: 0x060016FC RID: 5884 RVA: 0x0004B9E8 File Offset: 0x00049BE8
		public string Upn { get; private set; }

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060016FD RID: 5885 RVA: 0x0004B9F1 File Offset: 0x00049BF1
		// (set) Token: 0x060016FE RID: 5886 RVA: 0x0004B9F9 File Offset: 0x00049BF9
		public string GivenName { get; private set; }

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x0004BA02 File Offset: 0x00049C02
		// (set) Token: 0x06001700 RID: 5888 RVA: 0x0004BA0A File Offset: 0x00049C0A
		public string FamilyName { get; private set; }

		// Token: 0x06001701 RID: 5889 RVA: 0x0004BA13 File Offset: 0x00049C13
		public string GetUniqueId()
		{
			return this.ObjectId ?? this.Subject;
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001702 RID: 5890 RVA: 0x0004BA25 File Offset: 0x00049C25
		// (set) Token: 0x06001703 RID: 5891 RVA: 0x0004BA2D File Offset: 0x00049C2D
		public ClaimsPrincipal ClaimsPrincipal { get; private set; }

		// Token: 0x06001704 RID: 5892 RVA: 0x0004BA38 File Offset: 0x00049C38
		private static IdToken ClaimsToToken(List<Claim> claims)
		{
			ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims));
			return new IdToken
			{
				ClaimsPrincipal = claimsPrincipal,
				ObjectId = IdToken.<ClaimsToToken>g__FindClaim|42_0(claims, "oid"),
				Subject = IdToken.<ClaimsToToken>g__FindClaim|42_0(claims, "sub"),
				TenantId = IdToken.<ClaimsToToken>g__FindClaim|42_0(claims, "tid"),
				PreferredUsername = IdToken.<ClaimsToToken>g__FindClaim|42_0(claims, "preferred_username"),
				Name = IdToken.<ClaimsToToken>g__FindClaim|42_0(claims, "name"),
				Email = IdToken.<ClaimsToToken>g__FindClaim|42_0(claims, "email"),
				Upn = IdToken.<ClaimsToToken>g__FindClaim|42_0(claims, "upn"),
				GivenName = IdToken.<ClaimsToToken>g__FindClaim|42_0(claims, "given_name"),
				FamilyName = IdToken.<ClaimsToToken>g__FindClaim|42_0(claims, "family_name")
			};
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x0004BAF8 File Offset: 0x00049CF8
		public static IdToken Parse(string idToken)
		{
			if (string.IsNullOrEmpty(idToken))
			{
				return null;
			}
			string[] array = idToken.Split(new char[] { '.' });
			if (array.Length < 2)
			{
				throw new MsalClientException("invalid_jwt", "ID Token must have a valid JWT format. ");
			}
			IdToken idToken2;
			try
			{
				idToken2 = IdToken.ClaimsToToken(IdToken.GetClaimsFromRawToken(JsonConvert.DeserializeObject<Dictionary<string, object>>(Base64UrlHelpers.Decode(array[1]))));
			}
			catch (JsonException ex)
			{
				throw new MsalClientException("json_parse_failed", "Failed to parse the returned id token. ", ex);
			}
			return idToken2;
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x0004BB78 File Offset: 0x00049D78
		private static List<Claim> GetClaimsFromRawToken(Dictionary<string, object> idTokenClaims)
		{
			List<Claim> list = new List<Claim>();
			string text = null;
			object obj;
			if (idTokenClaims.TryGetValue("iss", out obj))
			{
				text = obj as string;
			}
			if (text == null)
			{
				text = "LOCAL AUTHORITY";
			}
			foreach (KeyValuePair<string, object> keyValuePair in idTokenClaims)
			{
				if (keyValuePair.Value == null)
				{
					list.Add(new Claim(keyValuePair.Key, string.Empty, "JSON_NULL", text, text));
				}
				else
				{
					string text2 = keyValuePair.Value as string;
					if (text2 != null)
					{
						list.Add(new Claim(keyValuePair.Key, text2, "http://www.w3.org/2001/XMLSchema#string", text, text));
					}
					else
					{
						JToken jtoken = keyValuePair.Value as JToken;
						if (jtoken != null)
						{
							IdToken.AddClaimsFromJToken(list, keyValuePair.Key, jtoken, text);
						}
						else
						{
							IEnumerable<object> enumerable = keyValuePair.Value as IEnumerable<object>;
							if (enumerable != null)
							{
								using (IEnumerator<object> enumerator2 = enumerable.GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										object obj2 = enumerator2.Current;
										text2 = obj2 as string;
										if (text2 != null)
										{
											list.Add(new Claim(keyValuePair.Key, text2, "http://www.w3.org/2001/XMLSchema#string", text, text));
										}
										else
										{
											jtoken = obj2 as JToken;
											if (jtoken != null)
											{
												IdToken.AddDefaultClaimFromJToken(list, keyValuePair.Key, jtoken, text);
											}
											else if (obj2 is DateTime)
											{
												DateTime dateTime = (DateTime)obj2;
												list.Add(new Claim(keyValuePair.Key, dateTime.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture), "http://www.w3.org/2001/XMLSchema#dateTime", text, text));
											}
											else
											{
												list.Add(new Claim(keyValuePair.Key, JsonConvert.SerializeObject(obj2), IdToken.GetClaimValueType(obj2), text, text));
											}
										}
									}
									continue;
								}
							}
							IDictionary<string, object> dictionary = keyValuePair.Value as IDictionary<string, object>;
							if (dictionary != null)
							{
								using (IEnumerator<KeyValuePair<string, object>> enumerator3 = dictionary.GetEnumerator())
								{
									while (enumerator3.MoveNext())
									{
										KeyValuePair<string, object> keyValuePair2 = enumerator3.Current;
										list.Add(new Claim(keyValuePair.Key, string.Concat(new string[]
										{
											"{",
											keyValuePair2.Key,
											":",
											JsonConvert.SerializeObject(keyValuePair2.Value),
											"}"
										}), IdToken.GetClaimValueType(keyValuePair2.Value), text, text));
									}
									continue;
								}
							}
							object value = keyValuePair.Value;
							if (value is DateTime)
							{
								DateTime dateTime2 = (DateTime)value;
								list.Add(new Claim(keyValuePair.Key, dateTime2.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture), "http://www.w3.org/2001/XMLSchema#dateTime", text, text));
							}
							else
							{
								list.Add(new Claim(keyValuePair.Key, JsonConvert.SerializeObject(keyValuePair.Value), IdToken.GetClaimValueType(keyValuePair.Value), text, text));
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x0004BEC8 File Offset: 0x0004A0C8
		private static void AddClaimsFromJToken(List<Claim> claims, string claimType, JToken jtoken, string issuer)
		{
			if (jtoken.Type == JTokenType.Object)
			{
				claims.Add(new Claim(claimType, jtoken.ToString(Formatting.None, Array.Empty<JsonConverter>()), "JSON"));
				return;
			}
			if (jtoken.Type == JTokenType.Array)
			{
				using (IEnumerator<JToken> enumerator = (jtoken as JArray).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JToken jtoken2 = enumerator.Current;
						JTokenType type = jtoken2.Type;
						if (type != JTokenType.Object)
						{
							if (type != JTokenType.Array)
							{
								IdToken.AddDefaultClaimFromJToken(claims, claimType, jtoken2, issuer);
							}
							else
							{
								claims.Add(new Claim(claimType, jtoken2.ToString(Formatting.None, Array.Empty<JsonConverter>()), "JSON_ARRAY", issuer, issuer));
							}
						}
						else
						{
							claims.Add(new Claim(claimType, jtoken2.ToString(Formatting.None, Array.Empty<JsonConverter>()), "JSON", issuer, issuer));
						}
					}
					return;
				}
			}
			IdToken.AddDefaultClaimFromJToken(claims, claimType, jtoken, issuer);
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x0004BFA8 File Offset: 0x0004A1A8
		private static void AddDefaultClaimFromJToken(List<Claim> claims, string claimType, JToken jtoken, string issuer)
		{
			JValue jvalue = jtoken as JValue;
			if (jvalue == null)
			{
				claims.Add(new Claim(claimType, jtoken.ToString(Formatting.None, Array.Empty<JsonConverter>()), IdToken.GetClaimValueType(jtoken), issuer, issuer));
				return;
			}
			if (jvalue.Type == JTokenType.String)
			{
				claims.Add(new Claim(claimType, jvalue.Value.ToString(), "http://www.w3.org/2001/XMLSchema#string", issuer, issuer));
				return;
			}
			object value = jvalue.Value;
			if (value is DateTime)
			{
				claims.Add(new Claim(claimType, ((DateTime)value).ToUniversalTime().ToString("o", CultureInfo.InvariantCulture), "http://www.w3.org/2001/XMLSchema#dateTime", issuer, issuer));
				return;
			}
			claims.Add(new Claim(claimType, jtoken.ToString(Formatting.None, Array.Empty<JsonConverter>()), IdToken.GetClaimValueType(jvalue.Value), issuer, issuer));
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x0004C074 File Offset: 0x0004A274
		private static string GetClaimValueType(object obj)
		{
			if (obj == null)
			{
				return "JSON_NULL";
			}
			Type type = obj.GetType();
			if (type == typeof(string))
			{
				return "http://www.w3.org/2001/XMLSchema#string";
			}
			if (type == typeof(int))
			{
				return "http://www.w3.org/2001/XMLSchema#integer";
			}
			if (type == typeof(bool))
			{
				return "http://www.w3.org/2001/XMLSchema#boolean";
			}
			if (type == typeof(double))
			{
				return "http://www.w3.org/2001/XMLSchema#double";
			}
			if (type == typeof(long))
			{
				long num = (long)obj;
				if (num >= -2147483648L && num <= 2147483647L)
				{
					return "http://www.w3.org/2001/XMLSchema#integer";
				}
				return "http://www.w3.org/2001/XMLSchema#integer64";
			}
			else
			{
				if (type == typeof(DateTime))
				{
					return "http://www.w3.org/2001/XMLSchema#dateTime";
				}
				if (type == typeof(JObject))
				{
					return "JSON";
				}
				if (type == typeof(JArray))
				{
					return "JSON_ARRAY";
				}
				return type.ToString();
			}
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x0004C180 File Offset: 0x0004A380
		[CompilerGenerated]
		internal static string <ClaimsToToken>g__FindClaim|42_0(List<Claim> claims, string type)
		{
			Claim claim = claims.SingleOrDefault((Claim _) => string.Equals(_.Type, type, StringComparison.OrdinalIgnoreCase));
			if (claim == null)
			{
				return null;
			}
			return claim.Value;
		}

		// Token: 0x040009FA RID: 2554
		private const string DefaultIssuser = "LOCAL AUTHORITY";
	}
}
