using System;

namespace Microsoft.ReportingServices.ReportProcessing.Persistence
{
	// Token: 0x0200079E RID: 1950
	internal enum MemberName
	{
		// Token: 0x0400377D RID: 14205
		ID,
		// Token: 0x0400377E RID: 14206
		Name,
		// Token: 0x0400377F RID: 14207
		StyleClass,
		// Token: 0x04003780 RID: 14208
		Top,
		// Token: 0x04003781 RID: 14209
		TopValue,
		// Token: 0x04003782 RID: 14210
		Left,
		// Token: 0x04003783 RID: 14211
		LeftValue,
		// Token: 0x04003784 RID: 14212
		Height,
		// Token: 0x04003785 RID: 14213
		HeightValue,
		// Token: 0x04003786 RID: 14214
		Width,
		// Token: 0x04003787 RID: 14215
		WidthValue,
		// Token: 0x04003788 RID: 14216
		ZIndex,
		// Token: 0x04003789 RID: 14217
		Visibility,
		// Token: 0x0400378A RID: 14218
		Label,
		// Token: 0x0400378B RID: 14219
		Custom,
		// Token: 0x0400378C RID: 14220
		RepeatedSibling,
		// Token: 0x0400378D RID: 14221
		Author,
		// Token: 0x0400378E RID: 14222
		AutoRefresh,
		// Token: 0x0400378F RID: 14223
		EmbeddedImages,
		// Token: 0x04003790 RID: 14224
		PageHeader,
		// Token: 0x04003791 RID: 14225
		PageFooter,
		// Token: 0x04003792 RID: 14226
		ReportItems,
		// Token: 0x04003793 RID: 14227
		DataSources,
		// Token: 0x04003794 RID: 14228
		PageHeight,
		// Token: 0x04003795 RID: 14229
		PageHeightValue,
		// Token: 0x04003796 RID: 14230
		PageWidth,
		// Token: 0x04003797 RID: 14231
		PageWidthValue,
		// Token: 0x04003798 RID: 14232
		LeftMargin,
		// Token: 0x04003799 RID: 14233
		LeftMarginValue,
		// Token: 0x0400379A RID: 14234
		RightMargin,
		// Token: 0x0400379B RID: 14235
		RightMarginValue,
		// Token: 0x0400379C RID: 14236
		TopMargin,
		// Token: 0x0400379D RID: 14237
		TopMarginValue,
		// Token: 0x0400379E RID: 14238
		BottomMargin,
		// Token: 0x0400379F RID: 14239
		BottomMarginValue,
		// Token: 0x040037A0 RID: 14240
		ClassName,
		// Token: 0x040037A1 RID: 14241
		InstanceName,
		// Token: 0x040037A2 RID: 14242
		CodeModules,
		// Token: 0x040037A3 RID: 14243
		CodeClasses,
		// Token: 0x040037A4 RID: 14244
		Columns,
		// Token: 0x040037A5 RID: 14245
		ColumnSpacing,
		// Token: 0x040037A6 RID: 14246
		ColumnSpacingValue,
		// Token: 0x040037A7 RID: 14247
		PageAggregates,
		// Token: 0x040037A8 RID: 14248
		CompiledCode,
		// Token: 0x040037A9 RID: 14249
		MergeOnePass,
		// Token: 0x040037AA RID: 14250
		PageMergeOnePass,
		// Token: 0x040037AB RID: 14251
		SubReportMergeTransactions,
		// Token: 0x040037AC RID: 14252
		NeedPostGroupProcessing,
		// Token: 0x040037AD RID: 14253
		HasPostSortAggregates,
		// Token: 0x040037AE RID: 14254
		HasReportItemReferences,
		// Token: 0x040037AF RID: 14255
		ShowHideType,
		// Token: 0x040037B0 RID: 14256
		ReportInstance,
		// Token: 0x040037B1 RID: 14257
		BodyID,
		// Token: 0x040037B2 RID: 14258
		PrintOnFirstPage,
		// Token: 0x040037B3 RID: 14259
		PrintOnLastPage,
		// Token: 0x040037B4 RID: 14260
		PostProcessEvaluate,
		// Token: 0x040037B5 RID: 14261
		Slanted,
		// Token: 0x040037B6 RID: 14262
		PageBreakAtEnd,
		// Token: 0x040037B7 RID: 14263
		PageBreakAtStart,
		// Token: 0x040037B8 RID: 14264
		HyperLinkURL,
		// Token: 0x040037B9 RID: 14265
		Source,
		// Token: 0x040037BA RID: 14266
		Value,
		// Token: 0x040037BB RID: 14267
		MIMEType,
		// Token: 0x040037BC RID: 14268
		Sizing,
		// Token: 0x040037BD RID: 14269
		HideDuplicates,
		// Token: 0x040037BE RID: 14270
		CanGrow,
		// Token: 0x040037BF RID: 14271
		CanShrink,
		// Token: 0x040037C0 RID: 14272
		IsToggle,
		// Token: 0x040037C1 RID: 14273
		InitialToggleState,
		// Token: 0x040037C2 RID: 14274
		ValueType,
		// Token: 0x040037C3 RID: 14275
		ReportPath,
		// Token: 0x040037C4 RID: 14276
		NoRows,
		// Token: 0x040037C5 RID: 14277
		Parameters,
		// Token: 0x040037C6 RID: 14278
		MergeTransactions,
		// Token: 0x040037C7 RID: 14279
		Status,
		// Token: 0x040037C8 RID: 14280
		ReportName,
		// Token: 0x040037C9 RID: 14281
		Description,
		// Token: 0x040037CA RID: 14282
		Report,
		// Token: 0x040037CB RID: 14283
		StringUri,
		// Token: 0x040037CC RID: 14284
		ClassID,
		// Token: 0x040037CD RID: 14285
		CodeBase,
		// Token: 0x040037CE RID: 14286
		DataSetName,
		// Token: 0x040037CF RID: 14287
		KeepTogether,
		// Token: 0x040037D0 RID: 14288
		RepeatSiblings,
		// Token: 0x040037D1 RID: 14289
		Grouping,
		// Token: 0x040037D2 RID: 14290
		Sorting,
		// Token: 0x040037D3 RID: 14291
		InnerHierarchy,
		// Token: 0x040037D4 RID: 14292
		DataRegionDef,
		// Token: 0x040037D5 RID: 14293
		GroupExpressions,
		// Token: 0x040037D6 RID: 14294
		GroupLabel,
		// Token: 0x040037D7 RID: 14295
		SortDirections,
		// Token: 0x040037D8 RID: 14296
		NaturalGroup,
		// Token: 0x040037D9 RID: 14297
		Aggregates,
		// Token: 0x040037DA RID: 14298
		GroupAndSort,
		// Token: 0x040037DB RID: 14299
		SortExpressions,
		// Token: 0x040037DC RID: 14300
		HierarchyDef,
		// Token: 0x040037DD RID: 14301
		FillPage,
		// Token: 0x040037DE RID: 14302
		CornerReportItems,
		// Token: 0x040037DF RID: 14303
		Rows,
		// Token: 0x040037E0 RID: 14304
		ColumnCount,
		// Token: 0x040037E1 RID: 14305
		RowCount,
		// Token: 0x040037E2 RID: 14306
		CellReportItems,
		// Token: 0x040037E3 RID: 14307
		CellAggregates,
		// Token: 0x040037E4 RID: 14308
		PropagatedPageBreakAtStart,
		// Token: 0x040037E5 RID: 14309
		PropagatedPageBreakAtEnd,
		// Token: 0x040037E6 RID: 14310
		InnerRowLevelWithPageBreak,
		// Token: 0x040037E7 RID: 14311
		MatrixRows,
		// Token: 0x040037E8 RID: 14312
		MatrixColumns,
		// Token: 0x040037E9 RID: 14313
		GroupsBeforeRowHeaders,
		// Token: 0x040037EA RID: 14314
		ProcessingInnerGrouping,
		// Token: 0x040037EB RID: 14315
		StaticColumns,
		// Token: 0x040037EC RID: 14316
		StaticRows,
		// Token: 0x040037ED RID: 14317
		Size,
		// Token: 0x040037EE RID: 14318
		SizeValue,
		// Token: 0x040037EF RID: 14319
		Subtotal,
		// Token: 0x040037F0 RID: 14320
		Level,
		// Token: 0x040037F1 RID: 14321
		IsColumn,
		// Token: 0x040037F2 RID: 14322
		SubtotalSpan,
		// Token: 0x040037F3 RID: 14323
		AutoDerived,
		// Token: 0x040037F4 RID: 14324
		Position,
		// Token: 0x040037F5 RID: 14325
		TableColumns,
		// Token: 0x040037F6 RID: 14326
		HeaderRows,
		// Token: 0x040037F7 RID: 14327
		HeaderRepeatOnNewPage,
		// Token: 0x040037F8 RID: 14328
		TableGroups,
		// Token: 0x040037F9 RID: 14329
		DetailRows,
		// Token: 0x040037FA RID: 14330
		FooterRows,
		// Token: 0x040037FB RID: 14331
		FooterRepeatOnNewPage,
		// Token: 0x040037FC RID: 14332
		GroupPageBreakAtStart,
		// Token: 0x040037FD RID: 14333
		GroupPageBreakAtEnd,
		// Token: 0x040037FE RID: 14334
		UseOwc,
		// Token: 0x040037FF RID: 14335
		OwcNonSharedStyles,
		// Token: 0x04003800 RID: 14336
		RunningValues,
		// Token: 0x04003801 RID: 14337
		ChartData,
		// Token: 0x04003802 RID: 14338
		ChartDefinition,
		// Token: 0x04003803 RID: 14339
		NonComputedReportItems,
		// Token: 0x04003804 RID: 14340
		ComputedReportItems,
		// Token: 0x04003805 RID: 14341
		SortedReportItems,
		// Token: 0x04003806 RID: 14342
		IsComputed,
		// Token: 0x04003807 RID: 14343
		Index,
		// Token: 0x04003808 RID: 14344
		StyleAttributes,
		// Token: 0x04003809 RID: 14345
		ExpressionList,
		// Token: 0x0400380A RID: 14346
		IsExpression,
		// Token: 0x0400380B RID: 14347
		StringValue,
		// Token: 0x0400380C RID: 14348
		BoolValue,
		// Token: 0x0400380D RID: 14349
		IntValue,
		// Token: 0x0400380E RID: 14350
		Hidden,
		// Token: 0x0400380F RID: 14351
		Toggle,
		// Token: 0x04003810 RID: 14352
		Type,
		// Token: 0x04003811 RID: 14353
		AggregateType,
		// Token: 0x04003812 RID: 14354
		Expressions,
		// Token: 0x04003813 RID: 14355
		DuplicateNames,
		// Token: 0x04003814 RID: 14356
		Scope,
		// Token: 0x04003815 RID: 14357
		Transaction,
		// Token: 0x04003816 RID: 14358
		ConnectString,
		// Token: 0x04003817 RID: 14359
		DataSets,
		// Token: 0x04003818 RID: 14360
		Fields,
		// Token: 0x04003819 RID: 14361
		DataField,
		// Token: 0x0400381A RID: 14362
		Query,
		// Token: 0x0400381B RID: 14363
		CaseSensitivity,
		// Token: 0x0400381C RID: 14364
		Collation,
		// Token: 0x0400381D RID: 14365
		AccentSensitivity,
		// Token: 0x0400381E RID: 14366
		KanatypeSensitivity,
		// Token: 0x0400381F RID: 14367
		WidthSensitivity,
		// Token: 0x04003820 RID: 14368
		DataRegions,
		// Token: 0x04003821 RID: 14369
		CommandType,
		// Token: 0x04003822 RID: 14370
		CommandText,
		// Token: 0x04003823 RID: 14371
		QueryParameters,
		// Token: 0x04003824 RID: 14372
		Timeout,
		// Token: 0x04003825 RID: 14373
		StartHidden,
		// Token: 0x04003826 RID: 14374
		ReceiverUniqueNames,
		// Token: 0x04003827 RID: 14375
		ContainerUniqueNames,
		// Token: 0x04003828 RID: 14376
		SenderUniqueName,
		// Token: 0x04003829 RID: 14377
		Offset,
		// Token: 0x0400382A RID: 14378
		OffsetInfo,
		// Token: 0x0400382B RID: 14379
		UniqueName,
		// Token: 0x0400382C RID: 14380
		ReportItemDef,
		// Token: 0x0400382D RID: 14381
		ReportItemColInstance,
		// Token: 0x0400382E RID: 14382
		StyleAttributeValues,
		// Token: 0x0400382F RID: 14383
		RequestUserName,
		// Token: 0x04003830 RID: 14384
		BodyUniqueName,
		// Token: 0x04003831 RID: 14385
		ChildrenUniqueNames,
		// Token: 0x04003832 RID: 14386
		ReportItemInstances,
		// Token: 0x04003833 RID: 14387
		ReportItemColDef,
		// Token: 0x04003834 RID: 14388
		ChildrenNonComputedUniqueNames,
		// Token: 0x04003835 RID: 14389
		FormattedValue,
		// Token: 0x04003836 RID: 14390
		OriginalValue,
		// Token: 0x04003837 RID: 14391
		Duplicate,
		// Token: 0x04003838 RID: 14392
		ImageValue,
		// Token: 0x04003839 RID: 14393
		ParameterValues,
		// Token: 0x0400383A RID: 14394
		ListContentInstances,
		// Token: 0x0400383B RID: 14395
		ListDef,
		// Token: 0x0400383C RID: 14396
		CornerContent,
		// Token: 0x0400383D RID: 14397
		ColumnInstances,
		// Token: 0x0400383E RID: 14398
		RowInstances,
		// Token: 0x0400383F RID: 14399
		Cells,
		// Token: 0x04003840 RID: 14400
		InstanceCountOfInnerRowWithPageBreak,
		// Token: 0x04003841 RID: 14401
		CornerNonComputedNames,
		// Token: 0x04003842 RID: 14402
		MatrixHeadingDef,
		// Token: 0x04003843 RID: 14403
		Content,
		// Token: 0x04003844 RID: 14404
		SubHeadingInstances,
		// Token: 0x04003845 RID: 14405
		IsSubtotal,
		// Token: 0x04003846 RID: 14406
		ContentUniqueNames,
		// Token: 0x04003847 RID: 14407
		HeadingCellIndex,
		// Token: 0x04003848 RID: 14408
		HeadingSpan,
		// Token: 0x04003849 RID: 14409
		RowIndex,
		// Token: 0x0400384A RID: 14410
		ColumnIndex,
		// Token: 0x0400384B RID: 14411
		HeaderRowInstances,
		// Token: 0x0400384C RID: 14412
		TableGroupInstances,
		// Token: 0x0400384D RID: 14413
		DetailRowInstances,
		// Token: 0x0400384E RID: 14414
		FooterRowInstances,
		// Token: 0x0400384F RID: 14415
		TableGroupDef,
		// Token: 0x04003850 RID: 14416
		SubGroupInstances,
		// Token: 0x04003851 RID: 14417
		TableRowReportItemColInstance,
		// Token: 0x04003852 RID: 14418
		TableRowDef,
		// Token: 0x04003853 RID: 14419
		DocumentMap,
		// Token: 0x04003854 RID: 14420
		ShowHideSenderInfo,
		// Token: 0x04003855 RID: 14421
		ShowHideReceiverInfo,
		// Token: 0x04003856 RID: 14422
		LastID,
		// Token: 0x04003857 RID: 14423
		Id,
		// Token: 0x04003858 RID: 14424
		Children,
		// Token: 0x04003859 RID: 14425
		DataType,
		// Token: 0x0400385A RID: 14426
		Nullable,
		// Token: 0x0400385B RID: 14427
		Prompt,
		// Token: 0x0400385C RID: 14428
		PromptUser,
		// Token: 0x0400385D RID: 14429
		IsUserSupplied,
		// Token: 0x0400385E RID: 14430
		QuickFind,
		// Token: 0x0400385F RID: 14431
		Images,
		// Token: 0x04003860 RID: 14432
		Bookmark,
		// Token: 0x04003861 RID: 14433
		IntegratedSecurity,
		// Token: 0x04003862 RID: 14434
		DataSourceReference,
		// Token: 0x04003863 RID: 14435
		LinkToChild,
		// Token: 0x04003864 RID: 14436
		DrillthroughReportName,
		// Token: 0x04003865 RID: 14437
		DrillthroughParameters,
		// Token: 0x04003866 RID: 14438
		DrillthroughParametersOmits,
		// Token: 0x04003867 RID: 14439
		BookmarkLink,
		// Token: 0x04003868 RID: 14440
		LayoutDirection,
		// Token: 0x04003869 RID: 14441
		ColSpans,
		// Token: 0x0400386A RID: 14442
		IDs,
		// Token: 0x0400386B RID: 14443
		CellIDs,
		// Token: 0x0400386C RID: 14444
		Expression,
		// Token: 0x0400386D RID: 14445
		Operator,
		// Token: 0x0400386E RID: 14446
		Values,
		// Token: 0x0400386F RID: 14447
		Filters,
		// Token: 0x04003870 RID: 14448
		OwcCellNames,
		// Token: 0x04003871 RID: 14449
		OwcGroupExpression,
		// Token: 0x04003872 RID: 14450
		GroupExpressionValue,
		// Token: 0x04003873 RID: 14451
		SubReports,
		// Token: 0x04003874 RID: 14452
		HasImageStreams,
		// Token: 0x04003875 RID: 14453
		IsFullSize,
		// Token: 0x04003876 RID: 14454
		HasBookmarks,
		// Token: 0x04003877 RID: 14455
		HasLabels,
		// Token: 0x04003878 RID: 14456
		SnapshotProcessingEnabled,
		// Token: 0x04003879 RID: 14457
		ReaderExtensionsSupported,
		// Token: 0x0400387A RID: 14458
		RecordFields,
		// Token: 0x0400387B RID: 14459
		IsAggregateRow,
		// Token: 0x0400387C RID: 14460
		AggregationFieldCount,
		// Token: 0x0400387D RID: 14461
		FieldValue,
		// Token: 0x0400387E RID: 14462
		IsAggregateField,
		// Token: 0x0400387F RID: 14463
		RecordSetSize,
		// Token: 0x04003880 RID: 14464
		DrillthroughBookmarkLink,
		// Token: 0x04003881 RID: 14465
		UsedInQuery,
		// Token: 0x04003882 RID: 14466
		UsedOnlyInParameters,
		// Token: 0x04003883 RID: 14467
		AllowBlank,
		// Token: 0x04003884 RID: 14468
		MultiValue,
		// Token: 0x04003885 RID: 14469
		ValidValues,
		// Token: 0x04003886 RID: 14470
		DefaultValue,
		// Token: 0x04003887 RID: 14471
		ValidValuesDataSource,
		// Token: 0x04003888 RID: 14472
		ValidValuesValueExpression,
		// Token: 0x04003889 RID: 14473
		ValidValuesLabelExpression,
		// Token: 0x0400388A RID: 14474
		DefaultValueDataSource,
		// Token: 0x0400388B RID: 14475
		DataSourceIndex,
		// Token: 0x0400388C RID: 14476
		DataSetIndex,
		// Token: 0x0400388D RID: 14477
		ValueFieldIndex,
		// Token: 0x0400388E RID: 14478
		LabelFieldIndex,
		// Token: 0x0400388F RID: 14479
		DynamicValidValues,
		// Token: 0x04003890 RID: 14480
		DynamicDefaultValue,
		// Token: 0x04003891 RID: 14481
		DependencyList,
		// Token: 0x04003892 RID: 14482
		NonCalculatedFieldCount,
		// Token: 0x04003893 RID: 14483
		ExecutionTime,
		// Token: 0x04003894 RID: 14484
		ReportServerUrl,
		// Token: 0x04003895 RID: 14485
		ReportFolder,
		// Token: 0x04003896 RID: 14486
		Language,
		// Token: 0x04003897 RID: 14487
		Formula,
		// Token: 0x04003898 RID: 14488
		ProcessingMessages,
		// Token: 0x04003899 RID: 14489
		Code,
		// Token: 0x0400389A RID: 14490
		Severity,
		// Token: 0x0400389B RID: 14491
		ObjectType,
		// Token: 0x0400389C RID: 14492
		ObjectName,
		// Token: 0x0400389D RID: 14493
		PropertyName,
		// Token: 0x0400389E RID: 14494
		Message,
		// Token: 0x0400389F RID: 14495
		CommonCode,
		// Token: 0x040038A0 RID: 14496
		ReportItemsWithHideDuplicates,
		// Token: 0x040038A1 RID: 14497
		SubtotalHeadingInstance,
		// Token: 0x040038A2 RID: 14498
		StyleExpressionList,
		// Token: 0x040038A3 RID: 14499
		ExprHostID,
		// Token: 0x040038A4 RID: 14500
		HasExprHost,
		// Token: 0x040038A5 RID: 14501
		ValueReferenced,
		// Token: 0x040038A6 RID: 14502
		Omit,
		// Token: 0x040038A7 RID: 14503
		ToolTip,
		// Token: 0x040038A8 RID: 14504
		Parent,
		// Token: 0x040038A9 RID: 14505
		TableDetail,
		// Token: 0x040038AA RID: 14506
		DetailGroup,
		// Token: 0x040038AB RID: 14507
		TableDetailDef,
		// Token: 0x040038AC RID: 14508
		TableDetailInstances,
		// Token: 0x040038AD RID: 14509
		DetailRunningValues,
		// Token: 0x040038AE RID: 14510
		PostSortAggregates,
		// Token: 0x040038AF RID: 14511
		RecursiveAggregates,
		// Token: 0x040038B0 RID: 14512
		CellPostSortAggregates,
		// Token: 0x040038B1 RID: 14513
		HasSpecialRecursiveAggregates,
		// Token: 0x040038B2 RID: 14514
		RecursiveSender,
		// Token: 0x040038B3 RID: 14515
		RecursiveReceiver,
		// Token: 0x040038B4 RID: 14516
		Title,
		// Token: 0x040038B5 RID: 14517
		Action,
		// Token: 0x040038B6 RID: 14518
		ChartHeadingDef,
		// Token: 0x040038B7 RID: 14519
		HeadingLabel,
		// Token: 0x040038B8 RID: 14520
		CellDataPoints,
		// Token: 0x040038B9 RID: 14521
		MultiChart,
		// Token: 0x040038BA RID: 14522
		MultiChartInstances,
		// Token: 0x040038BB RID: 14523
		CategoryAxis,
		// Token: 0x040038BC RID: 14524
		ValueAxis,
		// Token: 0x040038BD RID: 14525
		SubType,
		// Token: 0x040038BE RID: 14526
		PointWidth,
		// Token: 0x040038BF RID: 14527
		ThreeDProperties,
		// Token: 0x040038C0 RID: 14528
		DataTransform,
		// Token: 0x040038C1 RID: 14529
		DataSchema,
		// Token: 0x040038C2 RID: 14530
		DataElementName,
		// Token: 0x040038C3 RID: 14531
		DataElementStyleAttribute,
		// Token: 0x040038C4 RID: 14532
		DataElementOutput,
		// Token: 0x040038C5 RID: 14533
		DataCollectionName,
		// Token: 0x040038C6 RID: 14534
		CellDataElementName,
		// Token: 0x040038C7 RID: 14535
		CellDataElementOutput,
		// Token: 0x040038C8 RID: 14536
		DataInstanceName,
		// Token: 0x040038C9 RID: 14537
		DataInstanceElementOutput,
		// Token: 0x040038CA RID: 14538
		DetailDataElementName,
		// Token: 0x040038CB RID: 14539
		DetailDataCollectionName,
		// Token: 0x040038CC RID: 14540
		DetailDataElementOutput,
		// Token: 0x040038CD RID: 14541
		Palette,
		// Token: 0x040038CE RID: 14542
		Labels,
		// Token: 0x040038CF RID: 14543
		Caption,
		// Token: 0x040038D0 RID: 14544
		Axis,
		// Token: 0x040038D1 RID: 14545
		Legend,
		// Token: 0x040038D2 RID: 14546
		LegendStyleAttributeValues,
		// Token: 0x040038D3 RID: 14547
		PlotArea,
		// Token: 0x040038D4 RID: 14548
		PlotAreaStyleAttributeValues,
		// Token: 0x040038D5 RID: 14549
		Layout,
		// Token: 0x040038D6 RID: 14550
		MajorTickMarks,
		// Token: 0x040038D7 RID: 14551
		MinorTickMarks,
		// Token: 0x040038D8 RID: 14552
		MajorGridLines,
		// Token: 0x040038D9 RID: 14553
		MinorGridLines,
		// Token: 0x040038DA RID: 14554
		MajorInterval,
		// Token: 0x040038DB RID: 14555
		MinorInterval,
		// Token: 0x040038DC RID: 14556
		Reverse,
		// Token: 0x040038DD RID: 14557
		ShowGridLines,
		// Token: 0x040038DE RID: 14558
		MajorGridLinesStyleAttributeValues,
		// Token: 0x040038DF RID: 14559
		MinorGridLinesStyleAttributeValues,
		// Token: 0x040038E0 RID: 14560
		Min,
		// Token: 0x040038E1 RID: 14561
		Max,
		// Token: 0x040038E2 RID: 14562
		AutoScaleMin,
		// Token: 0x040038E3 RID: 14563
		AutoScaleMax,
		// Token: 0x040038E4 RID: 14564
		LogScale,
		// Token: 0x040038E5 RID: 14565
		SplitAxisStart,
		// Token: 0x040038E6 RID: 14566
		SplitAxisEnd,
		// Token: 0x040038E7 RID: 14567
		CrossAt,
		// Token: 0x040038E8 RID: 14568
		AutoCrossAt,
		// Token: 0x040038E9 RID: 14569
		DataValues,
		// Token: 0x040038EA RID: 14570
		DataLabel,
		// Token: 0x040038EB RID: 14571
		MarkerType,
		// Token: 0x040038EC RID: 14572
		MarkerSize,
		// Token: 0x040038ED RID: 14573
		MarkerStyleClass,
		// Token: 0x040038EE RID: 14574
		MaxCount,
		// Token: 0x040038EF RID: 14575
		SyncScale,
		// Token: 0x040038F0 RID: 14576
		PerspectiveProjectionMode,
		// Token: 0x040038F1 RID: 14577
		Rotation,
		// Token: 0x040038F2 RID: 14578
		Inclination,
		// Token: 0x040038F3 RID: 14579
		Perspective,
		// Token: 0x040038F4 RID: 14580
		HeightRatio,
		// Token: 0x040038F5 RID: 14581
		DepthRatio,
		// Token: 0x040038F6 RID: 14582
		Shading,
		// Token: 0x040038F7 RID: 14583
		GapDepth,
		// Token: 0x040038F8 RID: 14584
		WallThickness,
		// Token: 0x040038F9 RID: 14585
		Origin,
		// Token: 0x040038FA RID: 14586
		DataLabelStyleAttributeValues,
		// Token: 0x040038FB RID: 14587
		DataLabelValue,
		// Token: 0x040038FC RID: 14588
		MarkerStyleAttributeValues,
		// Token: 0x040038FD RID: 14589
		Visible,
		// Token: 0x040038FE RID: 14590
		Interlaced,
		// Token: 0x040038FF RID: 14591
		InsidePlotArea,
		// Token: 0x04003900 RID: 14592
		Enabled,
		// Token: 0x04003901 RID: 14593
		DrawingStyleCube,
		// Token: 0x04003902 RID: 14594
		Clustered,
		// Token: 0x04003903 RID: 14595
		Margin,
		// Token: 0x04003904 RID: 14596
		CellRunningValues,
		// Token: 0x04003905 RID: 14597
		MultiCharts,
		// Token: 0x04003906 RID: 14598
		DataPointIndex,
		// Token: 0x04003907 RID: 14599
		ChartGroupExpression,
		// Token: 0x04003908 RID: 14600
		Scalar,
		// Token: 0x04003909 RID: 14601
		StaticGroupingIndex,
		// Token: 0x0400390A RID: 14602
		PlotTypesLine,
		// Token: 0x0400390B RID: 14603
		BrokenImage,
		// Token: 0x0400390C RID: 14604
		CultureName,
		// Token: 0x0400390D RID: 14605
		StartPage,
		// Token: 0x0400390E RID: 14606
		EndPage,
		// Token: 0x0400390F RID: 14607
		DistanceFromReportTop,
		// Token: 0x04003910 RID: 14608
		DistanceBeforeTop,
		// Token: 0x04003911 RID: 14609
		DistanceBelowChildren,
		// Token: 0x04003912 RID: 14610
		SiblingAboveMe,
		// Token: 0x04003913 RID: 14611
		IsListMostInner,
		// Token: 0x04003914 RID: 14612
		ChildrenStartAndEndPages,
		// Token: 0x04003915 RID: 14613
		NumberOfPages,
		// Token: 0x04003916 RID: 14614
		Page,
		// Token: 0x04003917 RID: 14615
		IntermediateFormatVersion,
		// Token: 0x04003918 RID: 14616
		IntermediateFormatVersionMajor,
		// Token: 0x04003919 RID: 14617
		IntermediateFormatVersionMinor,
		// Token: 0x0400391A RID: 14618
		IntermediateFormatVersionBuild,
		// Token: 0x0400391B RID: 14619
		LCID,
		// Token: 0x0400391C RID: 14620
		ReportVersion,
		// Token: 0x0400391D RID: 14621
		ParametersFromCatalog,
		// Token: 0x0400391E RID: 14622
		StreamName,
		// Token: 0x0400391F RID: 14623
		ActionItem,
		// Token: 0x04003920 RID: 14624
		ActionItemList,
		// Token: 0x04003921 RID: 14625
		CoumputedActionsCount,
		// Token: 0x04003922 RID: 14626
		CustomProperties,
		// Token: 0x04003923 RID: 14627
		CustomPropertyInstances,
		// Token: 0x04003924 RID: 14628
		DataRowCells,
		// Token: 0x04003925 RID: 14629
		Static,
		// Token: 0x04003926 RID: 14630
		AltReportItemColInstance,
		// Token: 0x04003927 RID: 14631
		DataValueInstances,
		// Token: 0x04003928 RID: 14632
		HeadingDefinition,
		// Token: 0x04003929 RID: 14633
		InnerHeadings,
		// Token: 0x0400392A RID: 14634
		CellExprHostIDs,
		// Token: 0x0400392B RID: 14635
		RDLRowIndex,
		// Token: 0x0400392C RID: 14636
		RDLColumnIndex,
		// Token: 0x0400392D RID: 14637
		BookmarksInfo,
		// Token: 0x0400392E RID: 14638
		HasDocumentMap,
		// Token: 0x0400392F RID: 14639
		HasShowHide,
		// Token: 0x04003930 RID: 14640
		DocMapPage,
		// Token: 0x04003931 RID: 14641
		RenderReportItemColDef,
		// Token: 0x04003932 RID: 14642
		ImageMapAreas,
		// Token: 0x04003933 RID: 14643
		Shape,
		// Token: 0x04003934 RID: 14644
		Coordinates,
		// Token: 0x04003935 RID: 14645
		ActionInstance,
		// Token: 0x04003936 RID: 14646
		EventSource,
		// Token: 0x04003937 RID: 14647
		EventSourceScopeInfo,
		// Token: 0x04003938 RID: 14648
		DataSetID,
		// Token: 0x04003939 RID: 14649
		ContainingScopes,
		// Token: 0x0400393A RID: 14650
		UserSort,
		// Token: 0x0400393B RID: 14651
		SortExpressionScope,
		// Token: 0x0400393C RID: 14652
		GroupsInSortTarget,
		// Token: 0x0400393D RID: 14653
		SortTarget,
		// Token: 0x0400393E RID: 14654
		HasDetailUserSortFilter,
		// Token: 0x0400393F RID: 14655
		SaveGroupExprValues,
		// Token: 0x04003940 RID: 14656
		HasUserSortFilter,
		// Token: 0x04003941 RID: 14657
		IsMatrixCellScope,
		// Token: 0x04003942 RID: 14658
		UserSortExpressions,
		// Token: 0x04003943 RID: 14659
		SortExpressionIndex,
		// Token: 0x04003944 RID: 14660
		CommandTextValue,
		// Token: 0x04003945 RID: 14661
		RewrittenCommandText,
		// Token: 0x04003946 RID: 14662
		FixedHeader,
		// Token: 0x04003947 RID: 14663
		RowGroupingFixedHeader,
		// Token: 0x04003948 RID: 14664
		ColumnGroupingFixedHeader,
		// Token: 0x04003949 RID: 14665
		SharedDataSourceReferencePath,
		// Token: 0x0400394A RID: 14666
		PropertyNames,
		// Token: 0x0400394B RID: 14667
		FieldPropertyNames,
		// Token: 0x0400394C RID: 14668
		FieldPropertyValues,
		// Token: 0x0400394D RID: 14669
		DynamicFieldReferences,
		// Token: 0x0400394E RID: 14670
		DynamicPropertyReferences,
		// Token: 0x0400394F RID: 14671
		ReferencedProperties,
		// Token: 0x04003950 RID: 14672
		CompiledCodeGeneratedWithRefusedPermissions,
		// Token: 0x04003951 RID: 14673
		InteractiveHeight,
		// Token: 0x04003952 RID: 14674
		InteractiveHeightValue,
		// Token: 0x04003953 RID: 14675
		InteractiveWidth,
		// Token: 0x04003954 RID: 14676
		InteractiveWidthValue,
		// Token: 0x04003955 RID: 14677
		PageNumber,
		// Token: 0x04003956 RID: 14678
		PageSectionOffsets,
		// Token: 0x04003957 RID: 14679
		PageSectionInstanceInfo,
		// Token: 0x04003958 RID: 14680
		SimpleDetailRows,
		// Token: 0x04003959 RID: 14681
		SimpleDetailStartUniqueName,
		// Token: 0x0400395A RID: 14682
		DetailScopeSubReports,
		// Token: 0x0400395B RID: 14683
		DataSetUniqueNameMap,
		// Token: 0x0400395C RID: 14684
		LookupTable,
		// Token: 0x0400395D RID: 14685
		IsSubReportTopLevelScope,
		// Token: 0x0400395E RID: 14686
		NonDetailSortFiltersInScope,
		// Token: 0x0400395F RID: 14687
		DetailSortFiltersInScope,
		// Token: 0x04003960 RID: 14688
		DrillthroughHashtable,
		// Token: 0x04003961 RID: 14689
		RewrittenCommands,
		// Token: 0x04003962 RID: 14690
		CompareOptions,
		// Token: 0x04003963 RID: 14691
		InterpretSubtotalsAsDetails,
		// Token: 0x04003964 RID: 14692
		DiagnosticDetails
	}
}
