using System;

namespace Microsoft.Lucia.Diagnostics
{
	// Token: 0x0200003D RID: 61
	public static class TracingProviderFactory
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00003D4B File Offset: 0x00001F4B
		public static ITracingProvider Empty { get; } = new TracingProviderFactory.EmptyTracer();

		// Token: 0x020001FD RID: 509
		private sealed class EmptyTracer : ITracingProvider
		{
			// Token: 0x1700032E RID: 814
			// (get) Token: 0x06000B00 RID: 2816 RVA: 0x000147E1 File Offset: 0x000129E1
			public LevelTracer Fatal
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700032F RID: 815
			// (get) Token: 0x06000B01 RID: 2817 RVA: 0x000147E4 File Offset: 0x000129E4
			public LevelTracer Error
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000330 RID: 816
			// (get) Token: 0x06000B02 RID: 2818 RVA: 0x000147E7 File Offset: 0x000129E7
			public LevelTracer Warning
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000331 RID: 817
			// (get) Token: 0x06000B03 RID: 2819 RVA: 0x000147EA File Offset: 0x000129EA
			public LevelTracer Info
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000332 RID: 818
			// (get) Token: 0x06000B04 RID: 2820 RVA: 0x000147ED File Offset: 0x000129ED
			public LevelTracer Verbose
			{
				get
				{
					return null;
				}
			}
		}
	}
}
