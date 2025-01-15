using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Json;
using Microsoft.Identity.Json.Linq;

namespace Microsoft.Identity.Client.Utils
{
	// Token: 0x020001CA RID: 458
	internal static class JsonHelper
	{
		// Token: 0x06001433 RID: 5171 RVA: 0x00044D7F File Offset: 0x00042F7F
		internal static string SerializeToJson<T>(T toEncode)
		{
			return JsonConvert.SerializeObject(toEncode);
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x00044D8C File Offset: 0x00042F8C
		internal static T DeserializeFromJson<T>(string json)
		{
			if (string.IsNullOrEmpty(json))
			{
				return default(T);
			}
			return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
			{
				DateParseHandling = DateParseHandling.None
			});
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x00044DC0 File Offset: 0x00042FC0
		internal static T TryToDeserializeFromJson<T>(string json, RequestContext requestContext = null)
		{
			if (string.IsNullOrEmpty(json))
			{
				return default(T);
			}
			T t = default(T);
			try
			{
				t = JsonHelper.DeserializeFromJson<T>(json.ToByteArray());
			}
			catch (JsonException ex)
			{
				if (requestContext != null)
				{
					ILoggerAdapter logger = requestContext.Logger;
					if (logger != null)
					{
						logger.WarningPii(ex);
					}
				}
			}
			return t;
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x00044E20 File Offset: 0x00043020
		internal static T DeserializeFromJson<T>(byte[] jsonByteArray)
		{
			T t;
			if (jsonByteArray == null || jsonByteArray.Length == 0)
			{
				t = default(T);
				return t;
			}
			using (MemoryStream memoryStream = new MemoryStream(jsonByteArray))
			{
				using (StreamReader streamReader = new StreamReader(memoryStream, Encoding.UTF8))
				{
					t = (T)((object)JsonSerializer.Create().Deserialize(streamReader, typeof(T)));
				}
			}
			return t;
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x00044EA0 File Offset: 0x000430A0
		internal static string GetExistingOrEmptyString(JObject json, string key)
		{
			JToken jtoken;
			if (JsonHelper.TryGetValue(json, key, out jtoken))
			{
				return JsonHelper.GetValue<string>(jtoken);
			}
			return string.Empty;
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x00044EC4 File Offset: 0x000430C4
		internal static string ExtractExistingOrEmptyString(JObject json, string key)
		{
			JToken jtoken;
			if (JsonHelper.TryGetValue(json, key, out jtoken))
			{
				string value = JsonHelper.GetValue<string>(jtoken);
				json.Remove(key);
				return value;
			}
			return string.Empty;
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x00044EF0 File Offset: 0x000430F0
		internal static IDictionary<string, string> ExtractInnerJsonAsDictionary(JObject json, string key)
		{
			JToken jtoken;
			if (JsonHelper.TryGetValue(json, key, out jtoken))
			{
				IDictionary<string, string> dictionary = JsonHelper.ToJsonObject(jtoken).ToDictionary((KeyValuePair<string, JToken> pair) => pair.Key, (KeyValuePair<string, JToken> pair) => (string)pair.Value);
				json.Remove(key);
				return dictionary;
			}
			return null;
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x00044F5C File Offset: 0x0004315C
		internal static T ExtractExistingOrDefault<T>(JObject json, string key)
		{
			JToken jtoken;
			if (JsonHelper.TryGetValue(json, key, out jtoken))
			{
				T value = JsonHelper.GetValue<T>(jtoken);
				json.Remove(key);
				return value;
			}
			return default(T);
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x00044F8C File Offset: 0x0004318C
		internal static long ExtractParsedIntOrZero(JObject json, string key)
		{
			string text = JsonHelper.ExtractExistingOrEmptyString(json, key);
			long num;
			if (!string.IsNullOrWhiteSpace(text) && long.TryParse(text, out num))
			{
				return num;
			}
			return 0L;
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x00044FB7 File Offset: 0x000431B7
		internal static string JsonObjectToString(JObject jsonObject)
		{
			return jsonObject.ToString(Formatting.None, Array.Empty<JsonConverter>());
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x00044FC5 File Offset: 0x000431C5
		internal static JObject ParseIntoJsonObject(string json)
		{
			return JObject.Parse(json);
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x00044FCD File Offset: 0x000431CD
		internal static JObject ToJsonObject(JToken jsonNode)
		{
			return (JObject)jsonNode;
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x00044FD5 File Offset: 0x000431D5
		internal static bool TryGetValue(JObject json, string propertyName, out JToken value)
		{
			return json.TryGetValue(propertyName, out value);
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x00044FDF File Offset: 0x000431DF
		internal static T GetValue<T>(JToken json)
		{
			return json.Value<T>();
		}
	}
}
