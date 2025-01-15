using System;
using System.Collections;
using System.Reflection;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000D1 RID: 209
	public sealed class ValidationRule
	{
		// Token: 0x06000990 RID: 2448 RVA: 0x0002A4B5 File Offset: 0x000286B5
		private ValidationRule(ValidationRuleID id, ValidationRuleType type, ValidationRulePriority priority, string category, string description, string helpID)
		{
			this.id = (int)id;
			this.type = type;
			this.priority = priority;
			this.category = category;
			this.description = description;
			this.helpID = helpID;
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0002A4EC File Offset: 0x000286EC
		static ValidationRule()
		{
			ArrayList arrayList = new ArrayList();
			foreach (FieldInfo fieldInfo in typeof(ValidationRule).GetFields(BindingFlags.Static | BindingFlags.NonPublic))
			{
				if (fieldInfo.FieldType == typeof(ValidationRule))
				{
					ValidationRule validationRule = (ValidationRule)fieldInfo.GetValue(null);
					if (validationRule != null && validationRule.type == ValidationRuleType.Warning)
					{
						arrayList.Add(validationRule);
					}
				}
			}
			ValidationRule.warnings = new ValidationRule.WarningCollection(arrayList);
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x0002ABF5 File Offset: 0x00028DF5
		public static ICollection Warnings
		{
			get
			{
				return ValidationRule.warnings;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x0002ABFC File Offset: 0x00028DFC
		public int ID
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x0002AC04 File Offset: 0x00028E04
		public ValidationRuleType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x0002AC0C File Offset: 0x00028E0C
		public ValidationRulePriority Priority
		{
			get
			{
				return this.priority;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x0002AC14 File Offset: 0x00028E14
		public string Category
		{
			get
			{
				return this.category;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x0002AC1C File Offset: 0x00028E1C
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x0002AC24 File Offset: 0x00028E24
		public string HelpID
		{
			get
			{
				return this.helpID;
			}
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0002AC2C File Offset: 0x00028E2C
		internal static ValidationRule Find(int id)
		{
			foreach (object obj in ((IEnumerable)ValidationRule.warnings))
			{
				ValidationRule validationRule = (ValidationRule)obj;
				if (validationRule.id == id)
				{
					return validationRule;
				}
			}
			return null;
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0002AC90 File Offset: 0x00028E90
		internal string GetDescription(string[] parameters)
		{
			switch (this.id)
			{
			case 0:
				return ValidationSR.Partition_IsLargeWithNoAggs2(500000L);
			case 1:
				return ValidationSR.MeasureGroup_HasLargePartitions(20, 250);
			case 2:
				return ValidationSR.MeasureGroup_HasPartitionsToConsolidate(5, 2, 50);
			case 3:
				return ValidationSR.AggregationDesign_NotUsedByAnyPartition2;
			case 4:
				return ValidationSR.MeasureGroup_HasTooManyAggregationDesigns;
			case 5:
				return ValidationSR.IntermediateGranularityNotAggregated2(parameters[0], parameters[1]);
			case 6:
				return ValidationSR.CubeAttributeAggregationUsageWithUnaryOperatorOrCustomRollup2;
			case 8:
				return ValidationSR.PartitionWithTooManyAggregations(500);
			case 9:
				return ValidationSR.AggregationHasRelatedAttributes2(parameters[0], parameters[1]);
			case 10:
				return ValidationSR.Partition_RolapWithNoSlice;
			case 11:
				return ValidationSR.Dimension_IsNotParentChildAndHasNoHierarchy;
			case 12:
				return ValidationSR.Hierarchy_IsUnNatural2;
			case 13:
				return ValidationSR.DimensionIgnoresDuplicateKeys;
			case 14:
				return ValidationSR.DimensionHasAttributeUsedByLevelsAndWithVisibleHierarchy;
			case 15:
				return ValidationSR.Dimension_HasMultipleNonAggregatableAttributes2(parameters[0]);
			case 16:
				return ValidationSR.AggregationBelowGranularity2(parameters[0], parameters[1]);
			case 17:
				return ValidationSR.DimensionAttribute_IsNonAggregatableInParentChild;
			case 18:
				return ValidationSR.NonAggregatableAttributeNeedsDefaultMember;
			case 19:
				return ValidationSR.Dimension_KeyAttributeOfParentChildShouldHaveHierarchyNotVisible;
			case 20:
				return ValidationSR.Dimension_HasUnknownMemberSetToHidden;
			case 21:
				return ValidationSR.Attribute_LargeAttributeWithNonNumericKey(500000L);
			case 22:
				return ValidationSR.NonKeyLargeAttributeWithVisibleHierarchy(1000000, 95);
			case 23:
				return ValidationSR.Dimension_RolapWithUnaryOperatorsOrCustomRollups;
			case 24:
				return ValidationSR.AttributeTypeAccountOrTimeNeedsMatchingDimension2(parameters[0], parameters[1]);
			case 25:
				return ValidationSR.AttributeTypeNeedsMatchingDimension2(parameters[0], parameters[1]);
			case 26:
				return ValidationSR.DimensionTypeAccountOrTimeNeedsMatchingAttribute2(parameters[0]);
			case 27:
				return ValidationSR.DimensionTypeNeedsMatchingAttribute2(parameters[0]);
			case 28:
				return ValidationSR.AttributesTypesDontMatch2;
			case 29:
				return ValidationSR.LevelHasFewerMembersThanUpperLevel;
			case 30:
				return ValidationSR.AggregationDesignWithNoEstimatedRows2;
			case 31:
				return ValidationSR.DimensionAndRelationshipTypes;
			case 32:
				return ValidationSR.RedundantRelationship2;
			case 33:
				return ValidationSR.DiamondShapeRelationships2;
			case 34:
				return ValidationSR.AttributeRelationshipName2;
			case 35:
				return ValidationSR.DimensionWithPollingQuery;
			case 36:
				return ValidationSR.NoTimeDimension2;
			case 37:
				return ValidationSR.TooManyParentChildDimsWithOutlineCalcs(3);
			case 38:
				return ValidationSR.ParentChildDimensionWithLargeKey(500000L);
			case 39:
				return ValidationSR.DimensionProcessByTable2;
			case 40:
				return ValidationSR.Database_TooManyDimensionsWithSingleAttribute;
			case 41:
				return ValidationSR.DistinctCountMeasure;
			case 42:
				return ValidationSR.ManyToManyHasLargeIntermediate;
			case 43:
				return ValidationSR.CubeWithSingleDimension;
			case 44:
				return ValidationSR.LinkedDimensionWithOutlineCalculations;
			case 45:
				return ValidationSR.ReferencedMeasureGroupDimensionNotMaterialized;
			case 46:
				return ValidationSR.IndependentMeasureGroup2;
			case 47:
				return ValidationSR.PartitionWithPollingQuery;
			case 48:
				return ValidationSR.MeasureGroupsWithTheSameDimensionalityAndGranularity2(parameters[0], parameters[1]);
			case 49:
				return ValidationSR.CubeHasTooManyMeasureGroups2;
			case 50:
				return ValidationSR.PerspectiveDefaultMeasureNotIncluded2(parameters[0]);
			case 51:
				return ValidationSR.MeasureGroupWithSemiAdditiveMeasuresAndRolapDimensions2(parameters[0]);
			case 52:
				return ValidationSR.DotNetSqlClientProvider;
			case 53:
				return ValidationSR.UnsupportedOledbProvider;
			case 54:
				return ValidationSR.MeasureGroupWithNoPartitions;
			case 55:
				return ValidationSR.PartitionIsRemoteRolap;
			case 56:
				return ValidationSR.AggregationsForTimeGranularityWithSemiAdditiveMeasures;
			case 57:
				return ValidationSR.AggregationsForTimeGranularityWithOnlySemiAdditiveMeasures;
			case 58:
				return ValidationSR.AttributeRelationshipNamedDescription;
			}
			throw new NotImplementedException();
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0002AF59 File Offset: 0x00029159
		private static ValidationRule wh(ValidationRuleID id, string category, string description, string helpId)
		{
			return new ValidationRule(id, ValidationRuleType.Warning, ValidationRulePriority.High, category, description, helpId);
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0002AF66 File Offset: 0x00029166
		private static ValidationRule wm(ValidationRuleID id, string category, string description, string helpId)
		{
			return new ValidationRule(id, ValidationRuleType.Warning, ValidationRulePriority.Medium, category, description, helpId);
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0002AF73 File Offset: 0x00029173
		private static ValidationRule wl(ValidationRuleID id, string category, string description, string helpId)
		{
			return new ValidationRule(id, ValidationRuleType.Warning, ValidationRulePriority.Low, category, description, helpId);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0002AF80 File Offset: 0x00029180
		private static ValidationRule eh(ValidationRuleID id)
		{
			return new ValidationRule(id, ValidationRuleType.Error, ValidationRulePriority.High, null, null, null);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0002AF8D File Offset: 0x0002918D
		private static ValidationRule em(ValidationRuleID id)
		{
			return new ValidationRule(id, ValidationRuleType.Error, ValidationRulePriority.Medium, null, null, null);
		}

		// Token: 0x04000716 RID: 1814
		private const string HelpPrefix = "sql13.asvs.amovalidationwarning.";

		// Token: 0x04000717 RID: 1815
		private static readonly ValidationRule.WarningCollection warnings;

		// Token: 0x04000718 RID: 1816
		private readonly int id;

		// Token: 0x04000719 RID: 1817
		private readonly ValidationRuleType type;

		// Token: 0x0400071A RID: 1818
		private readonly ValidationRulePriority priority;

		// Token: 0x0400071B RID: 1819
		private readonly string category;

		// Token: 0x0400071C RID: 1820
		private readonly string description;

		// Token: 0x0400071D RID: 1821
		private readonly string helpID;

		// Token: 0x0400071E RID: 1822
		internal static readonly ValidationRule LargePartitionWithNoAggs = ValidationRule.wh(ValidationRuleID.LargePartitionWithNoAggs, ValidationSR.RuleCategory_PartitionAndAggregation, ValidationSR.Partition_IsLargeWithNoAggs1(500000L), "sql13.asvs.amovalidationwarning.Partition_IsLargeWithNoAggs");

		// Token: 0x0400071F RID: 1823
		internal static readonly ValidationRule MeasureGroupHasLargePartitions = ValidationRule.wm(ValidationRuleID.MeasureGroupHasLargePartitions, ValidationSR.RuleCategory_PartitionAndAggregation, ValidationSR.MeasureGroup_HasLargePartitions(20, 250), "sql13.asvs.amovalidationwarning.MeasureGroup_HasLargePartitions");

		// Token: 0x04000720 RID: 1824
		internal static readonly ValidationRule MeasureGroupHasPartitionsToConsolidate = ValidationRule.wm(ValidationRuleID.MeasureGroupHasPartitionsToConsolidate, ValidationSR.RuleCategory_PartitionAndAggregation, ValidationSR.MeasureGroup_HasPartitionsToConsolidate(5, 2, 50), "sql13.asvs.amovalidationwarning.MeasureGroup_HasPartitionsToConsolidate");

		// Token: 0x04000721 RID: 1825
		internal static readonly ValidationRule AggregationDesignIsNotUsedByAnyPartition = ValidationRule.wl(ValidationRuleID.AggregationDesignIsNotUsedByAnyPartition, ValidationSR.RuleCategory_PartitionAndAggregation, ValidationSR.AggregationDesign_NotUsedByAnyPartition1, "sql13.asvs.amovalidationwarning.AggregationDesign_NotUsedByAnyPartition");

		// Token: 0x04000722 RID: 1826
		internal static readonly ValidationRule MeasureGroupHasTooManyAggregationDesigns = ValidationRule.wl(ValidationRuleID.MeasureGroupHasTooManyAggregationDesigns, ValidationSR.RuleCategory_PartitionAndAggregation, ValidationSR.MeasureGroup_HasTooManyAggregationDesigns, "sql13.asvs.amovalidationwarning.MeasureGroup_HasTooManyAggregationDesigns");

		// Token: 0x04000723 RID: 1827
		internal static readonly ValidationRule IntermediateGranularityNotAggregated = ValidationRule.wm(ValidationRuleID.IntermediateGranularityNotAggregated, ValidationSR.RuleCategory_PartitionAndAggregation, ValidationSR.IntermediateGranularityNotAggregated1, "sql13.asvs.amovalidationwarning.IntermediateGranularityNotAggregated");

		// Token: 0x04000724 RID: 1828
		internal static readonly ValidationRule CubeAttributeAggregationUsageWithUnaryOperatorOrCustomRollup = ValidationRule.wm(ValidationRuleID.CubeAttributeAggregationUsageWithUnaryOperatorOrCustomRollup, ValidationSR.RuleCategory_PartitionAndAggregation, ValidationSR.CubeAttributeAggregationUsageWithUnaryOperatorOrCustomRollup1, "sql13.asvs.amovalidationwarning.CubeAttributeAggregationUsageWithUnaryOperatorOrCustomRollup");

		// Token: 0x04000725 RID: 1829
		internal static readonly ValidationRule PartitionWithTooManyAggregations = ValidationRule.wh(ValidationRuleID.PartitionWithTooManyAggregations, ValidationSR.RuleCategory_PartitionAndAggregation, ValidationSR.PartitionWithTooManyAggregations(500), "sql13.asvs.amovalidationwarning.PartitionWithTooManyAggregations");

		// Token: 0x04000726 RID: 1830
		internal static readonly ValidationRule AggregationHasRelatedAttributes = ValidationRule.wm(ValidationRuleID.AggregationHasRelatedAttributes, ValidationSR.RuleCategory_PartitionAndAggregation, ValidationSR.AggregationHasRelatedAttributes1, "sql13.asvs.amovalidationwarning.AggregationHasRelatedAttributes");

		// Token: 0x04000727 RID: 1831
		internal static readonly ValidationRule Partition_RolapWithNoSlice = ValidationRule.wm(ValidationRuleID.Partition_RolapWithNoSlice, ValidationSR.RuleCategory_PartitionAndAggregation, ValidationSR.Partition_RolapWithNoSlice, "sql13.asvs.amovalidationwarning.Partition_RolapWithNoSlice");

		// Token: 0x04000728 RID: 1832
		internal static readonly ValidationRule PartitionWithPollingQuery = ValidationRule.wm(ValidationRuleID.PartitionWithPollingQuery, ValidationSR.RuleCategory_PartitionAndAggregation, ValidationSR.PartitionWithPollingQuery, "sql13.asvs.amovalidationwarning.PartitionWithPollingQuery");

		// Token: 0x04000729 RID: 1833
		internal static readonly ValidationRule PartitionIsRemoteRolap = ValidationRule.wm(ValidationRuleID.PartitionIsRemoteRolap, ValidationSR.RuleCategory_PartitionAndAggregation, ValidationSR.PartitionIsRemoteRolap, "sql13.asvs.amovalidationwarning.PartitionIsRemoteRolap");

		// Token: 0x0400072A RID: 1834
		internal static readonly ValidationRule AggregationBelowGranularity = ValidationRule.wh(ValidationRuleID.AggregationBelowGranularity, ValidationSR.RuleCategory_PartitionAndAggregation, ValidationSR.AggregationBelowGranularity1, "sql13.asvs.amovalidationwarning.AggregationBelowGranularity");

		// Token: 0x0400072B RID: 1835
		internal static readonly ValidationRule AggregationDesignWithNoEstimatedRows = ValidationRule.wl(ValidationRuleID.AggregationDesignWithNoEstimatedRows, ValidationSR.RuleCategory_PartitionAndAggregation, ValidationSR.AggregationDesignWithNoEstimatedRows1, "sql13.asvs.amovalidationwarning.AggregationDesignWithNoEstimatedRows");

		// Token: 0x0400072C RID: 1836
		internal static readonly ValidationRule AggregationsForTimeGranularityWithSemiAdditiveMeasures = ValidationRule.wh(ValidationRuleID.AggregationsForTimeGranularityWithSemiAdditiveMeasures, ValidationSR.RuleCategory_PartitionAndAggregation, ValidationSR.AggregationsForTimeGranularityWithSemiAdditiveMeasures, "sql13.asvs.amovalidationwarning.AggregationsForTimeGranularityWithSemiAdditiveMeasures");

		// Token: 0x0400072D RID: 1837
		internal static readonly ValidationRule AggregationsForTimeGranularityWithOnlySemiAdditiveMeasures = ValidationRule.wh(ValidationRuleID.AggregationsForTimeGranularityWithOnlySemiAdditiveMeasures, ValidationSR.RuleCategory_PartitionAndAggregation, ValidationSR.AggregationsForTimeGranularityWithOnlySemiAdditiveMeasures, "sql13.asvs.amovalidationwarning.AggregationsForTimeGranularityWithOnlySemiAdditiveMeasures");

		// Token: 0x0400072E RID: 1838
		internal static readonly ValidationRule Dimension_IsNotParentChildAndHasNoHierarchy = ValidationRule.wl(ValidationRuleID.Dimension_IsNotParentChildAndHasNoHierarchy, ValidationSR.RuleCategory_Dimension, ValidationSR.Dimension_IsNotParentChildAndHasNoHierarchy, "sql13.asvs.amovalidationwarning.Dimension_IsNotParentChildAndHasNoHierarchy");

		// Token: 0x0400072F RID: 1839
		internal static readonly ValidationRule HierarchyIsUnNatural = ValidationRule.wh(ValidationRuleID.HierarchyIsUnNatural, ValidationSR.RuleCategory_Dimension, ValidationSR.Hierarchy_IsUnNatural1, "sql13.asvs.amovalidationwarning.Hierarchy_IsUnNatural");

		// Token: 0x04000730 RID: 1840
		internal static readonly ValidationRule DimensionIgnoresDuplicateKeys = ValidationRule.wm(ValidationRuleID.DimensionIgnoresDuplicateKeys, ValidationSR.RuleCategory_Dimension, ValidationSR.DimensionIgnoresDuplicateKeys, "sql13.asvs.amovalidationwarning.DimensionIgnoresDuplicateKeys");

		// Token: 0x04000731 RID: 1841
		internal static readonly ValidationRule DimensionHasAttributeUsedByLevelsAndWithVisibleHierarchy = ValidationRule.wl(ValidationRuleID.DimensionHasAttributeUsedByLevelsAndWithVisibleHierarchy, ValidationSR.RuleCategory_Dimension, ValidationSR.DimensionHasAttributeUsedByLevelsAndWithVisibleHierarchy, "sql13.asvs.amovalidationwarning.DimensionHasAttributeUsedByLevelsAndWithVisibleHierarchy");

		// Token: 0x04000732 RID: 1842
		internal static readonly ValidationRule DimensionHasMultipleNonAggregatableAttributes = ValidationRule.wh(ValidationRuleID.DimensionHasMultipleNonAggregatableAttributes, ValidationSR.RuleCategory_Dimension, ValidationSR.Dimension_HasMultipleNonAggregatableAttributes1, "sql13.asvs.amovalidationwarning.Dimension_HasMultipleNonAggregatableAttributes");

		// Token: 0x04000733 RID: 1843
		internal static readonly ValidationRule ParentChildDimensionWithNonAggregatableAttribute = ValidationRule.wm(ValidationRuleID.ParentChildDimensionWithNonAggregatableAttribute, ValidationSR.RuleCategory_Dimension, ValidationSR.DimensionAttribute_IsNonAggregatableInParentChild, "sql13.asvs.amovalidationwarning.DimensionAttribute_IsNonAggregatableInParentChild");

		// Token: 0x04000734 RID: 1844
		internal static readonly ValidationRule NonAggregatableAttributeNeedsDefaultMember = ValidationRule.wm(ValidationRuleID.NonAggregatableAttributeNeedsDefaultMember, ValidationSR.RuleCategory_Dimension, ValidationSR.NonAggregatableAttributeNeedsDefaultMember, "sql13.asvs.amovalidationwarning.NonAggregatableAttributeNeedsDefaultMember");

		// Token: 0x04000735 RID: 1845
		internal static readonly ValidationRule KeyAttributeOfParentChildHasVisibleHierarchy = ValidationRule.wm(ValidationRuleID.KeyAttributeOfParentChildHasVisibleHierarchy, ValidationSR.RuleCategory_Dimension, ValidationSR.Dimension_KeyAttributeOfParentChildShouldHaveHierarchyNotVisible, "sql13.asvs.amovalidationwarning.Dimension_KeyAttributeOfParentChildShouldHaveHierarchyNotVisible");

		// Token: 0x04000736 RID: 1846
		internal static readonly ValidationRule DimensionHasUnknownMemberSetToHidden = ValidationRule.wm(ValidationRuleID.DimensionHasUnknownMemberSetToHidden, ValidationSR.RuleCategory_Dimension, ValidationSR.Dimension_HasUnknownMemberSetToHidden, "sql13.asvs.amovalidationwarning.Dimension_HasUnknownMemberSetToHidden");

		// Token: 0x04000737 RID: 1847
		internal static readonly ValidationRule AttributeIsLargeWithNonNumericKey = ValidationRule.wh(ValidationRuleID.AttributeIsLargeWithNonNumericKey, ValidationSR.RuleCategory_Dimension, ValidationSR.Attribute_LargeAttributeWithNonNumericKey(500000L), "sql13.asvs.amovalidationwarning.Attribute_LargeAttributeWithNonNumericKey");

		// Token: 0x04000738 RID: 1848
		internal static readonly ValidationRule NonKeyLargeAttributeWithVisibleHierarchy = ValidationRule.wm(ValidationRuleID.NonKeyLargeAttributeWithVisibleHierarchy, ValidationSR.RuleCategory_Dimension, ValidationSR.NonKeyLargeAttributeWithVisibleHierarchy(1000000, 95), "sql13.asvs.amovalidationwarning.NonKeyLargeAttributeWithVisibleHierarchy");

		// Token: 0x04000739 RID: 1849
		internal static readonly ValidationRule Dimension_RolapWithUnaryOperatorsOrCustomRollups = ValidationRule.wh(ValidationRuleID.Dimension_RolapWithUnaryOperatorsOrCustomRollups, ValidationSR.RuleCategory_Dimension, ValidationSR.Dimension_RolapWithUnaryOperatorsOrCustomRollups, "sql13.asvs.amovalidationwarning.Dimension_RolapWithUnaryOperatorsOrCustomRollups");

		// Token: 0x0400073A RID: 1850
		internal static readonly ValidationRule AttributeTypeAccountOrTimeNeedsMatchingDimension = ValidationRule.wm(ValidationRuleID.AttributeTypeAccountOrTimeNeedsMatchingDimension, ValidationSR.RuleCategory_Dimension, ValidationSR.AttributeTypeAccountOrTimeNeedsMatchingDimension1, "sql13.asvs.amovalidationwarning.AttributeTypeAccountOrTimeNeedsMatchingDimension");

		// Token: 0x0400073B RID: 1851
		internal static readonly ValidationRule AttributeTypeNeedsMatchingDimension = ValidationRule.wl(ValidationRuleID.AttributeTypeNeedsMatchingDimension, ValidationSR.RuleCategory_Dimension, ValidationSR.AttributeTypeNeedsMatchingDimension1, "sql13.asvs.amovalidationwarning.AttributeTypeNeedsMatchingDimension");

		// Token: 0x0400073C RID: 1852
		internal static readonly ValidationRule DimensionTypeAccountOrTimeNeedsMatchingAttribute = ValidationRule.wm(ValidationRuleID.DimensionTypeAccountOrTimeNeedsMatchingAttribute, ValidationSR.RuleCategory_Dimension, ValidationSR.DimensionTypeAccountOrTimeNeedsMatchingAttribute1, "sql13.asvs.amovalidationwarning.DimensionTypeAccountOrTimeNeedsMatchingAttribute");

		// Token: 0x0400073D RID: 1853
		internal static readonly ValidationRule DimensionTypeNeedsMatchingAttribute = ValidationRule.wl(ValidationRuleID.DimensionTypeNeedsMatchingAttribute, ValidationSR.RuleCategory_Dimension, ValidationSR.DimensionTypeNeedsMatchingAttribute1, "sql13.asvs.amovalidationwarning.DimensionTypeNeedsMatchingAttribute");

		// Token: 0x0400073E RID: 1854
		internal static readonly ValidationRule AttributesTypesDontMatch = ValidationRule.wl(ValidationRuleID.AttributesTypesDontMatch, ValidationSR.RuleCategory_Dimension, ValidationSR.AttributesTypesDontMatch1, "sql13.asvs.amovalidationwarning.AttributesTypesDontMatch");

		// Token: 0x0400073F RID: 1855
		internal static readonly ValidationRule LevelHasFewerMembersThanUpperLevel = ValidationRule.wh(ValidationRuleID.LevelHasFewerMembersThanUpperLevel, ValidationSR.RuleCategory_Dimension, ValidationSR.LevelHasFewerMembersThanUpperLevel, "sql13.asvs.amovalidationwarning.LevelHasFewerMembersThanUpperLevel");

		// Token: 0x04000740 RID: 1856
		internal static readonly ValidationRule DimensionAndRelationshipTypes = ValidationRule.wm(ValidationRuleID.DimensionAndRelationshipTypes, ValidationSR.RuleCategory_Dimension, ValidationSR.DimensionAndRelationshipTypes, "sql13.asvs.amovalidationwarning.DimensionAndRelationshipTypes");

		// Token: 0x04000741 RID: 1857
		internal static readonly ValidationRule RedundantRelationship = ValidationRule.wm(ValidationRuleID.RedundantRelationship, ValidationSR.RuleCategory_Dimension, ValidationSR.RedundantRelationship1, "sql13.asvs.amovalidationwarning.RedundantRelationship");

		// Token: 0x04000742 RID: 1858
		internal static readonly ValidationRule DiamondShapeRelationships = ValidationRule.wm(ValidationRuleID.DiamondShapeRelationships, ValidationSR.RuleCategory_Dimension, ValidationSR.DiamondShapeRelationships1, "sql13.asvs.amovalidationwarning.DiamondShapeRelationships");

		// Token: 0x04000743 RID: 1859
		internal static readonly ValidationRule AttributeRelationshipName = ValidationRule.wl(ValidationRuleID.AttributeRelationshipName, ValidationSR.RuleCategory_Dimension, ValidationSR.AttributeRelationshipName1, "sql13.asvs.amovalidationwarning.AttributeRelationshipName");

		// Token: 0x04000744 RID: 1860
		internal static readonly ValidationRule DimensionWithPollingQuery = ValidationRule.wm(ValidationRuleID.DimensionWithPollingQuery, ValidationSR.RuleCategory_Dimension, ValidationSR.DimensionWithPollingQuery, "sql13.asvs.amovalidationwarning.DimensionWithPollingQuery");

		// Token: 0x04000745 RID: 1861
		internal static readonly ValidationRule ParentChildDimensionWithLargeKey = ValidationRule.wm(ValidationRuleID.ParentChildDimensionWithLargeKey, ValidationSR.RuleCategory_Dimension, ValidationSR.ParentChildDimensionWithLargeKey(500000L), "sql13.asvs.amovalidationwarning.ParentChildDimensionWithLargeKey");

		// Token: 0x04000746 RID: 1862
		internal static readonly ValidationRule DimensionProcessByTable = ValidationRule.wm(ValidationRuleID.DimensionProcessByTable, ValidationSR.RuleCategory_Dimension, ValidationSR.DimensionProcessByTable1, "sql13.asvs.amovalidationwarning.DimensionProcessByTable");

		// Token: 0x04000747 RID: 1863
		internal static readonly ValidationRule LinkedDimensionWithOutlineCalculations = ValidationRule.wm(ValidationRuleID.LinkedDimensionWithOutlineCalculations, ValidationSR.RuleCategory_Dimension, ValidationSR.LinkedDimensionWithOutlineCalculations, "sql13.asvs.amovalidationwarning.LinkedDimensionWithOutlineCalculations");

		// Token: 0x04000748 RID: 1864
		internal static readonly ValidationRule AttributeRelationshipNamedDescription = ValidationRule.wl(ValidationRuleID.AttributeRelationshipNamedDescription, ValidationSR.RuleCategory_Dimension, ValidationSR.AttributeRelationshipNamedDescription, "sql13.asvs.amovalidationwarning.AttributeRelationshipNamedDescription");

		// Token: 0x04000749 RID: 1865
		internal static readonly ValidationRule NoTimeDimension = ValidationRule.wm(ValidationRuleID.NoTimeDimension, ValidationSR.RuleCategory_Database, ValidationSR.NoTimeDimension1, "sql13.asvs.amovalidationwarning.NoTimeDimension");

		// Token: 0x0400074A RID: 1866
		internal static readonly ValidationRule TooManyParentChildDimsWithOutlineCalcs = ValidationRule.wm(ValidationRuleID.TooManyParentChildDimsWithOutlineCalcs, ValidationSR.RuleCategory_Database, ValidationSR.TooManyParentChildDimsWithOutlineCalcs(3), "sql13.asvs.amovalidationwarning.TooManyParentChildDimsWithOutlineCalcs");

		// Token: 0x0400074B RID: 1867
		internal static readonly ValidationRule DatabaseHasTooManyDimensionsWithSingleAttribute = ValidationRule.wm(ValidationRuleID.DatabaseHasTooManyDimensionsWithSingleAttribute, ValidationSR.RuleCategory_Database, ValidationSR.Database_TooManyDimensionsWithSingleAttribute, "sql13.asvs.amovalidationwarning.Database_TooManyDimensionsWithSingleAttribute");

		// Token: 0x0400074C RID: 1868
		internal static readonly ValidationRule DistinctCountMeasure = ValidationRule.wh(ValidationRuleID.DistinctCountMeasure, ValidationSR.RuleCategory_Cube, ValidationSR.DistinctCountMeasure, "sql13.asvs.amovalidationwarning.DistinctCountMeasure");

		// Token: 0x0400074D RID: 1869
		internal static readonly ValidationRule ManyToManyHasLargeIntermediate = ValidationRule.wh(ValidationRuleID.ManyToManyHasLargeIntermediate, ValidationSR.RuleCategory_Cube, ValidationSR.ManyToManyHasLargeIntermediate, "sql13.asvs.amovalidationwarning.ManyToManyHasLargeIntermediate");

		// Token: 0x0400074E RID: 1870
		internal static readonly ValidationRule CubeWithSingleDimension = ValidationRule.wm(ValidationRuleID.CubeWithSingleDimension, ValidationSR.RuleCategory_Cube, ValidationSR.CubeWithSingleDimension, "sql13.asvs.amovalidationwarning.CubeWithSingleDimension");

		// Token: 0x0400074F RID: 1871
		internal static readonly ValidationRule ReferencedMeasureGroupDimensionNotMaterialized = ValidationRule.wm(ValidationRuleID.ReferencedMeasureGroupDimensionNotMaterialized, ValidationSR.RuleCategory_Cube, ValidationSR.ReferencedMeasureGroupDimensionNotMaterialized, "sql13.asvs.amovalidationwarning.ReferencedMeasureGroupDimensionNotMaterialized");

		// Token: 0x04000750 RID: 1872
		internal static readonly ValidationRule IndependentMeasureGroup = ValidationRule.wm(ValidationRuleID.IndependentMeasureGroup, ValidationSR.RuleCategory_Cube, ValidationSR.IndependentMeasureGroup1, "sql13.asvs.amovalidationwarning.IndependentMeasureGroup");

		// Token: 0x04000751 RID: 1873
		internal static readonly ValidationRule MeasureGroupsWithTheSameDimensionalityAndGranularity = ValidationRule.wh(ValidationRuleID.MeasureGroupsWithTheSameDimensionalityAndGranularity, ValidationSR.RuleCategory_Cube, ValidationSR.MeasureGroupsWithTheSameDimensionalityAndGranularity1, "sql13.asvs.amovalidationwarning.MeasureGroupsWithTheSameDimensionalityAndGranularity");

		// Token: 0x04000752 RID: 1874
		internal static readonly ValidationRule CubeHasTooManyMeasureGroups = ValidationRule.wm(ValidationRuleID.CubeHasTooManyMeasureGroups, ValidationSR.RuleCategory_Cube, ValidationSR.CubeHasTooManyMeasureGroups1, "sql13.asvs.amovalidationwarning.CubeHasTooManyMeasureGroups");

		// Token: 0x04000753 RID: 1875
		internal static readonly ValidationRule PerspectiveDefaultMeasureNotIncluded = ValidationRule.wh(ValidationRuleID.PerspectiveDefaultMeasureNotIncluded, ValidationSR.RuleCategory_Cube, ValidationSR.PerspectiveDefaultMeasureNotIncluded1, "sql13.asvs.amovalidationwarning.PerspectiveDefaultMeasureNotIncluded");

		// Token: 0x04000754 RID: 1876
		internal static readonly ValidationRule MeasureGroupWithSemiAdditiveMeasuresAndRolapDimension = ValidationRule.wh(ValidationRuleID.MeasureGroupWithSemiAdditiveMeasuresAndRolapDimension, ValidationSR.RuleCategory_Cube, ValidationSR.MeasureGroupWithSemiAdditiveMeasuresAndRolapDimensions1, "sql13.asvs.amovalidationwarning.MeasureGroupWithSemiAdditiveMeasuresAndRolapDimensions");

		// Token: 0x04000755 RID: 1877
		internal static readonly ValidationRule MeasureGroupWithNoPartitions = ValidationRule.wl(ValidationRuleID.MeasureGroupWithNoPartitions, ValidationSR.RuleCategory_Cube, ValidationSR.MeasureGroupWithNoPartitions, "sql13.asvs.amovalidationwarning.MeasureGroupWithNoPartitions");

		// Token: 0x04000756 RID: 1878
		internal static readonly ValidationRule DotNetSqlClientProvider = ValidationRule.wh(ValidationRuleID.DotNetSqlClientProvider, ValidationSR.RuleCategory_DataSource, ValidationSR.DotNetSqlClientProvider, "sql13.asvs.amovalidationwarning.DotNetSqlClientProvider");

		// Token: 0x04000757 RID: 1879
		internal static readonly ValidationRule UnsupportedOledbProvider = ValidationRule.wm(ValidationRuleID.UnsupportedOledbProvider, ValidationSR.RuleCategory_DataSource, ValidationSR.UnsupportedOledbProvider, "sql13.asvs.amovalidationwarning.UnsupportedOledbProvider");

		// Token: 0x04000758 RID: 1880
		internal static readonly ValidationRule ErrorHigh = ValidationRule.eh(ValidationRuleID.ErrorHigh);

		// Token: 0x04000759 RID: 1881
		internal static readonly ValidationRule ErrorMedium = ValidationRule.em(ValidationRuleID.ErrorMedium);

		// Token: 0x0400075A RID: 1882
		internal static readonly ValidationRule NoDimensionsDefined = ValidationRule.eh(ValidationRuleID.NoDimensionsDefined);

		// Token: 0x0400075B RID: 1883
		internal static readonly ValidationRule MeasureGroupHasMoreThanOneDegenerateDimension = ValidationRule.eh(ValidationRuleID.MeasureGroupHasMoreThanOneDegenerateDimension);

		// Token: 0x0400075C RID: 1884
		internal static readonly ValidationRule MeasureGroupHasSemiadditiveMeasureWithNoTimeDimension = ValidationRule.eh(ValidationRuleID.MeasureGroupHasSemiadditiveMeasureWithNoTimeDimension);

		// Token: 0x0200019C RID: 412
		private class WarningCollection : ICollection, IEnumerable
		{
			// Token: 0x06001308 RID: 4872 RVA: 0x00043169 File Offset: 0x00041369
			internal WarningCollection(ArrayList list)
			{
				this.list = list;
			}

			// Token: 0x06001309 RID: 4873 RVA: 0x00043178 File Offset: 0x00041378
			void ICollection.CopyTo(Array array, int index)
			{
				this.list.CopyTo(array, index);
			}

			// Token: 0x1700062A RID: 1578
			// (get) Token: 0x0600130A RID: 4874 RVA: 0x00043187 File Offset: 0x00041387
			int ICollection.Count
			{
				get
				{
					return this.list.Count;
				}
			}

			// Token: 0x1700062B RID: 1579
			// (get) Token: 0x0600130B RID: 4875 RVA: 0x00043194 File Offset: 0x00041394
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700062C RID: 1580
			// (get) Token: 0x0600130C RID: 4876 RVA: 0x00043197 File Offset: 0x00041397
			object ICollection.SyncRoot
			{
				get
				{
					return null;
				}
			}

			// Token: 0x0600130D RID: 4877 RVA: 0x0004319A File Offset: 0x0004139A
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.list.GetEnumerator();
			}

			// Token: 0x04000C45 RID: 3141
			private ArrayList list;
		}
	}
}
