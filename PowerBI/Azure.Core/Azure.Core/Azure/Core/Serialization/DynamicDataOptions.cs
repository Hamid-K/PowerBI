using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Azure.Core.Serialization
{
	// Token: 0x020000C2 RID: 194
	[NullableContext(1)]
	[Nullable(0)]
	internal class DynamicDataOptions
	{
		// Token: 0x0600067D RID: 1661 RVA: 0x00016430 File Offset: 0x00014630
		public DynamicDataOptions()
		{
			this.DateTimeFormat = "o";
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00016443 File Offset: 0x00014643
		public DynamicDataOptions(DynamicDataOptions options)
		{
			this.PropertyNameFormat = options.PropertyNameFormat;
			this.DateTimeFormat = options.DateTimeFormat;
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x00016463 File Offset: 0x00014663
		// (set) Token: 0x06000680 RID: 1664 RVA: 0x0001646B File Offset: 0x0001466B
		public JsonPropertyNames PropertyNameFormat { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x00016474 File Offset: 0x00014674
		// (set) Token: 0x06000682 RID: 1666 RVA: 0x0001647C File Offset: 0x0001467C
		public string DateTimeFormat { get; set; }

		// Token: 0x06000683 RID: 1667 RVA: 0x00016488 File Offset: 0x00014688
		internal static JsonSerializerOptions ToSerializerOptions(DynamicDataOptions options)
		{
			JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
			{
				Converters = 
				{
					new DynamicData.DynamicTimeSpanConverter(),
					new DynamicData.DynamicDateTimeConverter(options.DateTimeFormat),
					new DynamicData.DynamicDateTimeOffsetConverter(options.DateTimeFormat)
				}
			};
			JsonPropertyNames propertyNameFormat = options.PropertyNameFormat;
			if (propertyNameFormat != JsonPropertyNames.UseExact && propertyNameFormat == JsonPropertyNames.CamelCase)
			{
				jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
			}
			return jsonSerializerOptions;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x000164F4 File Offset: 0x000146F4
		internal static DynamicDataOptions FromSerializerOptions(JsonSerializerOptions options)
		{
			DynamicDataOptions dynamicDataOptions = new DynamicDataOptions();
			DynamicData.DynamicDateTimeConverter dynamicDateTimeConverter = options.Converters.FirstOrDefault((JsonConverter c) => c is DynamicData.DynamicDateTimeConverter) as DynamicData.DynamicDateTimeConverter;
			if (dynamicDateTimeConverter != null)
			{
				dynamicDataOptions.DateTimeFormat = dynamicDateTimeConverter.Format;
			}
			if (options.PropertyNamingPolicy == JsonNamingPolicy.CamelCase)
			{
				dynamicDataOptions.PropertyNameFormat = JsonPropertyNames.CamelCase;
			}
			return dynamicDataOptions;
		}
	}
}
