using System;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000047 RID: 71
	[Serializable]
	internal enum DataShapeGenerationErrorCode
	{
		// Token: 0x04000131 RID: 305
		CannotProcessSelectExpression,
		// Token: 0x04000132 RID: 306
		CouldNotApplyAdditionalODataFilter,
		// Token: 0x04000133 RID: 307
		CouldNotConvertLiteralExpression,
		// Token: 0x04000134 RID: 308
		CouldNotCreateLimit,
		// Token: 0x04000135 RID: 309
		CouldNotInterpretAlgorithm,
		// Token: 0x04000136 RID: 310
		CouldNotNormalizeDataShapeBinding,
		// Token: 0x04000137 RID: 311
		CouldNotResolveDataShapeBinding,
		// Token: 0x04000138 RID: 312
		CouldNotResolveHiddenProjectionExpressionName,
		// Token: 0x04000139 RID: 313
		CouldNotResolveHighlightFilter,
		// Token: 0x0400013A RID: 314
		CouldNotResolveInstanceFilter,
		// Token: 0x0400013B RID: 315
		CouldNotResolveLetReference,
		// Token: 0x0400013C RID: 316
		CouldNotResolveModelReferencesInQueryExtensionSchema,
		// Token: 0x0400013D RID: 317
		CouldNotResolveModelReferencesInSemanticQuery,
		// Token: 0x0400013E RID: 318
		CouldNotResolveQueryExtensionSchema,
		// Token: 0x0400013F RID: 319
		CouldNotResolveQueryParameterReference,
		// Token: 0x04000140 RID: 320
		CouldNotResolveQuerySelectName,
		// Token: 0x04000141 RID: 321
		CouldNotResolveSemanticQueryDefinition,
		// Token: 0x04000142 RID: 322
		CouldNotResolveSourceReference,
		// Token: 0x04000143 RID: 323
		CouldNotResolveSuppressedJoinPredicateExpressionName,
		// Token: 0x04000144 RID: 324
		CouldNotResolveSuppressedJoinPredicateSourceName,
		// Token: 0x04000145 RID: 325
		CouldNotResolveTargetScopeForVisualAxis,
		// Token: 0x04000146 RID: 326
		CouldNotRewriteExpressionToExtensionSchemaItemCommand,
		// Token: 0x04000147 RID: 327
		CouldNotUpgradeDataShapeBinding,
		// Token: 0x04000148 RID: 328
		CouldNotUpgradeExtensionSchema,
		// Token: 0x04000149 RID: 329
		CouldNotUpgradeSemanticQueryDefinition,
		// Token: 0x0400014A RID: 330
		DifferentContextDataShapeFilterTargets,
		// Token: 0x0400014B RID: 331
		DuplicateGroupingKeysDetected,
		// Token: 0x0400014C RID: 332
		ExpressionToExtensionSchemaItemRewriterFailed,
		// Token: 0x0400014D RID: 333
		ExtensionColumnAndMeasureNamesNotUnique,
		// Token: 0x0400014E RID: 334
		ExtensionColumnEmptyExpression,
		// Token: 0x0400014F RID: 335
		ExtensionColumnNameNotUnique,
		// Token: 0x04000150 RID: 336
		ExtensionColumnNameNotUniqueFlexible,
		// Token: 0x04000151 RID: 337
		ExtensionColumnNameNotUniqueModel,
		// Token: 0x04000152 RID: 338
		ExtensionEntityNameDoesNotMatchExtends,
		// Token: 0x04000153 RID: 339
		ExtensionEntityNameNotUnique,
		// Token: 0x04000154 RID: 340
		ExtensionMeasureEmptyExpression,
		// Token: 0x04000155 RID: 341
		ExtensionMeasureNameNotUnique,
		// Token: 0x04000156 RID: 342
		ExtensionMeasureNameNotUniqueFlexible,
		// Token: 0x04000157 RID: 343
		ExtensionMeasureNameNotUniqueModel,
		// Token: 0x04000158 RID: 344
		ExtensionSchemaNameMatchesBaseEntityContainer,
		// Token: 0x04000159 RID: 345
		FilterIncompatibleWithParameter,
		// Token: 0x0400015A RID: 346
		FlexibleExtensionColumnWithNoExtensionEntityExtendsProperty,
		// Token: 0x0400015B RID: 347
		FlexibleExtensionEntityCanNotHaveColumns,
		// Token: 0x0400015C RID: 348
		FlexibleExtensionEntityShouldNotHaveExtendsProperty,
		// Token: 0x0400015D RID: 349
		FlexibleExtensionSchemaWithEmptyModel,
		// Token: 0x0400015E RID: 350
		GroupByAndSubqueryReferenceInSelect,
		// Token: 0x0400015F RID: 351
		GroupByAndSubtotal,
		// Token: 0x04000160 RID: 352
		GroupByNotSupportedWithScopedDataReduction,
		// Token: 0x04000161 RID: 353
		GroupByWithoutColumnSelect,
		// Token: 0x04000162 RID: 354
		HiddenProjectionSourceNameMustBeNull,
		// Token: 0x04000163 RID: 355
		IgnoredDataReductionAlgorithm,
		// Token: 0x04000164 RID: 356
		IgnoredOrderBy,
		// Token: 0x04000165 RID: 357
		InFilterReferencingSubqueryWithTransform,
		// Token: 0x04000166 RID: 358
		InternalDataShapeGenerationError,
		// Token: 0x04000167 RID: 359
		InvalidAggregateOfHiddenProjection,
		// Token: 0x04000168 RID: 360
		InvalidAnyValueOrDefaultValue,
		// Token: 0x04000169 RID: 361
		InvalidBetweenFilterConditionExpression,
		// Token: 0x0400016A RID: 362
		InvalidBinaryFilterConditionExpression,
		// Token: 0x0400016B RID: 363
		InvalidBinaryOperatorForAnyValueOrDefaultValue,
		// Token: 0x0400016C RID: 364
		InvalidDataReductionAlgorithmCount,
		// Token: 0x0400016D RID: 365
		InvalidDataShapeBindingAggregate,
		// Token: 0x0400016E RID: 366
		InvalidDataShapeBindingAxisWithSubqueryRegrouping,
		// Token: 0x0400016F RID: 367
		InvalidDataShapeBindingWithSubqueryRegrouping,
		// Token: 0x04000170 RID: 368
		InvalidExpansionStateEmptyExpansionInstances,
		// Token: 0x04000171 RID: 369
		InvalidExpansionStateInvalidExpansionLevel,
		// Token: 0x04000172 RID: 370
		InvalidExpansionStateMissingExpansionInstances,
		// Token: 0x04000173 RID: 371
		InvalidExpansionStateMissingExpansionLevels,
		// Token: 0x04000174 RID: 372
		InvalidExpression,
		// Token: 0x04000175 RID: 373
		InvalidExtensionSchema,
		// Token: 0x04000176 RID: 374
		InvalidFilterComparisonIncompatibleExpressions,
		// Token: 0x04000177 RID: 375
		InvalidFilteredEval,
		// Token: 0x04000178 RID: 376
		InvalidFilterExceedsMaxNumberOfValuesForInFilter,
		// Token: 0x04000179 RID: 377
		InvalidFilterSourceReference,
		// Token: 0x0400017A RID: 378
		InvalidFilterTargetExpression,
		// Token: 0x0400017B RID: 379
		InvalidFilterTargetForAnyValueOrDefaultValue,
		// Token: 0x0400017C RID: 380
		InvalidFilterTargetScope,
		// Token: 0x0400017D RID: 381
		InvalidFilterType,
		// Token: 0x0400017E RID: 382
		InvalidFromSourceExpressionWithSubqueryRegrouping,
		// Token: 0x0400017F RID: 383
		InvalidFromSourceItemWithSubqueryRegrouping,
		// Token: 0x04000180 RID: 384
		InvalidGroupSynchronization,
		// Token: 0x04000181 RID: 385
		InvalidInTableTypeFilterConditionMultipleEntities,
		// Token: 0x04000182 RID: 386
		InvalidNumberOfSubqueries,
		// Token: 0x04000183 RID: 387
		InvalidOrMalformedAnchorTime,
		// Token: 0x04000184 RID: 388
		InvalidOrMalformedDataShapeBinding,
		// Token: 0x04000185 RID: 389
		InvalidOrMalformedDataShapeFilterConditionExpressions,
		// Token: 0x04000186 RID: 390
		InvalidOrMalformedExpression,
		// Token: 0x04000187 RID: 391
		InvalidOrMalformedExtensionSchema,
		// Token: 0x04000188 RID: 392
		InvalidOrMalformedOrderByExpression,
		// Token: 0x04000189 RID: 393
		InvalidOrMalformedSemanticQueryDataShape,
		// Token: 0x0400018A RID: 394
		InvalidOrMalformedSemanticQueryDataShapeCommand,
		// Token: 0x0400018B RID: 395
		InvalidOrMalformedSemanticQueryDefinition,
		// Token: 0x0400018C RID: 396
		InvalidPlotAxisBindingIndex,
		// Token: 0x0400018D RID: 397
		InvalidPrimaryScalarKeyIndex,
		// Token: 0x0400018E RID: 398
		InvalidQueryParameterReferenceType,
		// Token: 0x0400018F RID: 399
		InvalidQueryParameterTypeExpression,
		// Token: 0x04000190 RID: 400
		InvalidScopedDataReductionAlgorithm,
		// Token: 0x04000191 RID: 401
		InvalidScopedDataReductionIndices,
		// Token: 0x04000192 RID: 402
		InvalidSingleValueAggregation,
		// Token: 0x04000193 RID: 403
		InvalidSortByTransformOutputRoleRef,
		// Token: 0x04000194 RID: 404
		InvalidTableArgumentToInOperatorInFilter,
		// Token: 0x04000195 RID: 405
		InvalidTopNPerLevelDataReduction,
		// Token: 0x04000196 RID: 406
		InvalidTransformColumnExpression,
		// Token: 0x04000197 RID: 407
		InvalidTransformColumnExpressionSchemaReference,
		// Token: 0x04000198 RID: 408
		InvalidTransformColumnReference,
		// Token: 0x04000199 RID: 409
		InvalidTransformOutputRoleRefExpression,
		// Token: 0x0400019A RID: 410
		InvalidTransformParameterExpression,
		// Token: 0x0400019B RID: 411
		InvalidVisualShapeAxisGroupProjection,
		// Token: 0x0400019C RID: 412
		InvalidWhereItemWithSubqueryAggregation,
		// Token: 0x0400019D RID: 413
		InvalidWindowScope,
		// Token: 0x0400019E RID: 414
		IsAfterNotSupported,
		// Token: 0x0400019F RID: 415
		MismatchedArgumentCountsToInOperator,
		// Token: 0x040001A0 RID: 416
		MismatchedArgumentCountsToInOperatorTableType,
		// Token: 0x040001A1 RID: 417
		MismatchedColumnArgumentsToInOperatorInFilter,
		// Token: 0x040001A2 RID: 418
		MissingIsAfterWithPrimaySubsetCovered,
		// Token: 0x040001A3 RID: 419
		NaNLiteralNotSupported,
		// Token: 0x040001A4 RID: 420
		NoInnermostDynamicDataMemberFound,
		// Token: 0x040001A5 RID: 421
		NoInnermostDynamicDataMemberFoundForIntersectionLimit,
		// Token: 0x040001A6 RID: 422
		NonColumnArgumentToInOperatorInFilter,
		// Token: 0x040001A7 RID: 423
		NonexistentDataVolumeLevel,
		// Token: 0x040001A8 RID: 424
		NoStableKeyAndNoVisibleFieldsForSelectedEntity,
		// Token: 0x040001A9 RID: 425
		NoStableKeyForSelectedEntity,
		// Token: 0x040001AA RID: 426
		OverlappingDataReductionScopes,
		// Token: 0x040001AB RID: 427
		OverlappingKeysOnOppositeHierarchies,
		// Token: 0x040001AC RID: 428
		ParameterMappingFilterConflict,
		// Token: 0x040001AD RID: 429
		ParameterMappingsNotSupportedOnHighlights,
		// Token: 0x040001AE RID: 430
		QueryTopWithDataReduction,
		// Token: 0x040001AF RID: 431
		QueryTopWithoutPrimary,
		// Token: 0x040001B0 RID: 432
		QueryTopWithSecondary,
		// Token: 0x040001B1 RID: 433
		ScopedDataReductionIntersectionScope,
		// Token: 0x040001B2 RID: 434
		ScopedReductionWithIntersectionReduction,
		// Token: 0x040001B3 RID: 435
		SecondaryGroupsWithoutPrimary,
		// Token: 0x040001B4 RID: 436
		SingleValueParameterWithMultipleValues,
		// Token: 0x040001B5 RID: 437
		SpecifiedDataWindowSizeExceedsMaxIntersections,
		// Token: 0x040001B6 RID: 438
		SpecifiedIntersectionReductionAlgorithmExceedsMaxIntersections,
		// Token: 0x040001B7 RID: 439
		SpecifiedLimitExceedsMaxIntersections,
		// Token: 0x040001B8 RID: 440
		SpecifiedMaxDynamicSeriesExceedsMaxAllowedDynamicSeries,
		// Token: 0x040001B9 RID: 441
		SpecifiedMinPointsPerSeriesExceedsMaxIntersections,
		// Token: 0x040001BA RID: 442
		SpecifiedPerLevelLimitExceedsMaxIntersections,
		// Token: 0x040001BB RID: 443
		SpecifiedReductionAlgorithmsExceedsMaxIntersections,
		// Token: 0x040001BC RID: 444
		TooManyExtensionProperties,
		// Token: 0x040001BD RID: 445
		TooManyScopedDataReductions,
		// Token: 0x040001BE RID: 446
		TransformAndGroupBy,
		// Token: 0x040001BF RID: 447
		TransformAndHighlight,
		// Token: 0x040001C0 RID: 448
		TransformAndInstanceFilter,
		// Token: 0x040001C1 RID: 449
		TransformAndScopedAggregates,
		// Token: 0x040001C2 RID: 450
		TransformAndSecondaryGroup,
		// Token: 0x040001C3 RID: 451
		TransformAndSubtotal,
		// Token: 0x040001C4 RID: 452
		TransformAndSuppressedProjections,
		// Token: 0x040001C5 RID: 453
		TransformInsideSubquery,
		// Token: 0x040001C6 RID: 454
		TransformReferencedFromMultipleScopes,
		// Token: 0x040001C7 RID: 455
		TransformRefersMultipleSubqueries,
		// Token: 0x040001C8 RID: 456
		TransformWithoutDataShapeBinding,
		// Token: 0x040001C9 RID: 457
		TransformWithSchemaOrSubqueryReferenceInSameScope,
		// Token: 0x040001CA RID: 458
		UnexpectedLimitType,
		// Token: 0x040001CB RID: 459
		UnexpectedReductionAlgorithmType,
		// Token: 0x040001CC RID: 460
		UnhandledExpression,
		// Token: 0x040001CD RID: 461
		UnsupportedDataSourceVariables,
		// Token: 0x040001CE RID: 462
		UnsupportedDuplicateVisualShapeKey,
		// Token: 0x040001CF RID: 463
		UnsupportedExpansionWithoutTotalsInSemanticQueryDataShapeCommand,
		// Token: 0x040001D0 RID: 464
		UnsupportedExtensionColumnDataType,
		// Token: 0x040001D1 RID: 465
		UnsupportedExtensionMeasureDataType,
		// Token: 0x040001D2 RID: 466
		UnsupportedFeatureInSemanticQueryDataShapeCommand,
		// Token: 0x040001D3 RID: 467
		UnsupportedFeatureOnContainer,
		// Token: 0x040001D4 RID: 468
		UnsupportedFeatureWithGenerationOptionInQueryDefinition,
		// Token: 0x040001D5 RID: 469
		UnsupportedGroupByInSemanticQuery,
		// Token: 0x040001D6 RID: 470
		UnsupportedGroupSynchronization,
		// Token: 0x040001D7 RID: 471
		UnsupportedHiddenProjection,
		// Token: 0x040001D8 RID: 472
		UnsupportedInconsistentTotalsInVisualShapeAxis,
		// Token: 0x040001D9 RID: 473
		UnsupportedIntermediateUnprojectedGroup,
		// Token: 0x040001DA RID: 474
		UnsupportedMissingVisualShape,
		// Token: 0x040001DB RID: 475
		UnsupportedMultipleGroupByEntitiesInSemanticQuery,
		// Token: 0x040001DC RID: 476
		UnsupportedMultipleSubqueryPostFilters,
		// Token: 0x040001DD RID: 477
		UnsupportedProjectionIndexInSemanticQuery,
		// Token: 0x040001DE RID: 478
		UnsupportedScopedDataReduction,
		// Token: 0x040001DF RID: 479
		UnsupportedSortByMeasure,
		// Token: 0x040001E0 RID: 480
		UnsupportedSparklineNumberOfPoints,
		// Token: 0x040001E1 RID: 481
		UnsupportedSparklineWithVisualCalculation,
		// Token: 0x040001E2 RID: 482
		UnsupportedSubqueryReferenceByTransform,
		// Token: 0x040001E3 RID: 483
		UnsupportedSubqueryRegrouping,
		// Token: 0x040001E4 RID: 484
		UnsupportedSubqueryUsage,
		// Token: 0x040001E5 RID: 485
		UnsupportedSuppressedProjectionWithVisualCalculations,
		// Token: 0x040001E6 RID: 486
		UnsupportedTableExpression,
		// Token: 0x040001E7 RID: 487
		UnsupportedUnprojectedFirstVisualShapeAxisGroup,
		// Token: 0x040001E8 RID: 488
		UnsupportedVisualCalculation,
		// Token: 0x040001E9 RID: 489
		UnsupportedVisualCalculationLanguage,
		// Token: 0x040001EA RID: 490
		UnsupportedVisualCalculationWithGroupBy,
		// Token: 0x040001EB RID: 491
		UnsupportedVisualCalculationWithShowItemsWithNoData,
		// Token: 0x040001EC RID: 492
		UnsupportedVisualCalculationWithTransforms,
		// Token: 0x040001ED RID: 493
		UnsupportedVisualShapeAxisGroupKeyExpressionType,
		// Token: 0x040001EE RID: 494
		UnsupportedVisualShapeAxisGroupKeysProjectedAcrossDSBGroups,
		// Token: 0x040001EF RID: 495
		UnsupportedVisualShapeAxisGroupKeyWithoutCorrespondingSelect,
		// Token: 0x040001F0 RID: 496
		UnsupportedVisualShapeAxisGroupOutOfOrder,
		// Token: 0x040001F1 RID: 497
		UnsupportedVisualShapeAxisGroupTotals,
		// Token: 0x040001F2 RID: 498
		UnsupportedVisualShapeDataMemberMismatch,
		// Token: 0x040001F3 RID: 499
		UnsupportedVisualShapeWithoutDataShapeBinding,
		// Token: 0x040001F4 RID: 500
		UnsupportedVisualShapeWithoutVisualCalcs
	}
}
