using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02003070 RID: 12400
	[GeneratedCode("DomGen", "2.0")]
	internal enum BorderValues
	{
		// Token: 0x0400B146 RID: 45382
		[EnumString("nil")]
		Nil,
		// Token: 0x0400B147 RID: 45383
		[EnumString("none")]
		None,
		// Token: 0x0400B148 RID: 45384
		[EnumString("single")]
		Single,
		// Token: 0x0400B149 RID: 45385
		[EnumString("thick")]
		Thick,
		// Token: 0x0400B14A RID: 45386
		[EnumString("double")]
		Double,
		// Token: 0x0400B14B RID: 45387
		[EnumString("dotted")]
		Dotted,
		// Token: 0x0400B14C RID: 45388
		[EnumString("dashed")]
		Dashed,
		// Token: 0x0400B14D RID: 45389
		[EnumString("dotDash")]
		DotDash,
		// Token: 0x0400B14E RID: 45390
		[EnumString("dotDotDash")]
		DotDotDash,
		// Token: 0x0400B14F RID: 45391
		[EnumString("triple")]
		Triple,
		// Token: 0x0400B150 RID: 45392
		[EnumString("thinThickSmallGap")]
		ThinThickSmallGap,
		// Token: 0x0400B151 RID: 45393
		[EnumString("thickThinSmallGap")]
		ThickThinSmallGap,
		// Token: 0x0400B152 RID: 45394
		[EnumString("thinThickThinSmallGap")]
		ThinThickThinSmallGap,
		// Token: 0x0400B153 RID: 45395
		[EnumString("thinThickMediumGap")]
		ThinThickMediumGap,
		// Token: 0x0400B154 RID: 45396
		[EnumString("thickThinMediumGap")]
		ThickThinMediumGap,
		// Token: 0x0400B155 RID: 45397
		[EnumString("thinThickThinMediumGap")]
		ThinThickThinMediumGap,
		// Token: 0x0400B156 RID: 45398
		[EnumString("thinThickLargeGap")]
		ThinThickLargeGap,
		// Token: 0x0400B157 RID: 45399
		[EnumString("thickThinLargeGap")]
		ThickThinLargeGap,
		// Token: 0x0400B158 RID: 45400
		[EnumString("thinThickThinLargeGap")]
		ThinThickThinLargeGap,
		// Token: 0x0400B159 RID: 45401
		[EnumString("wave")]
		Wave,
		// Token: 0x0400B15A RID: 45402
		[EnumString("doubleWave")]
		DoubleWave,
		// Token: 0x0400B15B RID: 45403
		[EnumString("dashSmallGap")]
		DashSmallGap,
		// Token: 0x0400B15C RID: 45404
		[EnumString("dashDotStroked")]
		DashDotStroked,
		// Token: 0x0400B15D RID: 45405
		[EnumString("threeDEmboss")]
		ThreeDEmboss,
		// Token: 0x0400B15E RID: 45406
		[EnumString("threeDEngrave")]
		ThreeDEngrave,
		// Token: 0x0400B15F RID: 45407
		[EnumString("outset")]
		Outset,
		// Token: 0x0400B160 RID: 45408
		[EnumString("inset")]
		Inset,
		// Token: 0x0400B161 RID: 45409
		[EnumString("apples")]
		Apples,
		// Token: 0x0400B162 RID: 45410
		[EnumString("archedScallops")]
		ArchedScallops,
		// Token: 0x0400B163 RID: 45411
		[EnumString("babyPacifier")]
		BabyPacifier,
		// Token: 0x0400B164 RID: 45412
		[EnumString("babyRattle")]
		BabyRattle,
		// Token: 0x0400B165 RID: 45413
		[EnumString("balloons3Colors")]
		Balloons3Colors,
		// Token: 0x0400B166 RID: 45414
		[EnumString("balloonsHotAir")]
		BalloonsHotAir,
		// Token: 0x0400B167 RID: 45415
		[EnumString("basicBlackDashes")]
		BasicBlackDashes,
		// Token: 0x0400B168 RID: 45416
		[EnumString("basicBlackDots")]
		BasicBlackDots,
		// Token: 0x0400B169 RID: 45417
		[EnumString("basicBlackSquares")]
		BasicBlackSquares,
		// Token: 0x0400B16A RID: 45418
		[EnumString("basicThinLines")]
		BasicThinLines,
		// Token: 0x0400B16B RID: 45419
		[EnumString("basicWhiteDashes")]
		BasicWhiteDashes,
		// Token: 0x0400B16C RID: 45420
		[EnumString("basicWhiteDots")]
		BasicWhiteDots,
		// Token: 0x0400B16D RID: 45421
		[EnumString("basicWhiteSquares")]
		BasicWhiteSquares,
		// Token: 0x0400B16E RID: 45422
		[EnumString("basicWideInline")]
		BasicWideInline,
		// Token: 0x0400B16F RID: 45423
		[EnumString("basicWideMidline")]
		BasicWideMidline,
		// Token: 0x0400B170 RID: 45424
		[EnumString("basicWideOutline")]
		BasicWideOutline,
		// Token: 0x0400B171 RID: 45425
		[EnumString("bats")]
		Bats,
		// Token: 0x0400B172 RID: 45426
		[EnumString("birds")]
		Birds,
		// Token: 0x0400B173 RID: 45427
		[EnumString("birdsFlight")]
		BirdsFlight,
		// Token: 0x0400B174 RID: 45428
		[EnumString("cabins")]
		Cabins,
		// Token: 0x0400B175 RID: 45429
		[EnumString("cakeSlice")]
		CakeSlice,
		// Token: 0x0400B176 RID: 45430
		[EnumString("candyCorn")]
		CandyCorn,
		// Token: 0x0400B177 RID: 45431
		[EnumString("celticKnotwork")]
		CelticKnotwork,
		// Token: 0x0400B178 RID: 45432
		[EnumString("certificateBanner")]
		CertificateBanner,
		// Token: 0x0400B179 RID: 45433
		[EnumString("chainLink")]
		ChainLink,
		// Token: 0x0400B17A RID: 45434
		[EnumString("champagneBottle")]
		ChampagneBottle,
		// Token: 0x0400B17B RID: 45435
		[EnumString("checkedBarBlack")]
		CheckedBarBlack,
		// Token: 0x0400B17C RID: 45436
		[EnumString("checkedBarColor")]
		CheckedBarColor,
		// Token: 0x0400B17D RID: 45437
		[EnumString("checkered")]
		Checkered,
		// Token: 0x0400B17E RID: 45438
		[EnumString("christmasTree")]
		ChristmasTree,
		// Token: 0x0400B17F RID: 45439
		[EnumString("circlesLines")]
		CirclesLines,
		// Token: 0x0400B180 RID: 45440
		[EnumString("circlesRectangles")]
		CirclesRectangles,
		// Token: 0x0400B181 RID: 45441
		[EnumString("classicalWave")]
		ClassicalWave,
		// Token: 0x0400B182 RID: 45442
		[EnumString("clocks")]
		Clocks,
		// Token: 0x0400B183 RID: 45443
		[EnumString("compass")]
		Compass,
		// Token: 0x0400B184 RID: 45444
		[EnumString("confetti")]
		Confetti,
		// Token: 0x0400B185 RID: 45445
		[EnumString("confettiGrays")]
		ConfettiGrays,
		// Token: 0x0400B186 RID: 45446
		[EnumString("confettiOutline")]
		ConfettiOutline,
		// Token: 0x0400B187 RID: 45447
		[EnumString("confettiStreamers")]
		ConfettiStreamers,
		// Token: 0x0400B188 RID: 45448
		[EnumString("confettiWhite")]
		ConfettiWhite,
		// Token: 0x0400B189 RID: 45449
		[EnumString("cornerTriangles")]
		CornerTriangles,
		// Token: 0x0400B18A RID: 45450
		[EnumString("couponCutoutDashes")]
		CouponCutoutDashes,
		// Token: 0x0400B18B RID: 45451
		[EnumString("couponCutoutDots")]
		CouponCutoutDots,
		// Token: 0x0400B18C RID: 45452
		[EnumString("crazyMaze")]
		CrazyMaze,
		// Token: 0x0400B18D RID: 45453
		[EnumString("creaturesButterfly")]
		CreaturesButterfly,
		// Token: 0x0400B18E RID: 45454
		[EnumString("creaturesFish")]
		CreaturesFish,
		// Token: 0x0400B18F RID: 45455
		[EnumString("creaturesInsects")]
		CreaturesInsects,
		// Token: 0x0400B190 RID: 45456
		[EnumString("creaturesLadyBug")]
		CreaturesLadyBug,
		// Token: 0x0400B191 RID: 45457
		[EnumString("crossStitch")]
		CrossStitch,
		// Token: 0x0400B192 RID: 45458
		[EnumString("cup")]
		Cup,
		// Token: 0x0400B193 RID: 45459
		[EnumString("decoArch")]
		DecoArch,
		// Token: 0x0400B194 RID: 45460
		[EnumString("decoArchColor")]
		DecoArchColor,
		// Token: 0x0400B195 RID: 45461
		[EnumString("decoBlocks")]
		DecoBlocks,
		// Token: 0x0400B196 RID: 45462
		[EnumString("diamondsGray")]
		DiamondsGray,
		// Token: 0x0400B197 RID: 45463
		[EnumString("doubleD")]
		DoubleD,
		// Token: 0x0400B198 RID: 45464
		[EnumString("doubleDiamonds")]
		DoubleDiamonds,
		// Token: 0x0400B199 RID: 45465
		[EnumString("earth1")]
		Earth1,
		// Token: 0x0400B19A RID: 45466
		[EnumString("earth2")]
		Earth2,
		// Token: 0x0400B19B RID: 45467
		[EnumString("eclipsingSquares1")]
		EclipsingSquares1,
		// Token: 0x0400B19C RID: 45468
		[EnumString("eclipsingSquares2")]
		EclipsingSquares2,
		// Token: 0x0400B19D RID: 45469
		[EnumString("eggsBlack")]
		EggsBlack,
		// Token: 0x0400B19E RID: 45470
		[EnumString("fans")]
		Fans,
		// Token: 0x0400B19F RID: 45471
		[EnumString("film")]
		Film,
		// Token: 0x0400B1A0 RID: 45472
		[EnumString("firecrackers")]
		Firecrackers,
		// Token: 0x0400B1A1 RID: 45473
		[EnumString("flowersBlockPrint")]
		FlowersBlockPrint,
		// Token: 0x0400B1A2 RID: 45474
		[EnumString("flowersDaisies")]
		FlowersDaisies,
		// Token: 0x0400B1A3 RID: 45475
		[EnumString("flowersModern1")]
		FlowersModern1,
		// Token: 0x0400B1A4 RID: 45476
		[EnumString("flowersModern2")]
		FlowersModern2,
		// Token: 0x0400B1A5 RID: 45477
		[EnumString("flowersPansy")]
		FlowersPansy,
		// Token: 0x0400B1A6 RID: 45478
		[EnumString("flowersRedRose")]
		FlowersRedRose,
		// Token: 0x0400B1A7 RID: 45479
		[EnumString("flowersRoses")]
		FlowersRoses,
		// Token: 0x0400B1A8 RID: 45480
		[EnumString("flowersTeacup")]
		FlowersTeacup,
		// Token: 0x0400B1A9 RID: 45481
		[EnumString("flowersTiny")]
		FlowersTiny,
		// Token: 0x0400B1AA RID: 45482
		[EnumString("gems")]
		Gems,
		// Token: 0x0400B1AB RID: 45483
		[EnumString("gingerbreadMan")]
		GingerbreadMan,
		// Token: 0x0400B1AC RID: 45484
		[EnumString("gradient")]
		Gradient,
		// Token: 0x0400B1AD RID: 45485
		[EnumString("handmade1")]
		Handmade1,
		// Token: 0x0400B1AE RID: 45486
		[EnumString("handmade2")]
		Handmade2,
		// Token: 0x0400B1AF RID: 45487
		[EnumString("heartBalloon")]
		HeartBalloon,
		// Token: 0x0400B1B0 RID: 45488
		[EnumString("heartGray")]
		HeartGray,
		// Token: 0x0400B1B1 RID: 45489
		[EnumString("hearts")]
		Hearts,
		// Token: 0x0400B1B2 RID: 45490
		[EnumString("heebieJeebies")]
		HeebieJeebies,
		// Token: 0x0400B1B3 RID: 45491
		[EnumString("holly")]
		Holly,
		// Token: 0x0400B1B4 RID: 45492
		[EnumString("houseFunky")]
		HouseFunky,
		// Token: 0x0400B1B5 RID: 45493
		[EnumString("hypnotic")]
		Hypnotic,
		// Token: 0x0400B1B6 RID: 45494
		[EnumString("iceCreamCones")]
		IceCreamCones,
		// Token: 0x0400B1B7 RID: 45495
		[EnumString("lightBulb")]
		LightBulb,
		// Token: 0x0400B1B8 RID: 45496
		[EnumString("lightning1")]
		Lightning1,
		// Token: 0x0400B1B9 RID: 45497
		[EnumString("lightning2")]
		Lightning2,
		// Token: 0x0400B1BA RID: 45498
		[EnumString("mapPins")]
		MapPins,
		// Token: 0x0400B1BB RID: 45499
		[EnumString("mapleLeaf")]
		MapleLeaf,
		// Token: 0x0400B1BC RID: 45500
		[EnumString("mapleMuffins")]
		MapleMuffins,
		// Token: 0x0400B1BD RID: 45501
		[EnumString("marquee")]
		Marquee,
		// Token: 0x0400B1BE RID: 45502
		[EnumString("marqueeToothed")]
		MarqueeToothed,
		// Token: 0x0400B1BF RID: 45503
		[EnumString("moons")]
		Moons,
		// Token: 0x0400B1C0 RID: 45504
		[EnumString("mosaic")]
		Mosaic,
		// Token: 0x0400B1C1 RID: 45505
		[EnumString("musicNotes")]
		MusicNotes,
		// Token: 0x0400B1C2 RID: 45506
		[EnumString("northwest")]
		Northwest,
		// Token: 0x0400B1C3 RID: 45507
		[EnumString("ovals")]
		Ovals,
		// Token: 0x0400B1C4 RID: 45508
		[EnumString("packages")]
		Packages,
		// Token: 0x0400B1C5 RID: 45509
		[EnumString("palmsBlack")]
		PalmsBlack,
		// Token: 0x0400B1C6 RID: 45510
		[EnumString("palmsColor")]
		PalmsColor,
		// Token: 0x0400B1C7 RID: 45511
		[EnumString("paperClips")]
		PaperClips,
		// Token: 0x0400B1C8 RID: 45512
		[EnumString("papyrus")]
		Papyrus,
		// Token: 0x0400B1C9 RID: 45513
		[EnumString("partyFavor")]
		PartyFavor,
		// Token: 0x0400B1CA RID: 45514
		[EnumString("partyGlass")]
		PartyGlass,
		// Token: 0x0400B1CB RID: 45515
		[EnumString("pencils")]
		Pencils,
		// Token: 0x0400B1CC RID: 45516
		[EnumString("people")]
		People,
		// Token: 0x0400B1CD RID: 45517
		[EnumString("peopleWaving")]
		PeopleWaving,
		// Token: 0x0400B1CE RID: 45518
		[EnumString("peopleHats")]
		PeopleHats,
		// Token: 0x0400B1CF RID: 45519
		[EnumString("poinsettias")]
		Poinsettias,
		// Token: 0x0400B1D0 RID: 45520
		[EnumString("postageStamp")]
		PostageStamp,
		// Token: 0x0400B1D1 RID: 45521
		[EnumString("pumpkin1")]
		Pumpkin1,
		// Token: 0x0400B1D2 RID: 45522
		[EnumString("pushPinNote2")]
		PushPinNote2,
		// Token: 0x0400B1D3 RID: 45523
		[EnumString("pushPinNote1")]
		PushPinNote1,
		// Token: 0x0400B1D4 RID: 45524
		[EnumString("pyramids")]
		Pyramids,
		// Token: 0x0400B1D5 RID: 45525
		[EnumString("pyramidsAbove")]
		PyramidsAbove,
		// Token: 0x0400B1D6 RID: 45526
		[EnumString("quadrants")]
		Quadrants,
		// Token: 0x0400B1D7 RID: 45527
		[EnumString("rings")]
		Rings,
		// Token: 0x0400B1D8 RID: 45528
		[EnumString("safari")]
		Safari,
		// Token: 0x0400B1D9 RID: 45529
		[EnumString("sawtooth")]
		Sawtooth,
		// Token: 0x0400B1DA RID: 45530
		[EnumString("sawtoothGray")]
		SawtoothGray,
		// Token: 0x0400B1DB RID: 45531
		[EnumString("scaredCat")]
		ScaredCat,
		// Token: 0x0400B1DC RID: 45532
		[EnumString("seattle")]
		Seattle,
		// Token: 0x0400B1DD RID: 45533
		[EnumString("shadowedSquares")]
		ShadowedSquares,
		// Token: 0x0400B1DE RID: 45534
		[EnumString("sharksTeeth")]
		SharksTeeth,
		// Token: 0x0400B1DF RID: 45535
		[EnumString("shorebirdTracks")]
		ShorebirdTracks,
		// Token: 0x0400B1E0 RID: 45536
		[EnumString("skyrocket")]
		Skyrocket,
		// Token: 0x0400B1E1 RID: 45537
		[EnumString("snowflakeFancy")]
		SnowflakeFancy,
		// Token: 0x0400B1E2 RID: 45538
		[EnumString("snowflakes")]
		Snowflakes,
		// Token: 0x0400B1E3 RID: 45539
		[EnumString("sombrero")]
		Sombrero,
		// Token: 0x0400B1E4 RID: 45540
		[EnumString("southwest")]
		Southwest,
		// Token: 0x0400B1E5 RID: 45541
		[EnumString("stars")]
		Stars,
		// Token: 0x0400B1E6 RID: 45542
		[EnumString("starsTop")]
		StarsTop,
		// Token: 0x0400B1E7 RID: 45543
		[EnumString("stars3d")]
		Stars3d,
		// Token: 0x0400B1E8 RID: 45544
		[EnumString("starsBlack")]
		StarsBlack,
		// Token: 0x0400B1E9 RID: 45545
		[EnumString("starsShadowed")]
		StarsShadowed,
		// Token: 0x0400B1EA RID: 45546
		[EnumString("sun")]
		Sun,
		// Token: 0x0400B1EB RID: 45547
		[EnumString("swirligig")]
		Swirligig,
		// Token: 0x0400B1EC RID: 45548
		[EnumString("tornPaper")]
		TornPaper,
		// Token: 0x0400B1ED RID: 45549
		[EnumString("tornPaperBlack")]
		TornPaperBlack,
		// Token: 0x0400B1EE RID: 45550
		[EnumString("trees")]
		Trees,
		// Token: 0x0400B1EF RID: 45551
		[EnumString("triangleParty")]
		TriangleParty,
		// Token: 0x0400B1F0 RID: 45552
		[EnumString("triangles")]
		Triangles,
		// Token: 0x0400B1F1 RID: 45553
		[EnumString("tribal1")]
		Tribal1,
		// Token: 0x0400B1F2 RID: 45554
		[EnumString("tribal2")]
		Tribal2,
		// Token: 0x0400B1F3 RID: 45555
		[EnumString("tribal3")]
		Tribal3,
		// Token: 0x0400B1F4 RID: 45556
		[EnumString("tribal4")]
		Tribal4,
		// Token: 0x0400B1F5 RID: 45557
		[EnumString("tribal5")]
		Tribal5,
		// Token: 0x0400B1F6 RID: 45558
		[EnumString("tribal6")]
		Tribal6,
		// Token: 0x0400B1F7 RID: 45559
		[EnumString("triangle1")]
		Triangle1,
		// Token: 0x0400B1F8 RID: 45560
		[EnumString("triangle2")]
		Triangle2,
		// Token: 0x0400B1F9 RID: 45561
		[EnumString("triangleCircle1")]
		TriangleCircle1,
		// Token: 0x0400B1FA RID: 45562
		[EnumString("triangleCircle2")]
		TriangleCircle2,
		// Token: 0x0400B1FB RID: 45563
		[EnumString("shapes1")]
		Shapes1,
		// Token: 0x0400B1FC RID: 45564
		[EnumString("shapes2")]
		Shapes2,
		// Token: 0x0400B1FD RID: 45565
		[EnumString("twistedLines1")]
		TwistedLines1,
		// Token: 0x0400B1FE RID: 45566
		[EnumString("twistedLines2")]
		TwistedLines2,
		// Token: 0x0400B1FF RID: 45567
		[EnumString("vine")]
		Vine,
		// Token: 0x0400B200 RID: 45568
		[EnumString("waveline")]
		Waveline,
		// Token: 0x0400B201 RID: 45569
		[EnumString("weavingAngles")]
		WeavingAngles,
		// Token: 0x0400B202 RID: 45570
		[EnumString("weavingBraid")]
		WeavingBraid,
		// Token: 0x0400B203 RID: 45571
		[EnumString("weavingRibbon")]
		WeavingRibbon,
		// Token: 0x0400B204 RID: 45572
		[EnumString("weavingStrips")]
		WeavingStrips,
		// Token: 0x0400B205 RID: 45573
		[EnumString("whiteFlowers")]
		WhiteFlowers,
		// Token: 0x0400B206 RID: 45574
		[EnumString("woodwork")]
		Woodwork,
		// Token: 0x0400B207 RID: 45575
		[EnumString("xIllusions")]
		XIllusions,
		// Token: 0x0400B208 RID: 45576
		[EnumString("zanyTriangles")]
		ZanyTriangles,
		// Token: 0x0400B209 RID: 45577
		[EnumString("zigZag")]
		ZigZag,
		// Token: 0x0400B20A RID: 45578
		[EnumString("zigZagStitch")]
		ZigZagStitch
	}
}
