using System;
using Newtonsoft.Json;

namespace Microsoft.InfoNav.Data.Contracts.DataShapeResult
{
	// Token: 0x0200010D RID: 269
	public sealed class DataShapeResultJsonConverter : JsonConverter
	{
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x0000EF97 File Offset: 0x0000D197
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0000EF9A File Offset: 0x0000D19A
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException("Json.Net should not try to use DataShapeResultJsonConverter to serialize the DSR since CanWrite is false.");
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0000EFA8 File Offset: 0x0000D1A8
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			DsrVersion dsrVersion;
			return DataShapeResultParser.Instance.ParseDataShapeResult(reader, serializer, true, out dsrVersion);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0000EFC5 File Offset: 0x0000D1C5
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(DataShapeResult);
		}
	}
}
