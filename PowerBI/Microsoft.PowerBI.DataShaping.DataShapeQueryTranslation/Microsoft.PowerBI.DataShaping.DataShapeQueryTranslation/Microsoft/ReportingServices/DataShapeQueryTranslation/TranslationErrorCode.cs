using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x0200005A RID: 90
	internal enum TranslationErrorCode
	{
		// Token: 0x040001B4 RID: 436
		AggregateWithMultipleInputScopes,
		// Token: 0x040001B5 RID: 437
		AllMandatoryConstraintsInDynamicLimits,
		// Token: 0x040001B6 RID: 438
		ComplexHighlightsNotAllowed,
		// Token: 0x040001B7 RID: 439
		ComplexSlicerNotAllowed,
		// Token: 0x040001B8 RID: 440
		ComplexSlicerNotAllowedWithMeasures,
		// Token: 0x040001B9 RID: 441
		CompoundFilterOperatorNotSupportedWithRemove,
		// Token: 0x040001BA RID: 442
		ConflictingQueryPatternRequirements,
		// Token: 0x040001BB RID: 443
		ContextFilterDataShapeCannotBeMerged,
		// Token: 0x040001BC RID: 444
		ContextFilterDataShapeDoesNotAllowLimits,
		// Token: 0x040001BD RID: 445
		ContextFilterDataShapeDoesNotAllowNestedDataShapes,
		// Token: 0x040001BE RID: 446
		ContextFilterDataShapeIntersectionMustBeEmpty,
		// Token: 0x040001BF RID: 447
		ContextFilterDataShapeMustHaveHierarchyMembers,
		// Token: 0x040001C0 RID: 448
		ContextFilterDoesNotAllowScopeFilterPeer,
		// Token: 0x040001C1 RID: 449
		ContextFilterOnlyAllowsScopeFilterInContextDataShape,
		// Token: 0x040001C2 RID: 450
		DataShapeWithContextFilterMustHaveHierarchyMembers,
		// Token: 0x040001C3 RID: 451
		DecimalCannotFitInCurrency,
		// Token: 0x040001C4 RID: 452
		DetailWithoutGroup,
		// Token: 0x040001C5 RID: 453
		DuplicateId,
		// Token: 0x040001C6 RID: 454
		EntitySetReferenceNotAllowed,
		// Token: 0x040001C7 RID: 455
		ExpressionGenerator_InvalidTypeKind,
		// Token: 0x040001C8 RID: 456
		ExpressionLexer_DecimalWithExponent,
		// Token: 0x040001C9 RID: 457
		ExpressionLexer_DigitExpected,
		// Token: 0x040001CA RID: 458
		ExpressionLexer_Int64WithExponentOrDot,
		// Token: 0x040001CB RID: 459
		ExpressionLexer_InvalidCharacterDetected,
		// Token: 0x040001CC RID: 460
		ExpressionLexer_NumericValueWithoutSuffix,
		// Token: 0x040001CD RID: 461
		ExpressionLexer_UnterminatedLiteral,
		// Token: 0x040001CE RID: 462
		ExpressionLexer_UnterminatedStringLiteral,
		// Token: 0x040001CF RID: 463
		ExpressionParser_CloseParenExpected,
		// Token: 0x040001D0 RID: 464
		ExpressionParser_DaxTextExpected,
		// Token: 0x040001D1 RID: 465
		ExpressionParser_InvalidModelReferenceSyntax,
		// Token: 0x040001D2 RID: 466
		ExpressionParser_InvalidStructureReferenceSyntax,
		// Token: 0x040001D3 RID: 467
		ExpressionParser_InvalidTokenKind,
		// Token: 0x040001D4 RID: 468
		ExpressionParser_NotAllTokensConsumed,
		// Token: 0x040001D5 RID: 469
		ExpressionParser_OpenParenExpected,
		// Token: 0x040001D6 RID: 470
		ExpressionParser_PrematureEndOfExpression,
		// Token: 0x040001D7 RID: 471
		ExpressionParser_UnexpectedTokenKind,
		// Token: 0x040001D8 RID: 472
		ExpressionParser_UnrecognizedLiteral,
		// Token: 0x040001D9 RID: 473
		ExpressionParser_UnsupportedUnaryOperator,
		// Token: 0x040001DA RID: 474
		ImageOrBinaryFieldReferenceNotAllowed,
		// Token: 0x040001DB RID: 475
		InconsistentFilterEmptyGroups,
		// Token: 0x040001DC RID: 476
		InconsistentShowItemsWithNoDataValue,
		// Token: 0x040001DD RID: 477
		InconsistentSortDirectionForBinnedLineSampleSeries,
		// Token: 0x040001DE RID: 478
		InconsistentSortDirectionForSubtotal,
		// Token: 0x040001DF RID: 479
		InTableFilterNotSupportedForModel,
		// Token: 0x040001E0 RID: 480
		InvalidApplyFilterConditionTarget,
		// Token: 0x040001E1 RID: 481
		InvalidApplyFilterDataShapeReference,
		// Token: 0x040001E2 RID: 482
		InvalidBatchSubtotalAnnotation,
		// Token: 0x040001E3 RID: 483
		InvalidCalculationInSyncGroup,
		// Token: 0x040001E4 RID: 484
		InvalidConflictingLimits,
		// Token: 0x040001E5 RID: 485
		InvalidContextFilterConditionTarget,
		// Token: 0x040001E6 RID: 486
		InvalidContextOnlyDataShape,
		// Token: 0x040001E7 RID: 487
		InvalidContextOnlyDataMemberParentIsDataShape,
		// Token: 0x040001E8 RID: 488
		InvalidContextOnlyDataMemberParentIsNotContextOnly,
		// Token: 0x040001E9 RID: 489
		InvalidContextOnlyFlagForFilterContextDataShape,
		// Token: 0x040001EA RID: 490
		InvalidCrossReference,
		// Token: 0x040001EB RID: 491
		InvalidDataShapeNoOutputData,
		// Token: 0x040001EC RID: 492
		InvalidDataShapeStructure,
		// Token: 0x040001ED RID: 493
		InvalidDeepComplexSlicer,
		// Token: 0x040001EE RID: 494
		InvalidDefaultFilterContextConditionTargetExpression,
		// Token: 0x040001EF RID: 495
		InvalidDetailFieldReference,
		// Token: 0x040001F0 RID: 496
		InvalidDetailTableExpression,
		// Token: 0x040001F1 RID: 497
		InvalidDynamicLimitBlockType,
		// Token: 0x040001F2 RID: 498
		InvalidDynamicLimitsStructure,
		// Token: 0x040001F3 RID: 499
		InvalidExistsFilter,
		// Token: 0x040001F4 RID: 500
		InvalidExistsFilterExpression,
		// Token: 0x040001F5 RID: 501
		InvalidExpression,
		// Token: 0x040001F6 RID: 502
		InvalidExtensionDax_UnclosedBracketIdentifier,
		// Token: 0x040001F7 RID: 503
		InvalidExtensionDax_UnclosedMultiLineComment,
		// Token: 0x040001F8 RID: 504
		InvalidExtensionDax_UnclosedParenthesis,
		// Token: 0x040001F9 RID: 505
		InvalidExtensionDax_UnclosedQuoteIdentifier,
		// Token: 0x040001FA RID: 506
		InvalidExtensionDax_UnclosedStringLiteral,
		// Token: 0x040001FB RID: 507
		InvalidExtensionDax_UnexpectedCloseParenthesis,
		// Token: 0x040001FC RID: 508
		InvalidFilterConditionExceedsMaxNumberOfDisjunctionsForSubqueryRewrite,
		// Token: 0x040001FD RID: 509
		InvalidFilterConditionExceedsMaxNumberOfValuesForInFilter,
		// Token: 0x040001FE RID: 510
		InvalidFilterConditionExceedsMaxNumberOfValuesForInFilterTreeRewrite,
		// Token: 0x040001FF RID: 511
		InvalidFilterConditionIncompatibleDataType,
		// Token: 0x04000200 RID: 512
		InvalidFilterConditionMultipleEntitySets,
		// Token: 0x04000201 RID: 513
		InvalidFilterConditionNonHierarchicalEntitySets,
		// Token: 0x04000202 RID: 514
		InvalidFilterEmptyGroupsTarget,
		// Token: 0x04000203 RID: 515
		InvalidFilterTarget,
		// Token: 0x04000204 RID: 516
		InvalidFilterTargetScope,
		// Token: 0x04000205 RID: 517
		InvalidGroupExpression,
		// Token: 0x04000206 RID: 518
		InvalidInFilterWithDuplicateColumns,
		// Token: 0x04000207 RID: 519
		InvalidInFilterTableWithoutIdentityComparison,
		// Token: 0x04000208 RID: 520
		InvalidInFilterValuesAndTable,
		// Token: 0x04000209 RID: 521
		InvalidInstanceFilters,
		// Token: 0x0400020A RID: 522
		InvalidIntersectionLimitNotInnerMostScope,
		// Token: 0x0400020B RID: 523
		InvalidLimitAcrossMultipleGroups,
		// Token: 0x0400020C RID: 524
		InvalidLimitInNestedDataShape,
		// Token: 0x0400020D RID: 525
		InvalidLimitOperator,
		// Token: 0x0400020E RID: 526
		InvalidLimitScopes,
		// Token: 0x0400020F RID: 527
		InvalidLimitTargetNotInnermostGroup,
		// Token: 0x04000210 RID: 528
		InvalidLimitTargets,
		// Token: 0x04000211 RID: 529
		InvalidLimitTargetsScopeType,
		// Token: 0x04000212 RID: 530
		InvalidLimitWithinDataShapeRequired,
		// Token: 0x04000213 RID: 531
		InvalidLiteralDataType,
		// Token: 0x04000214 RID: 532
		InvalidMandatoryLimitCountProduct,
		// Token: 0x04000215 RID: 533
		InvalidMandatoryLimitCountProductInBlock,
		// Token: 0x04000216 RID: 534
		InvalidMeasureInContextFilterDataShape,
		// Token: 0x04000217 RID: 535
		InvalidMixedCompoundFilterCondition,
		// Token: 0x04000218 RID: 536
		InvalidMultipleContextFilters,
		// Token: 0x04000219 RID: 537
		InvalidMultipleFiltersSameTarget,
		// Token: 0x0400021A RID: 538
		InvalidHierarchyLimitGap,
		// Token: 0x0400021B RID: 539
		InvalidMultiplePostRegroupLimit,
		// Token: 0x0400021C RID: 540
		InvalidMultipleScopeFilters,
		// Token: 0x0400021D RID: 541
		InvalidNestedContextFilter,
		// Token: 0x0400021E RID: 542
		InvalidNestedFilterCondition,
		// Token: 0x0400021F RID: 543
		InvalidParent,
		// Token: 0x04000220 RID: 544
		InvalidPeerDataShapes,
		// Token: 0x04000221 RID: 545
		InvalidPeerGroups,
		// Token: 0x04000222 RID: 546
		InvalidScalarValue,
		// Token: 0x04000223 RID: 547
		InvalidSortOnMeasureInSyncTarget,
		// Token: 0x04000224 RID: 548
		InvalidSyncDataShape,
		// Token: 0x04000225 RID: 549
		InvalidSyncGroup,
		// Token: 0x04000226 RID: 550
		InvalidSyncTargetScope,
		// Token: 0x04000227 RID: 551
		InvalidUnconstrainedJoin,
		// Token: 0x04000228 RID: 552
		InvalidUnlimitedBlock,
		// Token: 0x04000229 RID: 553
		InvalidUsageOfEvaluateFunction,
		// Token: 0x0400022A RID: 554
		InvalidValueFilterOnContextOnlyDataShape,
		// Token: 0x0400022B RID: 555
		IsRelatedToManyNotSupportedForDetailTable,
		// Token: 0x0400022C RID: 556
		MissingContainingMemberStartPosition,
		// Token: 0x0400022D RID: 557
		MissingOrEmptyGroupKeys,
		// Token: 0x0400022E RID: 558
		MissingOrInvalidPropertyValue,
		// Token: 0x0400022F RID: 559
		ModelGroupingInstructionsIgnored,
		// Token: 0x04000230 RID: 560
		ModelMeasuresNotSupportedForDetailTable,
		// Token: 0x04000231 RID: 561
		MoreThanOnePrimarySecondaryBlock,
		// Token: 0x04000232 RID: 562
		ModelUnavailable,
		// Token: 0x04000233 RID: 563
		NaNLiteralNotSupported,
		// Token: 0x04000234 RID: 564
		NestedDataShapeWithSubtotal,
		// Token: 0x04000235 RID: 565
		NonNegativeIntegerValueRequired,
		// Token: 0x04000236 RID: 566
		NoUniqueKeyForDetailTable,
		// Token: 0x04000237 RID: 567
		OverlappingKeysOnOppositeHierarchies,
		// Token: 0x04000238 RID: 568
		PositiveIntegerValueRequired,
		// Token: 0x04000239 RID: 569
		RestartTokensOnNestedDataShape,
		// Token: 0x0400023A RID: 570
		ShowAllWithDataTransform,
		// Token: 0x0400023B RID: 571
		SortKeysDuplicateExpressions,
		// Token: 0x0400023C RID: 572
		SortKeysInconsistentStartPosition,
		// Token: 0x0400023D RID: 573
		StartPositionNoSortKeys,
		// Token: 0x0400023E RID: 574
		StartPositionInSecondaryHierarchy,
		// Token: 0x0400023F RID: 575
		StartPositionNotSupportedForSyncTarget,
		// Token: 0x04000240 RID: 576
		StartPositionRequiresSortKeys,
		// Token: 0x04000241 RID: 577
		StartPositionWithRestartTokens,
		// Token: 0x04000242 RID: 578
		SubtotalAndNonSubtotalCalculations,
		// Token: 0x04000243 RID: 579
		SubtotalStartPositionOnNonSubtotal,
		// Token: 0x04000244 RID: 580
		SuppressJoinPredicateOnNonMeasure,
		// Token: 0x04000245 RID: 581
		SyncTargetNotAllowedWithGroupKeys,
		// Token: 0x04000246 RID: 582
		UnexpectedDataTransformParameter,
		// Token: 0x04000247 RID: 583
		UnexpectedQueryGenerationError,
		// Token: 0x04000248 RID: 584
		UnknownDataTransformAlgorithm,
		// Token: 0x04000249 RID: 585
		UnsupportedAggregateOverShowItemsWithNoData,
		// Token: 0x0400024A RID: 586
		UnsupportedDateTimeLiteral,
		// Token: 0x0400024B RID: 587
		UnsupportedNegatedTuplesFilter,
		// Token: 0x0400024C RID: 588
		UnsupportedStringMinMaxColumn,
		// Token: 0x0400024D RID: 589
		UnsupportedStringMinMaxExpression,
		// Token: 0x0400024E RID: 590
		VisualAxisWithoutSort,
		// Token: 0x0400024F RID: 591
		WrongNumberOfDataRows,
		// Token: 0x04000250 RID: 592
		WrongNumberOfFiltersWithContextFilterCondition,
		// Token: 0x04000251 RID: 593
		WrongNumberOfIntersections,
		// Token: 0x04000252 RID: 594
		WrongNumberOfScopeValueDefinitions,
		// Token: 0x04000253 RID: 595
		WrongNumberOfStartPositionValues,
		// Token: 0x04000254 RID: 596
		WrongOrderOfScopeValueDefinitions,
		// Token: 0x04000255 RID: 597
		TopNPerLevelLevelNotPresentOnPrimary
	}
}
