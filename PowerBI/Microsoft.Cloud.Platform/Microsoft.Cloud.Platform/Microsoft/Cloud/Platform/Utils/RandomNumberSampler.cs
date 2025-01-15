using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000275 RID: 629
	public sealed class RandomNumberSampler
	{
		// Token: 0x060010BC RID: 4284 RVA: 0x00039F37 File Offset: 0x00038137
		public RandomNumberSampler(double samplingRate)
		{
			ExtendedDiagnostics.EnsureArgumentIsBetween(samplingRate, 0.0, 1.0, "samplingRate");
			this.m_samplingRate = samplingRate;
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x00039F6E File Offset: 0x0003816E
		public bool IsSelected()
		{
			return this.m_randomGenerator.NextDoubleThreadSafe() <= this.m_samplingRate;
		}

		// Token: 0x04000631 RID: 1585
		private readonly Random m_randomGenerator = new Random();

		// Token: 0x04000632 RID: 1586
		private readonly double m_samplingRate;
	}
}
