using System;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Split.Text.Learning
{
	// Token: 0x0200139F RID: 5023
	public class RankingScore : Feature<double>
	{
		// Token: 0x06009BE9 RID: 39913 RVA: 0x000BFE5F File Offset: 0x000BE05F
		public RankingScore(Grammar grammar)
			: base(grammar, "Score", false, false, null, Feature<double>.FeatureInfoResolution.Declared)
		{
		}

		// Token: 0x06009BEA RID: 39914 RVA: 0x0020EF8F File Offset: 0x0020D18F
		protected override double GetFeatureValueForVariable(VariableNode variable)
		{
			return 100.0;
		}

		// Token: 0x06009BEB RID: 39915 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("OuterLetWitness")]
		public static double OutputScore(double split, double append)
		{
			return split + append;
		}

		// Token: 0x06009BEC RID: 39916 RVA: 0x0000E945 File Offset: 0x0000CB45
		[FeatureCalculator("Split")]
		public static double Score_Split(double v, double delimiter)
		{
			return delimiter;
		}

		// Token: 0x06009BED RID: 39917 RVA: 0x00004FAE File Offset: 0x000031AE
		[FeatureCalculator("Item1")]
		public static double Score_Item1(double pair)
		{
			return pair;
		}

		// Token: 0x06009BEE RID: 39918 RVA: 0x00004FAE File Offset: 0x000031AE
		[FeatureCalculator("Item2")]
		public static double Score_Item2(double pair)
		{
			return pair;
		}

		// Token: 0x06009BEF RID: 39919 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("Append")]
		public static double Score_Append(double first, double splitSequence)
		{
			return first + splitSequence;
		}

		// Token: 0x06009BF0 RID: 39920 RVA: 0x0020EF8F File Offset: 0x0020D18F
		[FeatureCalculator("List")]
		public static double Score_List(double v)
		{
			return 100.0;
		}

		// Token: 0x06009BF1 RID: 39921 RVA: 0x0020EF9A File Offset: 0x0020D19A
		[FeatureCalculator("delimiter", Method = CalculationMethod.FromLiteral)]
		public static double DelimiterScore(Record<RegularExpression, RegularExpression, RegularExpression> delimiter)
		{
			return RankingScore.RegexScore(delimiter.Item1) + 5.0 * RankingScore.RegexScore(delimiter.Item2) + RankingScore.RegexScore(delimiter.Item3);
		}

		// Token: 0x06009BF2 RID: 39922 RVA: 0x0020EFC9 File Offset: 0x0020D1C9
		private static double RegexScore(RegularExpression regex)
		{
			return (double)regex.Tokens.Sum((Token t) => t.Score);
		}

		// Token: 0x06009BF3 RID: 39923 RVA: 0x0020EFF6 File Offset: 0x0020D1F6
		[FeatureCalculator("SplitRegion")]
		public static double Rank_SplitRegion(double k1, double k2, double k3, double k4, double k5, double k6, double k7, double k8)
		{
			return k1 + k2 + k3 + k4 + k5 + k6 + k7 + k8;
		}

		// Token: 0x06009BF4 RID: 39924 RVA: 0x000BFE8B File Offset: 0x000BE08B
		[FeatureCalculator("ConditionalExtract")]
		[FeatureCalculator("GEN_LookAround")]
		[FeatureCalculator("GEN_FieldLookAroundEndPoints")]
		[FeatureCalculator("ExtractionSplit")]
		[FeatureCalculator("LookAround")]
		[FeatureCalculator("FieldLookAroundEndPoints")]
		[FeatureCalculator("ConstantDelimiterWithQuoting")]
		public static double Rank_Triple(double k1, double k2, double k3)
		{
			return k1 + k2 + k3;
		}

		// Token: 0x06009BF5 RID: 39925 RVA: 0x0020F00B File Offset: 0x0020D20B
		[FeatureCalculator("SpecialCharPattern")]
		[FeatureCalculator("ConstantDelimiter")]
		[FeatureCalculator("FixedWidth")]
		[FeatureCalculator("FixedWidthDelimiters")]
		[FeatureCalculator("DelimitersList")]
		[FeatureCalculator("ExtPointsList")]
		[FeatureCalculator("ConstAlphStr")]
		[FeatureCalculator("SplitMultiple")]
		[FeatureCalculator("GEN_Concat")]
		[FeatureCalculator("ConstStr")]
		[FeatureCalculator("ConstStrWithWhitespace")]
		[FeatureCalculator("Concat")]
		[FeatureCalculator("RegexMatch")]
		[FeatureCalculator("FieldMatch")]
		public static double Rank_Filter(double k1, double k2)
		{
			return k2 + k1;
		}

		// Token: 0x06009BF6 RID: 39926 RVA: 0x00004FAE File Offset: 0x000031AE
		[FeatureCalculator("FieldEndPoints")]
		[FeatureCalculator("Empty")]
		public static double Rank_SubNode(double k1)
		{
			return k1;
		}

		// Token: 0x06009BF7 RID: 39927 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("EmptyExtPointsList")]
		[FeatureCalculator("EmptyDelimitersList")]
		public static double Rank_EmptyDelimitersList()
		{
			return 0.0;
		}

		// Token: 0x06009BF8 RID: 39928 RVA: 0x00164725 File Offset: 0x00162925
		[FeatureCalculator("pattern", Method = CalculationMethod.FromLiteral)]
		[FeatureCalculator("s", Method = CalculationMethod.FromLiteral)]
		[FeatureCalculator("a", Method = CalculationMethod.FromLiteral)]
		public static double SexprRank(string sexpr)
		{
			return (double)sexpr.Length;
		}

		// Token: 0x06009BF9 RID: 39929 RVA: 0x00012DE5 File Offset: 0x00010FE5
		[FeatureCalculator("ignoreIndexes", Method = CalculationMethod.FromLiteral)]
		public static double IndexesRank(int[] indexes)
		{
			return 1.0;
		}

		// Token: 0x06009BFA RID: 39930 RVA: 0x00012DE5 File Offset: 0x00010FE5
		[FeatureCalculator("fieldStartPositions", Method = CalculationMethod.FromLiteral)]
		[FeatureCalculator("delimiterPositions", Method = CalculationMethod.FromLiteral)]
		public static double FieldStartPositionsRank(int[] fieldStartPositions)
		{
			return 1.0;
		}

		// Token: 0x06009BFB RID: 39931 RVA: 0x00012DE5 File Offset: 0x00010FE5
		[FeatureCalculator("extPoint", Method = CalculationMethod.FromLiteral)]
		public static double ExtractionPointRank(Record<int, int, int, int>? extPoint)
		{
			return 1.0;
		}

		// Token: 0x06009BFC RID: 39932 RVA: 0x00012DE5 File Offset: 0x00010FE5
		[FeatureCalculator("regex", Method = CalculationMethod.FromLiteral)]
		public static double RegexRank(RegularExpression r)
		{
			return 1.0;
		}

		// Token: 0x06009BFD RID: 39933 RVA: 0x00012DE5 File Offset: 0x00010FE5
		[FeatureCalculator("numSplits", Method = CalculationMethod.FromLiteral)]
		[FeatureCalculator("fillStrategy", Method = CalculationMethod.FromLiteral)]
		public static double IntRank(int n)
		{
			return 1.0;
		}

		// Token: 0x06009BFE RID: 39934 RVA: 0x00012DE5 File Offset: 0x00010FE5
		[FeatureCalculator("obj", Method = CalculationMethod.FromLiteral)]
		public static double ObjRank(object r)
		{
			return 1.0;
		}

		// Token: 0x06009BFF RID: 39935 RVA: 0x00012DE5 File Offset: 0x00010FE5
		[FeatureCalculator("delimiterStart", Method = CalculationMethod.FromLiteral)]
		[FeatureCalculator("delimiterEnd", Method = CalculationMethod.FromLiteral)]
		[FeatureCalculator("includeDelimiters", Method = CalculationMethod.FromLiteral)]
		public static double BoolRank(bool b)
		{
			return 1.0;
		}

		// Token: 0x06009C00 RID: 39936 RVA: 0x00012DE5 File Offset: 0x00010FE5
		[FeatureCalculator("fregex", Method = CalculationMethod.FromLiteral)]
		public static double FieldRegexRank(RegularExpression r)
		{
			return 1.0;
		}

		// Token: 0x06009C01 RID: 39937 RVA: 0x00012DE5 File Offset: 0x00010FE5
		[FeatureCalculator("quotingConf", Method = CalculationMethod.FromLiteral)]
		public static double QuotingConfRank(QuotingConfiguration _)
		{
			return 1.0;
		}
	}
}
