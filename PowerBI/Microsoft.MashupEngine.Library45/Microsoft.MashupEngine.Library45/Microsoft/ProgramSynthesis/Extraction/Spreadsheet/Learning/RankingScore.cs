using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics;
using Microsoft.ProgramSynthesis.Features;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Learning
{
	// Token: 0x02000E75 RID: 3701
	public class RankingScore : Feature<double>
	{
		// Token: 0x060064F7 RID: 25847 RVA: 0x0014743E File Offset: 0x0014563E
		public RankingScore(Grammar grammar, bool useComputedInputsForFccEquality = false)
			: base(grammar, "Score", useComputedInputsForFccEquality, false, null, Feature<double>.FeatureInfoResolution.Declared)
		{
		}

		// Token: 0x060064F8 RID: 25848 RVA: 0x0001AF59 File Offset: 0x00019159
		protected override double GetFeatureValueForVariable(VariableNode variable)
		{
			return 0.0;
		}

		// Token: 0x060064F9 RID: 25849 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("sheetPair", Method = CalculationMethod.FromProgramNode)]
		public double sheetPair(ProgramNode node)
		{
			return 0.0;
		}

		// Token: 0x060064FA RID: 25850 RVA: 0x00147450 File Offset: 0x00145650
		[FeatureCalculator("Output", Method = CalculationMethod.FromChildrenFeatureValues, SupportsLearningInfo = true)]
		public double Output(LearningInfo learningInfo, double score_Output_trim)
		{
			SpreadsheetPatterns.Score_OutputFeatures score_OutputFeatures = SpreadsheetPatterns.Score_Output(learningInfo);
			if (score_OutputFeatures.LeftColumnFinalCharIsColon || score_OutputFeatures.IsAllMergedCells || score_OutputFeatures.IsAllSingleCellRows || score_OutputFeatures.IsOnlyFrozenPanes)
			{
				return -1000.0;
			}
			if (score_OutputFeatures.Width <= 1.0 || score_OutputFeatures.Height <= 2.0)
			{
				return -1001.0;
			}
			double num = score_Output_trim + score_OutputFeatures.PatternScore;
			if (score_OutputFeatures.HasFullyMergedColumn)
			{
				num -= 14.0;
			}
			return num + (score_OutputFeatures.Height / 1000.0 + score_OutputFeatures.Width / 100.0);
		}

		// Token: 0x060064FB RID: 25851 RVA: 0x0000E945 File Offset: 0x0000CB45
		[FeatureCalculator("Trim", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double Trim(double score_Trim_area)
		{
			return score_Trim_area;
		}

		// Token: 0x060064FC RID: 25852 RVA: 0x00147506 File Offset: 0x00145706
		[FeatureCalculator("TrimHidden", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double TrimHidden(double score_TrimHidden_area)
		{
			return score_TrimHidden_area + 1.0;
		}

		// Token: 0x060064FD RID: 25853 RVA: 0x00147513 File Offset: 0x00145713
		[FeatureCalculator("TrimBottomSingleCellRows", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double TrimBottomSingleCellRows(double score_TrimBottomSingleCellRows_trimTop)
		{
			return score_TrimBottomSingleCellRows_trimTop + 3.0;
		}

		// Token: 0x060064FE RID: 25854 RVA: 0x00147506 File Offset: 0x00145706
		[FeatureCalculator("TakeUntilEmptyRow", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double TakeUntilEmptyRow(double score_TakeUntilEmptyRow_trimTop)
		{
			return score_TakeUntilEmptyRow_trimTop + 1.0;
		}

		// Token: 0x060064FF RID: 25855 RVA: 0x00147520 File Offset: 0x00145720
		[FeatureCalculator("TrimTopSingleCellRows", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double TrimTopSingleCellRows(double score_TrimTopSingleCellRows_sheetSection)
		{
			return score_TrimTopSingleCellRows_sheetSection + 5.0;
		}

		// Token: 0x06006500 RID: 25856 RVA: 0x00147513 File Offset: 0x00145713
		[FeatureCalculator("TrimTopMergedCellRows", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double TrimTopMergedCellRows(double score_TrimTopMergedCellRows_sheetSection)
		{
			return score_TrimTopMergedCellRows_sheetSection + 3.0;
		}

		// Token: 0x06006501 RID: 25857 RVA: 0x0014752D File Offset: 0x0014572D
		[FeatureCalculator("TrimTopFullWidthMergedCellRows", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double TrimTopFullWidthMergedCellRows(double score_TrimTopFullWidthMergedCellRows_sheetSection)
		{
			return score_TrimTopFullWidthMergedCellRows_sheetSection + 4.0;
		}

		// Token: 0x06006502 RID: 25858 RVA: 0x00147506 File Offset: 0x00145706
		[FeatureCalculator("TrimHiddenWholeSheet", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double TrimHiddenWholeSheet(double score_TrimHiddenWholeSheet_wholeSheetFull)
		{
			return score_TrimHiddenWholeSheet_wholeSheetFull + 1.0;
		}

		// Token: 0x06006503 RID: 25859 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("WholeSheet", Method = CalculationMethod.FromProgramNode)]
		public double WholeSheet(ProgramNode node)
		{
			return 0.0;
		}

		// Token: 0x06006504 RID: 25860 RVA: 0x0014753A File Offset: 0x0014573A
		[FeatureCalculator("FreezePaneTight", Method = CalculationMethod.FromProgramNode)]
		public double FreezePaneTight(ProgramNode node)
		{
			return 4.0;
		}

		// Token: 0x06006505 RID: 25861 RVA: 0x00147545 File Offset: 0x00145745
		[FeatureCalculator("FreezePaneToBlanks", Method = CalculationMethod.FromProgramNode)]
		public double FreezePaneToBlanks(ProgramNode node)
		{
			return 9.0;
		}

		// Token: 0x06006506 RID: 25862 RVA: 0x0001EA46 File Offset: 0x0001CC46
		[FeatureCalculator("FreezePaneToMultipleBlanks", Method = CalculationMethod.FromProgramNode)]
		public double FreezePaneToMultipleBlanks(ProgramNode node)
		{
			return 10.0;
		}

		// Token: 0x06006507 RID: 25863 RVA: 0x00147550 File Offset: 0x00145750
		[FeatureCalculator("DefinedRange", Method = CalculationMethod.FromProgramNode)]
		public double DefinedRange(ProgramNode node)
		{
			return 1000.0;
		}

		// Token: 0x06006508 RID: 25864 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("Area", Method = CalculationMethod.FromProgramNode)]
		public double Area(ProgramNode node)
		{
			return 0.0;
		}

		// Token: 0x06006509 RID: 25865 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("rangeName", Method = CalculationMethod.FromProgramNode)]
		public double rangeName(ProgramNode node)
		{
			return 0.0;
		}

		// Token: 0x0600650A RID: 25866 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("index", Method = CalculationMethod.FromProgramNode)]
		public double index(ProgramNode node)
		{
			return 0.0;
		}

		// Token: 0x0600650B RID: 25867 RVA: 0x0000E945 File Offset: 0x0000CB45
		[FeatureCalculator("WithFormatting", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double WithFormatting(double score_WithFormatting_sheet)
		{
			return score_WithFormatting_sheet;
		}

		// Token: 0x0600650C RID: 25868 RVA: 0x00147506 File Offset: 0x00145706
		[FeatureCalculator("TrimRightSingleCellColumns", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double TrimRightSingleCellColumns(double score_TrimRightSingleCellColumns_uncleanedSheetSection)
		{
			return score_TrimRightSingleCellColumns_uncleanedSheetSection + 1.0;
		}

		// Token: 0x0600650D RID: 25869 RVA: 0x00147506 File Offset: 0x00145706
		[FeatureCalculator("TrimLeftSingleCellColumns", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double TrimLeftSingleCellColumns(double score_TrimLeftSingleCellColumns_trimBottom)
		{
			return score_TrimLeftSingleCellColumns_trimBottom + 1.0;
		}

		// Token: 0x0600650E RID: 25870 RVA: 0x0014755B File Offset: 0x0014575B
		[FeatureCalculator("KthHorizontal", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double KthHorizontal(double score_KthHorizontal_sheetHorizontals, double score_KthHorizontal_k)
		{
			return score_KthHorizontal_sheetHorizontals + score_KthHorizontal_k;
		}

		// Token: 0x0600650F RID: 25871 RVA: 0x0014755B File Offset: 0x0014575B
		[FeatureCalculator("KthVertical", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double KthVertical(double score_KthVertical_sheetVerticals, double score_KthVertical_k)
		{
			return score_KthVertical_sheetVerticals + score_KthVertical_k;
		}

		// Token: 0x06006510 RID: 25872 RVA: 0x0014755B File Offset: 0x0014575B
		[FeatureCalculator("KthSplit", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double KthSplit(double score_KthSplit_sheetSplits, double score_KthSplit_k)
		{
			return score_KthSplit_sheetSplits + score_KthSplit_k;
		}

		// Token: 0x06006511 RID: 25873 RVA: 0x00147513 File Offset: 0x00145713
		[FeatureCalculator("BorderedAreas", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double BorderedAreas(double score_BorderedAreas_wholeSheet)
		{
			return score_BorderedAreas_wholeSheet + 3.0;
		}

		// Token: 0x06006512 RID: 25874 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("k", Method = CalculationMethod.FromProgramNode)]
		public double k(ProgramNode node)
		{
			return 0.0;
		}

		// Token: 0x06006513 RID: 25875 RVA: 0x0000E945 File Offset: 0x0000CB45
		[FeatureCalculator("TrimAboveBottomBorder", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double TrimAboveBottomBorder(double score_TrimAboveBottomBorder_trimTop)
		{
			return score_TrimAboveBottomBorder_trimTop;
		}

		// Token: 0x06006514 RID: 25876 RVA: 0x00147560 File Offset: 0x00145760
		[FeatureCalculator("TrimBelowTopBorder", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double TrimBelowTopBorder(double score_TrimBelowTopBorder_sheetSection)
		{
			return score_TrimBelowTopBorder_sheetSection + 2.0;
		}

		// Token: 0x06006515 RID: 25877 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("styleFilter", Method = CalculationMethod.FromProgramNode)]
		public double styleFilter(ProgramNode node)
		{
			return 0.0;
		}

		// Token: 0x06006516 RID: 25878 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("splitMode", Method = CalculationMethod.FromProgramNode)]
		public double splitMode(ProgramNode node)
		{
			return 0.0;
		}

		// Token: 0x06006517 RID: 25879 RVA: 0x0014756D File Offset: 0x0014576D
		[FeatureCalculator("SplitOnMatchingRows", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double SplitOnMatchingRows(double score_area, double score_styleFilter, double score_splitMode)
		{
			return score_area + score_styleFilter + score_splitMode;
		}

		// Token: 0x06006518 RID: 25880 RVA: 0x00147513 File Offset: 0x00145713
		[FeatureCalculator("TakeAfterEmptyRow", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double TakeAfterEmptyRow(double score_TakeAfterEmptyRow_sheetSection)
		{
			return score_TakeAfterEmptyRow_sheetSection + 3.0;
		}

		// Token: 0x06006519 RID: 25881 RVA: 0x00147560 File Offset: 0x00145760
		[FeatureCalculator("TakeUntilEmptyColumn", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double TakeUntilEmptyColumn(double score_TakeUntilEmptyColumn_uncleanedSheetSection)
		{
			return score_TakeUntilEmptyColumn_uncleanedSheetSection + 2.0;
		}

		// Token: 0x0600651A RID: 25882 RVA: 0x00147574 File Offset: 0x00145774
		[FeatureCalculator("SplitOnEmptyRows", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double SplitOnEmptyRows(double score_SplitOnEmptyRows_verticalSheetSection)
		{
			return -3.0 + score_SplitOnEmptyRows_verticalSheetSection;
		}

		// Token: 0x0600651B RID: 25883 RVA: 0x00147581 File Offset: 0x00145781
		[FeatureCalculator("SplitOnEmptyColumns", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double SplitOnEmptyColumns(double score_SplitOnEmptyColumns_uncleanedSheetSection)
		{
			return -4.0 + score_SplitOnEmptyColumns_uncleanedSheetSection;
		}

		// Token: 0x0600651C RID: 25884 RVA: 0x0000E945 File Offset: 0x0000CB45
		[FeatureCalculator("MWholeSheet", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double MWholeSheet(double score_MWholeSheet_withoutFormatting)
		{
			return score_MWholeSheet_withoutFormatting;
		}

		// Token: 0x0600651D RID: 25885 RVA: 0x0000E945 File Offset: 0x0000CB45
		[FeatureCalculator("WithoutFormatting", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double WithoutFormatting(double score_WithoutFormatting_sheet)
		{
			return score_WithoutFormatting_sheet;
		}

		// Token: 0x0600651E RID: 25886 RVA: 0x0014758E File Offset: 0x0014578E
		[FeatureCalculator("MTrimTopSingleCellRows", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double MTrimTopSingleCellRows(double score_MTrimTopSingleCellRows_mTable)
		{
			return score_MTrimTopSingleCellRows_mTable - 1.0;
		}

		// Token: 0x0600651F RID: 25887 RVA: 0x0014758E File Offset: 0x0014578E
		[FeatureCalculator("MTrimTopSingleLeftCellRows", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double MTrimTopSingleLeftCellRows(double score_MTrimTopSingleLeftCellRows_mTable)
		{
			return score_MTrimTopSingleLeftCellRows_mTable - 1.0;
		}

		// Token: 0x06006520 RID: 25888 RVA: 0x0014758E File Offset: 0x0014578E
		[FeatureCalculator("MTrimBottomSingleCellRows", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double MTrimBottomSingleCellRows(double score_MTrimBottomSingleCellRows_mTable)
		{
			return score_MTrimBottomSingleCellRows_mTable - 1.0;
		}

		// Token: 0x06006521 RID: 25889 RVA: 0x0014759B File Offset: 0x0014579B
		[FeatureCalculator("MTrimLeftSingleCellColumns", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double MTrimLeftSingleCellColumns(double score_MTrimLeftSingleCellColumns_mTable)
		{
			return score_MTrimLeftSingleCellColumns_mTable - 3.0;
		}

		// Token: 0x06006522 RID: 25890 RVA: 0x0014759B File Offset: 0x0014579B
		[FeatureCalculator("MTrimRightSingleCellColumns", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double MTrimRightSingleCellColumns(double score_MTrimRightSingleCellColumns_mTable)
		{
			return score_MTrimRightSingleCellColumns_mTable - 3.0;
		}

		// Token: 0x06006523 RID: 25891 RVA: 0x001475A8 File Offset: 0x001457A8
		[FeatureCalculator("MTrimTopDoubleCellRows", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double MTrimTopDoubleCellRows(double score_MTrimTopDoubleCellRows_mTable)
		{
			return score_MTrimTopDoubleCellRows_mTable - 2.0;
		}

		// Token: 0x06006524 RID: 25892 RVA: 0x001475A8 File Offset: 0x001457A8
		[FeatureCalculator("MTrimBottomDoubleCellRows", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double MTrimBottomDoubleCellRows(double score_MTrimBottomDoubleCellRows_mTable)
		{
			return score_MTrimBottomDoubleCellRows_mTable - 2.0;
		}

		// Token: 0x06006525 RID: 25893 RVA: 0x0014758E File Offset: 0x0014578E
		[FeatureCalculator("KthMSection", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double KthMSection(double score_KthMSection_mSection, double score_KthMSection_k)
		{
			return score_KthMSection_mSection - 1.0;
		}

		// Token: 0x06006526 RID: 25894 RVA: 0x0014759B File Offset: 0x0014579B
		[FeatureCalculator("KthAndNextMSection", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double KthAndNextMSection(double score_KthAndNextMSection_mSection, double score_KthAndNextMSection_k)
		{
			return score_KthAndNextMSection_mSection - 3.0;
		}

		// Token: 0x06006527 RID: 25895 RVA: 0x001475B5 File Offset: 0x001457B5
		[FeatureCalculator("MSplitOnEmptyRows", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double MSplitOnEmptyRows(double score_MSplitOnEmptyRows_mTable)
		{
			return score_MSplitOnEmptyRows_mTable - 8.0;
		}

		// Token: 0x06006528 RID: 25896 RVA: 0x001475C2 File Offset: 0x001457C2
		[FeatureCalculator("MSplitOnEmptyColumns", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double MSplitOnEmptyColumns(double score_MSplitOnEmptyColumns_mTable)
		{
			return score_MSplitOnEmptyColumns_mTable - 10.0;
		}

		// Token: 0x06006529 RID: 25897 RVA: 0x0014758E File Offset: 0x0014578E
		[FeatureCalculator("RemoveEmptyRows", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double RemoveEmptyRows(double score_RemoveEmptyRows_mProgram)
		{
			return score_RemoveEmptyRows_mProgram - 1.0;
		}

		// Token: 0x0600652A RID: 25898 RVA: 0x0014758E File Offset: 0x0014578E
		[FeatureCalculator("RemoveEmptyColumns", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double RemoveEmptyColumns(double score_RemoveEmptyColumns_mProgram)
		{
			return score_RemoveEmptyColumns_mProgram - 1.0;
		}

		// Token: 0x0600652B RID: 25899 RVA: 0x001475CF File Offset: 0x001457CF
		[FeatureCalculator("StartTitle", Method = CalculationMethod.FromChildrenFeatureValues, SupportsLearningInfo = true)]
		public double StartTitle(LearningInfo learningInfo, double score_StartTitle_title)
		{
			return SpreadsheetPatterns.Score_StartTitle(learningInfo, learningInfo.ProgramNode) + score_StartTitle_title;
		}

		// Token: 0x0600652C RID: 25900 RVA: 0x00012DE5 File Offset: 0x00010FE5
		[FeatureCalculator("TopLeftCell", Method = CalculationMethod.FromProgramNode)]
		public double TopLeftCell(ProgramNode node)
		{
			return 1.0;
		}

		// Token: 0x0600652D RID: 25901 RVA: 0x00147506 File Offset: 0x00145706
		[FeatureCalculator("TopSameFontCells", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double TopSameFontCells(double score_TopSameFontCells_aboveOrLeftmost)
		{
			return score_TopSameFontCells_aboveOrLeftmost + 1.0;
		}

		// Token: 0x0600652E RID: 25902 RVA: 0x0014758E File Offset: 0x0014578E
		[FeatureCalculator("BottomLeftSameFontCells", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double BottomLeftSameFontCells(double score_BottomLeftSameFontCells_headerSection)
		{
			return score_BottomLeftSameFontCells_headerSection - 1.0;
		}

		// Token: 0x0600652F RID: 25903 RVA: 0x001475DF File Offset: 0x001457DF
		[FeatureCalculator("LeftmostColumn", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double LeftmostColumn(double score_LeftmostColumn_aboveOrOutput)
		{
			return -2.0 + score_LeftmostColumn_aboveOrOutput;
		}

		// Token: 0x06006530 RID: 25904 RVA: 0x00012DE5 File Offset: 0x00010FE5
		[FeatureCalculator("LeftOf", Method = CalculationMethod.FromProgramNode)]
		public double LeftOf(ProgramNode node)
		{
			return 1.0;
		}

		// Token: 0x06006531 RID: 25905 RVA: 0x0000E945 File Offset: 0x0000CB45
		[FeatureCalculator("FirstSplit", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double FirstSplit(double score_FirstSplit_splitForTitle)
		{
			return score_FirstSplit_splitForTitle;
		}

		// Token: 0x06006532 RID: 25906 RVA: 0x0001AE68 File Offset: 0x00019068
		[FeatureCalculator("TitleSplitOnEmptyRows", Method = CalculationMethod.FromProgramNode)]
		public double TitleSplitOnEmptyRows(ProgramNode node)
		{
			return -1.0;
		}

		// Token: 0x06006533 RID: 25907 RVA: 0x001475EC File Offset: 0x001457EC
		[FeatureCalculator("TitleSplitOnMatchingRows", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double TitleSplitOnMatchingRows(double score_TitleSplitOnMatchingRows_output, double score_TitleSplitOnMatchingRows_styleFilter, double score_TitleSplitOnMatchingRows_splitMode)
		{
			return -2.0 + score_TitleSplitOnMatchingRows_styleFilter + score_TitleSplitOnMatchingRows_splitMode;
		}

		// Token: 0x06006534 RID: 25908 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("WrapOutputForTitle", Method = CalculationMethod.FromProgramNode)]
		public double WrapOutputForTitle(ProgramNode node)
		{
			return 0.0;
		}

		// Token: 0x06006535 RID: 25909 RVA: 0x0001AE68 File Offset: 0x00019068
		[FeatureCalculator("IncludeEmptyToLeft", Method = CalculationMethod.FromProgramNode)]
		public double IncludeEmptyToLeft(ProgramNode node)
		{
			return -1.0;
		}

		// Token: 0x06006536 RID: 25910 RVA: 0x001475FB File Offset: 0x001457FB
		[FeatureCalculator("TitleCellsAbove", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double TitleCellsAbove(double score_TitleCellsAbove_titleOf, double score_TitleCellsAbove_titleAboveMode)
		{
			return 2.0 + score_TitleCellsAbove_titleAboveMode;
		}

		// Token: 0x06006537 RID: 25911 RVA: 0x00147608 File Offset: 0x00145808
		[FeatureCalculator("TitleCellsAboveMatching", Method = CalculationMethod.FromChildrenFeatureValues)]
		public double TitleCellsAboveMatching(double score_TitleCellsAboveMatching_titleOf, double score_TitleCellsAboveMatching_titleAboveMode, double score_TitleCellsAboveMatching_styleFilter)
		{
			return 1.0 + score_TitleCellsAboveMatching_titleAboveMode + score_TitleCellsAboveMatching_styleFilter;
		}

		// Token: 0x06006538 RID: 25912 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("titleAboveMode", Method = CalculationMethod.FromLiteral)]
		public double titleAboveMode(TitleAboveMode mode)
		{
			return 0.0;
		}
	}
}
