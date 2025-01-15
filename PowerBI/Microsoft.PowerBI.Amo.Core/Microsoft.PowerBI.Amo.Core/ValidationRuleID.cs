using System;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000D2 RID: 210
	internal enum ValidationRuleID
	{
		// Token: 0x0400075E RID: 1886
		LargePartitionWithNoAggs,
		// Token: 0x0400075F RID: 1887
		MeasureGroupHasLargePartitions,
		// Token: 0x04000760 RID: 1888
		MeasureGroupHasPartitionsToConsolidate,
		// Token: 0x04000761 RID: 1889
		AggregationDesignIsNotUsedByAnyPartition,
		// Token: 0x04000762 RID: 1890
		MeasureGroupHasTooManyAggregationDesigns,
		// Token: 0x04000763 RID: 1891
		IntermediateGranularityNotAggregated,
		// Token: 0x04000764 RID: 1892
		CubeAttributeAggregationUsageWithUnaryOperatorOrCustomRollup,
		// Token: 0x04000765 RID: 1893
		PartitionWithTooManyAggregations = 8,
		// Token: 0x04000766 RID: 1894
		AggregationHasRelatedAttributes,
		// Token: 0x04000767 RID: 1895
		Partition_RolapWithNoSlice,
		// Token: 0x04000768 RID: 1896
		Dimension_IsNotParentChildAndHasNoHierarchy,
		// Token: 0x04000769 RID: 1897
		HierarchyIsUnNatural,
		// Token: 0x0400076A RID: 1898
		DimensionIgnoresDuplicateKeys,
		// Token: 0x0400076B RID: 1899
		DimensionHasAttributeUsedByLevelsAndWithVisibleHierarchy,
		// Token: 0x0400076C RID: 1900
		DimensionHasMultipleNonAggregatableAttributes,
		// Token: 0x0400076D RID: 1901
		AggregationBelowGranularity,
		// Token: 0x0400076E RID: 1902
		ParentChildDimensionWithNonAggregatableAttribute,
		// Token: 0x0400076F RID: 1903
		NonAggregatableAttributeNeedsDefaultMember,
		// Token: 0x04000770 RID: 1904
		KeyAttributeOfParentChildHasVisibleHierarchy,
		// Token: 0x04000771 RID: 1905
		DimensionHasUnknownMemberSetToHidden,
		// Token: 0x04000772 RID: 1906
		AttributeIsLargeWithNonNumericKey,
		// Token: 0x04000773 RID: 1907
		NonKeyLargeAttributeWithVisibleHierarchy,
		// Token: 0x04000774 RID: 1908
		Dimension_RolapWithUnaryOperatorsOrCustomRollups,
		// Token: 0x04000775 RID: 1909
		AttributeTypeAccountOrTimeNeedsMatchingDimension,
		// Token: 0x04000776 RID: 1910
		AttributeTypeNeedsMatchingDimension,
		// Token: 0x04000777 RID: 1911
		DimensionTypeAccountOrTimeNeedsMatchingAttribute,
		// Token: 0x04000778 RID: 1912
		DimensionTypeNeedsMatchingAttribute,
		// Token: 0x04000779 RID: 1913
		AttributesTypesDontMatch,
		// Token: 0x0400077A RID: 1914
		LevelHasFewerMembersThanUpperLevel,
		// Token: 0x0400077B RID: 1915
		AggregationDesignWithNoEstimatedRows,
		// Token: 0x0400077C RID: 1916
		DimensionAndRelationshipTypes,
		// Token: 0x0400077D RID: 1917
		RedundantRelationship,
		// Token: 0x0400077E RID: 1918
		DiamondShapeRelationships,
		// Token: 0x0400077F RID: 1919
		AttributeRelationshipName,
		// Token: 0x04000780 RID: 1920
		DimensionWithPollingQuery,
		// Token: 0x04000781 RID: 1921
		NoTimeDimension,
		// Token: 0x04000782 RID: 1922
		TooManyParentChildDimsWithOutlineCalcs,
		// Token: 0x04000783 RID: 1923
		ParentChildDimensionWithLargeKey,
		// Token: 0x04000784 RID: 1924
		DimensionProcessByTable,
		// Token: 0x04000785 RID: 1925
		DatabaseHasTooManyDimensionsWithSingleAttribute,
		// Token: 0x04000786 RID: 1926
		DistinctCountMeasure,
		// Token: 0x04000787 RID: 1927
		ManyToManyHasLargeIntermediate,
		// Token: 0x04000788 RID: 1928
		CubeWithSingleDimension,
		// Token: 0x04000789 RID: 1929
		LinkedDimensionWithOutlineCalculations,
		// Token: 0x0400078A RID: 1930
		ReferencedMeasureGroupDimensionNotMaterialized,
		// Token: 0x0400078B RID: 1931
		IndependentMeasureGroup,
		// Token: 0x0400078C RID: 1932
		PartitionWithPollingQuery,
		// Token: 0x0400078D RID: 1933
		MeasureGroupsWithTheSameDimensionalityAndGranularity,
		// Token: 0x0400078E RID: 1934
		CubeHasTooManyMeasureGroups,
		// Token: 0x0400078F RID: 1935
		PerspectiveDefaultMeasureNotIncluded,
		// Token: 0x04000790 RID: 1936
		MeasureGroupWithSemiAdditiveMeasuresAndRolapDimension,
		// Token: 0x04000791 RID: 1937
		DotNetSqlClientProvider,
		// Token: 0x04000792 RID: 1938
		UnsupportedOledbProvider,
		// Token: 0x04000793 RID: 1939
		MeasureGroupWithNoPartitions,
		// Token: 0x04000794 RID: 1940
		PartitionIsRemoteRolap,
		// Token: 0x04000795 RID: 1941
		AggregationsForTimeGranularityWithSemiAdditiveMeasures,
		// Token: 0x04000796 RID: 1942
		AggregationsForTimeGranularityWithOnlySemiAdditiveMeasures,
		// Token: 0x04000797 RID: 1943
		AttributeRelationshipNamedDescription,
		// Token: 0x04000798 RID: 1944
		ErrorHigh = 300,
		// Token: 0x04000799 RID: 1945
		ErrorMedium,
		// Token: 0x0400079A RID: 1946
		NoDimensionsDefined,
		// Token: 0x0400079B RID: 1947
		MeasureGroupHasMoreThanOneDegenerateDimension,
		// Token: 0x0400079C RID: 1948
		MeasureGroupHasSemiadditiveMeasureWithNoTimeDimension
	}
}
