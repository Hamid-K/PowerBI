using System;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x02000498 RID: 1176
	public abstract class EarlyStoppingCriterion<TArguments> : IEarlyStoppingCriterion where TArguments : EarlyStoppingCriterion<TArguments>.ArgumentsBase
	{
		// Token: 0x0600188B RID: 6283 RVA: 0x0008B885 File Offset: 0x00089A85
		internal EarlyStoppingCriterion(TArguments args, bool lowerIsBetter)
		{
			this._args = args;
			this._lowerIsBetter = lowerIsBetter;
			this._bestScore = (this._lowerIsBetter ? float.PositiveInfinity : float.NegativeInfinity);
		}

		// Token: 0x0600188C RID: 6284
		public abstract bool CheckScore(float validationScore, float trainingScore, out bool isBestCandidate);

		// Token: 0x0600188D RID: 6285 RVA: 0x0008B8B8 File Offset: 0x00089AB8
		protected bool CheckBestScore(float score)
		{
			bool flag = score > this._bestScore != this._lowerIsBetter;
			if (flag)
			{
				this._bestScore = score;
			}
			return flag;
		}

		// Token: 0x04000EC9 RID: 3785
		protected readonly TArguments _args;

		// Token: 0x04000ECA RID: 3786
		protected readonly bool _lowerIsBetter;

		// Token: 0x04000ECB RID: 3787
		protected float _bestScore;

		// Token: 0x02000499 RID: 1177
		public abstract class ArgumentsBase
		{
		}
	}
}
