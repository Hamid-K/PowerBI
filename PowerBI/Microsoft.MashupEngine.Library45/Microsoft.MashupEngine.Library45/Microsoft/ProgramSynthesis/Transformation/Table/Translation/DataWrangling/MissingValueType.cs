using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Translation.DataWrangling
{
	// Token: 0x02001B47 RID: 6983
	[Flags]
	[JsonConverter(typeof(StringEnumConverter))]
	public enum MissingValueType
	{
		// Token: 0x04005710 RID: 22288
		NAValue = 1,
		// Token: 0x04005711 RID: 22289
		EmptyString = 2,
		// Token: 0x04005712 RID: 22290
		WhiteSpace = 4,
		// Token: 0x04005713 RID: 22291
		NanString = 8,
		// Token: 0x04005714 RID: 22292
		All = -1,
		// Token: 0x04005715 RID: 22293
		None = 0
	}
}
