using System;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000049 RID: 73
	[Serializable]
	public enum ModelingErrorCode
	{
		// Token: 0x0400017D RID: 381
		None,
		// Token: 0x0400017E RID: 382
		InvalidDataSourceView,
		// Token: 0x0400017F RID: 383
		InvalidSemanticModel,
		// Token: 0x04000180 RID: 384
		InvalidSemanticQuery,
		// Token: 0x04000181 RID: 385
		InvalidDrillthroughContext,
		// Token: 0x04000182 RID: 386
		InvalidModelGenerationRules,
		// Token: 0x04000183 RID: 387
		InvalidCulture,
		// Token: 0x04000184 RID: 388
		DuplicateItemID,
		// Token: 0x04000185 RID: 389
		InvalidEntityBinding,
		// Token: 0x04000186 RID: 390
		NestedVariations,
		// Token: 0x04000187 RID: 391
		InvalidLinguistics,
		// Token: 0x04000188 RID: 392
		MissingRelationEnd,
		// Token: 0x04000189 RID: 393
		InvalidExpression,
		// Token: 0x0400018A RID: 394
		InvalidFunctionName,
		// Token: 0x0400018B RID: 395
		InvalidAttributeRef,
		// Token: 0x0400018C RID: 396
		InvalidLiteral,
		// Token: 0x0400018D RID: 397
		InvalidLiteralValue,
		// Token: 0x0400018E RID: 398
		ItemNotFound,
		// Token: 0x0400018F RID: 399
		InvalidReferencedItem,
		// Token: 0x04000190 RID: 400
		CircularInheritance,
		// Token: 0x04000191 RID: 401
		SelfReferentialRole,
		// Token: 0x04000192 RID: 402
		GroupingNotFound,
		// Token: 0x04000193 RID: 403
		MeasureNotFound,
		// Token: 0x04000194 RID: 404
		CalculatedAttributeNotFound,
		// Token: 0x04000195 RID: 405
		ParameterNotFound,
		// Token: 0x04000196 RID: 406
		ResultExpressionNotFound,
		// Token: 0x04000197 RID: 407
		MissingItemName,
		// Token: 0x04000198 RID: 408
		IDLocalNameLengthExceeded,
		// Token: 0x04000199 RID: 409
		IDNamespaceLengthExceeded,
		// Token: 0x0400019A RID: 410
		InvalidGuid,
		// Token: 0x0400019B RID: 411
		DuplicateItemName,
		// Token: 0x0400019C RID: 412
		DuplicateEntityName,
		// Token: 0x0400019D RID: 413
		DuplicateFieldName,
		// Token: 0x0400019E RID: 414
		MissingIdentifyingAttributes,
		// Token: 0x0400019F RID: 415
		InvalidSetAttributeReference,
		// Token: 0x040001A0 RID: 416
		InvalidAggregateAttributeReference,
		// Token: 0x040001A1 RID: 417
		InvalidScalarAttributeReference,
		// Token: 0x040001A2 RID: 418
		InvalidNonFilterAttributeReference,
		// Token: 0x040001A3 RID: 419
		InvalidHiddenAttributeReference,
		// Token: 0x040001A4 RID: 420
		ExpressionDataTypeMismatch,
		// Token: 0x040001A5 RID: 421
		ExpressionNullableMismatch,
		// Token: 0x040001A6 RID: 422
		MissingMimeType,
		// Token: 0x040001A7 RID: 423
		IsAggregateWithDefaultAggregate,
		// Token: 0x040001A8 RID: 424
		NonAggregateAsDefaultAggregate,
		// Token: 0x040001A9 RID: 425
		NonVariationAsDefaultAggregate,
		// Token: 0x040001AA RID: 426
		MissingRelatedRole,
		// Token: 0x040001AB RID: 427
		RelatedRolesMismatch,
		// Token: 0x040001AC RID: 428
		InvalidOptionalityOfRoleForColumnBoundEntity,
		// Token: 0x040001AD RID: 429
		InvalidModelItemInPerspective,
		// Token: 0x040001AE RID: 430
		MissingDataSourceView,
		// Token: 0x040001AF RID: 431
		MissingBinding,
		// Token: 0x040001B0 RID: 432
		InvalidBinding,
		// Token: 0x040001B1 RID: 433
		InvalidColumnReferenceInColumnEntity,
		// Token: 0x040001B2 RID: 434
		MissingColumnTableName,
		// Token: 0x040001B3 RID: 435
		InvalidColumnTableName,
		// Token: 0x040001B4 RID: 436
		InvalidColumnDataType,
		// Token: 0x040001B5 RID: 437
		NonPrimaryDataSource,
		// Token: 0x040001B6 RID: 438
		MissingPrimaryKey,
		// Token: 0x040001B7 RID: 439
		BinaryEntityColumn,
		// Token: 0x040001B8 RID: 440
		InvalidInheritanceRelationTable,
		// Token: 0x040001B9 RID: 441
		NonUniqueInheritanceRelationColumns,
		// Token: 0x040001BA RID: 442
		ColumnDataTypeMismatch,
		// Token: 0x040001BB RID: 443
		ColumnNullableMismatch,
		// Token: 0x040001BC RID: 444
		IsAggregateWithColumn,
		// Token: 0x040001BD RID: 445
		PromoteLookupForNonLookupEntity,
		// Token: 0x040001BE RID: 446
		RoleRelationsMismatch,
		// Token: 0x040001BF RID: 447
		RoleRelationEndsMismatch,
		// Token: 0x040001C0 RID: 448
		InvalidRoleRelationTable,
		// Token: 0x040001C1 RID: 449
		NonUniqueRoleRelationColumns,
		// Token: 0x040001C2 RID: 450
		NonBooleanFilterAttribute,
		// Token: 0x040001C3 RID: 451
		CyclicExpression,
		// Token: 0x040001C4 RID: 452
		FieldReferenceOutOfContext,
		// Token: 0x040001C5 RID: 453
		EntityReferenceOutOfContext,
		// Token: 0x040001C6 RID: 454
		NonAggregateExpression,
		// Token: 0x040001C7 RID: 455
		AggregateWithNonAggregateArgument,
		// Token: 0x040001C8 RID: 456
		WrongNumberOfArguments,
		// Token: 0x040001C9 RID: 457
		ArgumentDataTypeMismatch,
		// Token: 0x040001CA RID: 458
		ArgumentCardinalityMismatch,
		// Token: 0x040001CB RID: 459
		ArgumentValueOutOfRange,
		// Token: 0x040001CC RID: 460
		NonLiteralArgument,
		// Token: 0x040001CD RID: 461
		InvalidDateIntervalArgument,
		// Token: 0x040001CE RID: 462
		InvalidDateIntervalValue,
		// Token: 0x040001CF RID: 463
		InvalidInSetArgument,
		// Token: 0x040001D0 RID: 464
		InvalidLiteralSetArgument,
		// Token: 0x040001D1 RID: 465
		ImplicitDecimalCastToFloat,
		// Token: 0x040001D2 RID: 466
		EntityKeyTypeMismatch,
		// Token: 0x040001D3 RID: 467
		MissingExpressionName,
		// Token: 0x040001D4 RID: 468
		TopLevelSetExpression,
		// Token: 0x040001D5 RID: 469
		EmptySemanticQuery,
		// Token: 0x040001D6 RID: 470
		MultipleHierarchies,
		// Token: 0x040001D7 RID: 471
		MultipleMeasureGroups,
		// Token: 0x040001D8 RID: 472
		DuplicateGroupingName,
		// Token: 0x040001D9 RID: 473
		DuplicateExpressionName,
		// Token: 0x040001DA RID: 474
		MissingBaseEntity,
		// Token: 0x040001DB RID: 475
		MissingGroupingName,
		// Token: 0x040001DC RID: 476
		MissingGroupingExpression,
		// Token: 0x040001DD RID: 477
		BinaryGroupingExpression,
		// Token: 0x040001DE RID: 478
		NonEntityGroupingWithDetails,
		// Token: 0x040001DF RID: 479
		InvalidFilter,
		// Token: 0x040001E0 RID: 480
		BaseEntityMismatch,
		// Token: 0x040001E1 RID: 481
		MissingMeasures,
		// Token: 0x040001E2 RID: 482
		DuplicateParameterName,
		// Token: 0x040001E3 RID: 483
		MissingParameterName,
		// Token: 0x040001E4 RID: 484
		InvalidParameterName,
		// Token: 0x040001E5 RID: 485
		InvalidParameterExpression,
		// Token: 0x040001E6 RID: 486
		ParameterExpressionDataTypeMismatch,
		// Token: 0x040001E7 RID: 487
		ParameterExpressionCardinalityMismatch,
		// Token: 0x040001E8 RID: 488
		ParameterExpressionNullableMismatch,
		// Token: 0x040001E9 RID: 489
		InvalidParameterValueType,
		// Token: 0x040001EA RID: 490
		InvalidParameterValueCardinality,
		// Token: 0x040001EB RID: 491
		NullParameterValue,
		// Token: 0x040001EC RID: 492
		MissingParameterValue,
		// Token: 0x040001ED RID: 493
		UnusedParameterValue,
		// Token: 0x040001EE RID: 494
		InvalidDrillSelectedItems,
		// Token: 0x040001EF RID: 495
		InvalidDrillSelectedPath,
		// Token: 0x040001F0 RID: 496
		InvalidDrillTargetEntity,
		// Token: 0x040001F1 RID: 497
		LoopInSecurityFilters,
		// Token: 0x040001F2 RID: 498
		WrongSemanticModel,
		// Token: 0x040001F3 RID: 499
		WrongSemanticQuery,
		// Token: 0x040001F4 RID: 500
		InvalidEntityKeyValue,
		// Token: 0x040001F5 RID: 501
		InvalidEntityKeyPart
	}
}
