using System;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000197 RID: 407
	internal static class PlanNames
	{
		// Token: 0x06000DE4 RID: 3556 RVA: 0x00038A55 File Offset: 0x00036C55
		internal static string Body(Identifier dataShapeId)
		{
			return dataShapeId.Value + "Body";
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x00038A67 File Offset: 0x00036C67
		internal static string BodyWithLimitedPrimary(Identifier dataShapeId)
		{
			return dataShapeId.Value + "BodyWithLimitedPrimary";
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x00038A79 File Offset: 0x00036C79
		internal static string BodyWithLimitedSecondary(Identifier dataShapeId)
		{
			return dataShapeId.Value + "BodyWithLimitedSecondary";
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x00038A8B File Offset: 0x00036C8B
		internal static string BodyLimited(Identifier dataShapeId)
		{
			return dataShapeId.Value + "BodyLimited";
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x00038A9D File Offset: 0x00036C9D
		internal static string BodyWithKeyPoints(Identifier dataShapeId)
		{
			return dataShapeId.Value + "BodyWithKeyPoints";
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x00038AAF File Offset: 0x00036CAF
		internal static string BodyRegroupedTo(Identifier dataShapeId, Identifier memberId)
		{
			return dataShapeId.Value + "BodyRegroupedTo" + memberId.Value;
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00038AC7 File Offset: 0x00036CC7
		internal static string BodyRegroupedToLimited(Identifier dataShapeId, Identifier memberId)
		{
			return dataShapeId.Value + "BodyRegroupedTo" + memberId.Value + "Limited";
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00038AE4 File Offset: 0x00036CE4
		internal static string BodyRegroupedToScopedLimited(Identifier dataShapeId, Identifier memberId, Identifier limitId)
		{
			return string.Concat(new string[] { dataShapeId.Value, "BodyRegroupedTo", memberId.Value, "ScopedLimited", limitId.Value });
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x00038B1C File Offset: 0x00036D1C
		internal static string Aggregates(Identifier dataShapeId)
		{
			return dataShapeId.Value + "Aggregates";
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x00038B2E File Offset: 0x00036D2E
		internal static string LimitMetadata(Identifier dataShapeId)
		{
			return dataShapeId.Value + "LimitMetadata";
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x00038B40 File Offset: 0x00036D40
		internal static string KeyValues(Identifier dataShapeId)
		{
			return dataShapeId.Value + "KeyValues";
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x00038B52 File Offset: 0x00036D52
		internal static string KeyPoints(Identifier dataShapeId)
		{
			return dataShapeId.Value + "KeyPoints";
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x00038B64 File Offset: 0x00036D64
		internal static string KeyPointsLimitedCount(Identifier dataShapeId)
		{
			return dataShapeId.Value + "KeyPointsLimitedCount";
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x00038B76 File Offset: 0x00036D76
		internal static string KeyPrimaryLimitedCount(Identifier dataShapeId)
		{
			return dataShapeId.Value + "KeyPrimaryLimitedCount";
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00038B88 File Offset: 0x00036D88
		internal static string KeyPrimaryLimit(Identifier dataShapeId)
		{
			return dataShapeId.Value + "KeyPrimaryLimit";
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x00038B9A File Offset: 0x00036D9A
		internal static string KeyPrimaryValuesLimited(Identifier dataShapeId)
		{
			return dataShapeId.Value + "KeyPrimaryValuesLimited";
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x00038BAC File Offset: 0x00036DAC
		internal static string KeyPointsLimited(Identifier dataShapeId)
		{
			return dataShapeId.Value + "KeyPointsLimited";
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x00038BBE File Offset: 0x00036DBE
		internal static string ValueFilter(Identifier filterTargetId)
		{
			return "ValueFilter" + filterTargetId.Value;
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x00038BD0 File Offset: 0x00036DD0
		internal static string ValueFilterConstraint(Identifier filterTargetId)
		{
			return "ValueFilterConstraint" + filterTargetId.Value;
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x00038BE2 File Offset: 0x00036DE2
		internal static string ApplyFilter(Identifier applyFilterDataShapeId)
		{
			return "ApplyFilter" + applyFilterDataShapeId.Value;
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x00038BF4 File Offset: 0x00036DF4
		internal static string ExistsFilter(string name)
		{
			return "ExistsFilter" + name;
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00038C01 File Offset: 0x00036E01
		internal static string Core(Identifier dataShapeId)
		{
			return dataShapeId.Value + "Core";
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00038C13 File Offset: 0x00036E13
		internal static string ScopedCore(Identifier scopedCoreTableScopeId)
		{
			return "ScopedCore" + scopedCoreTableScopeId.Value;
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00038C25 File Offset: 0x00036E25
		internal static string ForSync(Identifier memberId, int sortKeyIndex)
		{
			return StringUtil.FormatInvariant("{0}SortKey{1}ForSync", new object[] { memberId.Value, sortKeyIndex });
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x00038C49 File Offset: 0x00036E49
		internal static string ScopedLimit(Identifier dataShapeId, Identifier limitId)
		{
			return dataShapeId.Value + "ScopedLimit" + limitId.Value;
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x00038C61 File Offset: 0x00036E61
		internal static string ScopedPreLimit(Identifier dataShapeId, Identifier limitId)
		{
			return dataShapeId.Value + "ScopedPreLimit" + limitId.Value;
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x00038C79 File Offset: 0x00036E79
		internal static string ScopedPreLimitCount(Identifier dataShapeId, Identifier limitId)
		{
			return dataShapeId.Value + "ScopedPreLimit" + limitId.Value + "Count";
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00038C96 File Offset: 0x00036E96
		internal static string ScopedPreLimitIsExceededCount(Identifier limitId)
		{
			return "ScopedPreLimitIsExceeded" + limitId.Value + "Count";
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x00038CAD File Offset: 0x00036EAD
		internal static string PreLimitPostRestart(Identifier limitId)
		{
			return "PreLimitPostRestart" + limitId.Value;
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x00038CBF File Offset: 0x00036EBF
		internal static string ScopedLimitCount(Identifier dataShapeId, Identifier limitId)
		{
			return dataShapeId.Value + "ScopedLimit" + limitId.Value + "Count";
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x00038CDC File Offset: 0x00036EDC
		internal static string CoreOnlyOutputTotals(Identifier dataShapeId)
		{
			return dataShapeId.Value + "CoreOnlyOutputTotals";
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00038CEE File Offset: 0x00036EEE
		internal static string CoreNoInstanceFiltersNoTotals(Identifier dataShapeId)
		{
			return dataShapeId.Value + "CoreNoInstanceFiltersNoTotals";
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x00038D00 File Offset: 0x00036F00
		internal static string CoreNoInstanceFilters(Identifier dataShapeId)
		{
			return dataShapeId.Value + "CoreNoInstanceFilters";
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x00038D12 File Offset: 0x00036F12
		internal static string CoreWithInstanceFilters(Identifier dataShapeId)
		{
			return dataShapeId.Value + "CoreWithInstanceFilters";
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x00038D24 File Offset: 0x00036F24
		internal static string Secondary(Identifier dataShapeId)
		{
			return dataShapeId.Value + "Secondary";
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x00038D36 File Offset: 0x00036F36
		internal static string SecondaryLimited(Identifier dataShapeId)
		{
			return dataShapeId.Value + "SecondaryLimited";
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x00038D48 File Offset: 0x00036F48
		internal static string SecondaryBase(Identifier dataShapeId)
		{
			return dataShapeId.Value + "SecondaryBase";
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00038D5A File Offset: 0x00036F5A
		internal static string SecondaryRegroupedTo(Identifier dataShapeId, Identifier memberId)
		{
			return dataShapeId.Value + "SecondaryRegroupedTo" + memberId.Value;
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00038D72 File Offset: 0x00036F72
		internal static string SecondaryRegroupedToLimited(Identifier dataShapeId, Identifier memberId)
		{
			return dataShapeId.Value + "SecondaryRegroupedTo" + memberId.Value + "Limited";
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00038D8F File Offset: 0x00036F8F
		internal static string SecondaryPreShowAll(Identifier dataShapeId)
		{
			return dataShapeId.Value + "SecondaryPreShowAll";
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x00038DA1 File Offset: 0x00036FA1
		internal static string SecondaryShowAll(Identifier dataShapeId)
		{
			return dataShapeId.Value + "SecondaryShowAll";
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x00038DB3 File Offset: 0x00036FB3
		internal static string SecondaryShowAllInstanceFilters(Identifier dataShapeId)
		{
			return dataShapeId.Value + "SecondaryShowAllInstanceFilters";
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00038DC5 File Offset: 0x00036FC5
		internal static string SecondaryShowAllNoTotals(Identifier dataShapeId)
		{
			return dataShapeId.Value + "SecondaryShowAllNoTotals";
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x00038DD7 File Offset: 0x00036FD7
		internal static string Primary(Identifier dataShapeId)
		{
			return dataShapeId.Value + "Primary";
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x00038DE9 File Offset: 0x00036FE9
		internal static string PrimaryBase(Identifier dataShapeId)
		{
			return dataShapeId.Value + "PrimaryBase";
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x00038DFB File Offset: 0x00036FFB
		internal static string PrimaryLimited(Identifier dataShapeId)
		{
			return dataShapeId.Value + "PrimaryLimited";
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x00038E0D File Offset: 0x0003700D
		internal static string PrimaryRegroupedTo(Identifier dataShapeId, Identifier memberId)
		{
			return dataShapeId.Value + "PrimaryRegroupedTo" + memberId.Value;
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x00038E25 File Offset: 0x00037025
		internal static string PrimaryRegroupedToLimited(Identifier dataShapeId, Identifier memberId)
		{
			return dataShapeId.Value + "PrimaryRegroupedTo" + memberId.Value + "Limited";
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x00038E42 File Offset: 0x00037042
		internal static string Restored(Identifier scope)
		{
			return scope.Value + "Restored";
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00038E54 File Offset: 0x00037054
		internal static string PrimaryShowAll(Identifier dataShapeId)
		{
			return dataShapeId.Value + "PrimaryShowAll";
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x00038E66 File Offset: 0x00037066
		internal static string PrimaryShowAllInstanceFilters(Identifier dataShapeId)
		{
			return dataShapeId.Value + "PrimaryShowAllInstanceFilters";
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00038E78 File Offset: 0x00037078
		internal static string PrimaryShowAllNoTotals(Identifier dataShapeId)
		{
			return dataShapeId.Value + "PrimaryShowAllNoTotals";
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x00038E8A File Offset: 0x0003708A
		internal static string PrimaryKeyPoints(Identifier dataShapeId)
		{
			return dataShapeId.Value + "PrimaryKeyPoints";
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x00038E9C File Offset: 0x0003709C
		internal static string PrimaryWindowed(Identifier dataShapeId)
		{
			return dataShapeId.Value + "PrimaryWindowed";
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00038EAE File Offset: 0x000370AE
		internal static string SortByMeasureTable(Identifier dataShapeId, Identifier memberId)
		{
			return dataShapeId.Value + "CoreTableBy" + memberId.Value;
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00038EC6 File Offset: 0x000370C6
		internal static string PrimaryWithSortColumns(Identifier dataShapeId)
		{
			return dataShapeId.Value + "PrimaryWithSortColumns";
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00038ED8 File Offset: 0x000370D8
		internal static string PrimaryWithSortColumnsOutputTotals(Identifier dataShapeId)
		{
			return dataShapeId.Value + "PrimaryWithSortColumnsOutputTotals";
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00038EEA File Offset: 0x000370EA
		internal static string SecondaryWithSortColumns(Identifier dataShapeId)
		{
			return dataShapeId.Value + "SecondaryWithSortColumns";
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x00038EFC File Offset: 0x000370FC
		internal static string SecondaryWithSortColumnsOutputTotals(Identifier dataShapeId)
		{
			return dataShapeId.Value + "SecondaryWithSortColumnsOutputTotals";
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00038F10 File Offset: 0x00037110
		internal static string SortByMeasureColumn(SortKey sortKey, DataMember member, int keyIndex)
		{
			if (sortKey.Id != null)
			{
				return "SortBy_" + sortKey.Id.Value;
			}
			return "SortBy_" + member.Id.Value + "_" + keyIndex.ToString();
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00038F62 File Offset: 0x00037162
		internal static string PrimaryWithScopedAggregates(Identifier dataShapeId)
		{
			return dataShapeId.Value + "PrimaryWithScopedAggregates";
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00038F74 File Offset: 0x00037174
		internal static string ScopedAggregatesTable(Identifier dataShapeId, Identifier scopeId)
		{
			return "ScopedAggregatesFor" + dataShapeId.Value + "By" + scopeId.Value;
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00038F91 File Offset: 0x00037191
		internal static string FullOuterCrossJoinTable(Identifier dataShapeId, int index)
		{
			return "FullOuterCrossJoinTable" + dataShapeId.Value + "_" + index.ToString();
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00038FAF File Offset: 0x000371AF
		internal static string ScopedAggregateReferenceTable(Identifier scopeId, bool respectInstanceFilters)
		{
			return "ScopedAggregateReferenceTable" + scopeId.Value + (respectInstanceFilters ? "WithInstanceFilters" : "WithoutInstanceFilters");
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x00038FD0 File Offset: 0x000371D0
		internal static string Column(string columnName)
		{
			return "Column(" + columnName + ")";
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x00038FE2 File Offset: 0x000371E2
		internal static string SingleValueTable(string name)
		{
			return "SingleValue" + name;
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x00038FEF File Offset: 0x000371EF
		internal static string Argument(string name)
		{
			return "Argument" + name;
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00038FFC File Offset: 0x000371FC
		internal static string FilterTable(Identifier dataShapeId, int? index = null)
		{
			return dataShapeId.Value + "FilterTable" + ((index == null) ? "" : ("_" + index.Value.ToString()));
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00039042 File Offset: 0x00037242
		internal static string SubqueryTable(string name)
		{
			return "SubqueryTable" + name;
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x0003904F File Offset: 0x0003724F
		internal static string SubqueryTableIn(string name, string referencedInName)
		{
			return "SubqueryTable" + name + "In" + referencedInName;
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x00039062 File Offset: 0x00037262
		internal static string SubqueryTableProjectedIn(string name, string referencedInName)
		{
			return "SubqueryTable" + name + "ProjectedIn" + referencedInName;
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x00039075 File Offset: 0x00037275
		internal static string BodyBinnedSample(Identifier dataShapeId)
		{
			return dataShapeId.Value + "BodyBinnedSample";
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x00039087 File Offset: 0x00037287
		internal static string BodyOverlappingPointsSample(Identifier dataShapeId)
		{
			return dataShapeId.Value + "BodyOverlappingPointsSample";
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x00039099 File Offset: 0x00037299
		internal static string BodyTopNPerLevel(Identifier dataShapeId)
		{
			return dataShapeId.Value + "BodyTopNPerLevel";
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x000390AB File Offset: 0x000372AB
		internal static string DetailTable(Identifier dataShapeId)
		{
			return dataShapeId.Value + "DetailTable";
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x000390BD File Offset: 0x000372BD
		internal static string IntersectionCount(Identifier dataShapeId)
		{
			return dataShapeId.Value + "IntersectionCount";
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x000390CF File Offset: 0x000372CF
		internal static string TargetIntersectionCount(Identifier dataShapeId)
		{
			return dataShapeId.Value + "TargetIntersectionCount";
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x000390E1 File Offset: 0x000372E1
		internal static string PrimaryDbCount(Identifier dataShapeId)
		{
			return dataShapeId.Value + "PrimaryDbCount";
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x000390F3 File Offset: 0x000372F3
		internal static string PrimaryCount(Identifier dataShapeId)
		{
			return dataShapeId.Value + "PrimaryCount";
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x00039105 File Offset: 0x00037305
		internal static string InitTargetPrimaryCount(Identifier dataShapeId)
		{
			return dataShapeId.Value + "InitTargetPrimaryCount";
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x00039117 File Offset: 0x00037317
		internal static string TargetPrimaryCount(Identifier dataShapeId)
		{
			return dataShapeId.Value + "TargetPrimaryCount";
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00039129 File Offset: 0x00037329
		internal static string Max(Identifier column)
		{
			return column.Value + "Max";
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0003913B File Offset: 0x0003733B
		internal static string MinPrimaryCount(Identifier dataShapeId)
		{
			return dataShapeId.Value + "MinPrimaryCount";
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0003914D File Offset: 0x0003734D
		internal static string Min(Identifier column)
		{
			return column.Value + "Min";
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0003915F File Offset: 0x0003735F
		internal static string MinMax(Identifier column)
		{
			return column.Value + "MinMax";
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x00039171 File Offset: 0x00037371
		internal static string SecondaryCount(Identifier dataShapeId)
		{
			return dataShapeId.Value + "SecondaryCount";
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00039183 File Offset: 0x00037383
		internal static string SecondaryDbCount(Identifier dataShapeId)
		{
			return dataShapeId.Value + "SecondaryDbCount";
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x00039195 File Offset: 0x00037395
		internal static string InitTargetSecondaryCount(Identifier dataShapeId)
		{
			return dataShapeId.Value + "InitTargetSecondaryCount";
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x000391A7 File Offset: 0x000373A7
		internal static string TargetSecondaryCount(Identifier dataShapeId)
		{
			return dataShapeId.Value + "TargetSecondaryCount";
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x000391B9 File Offset: 0x000373B9
		internal static string MinSecondaryCount(Identifier dataShapeId)
		{
			return dataShapeId.Value + "MinSecondaryCount";
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x000391CB File Offset: 0x000373CB
		internal static string SparseFactor(Identifier dataShapeId)
		{
			return dataShapeId.Value + "SparseFactor";
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x000391DD File Offset: 0x000373DD
		internal static string SpaceCount(Identifier dataShapeId)
		{
			return dataShapeId.Value + "SpaceCount";
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x000391EF File Offset: 0x000373EF
		internal static string DataTransform(Identifier transformId)
		{
			return "DataTransform" + transformId.Value;
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x00039201 File Offset: 0x00037401
		internal static string CanApplyLog(Identifier input)
		{
			return input.Value + "CanApplyLog";
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x00039213 File Offset: 0x00037413
		internal static string CanApplyNegativeLog(Identifier input)
		{
			return input.Value + "CanApplyNegativeLog";
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00039225 File Offset: 0x00037425
		internal static string TransformApplied(string axis)
		{
			return axis + "TransformApplied";
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00039232 File Offset: 0x00037432
		internal static string Reordered(string name)
		{
			return name + "Reordered";
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x0003923F File Offset: 0x0003743F
		internal static string ShowAllCompat(string name)
		{
			return name + "ShowAllCompat";
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x0003924C File Offset: 0x0003744C
		internal static string IsValid(string name)
		{
			return "Is" + name + "Valid";
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0003925E File Offset: 0x0003745E
		internal static string LimitBlockDbCount(Identifier dataShapeId, int index)
		{
			return dataShapeId.Value + "LimitBlock" + index.ToString() + "DbCount";
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0003927C File Offset: 0x0003747C
		internal static string LimitBlockMandatoryCount(Identifier dataShapeId, int index)
		{
			return dataShapeId.Value + "LimitBlock" + index.ToString() + "MandatoryCount";
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0003929A File Offset: 0x0003749A
		internal static string TotalMandatoryCount(Identifier dataShapeId)
		{
			return dataShapeId.Value + "TotalMandatoryCount";
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x000392AC File Offset: 0x000374AC
		internal static string LimitBlockCount(Identifier dataShapeId, int index)
		{
			return dataShapeId.Value + "LimitBlock" + index.ToString() + "Count";
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x000392CA File Offset: 0x000374CA
		internal static string RemainingCapacityAtStart(Identifier dataShapeId)
		{
			return dataShapeId.Value + "RemainingCapacityAtStart";
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x000392DC File Offset: 0x000374DC
		internal static string RemainingCapacityAfterBlock(Identifier dataShapeId, int index)
		{
			return dataShapeId.Value + "RemainingCapacityAfterBlock" + index.ToString() + "Count";
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x000392FA File Offset: 0x000374FA
		internal static string LimitBlockLimitCount(Identifier dataShapeId, int index)
		{
			return dataShapeId.Value + "LimitBlock" + index.ToString() + "LimitCount";
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00039318 File Offset: 0x00037518
		internal static string Synchronized(Identifier dataShapeId, Identifier syncDataShapeId)
		{
			return syncDataShapeId.Value + "_" + dataShapeId.Value + "Synchronized";
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x00039335 File Offset: 0x00037535
		internal static string SyncTable(Identifier dataShapeId)
		{
			return dataShapeId.Value + "SyncTable";
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x00039347 File Offset: 0x00037547
		internal static string VisualCalcs(Identifier dataShapeId)
		{
			return dataShapeId.Value + "VisualCalcs";
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x00039359 File Offset: 0x00037559
		internal static string VisualCalcsInput(Identifier dataShapeId)
		{
			return dataShapeId.Value + "VisualCalcsInput";
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x0003936B File Offset: 0x0003756B
		internal static string RemoveEmptyDensified(Identifier dataShapeId)
		{
			return dataShapeId.Value + "RemoveEmptyDensified";
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x0003937D File Offset: 0x0003757D
		internal static string RemoveContextOnlyColumns(Identifier dataShapeId)
		{
			return dataShapeId.Value + "RemoveContextOnlyColumns";
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x0003938F File Offset: 0x0003758F
		internal static string RemoveContextOnlyLevels(Identifier dataShapeId)
		{
			return dataShapeId.Value + "RemoveContextOnlyLevels";
		}

		// Token: 0x040006CC RID: 1740
		internal const string PlaceholderColumnName = "Placeholder";

		// Token: 0x040006CD RID: 1741
		internal static readonly string ColumnIndex = "ColumnIndex";

		// Token: 0x040006CE RID: 1742
		internal static readonly string IsDensifiedRow = "IsDensifiedRow";
	}
}
