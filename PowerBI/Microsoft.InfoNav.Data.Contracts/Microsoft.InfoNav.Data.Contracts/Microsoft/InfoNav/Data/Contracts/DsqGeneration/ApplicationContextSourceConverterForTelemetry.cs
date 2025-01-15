using System;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Utils;
using Newtonsoft.Json;

namespace Microsoft.InfoNav.Data.Contracts.DsqGeneration
{
	// Token: 0x020000FE RID: 254
	public sealed class ApplicationContextSourceConverterForTelemetry : JsonConverter<ApplicationContextSource>
	{
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x0000D98B File Offset: 0x0000BB8B
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0000D98E File Offset: 0x0000BB8E
		public override ApplicationContextSource ReadJson(JsonReader reader, Type objectType, ApplicationContextSource existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0000D998 File Offset: 0x0000BB98
		public override void WriteJson(JsonWriter writer, ApplicationContextSource source, JsonSerializer serializer)
		{
			if (source == null)
			{
				writer.WriteNull();
				return;
			}
			writer.WriteStartObject();
			if (source.ReportId != null)
			{
				writer.WritePropertyName("ReportId");
				writer.WriteValue(source.ReportId);
			}
			if (source.VisualId != null)
			{
				writer.WritePropertyName("VisualId");
				writer.WriteValue(source.VisualId);
			}
			if (source.DashboardId != null)
			{
				writer.WritePropertyName("DashboardId");
				writer.WriteValue(source.DashboardId);
			}
			if (source.TileId != null)
			{
				writer.WritePropertyName("TileId");
				writer.WriteValue(source.TileId);
			}
			if (source.Operation != null)
			{
				writer.WritePropertyName("Operation");
				writer.WriteValue(source.Operation);
			}
			if (source.CustomProperties != null)
			{
				writer.WritePropertyName("CustomProperties");
				writer.WriteValue(JsonConvert.SerializeObject(source.CustomProperties).MarkAsCustomerContent());
			}
			writer.WriteEndObject();
		}

		// Token: 0x040002C3 RID: 707
		private const string ReportId = "ReportId";

		// Token: 0x040002C4 RID: 708
		private const string VisualId = "VisualId";

		// Token: 0x040002C5 RID: 709
		private const string DashboardId = "DashboardId";

		// Token: 0x040002C6 RID: 710
		private const string TileId = "TileId";

		// Token: 0x040002C7 RID: 711
		private const string Operation = "Operation";

		// Token: 0x040002C8 RID: 712
		private const string CustomProperties = "CustomProperties";
	}
}
