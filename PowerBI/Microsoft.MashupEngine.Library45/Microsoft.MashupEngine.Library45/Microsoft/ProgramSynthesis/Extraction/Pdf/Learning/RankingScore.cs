using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build;
using Microsoft.ProgramSynthesis.Features;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Learning
{
	// Token: 0x02000C26 RID: 3110
	[NullableContext(1)]
	[Nullable(0)]
	public class RankingScore : Feature<double>
	{
		// Token: 0x06005051 RID: 20561 RVA: 0x000FC82B File Offset: 0x000FAA2B
		public RankingScore(Grammar grammar)
			: base(grammar, "Score", false, false, null, Feature<double>.FeatureInfoResolution.Declared)
		{
			this._build = GrammarBuilders.Instance(grammar);
		}

		// Token: 0x06005052 RID: 20562 RVA: 0x0001AF59 File Offset: 0x00019159
		protected override double GetFeatureValueForVariable(VariableNode variable)
		{
			return 0.0;
		}

		// Token: 0x06005053 RID: 20563 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("LetBetweenAxis")]
		[FeatureCalculator("LetBetweenBefore")]
		public static double Let_Score(double value, double body)
		{
			return value + body;
		}

		// Token: 0x06005054 RID: 20564 RVA: 0x00004FAE File Offset: 0x000031AE
		[FeatureCalculator("SnapToGlyphs")]
		public static double SnapToGlyphs_Score(double child)
		{
			return child;
		}

		// Token: 0x06005055 RID: 20565 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("CombineBounds")]
		public static double CombineBounds_Score(double horizontal, double vertical)
		{
			return horizontal + vertical;
		}

		// Token: 0x06005056 RID: 20566 RVA: 0x000BFE8B File Offset: 0x000BE08B
		[FeatureCalculator("Between")]
		public static double Between_Score(double axis, double before, double after)
		{
			return axis + before + after;
		}

		// Token: 0x06005057 RID: 20567 RVA: 0x000BFE8B File Offset: 0x000BE08B
		[FeatureCalculator("NextSeparator")]
		public static double NextSeparator_Score(double baseBounds, double dir, double k)
		{
			return baseBounds + dir + k;
		}

		// Token: 0x06005058 RID: 20568 RVA: 0x000FC849 File Offset: 0x000FAA49
		[FeatureCalculator("NextSeparator_beforeRelative")]
		public static double NextSeparator_beforeRelative_Score(double baseBounds, double dir, double k)
		{
			return 1.0 + RankingScore.NextSeparator_Score(baseBounds, dir, k);
		}

		// Token: 0x06005059 RID: 20569 RVA: 0x000FC85D File Offset: 0x000FAA5D
		[FeatureCalculator("NextSameWidthSeparator")]
		public static double NextSameWidthSeparator_Score(double baseBounds, double dir, double k, double tolerance)
		{
			return 1.0 + baseBounds + dir + k + tolerance;
		}

		// Token: 0x0600505A RID: 20570 RVA: 0x000FC870 File Offset: 0x000FAA70
		[FeatureCalculator("NextFontSizeDecrease")]
		public static double NextFontSizeDecrease_Score(double baseBounds, double dir)
		{
			return -1.0 + baseBounds + dir;
		}

		// Token: 0x0600505B RID: 20571 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("axis")]
		[FeatureCalculator("dir")]
		[FeatureCalculator("tolerance")]
		public static double ConstantBounds_Score()
		{
			return 0.0;
		}

		// Token: 0x0600505C RID: 20572 RVA: 0x00004FAE File Offset: 0x000031AE
		[FeatureCalculator("PageBounds")]
		public static double PageBounds_Score(double fixedBounds)
		{
			return fixedBounds;
		}

		// Token: 0x0600505D RID: 20573 RVA: 0x000FC87F File Offset: 0x000FAA7F
		[FeatureCalculator("k", Method = CalculationMethod.FromLiteral)]
		public static double k_Score(int k)
		{
			return 1.0 / (double)(k + 1);
		}

		// Token: 0x04002364 RID: 9060
		private readonly GrammarBuilders _build;
	}
}
