using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001DF RID: 479
	internal static class CompatibilityRestrictions
	{
		// Token: 0x040005D9 RID: 1497
		public static readonly CompatibilityRestrictionSet ObjectState_ForceCalculationNeeded = new CompatibilityRestrictionSet(new KeyValuePair<CompatibilityMode, int>[]
		{
			new KeyValuePair<CompatibilityMode, int>(CompatibilityMode.PowerBI, 1400)
		});

		// Token: 0x040005DA RID: 1498
		public static readonly CompatibilityRestrictionSet PartitionSourceType_M = new CompatibilityRestrictionSet(1400);

		// Token: 0x040005DB RID: 1499
		public static readonly CompatibilityRestrictionSet PartitionSourceType_Entity = new CompatibilityRestrictionSet(1400);

		// Token: 0x040005DC RID: 1500
		public static readonly CompatibilityRestrictionSet PartitionSourceType_PolicyRange = new CompatibilityRestrictionSet(1450);

		// Token: 0x040005DD RID: 1501
		public static readonly CompatibilityRestrictionSet PartitionSourceType_CalculationGroup = new CompatibilityRestrictionSet(1470);

		// Token: 0x040005DE RID: 1502
		public static readonly CompatibilityRestrictionSet PartitionSourceType_Inferred = new CompatibilityRestrictionSet(1563);

		// Token: 0x040005DF RID: 1503
		public static readonly CompatibilityRestrictionSet PartitionSourceType_Parquet = new CompatibilityRestrictionSet(int.MaxValue);

		// Token: 0x040005E0 RID: 1504
		public static readonly CompatibilityRestrictionSet EvaluationBehavior = new CompatibilityRestrictionSet(1000000);

		// Token: 0x040005E1 RID: 1505
		public static readonly CompatibilityRestrictionSet SecurityFilteringBehavior_None = new CompatibilityRestrictionSet(1561);

		// Token: 0x040005E2 RID: 1506
		public static readonly CompatibilityRestrictionSet DataSourceType_Structured = new CompatibilityRestrictionSet(1400);

		// Token: 0x040005E3 RID: 1507
		public static readonly CompatibilityRestrictionSet ModeType_Push = new CompatibilityRestrictionSet(new KeyValuePair<CompatibilityMode, int>[]
		{
			new KeyValuePair<CompatibilityMode, int>(CompatibilityMode.PowerBI, -1)
		});

		// Token: 0x040005E4 RID: 1508
		public static readonly CompatibilityRestrictionSet ModeType_Dual = new CompatibilityRestrictionSet(1455);

		// Token: 0x040005E5 RID: 1509
		public static readonly CompatibilityRestrictionSet ModeType_DirectLake = new CompatibilityRestrictionSet(1604);

		// Token: 0x040005E6 RID: 1510
		public static readonly CompatibilityRestrictionSet DirectLakeBehavior = new CompatibilityRestrictionSet(1604);

		// Token: 0x040005E7 RID: 1511
		public static readonly CompatibilityRestrictionSet ExtendedPropertyType = new CompatibilityRestrictionSet(1400);

		// Token: 0x040005E8 RID: 1512
		public static readonly CompatibilityRestrictionSet HierarchyHideMembersType = new CompatibilityRestrictionSet(1400);

		// Token: 0x040005E9 RID: 1513
		public static readonly CompatibilityRestrictionSet ExpressionKind = new CompatibilityRestrictionSet(1400);

		// Token: 0x040005EA RID: 1514
		public static readonly CompatibilityRestrictionSet MetadataPermission = new CompatibilityRestrictionSet(1400);

		// Token: 0x040005EB RID: 1515
		public static readonly CompatibilityRestrictionSet EncodingHintType = new CompatibilityRestrictionSet(1400);

		// Token: 0x040005EC RID: 1516
		public static readonly CompatibilityRestrictionSet SummarizationType = new CompatibilityRestrictionSet(1460);

		// Token: 0x040005ED RID: 1517
		public static readonly CompatibilityRestrictionSet RefreshGranularityType = new CompatibilityRestrictionSet(1450);

		// Token: 0x040005EE RID: 1518
		public static readonly CompatibilityRestrictionSet RefreshPolicyType = new CompatibilityRestrictionSet(1450);

		// Token: 0x040005EF RID: 1519
		public static readonly CompatibilityRestrictionSet PowerBIDataSourceVersion = new CompatibilityRestrictionSet(1450);

		// Token: 0x040005F0 RID: 1520
		public static readonly CompatibilityRestrictionSet PowerBIDataSourceVersion_PowerBI_V3 = new CompatibilityRestrictionSet(1465);

		// Token: 0x040005F1 RID: 1521
		public static readonly CompatibilityRestrictionSet ContentType = new CompatibilityRestrictionSet(1465);

		// Token: 0x040005F2 RID: 1522
		public static readonly CompatibilityRestrictionSet DataSourceVariablesOverrideBehaviorType = new CompatibilityRestrictionSet(1475);

		// Token: 0x040005F3 RID: 1523
		public static readonly CompatibilityRestrictionSet RefreshPolicyMode = new CompatibilityRestrictionSet(1565);

		// Token: 0x040005F4 RID: 1524
		public static readonly CompatibilityRestrictionSet TimeUnit = new CompatibilityRestrictionSet(1000000);

		// Token: 0x040005F5 RID: 1525
		public static readonly CompatibilityRestrictionSet CalculationGroupSelectionMode = new CompatibilityRestrictionSet(1605);

		// Token: 0x040005F6 RID: 1526
		public static readonly CompatibilityRestrictionSet ValueFilterBehaviorType = new CompatibilityRestrictionSet(1606);

		// Token: 0x040005F7 RID: 1527
		public static readonly CompatibilityRestrictionSet BindingInfoType = new CompatibilityRestrictionSet(1000000);

		// Token: 0x040005F8 RID: 1528
		public static readonly CompatibilityRestrictionSet Model_DataAccessOptions = new CompatibilityRestrictionSet(1400);

		// Token: 0x040005F9 RID: 1529
		public static readonly CompatibilityRestrictionSet Model_DefaultMeasure = new CompatibilityRestrictionSet(1400);

		// Token: 0x040005FA RID: 1530
		public static readonly CompatibilityRestrictionSet Model_DefaultPowerBIDataSourceVersion = new CompatibilityRestrictionSet(1450);

		// Token: 0x040005FB RID: 1531
		public static readonly CompatibilityRestrictionSet Model_ForceUniqueNames = new CompatibilityRestrictionSet(1465);

		// Token: 0x040005FC RID: 1532
		public static readonly CompatibilityRestrictionSet Model_DiscourageImplicitMeasures = new CompatibilityRestrictionSet(1470);

		// Token: 0x040005FD RID: 1533
		public static readonly CompatibilityRestrictionSet Model_DiscourageReportMeasures = new CompatibilityRestrictionSet(int.MaxValue);

		// Token: 0x040005FE RID: 1534
		public static readonly CompatibilityRestrictionSet Model_DataSourceVariablesOverrideBehavior = new CompatibilityRestrictionSet(1475);

		// Token: 0x040005FF RID: 1535
		public static readonly CompatibilityRestrictionSet Model_DataSourceDefaultMaxConnections = new CompatibilityRestrictionSet(1510);

		// Token: 0x04000600 RID: 1536
		public static readonly CompatibilityRestrictionSet Model_SourceQueryCulture = new CompatibilityRestrictionSet(1520);

		// Token: 0x04000601 RID: 1537
		public static readonly CompatibilityRestrictionSet Model_MAttributes = new CompatibilityRestrictionSet(1535);

		// Token: 0x04000602 RID: 1538
		public static readonly CompatibilityRestrictionSet Model_DiscourageCompositeModels = new CompatibilityRestrictionSet(1560);

		// Token: 0x04000603 RID: 1539
		public static readonly CompatibilityRestrictionSet Model_AutomaticAggregationOptions = new CompatibilityRestrictionSet(1564);

		// Token: 0x04000604 RID: 1540
		public static readonly CompatibilityRestrictionSet Model_DisableAutoExists = new CompatibilityRestrictionSet(1566);

		// Token: 0x04000605 RID: 1541
		public static readonly CompatibilityRestrictionSet Model_MaxParallelismPerRefresh = new CompatibilityRestrictionSet(1568);

		// Token: 0x04000606 RID: 1542
		public static readonly CompatibilityRestrictionSet Model_MaxParallelismPerQuery = new CompatibilityRestrictionSet(1569);

		// Token: 0x04000607 RID: 1543
		public static readonly CompatibilityRestrictionSet Model_DisableSystemDefaultExpression = new CompatibilityRestrictionSet(1000000);

		// Token: 0x04000608 RID: 1544
		public static readonly CompatibilityRestrictionSet Model_DirectLakeBehavior = new CompatibilityRestrictionSet(1604);

		// Token: 0x04000609 RID: 1545
		public static readonly CompatibilityRestrictionSet Model_ValueFilterBehavior = new CompatibilityRestrictionSet(1606);

		// Token: 0x0400060A RID: 1546
		public static readonly CompatibilityRestrictionSet DataSource_ConnectionDetails = new CompatibilityRestrictionSet(1400);

		// Token: 0x0400060B RID: 1547
		public static readonly CompatibilityRestrictionSet DataSource_Options = new CompatibilityRestrictionSet(1400);

		// Token: 0x0400060C RID: 1548
		public static readonly CompatibilityRestrictionSet DataSource_Credential = new CompatibilityRestrictionSet(1400);

		// Token: 0x0400060D RID: 1549
		public static readonly CompatibilityRestrictionSet DataSource_ContextExpression = new CompatibilityRestrictionSet(1400);

		// Token: 0x0400060E RID: 1550
		public static readonly CompatibilityRestrictionSet StructuredDataSource = new CompatibilityRestrictionSet(1400);

		// Token: 0x0400060F RID: 1551
		public static readonly CompatibilityRestrictionSet StructuredDataSource_ConnectionDetails = new CompatibilityRestrictionSet(1400);

		// Token: 0x04000610 RID: 1552
		public static readonly CompatibilityRestrictionSet StructuredDataSource_Options = new CompatibilityRestrictionSet(1400);

		// Token: 0x04000611 RID: 1553
		public static readonly CompatibilityRestrictionSet StructuredDataSource_Credential = new CompatibilityRestrictionSet(1400);

		// Token: 0x04000612 RID: 1554
		public static readonly CompatibilityRestrictionSet StructuredDataSource_ContextExpression = new CompatibilityRestrictionSet(1400);

		// Token: 0x04000613 RID: 1555
		public static readonly CompatibilityRestrictionSet Table_ShowAsVariationsOnly = new CompatibilityRestrictionSet(new KeyValuePair<CompatibilityMode, int>[]
		{
			new KeyValuePair<CompatibilityMode, int>(CompatibilityMode.PowerBI, -1),
			new KeyValuePair<CompatibilityMode, int>(CompatibilityMode.AnalysisServices, 1400),
			new KeyValuePair<CompatibilityMode, int>(CompatibilityMode.Excel, 1400)
		});

		// Token: 0x04000614 RID: 1556
		public static readonly CompatibilityRestrictionSet Table_IsPrivate = new CompatibilityRestrictionSet(new KeyValuePair<CompatibilityMode, int>[]
		{
			new KeyValuePair<CompatibilityMode, int>(CompatibilityMode.PowerBI, -1),
			new KeyValuePair<CompatibilityMode, int>(CompatibilityMode.AnalysisServices, 1400),
			new KeyValuePair<CompatibilityMode, int>(CompatibilityMode.Excel, 1400)
		});

		// Token: 0x04000615 RID: 1557
		public static readonly CompatibilityRestrictionSet Table_DefaultDetailRowsDefinition = new CompatibilityRestrictionSet(1400);

		// Token: 0x04000616 RID: 1558
		public static readonly CompatibilityRestrictionSet Table_AlternateSourcePrecedence = new CompatibilityRestrictionSet(1460);

		// Token: 0x04000617 RID: 1559
		public static readonly CompatibilityRestrictionSet Table_RefreshPolicy = new CompatibilityRestrictionSet(1450);

		// Token: 0x04000618 RID: 1560
		public static readonly CompatibilityRestrictionSet Table_CalculationGroup = new CompatibilityRestrictionSet(1470);

		// Token: 0x04000619 RID: 1561
		public static readonly CompatibilityRestrictionSet Table_ExcludeFromModelRefresh = new CompatibilityRestrictionSet(1480);

		// Token: 0x0400061A RID: 1562
		public static readonly CompatibilityRestrictionSet Table_LineageTag = new CompatibilityRestrictionSet(1540);

		// Token: 0x0400061B RID: 1563
		public static readonly CompatibilityRestrictionSet Table_SourceLineageTag = new CompatibilityRestrictionSet(1550);

		// Token: 0x0400061C RID: 1564
		public static readonly CompatibilityRestrictionSet Table_SystemManaged = new CompatibilityRestrictionSet(1562);

		// Token: 0x0400061D RID: 1565
		public static readonly CompatibilityRestrictionSet Table_ExcludeFromAutomaticAggregations = new CompatibilityRestrictionSet(1572);

		// Token: 0x0400061E RID: 1566
		public static readonly CompatibilityRestrictionSet Column_EncodingHint = new CompatibilityRestrictionSet(1400);

		// Token: 0x0400061F RID: 1567
		public static readonly CompatibilityRestrictionSet Column_RelatedColumnDetails = new CompatibilityRestrictionSet(new KeyValuePair<CompatibilityMode, int>[]
		{
			new KeyValuePair<CompatibilityMode, int>(CompatibilityMode.PowerBI, 1400)
		});

		// Token: 0x04000620 RID: 1568
		public static readonly CompatibilityRestrictionSet Column_AlternateOf = new CompatibilityRestrictionSet(1460);

		// Token: 0x04000621 RID: 1569
		public static readonly CompatibilityRestrictionSet Column_LineageTag = new CompatibilityRestrictionSet(1540);

		// Token: 0x04000622 RID: 1570
		public static readonly CompatibilityRestrictionSet Column_SourceLineageTag = new CompatibilityRestrictionSet(1550);

		// Token: 0x04000623 RID: 1571
		public static readonly CompatibilityRestrictionSet Column_EvaluationBehavior = new CompatibilityRestrictionSet(1000000);

		// Token: 0x04000624 RID: 1572
		public static readonly CompatibilityRestrictionSet CalculatedColumn_EvaluationBehavior = new CompatibilityRestrictionSet(1000000);

		// Token: 0x04000625 RID: 1573
		public static readonly CompatibilityRestrictionSet Partition_RetainDataTillForceCalculate = new CompatibilityRestrictionSet(1400);

		// Token: 0x04000626 RID: 1574
		public static readonly CompatibilityRestrictionSet Partition_RangeStart = new CompatibilityRestrictionSet(1450);

		// Token: 0x04000627 RID: 1575
		public static readonly CompatibilityRestrictionSet Partition_RangeEnd = new CompatibilityRestrictionSet(1450);

		// Token: 0x04000628 RID: 1576
		public static readonly CompatibilityRestrictionSet Partition_RangeGranularity = new CompatibilityRestrictionSet(1450);

		// Token: 0x04000629 RID: 1577
		public static readonly CompatibilityRestrictionSet Partition_RefreshBookmark = new CompatibilityRestrictionSet(1450);

		// Token: 0x0400062A RID: 1578
		public static readonly CompatibilityRestrictionSet Partition_QueryGroup = new CompatibilityRestrictionSet(1480);

		// Token: 0x0400062B RID: 1579
		public static readonly CompatibilityRestrictionSet Partition_ExpressionSource = new CompatibilityRestrictionSet(1530);

		// Token: 0x0400062C RID: 1580
		public static readonly CompatibilityRestrictionSet Partition_MAttributes = new CompatibilityRestrictionSet(1535);

		// Token: 0x0400062D RID: 1581
		public static readonly CompatibilityRestrictionSet Partition_DataCoverageDefinition = new CompatibilityRestrictionSet(1603);

		// Token: 0x0400062E RID: 1582
		public static readonly CompatibilityRestrictionSet Partition_SchemaName = new CompatibilityRestrictionSet(1604);

		// Token: 0x0400062F RID: 1583
		public static readonly CompatibilityRestrictionSet Measure_DetailRowsDefinition = new CompatibilityRestrictionSet(1400);

		// Token: 0x04000630 RID: 1584
		public static readonly CompatibilityRestrictionSet Measure_DataCategory = new CompatibilityRestrictionSet(1455);

		// Token: 0x04000631 RID: 1585
		public static readonly CompatibilityRestrictionSet Measure_FormatStringDefinition = new CompatibilityRestrictionSet(1601);

		// Token: 0x04000632 RID: 1586
		public static readonly CompatibilityRestrictionSet Measure_LineageTag = new CompatibilityRestrictionSet(1540);

		// Token: 0x04000633 RID: 1587
		public static readonly CompatibilityRestrictionSet Measure_SourceLineageTag = new CompatibilityRestrictionSet(1550);

		// Token: 0x04000634 RID: 1588
		public static readonly CompatibilityRestrictionSet Hierarchy_HideMembers = new CompatibilityRestrictionSet(1400);

		// Token: 0x04000635 RID: 1589
		public static readonly CompatibilityRestrictionSet Hierarchy_LineageTag = new CompatibilityRestrictionSet(1540);

		// Token: 0x04000636 RID: 1590
		public static readonly CompatibilityRestrictionSet Hierarchy_SourceLineageTag = new CompatibilityRestrictionSet(1550);

		// Token: 0x04000637 RID: 1591
		public static readonly CompatibilityRestrictionSet Level_LineageTag = new CompatibilityRestrictionSet(1540);

		// Token: 0x04000638 RID: 1592
		public static readonly CompatibilityRestrictionSet Level_SourceLineageTag = new CompatibilityRestrictionSet(1550);

		// Token: 0x04000639 RID: 1593
		public static readonly CompatibilityRestrictionSet ObjectTranslation_Altered = new CompatibilityRestrictionSet(1571);

		// Token: 0x0400063A RID: 1594
		public static readonly CompatibilityRestrictionSet LinguisticMetadata_ContentType = new CompatibilityRestrictionSet(1465);

		// Token: 0x0400063B RID: 1595
		public static readonly CompatibilityRestrictionSet TablePermission_MetadataPermission = new CompatibilityRestrictionSet(1400);

		// Token: 0x0400063C RID: 1596
		public static readonly CompatibilityRestrictionSet Variation = new CompatibilityRestrictionSet(new KeyValuePair<CompatibilityMode, int>[]
		{
			new KeyValuePair<CompatibilityMode, int>(CompatibilityMode.PowerBI, -1),
			new KeyValuePair<CompatibilityMode, int>(CompatibilityMode.AnalysisServices, 1400),
			new KeyValuePair<CompatibilityMode, int>(CompatibilityMode.Excel, 1400)
		});

		// Token: 0x0400063D RID: 1597
		public static readonly CompatibilityRestrictionSet Set = new CompatibilityRestrictionSet(new KeyValuePair<CompatibilityMode, int>[]
		{
			new KeyValuePair<CompatibilityMode, int>(CompatibilityMode.PowerBI, 1400)
		});

		// Token: 0x0400063E RID: 1598
		public static readonly CompatibilityRestrictionSet PerspectiveSet = new CompatibilityRestrictionSet(new KeyValuePair<CompatibilityMode, int>[]
		{
			new KeyValuePair<CompatibilityMode, int>(CompatibilityMode.PowerBI, 1400)
		});

		// Token: 0x0400063F RID: 1599
		public static readonly CompatibilityRestrictionSet ExtendedProperty = new CompatibilityRestrictionSet(1400);

		// Token: 0x04000640 RID: 1600
		public static readonly CompatibilityRestrictionSet StringExtendedProperty = new CompatibilityRestrictionSet(1400);

		// Token: 0x04000641 RID: 1601
		public static readonly CompatibilityRestrictionSet JsonExtendedProperty = new CompatibilityRestrictionSet(1400);

		// Token: 0x04000642 RID: 1602
		public static readonly CompatibilityRestrictionSet NamedExpression = new CompatibilityRestrictionSet(1400);

		// Token: 0x04000643 RID: 1603
		public static readonly CompatibilityRestrictionSet NamedExpression_QueryGroup = new CompatibilityRestrictionSet(1480);

		// Token: 0x04000644 RID: 1604
		public static readonly CompatibilityRestrictionSet NamedExpression_ParameterValuesColumn = new CompatibilityRestrictionSet(1545);

		// Token: 0x04000645 RID: 1605
		public static readonly CompatibilityRestrictionSet NamedExpression_MAttributes = new CompatibilityRestrictionSet(1535);

		// Token: 0x04000646 RID: 1606
		public static readonly CompatibilityRestrictionSet NamedExpression_LineageTag = new CompatibilityRestrictionSet(1540);

		// Token: 0x04000647 RID: 1607
		public static readonly CompatibilityRestrictionSet NamedExpression_SourceLineageTag = new CompatibilityRestrictionSet(1550);

		// Token: 0x04000648 RID: 1608
		public static readonly CompatibilityRestrictionSet NamedExpression_RemoteParameterName = new CompatibilityRestrictionSet(1570);

		// Token: 0x04000649 RID: 1609
		public static readonly CompatibilityRestrictionSet NamedExpression_ExpressionSource = new CompatibilityRestrictionSet(1570);

		// Token: 0x0400064A RID: 1610
		public static readonly CompatibilityRestrictionSet ColumnPermission = new CompatibilityRestrictionSet(1400);

		// Token: 0x0400064B RID: 1611
		public static readonly CompatibilityRestrictionSet DetailRowsDefinition = new CompatibilityRestrictionSet(1400);

		// Token: 0x0400064C RID: 1612
		public static readonly CompatibilityRestrictionSet RelatedColumnDetails = new CompatibilityRestrictionSet(new KeyValuePair<CompatibilityMode, int>[]
		{
			new KeyValuePair<CompatibilityMode, int>(CompatibilityMode.PowerBI, 1400)
		});

		// Token: 0x0400064D RID: 1613
		public static readonly CompatibilityRestrictionSet GroupByColumn = new CompatibilityRestrictionSet(new KeyValuePair<CompatibilityMode, int>[]
		{
			new KeyValuePair<CompatibilityMode, int>(CompatibilityMode.PowerBI, 1400)
		});

		// Token: 0x0400064E RID: 1614
		public static readonly CompatibilityRestrictionSet CalculationGroup = new CompatibilityRestrictionSet(1470);

		// Token: 0x0400064F RID: 1615
		public static readonly CompatibilityRestrictionSet CalculationItem = new CompatibilityRestrictionSet(1470);

		// Token: 0x04000650 RID: 1616
		public static readonly CompatibilityRestrictionSet CalculationItem_Ordinal = new CompatibilityRestrictionSet(1500);

		// Token: 0x04000651 RID: 1617
		public static readonly CompatibilityRestrictionSet AlternateOf = new CompatibilityRestrictionSet(1460);

		// Token: 0x04000652 RID: 1618
		public static readonly CompatibilityRestrictionSet RefreshPolicy = new CompatibilityRestrictionSet(1450);

		// Token: 0x04000653 RID: 1619
		public static readonly CompatibilityRestrictionSet RefreshPolicy_Mode = new CompatibilityRestrictionSet(1565);

		// Token: 0x04000654 RID: 1620
		public static readonly CompatibilityRestrictionSet BasicRefreshPolicy = new CompatibilityRestrictionSet(1450);

		// Token: 0x04000655 RID: 1621
		public static readonly CompatibilityRestrictionSet FormatStringDefinition = new CompatibilityRestrictionSet(1470);

		// Token: 0x04000656 RID: 1622
		public static readonly CompatibilityRestrictionSet QueryGroup = new CompatibilityRestrictionSet(1480);

		// Token: 0x04000657 RID: 1623
		public static readonly CompatibilityRestrictionSet AnalyticsAIMetadata = new CompatibilityRestrictionSet(1000000);

		// Token: 0x04000658 RID: 1624
		public static readonly CompatibilityRestrictionSet ChangedProperty = new CompatibilityRestrictionSet(1567);

		// Token: 0x04000659 RID: 1625
		public static readonly CompatibilityRestrictionSet ExcludedArtifact = new CompatibilityRestrictionSet(1000000);

		// Token: 0x0400065A RID: 1626
		public static readonly CompatibilityRestrictionSet DataCoverageDefinition = new CompatibilityRestrictionSet(1603);

		// Token: 0x0400065B RID: 1627
		public static readonly CompatibilityRestrictionSet CalculationGroupExpression = new CompatibilityRestrictionSet(1605);

		// Token: 0x0400065C RID: 1628
		public static readonly CompatibilityRestrictionSet Calendar = new CompatibilityRestrictionSet(1000000);

		// Token: 0x0400065D RID: 1629
		public static readonly CompatibilityRestrictionSet TimeUnitColumnAssociation = new CompatibilityRestrictionSet(1000000);

		// Token: 0x0400065E RID: 1630
		public static readonly CompatibilityRestrictionSet CalendarColumnReference = new CompatibilityRestrictionSet(1000000);

		// Token: 0x0400065F RID: 1631
		public static readonly CompatibilityRestrictionSet Function = new CompatibilityRestrictionSet(int.MaxValue);

		// Token: 0x04000660 RID: 1632
		public static readonly CompatibilityRestrictionSet BindingInfo = new CompatibilityRestrictionSet(1000000);

		// Token: 0x04000661 RID: 1633
		public static readonly CompatibilityRestrictionSet DataBindingHint = new CompatibilityRestrictionSet(1000000);

		// Token: 0x04000662 RID: 1634
		public static readonly CompatibilityRestrictionSet Feature_AdaptiveCaching = new CompatibilityRestrictionSet(1564);

		// Token: 0x04000663 RID: 1635
		public static readonly CompatibilityRestrictionSet Feature_DefaultMeasureAsDefaultMemberInMDX = new CompatibilityRestrictionSet(1602);

		// Token: 0x04000664 RID: 1636
		public static readonly CompatibilityRestrictionSet Feature_UpdatedMultiSelectionInCalculationGroups = new CompatibilityRestrictionSet(1607);
	}
}
