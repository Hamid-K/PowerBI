using System;
using System.IO;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020004A1 RID: 1185
	public static class JsonConvertUtils
	{
		// Token: 0x06001A99 RID: 6809 RVA: 0x00050099 File Offset: 0x0004E299
		public static object DeserializeObject(string json, Type type)
		{
			return JsonConvert.DeserializeObject(json, type, JsonConvertUtils.JsonSerializerSettings);
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x000500A7 File Offset: 0x0004E2A7
		public static T DeserializeObject<T>(string json)
		{
			return JsonConvert.DeserializeObject<T>(json, JsonConvertUtils.JsonSerializerSettings);
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x000500B4 File Offset: 0x0004E2B4
		public static T DeserializeObject<T>(string value, params JsonConverter[] converters)
		{
			JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
			{
				DateParseHandling = DateParseHandling.None,
				Converters = converters
			};
			return JsonConvert.DeserializeObject<T>(value, jsonSerializerSettings);
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x000500DC File Offset: 0x0004E2DC
		public static T Deserialize<T>(JsonSerializer serializer, JsonReader jsonReader)
		{
			return serializer.Deserialize<T>(jsonReader);
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x000500E8 File Offset: 0x0004E2E8
		public static T Deserialize<T>(JsonSerializer serializer, TextReader reader)
		{
			T t;
			using (JsonTextReader jsonTextReader = new JsonTextReader(reader))
			{
				t = JsonConvertUtils.Deserialize<T>(serializer, jsonTextReader);
			}
			return t;
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x00050124 File Offset: 0x0004E324
		public static T Deserialize<T>(JsonSerializer serializer, Stream stream)
		{
			T t;
			using (StreamReader streamReader = new StreamReader(stream))
			{
				t = JsonConvertUtils.Deserialize<T>(serializer, streamReader);
			}
			return t;
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x00050160 File Offset: 0x0004E360
		public static T Deserialize<T>(JsonTextReader reader)
		{
			return JsonConvertUtils.JsonSerializer.Deserialize<T>(reader);
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x0005016D File Offset: 0x0004E36D
		public static T Deserialize<T>(TextReader reader)
		{
			return JsonConvertUtils.Deserialize<T>(JsonConvertUtils.JsonSerializer, reader);
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x0005017A File Offset: 0x0004E37A
		public static T Deserialize<T>(Stream stream)
		{
			return JsonConvertUtils.Deserialize<T>(JsonConvertUtils.JsonSerializer, stream);
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x00050187 File Offset: 0x0004E387
		public static string SerializeObject(object obj)
		{
			return JsonConvert.SerializeObject(obj, JsonConvertUtils.JsonSerializerSettings);
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x00050194 File Offset: 0x0004E394
		public static string SerializeObject(object obj, Formatting formatting)
		{
			return JsonConvert.SerializeObject(obj, formatting, JsonConvertUtils.JsonSerializerSettings);
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x000501A4 File Offset: 0x0004E3A4
		public static string SerializeObject(object obj, params JsonConverter[] converters)
		{
			JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
			{
				DateParseHandling = DateParseHandling.None,
				Converters = converters
			};
			return JsonConvert.SerializeObject(obj, jsonSerializerSettings);
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x000501CC File Offset: 0x0004E3CC
		public static string SerializeObject(object obj, Formatting formatting, params JsonConverter[] converters)
		{
			JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
			{
				DateParseHandling = DateParseHandling.None,
				Converters = converters
			};
			return JsonConvert.SerializeObject(obj, formatting, jsonSerializerSettings);
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x000501F5 File Offset: 0x0004E3F5
		public static void SerializeObject(object obj, JsonTextWriter jsonWriter)
		{
			JsonConvertUtils.JsonSerializer.Serialize(jsonWriter, obj);
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x00050204 File Offset: 0x0004E404
		public static void SerializeObject(object obj, TextWriter textWriter)
		{
			using (JsonTextWriter jsonTextWriter = new JsonTextWriter(textWriter))
			{
				JsonConvertUtils.SerializeObject(obj, jsonTextWriter);
			}
		}

		// Token: 0x04000D1A RID: 3354
		private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
		{
			DateParseHandling = DateParseHandling.None
		};

		// Token: 0x04000D1B RID: 3355
		private static readonly JsonSerializer JsonSerializer = new JsonSerializer
		{
			DateParseHandling = DateParseHandling.None
		};
	}
}
