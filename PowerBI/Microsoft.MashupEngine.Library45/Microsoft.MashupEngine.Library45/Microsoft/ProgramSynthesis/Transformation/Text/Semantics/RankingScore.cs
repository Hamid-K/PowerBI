using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals;
using Microsoft.ProgramSynthesis.Conditionals.Learning;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.CustomExtraction;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Numbers;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics
{
	// Token: 0x02001CA6 RID: 7334
	public class RankingScore : Feature<double>
	{
		// Token: 0x0600F816 RID: 63510 RVA: 0x0034C450 File Offset: 0x0034A650
		public RankingScore(Grammar grammar, IRankingScoreModel model, bool useComputedInputsForFccEquality = false, int? randomSeed = null)
			: base(grammar, "Score", useComputedInputsForFccEquality, false, null, Feature<double>.FeatureInfoResolution.Declared)
		{
			this._grammar = grammar;
			this._model = model;
			this._randomSeed = randomSeed;
			this._subfeatureCalculator = new RankingSubfeatureCalculator(grammar, this._randomSeed);
		}

		// Token: 0x0600F817 RID: 63511 RVA: 0x0034C4A5 File Offset: 0x0034A6A5
		public RankingScore Clone()
		{
			return new RankingScore(this._grammar, this._model, base.UseComputedInputsForFccEquality, this._randomSeed);
		}

		// Token: 0x17002963 RID: 10595
		// (get) Token: 0x0600F818 RID: 63512 RVA: 0x0034C4C4 File Offset: 0x0034A6C4
		[ExternFeatureMapping("Predicate", 0)]
		public IFeature PredicateScore { get; } = new RankingScore(Language.Grammar);

		// Token: 0x0600F819 RID: 63513 RVA: 0x0001AF59 File Offset: 0x00019159
		protected override double GetFeatureValueForVariable(VariableNode variable)
		{
			return 0.0;
		}

		// Token: 0x0600F81A RID: 63514 RVA: 0x0034C4CC File Offset: 0x0034A6CC
		[FeatureCalculator("IfThenElse", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double IfThenElse(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			double featureValue3 = node.Children[2].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(2) : null);
			return this._model.IfThenElse(num, featureValue, featureValue2, featureValue3);
		}

		// Token: 0x0600F81B RID: 63515 RVA: 0x0034C548 File Offset: 0x0034A748
		[FeatureCalculator("Transformation", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double Transformation(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double num2 = this._subfeatureCalculator.Score_Transformation(learningInfo, node);
			return this._model.Transformation(num, featureValue, num2);
		}

		// Token: 0x0600F81C RID: 63516 RVA: 0x0034C598 File Offset: 0x0034A798
		[FeatureCalculator("Concat", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double Concat(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			ProgramNode programNode = node.Children[0];
			double featureValue = programNode.GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			ProgramNode programNode2 = node.Children[1];
			double featureValue2 = programNode2.GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			RankingSubfeatureCalculator.Score_ConcatFeatures score_ConcatFeatures = this._subfeatureCalculator.Score_Concat(learningInfo, programNode, programNode2);
			double newInputsCount = score_ConcatFeatures.NewInputsCount;
			double repeatWholeColumnsCount = score_ConcatFeatures.RepeatWholeColumnsCount;
			double bothSidesConstant = score_ConcatFeatures.BothSidesConstant;
			double concatNumbers = score_ConcatFeatures.ConcatNumbers;
			double fvalueLen = score_ConcatFeatures.FValueLen;
			double evalueLen = score_ConcatFeatures.EValueLen;
			double fvalueLast = score_ConcatFeatures.FValueLast;
			double evalueFirst = score_ConcatFeatures.EValueFirst;
			double concatNonCommonConstants = score_ConcatFeatures.ConcatNonCommonConstants;
			double fcontainsCommonDelimiters = score_ConcatFeatures.FContainsCommonDelimiters;
			double econtainsCommonDelimiters = score_ConcatFeatures.EContainsCommonDelimiters;
			double finputsCount = score_ConcatFeatures.FInputsCount;
			double einputsCount = score_ConcatFeatures.EInputsCount;
			double fwholeColumnsCount = score_ConcatFeatures.FWholeColumnsCount;
			double ewholeColumnsCount = score_ConcatFeatures.EWholeColumnsCount;
			return this._model.Concat(num, featureValue, featureValue2, newInputsCount, repeatWholeColumnsCount, bothSidesConstant, concatNumbers, fvalueLen, evalueLen, fvalueLast, evalueFirst, concatNonCommonConstants, fcontainsCommonDelimiters, econtainsCommonDelimiters, finputsCount, einputsCount, fwholeColumnsCount, ewholeColumnsCount);
		}

		// Token: 0x0600F81D RID: 63517 RVA: 0x0034C6B0 File Offset: 0x0034A8B0
		[FeatureCalculator("ConstStr", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double ConstStr(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			ProgramNode programNode = node.Children[0];
			double featureValue = programNode.GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			RankingSubfeatureCalculator.Score_ConstStrFeatures score_ConstStrFeatures = this._subfeatureCalculator.Score_ConstStr(learningInfo, (LiteralNode)programNode);
			double constantStringLength = score_ConstStrFeatures.ConstantStringLength;
			double logConstantStringLength = score_ConstStrFeatures.LogConstantStringLength;
			double isCommonDelimiter = score_ConstStrFeatures.IsCommonDelimiter;
			double exampleCount = score_ConstStrFeatures.ExampleCount;
			double allInputsCount = score_ConstStrFeatures.AllInputsCount;
			double constantInInput = score_ConstStrFeatures.ConstantInInput;
			double constantinInputPenalty = score_ConstStrFeatures.ConstantinInputPenalty;
			double conditionalTokenCounts = score_ConstStrFeatures.ConditionalTokenCounts;
			return this._model.ConstStr(num, featureValue, constantStringLength, logConstantStringLength, isCommonDelimiter, exampleCount, allInputsCount, constantInInput, constantinInputPenalty, conditionalTokenCounts);
		}

		// Token: 0x0600F81E RID: 63518 RVA: 0x0034C760 File Offset: 0x0034A960
		[FeatureCalculator("LetColumnName", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double LetColumnName(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			return this._model.LetColumnName(num, featureValue, featureValue2);
		}

		// Token: 0x0600F81F RID: 63519 RVA: 0x0034C7C0 File Offset: 0x0034A9C0
		[FeatureCalculator("LetCell", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double LetCell(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			return this._model.LetCell(num, featureValue, featureValue2);
		}

		// Token: 0x0600F820 RID: 63520 RVA: 0x0034C820 File Offset: 0x0034AA20
		[FeatureCalculator("LetX", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double LetX(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			return this._model.LetX(num, featureValue, featureValue2);
		}

		// Token: 0x0600F821 RID: 63521 RVA: 0x0034C880 File Offset: 0x0034AA80
		[FeatureCalculator("ChooseInput", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double ChooseInput(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			return this._model.ChooseInput(num, featureValue, featureValue2);
		}

		// Token: 0x0600F822 RID: 63522 RVA: 0x0034C8E0 File Offset: 0x0034AAE0
		[FeatureCalculator("IndexInputString", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double IndexInputString(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			return this._model.IndexInputString(num, featureValue, featureValue2);
		}

		// Token: 0x0600F823 RID: 63523 RVA: 0x0034C940 File Offset: 0x0034AB40
		[FeatureCalculator("LookupInput", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double LookupInput(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			return this._model.LookupInput(num, featureValue, featureValue2);
		}

		// Token: 0x0600F824 RID: 63524 RVA: 0x0034C9A0 File Offset: 0x0034ABA0
		[FeatureCalculator("LetSharedNumberFormat", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double LetSharedNumberFormat(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			return this._model.LetSharedNumberFormat(num, featureValue, featureValue2);
		}

		// Token: 0x0600F825 RID: 63525 RVA: 0x0034CA00 File Offset: 0x0034AC00
		[FeatureCalculator("LetSharedDateTimeFormat", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double LetSharedDateTimeFormat(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			return this._model.LetSharedDateTimeFormat(num, featureValue, featureValue2);
		}

		// Token: 0x0600F826 RID: 63526 RVA: 0x0034CA60 File Offset: 0x0034AC60
		[FeatureCalculator("ToLowercase", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double ToLowercase(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			return this._model.ToLowercase(num, featureValue);
		}

		// Token: 0x0600F827 RID: 63527 RVA: 0x0034CAA0 File Offset: 0x0034ACA0
		[FeatureCalculator("ToUppercase", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double ToUppercase(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			return this._model.ToUppercase(num, featureValue);
		}

		// Token: 0x0600F828 RID: 63528 RVA: 0x0034CAE0 File Offset: 0x0034ACE0
		[FeatureCalculator("ToSimpleTitleCase", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double ToSimpleTitleCase(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			return this._model.ToSimpleTitleCase(num, featureValue);
		}

		// Token: 0x0600F829 RID: 63529 RVA: 0x0034CB20 File Offset: 0x0034AD20
		[FeatureCalculator("FormatPartialDateTime", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double FormatPartialDateTime(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			ProgramNode programNode = node.Children[0];
			double featureValue = programNode.GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			ProgramNode programNode2 = node.Children[1];
			double featureValue2 = programNode2.GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			RankingSubfeatureCalculator.Score_FormatPartialDateTimeFeatures score_FormatPartialDateTimeFeatures = this._subfeatureCalculator.Score_FormatPartialDateTime(programNode, programNode2);
			double sameDateFormat = score_FormatPartialDateTimeFeatures.SameDateFormat;
			double sameNumberPenalty = score_FormatPartialDateTimeFeatures.SameNumberPenalty;
			double extractionMatches = score_FormatPartialDateTimeFeatures.ExtractionMatches;
			return this._model.FormatPartialDateTime(num, featureValue, featureValue2, sameDateFormat, sameNumberPenalty, extractionMatches);
		}

		// Token: 0x0600F82A RID: 63530 RVA: 0x0034CBB4 File Offset: 0x0034ADB4
		[FeatureCalculator("FormatNumber", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double FormatNumber(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			return this._model.FormatNumber(num, featureValue, featureValue2);
		}

		// Token: 0x0600F82B RID: 63531 RVA: 0x0034CC14 File Offset: 0x0034AE14
		[FeatureCalculator("Lookup", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double Lookup(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			return this._model.Lookup(num, featureValue, featureValue2);
		}

		// Token: 0x0600F82C RID: 63532 RVA: 0x0034CC74 File Offset: 0x0034AE74
		[FeatureCalculator("FormatNumericRange", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double FormatNumericRange(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			ProgramNode programNode = node.Children[0];
			double featureValue = programNode.GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			ProgramNode programNode2 = node.Children[1];
			double featureValue2 = programNode2.GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			ProgramNode programNode3 = node.Children[2];
			double featureValue3 = programNode3.GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(2) : null);
			ProgramNode programNode4 = node.Children[3];
			double featureValue4 = programNode4.GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(3) : null);
			ProgramNode programNode5 = node.Children[4];
			double featureValue5 = programNode5.GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(4) : null);
			RankingSubfeatureCalculator.Score_FormatNumericRangeFeatures score_FormatNumericRangeFeatures = this._subfeatureCalculator.Score_FormatNumericRange(programNode, programNode2, programNode3, programNode4, programNode5);
			double roundToMultipleOf = score_FormatNumericRangeFeatures.RoundToMultipleOf5;
			double roundToMultipleOf5Value = score_FormatNumericRangeFeatures.RoundToMultipleOf5Value;
			return this._model.FormatNumericRange(num, featureValue, featureValue2, featureValue3, featureValue4, featureValue5, roundToMultipleOf, roundToMultipleOf5Value);
		}

		// Token: 0x0600F82D RID: 63533 RVA: 0x0034CD6C File Offset: 0x0034AF6C
		[FeatureCalculator("FormatDateTimeRange", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double FormatDateTimeRange(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			ProgramNode programNode = node.Children[0];
			double featureValue = programNode.GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			ProgramNode programNode2 = node.Children[1];
			double featureValue2 = programNode2.GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			ProgramNode programNode3 = node.Children[2];
			double featureValue3 = programNode3.GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(2) : null);
			ProgramNode programNode4 = node.Children[3];
			double featureValue4 = programNode4.GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(3) : null);
			ProgramNode programNode5 = node.Children[4];
			double featureValue5 = programNode5.GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(4) : null);
			double num2 = this._subfeatureCalculator.Score_FormatDateTimeRange_SeparatorContainsDigit(programNode, programNode2, programNode3, programNode4, programNode5);
			double num3 = this._subfeatureCalculator.Score_FormatDateTimeRange_SeparatorIsOnlySymbols(programNode, programNode2, programNode3, programNode4, programNode5);
			double num4 = this._subfeatureCalculator.Score_FormatDateTimeRange_SeparatorIsOnlyWhitespace(programNode, programNode2, programNode3, programNode4, programNode5);
			double num5 = this._subfeatureCalculator.Score_FormatDateTimeRange_SeparatorIsWrappedByWhitespace(programNode, programNode2, programNode3, programNode4, programNode5);
			double num6 = this._subfeatureCalculator.Score_FormatDateTimeRange_SeparatorIsCommonDateTimeSeparator(programNode, programNode2, programNode3, programNode4, programNode5);
			return this._model.FormatDateTimeRange(num, featureValue, featureValue2, featureValue3, featureValue4, featureValue5, num2, num3, num4, num5, num6);
		}

		// Token: 0x0600F82E RID: 63534 RVA: 0x0034CEAC File Offset: 0x0034B0AC
		[FeatureCalculator("LetSharedParsedNumber", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double LetSharedParsedNumber(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			return this._model.LetSharedParsedNumber(num, featureValue, featureValue2);
		}

		// Token: 0x0600F82F RID: 63535 RVA: 0x0034CF0C File Offset: 0x0034B10C
		[FeatureCalculator("LetSharedParsedDateTime", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double LetSharedParsedDateTime(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			return this._model.LetSharedParsedDateTime(num, featureValue, featureValue2);
		}

		// Token: 0x0600F830 RID: 63536 RVA: 0x0034CF6C File Offset: 0x0034B16C
		[FeatureCalculator("RangeConcat", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double RangeConcat(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			return this._model.RangeConcat(num, featureValue, featureValue2);
		}

		// Token: 0x0600F831 RID: 63537 RVA: 0x0034CFCC File Offset: 0x0034B1CC
		[FeatureCalculator("RangeConstStr", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double RangeConstStr(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			ProgramNode programNode = node.Children[0];
			double featureValue = programNode.GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			RankingSubfeatureCalculator.Score_ConstStrFeatures score_ConstStrFeatures = this._subfeatureCalculator.Score_ConstStr(learningInfo, (LiteralNode)programNode);
			double constantStringLength = score_ConstStrFeatures.ConstantStringLength;
			double logConstantStringLength = score_ConstStrFeatures.LogConstantStringLength;
			double isCommonDelimiter = score_ConstStrFeatures.IsCommonDelimiter;
			double exampleCount = score_ConstStrFeatures.ExampleCount;
			double allInputsCount = score_ConstStrFeatures.AllInputsCount;
			double constantInInput = score_ConstStrFeatures.ConstantInInput;
			double constantinInputPenalty = score_ConstStrFeatures.ConstantinInputPenalty;
			double conditionalTokenCounts = score_ConstStrFeatures.ConditionalTokenCounts;
			return this._model.RangeConstStr(num, featureValue, constantStringLength, logConstantStringLength, isCommonDelimiter, exampleCount, allInputsCount, constantInInput, constantinInputPenalty, conditionalTokenCounts);
		}

		// Token: 0x0600F832 RID: 63538 RVA: 0x0034D07C File Offset: 0x0034B27C
		[FeatureCalculator("RangeFormatNumber", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double RangeFormatNumber(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			return this._model.RangeFormatNumber(num, featureValue, featureValue2);
		}

		// Token: 0x0600F833 RID: 63539 RVA: 0x0034D0DC File Offset: 0x0034B2DC
		[FeatureCalculator("RangeRoundNumber", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double RangeRoundNumber(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			return this._model.RangeRoundNumber(num, featureValue, featureValue2);
		}

		// Token: 0x0600F834 RID: 63540 RVA: 0x0034D13C File Offset: 0x0034B33C
		[FeatureCalculator("DtRangeConcat", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double DtRangeConcat(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			return this._model.DtRangeConcat(num, featureValue, featureValue2);
		}

		// Token: 0x0600F835 RID: 63541 RVA: 0x0034D19C File Offset: 0x0034B39C
		[FeatureCalculator("DtRangeConstStr", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double DtRangeConstStr(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			ProgramNode programNode = node.Children[0];
			double featureValue = programNode.GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			RankingSubfeatureCalculator.Score_ConstStrFeatures score_ConstStrFeatures = this._subfeatureCalculator.Score_ConstStr(learningInfo, (LiteralNode)programNode);
			double constantStringLength = score_ConstStrFeatures.ConstantStringLength;
			double logConstantStringLength = score_ConstStrFeatures.LogConstantStringLength;
			double isCommonDelimiter = score_ConstStrFeatures.IsCommonDelimiter;
			double exampleCount = score_ConstStrFeatures.ExampleCount;
			double allInputsCount = score_ConstStrFeatures.AllInputsCount;
			double constantInInput = score_ConstStrFeatures.ConstantInInput;
			double constantinInputPenalty = score_ConstStrFeatures.ConstantinInputPenalty;
			double conditionalTokenCounts = score_ConstStrFeatures.ConditionalTokenCounts;
			return this._model.DtRangeConstStr(num, featureValue, constantStringLength, logConstantStringLength, isCommonDelimiter, exampleCount, allInputsCount, constantInInput, constantinInputPenalty, conditionalTokenCounts);
		}

		// Token: 0x0600F836 RID: 63542 RVA: 0x0034D24C File Offset: 0x0034B44C
		[FeatureCalculator("RangeFormatDateTime", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double RangeFormatDateTime(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			RankingSubfeatureCalculator.Score_FormatPartialDateTimeFeatures score_FormatPartialDateTimeFeatures = this._subfeatureCalculator.Score_FormatPartialDateTime(node);
			double sameDateFormat = score_FormatPartialDateTimeFeatures.SameDateFormat;
			double sameNumberPenalty = score_FormatPartialDateTimeFeatures.SameNumberPenalty;
			double extractionMatches = score_FormatPartialDateTimeFeatures.ExtractionMatches;
			return this._model.RangeFormatDateTime(num, featureValue, featureValue2, sameDateFormat, sameNumberPenalty, extractionMatches);
		}

		// Token: 0x0600F837 RID: 63543 RVA: 0x0034D2D8 File Offset: 0x0034B4D8
		[FeatureCalculator("RangeRoundDateTime", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double RangeRoundDateTime(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			return this._model.RangeRoundDateTime(num, featureValue, featureValue2);
		}

		// Token: 0x0600F838 RID: 63544 RVA: 0x0034D338 File Offset: 0x0034B538
		[FeatureCalculator("RoundPartialDateTime", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double RoundPartialDateTime(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			return this._model.RoundPartialDateTime(num, featureValue, featureValue2);
		}

		// Token: 0x0600F839 RID: 63545 RVA: 0x0034D398 File Offset: 0x0034B598
		[FeatureCalculator("AsPartialDateTime", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double AsPartialDateTime(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double num2 = this._subfeatureCalculator.CastScore(learningInfo, node);
			return this._model.AsPartialDateTime(num, featureValue, num2);
		}

		// Token: 0x0600F83A RID: 63546 RVA: 0x0034D3E8 File Offset: 0x0034B5E8
		[FeatureCalculator("ParsePartialDateTime", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double ParsePartialDateTime(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			return this._model.ParsePartialDateTime(num, featureValue, featureValue2);
		}

		// Token: 0x0600F83B RID: 63547 RVA: 0x0034D448 File Offset: 0x0034B648
		[FeatureCalculator("WholeColumn", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double WholeColumn(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double num2 = this._subfeatureCalculator.WholeColumnScore(node);
			return this._model.WholeColumn(num, featureValue, num2);
		}

		// Token: 0x0600F83C RID: 63548 RVA: 0x0034D498 File Offset: 0x0034B698
		[FeatureCalculator("SubStr", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double SubStr(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			double num2 = RankingSubfeatureCalculator.Score_SubStr(featureValue, featureValue2);
			return this._model.SubStr(num, featureValue, featureValue2, num2);
		}

		// Token: 0x0600F83D RID: 63549 RVA: 0x0034D500 File Offset: 0x0034B700
		[FeatureCalculator("Add", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double Add(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			double num2 = RankingSubfeatureCalculator.Score_Add(featureValue, featureValue2);
			return this._model.Add(num, featureValue, featureValue2, num2);
		}

		// Token: 0x0600F83E RID: 63550 RVA: 0x0034D568 File Offset: 0x0034B768
		[FeatureCalculator("PosPairRelative", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double PosPairRelative(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			return this._model.PosPairRelative(num, featureValue, featureValue2);
		}

		// Token: 0x0600F83F RID: 63551 RVA: 0x0034D5C8 File Offset: 0x0034B7C8
		[FeatureCalculator("RSubStr", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double RSubStr(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			return this._model.RSubStr(num, featureValue, featureValue2);
		}

		// Token: 0x0600F840 RID: 63552 RVA: 0x0034D628 File Offset: 0x0034B828
		[FeatureCalculator("LetPL2", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double LetPL2(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			return this._model.LetPL2(num, featureValue, featureValue2);
		}

		// Token: 0x0600F841 RID: 63553 RVA: 0x0034D688 File Offset: 0x0034B888
		[FeatureCalculator("PosPair", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double PosPair(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			ProgramNode programNode = node.Children[0];
			double featureValue = programNode.GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			ProgramNode programNode2 = node.Children[1];
			double featureValue2 = programNode2.GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			RankingSubfeatureCalculator.Score_PosPairFeatures score_PosPairFeatures = this._subfeatureCalculator.Score_PosPair(programNode, programNode2);
			double constantRegexExtractionPenaltyFactorBias = score_PosPairFeatures.ConstantRegexExtractionPenaltyFactorBias;
			double regexExtractionBonusBias = score_PosPairFeatures.RegexExtractionBonusBias;
			double num2 = this._subfeatureCalculator.Score_PosPair(featureValue, featureValue2);
			return this._model.PosPair(num, featureValue, featureValue2, constantRegexExtractionPenaltyFactorBias, regexExtractionBonusBias, num2);
		}

		// Token: 0x0600F842 RID: 63554 RVA: 0x0034D724 File Offset: 0x0034B924
		[FeatureCalculator("LetPL1", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double LetPL1(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			double num2 = RankingSubfeatureCalculator.RelScore(featureValue, featureValue2);
			return this._model.LetPL1(num, featureValue, featureValue2, num2);
		}

		// Token: 0x0600F843 RID: 63555 RVA: 0x0034D78C File Offset: 0x0034B98C
		[FeatureCalculator("RegexPositionPair", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double RegexPositionPair(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			ProgramNode programNode = node.Children[0];
			double featureValue = programNode.GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			ProgramNode programNode2 = node.Children[1];
			double featureValue2 = programNode2.GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			ProgramNode programNode3 = node.Children[2];
			double featureValue3 = programNode3.GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(2) : null);
			RankingSubfeatureCalculator.Score_PosPairRelativeFeatures score_PosPairRelativeFeatures = this._subfeatureCalculator.Score_RegexPositionPair(learningInfo, (VariableNode)programNode, (LiteralNode)programNode2, (LiteralNode)programNode3);
			double regexIsConstant = score_PosPairRelativeFeatures.RegexIsConstant;
			double notMatchedFactor = score_PosPairRelativeFeatures.NotMatchedFactor;
			double proportionNull = score_PosPairRelativeFeatures.ProportionNull;
			RankingSubfeatureCalculator.DerivedScores_PosPairRelativeFeatures derivedScores_PosPairRelativeFeatures = this._subfeatureCalculator.Score_RegexPositionPair(featureValue, featureValue2, featureValue3);
			double posPairRelativeFeaturesRrKk = derivedScores_PosPairRelativeFeatures.PosPairRelativeFeaturesRrKk;
			double posPairRelativeFeaturesRKk = derivedScores_PosPairRelativeFeatures.PosPairRelativeFeaturesRKk;
			double posPairRelativeFeaturesKk = derivedScores_PosPairRelativeFeatures.PosPairRelativeFeaturesKk;
			double posPairRelativeFeaturesRScore = derivedScores_PosPairRelativeFeatures.PosPairRelativeFeaturesRScore;
			return this._model.RegexPositionPair(num, featureValue, featureValue2, featureValue3, regexIsConstant, notMatchedFactor, proportionNull, posPairRelativeFeaturesRrKk, posPairRelativeFeaturesRKk, posPairRelativeFeaturesKk, posPairRelativeFeaturesRScore);
		}

		// Token: 0x0600F844 RID: 63556 RVA: 0x0034D894 File Offset: 0x0034BA94
		[FeatureCalculator("ExternalExtractorPositionPair", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double ExternalExtractorPositionPair(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			double featureValue3 = node.Children[2].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(2) : null);
			double num2 = RankingSubfeatureCalculator.Score_ExternalExtrationPositionPair(featureValue, featureValue2, featureValue3);
			return this._model.ExternalExtractorPositionPair(num, featureValue, featureValue2, featureValue3, num2);
		}

		// Token: 0x0600F845 RID: 63557 RVA: 0x0034D91C File Offset: 0x0034BB1C
		[FeatureCalculator("RelativePosition", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double RelativePosition(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			RankingSubfeatureCalculator.Score_AbsolutePositionFeatures score_AbsolutePositionFeatures = this._subfeatureCalculator.Score_AbsolutePosition(learningInfo, node);
			double isLearningInfoNull = score_AbsolutePositionFeatures.IsLearningInfoNull;
			double allSameLength = score_AbsolutePositionFeatures.AllSameLength;
			return this._model.RelativePosition(num, featureValue, featureValue2, isLearningInfoNull, allSameLength);
		}

		// Token: 0x0600F846 RID: 63558 RVA: 0x0034D9A0 File Offset: 0x0034BBA0
		[FeatureCalculator("RegexPositionRelative", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double RegexPositionRelative(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			double featureValue3 = node.Children[2].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(2) : null);
			RankingSubfeatureCalculator.Score_RegexPositionFeatures score_RegexPositionFeatures = this._subfeatureCalculator.Score_RegexPosition(learningInfo, node);
			double useProportionNotMatched = score_RegexPositionFeatures.UseProportionNotMatched;
			double notMatchedFactor = score_RegexPositionFeatures.NotMatchedFactor;
			double num2 = this._subfeatureCalculator.Score_RegexPosition(featureValue, featureValue2, featureValue3);
			return this._model.RegexPositionRelative(num, featureValue, featureValue2, featureValue3, useProportionNotMatched, notMatchedFactor, num2);
		}

		// Token: 0x0600F847 RID: 63559 RVA: 0x0034DA54 File Offset: 0x0034BC54
		[FeatureCalculator("AbsolutePosition", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double AbsolutePosition(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			RankingSubfeatureCalculator.Score_AbsolutePositionFeatures score_AbsolutePositionFeatures = this._subfeatureCalculator.Score_AbsolutePosition(learningInfo, node);
			double isLearningInfoNull = score_AbsolutePositionFeatures.IsLearningInfoNull;
			double allSameLength = score_AbsolutePositionFeatures.AllSameLength;
			return this._model.AbsolutePosition(num, featureValue, featureValue2, isLearningInfoNull, allSameLength);
		}

		// Token: 0x0600F848 RID: 63560 RVA: 0x0034DAD8 File Offset: 0x0034BCD8
		[FeatureCalculator("RegexPosition", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double RegexPosition(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			double featureValue3 = node.Children[2].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(2) : null);
			RankingSubfeatureCalculator.Score_RegexPositionFeatures score_RegexPositionFeatures = this._subfeatureCalculator.Score_RegexPosition(learningInfo, node);
			double useProportionNotMatched = score_RegexPositionFeatures.UseProportionNotMatched;
			double notMatchedFactor = score_RegexPositionFeatures.NotMatchedFactor;
			double num2 = this._subfeatureCalculator.Score_RegexPosition(featureValue, featureValue2, featureValue3);
			return this._model.RegexPosition(num, featureValue, featureValue2, featureValue3, useProportionNotMatched, notMatchedFactor, num2);
		}

		// Token: 0x0600F849 RID: 63561 RVA: 0x0034DB8C File Offset: 0x0034BD8C
		[FeatureCalculator("RegexPair", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double RegexPair(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			return this._model.RegexPair(num, featureValue, featureValue2);
		}

		// Token: 0x0600F84A RID: 63562 RVA: 0x0034DBEC File Offset: 0x0034BDEC
		[FeatureCalculator("RoundNumber", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double RoundNumber(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			return this._model.RoundNumber(num, featureValue, featureValue2);
		}

		// Token: 0x0600F84B RID: 63563 RVA: 0x0034DC4C File Offset: 0x0034BE4C
		[FeatureCalculator("AsDecimal", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double AsDecimal(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double num2 = this._subfeatureCalculator.CastScore(learningInfo, node);
			return this._model.AsDecimal(num, featureValue, num2);
		}

		// Token: 0x0600F84C RID: 63564 RVA: 0x0034DC9C File Offset: 0x0034BE9C
		[FeatureCalculator("ParseNumber", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double ParseNumber(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			return this._model.ParseNumber(num, featureValue, featureValue2);
		}

		// Token: 0x0600F84D RID: 63565 RVA: 0x0034DCFC File Offset: 0x0034BEFC
		[FeatureCalculator("LetPredicate", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double LetPredicate(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			return this._model.LetPredicate(num, featureValue, featureValue2);
		}

		// Token: 0x0600F84E RID: 63566 RVA: 0x0034DD5C File Offset: 0x0034BF5C
		[FeatureCalculator("SelectInput", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double SelectInput(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			return this._model.SelectInput(num, featureValue, featureValue2);
		}

		// Token: 0x0600F84F RID: 63567 RVA: 0x0034DDBC File Offset: 0x0034BFBC
		[FeatureCalculator("k", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double k(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			LiteralNode literalNode = node as LiteralNode;
			RankingSubfeatureCalculator.KScoreFeatures kscoreFeatures = RankingSubfeatureCalculator.KScore((int)((literalNode != null) ? literalNode.Value : null));
			double kpositive = kscoreFeatures.KPositive;
			double kscore = kscoreFeatures.KScore;
			return this._model.k(num, kpositive, kscore);
		}

		// Token: 0x0600F850 RID: 63568 RVA: 0x0034DE10 File Offset: 0x0034C010
		[FeatureCalculator("externalExtractor", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double externalExtractor(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			RankingSubfeatureCalculator subfeatureCalculator = this._subfeatureCalculator;
			LiteralNode literalNode = node as LiteralNode;
			double num2 = subfeatureCalculator.ExternalExtractor_ExtractorScore((CustomExtractor)((literalNode != null) ? literalNode.Value : null));
			return this._model.externalExtractor(num, num2);
		}

		// Token: 0x0600F851 RID: 63569 RVA: 0x0034DE58 File Offset: 0x0034C058
		[FeatureCalculator("r", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double r(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			LiteralNode literalNode = node as LiteralNode;
			RankingSubfeatureCalculator.RegexScoreFeatures regexScoreFeatures = RankingSubfeatureCalculator.RegexScore((RegularExpression)((literalNode != null) ? literalNode.Value : null));
			double regexScore = regexScoreFeatures.RegexScore;
			double tokenCount = regexScoreFeatures.TokenCount;
			double tokenScoreSum = regexScoreFeatures.TokenScoreSum;
			double token0Score = regexScoreFeatures.Token0Score;
			double token1Score = regexScoreFeatures.Token1Score;
			double token2Score = regexScoreFeatures.Token2Score;
			return this._model.r(num, regexScore, tokenCount, tokenScoreSum, token0Score, token1Score, token2Score);
		}

		// Token: 0x0600F852 RID: 63570 RVA: 0x0034DED8 File Offset: 0x0034C0D8
		[FeatureCalculator("s", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double s(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			LiteralNode literalNode = node as LiteralNode;
			double num2 = RankingSubfeatureCalculator.SScore((string)((literalNode != null) ? literalNode.Value : null));
			return this._model.s(num, num2);
		}

		// Token: 0x0600F853 RID: 63571 RVA: 0x0034DF1C File Offset: 0x0034C11C
		[FeatureCalculator("name", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double name(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			LiteralNode literalNode = node as LiteralNode;
			double num2 = RankingSubfeatureCalculator.SScore((string)((literalNode != null) ? literalNode.Value : null));
			return this._model.name(num, num2);
		}

		// Token: 0x0600F854 RID: 63572 RVA: 0x0034DF60 File Offset: 0x0034C160
		[FeatureCalculator("roundingSpec", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double roundingSpec(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			LiteralNode literalNode = node as LiteralNode;
			RankingSubfeatureCalculator.RoundingSpecScoreFeatures roundingSpecScoreFeatures = RankingSubfeatureCalculator.RoundingSpecScore((RoundingSpec)((literalNode != null) ? literalNode.Value : null));
			double delta = roundingSpecScoreFeatures.Delta;
			double logDelta = roundingSpecScoreFeatures.LogDelta;
			double deltaIsPowerOf = roundingSpecScoreFeatures.DeltaIsPowerOf10;
			double zero = roundingSpecScoreFeatures.Zero;
			double zeroIsZero = roundingSpecScoreFeatures.ZeroIsZero;
			double roundingMode = roundingSpecScoreFeatures.RoundingMode;
			double roundingModeIsNearest = roundingSpecScoreFeatures.RoundingModeIsNearest;
			double roundingModeIsTowardZero = roundingSpecScoreFeatures.RoundingModeIsTowardZero;
			double roundingModeIsAwayFromZero = roundingSpecScoreFeatures.RoundingModeIsAwayFromZero;
			return this._model.roundingSpec(num, delta, logDelta, deltaIsPowerOf, zero, zeroIsZero, roundingMode, roundingModeIsNearest, roundingModeIsTowardZero, roundingModeIsAwayFromZero);
		}

		// Token: 0x0600F855 RID: 63573 RVA: 0x0034E000 File Offset: 0x0034C200
		[FeatureCalculator("dtRoundingSpec", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double dtRoundingSpec(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			LiteralNode literalNode = node as LiteralNode;
			RankingSubfeatureCalculator.DateTimeRoundingSpecScoreFeatures dateTimeRoundingSpecScoreFeatures = RankingSubfeatureCalculator.DateTimeRoundingSpecScore((DateTimeRoundingSpec)((literalNode != null) ? literalNode.Value : null));
			double unitScore = dateTimeRoundingSpecScoreFeatures.UnitScore;
			double roundingSpecUnit = dateTimeRoundingSpecScoreFeatures.RoundingSpecUnit;
			double isCloseFactor = dateTimeRoundingSpecScoreFeatures.IsCloseFactor;
			double upperExcludePart = dateTimeRoundingSpecScoreFeatures.UpperExcludePart;
			double displayDeltaRatio = dateTimeRoundingSpecScoreFeatures.DisplayDeltaRatio;
			double reducedDenominatorInverse = dateTimeRoundingSpecScoreFeatures.ReducedDenominatorInverse;
			double isRoundingUp = dateTimeRoundingSpecScoreFeatures.IsRoundingUp;
			return this._model.dtRoundingSpec(num, unitScore, roundingSpecUnit, isCloseFactor, upperExcludePart, displayDeltaRatio, reducedDenominatorInverse, isRoundingUp);
		}

		// Token: 0x0600F856 RID: 63574 RVA: 0x0034E08C File Offset: 0x0034C28C
		[FeatureCalculator("minTrailingZeros", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double minTrailingZeros(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			return this._model.minTrailingZeros(num);
		}

		// Token: 0x0600F857 RID: 63575 RVA: 0x0034E0B0 File Offset: 0x0034C2B0
		[FeatureCalculator("maxTrailingZeros", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double maxTrailingZeros(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			return this._model.maxTrailingZeros(num);
		}

		// Token: 0x0600F858 RID: 63576 RVA: 0x0034E0D4 File Offset: 0x0034C2D4
		[FeatureCalculator("minTrailingZerosAndWhitespace", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double minTrailingZerosAndWhitespace(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			return this._model.minTrailingZerosAndWhitespace(num);
		}

		// Token: 0x0600F859 RID: 63577 RVA: 0x0034E0F8 File Offset: 0x0034C2F8
		[FeatureCalculator("minLeadingZeros", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double minLeadingZeros(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			return this._model.minLeadingZeros(num);
		}

		// Token: 0x0600F85A RID: 63578 RVA: 0x0034E11C File Offset: 0x0034C31C
		[FeatureCalculator("minLeadingZerosAndWhitespace", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double minLeadingZerosAndWhitespace(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			return this._model.minLeadingZerosAndWhitespace(num);
		}

		// Token: 0x0600F85B RID: 63579 RVA: 0x0034E140 File Offset: 0x0034C340
		[FeatureCalculator("numberFormatSeparatorChar", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double numberFormatSeparatorChar(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			return this._model.numberFormatSeparatorChar(num);
		}

		// Token: 0x0600F85C RID: 63580 RVA: 0x0034E164 File Offset: 0x0034C364
		[FeatureCalculator("numberFormatDetails", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double numberFormatDetails(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			return this._model.numberFormatDetails(num);
		}

		// Token: 0x0600F85D RID: 63581 RVA: 0x0034E188 File Offset: 0x0034C388
		[FeatureCalculator("BuildNumberFormat", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double BuildNumberFormat(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			double featureValue = node.Children[0].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			double featureValue2 = node.Children[1].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(1) : null);
			double featureValue3 = node.Children[2].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(2) : null);
			double featureValue4 = node.Children[3].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(3) : null);
			double featureValue5 = node.Children[4].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(4) : null);
			double featureValue6 = node.Children[5].GetFeatureValue<double>(this, (learningInfo != null) ? learningInfo.ForChild(5) : null);
			RankingSubfeatureCalculator.Score_BuildNumberFormatFeatures score_BuildNumberFormatFeatures = this._subfeatureCalculator.Score_BuildNumberFormat(node);
			double minLeadingZerosHasValue = score_BuildNumberFormatFeatures.MinLeadingZerosHasValue;
			double minLeadingZeros = score_BuildNumberFormatFeatures.MinLeadingZeros;
			double minTrailingZerosHasValue = score_BuildNumberFormatFeatures.MinTrailingZerosHasValue;
			double minTrailingZeros = score_BuildNumberFormatFeatures.MinTrailingZeros;
			double maxTrailingZerosHasValue = score_BuildNumberFormatFeatures.MaxTrailingZerosHasValue;
			double maxTrailingZeros = score_BuildNumberFormatFeatures.MaxTrailingZeros;
			double minLeadingZerosAndWhiteSpaceHasValue = score_BuildNumberFormatFeatures.MinLeadingZerosAndWhiteSpaceHasValue;
			double minLeadingZerosAndWhitespace = score_BuildNumberFormatFeatures.MinLeadingZerosAndWhitespace;
			double minTrailingZerosAndWhiteSpaceHasValue = score_BuildNumberFormatFeatures.MinTrailingZerosAndWhiteSpaceHasValue;
			double minTrailingZerosAndWhitespace = score_BuildNumberFormatFeatures.MinTrailingZerosAndWhitespace;
			double minLeadingZeros_greaterThan_MinLeadingZerosAndWhitespace = score_BuildNumberFormatFeatures.MinLeadingZeros_greaterThan_MinLeadingZerosAndWhitespace;
			double minTrailingZeros_greaterThan_MinTrailingZerosAndWhitespace = score_BuildNumberFormatFeatures.MinTrailingZeros_greaterThan_MinTrailingZerosAndWhitespace;
			double maxTrailingZeros_lessThan_MinTrailingZerosAndWhitespace = score_BuildNumberFormatFeatures.MaxTrailingZeros_lessThan_MinTrailingZerosAndWhitespace;
			double minTrailingZeros_eq_MaxTrailingZeros = score_BuildNumberFormatFeatures.MinTrailingZeros_eq_MaxTrailingZeros;
			double minTrailingZeros_lessThan_minTrailingZerosAndWhitespace_and_maxTrailingZerosHasValue = score_BuildNumberFormatFeatures.MinTrailingZeros_lessThan_minTrailingZerosAndWhitespace_and_maxTrailingZerosHasValue;
			double minTrailingZeros_lessThan_minTrailingZerosAndWhitespace_and_maxTrailingZerosNoValue = score_BuildNumberFormatFeatures.MinTrailingZeros_lessThan_minTrailingZerosAndWhitespace_and_maxTrailingZerosNoValue;
			double minTrailingZeros_gte_minTrailingZerosAndWhitespace = score_BuildNumberFormatFeatures.MinTrailingZeros_gte_minTrailingZerosAndWhitespace;
			double scaleHasValueAndMaxTrailingZerosIsZero = score_BuildNumberFormatFeatures.ScaleHasValueAndMaxTrailingZerosIsZero;
			double scale = score_BuildNumberFormatFeatures.Scale;
			double hasScale = score_BuildNumberFormatFeatures.HasScale;
			double hasSeparator = score_BuildNumberFormatFeatures.HasSeparator;
			return this._model.BuildNumberFormat(num, featureValue, featureValue2, featureValue3, featureValue4, featureValue5, featureValue6, minLeadingZerosHasValue, minLeadingZeros, minTrailingZerosHasValue, minTrailingZeros, maxTrailingZerosHasValue, maxTrailingZeros, minLeadingZerosAndWhiteSpaceHasValue, minLeadingZerosAndWhitespace, minTrailingZerosAndWhiteSpaceHasValue, minTrailingZerosAndWhitespace, minLeadingZeros_greaterThan_MinLeadingZerosAndWhitespace, minTrailingZeros_greaterThan_MinTrailingZerosAndWhitespace, maxTrailingZeros_lessThan_MinTrailingZerosAndWhitespace, minTrailingZeros_eq_MaxTrailingZeros, minTrailingZeros_lessThan_minTrailingZerosAndWhitespace_and_maxTrailingZerosHasValue, minTrailingZeros_lessThan_minTrailingZerosAndWhitespace_and_maxTrailingZerosNoValue, minTrailingZeros_gte_minTrailingZerosAndWhitespace, scaleHasValueAndMaxTrailingZerosIsZero, scale, hasScale, hasSeparator);
		}

		// Token: 0x0600F85E RID: 63582 RVA: 0x0034E354 File Offset: 0x0034C554
		[FeatureCalculator("numberFormatLiteral", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double numberFormatLiteral(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			return this._model.numberFormatLiteral(num);
		}

		// Token: 0x0600F85F RID: 63583 RVA: 0x0034E378 File Offset: 0x0034C578
		[FeatureCalculator("outputDtFormat", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double outputDtFormat(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			LiteralNode literalNode = node as LiteralNode;
			RankingSubfeatureCalculator.OutputDtFormatScoreFeatures outputDtFormatScoreFeatures = RankingSubfeatureCalculator.OutputDtFormatScore(learningInfo, (DateTimeFormat)((literalNode != null) ? literalNode.Value : null));
			double timeBeforeDate = outputDtFormatScoreFeatures.TimeBeforeDate;
			double periodWithFullHour = outputDtFormatScoreFeatures.PeriodWithFullHour;
			double hasOneDecimalPoint = outputDtFormatScoreFeatures.HasOneDecimalPoint;
			double constantLength = outputDtFormatScoreFeatures.ConstantLength;
			double digitConstantLength = outputDtFormatScoreFeatures.DigitConstantLength;
			double separatorKindMatches = outputDtFormatScoreFeatures.SeparatorKindMatches;
			double separatorKindMisMatches = outputDtFormatScoreFeatures.SeparatorKindMisMatches;
			double unlikelySeparatorCount = outputDtFormatScoreFeatures.UnlikelySeparatorCount;
			double separatorCount = outputDtFormatScoreFeatures.SeparatorCount;
			double hasNonDelimitedNumbers = outputDtFormatScoreFeatures.HasNonDelimitedNumbers;
			double isNumeric = outputDtFormatScoreFeatures.IsNumeric;
			double minDateInversions = outputDtFormatScoreFeatures.MinDateInversions;
			double minTimeInversions = outputDtFormatScoreFeatures.MinTimeInversions;
			double isMatchingCommonDatePartsOrders = outputDtFormatScoreFeatures.IsMatchingCommonDatePartsOrders;
			double datePartOrderCount = outputDtFormatScoreFeatures.DatePartOrderCount;
			double isMatchingCommonTimePartsOrders = outputDtFormatScoreFeatures.IsMatchingCommonTimePartsOrders;
			double timePartOrderCount = outputDtFormatScoreFeatures.TimePartOrderCount;
			double variableLengthCount = outputDtFormatScoreFeatures.VariableLengthCount;
			double matchedPartsCount = outputDtFormatScoreFeatures.MatchedPartsCount;
			double hasDayOfWeekInMonth = outputDtFormatScoreFeatures.HasDayOfWeekInMonth;
			double timeAndDateShareSeparator = outputDtFormatScoreFeatures.TimeAndDateShareSeparator;
			double betweenTimeDateSeparatorReused = outputDtFormatScoreFeatures.BetweenTimeDateSeparatorReused;
			return this._model.outputDtFormat(num, timeBeforeDate, periodWithFullHour, hasOneDecimalPoint, constantLength, digitConstantLength, separatorKindMatches, separatorKindMisMatches, unlikelySeparatorCount, separatorCount, hasNonDelimitedNumbers, isNumeric, minDateInversions, minTimeInversions, isMatchingCommonDatePartsOrders, datePartOrderCount, isMatchingCommonTimePartsOrders, timePartOrderCount, variableLengthCount, matchedPartsCount, hasDayOfWeekInMonth, timeAndDateShareSeparator, betweenTimeDateSeparatorReused);
		}

		// Token: 0x0600F860 RID: 63584 RVA: 0x0034E4A8 File Offset: 0x0034C6A8
		[FeatureCalculator("inputDtFormats", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double inputDtFormats(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			LiteralNode literalNode = node as LiteralNode;
			RankingSubfeatureCalculator.InputDtFormatScoreFeatures inputDtFormatScoreFeatures = RankingSubfeatureCalculator.InputDtFormatScore(learningInfo, (DateTimeFormat[])((literalNode != null) ? literalNode.Value : null));
			double maxScore = inputDtFormatScoreFeatures.MaxScore;
			double minScore = inputDtFormatScoreFeatures.MinScore;
			double averageScore = inputDtFormatScoreFeatures.AverageScore;
			double numFormats = inputDtFormatScoreFeatures.NumFormats;
			double anyAmbiguous = inputDtFormatScoreFeatures.AnyAmbiguous;
			double matchedDifferentParts = inputDtFormatScoreFeatures.MatchedDifferentParts;
			return this._model.inputDtFormats(num, maxScore, minScore, averageScore, numFormats, anyAmbiguous, matchedDifferentParts);
		}

		// Token: 0x0600F861 RID: 63585 RVA: 0x0034E528 File Offset: 0x0034C728
		[FeatureCalculator("lookupDictionary", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double lookupDictionary(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			return this._model.lookupDictionary(num);
		}

		// Token: 0x0600F862 RID: 63586 RVA: 0x0034E54C File Offset: 0x0034C74C
		[FeatureCalculator("idx", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double idx(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			return this._model.idx(num);
		}

		// Token: 0x0600F863 RID: 63587 RVA: 0x0034E570 File Offset: 0x0034C770
		[FeatureCalculator("columnIdx", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double columnIdx(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			return this._model.columnIdx(num);
		}

		// Token: 0x0600F864 RID: 63588 RVA: 0x0034E594 File Offset: 0x0034C794
		[FeatureCalculator("vs", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double vs(LearningInfo learningInfo, ProgramNode node)
		{
			double num = 1.0;
			return this._model.vs(num);
		}

		// Token: 0x04005BDD RID: 23517
		private readonly Grammar _grammar;

		// Token: 0x04005BDE RID: 23518
		private readonly IRankingScoreModel _model;

		// Token: 0x04005BDF RID: 23519
		private readonly int? _randomSeed;

		// Token: 0x04005BE0 RID: 23520
		private readonly RankingSubfeatureCalculator _subfeatureCalculator;
	}
}
