using System;
using Microsoft.MachineLearning;
using Microsoft.MachineLearning.CommandLine;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x0200049E RID: 1182
	public sealed class GLEarlyStoppingCriterion : EarlyStoppingCriterion<GLEarlyStoppingCriterion.Arguments>
	{
		// Token: 0x06001897 RID: 6295 RVA: 0x0008BB28 File Offset: 0x00089D28
		public GLEarlyStoppingCriterion(GLEarlyStoppingCriterion.Arguments args, bool lowerIsBetter)
			: base(args, lowerIsBetter)
		{
			Contracts.CheckUserArg(this._args.threshold >= 0f && args.threshold <= 1f, "The threshould should be in range [0,1].");
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x0008BB64 File Offset: 0x00089D64
		public override bool CheckScore(float validationScore, float trainingScore, out bool isBestCandidate)
		{
			isBestCandidate = base.CheckBestScore(validationScore);
			if (this._lowerIsBetter)
			{
				return validationScore > (1f + this._args.threshold) * this._bestScore;
			}
			return validationScore < (1f - this._args.threshold) * this._bestScore;
		}

		// Token: 0x0200049F RID: 1183
		public class Arguments : EarlyStoppingCriterion<GLEarlyStoppingCriterion.Arguments>.ArgumentsBase
		{
			// Token: 0x04000ED0 RID: 3792
			[Argument(0, HelpText = "Threshold in range [0,1].", ShortName = "th")]
			public float threshold = 0.01f;
		}
	}
}
