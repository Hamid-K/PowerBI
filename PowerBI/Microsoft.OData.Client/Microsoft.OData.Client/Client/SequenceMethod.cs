using System;

namespace Microsoft.OData.Client
{
	// Token: 0x02000098 RID: 152
	internal enum SequenceMethod
	{
		// Token: 0x04000165 RID: 357
		Where,
		// Token: 0x04000166 RID: 358
		WhereOrdinal,
		// Token: 0x04000167 RID: 359
		OfType,
		// Token: 0x04000168 RID: 360
		Cast,
		// Token: 0x04000169 RID: 361
		Select,
		// Token: 0x0400016A RID: 362
		SelectOrdinal,
		// Token: 0x0400016B RID: 363
		SelectMany,
		// Token: 0x0400016C RID: 364
		SelectManyOrdinal,
		// Token: 0x0400016D RID: 365
		SelectManyResultSelector,
		// Token: 0x0400016E RID: 366
		SelectManyOrdinalResultSelector,
		// Token: 0x0400016F RID: 367
		Join,
		// Token: 0x04000170 RID: 368
		JoinComparer,
		// Token: 0x04000171 RID: 369
		GroupJoin,
		// Token: 0x04000172 RID: 370
		GroupJoinComparer,
		// Token: 0x04000173 RID: 371
		OrderBy,
		// Token: 0x04000174 RID: 372
		OrderByComparer,
		// Token: 0x04000175 RID: 373
		OrderByDescending,
		// Token: 0x04000176 RID: 374
		OrderByDescendingComparer,
		// Token: 0x04000177 RID: 375
		ThenBy,
		// Token: 0x04000178 RID: 376
		ThenByComparer,
		// Token: 0x04000179 RID: 377
		ThenByDescending,
		// Token: 0x0400017A RID: 378
		ThenByDescendingComparer,
		// Token: 0x0400017B RID: 379
		Take,
		// Token: 0x0400017C RID: 380
		TakeWhile,
		// Token: 0x0400017D RID: 381
		TakeWhileOrdinal,
		// Token: 0x0400017E RID: 382
		Skip,
		// Token: 0x0400017F RID: 383
		SkipWhile,
		// Token: 0x04000180 RID: 384
		SkipWhileOrdinal,
		// Token: 0x04000181 RID: 385
		GroupBy,
		// Token: 0x04000182 RID: 386
		GroupByComparer,
		// Token: 0x04000183 RID: 387
		GroupByElementSelector,
		// Token: 0x04000184 RID: 388
		GroupByElementSelectorComparer,
		// Token: 0x04000185 RID: 389
		GroupByResultSelector,
		// Token: 0x04000186 RID: 390
		GroupByResultSelectorComparer,
		// Token: 0x04000187 RID: 391
		GroupByElementSelectorResultSelector,
		// Token: 0x04000188 RID: 392
		GroupByElementSelectorResultSelectorComparer,
		// Token: 0x04000189 RID: 393
		Distinct,
		// Token: 0x0400018A RID: 394
		DistinctComparer,
		// Token: 0x0400018B RID: 395
		Concat,
		// Token: 0x0400018C RID: 396
		Union,
		// Token: 0x0400018D RID: 397
		UnionComparer,
		// Token: 0x0400018E RID: 398
		Intersect,
		// Token: 0x0400018F RID: 399
		IntersectComparer,
		// Token: 0x04000190 RID: 400
		Except,
		// Token: 0x04000191 RID: 401
		ExceptComparer,
		// Token: 0x04000192 RID: 402
		First,
		// Token: 0x04000193 RID: 403
		FirstPredicate,
		// Token: 0x04000194 RID: 404
		FirstOrDefault,
		// Token: 0x04000195 RID: 405
		FirstOrDefaultPredicate,
		// Token: 0x04000196 RID: 406
		Last,
		// Token: 0x04000197 RID: 407
		LastPredicate,
		// Token: 0x04000198 RID: 408
		LastOrDefault,
		// Token: 0x04000199 RID: 409
		LastOrDefaultPredicate,
		// Token: 0x0400019A RID: 410
		Single,
		// Token: 0x0400019B RID: 411
		SinglePredicate,
		// Token: 0x0400019C RID: 412
		SingleOrDefault,
		// Token: 0x0400019D RID: 413
		SingleOrDefaultPredicate,
		// Token: 0x0400019E RID: 414
		ElementAt,
		// Token: 0x0400019F RID: 415
		ElementAtOrDefault,
		// Token: 0x040001A0 RID: 416
		DefaultIfEmpty,
		// Token: 0x040001A1 RID: 417
		DefaultIfEmptyValue,
		// Token: 0x040001A2 RID: 418
		Contains,
		// Token: 0x040001A3 RID: 419
		ContainsComparer,
		// Token: 0x040001A4 RID: 420
		Reverse,
		// Token: 0x040001A5 RID: 421
		Empty,
		// Token: 0x040001A6 RID: 422
		SequenceEqual,
		// Token: 0x040001A7 RID: 423
		SequenceEqualComparer,
		// Token: 0x040001A8 RID: 424
		Any,
		// Token: 0x040001A9 RID: 425
		AnyPredicate,
		// Token: 0x040001AA RID: 426
		All,
		// Token: 0x040001AB RID: 427
		Count,
		// Token: 0x040001AC RID: 428
		CountPredicate,
		// Token: 0x040001AD RID: 429
		LongCount,
		// Token: 0x040001AE RID: 430
		LongCountPredicate,
		// Token: 0x040001AF RID: 431
		Min,
		// Token: 0x040001B0 RID: 432
		MinSelector,
		// Token: 0x040001B1 RID: 433
		Max,
		// Token: 0x040001B2 RID: 434
		MaxSelector,
		// Token: 0x040001B3 RID: 435
		MinInt,
		// Token: 0x040001B4 RID: 436
		MinNullableInt,
		// Token: 0x040001B5 RID: 437
		MinLong,
		// Token: 0x040001B6 RID: 438
		MinNullableLong,
		// Token: 0x040001B7 RID: 439
		MinDouble,
		// Token: 0x040001B8 RID: 440
		MinNullableDouble,
		// Token: 0x040001B9 RID: 441
		MinDecimal,
		// Token: 0x040001BA RID: 442
		MinNullableDecimal,
		// Token: 0x040001BB RID: 443
		MinSingle,
		// Token: 0x040001BC RID: 444
		MinNullableSingle,
		// Token: 0x040001BD RID: 445
		MinIntSelector,
		// Token: 0x040001BE RID: 446
		MinNullableIntSelector,
		// Token: 0x040001BF RID: 447
		MinLongSelector,
		// Token: 0x040001C0 RID: 448
		MinNullableLongSelector,
		// Token: 0x040001C1 RID: 449
		MinDoubleSelector,
		// Token: 0x040001C2 RID: 450
		MinNullableDoubleSelector,
		// Token: 0x040001C3 RID: 451
		MinDecimalSelector,
		// Token: 0x040001C4 RID: 452
		MinNullableDecimalSelector,
		// Token: 0x040001C5 RID: 453
		MinSingleSelector,
		// Token: 0x040001C6 RID: 454
		MinNullableSingleSelector,
		// Token: 0x040001C7 RID: 455
		MaxInt,
		// Token: 0x040001C8 RID: 456
		MaxNullableInt,
		// Token: 0x040001C9 RID: 457
		MaxLong,
		// Token: 0x040001CA RID: 458
		MaxNullableLong,
		// Token: 0x040001CB RID: 459
		MaxDouble,
		// Token: 0x040001CC RID: 460
		MaxNullableDouble,
		// Token: 0x040001CD RID: 461
		MaxDecimal,
		// Token: 0x040001CE RID: 462
		MaxNullableDecimal,
		// Token: 0x040001CF RID: 463
		MaxSingle,
		// Token: 0x040001D0 RID: 464
		MaxNullableSingle,
		// Token: 0x040001D1 RID: 465
		MaxIntSelector,
		// Token: 0x040001D2 RID: 466
		MaxNullableIntSelector,
		// Token: 0x040001D3 RID: 467
		MaxLongSelector,
		// Token: 0x040001D4 RID: 468
		MaxNullableLongSelector,
		// Token: 0x040001D5 RID: 469
		MaxDoubleSelector,
		// Token: 0x040001D6 RID: 470
		MaxNullableDoubleSelector,
		// Token: 0x040001D7 RID: 471
		MaxDecimalSelector,
		// Token: 0x040001D8 RID: 472
		MaxNullableDecimalSelector,
		// Token: 0x040001D9 RID: 473
		MaxSingleSelector,
		// Token: 0x040001DA RID: 474
		MaxNullableSingleSelector,
		// Token: 0x040001DB RID: 475
		SumInt,
		// Token: 0x040001DC RID: 476
		SumNullableInt,
		// Token: 0x040001DD RID: 477
		SumLong,
		// Token: 0x040001DE RID: 478
		SumNullableLong,
		// Token: 0x040001DF RID: 479
		SumDouble,
		// Token: 0x040001E0 RID: 480
		SumNullableDouble,
		// Token: 0x040001E1 RID: 481
		SumDecimal,
		// Token: 0x040001E2 RID: 482
		SumNullableDecimal,
		// Token: 0x040001E3 RID: 483
		SumSingle,
		// Token: 0x040001E4 RID: 484
		SumNullableSingle,
		// Token: 0x040001E5 RID: 485
		SumIntSelector,
		// Token: 0x040001E6 RID: 486
		SumNullableIntSelector,
		// Token: 0x040001E7 RID: 487
		SumLongSelector,
		// Token: 0x040001E8 RID: 488
		SumNullableLongSelector,
		// Token: 0x040001E9 RID: 489
		SumDoubleSelector,
		// Token: 0x040001EA RID: 490
		SumNullableDoubleSelector,
		// Token: 0x040001EB RID: 491
		SumDecimalSelector,
		// Token: 0x040001EC RID: 492
		SumNullableDecimalSelector,
		// Token: 0x040001ED RID: 493
		SumSingleSelector,
		// Token: 0x040001EE RID: 494
		SumNullableSingleSelector,
		// Token: 0x040001EF RID: 495
		AverageInt,
		// Token: 0x040001F0 RID: 496
		AverageNullableInt,
		// Token: 0x040001F1 RID: 497
		AverageLong,
		// Token: 0x040001F2 RID: 498
		AverageNullableLong,
		// Token: 0x040001F3 RID: 499
		AverageDouble,
		// Token: 0x040001F4 RID: 500
		AverageNullableDouble,
		// Token: 0x040001F5 RID: 501
		AverageDecimal,
		// Token: 0x040001F6 RID: 502
		AverageNullableDecimal,
		// Token: 0x040001F7 RID: 503
		AverageSingle,
		// Token: 0x040001F8 RID: 504
		AverageNullableSingle,
		// Token: 0x040001F9 RID: 505
		AverageIntSelector,
		// Token: 0x040001FA RID: 506
		AverageNullableIntSelector,
		// Token: 0x040001FB RID: 507
		AverageLongSelector,
		// Token: 0x040001FC RID: 508
		AverageNullableLongSelector,
		// Token: 0x040001FD RID: 509
		AverageDoubleSelector,
		// Token: 0x040001FE RID: 510
		AverageNullableDoubleSelector,
		// Token: 0x040001FF RID: 511
		AverageDecimalSelector,
		// Token: 0x04000200 RID: 512
		AverageNullableDecimalSelector,
		// Token: 0x04000201 RID: 513
		AverageSingleSelector,
		// Token: 0x04000202 RID: 514
		AverageNullableSingleSelector,
		// Token: 0x04000203 RID: 515
		Aggregate,
		// Token: 0x04000204 RID: 516
		AggregateSeed,
		// Token: 0x04000205 RID: 517
		AggregateSeedSelector,
		// Token: 0x04000206 RID: 518
		AsQueryable,
		// Token: 0x04000207 RID: 519
		AsQueryableGeneric,
		// Token: 0x04000208 RID: 520
		AsEnumerable,
		// Token: 0x04000209 RID: 521
		ToList,
		// Token: 0x0400020A RID: 522
		NotSupported
	}
}
