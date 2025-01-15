using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000022 RID: 34
	internal sealed class DataReductionAlgorithmTelemetryStateCollectionConverter : JsonConverter<List<DataReductionAlgorithmTelemetryState>>
	{
		// Token: 0x06000177 RID: 375 RVA: 0x00008DCC File Offset: 0x00006FCC
		public override List<DataReductionAlgorithmTelemetryState> ReadJson(JsonReader reader, Type objectType, List<DataReductionAlgorithmTelemetryState> existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			if (reader.TokenType == JsonToken.StartArray)
			{
				return serializer.Deserialize<List<DataReductionAlgorithmTelemetryState>>(reader);
			}
			DataReductionAlgorithmTelemetryState dataReductionAlgorithmTelemetryState = serializer.Deserialize<DataReductionAlgorithmTelemetryState>(reader);
			return new List<DataReductionAlgorithmTelemetryState>(1) { dataReductionAlgorithmTelemetryState };
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00008E0D File Offset: 0x0000700D
		public override void WriteJson(JsonWriter writer, List<DataReductionAlgorithmTelemetryState> value, JsonSerializer serializer)
		{
			if (value != null && value.Count == 1)
			{
				serializer.Serialize(writer, value[0]);
				return;
			}
			serializer.Serialize(writer, value);
		}
	}
}
