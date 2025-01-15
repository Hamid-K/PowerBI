using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Utils;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200004D RID: 77
	internal static class DataShapeGenerationMessagePhrases
	{
		// Token: 0x06000289 RID: 649 RVA: 0x0000AC9B File Offset: 0x00008E9B
		internal static DataShapeGenerationMessagePhrase InvalidProperty(string property)
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("Invalid property '{0}'.", property));
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000ACAD File Offset: 0x00008EAD
		internal static DataShapeGenerationMessagePhrase InvalidCombinationOfProperties(string property1, string property2)
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("Invalid combination of properties '{0}-{1}'.", property1, property2));
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000ACC0 File Offset: 0x00008EC0
		internal static DataShapeGenerationMessagePhrase MissingRequiredProperty(string property)
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("Missing required property '{0}'.", property));
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000ACD2 File Offset: 0x00008ED2
		internal static DataShapeGenerationMessagePhrase ReduceDataWindowToDefault(int defaultValue)
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("The data window size will be reduced to its default of '{0}'.", defaultValue));
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000ACE9 File Offset: 0x00008EE9
		internal static DataShapeGenerationMessagePhrase ReduceLimitToAllowedMaximum(int maximum)
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("The limit will be reduced to its allowed maximum of '{0}'.", maximum));
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000AD00 File Offset: 0x00008F00
		internal static DataShapeGenerationMessagePhrase ReduceLimitToDefault(int defaultValue)
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("The limit will be reduced to its default of '{0}'.", defaultValue));
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000AD17 File Offset: 0x00008F17
		internal static DataShapeGenerationMessagePhrase ReduceLimitTo(int value)
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("The limit will be reduced to '{0}'.", value));
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000AD2E File Offset: 0x00008F2E
		internal static DataShapeGenerationMessagePhrase InvalidDataReductionInIntersection(string algorithmName)
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("The query contains an invalid use of {0}. When {0} is used as the DataReduction Intersection algorithm and the Scoped is null or empty, it must not be combined with either a Primary or a Secondary algorithm.", algorithmName));
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000AD40 File Offset: 0x00008F40
		internal static DataShapeGenerationMessagePhrase InvalidDataReductionInPrimary(string algorithmName)
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("The query contains an invalid use of {0}. When {0} is used as the DataReduction Primary algorithm and the Scoped is null or empty, it must not be combined with either a Secondary or an Intersection algorithm.", algorithmName));
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000AD52 File Offset: 0x00008F52
		internal static DataShapeGenerationMessagePhrase InvalidDataReductionInPrimaryWithSecondaryAxis(string algorithmName)
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("The query contains an invalid use of {0}. When {0} is used as the DataReduction Primary algorithm, it must not be combined with a secondary axis.", algorithmName));
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000AD64 File Offset: 0x00008F64
		internal static DataShapeGenerationMessagePhrase InvalidDataReductionInSecondary(string algorithmName)
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("The query contains an invalid use of {0}. {0} can not be used as the DataReduction Secondary algorithm.", algorithmName));
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000AD76 File Offset: 0x00008F76
		internal static DataShapeGenerationMessagePhrase InvalidBinnedLineSampleDataReductionWithNoPrimaryProjections()
		{
			return new DataShapeGenerationMessagePhrase("The query contains an invalid use of BinnedLineSample. When BinnedLineSample is used as the DataReduction algorithm, the Primary hierarchy must have at least one projection.");
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000AD82 File Offset: 0x00008F82
		internal static DataShapeGenerationMessagePhrase InvalidDataReductionInIntersectionWithNoSecondarySeries(string algorithmName)
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("The query contains an invalid use of {0}. When {0} is used as the DataReduction algorithm, the Secondary hierarchy must have at least one dynamic series.", algorithmName));
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000AD94 File Offset: 0x00008F94
		internal static DataShapeGenerationMessagePhrase FilteredEvalUnsupportedAsInnerExpression()
		{
			return new DataShapeGenerationMessagePhrase("FilteredEval has to be a top level expression; FilteredEvals are not supported as inner expressions.");
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000ADA0 File Offset: 0x00008FA0
		internal static DataShapeGenerationMessagePhrase FilteredEvalExpressionInUnsupportedSQClause()
		{
			return new DataShapeGenerationMessagePhrase("FilteredEval can only be used in Semantic Query's Select clause; FilteredEvals are not supported in other Semantic Query clauses.");
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000ADAC File Offset: 0x00008FAC
		internal static DataShapeGenerationMessagePhrase InvalidFilteredEvalMeasure()
		{
			return new DataShapeGenerationMessagePhrase("FilteredEval's inner expression has to be a scalar expression, eligible to be a measure on its own. Other expressions or nested FilteredEval are not allowed.");
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000ADB8 File Offset: 0x00008FB8
		internal static DataShapeGenerationMessagePhrase InvalidFilteredEvalFilter()
		{
			return new DataShapeGenerationMessagePhrase("FilteredEval's filters can not reference any subquery columns in their conditions.");
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000ADC4 File Offset: 0x00008FC4
		internal static DataShapeGenerationMessagePhrase InvalidExpansionLevel(int index)
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("The expansion level at index {0} is invalid.", index));
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000ADDB File Offset: 0x00008FDB
		internal static DataShapeGenerationMessagePhrase MissingExpansionLevels()
		{
			return new DataShapeGenerationMessagePhrase("There should be at least 1 expansion level defined.");
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000ADE7 File Offset: 0x00008FE7
		internal static DataShapeGenerationMessagePhrase MissingExpansionInstances()
		{
			return new DataShapeGenerationMessagePhrase("Instances should be defined on ExpansionState.");
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000ADF3 File Offset: 0x00008FF3
		internal static DataShapeGenerationMessagePhrase EmptyExpansionInstances()
		{
			return new DataShapeGenerationMessagePhrase("Instances should not be empty on ExpansionState.");
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000ADFF File Offset: 0x00008FFF
		internal static DataShapeGenerationMessagePhrase ModelReferenceNotAllowed()
		{
			return new DataShapeGenerationMessagePhrase("A model reference was used. Model references are not allowed in this context.");
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000AE0B File Offset: 0x0000900B
		internal static DataShapeGenerationMessagePhrase SubqueryReferenceNotAllowed()
		{
			return new DataShapeGenerationMessagePhrase("A subquery reference was used. Subquery references are not allowed in this context.");
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000AE17 File Offset: 0x00009017
		internal static DataShapeGenerationMessagePhrase ScopedEvalNotAllowed()
		{
			return new DataShapeGenerationMessagePhrase("A scoped eval was used. Scoped evaluations are not allowed in this context.");
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000AE23 File Offset: 0x00009023
		internal static DataShapeGenerationMessagePhrase MedianNotAllowed()
		{
			return new DataShapeGenerationMessagePhrase("A Median aggregation was used. Median aggregation is not allowed in this context.");
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000AE2F File Offset: 0x0000902F
		internal static DataShapeGenerationMessagePhrase PercentileNotAllowed()
		{
			return new DataShapeGenerationMessagePhrase("A Percentile was used. Percentile is not allowed in this context.");
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000AE3B File Offset: 0x0000903B
		internal static DataShapeGenerationMessagePhrase SparklineDataNotAllowed()
		{
			return new DataShapeGenerationMessagePhrase("A Sparkline Data was used. Sparkline Data is not allowed in this context.");
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000AE47 File Offset: 0x00009047
		internal static DataShapeGenerationMessagePhrase NativeVisualCalculationNotAllowed()
		{
			return new DataShapeGenerationMessagePhrase("A Native Visual Calculation was used. Native Visual Calculations are not allowed in this context");
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000AE53 File Offset: 0x00009053
		internal static DataShapeGenerationMessagePhrase GroupByAggregateNotAllowed()
		{
			return new DataShapeGenerationMessagePhrase("A GroupBy aggregate function was used. GroupBy aggregate functions are not allowed in this context.");
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000AE5F File Offset: 0x0000905F
		internal static DataShapeGenerationMessagePhrase InvalidDataReductionWindowExpansionState(string expansionState)
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("The query contains an invalid use of expansion state: {0}. All levels are expected to be collapsed.", expansionState));
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000AE71 File Offset: 0x00009071
		internal static DataShapeGenerationMessagePhrase InvalidDataReductionWithNoProperty(string expansionState, string property)
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("The query contains invalid use of {0} with no {1}.", expansionState, property));
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000AE84 File Offset: 0x00009084
		internal static DataShapeGenerationMessagePhrase MismatchedValuesCountWithLevelExpressionsCount(string property, int valueCount, int levelExpressionsCount)
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("The query contains invalid use of {0} with value counts {1} mismatches the expected level expressions count {2}.", property, valueCount, levelExpressionsCount));
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000AEA2 File Offset: 0x000090A2
		internal static DataShapeGenerationMessagePhrase InvalidDataReductionWithUnexpectedValues(string property)
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("The query contains invalid use of {0}. The value should not exist.", property));
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000AEB4 File Offset: 0x000090B4
		internal static DataShapeGenerationMessagePhrase MismatchedAxisGroupingWithLevelsCount(int levelCount, int groupingCount)
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("The query contains {0} levels in expansions but contains {1} groupings on axis. Expect the level count to be no more than than the groupings on axis count.", levelCount, groupingCount));
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000AED1 File Offset: 0x000090D1
		internal static DataShapeGenerationMessagePhrase TooManyWindowExpansionInstanceWithLevel()
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("The query contains too many window expansion instances compared to the given expansion levels.", Array.Empty<object>()));
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000AEE7 File Offset: 0x000090E7
		internal static DataShapeGenerationMessagePhrase NoLevelInfoWithMoreThanOneLevelOfExpansion()
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("The window expansion state must have level information if there is more than one level of expansion.", Array.Empty<object>()));
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000AEFD File Offset: 0x000090FD
		internal static DataShapeGenerationMessagePhrase UnsupportedScopedAggregates()
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("Either the model capabilities do not support scoped aggregates or the feature switch is off.", Array.Empty<object>()));
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000AF13 File Offset: 0x00009113
		internal static DataShapeGenerationMessagePhrase UnsupportedScopedAggregatesOnLastGroupingMember()
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("The binding contains aggregates on the last grouping of the axis, aggregates are allowed on any grouping except the last.", Array.Empty<object>()));
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000AF29 File Offset: 0x00009129
		internal static DataShapeGenerationMessagePhrase UnsupportedScopedAggregatesWithNoTotals()
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("Scoped aggregates are supported with Subtotals only.", Array.Empty<object>()));
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000AF3F File Offset: 0x0000913F
		internal static DataShapeGenerationMessagePhrase UnsupportedScopedAggregatesNotOnDataShapeOrPrimaryAxis()
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("Scoped aggregates are supported on DataShape or primary axis only.", Array.Empty<object>()));
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000AF55 File Offset: 0x00009155
		internal static DataShapeGenerationMessagePhrase UnsupportedScopedAggregatesWithSecondaryAxis()
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("Scoped aggregates are not supported when secondary axis exist.", Array.Empty<object>()));
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000AF6B File Offset: 0x0000916B
		internal static DataShapeGenerationMessagePhrase UnsupportedScopedAggregatesWithSecondaryDepth()
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("Scoped aggregates are not supported with scopes that have secondary depth.", Array.Empty<object>()));
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000AF81 File Offset: 0x00009181
		internal static DataShapeGenerationMessagePhrase UnsupportedScopedAggregatesWithoutRespectingInstanceFilters()
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("Scoped aggregates are supported with RespectInstanceFilters only.", Array.Empty<object>()));
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000AF97 File Offset: 0x00009197
		internal static DataShapeGenerationMessagePhrase UnsupportedAggregatesForScopedAggregates()
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("Median and Percentile aggregates are not allowed with scoped aggregates.", Array.Empty<object>()));
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000AFAD File Offset: 0x000091AD
		internal static DataShapeGenerationMessagePhrase InvalidScopedAggregatePrimaryDepth()
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("The primary depth of the scoped aggregate is invalid, a valid scope primary depth should be within the range of the primary groupings, not smaller than 1 and should be larger than the grouping index of the grouping that contains the scoped aggregate.", Array.Empty<object>()));
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000AFC3 File Offset: 0x000091C3
		internal static DataShapeGenerationMessagePhrase UnsupportedScopedAggregatesWithNonAggregatableColumn()
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("Scoped aggregates are not supported with Non-Aggregatable columns.", Array.Empty<object>()));
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000AFD9 File Offset: 0x000091D9
		internal static DataShapeGenerationMessagePhrase UnsupportedScopedAggregatesWithShowAll()
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("Scoped aggregates are not supported with Show items with no data.", Array.Empty<object>()));
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000AFEF File Offset: 0x000091EF
		internal static DataShapeGenerationMessagePhrase UnsupportedWhereFilterWithSubqueriesAggregation(int filterIndex, List<string> subqueriesNames)
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("Invalid filter at index {0}. It contains references to subqueries: {1}. Only one subquery referenced in a filter is allowed.", filterIndex, subqueriesNames.StringJoin(", ").MarkAsModelInfo()));
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000B016 File Offset: 0x00009216
		internal static DataShapeGenerationMessagePhrase UnsupportedGroupSynchronizationIndex(int index)
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("The Group Synchronization index is out of supported range: {0}.", index));
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000B02D File Offset: 0x0000922D
		internal static DataShapeGenerationMessagePhrase UnsupportedGroupSynchronizationIndices(IList<int> indices)
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("The Group Synchronization indices are not contiguous {0}. Only contiguous grouping indices are allowed.", indices.StringJoin(", ")));
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000B049 File Offset: 0x00009249
		internal static DataShapeGenerationMessagePhrase UnsupportedNumberOfSynchronizationBlocks(int numOfSyncBlocks, int maxNumberOfSyncBlocksAllowed)
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("The Axis Synchronization contains {0} Synchronization Blocks. The maximum number of Synchronization Blocks is {1}.", numOfSyncBlocks, maxNumberOfSyncBlocksAllowed));
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000B066 File Offset: 0x00009266
		internal static DataShapeGenerationMessagePhrase UnsupportedGroupSynchronizationNumberOfGroups(int numOfGroups, int maxNumberOfGroupsAllowed)
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("The Group Synchronization contains {0} groups. The maximum number of groups in a Group Synchronization is {1}.", numOfGroups, maxNumberOfGroupsAllowed));
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000B083 File Offset: 0x00009283
		internal static DataShapeGenerationMessagePhrase UnsupportedGroupSynchronizationWithTotals()
		{
			return new DataShapeGenerationMessagePhrase(StringUtil.FormatInvariant("Group Synchronization is not supported on groups with Subtotals.", Array.Empty<object>()));
		}
	}
}
