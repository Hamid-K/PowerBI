using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Translation.DataWrangling
{
	// Token: 0x02001B4D RID: 6989
	[JsonConverter(typeof(StringEnumConverter))]
	public enum FillMethod
	{
		// Token: 0x04005723 RID: 22307
		None,
		// Token: 0x04005724 RID: 22308
		Mode,
		// Token: 0x04005725 RID: 22309
		Mean,
		// Token: 0x04005726 RID: 22310
		RoundedMean,
		// Token: 0x04005727 RID: 22311
		Max,
		// Token: 0x04005728 RID: 22312
		Min,
		// Token: 0x04005729 RID: 22313
		FixedValueZero
	}
}
