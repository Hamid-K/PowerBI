using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020016A4 RID: 5796
	[Flags]
	public enum ValueFlags : byte
	{
		// Token: 0x04004EA9 RID: 20137
		HasMeta = 32,
		// Token: 0x04004EAA RID: 20138
		HasType = 64,
		// Token: 0x04004EAB RID: 20139
		None = 0,
		// Token: 0x04004EAC RID: 20140
		All = 96
	}
}
