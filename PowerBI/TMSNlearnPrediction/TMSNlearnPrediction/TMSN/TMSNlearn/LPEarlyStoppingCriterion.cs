using System;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004A0 RID: 1184
	public sealed class LPEarlyStoppingCriterion : MovingWindowEarlyStoppingCriterion
	{
		// Token: 0x0600189A RID: 6298 RVA: 0x0008BBCC File Offset: 0x00089DCC
		public LPEarlyStoppingCriterion(MovingWindowEarlyStoppingCriterion.Arguments args, bool lowerIsBetter)
			: base(args, lowerIsBetter)
		{
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x0008BBD8 File Offset: 0x00089DD8
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
				return num2 <= (1f + this._args.threshold) * num;
			}
			return num2 >= (1f - this._args.threshold) * num;
		}
	}
}
