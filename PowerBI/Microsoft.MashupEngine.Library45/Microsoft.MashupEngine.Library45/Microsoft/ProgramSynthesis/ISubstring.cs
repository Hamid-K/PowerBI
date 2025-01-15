using System;

namespace Microsoft.ProgramSynthesis
{
	// Token: 0x0200008D RID: 141
	public interface ISubstring
	{
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000319 RID: 793
		string Source { get; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600031A RID: 794
		uint Start { get; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600031B RID: 795
		uint End { get; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600031C RID: 796
		uint Length { get; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600031D RID: 797
		string Value { get; }
	}
}
