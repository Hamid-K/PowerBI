using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D51 RID: 7505
	internal class FormatScore : RankingFeature
	{
		// Token: 0x0600FC99 RID: 64665 RVA: 0x0035E494 File Offset: 0x0035C694
		public FormatScore(Grammar grammar, Feature<double> rankingScore)
			: base(grammar, "FormatScore", 13.4211453377542, true)
		{
			this._rankingScore = rankingScore;
		}

		// Token: 0x0600FC9A RID: 64666 RVA: 0x0035E472 File Offset: 0x0035C672
		[FeatureCalculator("RegexPosition", Method = CalculationMethod.FromChildrenNodes)]
		[FeatureCalculator("RegexPositionRelative", Method = CalculationMethod.FromChildrenNodes)]
		public static double Calculate_RegexPosition(ProgramNode x, ProgramNode regexPair, LiteralNode k)
		{
			return (int)k.Value < 0;
		}

		// Token: 0x0600FC9B RID: 64667 RVA: 0x0035E4B3 File Offset: 0x0035C6B3
		[FeatureCalculator("FormatPartialDateTime", Method = CalculationMethod.FromChildrenNodes)]
		public double Calculate_FormatPartialDateTime(ProgramNode dt, LiteralNode format)
		{
			return this._rankingScore.Calculate(format, null);
		}

		// Token: 0x0600FC9C RID: 64668 RVA: 0x0035E4B3 File Offset: 0x0035C6B3
		[FeatureCalculator("ParsePartialDateTime", Method = CalculationMethod.FromChildrenNodes)]
		public double Calculate_ParsePartialDateTime(ProgramNode ss, LiteralNode formats)
		{
			return this._rankingScore.Calculate(formats, null);
		}

		// Token: 0x0600FC9D RID: 64669 RVA: 0x0035E4B3 File Offset: 0x0035C6B3
		[FeatureCalculator("RoundNumber", Method = CalculationMethod.FromChildrenNodes)]
		public double Calculate_RoundNumber(ProgramNode number, LiteralNode spec)
		{
			return this._rankingScore.Calculate(spec, null);
		}

		// Token: 0x0600FC9E RID: 64670 RVA: 0x0035E4C2 File Offset: 0x0035C6C2
		[FeatureCalculator("BuildNumberFormat", Method = CalculationMethod.FromProgramNode)]
		public double Calculate_BuildNumberFormat(ProgramNode program)
		{
			return this._rankingScore.Calculate(program, null);
		}

		// Token: 0x04005E4E RID: 24142
		private readonly Feature<double> _rankingScore;
	}
}
