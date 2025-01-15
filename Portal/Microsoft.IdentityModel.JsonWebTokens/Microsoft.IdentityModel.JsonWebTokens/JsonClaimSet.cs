using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.IdentityModel.Json.Linq;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.IdentityModel.JsonWebTokens
{
	// Token: 0x02000006 RID: 6
	internal class JsonClaimSet
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00005AF0 File Offset: 0x00003CF0
		internal JsonClaimSet(JsonDocument jsonDocument)
		{
			foreach (JsonProperty jsonProperty in jsonDocument.RootElement.EnumerateObject())
			{
				this.Elements[jsonProperty.Name] = jsonProperty.Value.Clone();
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00005B84 File Offset: 0x00003D84
		internal JsonClaimSet(byte[] jsonBytes)
			: this(JsonDocument.Parse(jsonBytes, default(JsonDocumentOptions)))
		{
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00005BAC File Offset: 0x00003DAC
		internal JsonClaimSet(string json)
			: this(JsonDocument.Parse(json, default(JsonDocumentOptions)))
		{
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00005BD0 File Offset: 0x00003DD0
		internal IList<Claim> Claims(string issuer)
		{
			if (this._claims == null)
			{
				object claimsLock = this._claimsLock;
				lock (claimsLock)
				{
					if (this._claims == null)
					{
						this._claims = this.CreateClaims(issuer);
					}
				}
			}
			return this._claims;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00005C30 File Offset: 0x00003E30
		internal IList<Claim> CreateClaims(string issuer)
		{
			IList<Claim> list = new List<Claim>();
			foreach (KeyValuePair<string, JsonElement> keyValuePair in this.Elements)
			{
				if (keyValuePair.Value.ValueKind == 2)
				{
					using (JsonElement.ArrayEnumerator enumerator2 = keyValuePair.Value.EnumerateArray().GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							JsonElement jsonElement = enumerator2.Current;
							Claim claim = JsonClaimSet.CreateClaimFromJsonElement(keyValuePair.Key, issuer, jsonElement);
							if (claim != null)
							{
								list.Add(claim);
							}
						}
						continue;
					}
				}
				Claim claim2 = JsonClaimSet.CreateClaimFromJsonElement(keyValuePair.Key, issuer, keyValuePair.Value);
				if (claim2 != null)
				{
					list.Add(claim2);
				}
			}
			return list;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00005D24 File Offset: 0x00003F24
		private static Claim CreateClaimFromJsonElement(string key, string issuer, JsonElement jsonElement)
		{
			if (jsonElement.ValueKind == 3)
			{
				try
				{
					DateTime dateTime;
					if (jsonElement.TryGetDateTime(ref dateTime))
					{
						return new Claim(key, dateTime.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture), "http://www.w3.org/2001/XMLSchema#dateTime", issuer, issuer);
					}
					return new Claim(key, jsonElement.ToString(), "http://www.w3.org/2001/XMLSchema#string", issuer, issuer);
				}
				catch (IndexOutOfRangeException)
				{
					return new Claim(key, jsonElement.ToString(), "http://www.w3.org/2001/XMLSchema#string", issuer, issuer);
				}
			}
			if (jsonElement.ValueKind == 7)
			{
				return new Claim(key, string.Empty, "JSON_NULL", issuer, issuer);
			}
			if (jsonElement.ValueKind == 1)
			{
				return new Claim(key, jsonElement.ToString(), "JSON", issuer, issuer);
			}
			if (jsonElement.ValueKind == 6)
			{
				return new Claim(key, "false", "http://www.w3.org/2001/XMLSchema#boolean", issuer, issuer);
			}
			if (jsonElement.ValueKind == 5)
			{
				return new Claim(key, "true", "http://www.w3.org/2001/XMLSchema#boolean", issuer, issuer);
			}
			if (jsonElement.ValueKind == 4)
			{
				short num;
				if (jsonElement.TryGetInt16(ref num))
				{
					return new Claim(key, jsonElement.ToString(), "http://www.w3.org/2001/XMLSchema#integer", issuer, issuer);
				}
				int num2;
				if (jsonElement.TryGetInt32(ref num2))
				{
					return new Claim(key, jsonElement.ToString(), "http://www.w3.org/2001/XMLSchema#integer", issuer, issuer);
				}
				long num3;
				if (jsonElement.TryGetInt64(ref num3))
				{
					return new Claim(key, jsonElement.ToString(), "http://www.w3.org/2001/XMLSchema#integer64", issuer, issuer);
				}
				decimal num4;
				if (jsonElement.TryGetDecimal(ref num4))
				{
					return new Claim(key, jsonElement.ToString(), "http://www.w3.org/2001/XMLSchema#double", issuer, issuer);
				}
				double num5;
				if (jsonElement.TryGetDouble(ref num5))
				{
					return new Claim(key, jsonElement.ToString(), "http://www.w3.org/2001/XMLSchema#double", issuer, issuer);
				}
				uint num6;
				if (jsonElement.TryGetUInt32(ref num6))
				{
					return new Claim(key, jsonElement.ToString(), "http://www.w3.org/2001/XMLSchema#uinteger32", issuer, issuer);
				}
				ulong num7;
				if (jsonElement.TryGetUInt64(ref num7))
				{
					return new Claim(key, jsonElement.ToString(), "http://www.w3.org/2001/XMLSchema#uinteger64", issuer, issuer);
				}
			}
			else if (jsonElement.ValueKind == 2)
			{
				return new Claim(key, jsonElement.ToString(), "JSON_ARRAY", issuer, issuer);
			}
			return null;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00005F80 File Offset: 0x00004180
		private static object CreateObjectFromJsonElement(JsonElement jsonElement)
		{
			if (jsonElement.ValueKind == 2)
			{
				int num = 0;
				foreach (JsonElement jsonElement2 in jsonElement.EnumerateArray())
				{
					num++;
				}
				object[] array = new object[num];
				int num2 = 0;
				foreach (JsonElement jsonElement3 in jsonElement.EnumerateArray())
				{
					array[num2++] = JsonClaimSet.CreateObjectFromJsonElement(jsonElement3);
				}
				return array;
			}
			if (jsonElement.ValueKind == 3)
			{
				DateTime dateTime;
				if (DateTime.TryParse(jsonElement.GetString(), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out dateTime))
				{
					return dateTime;
				}
				return jsonElement.GetString();
			}
			else
			{
				if (jsonElement.ValueKind == 7)
				{
					return null;
				}
				if (jsonElement.ValueKind == 1)
				{
					return jsonElement.ToString();
				}
				if (jsonElement.ValueKind == 6)
				{
					return false;
				}
				if (jsonElement.ValueKind == 5)
				{
					return true;
				}
				if (jsonElement.ValueKind == 4)
				{
					long num3;
					if (jsonElement.TryGetInt64(ref num3))
					{
						return num3;
					}
					int num4;
					if (jsonElement.TryGetInt32(ref num4))
					{
						return num4;
					}
					decimal num5;
					if (jsonElement.TryGetDecimal(ref num5))
					{
						return num5;
					}
					double num6;
					if (jsonElement.TryGetDouble(ref num6))
					{
						return num6;
					}
					uint num7;
					if (jsonElement.TryGetUInt32(ref num7))
					{
						return num7;
					}
					ulong num8;
					if (jsonElement.TryGetUInt64(ref num8))
					{
						return num8;
					}
				}
				return jsonElement.GetString();
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600009C RID: 156 RVA: 0x0000613C File Offset: 0x0000433C
		internal Dictionary<string, JsonElement> Elements { get; } = new Dictionary<string, JsonElement>();

		// Token: 0x0600009D RID: 157 RVA: 0x00006144 File Offset: 0x00004344
		internal Claim GetClaim(string key, string issuer)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			JsonElement jsonElement;
			if (!this.Elements.TryGetValue(key, out jsonElement))
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX14304: Claim with name '{0}' does not exist in the payload.", new object[] { key })));
			}
			return JsonClaimSet.CreateClaimFromJsonElement(key, issuer, jsonElement);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00006198 File Offset: 0x00004398
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
				return type.ToString();
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000626C File Offset: 0x0000446C
		internal string GetStringValue(string key)
		{
			JsonElement jsonElement;
			if (this.Elements.TryGetValue(key, out jsonElement) && jsonElement.ValueKind == 3)
			{
				return jsonElement.GetString();
			}
			return string.Empty;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000062A0 File Offset: 0x000044A0
		internal DateTime GetDateTime(string key)
		{
			JsonElement jsonElement;
			if (!this.Elements.TryGetValue(key, out jsonElement))
			{
				return DateTime.MinValue;
			}
			return EpochTime.DateTime(Convert.ToInt64(Math.Truncate(Convert.ToDouble(JsonClaimSet.ParseTimeValue(key, jsonElement), CultureInfo.InvariantCulture))));
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000062E8 File Offset: 0x000044E8
		internal T GetValue<T>(string key)
		{
			bool flag;
			return this.GetValue<T>(key, true, out flag);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00006300 File Offset: 0x00004500
		internal T GetValue<T>(string key, bool throwEx, out bool found)
		{
			JsonElement jsonElement;
			found = this.Elements.TryGetValue(key, out jsonElement);
			if (!found)
			{
				if (throwEx)
				{
					throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX14304: Claim with name '{0}' does not exist in the payload.", new object[] { key })));
				}
				return default(T);
			}
			else
			{
				if (typeof(T) == typeof(JsonElement))
				{
					return (T)((object)jsonElement);
				}
				try
				{
					if (jsonElement.ValueKind == 7)
					{
						if (typeof(T) == typeof(object) || typeof(T).IsClass || Nullable.GetUnderlyingType(typeof(T)) != null)
						{
							return (T)((object)null);
						}
						found = false;
						return default(T);
					}
					else
					{
						if (typeof(T) == typeof(JObject))
						{
							return (T)((object)JObject.Parse(jsonElement.ToString()));
						}
						if (typeof(T) == typeof(JArray))
						{
							return (T)((object)JArray.Parse(jsonElement.ToString()));
						}
						if (typeof(T) == typeof(object))
						{
							return (T)((object)JsonClaimSet.CreateObjectFromJsonElement(jsonElement));
						}
						if (typeof(T) == typeof(object[]))
						{
							if (jsonElement.ValueKind == 2)
							{
								int num = 0;
								foreach (JsonElement jsonElement2 in jsonElement.EnumerateArray())
								{
									num++;
								}
								object[] array = new object[num];
								int num2 = 0;
								foreach (JsonElement jsonElement3 in jsonElement.EnumerateArray())
								{
									array[num2++] = JsonClaimSet.CreateObjectFromJsonElement(jsonElement3);
								}
								return (T)((object)array);
							}
							return (T)((object)new object[] { JsonClaimSet.CreateObjectFromJsonElement(jsonElement) });
						}
						else
						{
							if (typeof(T) == typeof(string))
							{
								return (T)((object)jsonElement.ToString());
							}
							if (jsonElement.ValueKind == 3)
							{
								long num3;
								if (typeof(T) == typeof(long) && long.TryParse(jsonElement.ToString(), out num3))
								{
									return (T)((object)num3);
								}
								int num4;
								if (typeof(T) == typeof(int) && int.TryParse(jsonElement.ToString(), out num4))
								{
									return (T)((object)num4);
								}
								if (typeof(T) == typeof(DateTime))
								{
									DateTime dateTime;
									if (DateTime.TryParse(jsonElement.GetString(), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out dateTime))
									{
										return (T)((object)dateTime);
									}
									return JsonSerializer.Deserialize<T>(jsonElement.GetRawText(), null);
								}
								else
								{
									double num5;
									if (typeof(T) == typeof(double) && double.TryParse(jsonElement.ToString(), out num5))
									{
										return (T)((object)num5);
									}
									float num6;
									if (typeof(T) == typeof(float) && float.TryParse(jsonElement.ToString(), out num6))
									{
										return (T)((object)num6);
									}
								}
							}
							return JsonSerializer.Deserialize<T>(jsonElement.GetRawText(), null);
						}
					}
				}
				catch (Exception ex)
				{
					found = false;
					if (throwEx)
					{
						throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX14305: Unable to convert the '{0}' json property to the following type: '{1}'. Property type was: '{2}'. Value: '{3}'.", new object[]
						{
							key,
							typeof(T),
							jsonElement.ValueKind,
							jsonElement.GetRawText()
						}), ex));
					}
				}
				return default(T);
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000067B8 File Offset: 0x000049B8
		internal bool TryGetClaim(string key, string issuer, out Claim claim)
		{
			JsonElement jsonElement;
			if (!this.Elements.TryGetValue(key, out jsonElement))
			{
				claim = null;
				return false;
			}
			claim = JsonClaimSet.CreateClaimFromJsonElement(key, issuer, jsonElement);
			return true;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000067E8 File Offset: 0x000049E8
		internal bool TryGetValue<T>(string key, out T value)
		{
			bool flag;
			value = this.GetValue<T>(key, false, out flag);
			return flag;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00006808 File Offset: 0x00004A08
		internal bool HasClaim(string claimName)
		{
			JsonElement jsonElement;
			return this.Elements.TryGetValue(claimName, out jsonElement);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00006824 File Offset: 0x00004A24
		private static long ParseTimeValue(string claimName, JsonElement jsonElement)
		{
			if (jsonElement.ValueKind == 4)
			{
				long num;
				if (jsonElement.TryGetInt64(ref num))
				{
					return num;
				}
				double num2;
				if (jsonElement.TryGetDouble(ref num2))
				{
					return (long)num2;
				}
				int num3;
				if (jsonElement.TryGetInt32(ref num3))
				{
					return (long)num3;
				}
				decimal num4;
				if (jsonElement.TryGetDecimal(ref num4))
				{
					return (long)num4;
				}
			}
			if (jsonElement.ValueKind == 3)
			{
				string @string = jsonElement.GetString();
				long num5;
				if (long.TryParse(@string, out num5))
				{
					return num5;
				}
				float num6;
				if (float.TryParse(@string, out num6))
				{
					return (long)num6;
				}
				double num7;
				if (double.TryParse(@string, out num7))
				{
					return (long)num7;
				}
			}
			throw LogHelper.LogExceptionMessage(new FormatException(LogHelper.FormatInvariant("IDX14300: Could not parse '{0}' : '{1}' as a '{2}'.", new object[]
			{
				claimName,
				jsonElement.ToString(),
				typeof(long)
			})));
		}

		// Token: 0x0400003C RID: 60
		private IList<Claim> _claims;

		// Token: 0x0400003D RID: 61
		private readonly object _claimsLock = new object();
	}
}
