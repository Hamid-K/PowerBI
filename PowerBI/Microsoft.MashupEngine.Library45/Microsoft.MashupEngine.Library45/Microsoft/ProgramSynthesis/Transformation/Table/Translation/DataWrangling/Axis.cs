using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Translation.DataWrangling
{
	// Token: 0x02001B49 RID: 6985
	[JsonConverter(typeof(StringEnumConverter))]
	public enum Axis
	{
		// Token: 0x04005718 RID: 22296
		Index,
		// Token: 0x04005719 RID: 22297
		Columns
	}
}
