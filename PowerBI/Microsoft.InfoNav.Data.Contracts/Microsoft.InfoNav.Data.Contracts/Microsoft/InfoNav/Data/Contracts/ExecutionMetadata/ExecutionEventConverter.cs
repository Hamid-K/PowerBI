using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Microsoft.InfoNav.Data.Contracts.ExecutionMetadata
{
	// Token: 0x020000F0 RID: 240
	public sealed class ExecutionEventConverter : JsonConverter<ExecutionEvent>
	{
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x0000D40C File Offset: 0x0000B60C
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0000D40F File Offset: 0x0000B60F
		public override ExecutionEvent ReadJson(JsonReader reader, Type objectType, ExecutionEvent existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0000D418 File Offset: 0x0000B618
		public override void WriteJson(JsonWriter writer, ExecutionEvent value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			writer.WriteStartObject();
			writer.WritePropertyName("Id");
			writer.WriteValue(value.Id);
			string parentId = value.ParentId;
			if (parentId != null)
			{
				writer.WritePropertyName("ParentId");
				writer.WriteValue(parentId);
			}
			writer.WritePropertyName("Name");
			writer.WriteValue(value.Name);
			writer.WritePropertyName("Component");
			writer.WriteValue(value.Component);
			writer.WritePropertyName("Start");
			writer.WriteValue(value.Start);
			DateTime? end = value.End;
			if (end != null)
			{
				writer.WritePropertyName("End");
				writer.WriteValue(end);
			}
			string metricsRawJson = value.MetricsRawJson;
			if (metricsRawJson != null)
			{
				writer.WritePropertyName("Metrics");
				writer.WriteRawValue(metricsRawJson);
			}
			Dictionary<string, object> metrics = value.Metrics;
			if (metrics != null)
			{
				writer.WritePropertyName("Metrics");
				serializer.Serialize(writer, metrics);
			}
			writer.WriteEndObject();
		}

		// Token: 0x040002B3 RID: 691
		private const string IdPropertyName = "Id";

		// Token: 0x040002B4 RID: 692
		private const string ParentIdPropertyName = "ParentId";

		// Token: 0x040002B5 RID: 693
		private const string NamePropertyName = "Name";

		// Token: 0x040002B6 RID: 694
		private const string ComponentPropertyName = "Component";

		// Token: 0x040002B7 RID: 695
		private const string StartPropertyName = "Start";

		// Token: 0x040002B8 RID: 696
		private const string EndPropertyName = "End";

		// Token: 0x040002B9 RID: 697
		private const string MetricsPropertyName = "Metrics";
	}
}
