using System;

namespace Microsoft.Lucia.Diagnostics
{
	// Token: 0x0200003A RID: 58
	public interface ITracingProvider
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000E8 RID: 232
		[Nullable]
		LevelTracer Fatal { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000E9 RID: 233
		[Nullable]
		LevelTracer Error { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000EA RID: 234
		[Nullable]
		LevelTracer Warning { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000EB RID: 235
		[Nullable]
		LevelTracer Info { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000EC RID: 236
		[Nullable]
		LevelTracer Verbose { get; }
	}
}
