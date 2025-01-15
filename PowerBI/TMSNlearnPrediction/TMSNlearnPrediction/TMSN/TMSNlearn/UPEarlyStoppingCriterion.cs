using System;
using Microsoft.MachineLearning;
using Microsoft.MachineLearning.CommandLine;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004A2 RID: 1186
	public sealed class UPEarlyStoppingCriterion : EarlyStoppingCriterion<UPEarlyStoppingCriterion.Arguments>
	{
		// Token: 0x0600189E RID: 6302 RVA: 0x0008BCCD File Offset: 0x00089ECD
		public UPEarlyStoppingCriterion(UPEarlyStoppingCriterion.Arguments args, bool lowerIsBetter)
			: base(args, lowerIsBetter)
		{
			Contracts.CheckUserArg(this._args.windowSize > 0, "Window size must be a positive value.");
			this._prevScore = (this._lowerIsBetter ? float.PositiveInfinity : float.NegativeInfinity);
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x0008BD0C File Offset: 0x00089F0C
		public override bool CheckScore(float validationScore, float trainingScore, out bool isBestCandidate)
		{
			isBestCandidate = base.CheckBestScore(validationScore);
			this._count = ((validationScore < this._prevScore != this._lowerIsBetter) ? (this._count + 1) : 0);
			this._prevScore = validationScore;
			return this._count >= this._args.windowSize;
		}

		// Token: 0x04000ED1 RID: 3793
		private int _count;

		// Token: 0x04000ED2 RID: 3794
		private float _prevScore;

		// Token: 0x020004A3 RID: 1187
		public class Arguments : EarlyStoppingCriterion<UPEarlyStoppingCriterion.Arguments>.ArgumentsBase
		{
			// Token: 0x04000ED3 RID: 3795
			[Argument(0, HelpText = "The window size.", ShortName = "w")]
			public int windowSize = 5;
		}
	}
}
