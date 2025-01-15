using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x020000B4 RID: 180
	internal enum ObjectType
	{
		// Token: 0x040001CA RID: 458
		ActionPath,
		// Token: 0x040001CB RID: 459
		ActionSplitByName,
		// Token: 0x040001CC RID: 460
		ActionSplitByValues,
		// Token: 0x040001CD RID: 461
		AnyValueFilterCondition,
		// Token: 0x040001CE RID: 462
		ApplyFilterCondition,
		// Token: 0x040001CF RID: 463
		BinaryFilterCondition,
		// Token: 0x040001D0 RID: 464
		Bin,
		// Token: 0x040001D1 RID: 465
		BinnedLineSampleLimitOperator,
		// Token: 0x040001D2 RID: 466
		Binning,
		// Token: 0x040001D3 RID: 467
		BinningCondition,
		// Token: 0x040001D4 RID: 468
		BottomLimitOperator,
		// Token: 0x040001D5 RID: 469
		Calculation,
		// Token: 0x040001D6 RID: 470
		CompoundFilterCondition,
		// Token: 0x040001D7 RID: 471
		ContextFilterCondition,
		// Token: 0x040001D8 RID: 472
		DataHierarchy,
		// Token: 0x040001D9 RID: 473
		DataIntersection,
		// Token: 0x040001DA RID: 474
		DataMember,
		// Token: 0x040001DB RID: 475
		DataRow,
		// Token: 0x040001DC RID: 476
		DataSet,
		// Token: 0x040001DD RID: 477
		DataShape,
		// Token: 0x040001DE RID: 478
		DataSource,
		// Token: 0x040001DF RID: 479
		DataTransform,
		// Token: 0x040001E0 RID: 480
		DataTransformParameter,
		// Token: 0x040001E1 RID: 481
		DataTransformTable,
		// Token: 0x040001E2 RID: 482
		DataTransformTableColumn,
		// Token: 0x040001E3 RID: 483
		DetailGroupIdentity,
		// Token: 0x040001E4 RID: 484
		DefaultValueFilterCondition,
		// Token: 0x040001E5 RID: 485
		DynamicLimit,
		// Token: 0x040001E6 RID: 486
		DynamicLimitEvenDistributionBlock,
		// Token: 0x040001E7 RID: 487
		DynamicLimitPrimarySecondaryBlock,
		// Token: 0x040001E8 RID: 488
		DynamicLimits,
		// Token: 0x040001E9 RID: 489
		ExistsFilterCondition,
		// Token: 0x040001EA RID: 490
		ExistsFilterItem,
		// Token: 0x040001EB RID: 491
		ExtensionColumn,
		// Token: 0x040001EC RID: 492
		ExtensionMeasure,
		// Token: 0x040001ED RID: 493
		Filter,
		// Token: 0x040001EE RID: 494
		FilterCondition,
		// Token: 0x040001EF RID: 495
		FilterEmptyGroupsCondition,
		// Token: 0x040001F0 RID: 496
		FirstLimitOperator,
		// Token: 0x040001F1 RID: 497
		Group,
		// Token: 0x040001F2 RID: 498
		GroupKey,
		// Token: 0x040001F3 RID: 499
		InFilterCondition,
		// Token: 0x040001F4 RID: 500
		LastLimitOperator,
		// Token: 0x040001F5 RID: 501
		Limit,
		// Token: 0x040001F6 RID: 502
		LimitOperator,
		// Token: 0x040001F7 RID: 503
		LimitPlotAxis,
		// Token: 0x040001F8 RID: 504
		ModelParameter,
		// Token: 0x040001F9 RID: 505
		OverlappingPointsSampleLimitOperator,
		// Token: 0x040001FA RID: 506
		RestartDefinition,
		// Token: 0x040001FB RID: 507
		RestartToken,
		// Token: 0x040001FC RID: 508
		SampleLimitOperator,
		// Token: 0x040001FD RID: 509
		ScopeValue,
		// Token: 0x040001FE RID: 510
		ScopeValueDefinition,
		// Token: 0x040001FF RID: 511
		SortKey,
		// Token: 0x04000200 RID: 512
		StartPosition,
		// Token: 0x04000201 RID: 513
		SyncTarget,
		// Token: 0x04000202 RID: 514
		TopLimitOperator,
		// Token: 0x04000203 RID: 515
		TopNPerLevelLimitOperator,
		// Token: 0x04000204 RID: 516
		UnaryFilterCondition,
		// Token: 0x04000205 RID: 517
		VisualAxis,
		// Token: 0x04000206 RID: 518
		VisualAxisGroup,
		// Token: 0x04000207 RID: 519
		VisualAxisGroupMember,
		// Token: 0x04000208 RID: 520
		WindowLimitOperator
	}
}
