using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Compound.Split.Learning
{
	// Token: 0x020009E3 RID: 2531
	public class RankingScore : Feature<double>
	{
		// Token: 0x06003CF8 RID: 15608 RVA: 0x000BFE5F File Offset: 0x000BE05F
		public RankingScore(Grammar grammar)
			: base(grammar, "Score", false, false, null, Feature<double>.FeatureInfoResolution.Declared)
		{
		}

		// Token: 0x06003CF9 RID: 15609 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("SplitTextProg", Method = CalculationMethod.FromProgramNode)]
		public double ScoreSplitTextProg(ProgramNode _)
		{
			return 0.0;
		}

		// Token: 0x06003CFA RID: 15610 RVA: 0x0001AF59 File Offset: 0x00019159
		protected override double GetFeatureValueForVariable(VariableNode variable)
		{
			return 0.0;
		}

		// Token: 0x06003CFB RID: 15611 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("LetFileRecordSplit")]
		public static double LetFileRecordSplitScore(double splitFile, double splitRecords)
		{
			return splitFile + splitRecords;
		}

		// Token: 0x06003CFC RID: 15612 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("TableFromCells")]
		public static double TableFromCellsScore(double simpleSplit, double hasHeader)
		{
			return simpleSplit + hasHeader;
		}

		// Token: 0x06003CFD RID: 15613 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("SelectColumns")]
		public static double SelectColumnsScore(double columnList, double splitRecords)
		{
			return columnList + splitRecords;
		}

		// Token: 0x06003CFE RID: 15614 RVA: 0x00004FAE File Offset: 0x000031AE
		[FeatureCalculator("SplitFile")]
		public static double SplitFileScore(double v)
		{
			return v;
		}

		// Token: 0x06003CFF RID: 15615 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("LetSplitFile")]
		public static double LetSplitFileScore(double splitFile, double splitLines)
		{
			return splitFile + splitLines;
		}

		// Token: 0x06003D00 RID: 15616 RVA: 0x00004FAE File Offset: 0x000031AE
		[FeatureCalculator("MergeRecordLines")]
		public static double MergeRecordLinesScore(double splitFiles)
		{
			return splitFiles;
		}

		// Token: 0x06003D01 RID: 15617 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("NoSplit")]
		public static double NoSplitScore(double records, double hasHeader)
		{
			return records + hasHeader;
		}

		// Token: 0x06003D02 RID: 15618 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("SplitToCells")]
		public static double SplitToCellsScore(double splitTextProg, double records)
		{
			return splitTextProg + records;
		}

		// Token: 0x06003D03 RID: 15619 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("hasHeader", Method = CalculationMethod.FromLiteral)]
		public static double HasHeaderScore(bool _)
		{
			return 0.0;
		}

		// Token: 0x06003D04 RID: 15620 RVA: 0x00004FAE File Offset: 0x000031AE
		[FeatureCalculator("MultiRecordSplit")]
		public static double KeyValueSplitScore(double multiRecordSplit)
		{
			return multiRecordSplit;
		}

		// Token: 0x06003D05 RID: 15621 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("key", Method = CalculationMethod.FromLiteral)]
		public static double KeyScore(string _)
		{
			return 0.0;
		}

		// Token: 0x06003D06 RID: 15622 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("sep", Method = CalculationMethod.FromLiteral)]
		public static double SepScore(string _)
		{
			return 0.0;
		}

		// Token: 0x06003D07 RID: 15623 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("newLineSep", Method = CalculationMethod.FromLiteral)]
		public static double NewLineSepScore(string _)
		{
			return 0.0;
		}

		// Token: 0x06003D08 RID: 15624 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("fwPos", Method = CalculationMethod.FromLiteral)]
		public static double FwPosScore(int _)
		{
			return 0.0;
		}

		// Token: 0x06003D09 RID: 15625 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("LetMultiRecordSplit")]
		public static double LetMultiRecordSplitScore(double primarySelector, double mapSelectorList)
		{
			return primarySelector + mapSelectorList;
		}

		// Token: 0x06003D0A RID: 15626 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("MapColumnSelector")]
		public static double MapColumnSelectorScore(double columnSelectorList, double rowRecords)
		{
			return columnSelectorList + rowRecords;
		}

		// Token: 0x06003D0B RID: 15627 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("Empty")]
		public static double EmptyScore()
		{
			return 0.0;
		}

		// Token: 0x06003D0C RID: 15628 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("SelectorList")]
		public static double SelectorListScore(double columnSelector, double columnSelectorList)
		{
			return columnSelector + columnSelectorList;
		}

		// Token: 0x06003D0D RID: 15629 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("KthLine")]
		public static double KthLineScore(double k, double rowRecords)
		{
			return k + rowRecords;
		}

		// Token: 0x06003D0E RID: 15630 RVA: 0x000BFE76 File Offset: 0x000BE076
		[FeatureCalculator("KthKeyValue")]
		public static double KthKeyValueScore(double key, double sep, double k, double rowRecords)
		{
			return key + sep + k + rowRecords;
		}

		// Token: 0x06003D0F RID: 15631 RVA: 0x000BFE76 File Offset: 0x000BE076
		[FeatureCalculator("KthTwoLineKeyValue")]
		public static double KthTwoLinesKeyValueScore(double key, double sep, double k, double rowRecords)
		{
			return key + sep + k + rowRecords;
		}

		// Token: 0x06003D10 RID: 15632 RVA: 0x000BFE76 File Offset: 0x000BE076
		[FeatureCalculator("KthKeyQuote")]
		public static double KthKeyQuoteScore(double key, double k, double newLineSep, double rowRecords)
		{
			return key + k + newLineSep + rowRecords;
		}

		// Token: 0x06003D11 RID: 15633 RVA: 0x000BFE7F File Offset: 0x000BE07F
		[FeatureCalculator("KthKeyValueFw")]
		public static double KthKeyValueFwScore(double key, double fwPos, double k, double newLineSep, double rowRecord)
		{
			return key + fwPos + k + newLineSep + rowRecord;
		}

		// Token: 0x06003D12 RID: 15634 RVA: 0x00004FAE File Offset: 0x000031AE
		[FeatureCalculator("BreakLine")]
		public static double BreakLineScore(double records)
		{
			return records;
		}

		// Token: 0x06003D13 RID: 15635 RVA: 0x000BFE8B File Offset: 0x000BE08B
		[FeatureCalculator("KeyValue")]
		public static double KeyValueScore(double key, double sep, double rowRecords)
		{
			return key + sep + rowRecords;
		}

		// Token: 0x06003D14 RID: 15636 RVA: 0x000BFE8B File Offset: 0x000BE08B
		[FeatureCalculator("TwoLineKeyValue")]
		public static double TwoLineKeyValueScore(double key, double sep, double rowRecords)
		{
			return key + sep + rowRecords;
		}

		// Token: 0x06003D15 RID: 15637 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("KeyQuote")]
		public static double KeyQuoteScore(double key, double rowRecords)
		{
			return key + rowRecords;
		}

		// Token: 0x06003D16 RID: 15638 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("SplitSequenceLet")]
		public static double OutputSplitScore(double filteredLines, double splitSequence)
		{
			return filteredLines + splitSequence;
		}

		// Token: 0x06003D17 RID: 15639 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("SplitSequence")]
		public static double Score_SplitSequence(double r, double lines)
		{
			return r + lines;
		}

		// Token: 0x06003D18 RID: 15640 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("Sequence")]
		public static double Score_Sequence(double _)
		{
			return 0.0;
		}

		// Token: 0x06003D19 RID: 15641 RVA: 0x000BFE8B File Offset: 0x000BE08B
		[FeatureCalculator("Skip")]
		public static double Score_Skip(double k, double headerIndex, double allLines)
		{
			return k + headerIndex + allLines;
		}

		// Token: 0x06003D1A RID: 15642 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("SkipFooter")]
		public static double Score_SkipWithFooter(double k, double allLines)
		{
			return k + allLines;
		}

		// Token: 0x06003D1B RID: 15643 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("SelectData")]
		public static double Score_SelectData(double basicLinePredicate, double allLines)
		{
			return basicLinePredicate + allLines;
		}

		// Token: 0x06003D1C RID: 15644 RVA: 0x000BFE71 File Offset: 0x000BE071
		[FeatureCalculator("FilterHeader")]
		public static double Score_FilterHeader(double basicLinePredicate, double allLines)
		{
			return basicLinePredicate + allLines;
		}

		// Token: 0x06003D1D RID: 15645 RVA: 0x000BFE92 File Offset: 0x000BE092
		[FeatureCalculator("StartsWith")]
		public static double Score_StartsWith(double s, double r)
		{
			return r + 300.0;
		}

		// Token: 0x06003D1E RID: 15646 RVA: 0x000BFE7F File Offset: 0x000BE07F
		[FeatureCalculator("FilterRecords")]
		public static double Score_FilterRecords(double skipEmpty, double delimiter, double commentStr, double hasCommentHeader, double records)
		{
			return skipEmpty + delimiter + commentStr + hasCommentHeader + records;
		}

		// Token: 0x06003D1F RID: 15647 RVA: 0x000BFE8B File Offset: 0x000BE08B
		[FeatureCalculator("QuoteRecords")]
		public static double Score_QuoteRecords(double quotingConfig, double conf, double lines)
		{
			return quotingConfig + conf + lines;
		}

		// Token: 0x06003D20 RID: 15648 RVA: 0x000BFE9F File Offset: 0x000BE09F
		[FeatureCalculator("r", Method = CalculationMethod.FromLiteral)]
		public static double RegexScore(RegularExpression r)
		{
			return (double)r.Score;
		}

		// Token: 0x06003D21 RID: 15649 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("k", Method = CalculationMethod.FromLiteral)]
		public static double KScore(int _)
		{
			return 0.0;
		}

		// Token: 0x06003D22 RID: 15650 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("headerIndex", Method = CalculationMethod.FromLiteral)]
		public static double HeaderIndexScore(Optional<int> _)
		{
			return 0.0;
		}

		// Token: 0x06003D23 RID: 15651 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("quotingConfig", Method = CalculationMethod.FromLiteral)]
		public static double QuotingConfigScore(QuotingConfiguration _)
		{
			return 0.0;
		}

		// Token: 0x06003D24 RID: 15652 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("delimiter", Method = CalculationMethod.FromLiteral)]
		public static double DelimiterScore(Optional<string> _)
		{
			return 0.0;
		}

		// Token: 0x06003D25 RID: 15653 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("commentStr", Method = CalculationMethod.FromLiteral)]
		public static double CommentStrScore(Optional<string> _)
		{
			return 0.0;
		}

		// Token: 0x06003D26 RID: 15654 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("skipEmpty", Method = CalculationMethod.FromLiteral)]
		public static double SkipEmptyScore(bool _)
		{
			return 0.0;
		}

		// Token: 0x06003D27 RID: 15655 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("hasCommentHeader", Method = CalculationMethod.FromLiteral)]
		public static double HasCommentHeaderScore(bool _)
		{
			return 0.0;
		}

		// Token: 0x06003D28 RID: 15656 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("columnList", Method = CalculationMethod.FromLiteral)]
		public static double ColumnListScore(int[] _)
		{
			return 0.0;
		}
	}
}
