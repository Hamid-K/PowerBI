using System;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics
{
	// Token: 0x02001CA5 RID: 7333
	public interface IRankingScoreModel
	{
		// Token: 0x0600F7C9 RID: 63433
		double IfThenElse(double bias_IfThenElse, double score_IfThenElse_b, double score_IfThenElse_st, double score_IfThenElse_switch);

		// Token: 0x0600F7CA RID: 63434
		double Transformation(double bias_Transformation, double score_Transformation_e, double base_NullOutputProportion);

		// Token: 0x0600F7CB RID: 63435
		double Concat(double bias_Concat, double score_Concat_f, double score_Concat_e, double base_NewInputsCount, double base_RepeatWholeColumnsCount, double base_BothSidesConstant, double base_ConcatNumbers, double base_FValueLen, double base_EValueLen, double base_FValueLast, double base_EValueFirst, double base_ConcatNonCommonConstants, double base_FContainsCommonDelimiters, double base_EContainsCommonDelimiters, double base_FInputsCount, double base_EInputsCount, double base_FWholeColumnsCount, double base_EWholeColumnsCount);

		// Token: 0x0600F7CC RID: 63436
		double ConstStr(double bias_ConstStr, double score_ConstStr_s, double base_ConstantStringLength, double base_LogConstantStringLength, double base_IsCommonDelimiter, double base_ExampleCount, double base_AllInputsCount, double base_ConstantInInput, double base_ConstantinInputPenalty, double base_ConditionalTokenCounts);

		// Token: 0x0600F7CD RID: 63437
		double LetColumnName(double bias_LetColumnName, double score_LetColumnName_idx, double score_LetColumnName_letOptions);

		// Token: 0x0600F7CE RID: 63438
		double LetCell(double bias_LetCell, double score_LetCell_lookupInput, double score_LetCell_conv);

		// Token: 0x0600F7CF RID: 63439
		double LetX(double bias_LetX, double score_LetX_v, double score_LetX_conv);

		// Token: 0x0600F7D0 RID: 63440
		double ChooseInput(double bias_ChooseInput, double score_ChooseInput_vs, double score_ChooseInput_columnName);

		// Token: 0x0600F7D1 RID: 63441
		double IndexInputString(double bias_IndexInputString, double score_IndexInputString_vs, double score_IndexInputString_columnIdx);

		// Token: 0x0600F7D2 RID: 63442
		double LookupInput(double bias_LookupInput, double score_LookupInput_vs, double score_LookupInput_columnName);

		// Token: 0x0600F7D3 RID: 63443
		double LetSharedNumberFormat(double bias_LetSharedNumberFormat, double score_LetSharedNumberFormat_numberFormat, double score_LetSharedNumberFormat_rangeString);

		// Token: 0x0600F7D4 RID: 63444
		double LetSharedDateTimeFormat(double bias_LetSharedDateTimeFormat, double score_LetSharedDateTimeFormat_outputDtFormat, double score_LetSharedDateTimeFormat_dtRangeString);

		// Token: 0x0600F7D5 RID: 63445
		double ToLowercase(double bias_ToLowercase, double score_ToLowercase_SS);

		// Token: 0x0600F7D6 RID: 63446
		double ToUppercase(double bias_ToUppercase, double score_ToUppercase_SS);

		// Token: 0x0600F7D7 RID: 63447
		double ToSimpleTitleCase(double bias_ToSimpleTitleCase, double score_ToSimpleTitleCase_SS);

		// Token: 0x0600F7D8 RID: 63448
		double FormatPartialDateTime(double bias_FormatPartialDateTime, double score_FormatPartialDateTime_datetime, double score_FormatPartialDateTime_outputDtFormat, double base_SameDateFormat, double base_SameNumberPenalty, double base_ExtractionMatches);

		// Token: 0x0600F7D9 RID: 63449
		double FormatNumber(double bias_FormatNumber, double score_FormatNumber_number, double score_FormatNumber_numberFormat);

		// Token: 0x0600F7DA RID: 63450
		double Lookup(double bias_Lookup, double score_Lookup_x, double score_Lookup_lookupDictionary);

		// Token: 0x0600F7DB RID: 63451
		double FormatNumericRange(double bias_FormatNumericRange, double score_FormatNumericRange_inputNumber, double score_FormatNumericRange_numberFormat, double score_FormatNumericRange_s, double score_FormatNumericRange_roundingSpec, double score_FormatNumericRange_roundingSpec2, double base_RoundToMultipleOf5, double base_RoundToMultipleOf5Value);

		// Token: 0x0600F7DC RID: 63452
		double FormatDateTimeRange(double bias_FormatDateTimeRange, double score_FormatDateTimeRange_inputDateTime, double score_FormatDateTimeRange_outputDtFormat, double score_FormatDateTimeRange_s, double score_FormatDateTimeRange_dtRoundingSpec, double score_FormatDateTimeRange_dtRoundingSpec2, double base_SeparatorContainsDigit, double base_SeparatorIsOnlySymbolsAndPunctuation, double base_SeparatorIsOnlyWhitespace, double base_SeparatorIsWrappedByWhitespace, double base_SeparatorIsCommonDateTimeSeparator);

		// Token: 0x0600F7DD RID: 63453
		double LetSharedParsedNumber(double bias_LetSharedParsedNumber, double score_LetSharedParsedNumber_inputNumber, double score_LetSharedParsedNumber__LetB0);

		// Token: 0x0600F7DE RID: 63454
		double LetSharedParsedDateTime(double bias_LetSharedParsedDateTime, double score_LetSharedParsedDateTime_inputDateTime, double score_LetSharedParsedDateTime__LetB1);

		// Token: 0x0600F7DF RID: 63455
		double RangeConcat(double bias_RangeConcat, double score_RangeConcat_rangeSubstring, double score_RangeConcat_rangeString);

		// Token: 0x0600F7E0 RID: 63456
		double RangeConstStr(double bias_RangeConstStr, double score_RangeConstStr_s, double base_ConstantStringLength, double base_LogConstantStringLength, double base_IsCommonDelimiter, double base_ExampleCount, double base_AllInputsCount, double base_ConstantInInput, double base_ConstantinInputPenalty, double base_ConditionalTokenCounts);

		// Token: 0x0600F7E1 RID: 63457
		double RangeFormatNumber(double bias_RangeFormatNumber, double score_RangeFormatNumber_rangeNumber, double score_RangeFormatNumber_sharedNumberFormat);

		// Token: 0x0600F7E2 RID: 63458
		double RangeRoundNumber(double bias_RangeRoundNumber, double score_RangeRoundNumber_sharedParsedNumber, double score_RangeRoundNumber_roundingSpec);

		// Token: 0x0600F7E3 RID: 63459
		double DtRangeConcat(double bias_DtRangeConcat, double score_DtRangeConcat_dtRangeSubstring, double score_DtRangeConcat_dtRangeString);

		// Token: 0x0600F7E4 RID: 63460
		double DtRangeConstStr(double bias_DtRangeConstStr, double score_DtRangeConstStr_s, double base_ConstantStringLength, double base_LogConstantStringLength, double base_IsCommonDelimiter, double base_ExampleCount, double base_AllInputsCount, double base_ConstantInInput, double base_ConstantinInputPenalty, double base_ConditionalTokenCounts);

		// Token: 0x0600F7E5 RID: 63461
		double RangeFormatDateTime(double bias_RangeFormatDateTime, double score_RangeFormatDateTime_rangeDateTime, double score_RangeFormatDateTime_sharedDtFormat, double base_SameDateFormat, double base_SameNumberPenalty, double base_ExtractionMatches);

		// Token: 0x0600F7E6 RID: 63462
		double RangeRoundDateTime(double bias_RangeRoundDateTime, double score_RangeRoundDateTime_sharedParsedDt, double score_RangeRoundDateTime_dtRoundingSpec);

		// Token: 0x0600F7E7 RID: 63463
		double RoundPartialDateTime(double bias_RoundPartialDateTime, double score_RoundPartialDateTime_inputDateTime, double score_RoundPartialDateTime_dtRoundingSpec);

		// Token: 0x0600F7E8 RID: 63464
		double AsPartialDateTime(double bias_AsPartialDateTime, double score_AsPartialDateTime_cell, double base_CastingInputStringToNumberOrDate);

		// Token: 0x0600F7E9 RID: 63465
		double ParsePartialDateTime(double bias_ParsePartialDateTime, double score_ParsePartialDateTime_SS, double score_ParsePartialDateTime_inputDtFormats);

		// Token: 0x0600F7EA RID: 63466
		double WholeColumn(double bias_WholeColumn, double score_WholeColumn_x, double base_WholeColumnBonus);

		// Token: 0x0600F7EB RID: 63467
		double SubStr(double bias_SubStr, double score_SubStr_x, double score_SubStr_PP, double derived_LogPP);

		// Token: 0x0600F7EC RID: 63468
		double Add(double bias_Add, double score_Add_pl1, double score_Add_pl2, double derived_Pp1Pp2);

		// Token: 0x0600F7ED RID: 63469
		double PosPairRelative(double bias_PosPairRelative, double score_PosPairRelative_pl1, double score_PosPairRelative_pl2p);

		// Token: 0x0600F7EE RID: 63470
		double rule(double bias_rule, double score_rule__LetB2, double score_rule__LetB3);

		// Token: 0x0600F7EF RID: 63471
		double RSubStr(double bias_RSubStr, double score_RSubStr_x, double score_RSubStr_pl1);

		// Token: 0x0600F7F0 RID: 63472
		double LetPL2(double bias_LetPL2, double score_LetPL2_pos, double score_LetPL2__LetB4);

		// Token: 0x0600F7F1 RID: 63473
		double rule2(double bias_rule2, double score_rule2__LetB5, double score_rule2__LetB6);

		// Token: 0x0600F7F2 RID: 63474
		double PosPair(double bias_PosPair, double score_PosPair_pos, double score_PosPair_pos2, double base_ConstantRegexExtractionPenaltyFactorBias, double base_RegexExtractionBonusBias, double derived_Pp1Pp2);

		// Token: 0x0600F7F3 RID: 63475
		double LetPL1(double bias_LetPL1, double score_LetPL1_pos, double score_LetPL1__LetB7, double derived_Pl1Pl2);

		// Token: 0x0600F7F4 RID: 63476
		double RegexPositionPair(double bias_RegexPositionPair, double score_RegexPositionPair_x, double score_RegexPositionPair_r, double score_RegexPositionPair_k, double base_RegexIsConstant, double base_NotMatchedFactor, double base_ProportionNull, double derived_PosPairRelativeFeaturesRrKk, double derived_PosPairRelativeFeaturesRKk, double derived_PosPairRelativeFeaturesKk, double derived_PosPairRelativeFeaturesRScore);

		// Token: 0x0600F7F5 RID: 63477
		double ExternalExtractorPositionPair(double bias_ExternalExtractorPositionPair, double score_ExternalExtractorPositionPair_x, double score_ExternalExtractorPositionPair_externalExtractor, double score_ExternalExtractorPositionPair_k, double derived_ExtractorKK);

		// Token: 0x0600F7F6 RID: 63478
		double RelativePosition(double bias_RelativePosition, double score_RelativePosition_x, double score_RelativePosition_k, double base_IsLearningInfoNull, double base_AllSameLength);

		// Token: 0x0600F7F7 RID: 63479
		double RegexPositionRelative(double bias_RegexPositionRelative, double score_RegexPositionRelative_x, double score_RegexPositionRelative_regexPair, double score_RegexPositionRelative_k, double base_UseProportionNotMatched, double base_NotMatchedFactor, double derived_RrK);

		// Token: 0x0600F7F8 RID: 63480
		double AbsolutePosition(double bias_AbsolutePosition, double score_AbsolutePosition_x, double score_AbsolutePosition_k, double base_IsLearningInfoNull, double base_AllSameLength);

		// Token: 0x0600F7F9 RID: 63481
		double RegexPosition(double bias_RegexPosition, double score_RegexPosition_x, double score_RegexPosition_regexPair, double score_RegexPosition_k, double base_UseProportionNotMatched, double base_NotMatchedFactor, double derived_RrK);

		// Token: 0x0600F7FA RID: 63482
		double RegexPair(double bias_RegexPair, double score_RegexPair_r, double score_RegexPair_r2);

		// Token: 0x0600F7FB RID: 63483
		double RoundNumber(double bias_RoundNumber, double score_RoundNumber_inputNumber, double score_RoundNumber_roundingSpec);

		// Token: 0x0600F7FC RID: 63484
		double AsDecimal(double bias_AsDecimal, double score_AsDecimal_cell, double base_CastingInputStringToNumberOrDate);

		// Token: 0x0600F7FD RID: 63485
		double ParseNumber(double bias_ParseNumber, double score_ParseNumber_SS, double score_ParseNumber_numberFormatDetails);

		// Token: 0x0600F7FE RID: 63486
		double LetPredicate(double bias_LetPredicate, double score_LetPredicate_y, double score_LetPredicate_pred);

		// Token: 0x0600F7FF RID: 63487
		double SelectInput(double bias_SelectInput, double score_SelectInput_vs, double score_SelectInput_name);

		// Token: 0x0600F800 RID: 63488
		double k(double bias_k, double base_KPositive, double base_KScore);

		// Token: 0x0600F801 RID: 63489
		double externalExtractor(double bias_externalExtractor, double base_ExtractorScore);

		// Token: 0x0600F802 RID: 63490
		double r(double bias_r, double base_RegexScore, double base_TokenCount, double base_TokenScoreSum, double base_Token0Score, double base_Token1Score, double base_Token2Score);

		// Token: 0x0600F803 RID: 63491
		double s(double bias_s, double base_StringLength);

		// Token: 0x0600F804 RID: 63492
		double name(double bias_name, double base_StringLength);

		// Token: 0x0600F805 RID: 63493
		double roundingSpec(double bias_roundingSpec, double base_Delta, double base_LogDelta, double base_DeltaIsPowerOf10, double base_Zero, double base_ZeroIsZero, double base_RoundingMode, double base_RoundingModeIsNearest, double base_RoundingModeIsTowardZero, double base_RoundingModeIsAwayFromZero);

		// Token: 0x0600F806 RID: 63494
		double dtRoundingSpec(double bias_dtRoundingSpec, double base_UnitScore, double base_RoundingSpecUnit, double base_IsCloseFactor, double base_UpperExcludePart, double base_DisplayDeltaRatio, double base_ReducedDenominatorInverse, double base_IsRoundingUp);

		// Token: 0x0600F807 RID: 63495
		double minTrailingZeros(double bias_minTrailingZeros);

		// Token: 0x0600F808 RID: 63496
		double maxTrailingZeros(double bias_maxTrailingZeros);

		// Token: 0x0600F809 RID: 63497
		double minTrailingZerosAndWhitespace(double bias_minTrailingZerosAndWhitespace);

		// Token: 0x0600F80A RID: 63498
		double minLeadingZeros(double bias_minLeadingZeros);

		// Token: 0x0600F80B RID: 63499
		double minLeadingZerosAndWhitespace(double bias_minLeadingZerosAndWhitespace);

		// Token: 0x0600F80C RID: 63500
		double numberFormatSeparatorChar(double bias_numberFormatSeparatorChar);

		// Token: 0x0600F80D RID: 63501
		double numberFormatDetails(double bias_numberFormatDetails);

		// Token: 0x0600F80E RID: 63502
		double BuildNumberFormat(double bias_BuildNumberFormat, double score_BuildNumberFormat_minTrailingZeros, double score_BuildNumberFormat_maxTrailingZeros, double score_BuildNumberFormat_minTrailingZerosAndWhitespace, double score_BuildNumberFormat_minLeadingZeros, double score_BuildNumberFormat_minLeadingZerosAndWhitespace, double score_BuildNumberFormat_numberFormatDetails, double base_MinLeadingZerosHasValue, double base_MinLeadingZeros, double base_MinTrailingZerosHasValue, double base_MinTrailingZeros, double base_MaxTrailingZerosHasValue, double base_MaxTrailingZeros, double base_MinLeadingZerosAndWhiteSpaceHasValue, double base_MinLeadingZerosAndWhitespace, double base_MinTrailingZerosAndWhiteSpaceHasValue, double base_MinTrailingZerosAndWhitespace, double base_MinLeadingZeros_greaterThan_MinLeadingZerosAndWhitespace, double base_MinTrailingZeros_greaterThan_MinTrailingZerosAndWhitespace, double base_MaxTrailingZeros_lessThan_MinTrailingZerosAndWhitespace, double base_MinTrailingZeros_eq_MaxTrailingZeros, double base_MinTrailingZeros_lessThan_minTrailingZerosAndWhitespace_and_maxTrailingZerosHasValue, double base_MinTrailingZeros_lessThan_minTrailingZerosAndWhitespace_and_maxTrailingZerosNoValue, double base_MinTrailingZeros_gte_minTrailingZerosAndWhitespace, double base_ScaleHasValueAndMaxTrailingZerosIsZero, double base_Scale, double base_HasScale, double base_HasSeparator);

		// Token: 0x0600F80F RID: 63503
		double numberFormatLiteral(double bias_numberFormatLiteral);

		// Token: 0x0600F810 RID: 63504
		double outputDtFormat(double bias_outputDtFormat, double base_TimeBeforeDate, double base_PeriodWithFullHour, double base_HasOneDecimalPoint, double base_ConstantLength, double base_DigitConstantLength, double base_SeparatorKindMatches, double base_SeparatorKindMisMatches, double base_UnlikelySeparatorCount, double base_SeparatorCount, double base_HasNonDelimitedNumbers, double base_IsNumeric, double base_MinDateInversions, double base_MinTimeInversions, double base_IsMatchingCommonDatePartsOrders, double base_DatePartOrderCount, double base_IsMatchingCommonTimePartsOrders, double base_TimePartOrderCount, double base_VariableLengthCount, double base_MatchedPartsCount, double base_HasDayOfWeekInMonth, double base_TimeAndDateShareSeparator, double base_BetweenTimeDateSeparatorReused);

		// Token: 0x0600F811 RID: 63505
		double inputDtFormats(double bias_inputDtFormats, double base_MaxScore, double base_MinScore, double base_AverageScore, double base_NumFormats, double base_AnyAmbiguous, double base_MatchedDifferentParts);

		// Token: 0x0600F812 RID: 63506
		double lookupDictionary(double bias_lookupDictionary);

		// Token: 0x0600F813 RID: 63507
		double idx(double bias_idx);

		// Token: 0x0600F814 RID: 63508
		double columnIdx(double bias_columnIdx);

		// Token: 0x0600F815 RID: 63509
		double vs(double bias_vs);
	}
}
