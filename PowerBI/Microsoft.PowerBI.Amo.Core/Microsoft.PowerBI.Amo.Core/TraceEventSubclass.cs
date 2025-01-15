using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000C4 RID: 196
	[Guid("704490F9-CB5D-442a-9FF8-7B81E2BB5F18")]
	public enum TraceEventSubclass
	{
		// Token: 0x0400059F RID: 1439
		NotAvailable,
		// Token: 0x040005A0 RID: 1440
		InstanceShutdown,
		// Token: 0x040005A1 RID: 1441
		InstanceStarted,
		// Token: 0x040005A2 RID: 1442
		InstancePaused,
		// Token: 0x040005A3 RID: 1443
		InstanceContinued,
		// Token: 0x040005A4 RID: 1444
		Backup,
		// Token: 0x040005A5 RID: 1445
		Restore,
		// Token: 0x040005A6 RID: 1446
		Synchronize,
		// Token: 0x040005A7 RID: 1447
		Process,
		// Token: 0x040005A8 RID: 1448
		Merge,
		// Token: 0x040005A9 RID: 1449
		Delete,
		// Token: 0x040005AA RID: 1450
		DeleteOldAggregations,
		// Token: 0x040005AB RID: 1451
		Rebuild,
		// Token: 0x040005AC RID: 1452
		Commit,
		// Token: 0x040005AD RID: 1453
		Rollback,
		// Token: 0x040005AE RID: 1454
		CreateIndexes,
		// Token: 0x040005AF RID: 1455
		CreateTable,
		// Token: 0x040005B0 RID: 1456
		InsertInto,
		// Token: 0x040005B1 RID: 1457
		Transaction,
		// Token: 0x040005B2 RID: 1458
		Initialize,
		// Token: 0x040005B3 RID: 1459
		Discretize,
		// Token: 0x040005B4 RID: 1460
		Query,
		// Token: 0x040005B5 RID: 1461
		CreateView,
		// Token: 0x040005B6 RID: 1462
		WriteData,
		// Token: 0x040005B7 RID: 1463
		ReadData,
		// Token: 0x040005B8 RID: 1464
		GroupData,
		// Token: 0x040005B9 RID: 1465
		GroupDataRecord,
		// Token: 0x040005BA RID: 1466
		BuildIndex,
		// Token: 0x040005BB RID: 1467
		Aggregate,
		// Token: 0x040005BC RID: 1468
		BuildDecode,
		// Token: 0x040005BD RID: 1469
		WriteDecode,
		// Token: 0x040005BE RID: 1470
		BuildDataMiningDecode,
		// Token: 0x040005BF RID: 1471
		ExecuteSql,
		// Token: 0x040005C0 RID: 1472
		ExecuteModifiedSql,
		// Token: 0x040005C1 RID: 1473
		Connecting,
		// Token: 0x040005C2 RID: 1474
		BuildAggregationsAndIndexes,
		// Token: 0x040005C3 RID: 1475
		MergeAggregationsOnDisk,
		// Token: 0x040005C4 RID: 1476
		BuildIndexForRigidAggregations,
		// Token: 0x040005C5 RID: 1477
		BuildIndexForFlexibleAggregations,
		// Token: 0x040005C6 RID: 1478
		WriteAggregationsAndIndexes,
		// Token: 0x040005C7 RID: 1479
		WriteSegment,
		// Token: 0x040005C8 RID: 1480
		DataMiningProgress,
		// Token: 0x040005C9 RID: 1481
		ReadBufferFullReport,
		// Token: 0x040005CA RID: 1482
		ProactiveCacheConversion,
		// Token: 0x040005CB RID: 1483
		BuildProcessingSchedule,
		// Token: 0x040005CC RID: 1484
		MdxQuery,
		// Token: 0x040005CD RID: 1485
		DmxQuery,
		// Token: 0x040005CE RID: 1486
		SqlQuery,
		// Token: 0x040005CF RID: 1487
		Create,
		// Token: 0x040005D0 RID: 1488
		Alter,
		// Token: 0x040005D1 RID: 1489
		DesignAggregations,
		// Token: 0x040005D2 RID: 1490
		WBInsert,
		// Token: 0x040005D3 RID: 1491
		WBUpdate,
		// Token: 0x040005D4 RID: 1492
		WBDelete,
		// Token: 0x040005D5 RID: 1493
		MergePartitions,
		// Token: 0x040005D6 RID: 1494
		Subscribe,
		// Token: 0x040005D7 RID: 1495
		Batch,
		// Token: 0x040005D8 RID: 1496
		BeginTransaction,
		// Token: 0x040005D9 RID: 1497
		CommitTransaction,
		// Token: 0x040005DA RID: 1498
		RollbackTransaction,
		// Token: 0x040005DB RID: 1499
		GetTransactionState,
		// Token: 0x040005DC RID: 1500
		Cancel,
		// Token: 0x040005DD RID: 1501
		Import80MiningModels,
		// Token: 0x040005DE RID: 1502
		Other,
		// Token: 0x040005DF RID: 1503
		DiscoverConnections,
		// Token: 0x040005E0 RID: 1504
		DiscoverSessions,
		// Token: 0x040005E1 RID: 1505
		DiscoverTransactions,
		// Token: 0x040005E2 RID: 1506
		DiscoverDatabaseConnections,
		// Token: 0x040005E3 RID: 1507
		DiscoverJobs,
		// Token: 0x040005E4 RID: 1508
		DiscoverLocks,
		// Token: 0x040005E5 RID: 1509
		DiscoverPerformanceCounters,
		// Token: 0x040005E6 RID: 1510
		DiscoverMemoryUsage,
		// Token: 0x040005E7 RID: 1511
		DiscoverJobProgress,
		// Token: 0x040005E8 RID: 1512
		DiscoverMemoryGrant,
		// Token: 0x040005E9 RID: 1513
		SchemaCatalogs,
		// Token: 0x040005EA RID: 1514
		SchemaTables,
		// Token: 0x040005EB RID: 1515
		SchemaColumns,
		// Token: 0x040005EC RID: 1516
		SchemaProviderTypes,
		// Token: 0x040005ED RID: 1517
		SchemaCubes,
		// Token: 0x040005EE RID: 1518
		SchemaDimensions,
		// Token: 0x040005EF RID: 1519
		SchemaHierarchies,
		// Token: 0x040005F0 RID: 1520
		SchemaLevels,
		// Token: 0x040005F1 RID: 1521
		SchemaMeasures,
		// Token: 0x040005F2 RID: 1522
		SchemaProperties,
		// Token: 0x040005F3 RID: 1523
		SchemaMembers,
		// Token: 0x040005F4 RID: 1524
		SchemaFunctions,
		// Token: 0x040005F5 RID: 1525
		SchemaActions,
		// Token: 0x040005F6 RID: 1526
		SchemaSets,
		// Token: 0x040005F7 RID: 1527
		DiscoverInstances,
		// Token: 0x040005F8 RID: 1528
		SchemaKpis,
		// Token: 0x040005F9 RID: 1529
		SchemaMeasureGroups,
		// Token: 0x040005FA RID: 1530
		SchemaCommands,
		// Token: 0x040005FB RID: 1531
		SchemaMiningServices,
		// Token: 0x040005FC RID: 1532
		SchemaMiningServiceParameters,
		// Token: 0x040005FD RID: 1533
		SchemaMiningFunctions,
		// Token: 0x040005FE RID: 1534
		SchemaMiningModelContent,
		// Token: 0x040005FF RID: 1535
		SchemaMiningModelXml,
		// Token: 0x04000600 RID: 1536
		SchemaMiningModels,
		// Token: 0x04000601 RID: 1537
		SchemaMiningColumns,
		// Token: 0x04000602 RID: 1538
		DiscoverDataSources,
		// Token: 0x04000603 RID: 1539
		DiscoverProperties,
		// Token: 0x04000604 RID: 1540
		DiscoverSchemaRowsets,
		// Token: 0x04000605 RID: 1541
		DiscoverEnumerators,
		// Token: 0x04000606 RID: 1542
		DiscoverKeywords,
		// Token: 0x04000607 RID: 1543
		DiscoverLiterals,
		// Token: 0x04000608 RID: 1544
		DiscoverXmlMetadata,
		// Token: 0x04000609 RID: 1545
		DiscoverTraces,
		// Token: 0x0400060A RID: 1546
		DiscoverTraceDefinitionProviderInfo,
		// Token: 0x0400060B RID: 1547
		DiscoverTraceColumns,
		// Token: 0x0400060C RID: 1548
		DiscoverTraceEventCategories,
		// Token: 0x0400060D RID: 1549
		SchemaMiningStructures,
		// Token: 0x0400060E RID: 1550
		SchemaMiningStructureColumns,
		// Token: 0x0400060F RID: 1551
		DiscoverMasterKey,
		// Token: 0x04000610 RID: 1552
		SchemaInputDataSources,
		// Token: 0x04000611 RID: 1553
		DiscoverLocations,
		// Token: 0x04000612 RID: 1554
		DiscoverPartitionDimensionStat,
		// Token: 0x04000613 RID: 1555
		DiscoverPartitionStat,
		// Token: 0x04000614 RID: 1556
		DiscoverDimensionStat,
		// Token: 0x04000615 RID: 1557
		ProactiveCachingBegin,
		// Token: 0x04000616 RID: 1558
		ProactiveCachingEnd,
		// Token: 0x04000617 RID: 1559
		FlightRecorderStarted,
		// Token: 0x04000618 RID: 1560
		FlightRecorderStopped,
		// Token: 0x04000619 RID: 1561
		ConfigurationPropertiesUpdated,
		// Token: 0x0400061A RID: 1562
		SqlTrace,
		// Token: 0x0400061B RID: 1563
		ObjectCreated,
		// Token: 0x0400061C RID: 1564
		ObjectDeleted,
		// Token: 0x0400061D RID: 1565
		ObjectAltered,
		// Token: 0x0400061E RID: 1566
		ProactiveCachingPollingBegin,
		// Token: 0x0400061F RID: 1567
		ProactiveCachingPollingEnd,
		// Token: 0x04000620 RID: 1568
		FlightRecorderSnapshotBegin,
		// Token: 0x04000621 RID: 1569
		FlightRecorderSnapshotEnd,
		// Token: 0x04000622 RID: 1570
		ProactiveCachingNotifiableObjectUpdated,
		// Token: 0x04000623 RID: 1571
		LazyProcessingStartProcessing,
		// Token: 0x04000624 RID: 1572
		LazyProcessingProcessingComplete,
		// Token: 0x04000625 RID: 1573
		SessionOpenedEventBegin,
		// Token: 0x04000626 RID: 1574
		SessionOpenedEventEnd,
		// Token: 0x04000627 RID: 1575
		SessionClosingEventBegin,
		// Token: 0x04000628 RID: 1576
		SessionClosingEventEnd,
		// Token: 0x04000629 RID: 1577
		CubeOpenedEventBegin,
		// Token: 0x0400062A RID: 1578
		CubeOpenedEventEnd,
		// Token: 0x0400062B RID: 1579
		CubeClosingEventBegin,
		// Token: 0x0400062C RID: 1580
		CubeClosingEventEnd,
		// Token: 0x0400062D RID: 1581
		GetData,
		// Token: 0x0400062E RID: 1582
		ProcessCalculatedMembers,
		// Token: 0x0400062F RID: 1583
		PostOrder,
		// Token: 0x04000630 RID: 1584
		SerializeAxes,
		// Token: 0x04000631 RID: 1585
		SerializeCells,
		// Token: 0x04000632 RID: 1586
		SerializeSqlRowset,
		// Token: 0x04000633 RID: 1587
		SerializeFlattenedRowset,
		// Token: 0x04000634 RID: 1588
		CacheData,
		// Token: 0x04000635 RID: 1589
		NonCacheData,
		// Token: 0x04000636 RID: 1590
		InternalData,
		// Token: 0x04000637 RID: 1591
		SqlData,
		// Token: 0x04000638 RID: 1592
		MeasureGroupStructuralChange,
		// Token: 0x04000639 RID: 1593
		MeasureGroupDeletion,
		// Token: 0x0400063A RID: 1594
		GetDataFromMeasureGroupCache,
		// Token: 0x0400063B RID: 1595
		GetDataFromFlatCache,
		// Token: 0x0400063C RID: 1596
		GetDataFromCalculationCache,
		// Token: 0x0400063D RID: 1597
		GetDataFromPersistedCache,
		// Token: 0x0400063E RID: 1598
		Detach,
		// Token: 0x0400063F RID: 1599
		Attach,
		// Token: 0x04000640 RID: 1600
		AnalyzeEncodeData,
		// Token: 0x04000641 RID: 1601
		CompressSegment,
		// Token: 0x04000642 RID: 1602
		WriteTableColumn,
		// Token: 0x04000643 RID: 1603
		RelationshipBuildPrepare,
		// Token: 0x04000644 RID: 1604
		BuildRelationshipSegment,
		// Token: 0x04000645 RID: 1605
		SchemaMeasureGroupDimensions,
		// Token: 0x04000646 RID: 1606
		Load,
		// Token: 0x04000647 RID: 1607
		MetadataLoad,
		// Token: 0x04000648 RID: 1608
		DataLoad,
		// Token: 0x04000649 RID: 1609
		PostLoad,
		// Token: 0x0400064A RID: 1610
		MetadataTraversalDuringBackup,
		// Token: 0x0400064B RID: 1611
		SetAuthContext,
		// Token: 0x0400064C RID: 1612
		ImageLoad,
		// Token: 0x0400064D RID: 1613
		ImageSave,
		// Token: 0x0400064E RID: 1614
		TransactionAbortRequested,
		// Token: 0x0400064F RID: 1615
		VertiPaqScan,
		// Token: 0x04000650 RID: 1616
		TabularQuery,
		// Token: 0x04000651 RID: 1617
		VertiPaq,
		// Token: 0x04000652 RID: 1618
		HierarchyProcessing,
		// Token: 0x04000653 RID: 1619
		VertiPaqScanInternal,
		// Token: 0x04000654 RID: 1620
		TabularQueryInternal,
		// Token: 0x04000655 RID: 1621
		SwitchingDictionary,
		// Token: 0x04000656 RID: 1622
		MdxScript,
		// Token: 0x04000657 RID: 1623
		MdxScriptCommand,
		// Token: 0x04000658 RID: 1624
		DiscoverXEventTraceDefinition,
		// Token: 0x04000659 RID: 1625
		UserHierarchyProcessingQuery,
		// Token: 0x0400065A RID: 1626
		UserHierarchyProcessingQueryInternal,
		// Token: 0x0400065B RID: 1627
		DAXQuery,
		// Token: 0x0400065C RID: 1628
		DISCOVER_COMMANDS,
		// Token: 0x0400065D RID: 1629
		DISCOVER_COMMAND_OBJECTS,
		// Token: 0x0400065E RID: 1630
		DISCOVER_OBJECT_ACTIVITY,
		// Token: 0x0400065F RID: 1631
		DISCOVER_OBJECT_MEMORY_USAGE,
		// Token: 0x04000660 RID: 1632
		DISCOVER_XEVENT_TRACE_DEFINITION,
		// Token: 0x04000661 RID: 1633
		DISCOVER_STORAGE_TABLES,
		// Token: 0x04000662 RID: 1634
		DISCOVER_STORAGE_TABLE_COLUMNS,
		// Token: 0x04000663 RID: 1635
		DISCOVER_STORAGE_TABLE_COLUMN_SEGMENTS,
		// Token: 0x04000664 RID: 1636
		DISCOVER_CALC_DEPENDENCY,
		// Token: 0x04000665 RID: 1637
		DISCOVER_CSDL_METADATA,
		// Token: 0x04000666 RID: 1638
		VertiPaqCacheExactMatch,
		// Token: 0x04000667 RID: 1639
		InitEvalNodeStart,
		// Token: 0x04000668 RID: 1640
		InitEvalNodeEnd,
		// Token: 0x04000669 RID: 1641
		BuildEvalNodeStart,
		// Token: 0x0400066A RID: 1642
		BuildEvalNodeEnd,
		// Token: 0x0400066B RID: 1643
		PrepareEvalNodeStart,
		// Token: 0x0400066C RID: 1644
		PrepareEvalNodeEnd,
		// Token: 0x0400066D RID: 1645
		RunEvalNodeStart,
		// Token: 0x0400066E RID: 1646
		RunEvalNodeEnd,
		// Token: 0x0400066F RID: 1647
		BuildEvalNodeEliminatedEmptyCalculations,
		// Token: 0x04000670 RID: 1648
		BuildEvalNodeSubtractedCalculationSpaces,
		// Token: 0x04000671 RID: 1649
		BuildEvalNodeAppliedVisualTotals,
		// Token: 0x04000672 RID: 1650
		BuildEvalNodeDetectedCachedEvaluationNode,
		// Token: 0x04000673 RID: 1651
		BuildEvalNodeDetectedCachedEvaluationResults,
		// Token: 0x04000674 RID: 1652
		PrepareEvalNodeBeginPrepareEvaluationItem,
		// Token: 0x04000675 RID: 1653
		PrepareEvalNodeFinishedPrepareEvaluationItem,
		// Token: 0x04000676 RID: 1654
		RunEvalNodeFinishedCalculatingItem,
		// Token: 0x04000677 RID: 1655
		DAXVertiPaqLogicalPlan,
		// Token: 0x04000678 RID: 1656
		DAXVertiPaqPhysicalPlan,
		// Token: 0x04000679 RID: 1657
		DAXDirectQueryAlgebrizerTree,
		// Token: 0x0400067A RID: 1658
		DAXDirectQueryLogicalPlan,
		// Token: 0x0400067B RID: 1659
		CloneDatabase,
		// Token: 0x0400067C RID: 1660
		RGWLGroupExceedHighMemoryLimit,
		// Token: 0x0400067D RID: 1661
		RGWLGroupExceedHardMemoryLimit,
		// Token: 0x0400067E RID: 1662
		RGWLGroupBelowHighMemoryLimit,
		// Token: 0x0400067F RID: 1663
		RGWLGroupBelowHardMemoryLimit,
		// Token: 0x04000680 RID: 1664
		VertiPaqScanQueryPlan,
		// Token: 0x04000681 RID: 1665
		VertiPaqScanLocal,
		// Token: 0x04000682 RID: 1666
		VertiPaqScanRemote,
		// Token: 0x04000683 RID: 1667
		Interpret,
		// Token: 0x04000684 RID: 1668
		TabularCreate,
		// Token: 0x04000685 RID: 1669
		TabularAlter,
		// Token: 0x04000686 RID: 1670
		TabularDelete,
		// Token: 0x04000687 RID: 1671
		TabularRefresh,
		// Token: 0x04000688 RID: 1672
		ExtAuth,
		// Token: 0x04000689 RID: 1673
		Dbcc,
		// Token: 0x0400068A RID: 1674
		TabularRename,
		// Token: 0x0400068B RID: 1675
		TabularSequencePoint,
		// Token: 0x0400068C RID: 1676
		TabularCommit,
		// Token: 0x0400068D RID: 1677
		TabularSave,
		// Token: 0x0400068E RID: 1678
		DISCOVER_RESOURCE_POOLS,
		// Token: 0x0400068F RID: 1679
		DISCOVER_RING_BUFFERS,
		// Token: 0x04000690 RID: 1680
		TabularSchemaModel,
		// Token: 0x04000691 RID: 1681
		TabularSchemaDataSources,
		// Token: 0x04000692 RID: 1682
		TabularSchemaTables,
		// Token: 0x04000693 RID: 1683
		TabularSchemaColumns,
		// Token: 0x04000694 RID: 1684
		TabularSchemaAttributeHierarchies,
		// Token: 0x04000695 RID: 1685
		TabularSchemaPartitions,
		// Token: 0x04000696 RID: 1686
		TabularSchemaRelationships,
		// Token: 0x04000697 RID: 1687
		TabularSchemaMeasures,
		// Token: 0x04000698 RID: 1688
		TabularSchemaHierarchies,
		// Token: 0x04000699 RID: 1689
		TabularSchemaLevels,
		// Token: 0x0400069A RID: 1690
		TabularSchemaTableStorages,
		// Token: 0x0400069B RID: 1691
		TabularSchemaColumnStorages,
		// Token: 0x0400069C RID: 1692
		TabularSchemaPartitionStorages,
		// Token: 0x0400069D RID: 1693
		TabularSchemaSegmentMapStorages,
		// Token: 0x0400069E RID: 1694
		TabularSchemaDictionaryStorages,
		// Token: 0x0400069F RID: 1695
		TabularSchemaColumnPartitionStorages,
		// Token: 0x040006A0 RID: 1696
		TabularSchemaRelationshipStorages,
		// Token: 0x040006A1 RID: 1697
		TabularSchemaRelationshipIndexStorages,
		// Token: 0x040006A2 RID: 1698
		TabularSchemaAttributeHierarchyStorages,
		// Token: 0x040006A3 RID: 1699
		TabularSchemaHierarchyStorages,
		// Token: 0x040006A4 RID: 1700
		TabularSchemaKpis,
		// Token: 0x040006A5 RID: 1701
		TabularSchemaStorageFolders,
		// Token: 0x040006A6 RID: 1702
		TabularSchemaStorageFiles,
		// Token: 0x040006A7 RID: 1703
		TabularSchemaSegmentStorages,
		// Token: 0x040006A8 RID: 1704
		TabularSchemaCultures,
		// Token: 0x040006A9 RID: 1705
		TabularSchemaObjectTranslations,
		// Token: 0x040006AA RID: 1706
		TabularSchemaLinguisticMetadata,
		// Token: 0x040006AB RID: 1707
		TabularSchemaAnnotations,
		// Token: 0x040006AC RID: 1708
		TabularUpgrade,
		// Token: 0x040006AD RID: 1709
		TabularMergePartitions,
		// Token: 0x040006AE RID: 1710
		DisableDatabase,
		// Token: 0x040006AF RID: 1711
		TokenizationStoreProcessing,
		// Token: 0x040006B0 RID: 1712
		CheckTabularDataStructure,
		// Token: 0x040006B1 RID: 1713
		CheckColumnDataForDuplicatesOrNullValues,
		// Token: 0x040006B2 RID: 1714
		JSON,
		// Token: 0x040006B3 RID: 1715
		DISCOVER_XEVENT_PACKAGES,
		// Token: 0x040006B4 RID: 1716
		DISCOVER_XEVENT_OBJECTS,
		// Token: 0x040006B5 RID: 1717
		DISCOVER_XEVENT_OBJECT_COLUMNS,
		// Token: 0x040006B6 RID: 1718
		DISCOVER_XEVENT_SESSION_TARGETS,
		// Token: 0x040006B7 RID: 1719
		TabularSchemaPerspectives,
		// Token: 0x040006B8 RID: 1720
		TabularSchemaPerspectiveTables,
		// Token: 0x040006B9 RID: 1721
		TabularSchemaPerspectiveColumns,
		// Token: 0x040006BA RID: 1722
		TabularSchemaPerspectiveHierarchies,
		// Token: 0x040006BB RID: 1723
		TabularSchemaPerspectiveMeasures,
		// Token: 0x040006BC RID: 1724
		TabularSchemaRoles,
		// Token: 0x040006BD RID: 1725
		TabularSchemaRoleMemberships,
		// Token: 0x040006BE RID: 1726
		TabularSchemaTablePermissions,
		// Token: 0x040006BF RID: 1727
		TabularSchemaVariations,
		// Token: 0x040006C0 RID: 1728
		VertiPaqCacheProbe,
		// Token: 0x040006C1 RID: 1729
		VertiPaqCacheNotFound,
		// Token: 0x040006C2 RID: 1730
		JsonCommand,
		// Token: 0x040006C3 RID: 1731
		Evict,
		// Token: 0x040006C4 RID: 1732
		CommitImport,
		// Token: 0x040006C5 RID: 1733
		OpenedConnection,
		// Token: 0x040006C6 RID: 1734
		BatchVertiPaqScan,
		// Token: 0x040006C7 RID: 1735
		RewriteAttempted,
		// Token: 0x040006C8 RID: 1736
		AnalyzeRefreshPolicyImpactForTabularPartition,
		// Token: 0x040006C9 RID: 1737
		TabularSchemaSets,
		// Token: 0x040006CA RID: 1738
		TabularSchemaPerspectiveSets,
		// Token: 0x040006CB RID: 1739
		TabularSchemaExtendedProperties,
		// Token: 0x040006CC RID: 1740
		TabularSchemaExpressions,
		// Token: 0x040006CD RID: 1741
		TabularSchemaColumnPermissions,
		// Token: 0x040006CE RID: 1742
		TabularSchemaDetailRowsDefinitions,
		// Token: 0x040006CF RID: 1743
		TabularSchemaRelatedColumnDetails,
		// Token: 0x040006D0 RID: 1744
		TabularSchemaGroupByColumns,
		// Token: 0x040006D1 RID: 1745
		TabularSchemaCalculationGroups,
		// Token: 0x040006D2 RID: 1746
		TabularSchemaCalculationItems,
		// Token: 0x040006D3 RID: 1747
		TabularSchemaAlternateOfDefinitions,
		// Token: 0x040006D4 RID: 1748
		TabularSchemaRefreshPolicies,
		// Token: 0x040006D5 RID: 1749
		DiscoverPowerBIDatasources,
		// Token: 0x040006D6 RID: 1750
		TabularSchemaFormatStringDefinitions,
		// Token: 0x040006D7 RID: 1751
		DiscoverMExpressions,
		// Token: 0x040006D8 RID: 1752
		TabularSchemaPowerbiRoles,
		// Token: 0x040006D9 RID: 1753
		TabularSchemaQueryGroups,
		// Token: 0x040006DA RID: 1754
		DiscoverDBMemoryStats,
		// Token: 0x040006DB RID: 1755
		DiscoverMemoryStats,
		// Token: 0x040006DC RID: 1756
		TabularSchemaAnalyticsAimetadata,
		// Token: 0x040006DD RID: 1757
		DiscoverObjectCounters,
		// Token: 0x040006DE RID: 1758
		GraphCreated,
		// Token: 0x040006DF RID: 1759
		GraphFinished,
		// Token: 0x040006E0 RID: 1760
		RemoveDiscontinuedFeatures,
		// Token: 0x040006E1 RID: 1761
		DiscoverModelSecurity,
		// Token: 0x040006E2 RID: 1762
		ParallelSession,
		// Token: 0x040006E3 RID: 1763
		[Obsolete("Deprecated")]
		TabularSchemaPartitioningHint,
		// Token: 0x040006E4 RID: 1764
		[Obsolete("Deprecated")]
		TabularSchemaPartitioningHints,
		// Token: 0x040006E5 RID: 1765
		TabularSchemaCalculationExpressions,
		// Token: 0x040006E6 RID: 1766
		TabularSchemaDataCoverageDefinitions,
		// Token: 0x040006E7 RID: 1767
		AutoAggsTraining,
		// Token: 0x040006E8 RID: 1768
		AutoAggsCardinalityAnalysis,
		// Token: 0x040006E9 RID: 1769
		Export
	}
}
