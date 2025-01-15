using System;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Metrics
{
	// Token: 0x020006B3 RID: 1715
	public class NormalizedDiscountedCumulativeGain
	{
		// Token: 0x06002521 RID: 9505 RVA: 0x000671D0 File Offset: 0x000653D0
		public double Evaluate(int k, double[] relevances)
		{
			double num = relevances.Take(k).Select(new Func<double, int, double>(this.DiscountedGain)).Sum();
			double num2 = relevances.OrderByDescending((double r) => r).Take(k).Select(new Func<double, int, double>(this.DiscountedGain))
				.Sum();
			return num / num2;
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x0006723E File Offset: 0x0006543E
		private double DiscountedGain(double relevance, int rankIndex)
		{
			return (Math.Pow(2.0, relevance) - 1.0) / Math.Log((double)(rankIndex + 2), 2.0);
		}
	}
}
