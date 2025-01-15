using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Utils.Clustering
{
	// Token: 0x02000616 RID: 1558
	internal class Distribution
	{
		// Token: 0x060021D6 RID: 8662 RVA: 0x00060598 File Offset: 0x0005E798
		internal Distribution(params Record<SamplingStrategy, double>[] elementProbabilities)
		{
			this._elementProbabilities = elementProbabilities;
			if (Math.Abs(this._elementProbabilities.Sum((Record<SamplingStrategy, double> pair) => pair.Item2) - 1.0) > 0.001)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Element probabilities should add up to 1.0", Array.Empty<object>())));
			}
			if (this._elementProbabilities.Any((Record<SamplingStrategy, double> pair) => pair.Item2 > 1.0 || pair.Item2 <= 0.0))
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Element probabilities should be between 0.0 (exclusive) and 1.0 (inclusive).", Array.Empty<object>())));
			}
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x00060658 File Offset: 0x0005E858
		internal SamplingStrategy Sample(Random rng)
		{
			if (this._elementProbabilities.Count == 1)
			{
				return this._elementProbabilities.First<Record<SamplingStrategy, double>>().Item1;
			}
			double num = rng.NextDouble();
			foreach (Record<SamplingStrategy, double> record in this._elementProbabilities)
			{
				SamplingStrategy item = record.Item1;
				double item2 = record.Item2;
				if (num < item2)
				{
					return item;
				}
				num -= item2;
			}
			return this._elementProbabilities.Last<Record<SamplingStrategy, double>>().Item1;
		}

		// Token: 0x0400102D RID: 4141
		private readonly IReadOnlyList<Record<SamplingStrategy, double>> _elementProbabilities;
	}
}
