using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Translation.DataWrangling
{
	// Token: 0x02001B40 RID: 6976
	[JsonConverter(typeof(StringEnumConverter))]
	public enum DataType
	{
		// Token: 0x040056FA RID: 22266
		Object,
		// Token: 0x040056FB RID: 22267
		String,
		// Token: 0x040056FC RID: 22268
		DateTime64,
		// Token: 0x040056FD RID: 22269
		TimeDelta,
		// Token: 0x040056FE RID: 22270
		Category,
		// Token: 0x040056FF RID: 22271
		Float,
		// Token: 0x04005700 RID: 22272
		Int,
		// Token: 0x04005701 RID: 22273
		UInt,
		// Token: 0x04005702 RID: 22274
		Bool
	}
}
