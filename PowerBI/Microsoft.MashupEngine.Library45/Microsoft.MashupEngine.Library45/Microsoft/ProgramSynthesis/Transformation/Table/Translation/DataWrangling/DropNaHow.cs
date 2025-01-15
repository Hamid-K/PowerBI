using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Translation.DataWrangling
{
	// Token: 0x02001B4A RID: 6986
	[JsonConverter(typeof(StringEnumConverter))]
	public enum DropNaHow
	{
		// Token: 0x0400571B RID: 22299
		Any,
		// Token: 0x0400571C RID: 22300
		All
	}
}
