using System;
using Microsoft.MachineLearning;
using Microsoft.MachineLearning.CommandLine;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x0200049A RID: 1178
	public sealed class TolerantEarlyStoppingCriterion : EarlyStoppingCriterion<TolerantEarlyStoppingCriterion.Arguments>
	{
		// Token: 0x0600188F RID: 6287 RVA: 0x0008B8ED File Offset: 0x00089AED
		public TolerantEarlyStoppingCriterion(TolerantEarlyStoppingCriterion.Arguments args, bool lowerIsBetter)
			: base(args, lowerIsBetter)
		{
			Contracts.CheckUserArg(this._args.threshold >= 0f, "Threshold cannot be a negative value.");
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x0008B916 File Offset: 0x00089B16
		public override bool CheckScore(float validationScore, float trainingScore, out bool isBestCandidate)
		{
			isBestCandidate = base.CheckBestScore(validationScore);
			if (this._lowerIsBetter)
			{
				return validationScore - this._bestScore > this._args.threshold;
			}
			return this._bestScore - validationScore > this._args.threshold;
		}

		// Token: 0x0200049B RID: 1179
		public class Arguments : EarlyStoppingCriterion<TolerantEarlyStoppingCriterion.Arguments>.ArgumentsBase
		{
			// Token: 0x04000ECC RID: 3788
			[Argument(0, HelpText = "Tolerance threshold. (Non negative value)", ShortName = "th")]
			public float threshold = 0.01f;
		}
	}
}
