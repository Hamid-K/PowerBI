using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Json;
using Microsoft.IdentityModel.Json.Linq;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace System.IdentityModel.Tokens.Jwt
{
	// Token: 0x02000009 RID: 9
	public class JwtPayload : Dictionary<string, object>
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002574 File Offset: 0x00000774
		public JwtPayload()
			: this(null, null, null, null, null)
		{
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000259C File Offset: 0x0000079C
		public JwtPayload(IEnumerable<Claim> claims)
			: this(null, null, claims, null, null)
		{
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000025C4 File Offset: 0x000007C4
		public JwtPayload(string issuer, string audience, IEnumerable<Claim> claims, DateTime? notBefore, DateTime? expires)
			: this(issuer, audience, claims, notBefore, expires, null)
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000025E7 File Offset: 0x000007E7
		public JwtPayload(string issuer, string audience, IEnumerable<Claim> claims, DateTime? notBefore, DateTime? expires, DateTime? issuedAt)
			: base(StringComparer.Ordinal)
		{
			if (claims != null)
			{
				this.AddClaims(claims);
			}
			this.AddFirstPriorityClaims(issuer, audience, notBefore, expires, issuedAt);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000260C File Offset: 0x0000080C
		public JwtPayload(string issuer, string audience, IEnumerable<Claim> claims, IDictionary<string, object> claimsCollection, DateTime? notBefore, DateTime? expires, DateTime? issuedAt)
			: base(StringComparer.Ordinal)
		{
			if (claims != null)
			{
				this.AddClaims(claims);
			}
			if (claimsCollection != null && claimsCollection.Any<KeyValuePair<string, object>>())
			{
				this.AddDictionaryClaims(claimsCollection);
			}
			this.AddFirstPriorityClaims(issuer, audience, notBefore, expires, issuedAt);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002648 File Offset: 0x00000848
		internal void AddFirstPriorityClaims(string issuer, string audience, DateTime? notBefore, DateTime? expires, DateTime? issuedAt)
		{
			if (expires != null)
			{
				if (notBefore != null)
				{
					if (notBefore.Value >= expires.Value)
					{
						throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX12401: Expires: '{0}' must be after NotBefore: '{1}'.", new object[]
						{
							LogHelper.MarkAsNonPII(expires.Value),
							LogHelper.MarkAsNonPII(notBefore.Value)
						})));
					}
					base["nbf"] = EpochTime.GetIntDate(notBefore.Value.ToUniversalTime());
				}
				base["exp"] = EpochTime.GetIntDate(expires.Value.ToUniversalTime());
			}
			if (issuedAt != null)
			{
				base["iat"] = EpochTime.GetIntDate(issuedAt.Value.ToUniversalTime());
			}
			if (!string.IsNullOrEmpty(issuer))
			{
				base["iss"] = issuer;
			}
			if (!string.IsNullOrEmpty(audience))
			{
				this.AddClaim(new Claim("aud", audience, "http://www.w3.org/2001/XMLSchema#string"));
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002769 File Offset: 0x00000969
		public string Actort
		{
			get
			{
				return this.GetStandardClaim("actort");
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002776 File Offset: 0x00000976
		public string Acr
		{
			get
			{
				return this.GetStandardClaim("acr");
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002783 File Offset: 0x00000983
		public IList<string> Amr
		{
			get
			{
				return this.GetIListClaims("amr");
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002790 File Offset: 0x00000990
		public int? AuthTime
		{
			get
			{
				return this.GetIntClaim("auth_time");
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003E RID: 62 RVA: 0x0000279D File Offset: 0x0000099D
		public IList<string> Aud
		{
			get
			{
				return this.GetIListClaims("aud");
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000027AA File Offset: 0x000009AA
		public string Azp
		{
			get
			{
				return this.GetStandardClaim("azp");
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000027B7 File Offset: 0x000009B7
		public string CHash
		{
			get
			{
				return this.GetStandardClaim("c_hash");
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000027C4 File Offset: 0x000009C4
		public int? Exp
		{
			get
			{
				return this.GetIntClaim("exp");
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000027D1 File Offset: 0x000009D1
		public string Jti
		{
			get
			{
				return this.GetStandardClaim("jti");
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000027DE File Offset: 0x000009DE
		public int? Iat
		{
			get
			{
				return this.GetIntClaim("iat");
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000027EB File Offset: 0x000009EB
		public string Iss
		{
			get
			{
				return this.GetStandardClaim("iss");
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000027F8 File Offset: 0x000009F8
		public int? Nbf
		{
			get
			{
				return this.GetIntClaim("nbf");
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002805 File Offset: 0x00000A05
		public string Nonce
		{
			get
			{
				return this.GetStandardClaim("nonce");
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002812 File Offset: 0x00000A12
		public string Sub
		{
			get
			{
				return this.GetStandardClaim("sub");
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000048 RID: 72 RVA: 0x0000281F File Offset: 0x00000A1F
		public DateTime ValidFrom
		{
			get
			{
				return this.GetDateTime("nbf");
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000282C File Offset: 0x00000A2C
		public DateTime ValidTo
		{
			get
			{
				return this.GetDateTime("exp");
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002839 File Offset: 0x00000A39
		public DateTime IssuedAt
		{
			get
			{
				return this.GetDateTime("iat");
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002848 File Offset: 0x00000A48
		public virtual IEnumerable<Claim> Claims
		{
			get
			{
				List<Claim> list = new List<Claim>();
				string text = this.Iss ?? "LOCAL AUTHORITY";
				foreach (KeyValuePair<string, object> keyValuePair in this)
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
								JwtPayload.AddClaimsFromJToken(list, keyValuePair.Key, jtoken, text);
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
											object obj = enumerator2.Current;
											text2 = obj as string;
											if (text2 != null)
											{
												list.Add(new Claim(keyValuePair.Key, text2, "http://www.w3.org/2001/XMLSchema#string", text, text));
											}
											else
											{
												jtoken = obj as JToken;
												if (jtoken != null)
												{
													JwtPayload.AddDefaultClaimFromJToken(list, keyValuePair.Key, jtoken, text);
												}
												else if (obj is DateTime)
												{
													DateTime dateTime = (DateTime)obj;
													list.Add(new Claim(keyValuePair.Key, dateTime.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture), "http://www.w3.org/2001/XMLSchema#dateTime", text, text));
												}
												else
												{
													list.Add(new Claim(keyValuePair.Key, JsonConvert.SerializeObject(obj), JwtPayload.GetClaimValueType(obj), text, text));
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
											}), JwtPayload.GetClaimValueType(keyValuePair2.Value), text, text));
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
									list.Add(new Claim(keyValuePair.Key, JsonConvert.SerializeObject(keyValuePair.Value), JwtPayload.GetClaimValueType(keyValuePair.Value), text, text));
								}
							}
						}
					}
				}
				return list;
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002B88 File Offset: 0x00000D88
		private static void AddClaimsFromJToken(List<Claim> claims, string claimType, JToken jtoken, string issuer)
		{
			if (jtoken.Type == JTokenType.Object)
			{
				claims.Add(new Claim(claimType, jtoken.ToString(Formatting.None, Array.Empty<JsonConverter>()), "JSON", issuer, issuer));
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
								JwtPayload.AddDefaultClaimFromJToken(claims, claimType, jtoken2, issuer);
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
			JwtPayload.AddDefaultClaimFromJToken(claims, claimType, jtoken, issuer);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002C68 File Offset: 0x00000E68
		private static void AddDefaultClaimFromJToken(List<Claim> claims, string claimType, JToken jtoken, string issuer)
		{
			JValue jvalue = jtoken as JValue;
			if (jvalue == null)
			{
				claims.Add(new Claim(claimType, jtoken.ToString(Formatting.None, Array.Empty<JsonConverter>()), JwtPayload.GetClaimValueType(jtoken), issuer, issuer));
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
			claims.Add(new Claim(claimType, jtoken.ToString(Formatting.None, Array.Empty<JsonConverter>()), JwtPayload.GetClaimValueType(jvalue.Value), issuer, issuer));
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002D33 File Offset: 0x00000F33
		public void AddClaim(Claim claim)
		{
			if (claim == null)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentNullException("claim"));
			}
			this.AddClaims(new Claim[] { claim });
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002D58 File Offset: 0x00000F58
		public void AddClaims(IEnumerable<Claim> claims)
		{
			if (claims == null)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentNullException("claims"));
			}
			foreach (Claim claim in claims)
			{
				if (claim != null)
				{
					string type = claim.Type;
					object obj = (claim.ValueType.Equals("http://www.w3.org/2001/XMLSchema#string") ? claim.Value : TokenUtilities.GetClaimValueUsingValueType(claim));
					object obj2;
					if (base.TryGetValue(type, out obj2))
					{
						IList<object> list = obj2 as IList<object>;
						if (list == null)
						{
							list = new List<object>();
							list.Add(obj2);
							base[type] = list;
						}
						list.Add(obj);
					}
					else
					{
						base[type] = obj;
					}
				}
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002E1C File Offset: 0x0000101C
		internal void AddDictionaryClaims(IDictionary<string, object> claimsCollection)
		{
			if (claimsCollection == null)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentNullException("claimsCollection"));
			}
			foreach (string text in claimsCollection.Keys)
			{
				base[text] = claimsCollection[text];
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002E84 File Offset: 0x00001084
		internal static string GetClaimValueType(object obj)
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

		// Token: 0x06000052 RID: 82 RVA: 0x00002F88 File Offset: 0x00001188
		internal string GetStandardClaim(string claimType)
		{
			object obj;
			if (!base.TryGetValue(claimType, out obj))
			{
				return null;
			}
			if (obj == null)
			{
				return null;
			}
			string text = obj as string;
			if (text != null)
			{
				return text;
			}
			return JsonExtensions.SerializeToJson(obj);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002FBC File Offset: 0x000011BC
		internal int? GetIntClaim(string claimType)
		{
			int? num = null;
			object obj;
			if (base.TryGetValue(claimType, out obj))
			{
				IList<object> list = obj as IList<object>;
				if (list != null)
				{
					using (IEnumerator<object> enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							num = null;
							if (obj2 != null)
							{
								try
								{
									num = new int?(Convert.ToInt32(Math.Truncate(Convert.ToDouble(obj2, CultureInfo.InvariantCulture))));
								}
								catch (FormatException)
								{
									num = null;
								}
								catch (InvalidCastException)
								{
									num = null;
								}
								catch (OverflowException)
								{
									num = null;
								}
								if (num != null)
								{
									return num;
								}
							}
						}
						return num;
					}
				}
				try
				{
					num = new int?(Convert.ToInt32(Math.Truncate(Convert.ToDouble(obj, CultureInfo.InvariantCulture))));
				}
				catch (FormatException)
				{
					num = null;
				}
				catch (OverflowException)
				{
					num = null;
				}
				return num;
			}
			return num;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000030F0 File Offset: 0x000012F0
		internal IList<string> GetIListClaims(string claimType)
		{
			List<string> list = new List<string>();
			object obj = null;
			if (!base.TryGetValue(claimType, out obj))
			{
				return list;
			}
			string text = obj as string;
			if (text != null)
			{
				list.Add(text);
				return list;
			}
			IEnumerable<object> enumerable = obj as IEnumerable<object>;
			if (enumerable != null)
			{
				using (IEnumerator<object> enumerator = enumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj2 = enumerator.Current;
						list.Add(obj2.ToString());
					}
					return list;
				}
			}
			list.Add(JsonExtensions.SerializeToJson(obj));
			return list;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003184 File Offset: 0x00001384
		private DateTime GetDateTime(string key)
		{
			object obj;
			if (!base.TryGetValue(key, out obj))
			{
				return DateTime.MinValue;
			}
			DateTime dateTime;
			try
			{
				IList<object> list = obj as IList<object>;
				if (list != null)
				{
					if (list.Count == 0)
					{
						return DateTime.MinValue;
					}
					obj = list[0];
				}
				dateTime = EpochTime.DateTime(Convert.ToInt64(Math.Truncate(Convert.ToDouble(obj, CultureInfo.InvariantCulture))));
			}
			catch (Exception ex)
			{
				if (ex is FormatException || ex is ArgumentException || ex is InvalidCastException)
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenException(LogHelper.FormatInvariant("IDX12700: Error found while parsing date time. The '{0}' claim has value '{1}' which is could not be parsed to an integer.", new object[]
					{
						key,
						LogHelper.MarkAsNonPII(obj ?? "Null")
					}), ex));
				}
				if (ex is OverflowException)
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenException(LogHelper.FormatInvariant("IDX12701: Error found while parsing date time. The '{0}' claim has value '{1}' does not lie in the valid range.", new object[]
					{
						key,
						LogHelper.MarkAsNonPII(obj ?? "Null")
					}), ex));
				}
				throw;
			}
			return dateTime;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003280 File Offset: 0x00001480
		public virtual string SerializeToJson()
		{
			return JsonExtensions.SerializeToJson(this);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003288 File Offset: 0x00001488
		public virtual string Base64UrlEncode()
		{
			return Base64UrlEncoder.Encode(this.SerializeToJson());
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003295 File Offset: 0x00001495
		public static JwtPayload Base64UrlDeserialize(string base64UrlEncodedJsonString)
		{
			return JsonExtensions.DeserializeJwtPayload(Base64UrlEncoder.Decode(base64UrlEncodedJsonString));
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000032A2 File Offset: 0x000014A2
		public static JwtPayload Deserialize(string jsonString)
		{
			return JsonExtensions.DeserializeJwtPayload(jsonString);
		}
	}
}
