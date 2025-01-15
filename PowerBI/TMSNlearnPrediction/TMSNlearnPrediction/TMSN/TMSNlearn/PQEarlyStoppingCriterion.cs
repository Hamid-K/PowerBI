using System;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004A1 RID: 1185
	public sealed class PQEarlyStoppingCriterion : MovingWindowEarlyStoppingCriterion
	{
		// Token: 0x0600189C RID: 6300 RVA: 0x0008BC43 File Offset: 0x00089E43
		public PQEarlyStoppingCriterion(MovingWindowEarlyStoppingCriterion.Arguments args, bool lowerIsBetter)
			: base(args, lowerIsBetter)
		{
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x0008BC50 File Offset: 0x00089E50
		public override bool CheckScore(float validationScore, float trainingScore, out bool isBestCandidate)
		{
			isBestCandidate = base.CheckBestScore(validationScore);
			float num;
			float num2;
			if (!base.CheckRecentScores(trainingScore, this._args.windowSize, out num, out num2))
			{
				return false;
			}
			if (this._lowerIsBetter)
			{
				return validationScore * num >= (1f + this._args.threshold) * this._bestScore * num2;
			}
			return validationScore * num <= (1f - this._args.threshold) * this._bestScore * num2;
		}
	}
}
