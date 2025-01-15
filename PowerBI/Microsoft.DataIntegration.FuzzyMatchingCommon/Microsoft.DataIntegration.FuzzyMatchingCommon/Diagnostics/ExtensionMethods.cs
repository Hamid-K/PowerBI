using System;
using System.Diagnostics;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Diagnostics
{
	// Token: 0x02000049 RID: 73
	public static class ExtensionMethods
	{
		// Token: 0x0600024F RID: 591 RVA: 0x0001355C File Offset: 0x0001175C
		public static IDisposable StartUsing(this Stopwatch stopwatch)
		{
			return new ExtensionMethods.TimerBlock(stopwatch);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00013564 File Offset: 0x00011764
		public static void StartIf(this Stopwatch stopwatch, bool enabledFlag)
		{
			if (enabledFlag)
			{
				stopwatch.Start();
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0001356F File Offset: 0x0001176F
		public static void StopIf(this Stopwatch stopwatch, bool enabledFlag)
		{
			if (enabledFlag)
			{
				stopwatch.Stop();
			}
		}

		// Token: 0x020000D6 RID: 214
		public class TimerBlock : IDisposable
		{
			// Token: 0x060008A7 RID: 2215 RVA: 0x0002C3CA File Offset: 0x0002A5CA
			public TimerBlock(Stopwatch stopwatch)
			{
				this.Stopwatch = stopwatch;
				this.Stopwatch.Start();
			}

			// Token: 0x060008A8 RID: 2216 RVA: 0x0002C3E4 File Offset: 0x0002A5E4
			public void Dispose()
			{
				this.Stopwatch.Stop();
			}

			// Token: 0x0400020C RID: 524
			private Stopwatch Stopwatch;
		}
	}
}
