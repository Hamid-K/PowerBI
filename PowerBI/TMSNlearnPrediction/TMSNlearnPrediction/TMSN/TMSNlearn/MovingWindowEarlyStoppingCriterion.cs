using System;
using System.Collections.Generic;
using Microsoft.MachineLearning;
using Microsoft.MachineLearning.CommandLine;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x0200049C RID: 1180
	public abstract class MovingWindowEarlyStoppingCriterion : EarlyStoppingCriterion<MovingWindowEarlyStoppingCriterion.Arguments>
	{
		// Token: 0x06001892 RID: 6290 RVA: 0x0008B968 File Offset: 0x00089B68
		internal MovingWindowEarlyStoppingCriterion(MovingWindowEarlyStoppingCriterion.Arguments args, bool lowerIsBetter)
			: base(args, lowerIsBetter)
		{
			Contracts.CheckUserArg(this._args.threshold >= 0f && args.threshold <= 1f, "The threshold should be in range [0,1].");
			Contracts.CheckUserArg(this._args.windowSize > 0, "Window size must be a positive value.");
			this._pastScores = new Queue<float>(this._args.windowSize);
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x0008B9DC File Offset: 0x00089BDC
		private float GetRecentAvg(Queue<float> recentScores)
		{
			float num = 0f;
			foreach (float num2 in recentScores)
			{
				float num3 = num2;
				num += num3;
			}
			return num / (float)recentScores.Count;
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x0008BA38 File Offset: 0x00089C38
		private float GetRecentBest(IEnumerable<float> recentScores)
		{
			float num = (this._lowerIsBetter ? float.PositiveInfinity : float.NegativeInfinity);
			foreach (float num2 in recentScores)
			{
				float num3 = num2;
				if (num3 > num != this._lowerIsBetter)
				{
					num = num3;
				}
			}
			return num;
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x0008BAA0 File Offset: 0x00089CA0
		protected bool CheckRecentScores(float score, int windowSize, out float recentBest, out float recentAverage)
		{
			if (this._pastScores.Count >= windowSize)
			{
				this._pastScores.Dequeue();
				this._pastScores.Enqueue(score);
				recentAverage = this.GetRecentAvg(this._pastScores);
				recentBest = this.GetRecentBest(this._pastScores);
				return true;
			}
			this._pastScores.Enqueue(score);
			recentBest = 0f;
			recentAverage = 0f;
			return false;
		}

		// Token: 0x04000ECD RID: 3789
		protected Queue<float> _pastScores;

		// Token: 0x0200049D RID: 1181
		public class Arguments : EarlyStoppingCriterion<MovingWindowEarlyStoppingCriterion.Arguments>.ArgumentsBase
		{
			// Token: 0x04000ECE RID: 3790
			[Argument(0, HelpText = "Threshold in range [0,1].", ShortName = "th")]
			public float threshold = 0.01f;

			// Token: 0x04000ECF RID: 3791
			[Argument(0, HelpText = "The window size.", ShortName = "w")]
			public int windowSize = 5;
		}
	}
}
