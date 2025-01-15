using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000E5 RID: 229
	internal enum TSqlTokenType
	{
		// Token: 0x0400094C RID: 2380
		EndOfFile = 1,
		// Token: 0x0400094D RID: 2381
		None = 0,
		// Token: 0x0400094E RID: 2382
		Add = 4,
		// Token: 0x0400094F RID: 2383
		All,
		// Token: 0x04000950 RID: 2384
		Alter,
		// Token: 0x04000951 RID: 2385
		And,
		// Token: 0x04000952 RID: 2386
		Any,
		// Token: 0x04000953 RID: 2387
		As,
		// Token: 0x04000954 RID: 2388
		Asc,
		// Token: 0x04000955 RID: 2389
		Authorization,
		// Token: 0x04000956 RID: 2390
		Backup,
		// Token: 0x04000957 RID: 2391
		Begin,
		// Token: 0x04000958 RID: 2392
		Between,
		// Token: 0x04000959 RID: 2393
		Break,
		// Token: 0x0400095A RID: 2394
		Browse,
		// Token: 0x0400095B RID: 2395
		Bulk,
		// Token: 0x0400095C RID: 2396
		By,
		// Token: 0x0400095D RID: 2397
		Cascade,
		// Token: 0x0400095E RID: 2398
		Case,
		// Token: 0x0400095F RID: 2399
		Check,
		// Token: 0x04000960 RID: 2400
		Checkpoint,
		// Token: 0x04000961 RID: 2401
		Close,
		// Token: 0x04000962 RID: 2402
		Clustered,
		// Token: 0x04000963 RID: 2403
		Coalesce,
		// Token: 0x04000964 RID: 2404
		Collate,
		// Token: 0x04000965 RID: 2405
		Column,
		// Token: 0x04000966 RID: 2406
		Commit,
		// Token: 0x04000967 RID: 2407
		Compute,
		// Token: 0x04000968 RID: 2408
		Constraint,
		// Token: 0x04000969 RID: 2409
		Contains,
		// Token: 0x0400096A RID: 2410
		ContainsTable,
		// Token: 0x0400096B RID: 2411
		Continue,
		// Token: 0x0400096C RID: 2412
		Convert,
		// Token: 0x0400096D RID: 2413
		Create,
		// Token: 0x0400096E RID: 2414
		Cross,
		// Token: 0x0400096F RID: 2415
		Current,
		// Token: 0x04000970 RID: 2416
		CurrentDate,
		// Token: 0x04000971 RID: 2417
		CurrentTime,
		// Token: 0x04000972 RID: 2418
		CurrentTimestamp,
		// Token: 0x04000973 RID: 2419
		CurrentUser,
		// Token: 0x04000974 RID: 2420
		Cursor,
		// Token: 0x04000975 RID: 2421
		Database,
		// Token: 0x04000976 RID: 2422
		Dbcc,
		// Token: 0x04000977 RID: 2423
		Deallocate,
		// Token: 0x04000978 RID: 2424
		Declare,
		// Token: 0x04000979 RID: 2425
		Default,
		// Token: 0x0400097A RID: 2426
		Delete,
		// Token: 0x0400097B RID: 2427
		Deny,
		// Token: 0x0400097C RID: 2428
		Desc,
		// Token: 0x0400097D RID: 2429
		Distinct,
		// Token: 0x0400097E RID: 2430
		Distributed,
		// Token: 0x0400097F RID: 2431
		Double,
		// Token: 0x04000980 RID: 2432
		Drop,
		// Token: 0x04000981 RID: 2433
		Else,
		// Token: 0x04000982 RID: 2434
		End,
		// Token: 0x04000983 RID: 2435
		Errlvl,
		// Token: 0x04000984 RID: 2436
		Escape,
		// Token: 0x04000985 RID: 2437
		Except,
		// Token: 0x04000986 RID: 2438
		Exec,
		// Token: 0x04000987 RID: 2439
		Execute,
		// Token: 0x04000988 RID: 2440
		Exists,
		// Token: 0x04000989 RID: 2441
		Exit,
		// Token: 0x0400098A RID: 2442
		Fetch,
		// Token: 0x0400098B RID: 2443
		File,
		// Token: 0x0400098C RID: 2444
		FillFactor,
		// Token: 0x0400098D RID: 2445
		For,
		// Token: 0x0400098E RID: 2446
		Foreign,
		// Token: 0x0400098F RID: 2447
		FreeText,
		// Token: 0x04000990 RID: 2448
		FreeTextTable,
		// Token: 0x04000991 RID: 2449
		From,
		// Token: 0x04000992 RID: 2450
		Full,
		// Token: 0x04000993 RID: 2451
		Function,
		// Token: 0x04000994 RID: 2452
		GoTo,
		// Token: 0x04000995 RID: 2453
		Grant,
		// Token: 0x04000996 RID: 2454
		Group,
		// Token: 0x04000997 RID: 2455
		Having,
		// Token: 0x04000998 RID: 2456
		HoldLock,
		// Token: 0x04000999 RID: 2457
		Identity,
		// Token: 0x0400099A RID: 2458
		IdentityInsert,
		// Token: 0x0400099B RID: 2459
		IdentityColumn,
		// Token: 0x0400099C RID: 2460
		If,
		// Token: 0x0400099D RID: 2461
		In,
		// Token: 0x0400099E RID: 2462
		Index,
		// Token: 0x0400099F RID: 2463
		Inner,
		// Token: 0x040009A0 RID: 2464
		Insert,
		// Token: 0x040009A1 RID: 2465
		Intersect,
		// Token: 0x040009A2 RID: 2466
		Into,
		// Token: 0x040009A3 RID: 2467
		Is,
		// Token: 0x040009A4 RID: 2468
		Join,
		// Token: 0x040009A5 RID: 2469
		Key,
		// Token: 0x040009A6 RID: 2470
		Kill,
		// Token: 0x040009A7 RID: 2471
		Left,
		// Token: 0x040009A8 RID: 2472
		Like,
		// Token: 0x040009A9 RID: 2473
		LineNo,
		// Token: 0x040009AA RID: 2474
		National,
		// Token: 0x040009AB RID: 2475
		NoCheck,
		// Token: 0x040009AC RID: 2476
		NonClustered,
		// Token: 0x040009AD RID: 2477
		Not,
		// Token: 0x040009AE RID: 2478
		Null,
		// Token: 0x040009AF RID: 2479
		NullIf,
		// Token: 0x040009B0 RID: 2480
		Of,
		// Token: 0x040009B1 RID: 2481
		Off,
		// Token: 0x040009B2 RID: 2482
		Offsets,
		// Token: 0x040009B3 RID: 2483
		On,
		// Token: 0x040009B4 RID: 2484
		Open,
		// Token: 0x040009B5 RID: 2485
		OpenDataSource,
		// Token: 0x040009B6 RID: 2486
		OpenQuery,
		// Token: 0x040009B7 RID: 2487
		OpenRowSet,
		// Token: 0x040009B8 RID: 2488
		OpenXml,
		// Token: 0x040009B9 RID: 2489
		Option,
		// Token: 0x040009BA RID: 2490
		Or,
		// Token: 0x040009BB RID: 2491
		Order,
		// Token: 0x040009BC RID: 2492
		Outer,
		// Token: 0x040009BD RID: 2493
		Over,
		// Token: 0x040009BE RID: 2494
		Percent,
		// Token: 0x040009BF RID: 2495
		Plan,
		// Token: 0x040009C0 RID: 2496
		Primary,
		// Token: 0x040009C1 RID: 2497
		Print,
		// Token: 0x040009C2 RID: 2498
		Proc,
		// Token: 0x040009C3 RID: 2499
		Procedure,
		// Token: 0x040009C4 RID: 2500
		Public,
		// Token: 0x040009C5 RID: 2501
		Raiserror,
		// Token: 0x040009C6 RID: 2502
		Read,
		// Token: 0x040009C7 RID: 2503
		ReadText,
		// Token: 0x040009C8 RID: 2504
		Reconfigure,
		// Token: 0x040009C9 RID: 2505
		References,
		// Token: 0x040009CA RID: 2506
		Replication,
		// Token: 0x040009CB RID: 2507
		Restore,
		// Token: 0x040009CC RID: 2508
		Restrict,
		// Token: 0x040009CD RID: 2509
		Return,
		// Token: 0x040009CE RID: 2510
		Revoke,
		// Token: 0x040009CF RID: 2511
		Right,
		// Token: 0x040009D0 RID: 2512
		Rollback,
		// Token: 0x040009D1 RID: 2513
		RowCount,
		// Token: 0x040009D2 RID: 2514
		RowGuidColumn,
		// Token: 0x040009D3 RID: 2515
		Rule,
		// Token: 0x040009D4 RID: 2516
		Save,
		// Token: 0x040009D5 RID: 2517
		Schema,
		// Token: 0x040009D6 RID: 2518
		Select,
		// Token: 0x040009D7 RID: 2519
		SessionUser,
		// Token: 0x040009D8 RID: 2520
		Set,
		// Token: 0x040009D9 RID: 2521
		SetUser,
		// Token: 0x040009DA RID: 2522
		Shutdown,
		// Token: 0x040009DB RID: 2523
		Some,
		// Token: 0x040009DC RID: 2524
		Statistics,
		// Token: 0x040009DD RID: 2525
		SystemUser,
		// Token: 0x040009DE RID: 2526
		Table,
		// Token: 0x040009DF RID: 2527
		TextSize,
		// Token: 0x040009E0 RID: 2528
		Then,
		// Token: 0x040009E1 RID: 2529
		To,
		// Token: 0x040009E2 RID: 2530
		Top,
		// Token: 0x040009E3 RID: 2531
		Tran,
		// Token: 0x040009E4 RID: 2532
		Transaction,
		// Token: 0x040009E5 RID: 2533
		Trigger,
		// Token: 0x040009E6 RID: 2534
		Truncate,
		// Token: 0x040009E7 RID: 2535
		TSEqual,
		// Token: 0x040009E8 RID: 2536
		Union,
		// Token: 0x040009E9 RID: 2537
		Unique,
		// Token: 0x040009EA RID: 2538
		Update,
		// Token: 0x040009EB RID: 2539
		UpdateText,
		// Token: 0x040009EC RID: 2540
		Use,
		// Token: 0x040009ED RID: 2541
		User,
		// Token: 0x040009EE RID: 2542
		Values,
		// Token: 0x040009EF RID: 2543
		Varying,
		// Token: 0x040009F0 RID: 2544
		View,
		// Token: 0x040009F1 RID: 2545
		WaitFor,
		// Token: 0x040009F2 RID: 2546
		When,
		// Token: 0x040009F3 RID: 2547
		Where,
		// Token: 0x040009F4 RID: 2548
		While,
		// Token: 0x040009F5 RID: 2549
		With,
		// Token: 0x040009F6 RID: 2550
		WriteText,
		// Token: 0x040009F7 RID: 2551
		Disk,
		// Token: 0x040009F8 RID: 2552
		Precision,
		// Token: 0x040009F9 RID: 2553
		External,
		// Token: 0x040009FA RID: 2554
		Revert,
		// Token: 0x040009FB RID: 2555
		Pivot,
		// Token: 0x040009FC RID: 2556
		Unpivot,
		// Token: 0x040009FD RID: 2557
		TableSample,
		// Token: 0x040009FE RID: 2558
		Dump,
		// Token: 0x040009FF RID: 2559
		Load,
		// Token: 0x04000A00 RID: 2560
		Merge,
		// Token: 0x04000A01 RID: 2561
		StopList,
		// Token: 0x04000A02 RID: 2562
		SemanticKeyPhraseTable,
		// Token: 0x04000A03 RID: 2563
		SemanticSimilarityTable,
		// Token: 0x04000A04 RID: 2564
		SemanticSimilarityDetailsTable,
		// Token: 0x04000A05 RID: 2565
		TryConvert,
		// Token: 0x04000A06 RID: 2566
		Bang,
		// Token: 0x04000A07 RID: 2567
		PercentSign,
		// Token: 0x04000A08 RID: 2568
		Ampersand,
		// Token: 0x04000A09 RID: 2569
		LeftParenthesis,
		// Token: 0x04000A0A RID: 2570
		RightParenthesis,
		// Token: 0x04000A0B RID: 2571
		LeftCurly,
		// Token: 0x04000A0C RID: 2572
		RightCurly,
		// Token: 0x04000A0D RID: 2573
		Star,
		// Token: 0x04000A0E RID: 2574
		MultiplyEquals,
		// Token: 0x04000A0F RID: 2575
		Plus,
		// Token: 0x04000A10 RID: 2576
		Comma,
		// Token: 0x04000A11 RID: 2577
		Minus,
		// Token: 0x04000A12 RID: 2578
		Dot,
		// Token: 0x04000A13 RID: 2579
		Divide,
		// Token: 0x04000A14 RID: 2580
		Colon,
		// Token: 0x04000A15 RID: 2581
		DoubleColon,
		// Token: 0x04000A16 RID: 2582
		Semicolon,
		// Token: 0x04000A17 RID: 2583
		LessThan,
		// Token: 0x04000A18 RID: 2584
		EqualsSign,
		// Token: 0x04000A19 RID: 2585
		RightOuterJoin,
		// Token: 0x04000A1A RID: 2586
		GreaterThan,
		// Token: 0x04000A1B RID: 2587
		Circumflex,
		// Token: 0x04000A1C RID: 2588
		VerticalLine,
		// Token: 0x04000A1D RID: 2589
		Tilde,
		// Token: 0x04000A1E RID: 2590
		AddEquals,
		// Token: 0x04000A1F RID: 2591
		SubtractEquals,
		// Token: 0x04000A20 RID: 2592
		DivideEquals,
		// Token: 0x04000A21 RID: 2593
		ModEquals,
		// Token: 0x04000A22 RID: 2594
		BitwiseAndEquals,
		// Token: 0x04000A23 RID: 2595
		BitwiseOrEquals,
		// Token: 0x04000A24 RID: 2596
		BitwiseXorEquals,
		// Token: 0x04000A25 RID: 2597
		Go,
		// Token: 0x04000A26 RID: 2598
		Label,
		// Token: 0x04000A27 RID: 2599
		Integer,
		// Token: 0x04000A28 RID: 2600
		Numeric,
		// Token: 0x04000A29 RID: 2601
		Real,
		// Token: 0x04000A2A RID: 2602
		HexLiteral,
		// Token: 0x04000A2B RID: 2603
		Money,
		// Token: 0x04000A2C RID: 2604
		SqlCommandIdentifier,
		// Token: 0x04000A2D RID: 2605
		PseudoColumn,
		// Token: 0x04000A2E RID: 2606
		DollarPartition,
		// Token: 0x04000A2F RID: 2607
		AsciiStringOrQuotedIdentifier,
		// Token: 0x04000A30 RID: 2608
		AsciiStringLiteral,
		// Token: 0x04000A31 RID: 2609
		UnicodeStringLiteral,
		// Token: 0x04000A32 RID: 2610
		Identifier,
		// Token: 0x04000A33 RID: 2611
		QuotedIdentifier,
		// Token: 0x04000A34 RID: 2612
		Variable,
		// Token: 0x04000A35 RID: 2613
		OdbcInitiator,
		// Token: 0x04000A36 RID: 2614
		ProcNameSemicolon,
		// Token: 0x04000A37 RID: 2615
		SingleLineComment,
		// Token: 0x04000A38 RID: 2616
		MultilineComment,
		// Token: 0x04000A39 RID: 2617
		WhiteSpace
	}
}
