using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Newtonsoft.Json;

namespace Microsoft.DataShaping
{
	// Token: 0x0200000C RID: 12
	public static class SerializationUtils
	{
		// Token: 0x06000087 RID: 135 RVA: 0x000031BA File Offset: 0x000013BA
		public static string SerializeForTelemetry(this object obj, Action<string> sanitizedTrace)
		{
			return SerializationUtils.UnescapePIITags(JsonConvert.SerializeObject(obj, obj.GetType(), SerializationUtils.SerializerSettings), sanitizedTrace);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000031D4 File Offset: 0x000013D4
		public static string ToJsonString(this DataContractJsonSerializer serializer, object o)
		{
			string @string;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				serializer.WriteObject(memoryStream, o);
				@string = SerializationUtils.JsonEncoding.GetString(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
			}
			return @string;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003228 File Offset: 0x00001428
		private static string UnescapePIITags(string input, Action<string> sanitizedTrace)
		{
			string text = input.Replace("<\\/pi>", "</pi>");
			if (text.Length != input.Length)
			{
				sanitizedTrace("Replaced escaped pii tags in SerializationUtils.UnescapePIITags.");
			}
			return text;
		}

		// Token: 0x0400003B RID: 59
		private const string EscapedPIClosingTag = "<\\/pi>";

		// Token: 0x0400003C RID: 60
		private const string UnescapedPIClosingTag = "</pi>";

		// Token: 0x0400003D RID: 61
		private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
		{
			MaxDepth = new int?(128)
		};

		// Token: 0x0400003E RID: 62
		private static readonly Encoding JsonEncoding = Encoding.UTF8;
	}
}
