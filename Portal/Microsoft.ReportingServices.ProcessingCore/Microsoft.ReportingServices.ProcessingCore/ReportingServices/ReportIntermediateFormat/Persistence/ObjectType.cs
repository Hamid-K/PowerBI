using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000556 RID: 1366
	public enum ObjectType
	{
		// Token: 0x040020C0 RID: 8384
		Null,
		// Token: 0x040020C1 RID: 8385
		None,
		// Token: 0x040020C2 RID: 8386
		RIFObjectArray,
		// Token: 0x040020C3 RID: 8387
		RIFObjectList,
		// Token: 0x040020C4 RID: 8388
		PrimitiveArray,
		// Token: 0x040020C5 RID: 8389
		PrimitiveList,
		// Token: 0x040020C6 RID: 8390
		PrimitiveTypedArray,
		// Token: 0x040020C7 RID: 8391
		StringRIFObjectDictionary,
		// Token: 0x040020C8 RID: 8392
		StringRIFObjectHashtable,
		// Token: 0x040020C9 RID: 8393
		NameObjectCollection,
		// Token: 0x040020CA RID: 8394
		Int32RIFObjectDictionary,
		// Token: 0x040020CB RID: 8395
		Int32PrimitiveListHashtable,
		// Token: 0x040020CC RID: 8396
		ObjectHashtableHashtable,
		// Token: 0x040020CD RID: 8397
		StringObjectHashtable,
		// Token: 0x040020CE RID: 8398
		ListOfRIFObjectRIFObjectDictionary,
		// Token: 0x040020CF RID: 8399
		RIFObjectStringHashtable,
		// Token: 0x040020D0 RID: 8400
		RecordSetInfo,
		// Token: 0x040020D1 RID: 8401
		RecordRow,
		// Token: 0x040020D2 RID: 8402
		RecordField,
		// Token: 0x040020D3 RID: 8403
		RecordSetPropertyNames,
		// Token: 0x040020D4 RID: 8404
		Nullable,
		// Token: 0x040020D5 RID: 8405
		StorageItem,
		// Token: 0x040020D6 RID: 8406
		Reference,
		// Token: 0x040020D7 RID: 8407
		ScalableDictionaryNodeReference,
		// Token: 0x040020D8 RID: 8408
		IScalableDictionaryEntry,
		// Token: 0x040020D9 RID: 8409
		ScalableDictionaryValues,
		// Token: 0x040020DA RID: 8410
		ScalableDictionaryNode,
		// Token: 0x040020DB RID: 8411
		ScalableDictionary,
		// Token: 0x040020DC RID: 8412
		StorableArray,
		// Token: 0x040020DD RID: 8413
		StorableArrayReference,
		// Token: 0x040020DE RID: 8414
		ScalableList,
		// Token: 0x040020DF RID: 8415
		Array2D,
		// Token: 0x040020E0 RID: 8416
		DataRegionInstanceReference,
		// Token: 0x040020E1 RID: 8417
		SubReportInstanceReference,
		// Token: 0x040020E2 RID: 8418
		ReportInstanceReference,
		// Token: 0x040020E3 RID: 8419
		ScopeInstanceReference,
		// Token: 0x040020E4 RID: 8420
		DataFieldRow,
		// Token: 0x040020E5 RID: 8421
		FieldImpl,
		// Token: 0x040020E6 RID: 8422
		BTreeNode,
		// Token: 0x040020E7 RID: 8423
		BTreeNodeTupleList,
		// Token: 0x040020E8 RID: 8424
		BTreeNodeTuple,
		// Token: 0x040020E9 RID: 8425
		BTreeNodeValue,
		// Token: 0x040020EA RID: 8426
		ScalableHybridListEntry,
		// Token: 0x040020EB RID: 8427
		FilterKey,
		// Token: 0x040020EC RID: 8428
		RuntimeSortHierarchyObj,
		// Token: 0x040020ED RID: 8429
		SortHierarchyStruct,
		// Token: 0x040020EE RID: 8430
		RuntimeSortFilterEventInfo,
		// Token: 0x040020EF RID: 8431
		SortFilterExpressionScopeObj,
		// Token: 0x040020F0 RID: 8432
		SortExpressionScopeInstanceHolder,
		// Token: 0x040020F1 RID: 8433
		DataAggregateObj,
		// Token: 0x040020F2 RID: 8434
		RuntimeRICollection,
		// Token: 0x040020F3 RID: 8435
		RuntimeTablixCell,
		// Token: 0x040020F4 RID: 8436
		RuntimeChartCriCell,
		// Token: 0x040020F5 RID: 8437
		RuntimeUserSortTargetInfo,
		// Token: 0x040020F6 RID: 8438
		Aggregate,
		// Token: 0x040020F7 RID: 8439
		First,
		// Token: 0x040020F8 RID: 8440
		Last,
		// Token: 0x040020F9 RID: 8441
		Sum,
		// Token: 0x040020FA RID: 8442
		Avg,
		// Token: 0x040020FB RID: 8443
		Max,
		// Token: 0x040020FC RID: 8444
		Min,
		// Token: 0x040020FD RID: 8445
		Count,
		// Token: 0x040020FE RID: 8446
		VariantVariantHashtable,
		// Token: 0x040020FF RID: 8447
		CountDistinct,
		// Token: 0x04002100 RID: 8448
		CountRows,
		// Token: 0x04002101 RID: 8449
		VarBase,
		// Token: 0x04002102 RID: 8450
		Var,
		// Token: 0x04002103 RID: 8451
		StDev,
		// Token: 0x04002104 RID: 8452
		VarP,
		// Token: 0x04002105 RID: 8453
		StDevP,
		// Token: 0x04002106 RID: 8454
		Previous,
		// Token: 0x04002107 RID: 8455
		AggregateRow,
		// Token: 0x04002108 RID: 8456
		RuntimeCells,
		// Token: 0x04002109 RID: 8457
		RuntimeExpressionInfo,
		// Token: 0x0400210A RID: 8458
		Int32StringHashtable,
		// Token: 0x0400210B RID: 8459
		RuntimeHierarchyObj,
		// Token: 0x0400210C RID: 8460
		RuntimeGroupingObj,
		// Token: 0x0400210D RID: 8461
		VariantRifObjectDictionary,
		// Token: 0x0400210E RID: 8462
		VariantListOfRifObjectDictionary,
		// Token: 0x0400210F RID: 8463
		AggregatesImpl,
		// Token: 0x04002110 RID: 8464
		RuntimeDataTablixGroupRootObj,
		// Token: 0x04002111 RID: 8465
		RuntimeDataTablixMemberObj,
		// Token: 0x04002112 RID: 8466
		RuntimeTablixMemberObj,
		// Token: 0x04002113 RID: 8467
		RuntimeDataTablixObj,
		// Token: 0x04002114 RID: 8468
		RuntimeTablixObj,
		// Token: 0x04002115 RID: 8469
		RuntimeChartObj,
		// Token: 0x04002116 RID: 8470
		RuntimeCriObj,
		// Token: 0x04002117 RID: 8471
		RuntimeTablixGroupLeafObj,
		// Token: 0x04002118 RID: 8472
		RuntimeChartCriGroupLeafObj,
		// Token: 0x04002119 RID: 8473
		CalculatedFieldWrapperImpl,
		// Token: 0x0400211A RID: 8474
		RuntimeSortDataHolder,
		// Token: 0x0400211B RID: 8475
		VariantListVariantDictionary,
		// Token: 0x0400211C RID: 8476
		BTree,
		// Token: 0x0400211D RID: 8477
		DataFieldRowReference,
		// Token: 0x0400211E RID: 8478
		RuntimeSortHierarchyObjReference,
		// Token: 0x0400211F RID: 8479
		RuntimeSortFilterEventInfoReference,
		// Token: 0x04002120 RID: 8480
		SortFilterExpressionScopeObjReference,
		// Token: 0x04002121 RID: 8481
		SortExpressionScopeInstanceHolderReference,
		// Token: 0x04002122 RID: 8482
		DataAggregateObjReference,
		// Token: 0x04002123 RID: 8483
		RuntimeRICollectionReference,
		// Token: 0x04002124 RID: 8484
		RuntimeTablixCellReference,
		// Token: 0x04002125 RID: 8485
		RuntimeChartCriCellReference,
		// Token: 0x04002126 RID: 8486
		RuntimeUserSortTargetInfoReference,
		// Token: 0x04002127 RID: 8487
		AggregateRowReference,
		// Token: 0x04002128 RID: 8488
		RuntimeCellsReference,
		// Token: 0x04002129 RID: 8489
		RuntimeHierarchyObjReference,
		// Token: 0x0400212A RID: 8490
		RuntimeGroupingObjReference,
		// Token: 0x0400212B RID: 8491
		RuntimeDataTablixGroupRootObjReference,
		// Token: 0x0400212C RID: 8492
		RuntimeDataTablixMemberObjReference,
		// Token: 0x0400212D RID: 8493
		RuntimeTablixMemberObjReference,
		// Token: 0x0400212E RID: 8494
		RuntimeTablixObjReference,
		// Token: 0x0400212F RID: 8495
		RuntimeChartObjReference,
		// Token: 0x04002130 RID: 8496
		RuntimeCriObjReference,
		// Token: 0x04002131 RID: 8497
		RuntimeTablixGroupLeafObjReference,
		// Token: 0x04002132 RID: 8498
		RuntimeChartCriGroupLeafObjReference,
		// Token: 0x04002133 RID: 8499
		RuntimeSortDataHolderReference,
		// Token: 0x04002134 RID: 8500
		StringVariantListDictionary,
		// Token: 0x04002135 RID: 8501
		RowMemberInfo,
		// Token: 0x04002136 RID: 8502
		SizeInfo,
		// Token: 0x04002137 RID: 8503
		DetailCell,
		// Token: 0x04002138 RID: 8504
		CornerCell,
		// Token: 0x04002139 RID: 8505
		MemberCell,
		// Token: 0x0400213A RID: 8506
		PageMemberCell,
		// Token: 0x0400213B RID: 8507
		BTreeNodeHierarchyObj,
		// Token: 0x0400213C RID: 8508
		BTreeNodeDataRow,
		// Token: 0x0400213D RID: 8509
		DataCellInstanceList,
		// Token: 0x0400213E RID: 8510
		IDOwner = 128,
		// Token: 0x0400213F RID: 8511
		ReportItem,
		// Token: 0x04002140 RID: 8512
		Report,
		// Token: 0x04002141 RID: 8513
		PageSection,
		// Token: 0x04002142 RID: 8514
		Line,
		// Token: 0x04002143 RID: 8515
		Rectangle,
		// Token: 0x04002144 RID: 8516
		Image,
		// Token: 0x04002145 RID: 8517
		TextBox,
		// Token: 0x04002146 RID: 8518
		SubReport,
		// Token: 0x04002147 RID: 8519
		DataRegion,
		// Token: 0x04002148 RID: 8520
		ReportHierarchyNode,
		// Token: 0x04002149 RID: 8521
		Grouping,
		// Token: 0x0400214A RID: 8522
		Sorting,
		// Token: 0x0400214B RID: 8523
		ReportItemCollection,
		// Token: 0x0400214C RID: 8524
		ReportItemIndexer,
		// Token: 0x0400214D RID: 8525
		Style,
		// Token: 0x0400214E RID: 8526
		AttributeInfo,
		// Token: 0x0400214F RID: 8527
		Visibility,
		// Token: 0x04002150 RID: 8528
		ExpressionInfo,
		// Token: 0x04002151 RID: 8529
		DataAggregateInfo,
		// Token: 0x04002152 RID: 8530
		RunningValueInfo,
		// Token: 0x04002153 RID: 8531
		Filter,
		// Token: 0x04002154 RID: 8532
		DataSource,
		// Token: 0x04002155 RID: 8533
		DataSet,
		// Token: 0x04002156 RID: 8534
		ReportQuery,
		// Token: 0x04002157 RID: 8535
		Field,
		// Token: 0x04002158 RID: 8536
		ParameterValue,
		// Token: 0x04002159 RID: 8537
		ReportSnapshot,
		// Token: 0x0400215A RID: 8538
		DocumentMapNode,
		// Token: 0x0400215B RID: 8539
		InstanceInfo,
		// Token: 0x0400215C RID: 8540
		ScopeInstance,
		// Token: 0x0400215D RID: 8541
		ReportInstance,
		// Token: 0x0400215E RID: 8542
		ParameterInfo,
		// Token: 0x0400215F RID: 8543
		ParameterInfoCollection,
		// Token: 0x04002160 RID: 8544
		Variant,
		// Token: 0x04002161 RID: 8545
		VariantList,
		// Token: 0x04002162 RID: 8546
		ValidValue,
		// Token: 0x04002163 RID: 8547
		ParameterDataSource,
		// Token: 0x04002164 RID: 8548
		ParameterDef,
		// Token: 0x04002165 RID: 8549
		ParameterBase,
		// Token: 0x04002166 RID: 8550
		ProcessingMessageList,
		// Token: 0x04002167 RID: 8551
		ProcessingMessage,
		// Token: 0x04002168 RID: 8552
		CodeClass,
		// Token: 0x04002169 RID: 8553
		String,
		// Token: 0x0400216A RID: 8554
		Action,
		// Token: 0x0400216B RID: 8555
		RenderingPagesRanges,
		// Token: 0x0400216C RID: 8556
		IntermediateFormatVersion,
		// Token: 0x0400216D RID: 8557
		ImageInfo,
		// Token: 0x0400216E RID: 8558
		ActionItem,
		// Token: 0x0400216F RID: 8559
		DataValue,
		// Token: 0x04002170 RID: 8560
		CustomReportItem,
		// Token: 0x04002171 RID: 8561
		SortFilterEventInfo,
		// Token: 0x04002172 RID: 8562
		SortFilterEventInfoMap,
		// Token: 0x04002173 RID: 8563
		EndUserSort,
		// Token: 0x04002174 RID: 8564
		ISortFilterScope,
		// Token: 0x04002175 RID: 8565
		GroupingList,
		// Token: 0x04002176 RID: 8566
		ScopeLookupTable,
		// Token: 0x04002177 RID: 8567
		Row,
		// Token: 0x04002178 RID: 8568
		Cell,
		// Token: 0x04002179 RID: 8569
		Tablix,
		// Token: 0x0400217A RID: 8570
		TablixHeader,
		// Token: 0x0400217B RID: 8571
		TablixMember,
		// Token: 0x0400217C RID: 8572
		TablixColumn,
		// Token: 0x0400217D RID: 8573
		TablixRow,
		// Token: 0x0400217E RID: 8574
		TablixCornerCell,
		// Token: 0x0400217F RID: 8575
		TablixCell,
		// Token: 0x04002180 RID: 8576
		Chart,
		// Token: 0x04002181 RID: 8577
		ChartMember,
		// Token: 0x04002182 RID: 8578
		ChartSeries,
		// Token: 0x04002183 RID: 8579
		ChartDataPoint,
		// Token: 0x04002184 RID: 8580
		ChartAxis,
		// Token: 0x04002185 RID: 8581
		AxisList,
		// Token: 0x04002186 RID: 8582
		ThreeDProperties,
		// Token: 0x04002187 RID: 8583
		PlotArea,
		// Token: 0x04002188 RID: 8584
		ChartDataLabel,
		// Token: 0x04002189 RID: 8585
		ChartDataPointValues,
		// Token: 0x0400218A RID: 8586
		ChartArea,
		// Token: 0x0400218B RID: 8587
		ChartTitleBase,
		// Token: 0x0400218C RID: 8588
		ChartTitle,
		// Token: 0x0400218D RID: 8589
		ChartAxisTitle,
		// Token: 0x0400218E RID: 8590
		ChartLegendTitle,
		// Token: 0x0400218F RID: 8591
		ChartLegend,
		// Token: 0x04002190 RID: 8592
		ChartBorderSkin,
		// Token: 0x04002191 RID: 8593
		ChartTickMarks,
		// Token: 0x04002192 RID: 8594
		ChartNoDataMessage,
		// Token: 0x04002193 RID: 8595
		ChartCustomPaletteColor,
		// Token: 0x04002194 RID: 8596
		ChartLegendColumn,
		// Token: 0x04002195 RID: 8597
		ChartLegendColumnHeader,
		// Token: 0x04002196 RID: 8598
		ChartLegendCustomItem,
		// Token: 0x04002197 RID: 8599
		ChartLegendCustomItemCell,
		// Token: 0x04002198 RID: 8600
		ChartStripLine,
		// Token: 0x04002199 RID: 8601
		ChartAxisScaleBreak,
		// Token: 0x0400219A RID: 8602
		ChartDerivedSeries,
		// Token: 0x0400219B RID: 8603
		ChartFormulaParameter,
		// Token: 0x0400219C RID: 8604
		ChartEmptyPoints,
		// Token: 0x0400219D RID: 8605
		ChartItemInLegend,
		// Token: 0x0400219E RID: 8606
		ChartSmartLabel,
		// Token: 0x0400219F RID: 8607
		ChartNoMoveDirections,
		// Token: 0x040021A0 RID: 8608
		GridLines,
		// Token: 0x040021A1 RID: 8609
		DataMember,
		// Token: 0x040021A2 RID: 8610
		CustomDataRow,
		// Token: 0x040021A3 RID: 8611
		DataCell,
		// Token: 0x040021A4 RID: 8612
		Variable,
		// Token: 0x040021A5 RID: 8613
		ExpressionInfoTypeValuePair,
		// Token: 0x040021A6 RID: 8614
		Page,
		// Token: 0x040021A7 RID: 8615
		IReferenceable,
		// Token: 0x040021A8 RID: 8616
		SubReportInstance,
		// Token: 0x040021A9 RID: 8617
		SubReportInstanceItem,
		// Token: 0x040021AA RID: 8618
		Parameter,
		// Token: 0x040021AB RID: 8619
		CultureInfo,
		// Token: 0x040021AC RID: 8620
		Declaration,
		// Token: 0x040021AD RID: 8621
		DocumentMapBeginContainer,
		// Token: 0x040021AE RID: 8622
		DocumentMapEndContainer,
		// Token: 0x040021AF RID: 8623
		OnDemandMetadata,
		// Token: 0x040021B0 RID: 8624
		GroupTreePartition,
		// Token: 0x040021B1 RID: 8625
		FieldInfo,
		// Token: 0x040021B2 RID: 8626
		DataSetInstance,
		// Token: 0x040021B3 RID: 8627
		DataRegionInstance,
		// Token: 0x040021B4 RID: 8628
		DataRegionMemberInstance,
		// Token: 0x040021B5 RID: 8629
		DataCellInstance,
		// Token: 0x040021B6 RID: 8630
		DataAggregateObjResult,
		// Token: 0x040021B7 RID: 8631
		Parameters,
		// Token: 0x040021B8 RID: 8632
		StringInt32Hashtable,
		// Token: 0x040021B9 RID: 8633
		Variables,
		// Token: 0x040021BA RID: 8634
		SubReportInfo,
		// Token: 0x040021BB RID: 8635
		StringStringHashtable,
		// Token: 0x040021BC RID: 8636
		ChartStyleContainer,
		// Token: 0x040021BD RID: 8637
		ChartMarker,
		// Token: 0x040021BE RID: 8638
		IInScopeEventSource,
		// Token: 0x040021BF RID: 8639
		IVisibilityOwner,
		// Token: 0x040021C0 RID: 8640
		ReportElementInstance,
		// Token: 0x040021C1 RID: 8641
		ReportItemInstance,
		// Token: 0x040021C2 RID: 8642
		ImageInstance,
		// Token: 0x040021C3 RID: 8643
		ActionInstance,
		// Token: 0x040021C4 RID: 8644
		ParameterInstance,
		// Token: 0x040021C5 RID: 8645
		ActionInfoWithDynamicImageMap,
		// Token: 0x040021C6 RID: 8646
		ImageMapAreaInstance,
		// Token: 0x040021C7 RID: 8647
		StyleInstance,
		// Token: 0x040021C8 RID: 8648
		StringListOfStringDictionary,
		// Token: 0x040021C9 RID: 8649
		ChartAlignType,
		// Token: 0x040021CA RID: 8650
		RIFObject,
		// Token: 0x040021CB RID: 8651
		OnDemandProcessingContext,
		// Token: 0x040021CC RID: 8652
		ObjectModelImpl,
		// Token: 0x040021CD RID: 8653
		RuntimeOnDemandDataSetObj,
		// Token: 0x040021CE RID: 8654
		ISortDataHolder,
		// Token: 0x040021CF RID: 8655
		IHierarchyObj,
		// Token: 0x040021D0 RID: 8656
		RuntimeRDLDataRegionObj,
		// Token: 0x040021D1 RID: 8657
		RuntimeCell,
		// Token: 0x040021D2 RID: 8658
		Filters,
		// Token: 0x040021D3 RID: 8659
		ReportRuntime,
		// Token: 0x040021D4 RID: 8660
		DataAggregate,
		// Token: 0x040021D5 RID: 8661
		IScope,
		// Token: 0x040021D6 RID: 8662
		IndexedExprHost,
		// Token: 0x040021D7 RID: 8663
		RuntimeGroupLeafObj,
		// Token: 0x040021D8 RID: 8664
		RuntimeGroupObj,
		// Token: 0x040021D9 RID: 8665
		RuntimeDetailObj,
		// Token: 0x040021DA RID: 8666
		IErrorContext,
		// Token: 0x040021DB RID: 8667
		RuntimeGroupRootObj,
		// Token: 0x040021DC RID: 8668
		RuntimeMemberObj,
		// Token: 0x040021DD RID: 8669
		RuntimeChartCriObj,
		// Token: 0x040021DE RID: 8670
		RuntimeDataTablixGroupLeafObj,
		// Token: 0x040021DF RID: 8671
		RuntimeOnDemandDataSetObjReference,
		// Token: 0x040021E0 RID: 8672
		IHierarchyObjReference,
		// Token: 0x040021E1 RID: 8673
		RuntimeCellReference,
		// Token: 0x040021E2 RID: 8674
		RuntimeRDLDataRegionObjReference,
		// Token: 0x040021E3 RID: 8675
		IScopeReference,
		// Token: 0x040021E4 RID: 8676
		RuntimeDataTablixGroupLeafObjReference,
		// Token: 0x040021E5 RID: 8677
		RuntimeGroupLeafObjReference,
		// Token: 0x040021E6 RID: 8678
		RuntimeGroupObjReference,
		// Token: 0x040021E7 RID: 8679
		RuntimeDetailObjReference,
		// Token: 0x040021E8 RID: 8680
		RuntimeGroupRootObjReference,
		// Token: 0x040021E9 RID: 8681
		RuntimeMemberObjReference,
		// Token: 0x040021EA RID: 8682
		RuntimeDataTablixObjReference,
		// Token: 0x040021EB RID: 8683
		RuntimeChartCriObjReference,
		// Token: 0x040021EC RID: 8684
		ISortDataHolderReference,
		// Token: 0x040021ED RID: 8685
		RuntimeDataRegionObjReference,
		// Token: 0x040021EE RID: 8686
		RuntimeDataRegionObj,
		// Token: 0x040021EF RID: 8687
		DataAggregateReference,
		// Token: 0x040021F0 RID: 8688
		StreamMemberCell,
		// Token: 0x040021F1 RID: 8689
		RPLMemberCell,
		// Token: 0x040021F2 RID: 8690
		ItemSizes,
		// Token: 0x040021F3 RID: 8691
		PageItem,
		// Token: 0x040021F4 RID: 8692
		HiddenPageItem,
		// Token: 0x040021F5 RID: 8693
		NoRowsItem,
		// Token: 0x040021F6 RID: 8694
		PageItemContainer,
		// Token: 0x040021F7 RID: 8695
		ReportBody,
		// Token: 0x040021F8 RID: 8696
		RowInfo,
		// Token: 0x040021F9 RID: 8697
		ColumnInfo,
		// Token: 0x040021FA RID: 8698
		PageTablixCell,
		// Token: 0x040021FB RID: 8699
		PageDetailCell,
		// Token: 0x040021FC RID: 8700
		PageCornerCell,
		// Token: 0x040021FD RID: 8701
		PageStructMemberCell,
		// Token: 0x040021FE RID: 8702
		PageStructStaticMemberCell,
		// Token: 0x040021FF RID: 8703
		PageStructDynamicMemberCell,
		// Token: 0x04002200 RID: 8704
		CommonSubReportInfo,
		// Token: 0x04002201 RID: 8705
		TablixCellBase,
		// Token: 0x04002202 RID: 8706
		RuntimeGaugePanelObj,
		// Token: 0x04002203 RID: 8707
		RuntimeGaugePanelObjReference,
		// Token: 0x04002204 RID: 8708
		GaugePanel,
		// Token: 0x04002205 RID: 8709
		GaugeMember,
		// Token: 0x04002206 RID: 8710
		GaugeRow,
		// Token: 0x04002207 RID: 8711
		GaugeCell,
		// Token: 0x04002208 RID: 8712
		GaugePanelStyleContainer,
		// Token: 0x04002209 RID: 8713
		FrameBackground,
		// Token: 0x0400220A RID: 8714
		BaseGaugeImage,
		// Token: 0x0400220B RID: 8715
		IndicatorImage,
		// Token: 0x0400220C RID: 8716
		PointerImage,
		// Token: 0x0400220D RID: 8717
		CapImage,
		// Token: 0x0400220E RID: 8718
		FrameImage,
		// Token: 0x0400220F RID: 8719
		CustomLabel,
		// Token: 0x04002210 RID: 8720
		Gauge,
		// Token: 0x04002211 RID: 8721
		RadialGauge,
		// Token: 0x04002212 RID: 8722
		LinearGauge,
		// Token: 0x04002213 RID: 8723
		GaugeImage,
		// Token: 0x04002214 RID: 8724
		GaugeLabel,
		// Token: 0x04002215 RID: 8725
		GaugePanelItem,
		// Token: 0x04002216 RID: 8726
		GaugePointer,
		// Token: 0x04002217 RID: 8727
		RadialPointer,
		// Token: 0x04002218 RID: 8728
		LinearPointer,
		// Token: 0x04002219 RID: 8729
		GaugeScale,
		// Token: 0x0400221A RID: 8730
		RadialScale,
		// Token: 0x0400221B RID: 8731
		LinearScale,
		// Token: 0x0400221C RID: 8732
		GaugeTickMarks,
		// Token: 0x0400221D RID: 8733
		TickMarkStyle,
		// Token: 0x0400221E RID: 8734
		ScalePin,
		// Token: 0x0400221F RID: 8735
		GaugeInputValue,
		// Token: 0x04002220 RID: 8736
		NumericIndicator,
		// Token: 0x04002221 RID: 8737
		PinLabel,
		// Token: 0x04002222 RID: 8738
		PointerCap,
		// Token: 0x04002223 RID: 8739
		ScaleLabels,
		// Token: 0x04002224 RID: 8740
		ScaleRange,
		// Token: 0x04002225 RID: 8741
		StateIndicator,
		// Token: 0x04002226 RID: 8742
		BackFrame,
		// Token: 0x04002227 RID: 8743
		TopImage,
		// Token: 0x04002228 RID: 8744
		Thermometer,
		// Token: 0x04002229 RID: 8745
		DynamicImage,
		// Token: 0x0400222A RID: 8746
		ExcelRowInfo,
		// Token: 0x0400222B RID: 8747
		IRowItemStruct,
		// Token: 0x0400222C RID: 8748
		RowItemStruct,
		// Token: 0x0400222D RID: 8749
		TablixItemStruct,
		// Token: 0x0400222E RID: 8750
		TablixStruct,
		// Token: 0x0400222F RID: 8751
		TablixMemberStruct,
		// Token: 0x04002230 RID: 8752
		ToggleParent,
		// Token: 0x04002231 RID: 8753
		ChildLeafInfo,
		// Token: 0x04002232 RID: 8754
		TextOrientation,
		// Token: 0x04002233 RID: 8755
		AspectRatio,
		// Token: 0x04002234 RID: 8756
		ChartElementPosition,
		// Token: 0x04002235 RID: 8757
		Paragraph,
		// Token: 0x04002236 RID: 8758
		TextRun,
		// Token: 0x04002237 RID: 8759
		ByteVariantHashtable,
		// Token: 0x04002238 RID: 8760
		TextBoxOffset,
		// Token: 0x04002239 RID: 8761
		StringBoolArrayDictionary,
		// Token: 0x0400223A RID: 8762
		LookupInfo,
		// Token: 0x0400223B RID: 8763
		LookupDestinationInfo,
		// Token: 0x0400223C RID: 8764
		LookupTable,
		// Token: 0x0400223D RID: 8765
		LookupTableReference,
		// Token: 0x0400223E RID: 8766
		LookupObjResult,
		// Token: 0x0400223F RID: 8767
		LookupMatches,
		// Token: 0x04002240 RID: 8768
		LookupMatchesWithRows,
		// Token: 0x04002241 RID: 8769
		TreePartitionManager,
		// Token: 0x04002242 RID: 8770
		ReportSection,
		// Token: 0x04002243 RID: 8771
		RuntimeMapDataRegionObj,
		// Token: 0x04002244 RID: 8772
		RuntimeMapDataRegionObjReference,
		// Token: 0x04002245 RID: 8773
		Map,
		// Token: 0x04002246 RID: 8774
		MapDataRegion,
		// Token: 0x04002247 RID: 8775
		MapMember,
		// Token: 0x04002248 RID: 8776
		MapRow,
		// Token: 0x04002249 RID: 8777
		MapCell,
		// Token: 0x0400224A RID: 8778
		MapStyleContainer,
		// Token: 0x0400224B RID: 8779
		MapLocation,
		// Token: 0x0400224C RID: 8780
		MapSize,
		// Token: 0x0400224D RID: 8781
		MapGridLines,
		// Token: 0x0400224E RID: 8782
		MapSubItem,
		// Token: 0x0400224F RID: 8783
		MapDockableSubItem,
		// Token: 0x04002250 RID: 8784
		MapBindingFieldPair,
		// Token: 0x04002251 RID: 8785
		MapViewport,
		// Token: 0x04002252 RID: 8786
		MapLimits,
		// Token: 0x04002253 RID: 8787
		MapColorScale,
		// Token: 0x04002254 RID: 8788
		MapColorScaleTitle,
		// Token: 0x04002255 RID: 8789
		MapDistanceScale,
		// Token: 0x04002256 RID: 8790
		MapTitle,
		// Token: 0x04002257 RID: 8791
		MapLegend,
		// Token: 0x04002258 RID: 8792
		MapLegendTitle,
		// Token: 0x04002259 RID: 8793
		MapAppearanceRule,
		// Token: 0x0400225A RID: 8794
		MapBucket,
		// Token: 0x0400225B RID: 8795
		MapColorPaletteRule,
		// Token: 0x0400225C RID: 8796
		MapColorRangeRule,
		// Token: 0x0400225D RID: 8797
		MapColorRule,
		// Token: 0x0400225E RID: 8798
		MapLineRules,
		// Token: 0x0400225F RID: 8799
		MapPolygonRules,
		// Token: 0x04002260 RID: 8800
		MapSizeRule,
		// Token: 0x04002261 RID: 8801
		MapMarkerImage,
		// Token: 0x04002262 RID: 8802
		MapMarker,
		// Token: 0x04002263 RID: 8803
		MapMarkerRule,
		// Token: 0x04002264 RID: 8804
		MapPointRules,
		// Token: 0x04002265 RID: 8805
		MapCustomColor,
		// Token: 0x04002266 RID: 8806
		MapCustomColorRule,
		// Token: 0x04002267 RID: 8807
		MapLineTemplate,
		// Token: 0x04002268 RID: 8808
		MapPolygonTemplate,
		// Token: 0x04002269 RID: 8809
		MapMarkerTemplate,
		// Token: 0x0400226A RID: 8810
		MapPointTemplate,
		// Token: 0x0400226B RID: 8811
		MapSpatialElementTemplate,
		// Token: 0x0400226C RID: 8812
		MapField,
		// Token: 0x0400226D RID: 8813
		MapLine,
		// Token: 0x0400226E RID: 8814
		MapPolygon,
		// Token: 0x0400226F RID: 8815
		MapSpatialElement,
		// Token: 0x04002270 RID: 8816
		MapPoint,
		// Token: 0x04002271 RID: 8817
		MapFieldDefinition,
		// Token: 0x04002272 RID: 8818
		MapFieldName,
		// Token: 0x04002273 RID: 8819
		MapLayer,
		// Token: 0x04002274 RID: 8820
		MapLineLayer,
		// Token: 0x04002275 RID: 8821
		MapShapefile,
		// Token: 0x04002276 RID: 8822
		MapPolygonLayer,
		// Token: 0x04002277 RID: 8823
		MapSpatialData,
		// Token: 0x04002278 RID: 8824
		MapSpatialDataRegion,
		// Token: 0x04002279 RID: 8825
		MapSpatialDataSet,
		// Token: 0x0400227A RID: 8826
		MapPointLayer,
		// Token: 0x0400227B RID: 8827
		MapTile,
		// Token: 0x0400227C RID: 8828
		MapTileLayer,
		// Token: 0x0400227D RID: 8829
		MapVectorLayer,
		// Token: 0x0400227E RID: 8830
		MapBorderSkin,
		// Token: 0x0400227F RID: 8831
		MapCustomView,
		// Token: 0x04002280 RID: 8832
		MapDataBoundView,
		// Token: 0x04002281 RID: 8833
		MapElementView,
		// Token: 0x04002282 RID: 8834
		MapView,
		// Token: 0x04002283 RID: 8835
		ShapefileInfo,
		// Token: 0x04002284 RID: 8836
		Union,
		// Token: 0x04002285 RID: 8837
		PageBreak,
		// Token: 0x04002286 RID: 8838
		PageBreakProperties,
		// Token: 0x04002287 RID: 8839
		UpdatedVariableValues,
		// Token: 0x04002288 RID: 8840
		Int32SerializableDictionary,
		// Token: 0x04002289 RID: 8841
		SerializableArray,
		// Token: 0x0400228A RID: 8842
		DataScopeInfo,
		// Token: 0x0400228B RID: 8843
		BucketedDataAggregateInfos,
		// Token: 0x0400228C RID: 8844
		DataAggregateInfoBucket,
		// Token: 0x0400228D RID: 8845
		BucketedDataAggregateObjs,
		// Token: 0x0400228E RID: 8846
		DataAggregateObjBucket,
		// Token: 0x0400228F RID: 8847
		NumericIndicatorRange,
		// Token: 0x04002290 RID: 8848
		IndicatorState,
		// Token: 0x04002291 RID: 8849
		BucketedRunningValueInfos,
		// Token: 0x04002292 RID: 8850
		RunningValueInfoBucket,
		// Token: 0x04002293 RID: 8851
		SharedDataSetQuery,
		// Token: 0x04002294 RID: 8852
		DataSetCore,
		// Token: 0x04002295 RID: 8853
		DataSetParameterValue,
		// Token: 0x04002296 RID: 8854
		RIFVariantContainer,
		// Token: 0x04002297 RID: 8855
		NLevelVariantHashtable,
		// Token: 0x04002298 RID: 8856
		SortScopeValuesHolder,
		// Token: 0x04002299 RID: 8857
		RuntimeDataRowSortHierarchyObj,
		// Token: 0x0400229A RID: 8858
		SyntheticTriangulatedCellReference,
		// Token: 0x0400229B RID: 8859
		RuntimeGroupingObjHash,
		// Token: 0x0400229C RID: 8860
		RuntimeGroupingObjTree,
		// Token: 0x0400229D RID: 8861
		RuntimeGroupingObjDetail,
		// Token: 0x0400229E RID: 8862
		RuntimeGroupingObjLinkedList,
		// Token: 0x0400229F RID: 8863
		RuntimeGroupingObjDetailUserSort,
		// Token: 0x040022A0 RID: 8864
		RuntimeGroupingObjNaturalGroup,
		// Token: 0x040022A1 RID: 8865
		Relationship,
		// Token: 0x040022A2 RID: 8866
		IdcRelationship,
		// Token: 0x040022A3 RID: 8867
		DefaultRelationship,
		// Token: 0x040022A4 RID: 8868
		JoinCondition,
		// Token: 0x040022A5 RID: 8869
		BandLayoutOptions,
		// Token: 0x040022A6 RID: 8870
		LabelData,
		// Token: 0x040022A7 RID: 8871
		Slider,
		// Token: 0x040022A8 RID: 8872
		Coverflow,
		// Token: 0x040022A9 RID: 8873
		Navigation,
		// Token: 0x040022AA RID: 8874
		PlayAxis,
		// Token: 0x040022AB RID: 8875
		BandNavigationCell,
		// Token: 0x040022AC RID: 8876
		Tabstrip,
		// Token: 0x040022AD RID: 8877
		NavigationItem,
		// Token: 0x040022AE RID: 8878
		ScopeIDInfo,
		// Token: 0x040022AF RID: 8879
		WordOpenXmlTableRowProperties,
		// Token: 0x040022B0 RID: 8880
		WordOpenXmlBorderProperties,
		// Token: 0x040022B1 RID: 8881
		WordOpenXmlBaseInterleaver,
		// Token: 0x040022B2 RID: 8882
		WordOpenXmlTableGrid,
		// Token: 0x040022B3 RID: 8883
		WordOpenXmlHeaderFooterReferences,
		// Token: 0x040022B4 RID: 8884
		StreamingNoRowsDataRegionInstance,
		// Token: 0x040022B5 RID: 8885
		StreamingNoRowsCellInstance,
		// Token: 0x040022B6 RID: 8886
		StreamingNoRowsMemberInstance,
		// Token: 0x040022B7 RID: 8887
		SyntheticOnDemandScopeInstanceReference,
		// Token: 0x040022B8 RID: 8888
		SyntheticOnDemandDataRegionInstanceReference,
		// Token: 0x040022B9 RID: 8889
		SyntheticOnDemandMemberInstanceReference,
		// Token: 0x040022BA RID: 8890
		JoinInfo,
		// Token: 0x040022BB RID: 8891
		LinearJoinInfo,
		// Token: 0x040022BC RID: 8892
		IntersectJoinInfo,
		// Token: 0x040022BD RID: 8893
		RdlFunctionInfo,
		// Token: 0x040022BE RID: 8894
		ProcessingComparer,
		// Token: 0x040022BF RID: 8895
		ScopedFieldInfo,
		// Token: 0x040022C0 RID: 8896
		RuntimeCellWithContents,
		// Token: 0x040022C1 RID: 8897
		RuntimeDataTablixWithScopedItemsGroupLeafObj,
		// Token: 0x040022C2 RID: 8898
		RuntimeDataTablixWithScopedItemsObj,
		// Token: 0x040022C3 RID: 8899
		RuntimeDataShapeObj,
		// Token: 0x040022C4 RID: 8900
		RuntimeDataShapeGroupLeafObj,
		// Token: 0x040022C5 RID: 8901
		RuntimeDataShapeIntersection,
		// Token: 0x040022C6 RID: 8902
		RuntimeDataShapeObjReference,
		// Token: 0x040022C7 RID: 8903
		RuntimeDataShapeGroupLeafObjReference,
		// Token: 0x040022C8 RID: 8904
		RuntimeDataShapeIntersectionReference,
		// Token: 0x040022C9 RID: 8905
		ParametersLayout,
		// Token: 0x040022CA RID: 8906
		ParameterGridLayoutCellDefinition,
		// Token: 0x040022CB RID: 8907
		MaxValue
	}
}
