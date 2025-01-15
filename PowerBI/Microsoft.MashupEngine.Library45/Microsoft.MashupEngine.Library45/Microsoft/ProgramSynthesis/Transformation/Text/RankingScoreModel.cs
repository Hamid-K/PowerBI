using System;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;

namespace Microsoft.ProgramSynthesis.Transformation.Text
{
	// Token: 0x02001BA3 RID: 7075
	public class RankingScoreModel : IRankingScoreModel
	{
		// Token: 0x0600E791 RID: 59281 RVA: 0x00311E14 File Offset: 0x00310014
		public double AbsolutePosition(double bias_AbsolutePosition, double score_AbsolutePosition_x, double score_AbsolutePosition_k, double base_IsLearningInfoNull, double base_AllSameLength)
		{
			return 0.0001 * score_AbsolutePosition_k * ((base_AllSameLength == 0.0) ? 1.0 : 50000.0);
		}

		// Token: 0x0600E792 RID: 59282 RVA: 0x00311E14 File Offset: 0x00310014
		public double RelativePosition(double bias_RelativePosition, double score_RelativePosition_x, double score_RelativePosition_k, double base_IsLearningInfoNull, double base_AllSameLength)
		{
			return 0.0001 * score_RelativePosition_k * ((base_AllSameLength == 0.0) ? 1.0 : 50000.0);
		}

		// Token: 0x0600E793 RID: 59283 RVA: 0x00311E43 File Offset: 0x00310043
		public double Add(double bias_Add, double score_Add_pl1, double score_Add_pl2, double derived_Pp1Pp2)
		{
			return derived_Pp1Pp2;
		}

		// Token: 0x0600E794 RID: 59284 RVA: 0x00311E47 File Offset: 0x00310047
		public double AsDecimal(double bias_AsDecimal, double score_AsDecimal_cell, double base_CastingInputStringToNumberOrDate)
		{
			return score_AsDecimal_cell + base_CastingInputStringToNumberOrDate * -100.0;
		}

		// Token: 0x0600E795 RID: 59285 RVA: 0x00311E47 File Offset: 0x00310047
		public double AsPartialDateTime(double bias_AsPartialDateTime, double score_AsPartialDateTime_cell, double base_CastingInputStringToNumberOrDate)
		{
			return score_AsPartialDateTime_cell + base_CastingInputStringToNumberOrDate * -100.0;
		}

		// Token: 0x0600E796 RID: 59286 RVA: 0x00311E56 File Offset: 0x00310056
		public double AsValueSubstring(double bias_AsValueSubstring, double score_AsValueSubstring_indexInput)
		{
			throw new InvalidOperationException("Attempted to rank program containing AsValueSubstring");
		}

		// Token: 0x0600E797 RID: 59287 RVA: 0x00311E64 File Offset: 0x00310064
		public double BuildNumberFormat(double bias_BuildNumberFormat, double score_BuildNumberFormat_minTrailingZeros, double score_BuildNumberFormat_maxTrailingZeros, double score_BuildNumberFormat_minTrailingZerosAndWhitespace, double score_BuildNumberFormat_minLeadingZeros, double score_BuildNumberFormat_minLeadingZerosAndWhitespace, double score_BuildNumberFormat_numberFormatDetails, double base_MinLeadingZerosHasValue, double base_MinLeadingZeros, double base_MinTrailingZerosHasValue, double base_MinTrailingZeros, double base_MaxTrailingZerosHasValue, double base_MaxTrailingZeros, double base_MinLeadingZerosAndWhiteSpaceHasValue, double base_MinLeadingZerosAndWhitespace, double base_MinTrailingZerosAndWhiteSpaceHasValue, double base_MinTrailingZerosAndWhitespace, double base_MinLeadingZeros_greaterThan_MinLeadingZerosAndWhitespace, double base_MinTrailingZeros_greaterThan_MinTrailingZerosAndWhitespace, double base_MaxTrailingZeros_lessThan_MinTrailingZerosAndWhitespace, double base_MinTrailingZeros_eq_MaxTrailingZeros, double base_MinTrailingZeros_lessThan_minTrailingZerosAndWhitespace_and_maxTrailingZerosHasValue, double base_MinTrailingZeros_lessThan_minTrailingZerosAndWhitespace_and_maxTrailingZerosNoValue, double base_MinTrailingZeros_gte_minTrailingZerosAndWhitespace, double base_ScaleHasValueAndMaxTrailingZerosIsZero, double base_Scale, double base_HasScale, double base_HasSeparator)
		{
			return base_MinLeadingZeros_greaterThan_MinLeadingZerosAndWhitespace * -100.0 + base_MinTrailingZeros_greaterThan_MinTrailingZerosAndWhitespace * -100.0 + base_MinTrailingZeros_lessThan_minTrailingZerosAndWhitespace_and_maxTrailingZerosHasValue * -0.1 + base_MinTrailingZeros_lessThan_minTrailingZerosAndWhitespace_and_maxTrailingZerosNoValue * -0.3 + base_MinTrailingZeros_gte_minTrailingZerosAndWhitespace * -0.01 + base_MaxTrailingZeros_lessThan_MinTrailingZerosAndWhitespace * -0.1 + base_MaxTrailingZerosHasValue * -0.1 + base_MinTrailingZerosHasValue * -0.3 + base_MinTrailingZerosAndWhiteSpaceHasValue * -0.01 + base_MinLeadingZerosHasValue * -0.01 * base_MinLeadingZeros + base_MinLeadingZerosAndWhiteSpaceHasValue * -0.01 * base_MinLeadingZerosAndWhitespace + base_MinTrailingZeros_eq_MaxTrailingZeros * 0.2 + base_ScaleHasValueAndMaxTrailingZerosIsZero * -15.0 + base_HasScale * -10.0;
		}

		// Token: 0x0600E798 RID: 59288 RVA: 0x0001AF59 File Offset: 0x00019159
		public double numberFormatLiteral(double bias_numberFormatLiteral)
		{
			return 0.0;
		}

		// Token: 0x0600E799 RID: 59289 RVA: 0x0003B61D File Offset: 0x0003981D
		public double ChooseInput(double bias_ChooseInput, double score_ChooseInput_vs, double score_ChooseInput_columnName)
		{
			return score_ChooseInput_vs;
		}

		// Token: 0x0600E79A RID: 59290 RVA: 0x00311F2C File Offset: 0x0031012C
		public double columnIdx(double bias_columnIdx)
		{
			throw new InvalidOperationException("Attempted to rank program containing columnIdx");
		}

		// Token: 0x0600E79B RID: 59291 RVA: 0x00311F38 File Offset: 0x00310138
		public double Concat(double bias_Concat, double score_Concat_f, double score_Concat_e, double base_NewInputsCount, double base_RepeatWholeColumnsCount, double base_BothSidesConstant, double base_ConcatNumbers, double base_FValueLen, double base_EValueLen, double base_FValueLast, double base_EValueFirst, double base_ConcatNonCommonConstants, double base_FContainsCommonDelimiters, double base_EContainsCommonDelimiters, double base_FInputsCount, double base_EInputsCount, double base_FWholeColumnsCount, double base_EWholeColumnsCount)
		{
			return score_Concat_f + score_Concat_e - 0.2 - ((base_FInputsCount > 0.0 && base_EInputsCount > 0.0) ? (base_NewInputsCount * 0.5) : 0.0) - ((base_FWholeColumnsCount > 0.0 && base_EWholeColumnsCount > 0.0) ? (base_RepeatWholeColumnsCount * 20.0 * 0.9) : 0.0) - base_ConcatNumbers * 100.0 - base_ConcatNonCommonConstants * 10000.0;
		}

		// Token: 0x0600E79C RID: 59292 RVA: 0x00311FDA File Offset: 0x003101DA
		public virtual double ConstStr(double bias_ConstStr, double score_ConstStr_s, double base_ConstantStringLength, double base_LogConstantStringLength, double base_IsCommonDelimiter, double base_ExampleCount, double base_AllInputsCount, double base_ConstantInInput, double base_ConstantinInputPenalty, double base_ConditionalTokenCounts)
		{
			if (base_IsCommonDelimiter != 1.0)
			{
				return -18.4207 - base_LogConstantStringLength - base_ConstantinInputPenalty;
			}
			return 1.0;
		}

		// Token: 0x0600E79D RID: 59293 RVA: 0x00312002 File Offset: 0x00310202
		public double DtRangeConcat(double bias_DtRangeConcat, double score_DtRangeConcat_dtRangeSubstring, double score_DtRangeConcat_dtRangeString)
		{
			return score_DtRangeConcat_dtRangeSubstring + score_DtRangeConcat_dtRangeString;
		}

		// Token: 0x0600E79E RID: 59294 RVA: 0x00311FDA File Offset: 0x003101DA
		public double DtRangeConstStr(double bias_DtRangeConstStr, double score_DtRangeConstStr_s, double base_ConstantStringLength, double base_LogConstantStringLength, double base_IsCommonDelimiter, double base_ExampleCount, double base_AllInputsCount, double base_ConstantInInput, double base_ConstantinInputPenalty, double base_ConditionalTokenCounts)
		{
			if (base_IsCommonDelimiter != 1.0)
			{
				return -18.4207 - base_LogConstantStringLength - base_ConstantinInputPenalty;
			}
			return 1.0;
		}

		// Token: 0x0600E79F RID: 59295 RVA: 0x00312008 File Offset: 0x00310208
		public double dtRoundingSpec(double bias_dtRoundingSpec, double base_UnitScore, double base_RoundingSpecUnit, double base_IsCloseFactor, double base_UpperExcludePart, double base_DisplayDeltaRatio, double base_ReducedDenominatorInverse, double base_IsRoundingUp)
		{
			return base_UnitScore + base_IsCloseFactor * 0.05 - base_UpperExcludePart * 0.03 + base_DisplayDeltaRatio * 0.1 + 0.01 * base_ReducedDenominatorInverse - base_IsRoundingUp * 0.001;
		}

		// Token: 0x0600E7A0 RID: 59296 RVA: 0x0003B61D File Offset: 0x0003981D
		public double externalExtractor(double bias_externalExtractor, double base_ExtractorScore)
		{
			return base_ExtractorScore;
		}

		// Token: 0x0600E7A1 RID: 59297 RVA: 0x00312057 File Offset: 0x00310257
		public double ExternalExtractorPositionPair(double bias_ExternalExtractorPositionPair, double score_ExternalExtractorPositionPair_x, double score_ExternalExtractorPositionPair_externalExtractor, double score_ExternalExtractorPositionPair_k, double derived_ExtractorKK)
		{
			return 2.0 * derived_ExtractorKK;
		}

		// Token: 0x0600E7A2 RID: 59298 RVA: 0x00312068 File Offset: 0x00310268
		public double FormatDateTimeRange(double bias_FormatDateTimeRange, double score_FormatDateTimeRange_inputDateTime, double score_FormatDateTimeRange_outputDtFormat, double score_FormatDateTimeRange_s, double score_FormatDateTimeRange_dtRoundingSpec, double score_FormatDateTimeRange_dtRoundingSpec2, double base_SeparatorContainsDigit, double base_SeparatorIsOnlySymbolsAndPunctuation, double base_SeparatorIsOnlyWhitespace, double base_SeparatorIsWrappedByWhitespace, double base_SeparatorIsCommonDateTimeSeparator)
		{
			return score_FormatDateTimeRange_inputDateTime + score_FormatDateTimeRange_outputDtFormat + score_FormatDateTimeRange_s + score_FormatDateTimeRange_dtRoundingSpec + score_FormatDateTimeRange_dtRoundingSpec2 + -10.0 + base_SeparatorContainsDigit * -100.0 + 5.0 * (base_SeparatorIsOnlySymbolsAndPunctuation + base_SeparatorIsWrappedByWhitespace) - 5.0 * base_SeparatorIsOnlyWhitespace - 10.0 * base_SeparatorIsCommonDateTimeSeparator;
		}

		// Token: 0x0600E7A3 RID: 59299 RVA: 0x003120C2 File Offset: 0x003102C2
		public double FormatNumber(double bias_FormatNumber, double score_FormatNumber_number, double score_FormatNumber_numberFormat)
		{
			return score_FormatNumber_number + score_FormatNumber_numberFormat - 10.0;
		}

		// Token: 0x0600E7A4 RID: 59300 RVA: 0x003120D1 File Offset: 0x003102D1
		public double FormatNumericRange(double bias_FormatNumericRange, double score_FormatNumericRange_inputNumber, double score_FormatNumericRange_numberFormat, double score_FormatNumericRange_s, double score_FormatNumericRange_roundingSpec, double score_FormatNumericRange_roundingSpec2, double base_RoundToMultipleOf5, double base_RoundToMultipleOf5Value)
		{
			return score_FormatNumericRange_inputNumber + score_FormatNumericRange_numberFormat + score_FormatNumericRange_s + score_FormatNumericRange_roundingSpec + score_FormatNumericRange_roundingSpec2 + base_RoundToMultipleOf5 * 0.1;
		}

		// Token: 0x0600E7A5 RID: 59301 RVA: 0x003120EC File Offset: 0x003102EC
		public double FormatPartialDateTime(double bias_FormatPartialDateTime, double score_FormatPartialDateTime_datetime, double score_FormatPartialDateTime_outputDtFormat, double base_SameDateFormat, double base_SameNumberPenalty, double base_ExtractionMatches)
		{
			return score_FormatPartialDateTime_datetime + score_FormatPartialDateTime_outputDtFormat + base_SameDateFormat * -50.0 + base_SameNumberPenalty * -100.0 + base_ExtractionMatches * 3.0 - 5.0;
		}

		// Token: 0x0600E7A6 RID: 59302 RVA: 0x0001AF59 File Offset: 0x00019159
		public double idx(double bias_idx)
		{
			return 0.0;
		}

		// Token: 0x0600E7A7 RID: 59303 RVA: 0x00312122 File Offset: 0x00310322
		public double IfThenElse(double bias_IfThenElse, double score_IfThenElse_b, double score_IfThenElse_st, double score_IfThenElse_switch)
		{
			return score_IfThenElse_b + score_IfThenElse_st + score_IfThenElse_switch - 100.0;
		}

		// Token: 0x0600E7A8 RID: 59304 RVA: 0x00312134 File Offset: 0x00310334
		public double IndexInputString(double bias_IndexInputString, double score_IndexInputString_vs, double score_IndexInputString_columnIdx)
		{
			throw new InvalidOperationException("Attempt to rank program containing IndexInput");
		}

		// Token: 0x0600E7A9 RID: 59305 RVA: 0x00312140 File Offset: 0x00310340
		public double inputDtFormats(double bias_inputDtFormats, double base_MaxScore, double base_MinScore, double base_AverageScore, double base_NumFormats, double base_AnyAmbiguous, double base_MatchedDifferentParts)
		{
			return (base_MaxScore + base_AverageScore) / 2.0 - base_NumFormats / 100.0 + base_AnyAmbiguous * -10.0 + base_MatchedDifferentParts * -2.0;
		}

		// Token: 0x0600E7AA RID: 59306 RVA: 0x00312177 File Offset: 0x00310377
		public double k(double bias_k, double base_KPositive, double base_KScore)
		{
			return base_KScore;
		}

		// Token: 0x0600E7AB RID: 59307 RVA: 0x00312177 File Offset: 0x00310377
		public double LetCell(double bias_LetCell, double score_LetCell_lookupInput, double score_LetCell_conv)
		{
			return score_LetCell_conv;
		}

		// Token: 0x0600E7AC RID: 59308 RVA: 0x00312177 File Offset: 0x00310377
		public double LetColumnName(double bias_LetColumnName, double score_LetColumnName_idx, double score_LetColumnName_letOptions)
		{
			return score_LetColumnName_letOptions;
		}

		// Token: 0x0600E7AD RID: 59309 RVA: 0x0031217A File Offset: 0x0031037A
		public double LetPL1(double bias_LetPL1, double score_LetPL1_pos, double score_LetPL1__LetB7, double derived_Pl1Pl2)
		{
			return derived_Pl1Pl2 * 0.001;
		}

		// Token: 0x0600E7AE RID: 59310 RVA: 0x0003B61D File Offset: 0x0003981D
		public double LetPL2(double bias_LetPL2, double score_LetPL2_pos, double score_LetPL2__LetB4)
		{
			return score_LetPL2_pos;
		}

		// Token: 0x0600E7AF RID: 59311 RVA: 0x00312177 File Offset: 0x00310377
		public double LetPredicate(double bias_LetPredicate, double score_LetPredicate_y, double score_LetPredicate_pred)
		{
			return score_LetPredicate_pred;
		}

		// Token: 0x0600E7B0 RID: 59312 RVA: 0x00312177 File Offset: 0x00310377
		public double LetSharedDateTimeFormat(double bias_LetSharedDateTimeFormat, double score_LetSharedDateTimeFormat_outputDtFormat, double score_LetSharedDateTimeFormat_dtRangeString)
		{
			return score_LetSharedDateTimeFormat_dtRangeString;
		}

		// Token: 0x0600E7B1 RID: 59313 RVA: 0x00312177 File Offset: 0x00310377
		public double LetSharedNumberFormat(double bias_LetSharedNumberFormat, double score_LetSharedNumberFormat_numberFormat, double score_LetSharedNumberFormat_rangeString)
		{
			return score_LetSharedNumberFormat_rangeString;
		}

		// Token: 0x0600E7B2 RID: 59314 RVA: 0x00312177 File Offset: 0x00310377
		public double LetSharedParsedNumber(double bias_LetSharedParsedNumber, double score_LetSharedParsedNumber_inputNumber, double score_LetSharedParsedNumber__LetB0)
		{
			return score_LetSharedParsedNumber__LetB0;
		}

		// Token: 0x0600E7B3 RID: 59315 RVA: 0x00312177 File Offset: 0x00310377
		public double LetSharedParsedDateTime(double bias_LetSharedParsedDateTime, double score_LetSharedParsedDateTime_inputDateTime, double score_LetSharedParsedDateTime__LetB1)
		{
			return score_LetSharedParsedDateTime__LetB1;
		}

		// Token: 0x0600E7B4 RID: 59316 RVA: 0x00312177 File Offset: 0x00310377
		public double LetX(double bias_LetX, double score_LetX_v, double score_LetX_conv)
		{
			return score_LetX_conv;
		}

		// Token: 0x0600E7B5 RID: 59317 RVA: 0x00312188 File Offset: 0x00310388
		public double Lookup(double bias_Lookup, double score_Lookup_x, double score_Lookup_lookupDictionary)
		{
			return score_Lookup_x + score_Lookup_lookupDictionary - 10000000.0;
		}

		// Token: 0x0600E7B6 RID: 59318 RVA: 0x0001AF59 File Offset: 0x00019159
		public double lookupDictionary(double bias_lookupDictionary)
		{
			return 0.0;
		}

		// Token: 0x0600E7B7 RID: 59319 RVA: 0x0003B61D File Offset: 0x0003981D
		public double LookupInput(double bias_LookupInput, double score_LookupInput_vs, double score_LookupInput_columnName)
		{
			return score_LookupInput_vs;
		}

		// Token: 0x0600E7B8 RID: 59320 RVA: 0x0001AF59 File Offset: 0x00019159
		public double maxTrailingZeros(double bias_maxTrailingZeros)
		{
			return 0.0;
		}

		// Token: 0x0600E7B9 RID: 59321 RVA: 0x0001AF59 File Offset: 0x00019159
		public double minLeadingZeros(double bias_minLeadingZeros)
		{
			return 0.0;
		}

		// Token: 0x0600E7BA RID: 59322 RVA: 0x0001AF59 File Offset: 0x00019159
		public double minLeadingZerosAndWhitespace(double bias_minLeadingZerosAndWhitespace)
		{
			return 0.0;
		}

		// Token: 0x0600E7BB RID: 59323 RVA: 0x0001AF59 File Offset: 0x00019159
		public double minTrailingZeros(double bias_minTrailingZeros)
		{
			return 0.0;
		}

		// Token: 0x0600E7BC RID: 59324 RVA: 0x0001AF59 File Offset: 0x00019159
		public double minTrailingZerosAndWhitespace(double bias_minTrailingZerosAndWhitespace)
		{
			return 0.0;
		}

		// Token: 0x0600E7BD RID: 59325 RVA: 0x0003B61D File Offset: 0x0003981D
		public double name(double bias_name, double base_StringLength)
		{
			return base_StringLength;
		}

		// Token: 0x0600E7BE RID: 59326 RVA: 0x0001AF59 File Offset: 0x00019159
		public double numberFormatDetails(double bias_numberFormatDetails)
		{
			return 0.0;
		}

		// Token: 0x0600E7BF RID: 59327 RVA: 0x0001AF59 File Offset: 0x00019159
		public double numberFormatSeparatorChar(double bias_numberFormatSeparatorChar)
		{
			return 0.0;
		}

		// Token: 0x0600E7C0 RID: 59328 RVA: 0x00312198 File Offset: 0x00310398
		public double outputDtFormat(double bias_outputDtFormat, double base_TimeBeforeDate, double base_PeriodWithFullHour, double base_HasOneDecimalPoint, double base_ConstantLength, double base_DigitConstantLength, double base_SeparatorKindMatches, double base_SeparatorKindMisMatches, double base_UnlikelySeparatorCount, double base_SeparatorCount, double base_HasNonDelimitedNumbers, double base_IsNumeric, double base_MinDateInversions, double base_MinTimeInversions, double base_IsMatchingCommonDatePartsOrders, double base_DatePartOrderCount, double base_IsMatchingCommonTimePartsOrders, double base_TimePartOrderCount, double base_VariableLengthCount, double base_MatchedPartsCount, double base_HasDayOfWeekInMonth, double base_TimeAndDateShareSeparator, double base_BetweenTimeDateSeparatorReused)
		{
			return -15.0 + (base_ConstantLength + base_DigitConstantLength * 10.0 + base_HasOneDecimalPoint * 50.0) * -2.0 + base_SeparatorKindMatches * 3.0 + base_SeparatorKindMisMatches * -3.0 + base_UnlikelySeparatorCount * -200.0 + base_TimeAndDateShareSeparator * -3.0 + base_BetweenTimeDateSeparatorReused * -100.0 + base_HasNonDelimitedNumbers * -10.0 + base_MinDateInversions * -1.0 + base_MinTimeInversions * -3.0 + base_TimeBeforeDate * -5.0 + base_PeriodWithFullHour * -0.5 + base_VariableLengthCount * -1.5 + base_MatchedPartsCount * 5.0 + base_HasDayOfWeekInMonth * -2.0;
		}

		// Token: 0x0600E7C1 RID: 59329 RVA: 0x0031227C File Offset: 0x0031047C
		public double ParseNumber(double bias_ParseNumber, double score_ParseNumber_SS, double score_ParseNumber_numberFormatDetails)
		{
			return score_ParseNumber_SS - 1.0;
		}

		// Token: 0x0600E7C2 RID: 59330 RVA: 0x00312002 File Offset: 0x00310202
		public double ParsePartialDateTime(double bias_ParsePartialDateTime, double score_ParsePartialDateTime_SS, double score_ParsePartialDateTime_inputDtFormats)
		{
			return score_ParsePartialDateTime_SS + score_ParsePartialDateTime_inputDtFormats;
		}

		// Token: 0x0600E7C3 RID: 59331 RVA: 0x0031228C File Offset: 0x0031048C
		public double PosPair(double bias_PosPair, double score_PosPair_pos, double score_PosPair_pos2, double base_ConstantRegexExtractionPenaltyFactorBias, double base_RegexExtractionBonusBias, double derived_Pp1Pp2)
		{
			return ((base_ConstantRegexExtractionPenaltyFactorBias == 1.0) ? 0.01 : 1.0) * ((base_RegexExtractionBonusBias == 1.0) ? 1.166 : 1.0) * derived_Pp1Pp2;
		}

		// Token: 0x0600E7C4 RID: 59332 RVA: 0x0001AF59 File Offset: 0x00019159
		public double PosPairRelative(double bias_PosPairRelative, double score_PosPairRelative_pl1, double score_PosPairRelative_pl2p)
		{
			return 0.0;
		}

		// Token: 0x0600E7C5 RID: 59333 RVA: 0x003122DF File Offset: 0x003104DF
		public double r(double bias_r, double base_RegexScore, double base_TokenCount, double base_TokenScoreSum, double base_Token0Score, double base_Token1Score, double base_Token2Score)
		{
			return base_RegexScore / 1000.0;
		}

		// Token: 0x0600E7C6 RID: 59334 RVA: 0x00312002 File Offset: 0x00310202
		public double RangeConcat(double bias_RangeConcat, double score_RangeConcat_rangeSubstring, double score_RangeConcat_rangeString)
		{
			return score_RangeConcat_rangeSubstring + score_RangeConcat_rangeString;
		}

		// Token: 0x0600E7C7 RID: 59335 RVA: 0x00311FDA File Offset: 0x003101DA
		public double RangeConstStr(double bias_RangeConstStr, double score_RangeConstStr_s, double base_ConstantStringLength, double base_LogConstantStringLength, double base_IsCommonDelimiter, double base_ExampleCount, double base_AllInputsCount, double base_ConstantInInput, double base_ConstantinInputPenalty, double base_ConditionalTokenCounts)
		{
			if (base_IsCommonDelimiter != 1.0)
			{
				return -18.4207 - base_LogConstantStringLength - base_ConstantinInputPenalty;
			}
			return 1.0;
		}

		// Token: 0x0600E7C8 RID: 59336 RVA: 0x003120EC File Offset: 0x003102EC
		public double RangeFormatDateTime(double bias_RangeFormatDateTime, double score_RangeFormatDateTime_rangeDateTime, double score_RangeFormatDateTime_sharedDtFormat, double base_SameDateFormat, double base_SameNumberPenalty, double base_ExtractionMatches)
		{
			return score_RangeFormatDateTime_rangeDateTime + score_RangeFormatDateTime_sharedDtFormat + base_SameDateFormat * -50.0 + base_SameNumberPenalty * -100.0 + base_ExtractionMatches * 3.0 - 5.0;
		}

		// Token: 0x0600E7C9 RID: 59337 RVA: 0x003120C2 File Offset: 0x003102C2
		public double RangeFormatNumber(double bias_RangeFormatNumber, double score_RangeFormatNumber_rangeNumber, double score_RangeFormatNumber_sharedNumberFormat)
		{
			return score_RangeFormatNumber_rangeNumber + score_RangeFormatNumber_sharedNumberFormat - 10.0;
		}

		// Token: 0x0600E7CA RID: 59338 RVA: 0x003120C2 File Offset: 0x003102C2
		public double RangeRoundDateTime(double bias_RangeRoundDateTime, double score_RangeRoundDateTime_sharedParsedDt, double score_RangeRoundDateTime_dtRoundingSpec)
		{
			return score_RangeRoundDateTime_sharedParsedDt + score_RangeRoundDateTime_dtRoundingSpec - 10.0;
		}

		// Token: 0x0600E7CB RID: 59339 RVA: 0x003120C2 File Offset: 0x003102C2
		public double RangeRoundNumber(double bias_RangeRoundNumber, double score_RangeRoundNumber_sharedParsedNumber, double score_RangeRoundNumber_roundingSpec)
		{
			return score_RangeRoundNumber_sharedParsedNumber + score_RangeRoundNumber_roundingSpec - 10.0;
		}

		// Token: 0x0600E7CC RID: 59340 RVA: 0x00312002 File Offset: 0x00310202
		public double RegexPair(double bias_RegexPair, double score_RegexPair_r, double score_RegexPair_r2)
		{
			return score_RegexPair_r + score_RegexPair_r2;
		}

		// Token: 0x0600E7CD RID: 59341 RVA: 0x003122EC File Offset: 0x003104EC
		public double RegexPosition(double bias_RegexPosition, double score_RegexPosition_x, double score_RegexPosition_regexPair, double score_RegexPosition_k, double base_UseProportionNotMatched, double base_NotMatchedFactor, double derived_RrK)
		{
			return derived_RrK * base_NotMatchedFactor;
		}

		// Token: 0x0600E7CE RID: 59342 RVA: 0x003122EC File Offset: 0x003104EC
		public double RegexPositionRelative(double bias_RegexPositionRelative, double score_RegexPositionRelative_x, double score_RegexPositionRelative_regexPair, double score_RegexPositionRelative_k, double base_UseProportionNotMatched, double base_NotMatchedFactor, double derived_RrK)
		{
			return derived_RrK * base_NotMatchedFactor;
		}

		// Token: 0x0600E7CF RID: 59343 RVA: 0x003122F3 File Offset: 0x003104F3
		public double RegexPositionPair(double bias_RegexPositionPair, double score_RegexPositionPair_x, double score_RegexPositionPair_r, double score_RegexPositionPair_k, double base_RegexIsConstant, double base_NotMatchedFactor, double base_ProportionNull, double derived_PosPairRelativeFeaturesRrKk, double derived_PosPairRelativeFeaturesRKk, double derived_PosPairRelativeFeaturesKk, double derived_PosPairRelativeFeaturesRScore)
		{
			return ((base_RegexIsConstant == 1.0) ? 0.01 : 1.166) * (score_RegexPositionPair_r + RankingScoreModel.EmptyRegularExpressionScore) * (score_RegexPositionPair_r + RankingScoreModel.EmptyRegularExpressionScore) * derived_PosPairRelativeFeaturesKk * base_NotMatchedFactor;
		}

		// Token: 0x0600E7D0 RID: 59344 RVA: 0x0031232C File Offset: 0x0031052C
		public double roundingSpec(double bias_roundingSpec, double base_Delta, double base_LogDelta, double base_DeltaIsPowerOf10, double base_Zero, double base_ZeroIsZero, double base_RoundingMode, double base_RoundingModeIsNearest, double base_RoundingModeIsTowardZero, double base_RoundingModeIsAwayFromZero)
		{
			return base_Delta / 1000000000.0 + base_DeltaIsPowerOf10 * base_Delta / 100000000.0 + ((base_RoundingModeIsNearest == 1.0) ? 0.0 : ((base_RoundingModeIsTowardZero == 1.0 || base_RoundingModeIsAwayFromZero == 1.0) ? (-0.002) : (-0.001))) + base_ZeroIsZero * 0.1;
		}

		// Token: 0x0600E7D1 RID: 59345 RVA: 0x003120C2 File Offset: 0x003102C2
		public double RoundNumber(double bias_RoundNumber, double score_RoundNumber_inputNumber, double score_RoundNumber_roundingSpec)
		{
			return score_RoundNumber_inputNumber + score_RoundNumber_roundingSpec - 10.0;
		}

		// Token: 0x0600E7D2 RID: 59346 RVA: 0x003120C2 File Offset: 0x003102C2
		public double RoundPartialDateTime(double bias_RoundPartialDateTime, double score_RoundPartialDateTime_inputDateTime, double score_RoundPartialDateTime_dtRoundingSpec)
		{
			return score_RoundPartialDateTime_inputDateTime + score_RoundPartialDateTime_dtRoundingSpec - 10.0;
		}

		// Token: 0x0600E7D3 RID: 59347 RVA: 0x003123A7 File Offset: 0x003105A7
		public double RSubStr(double bias_RSubStr, double score_RSubStr_x, double score_RSubStr_pl1)
		{
			return 1E-07;
		}

		// Token: 0x0600E7D4 RID: 59348 RVA: 0x00312177 File Offset: 0x00310377
		public double rule(double bias_rule, double score_rule__LetB2, double score_rule__LetB3)
		{
			return score_rule__LetB3;
		}

		// Token: 0x0600E7D5 RID: 59349 RVA: 0x00312177 File Offset: 0x00310377
		public double rule2(double bias_rule2, double score_rule2__LetB5, double score_rule2__LetB6)
		{
			return score_rule2__LetB6;
		}

		// Token: 0x0600E7D6 RID: 59350 RVA: 0x0003B61D File Offset: 0x0003981D
		public double s(double bias_s, double base_StringLength)
		{
			return base_StringLength;
		}

		// Token: 0x0600E7D7 RID: 59351 RVA: 0x00312002 File Offset: 0x00310202
		public double SelectInput(double bias_SelectInput, double score_SelectInput_vs, double score_SelectInput_name)
		{
			return score_SelectInput_vs + score_SelectInput_name;
		}

		// Token: 0x0600E7D8 RID: 59352 RVA: 0x00311E43 File Offset: 0x00310043
		public double SubStr(double bias_SubStr, double score_SubStr_x, double score_SubStr_PP, double derived_LogPP)
		{
			return derived_LogPP;
		}

		// Token: 0x0600E7D9 RID: 59353 RVA: 0x0031227C File Offset: 0x0031047C
		public double ToLowercase(double bias_ToLowercase, double score_ToLowercase_SS)
		{
			return score_ToLowercase_SS - 1.0;
		}

		// Token: 0x0600E7DA RID: 59354 RVA: 0x0031227C File Offset: 0x0031047C
		public double ToSimpleTitleCase(double bias_ToSimpleTitleCase, double score_ToSimpleTitleCase_SS)
		{
			return score_ToSimpleTitleCase_SS - 1.0;
		}

		// Token: 0x0600E7DB RID: 59355 RVA: 0x0031227C File Offset: 0x0031047C
		public double ToUppercase(double bias_ToUppercase, double score_ToUppercase_SS)
		{
			return score_ToUppercase_SS - 1.0;
		}

		// Token: 0x0600E7DC RID: 59356 RVA: 0x003123B2 File Offset: 0x003105B2
		public double Transformation(double bias_Transformation, double score_Transformation_e, double base_NullOutputProportion)
		{
			return -100.0 * base_NullOutputProportion + score_Transformation_e;
		}

		// Token: 0x0600E7DD RID: 59357 RVA: 0x0001AF59 File Offset: 0x00019159
		public double vs(double bias_vs)
		{
			return 0.0;
		}

		// Token: 0x0600E7DE RID: 59358 RVA: 0x003123C1 File Offset: 0x003105C1
		public double WholeColumn(double bias_WholeColumn, double score_WholeColumn_x, double base_WholeColumnBonus)
		{
			return 20.0;
		}

		// Token: 0x0400583D RID: 22589
		private const double KFactor = 0.0001;

		// Token: 0x0400583E RID: 22590
		private const double AllSameLengthBonus = 50000.0;

		// Token: 0x0400583F RID: 22591
		private static readonly double EmptyRegularExpressionScore = (double)new RegularExpression(0).Score / 1000.0;
	}
}
