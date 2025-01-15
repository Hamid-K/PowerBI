using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02003069 RID: 12393
	[GeneratedCode("DomGen", "2.0")]
	internal enum NumberFormatValues
	{
		// Token: 0x0400B0D8 RID: 45272
		[EnumString("decimal")]
		Decimal,
		// Token: 0x0400B0D9 RID: 45273
		[EnumString("upperRoman")]
		UpperRoman,
		// Token: 0x0400B0DA RID: 45274
		[EnumString("lowerRoman")]
		LowerRoman,
		// Token: 0x0400B0DB RID: 45275
		[EnumString("upperLetter")]
		UpperLetter,
		// Token: 0x0400B0DC RID: 45276
		[EnumString("lowerLetter")]
		LowerLetter,
		// Token: 0x0400B0DD RID: 45277
		[EnumString("ordinal")]
		Ordinal,
		// Token: 0x0400B0DE RID: 45278
		[EnumString("cardinalText")]
		CardinalText,
		// Token: 0x0400B0DF RID: 45279
		[EnumString("ordinalText")]
		OrdinalText,
		// Token: 0x0400B0E0 RID: 45280
		[EnumString("hex")]
		Hex,
		// Token: 0x0400B0E1 RID: 45281
		[EnumString("chicago")]
		Chicago,
		// Token: 0x0400B0E2 RID: 45282
		[EnumString("ideographDigital")]
		IdeographDigital,
		// Token: 0x0400B0E3 RID: 45283
		[EnumString("japaneseCounting")]
		JapaneseCounting,
		// Token: 0x0400B0E4 RID: 45284
		[EnumString("aiueo")]
		Aiueo,
		// Token: 0x0400B0E5 RID: 45285
		[EnumString("iroha")]
		Iroha,
		// Token: 0x0400B0E6 RID: 45286
		[EnumString("decimalFullWidth")]
		DecimalFullWidth,
		// Token: 0x0400B0E7 RID: 45287
		[EnumString("decimalHalfWidth")]
		DecimalHalfWidth,
		// Token: 0x0400B0E8 RID: 45288
		[EnumString("japaneseLegal")]
		JapaneseLegal,
		// Token: 0x0400B0E9 RID: 45289
		[EnumString("japaneseDigitalTenThousand")]
		JapaneseDigitalTenThousand,
		// Token: 0x0400B0EA RID: 45290
		[EnumString("decimalEnclosedCircle")]
		DecimalEnclosedCircle,
		// Token: 0x0400B0EB RID: 45291
		[EnumString("decimalFullWidth2")]
		DecimalFullWidth2,
		// Token: 0x0400B0EC RID: 45292
		[EnumString("aiueoFullWidth")]
		AiueoFullWidth,
		// Token: 0x0400B0ED RID: 45293
		[EnumString("irohaFullWidth")]
		IrohaFullWidth,
		// Token: 0x0400B0EE RID: 45294
		[EnumString("decimalZero")]
		DecimalZero,
		// Token: 0x0400B0EF RID: 45295
		[EnumString("bullet")]
		Bullet,
		// Token: 0x0400B0F0 RID: 45296
		[EnumString("ganada")]
		Ganada,
		// Token: 0x0400B0F1 RID: 45297
		[EnumString("chosung")]
		Chosung,
		// Token: 0x0400B0F2 RID: 45298
		[EnumString("decimalEnclosedFullstop")]
		DecimalEnclosedFullstop,
		// Token: 0x0400B0F3 RID: 45299
		[EnumString("decimalEnclosedParen")]
		DecimalEnclosedParen,
		// Token: 0x0400B0F4 RID: 45300
		[EnumString("decimalEnclosedCircleChinese")]
		DecimalEnclosedCircleChinese,
		// Token: 0x0400B0F5 RID: 45301
		[EnumString("ideographEnclosedCircle")]
		IdeographEnclosedCircle,
		// Token: 0x0400B0F6 RID: 45302
		[EnumString("ideographTraditional")]
		IdeographTraditional,
		// Token: 0x0400B0F7 RID: 45303
		[EnumString("ideographZodiac")]
		IdeographZodiac,
		// Token: 0x0400B0F8 RID: 45304
		[EnumString("ideographZodiacTraditional")]
		IdeographZodiacTraditional,
		// Token: 0x0400B0F9 RID: 45305
		[EnumString("taiwaneseCounting")]
		TaiwaneseCounting,
		// Token: 0x0400B0FA RID: 45306
		[EnumString("ideographLegalTraditional")]
		IdeographLegalTraditional,
		// Token: 0x0400B0FB RID: 45307
		[EnumString("taiwaneseCountingThousand")]
		TaiwaneseCountingThousand,
		// Token: 0x0400B0FC RID: 45308
		[EnumString("taiwaneseDigital")]
		TaiwaneseDigital,
		// Token: 0x0400B0FD RID: 45309
		[EnumString("chineseCounting")]
		ChineseCounting,
		// Token: 0x0400B0FE RID: 45310
		[EnumString("chineseLegalSimplified")]
		ChineseLegalSimplified,
		// Token: 0x0400B0FF RID: 45311
		[EnumString("chineseCountingThousand")]
		ChineseCountingThousand,
		// Token: 0x0400B100 RID: 45312
		[EnumString("koreanDigital")]
		KoreanDigital,
		// Token: 0x0400B101 RID: 45313
		[EnumString("koreanCounting")]
		KoreanCounting,
		// Token: 0x0400B102 RID: 45314
		[EnumString("koreanLegal")]
		KoreanLegal,
		// Token: 0x0400B103 RID: 45315
		[EnumString("koreanDigital2")]
		KoreanDigital2,
		// Token: 0x0400B104 RID: 45316
		[EnumString("vietnameseCounting")]
		VietnameseCounting,
		// Token: 0x0400B105 RID: 45317
		[EnumString("russianLower")]
		RussianLower,
		// Token: 0x0400B106 RID: 45318
		[EnumString("russianUpper")]
		RussianUpper,
		// Token: 0x0400B107 RID: 45319
		[EnumString("none")]
		None,
		// Token: 0x0400B108 RID: 45320
		[EnumString("numberInDash")]
		NumberInDash,
		// Token: 0x0400B109 RID: 45321
		[EnumString("hebrew1")]
		Hebrew1,
		// Token: 0x0400B10A RID: 45322
		[EnumString("hebrew2")]
		Hebrew2,
		// Token: 0x0400B10B RID: 45323
		[EnumString("arabicAlpha")]
		ArabicAlpha,
		// Token: 0x0400B10C RID: 45324
		[EnumString("arabicAbjad")]
		ArabicAbjad,
		// Token: 0x0400B10D RID: 45325
		[EnumString("hindiVowels")]
		HindiVowels,
		// Token: 0x0400B10E RID: 45326
		[EnumString("hindiConsonants")]
		HindiConsonants,
		// Token: 0x0400B10F RID: 45327
		[EnumString("hindiNumbers")]
		HindiNumbers,
		// Token: 0x0400B110 RID: 45328
		[EnumString("hindiCounting")]
		HindiCounting,
		// Token: 0x0400B111 RID: 45329
		[EnumString("thaiLetters")]
		ThaiLetters,
		// Token: 0x0400B112 RID: 45330
		[EnumString("thaiNumbers")]
		ThaiNumbers,
		// Token: 0x0400B113 RID: 45331
		[EnumString("thaiCounting")]
		ThaiCounting,
		// Token: 0x0400B114 RID: 45332
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[EnumString("bahtText")]
		BahtText,
		// Token: 0x0400B115 RID: 45333
		[EnumString("dollarText")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		DollarText,
		// Token: 0x0400B116 RID: 45334
		[EnumString("custom")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		Custom
	}
}
