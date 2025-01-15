using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Utils.Clustering
{
	// Token: 0x02000628 RID: 1576
	public class ScoreUtils<TNode> where TNode : IProgramNodeBuilder
	{
		// Token: 0x06002223 RID: 8739 RVA: 0x000612B2 File Offset: 0x0005F4B2
		public ScoreUtils(double significantScoreRatio, double insignificantlyCheapScore, double baseOffsetScore)
		{
			this.SignificantScoreRatio = significantScoreRatio;
			this.InsignificantlyCheapScore = insignificantlyCheapScore;
			this.BaseOffsetScore = baseOffsetScore;
			this._cardinalityExponent = 1.0;
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x000612DE File Offset: 0x0005F4DE
		internal bool SignificantlyWorseThan(Cluster<TNode> current, Cluster<TNode> other)
		{
			return this.Score(current) / this.Score(other) > this.SignificantScoreRatio && this.Score(current) > this.InsignificantlyCheapScore;
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x00061308 File Offset: 0x0005F508
		internal void SetCardinalityExponent(double exponent)
		{
			this._cardinalityExponent = exponent;
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x00061311 File Offset: 0x0005F511
		internal double NormalizeScore(double score)
		{
			return this.BaseOffsetScore + score;
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x0006131C File Offset: 0x0005F51C
		private double Preference(double score, uint size)
		{
			double num = this.NormalizeScore(score);
			return Math.Pow(size, this._cardinalityExponent) / num;
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x00061341 File Offset: 0x0005F541
		internal double Preference(Cluster<TNode> c)
		{
			return this.Preference(this.Score(c), c.DataCount);
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x00061356 File Offset: 0x0005F556
		internal double Score(Cluster<TNode> c)
		{
			return -c.BestProgramScore;
		}

		// Token: 0x0400105C RID: 4188
		internal readonly double SignificantScoreRatio;

		// Token: 0x0400105D RID: 4189
		internal readonly double InsignificantlyCheapScore;

		// Token: 0x0400105E RID: 4190
		internal readonly double BaseOffsetScore;

		// Token: 0x0400105F RID: 4191
		private double _cardinalityExponent;
	}
}
