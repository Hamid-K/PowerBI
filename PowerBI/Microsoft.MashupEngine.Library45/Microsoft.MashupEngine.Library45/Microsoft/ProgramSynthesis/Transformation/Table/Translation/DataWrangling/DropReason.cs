using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Translation.DataWrangling
{
	// Token: 0x02001B42 RID: 6978
	[JsonConverter(typeof(StringEnumConverter))]
	public enum DropReason
	{
		// Token: 0x04005705 RID: 22277
		Empty,
		// Token: 0x04005706 RID: 22278
		Duplicate,
		// Token: 0x04005707 RID: 22279
		Constant,
		// Token: 0x04005708 RID: 22280
		Index,
		// Token: 0x04005709 RID: 22281
		Outlier
	}
}
