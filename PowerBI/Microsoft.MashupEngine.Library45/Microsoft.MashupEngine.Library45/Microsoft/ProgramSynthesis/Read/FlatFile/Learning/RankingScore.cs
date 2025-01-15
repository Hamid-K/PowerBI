using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text;
using Microsoft.ProgramSynthesis.Extraction.Text.Learning;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Learning
{
	// Token: 0x020012CE RID: 4814
	public class RankingScore : Feature<double>
	{
		// Token: 0x06009137 RID: 37175 RVA: 0x001E9437 File Offset: 0x001E7637
		public RankingScore(Grammar grammar)
			: base(grammar, "Score", false, false, null, Feature<double>.FeatureInfoResolution.Declared)
		{
		}

		// Token: 0x06009138 RID: 37176 RVA: 0x0001AF59 File Offset: 0x00019159
		protected override double GetFeatureValueForVariable(VariableNode variable)
		{
			return 0.0;
		}

		// Token: 0x06009139 RID: 37177 RVA: 0x001E9459 File Offset: 0x001E7659
		[FeatureCalculator("Csv")]
		public static double CsvScore(double input, double columnNames, double skip, double skipFooter, double delimiter, double filterEmptyLines, double comment, double quoteChar, double escapeChar, double doubleQuote)
		{
			return input + columnNames + skip + skipFooter + delimiter + filterEmptyLines + comment + quoteChar + escapeChar + doubleQuote;
		}

		// Token: 0x0600913A RID: 37178 RVA: 0x001E9474 File Offset: 0x001E7674
		[FeatureCalculator("Fw")]
		public static double FwScore(double input, double columnNames, double skip, double skipFooter, double fieldPositions, double filterEmptyLines, double comment)
		{
			return input + columnNames + skip + skipFooter + fieldPositions + filterEmptyLines + comment;
		}

		// Token: 0x0600913B RID: 37179 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("columnNames", Method = CalculationMethod.FromLiteral)]
		public static double ColumnNamesScore(IReadOnlyList<string> _)
		{
			return 0.0;
		}

		// Token: 0x0600913C RID: 37180 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("skip", Method = CalculationMethod.FromLiteral)]
		[FeatureCalculator("skipFooter", Method = CalculationMethod.FromLiteral)]
		public static double IntScore(int _)
		{
			return 0.0;
		}

		// Token: 0x0600913D RID: 37181 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("delimiter", Method = CalculationMethod.FromLiteral)]
		public static double DelimiterScore(string _)
		{
			return 0.0;
		}

		// Token: 0x0600913E RID: 37182 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("fieldPositions", Method = CalculationMethod.FromLiteral)]
		public static double FieldPositionsScore(IReadOnlyList<Record<int, int?>> _)
		{
			return 0.0;
		}

		// Token: 0x0600913F RID: 37183 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("filterEmptyLines", Method = CalculationMethod.FromLiteral)]
		[FeatureCalculator("doubleQuote", Method = CalculationMethod.FromLiteral)]
		public static double BoolScore(bool _)
		{
			return 0.0;
		}

		// Token: 0x06009140 RID: 37184 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("commentStr", Method = CalculationMethod.FromLiteral)]
		public static double CommentScore(Optional<string> _)
		{
			return 0.0;
		}

		// Token: 0x06009141 RID: 37185 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("quoteChar", Method = CalculationMethod.FromLiteral)]
		[FeatureCalculator("escapeChar", Method = CalculationMethod.FromLiteral)]
		public static double OptionalCharScore(Optional<char> _)
		{
			return 0.0;
		}

		// Token: 0x06009142 RID: 37186 RVA: 0x00004FAE File Offset: 0x000031AE
		[FeatureCalculator("CreateStringRegion")]
		[FeatureCalculator("StringRegionToStringTable")]
		public static double PassthroughScore(double child)
		{
			return child;
		}

		// Token: 0x170018F7 RID: 6391
		// (get) Token: 0x06009143 RID: 37187 RVA: 0x001E9486 File Offset: 0x001E7686
		[ExternFeatureMapping("ETextOutput", 0)]
		public IFeature ETextScore { get; } = new RankingScore(Language.Grammar);
	}
}
