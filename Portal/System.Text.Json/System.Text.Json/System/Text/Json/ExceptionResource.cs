using System;

namespace System.Text.Json
{
	// Token: 0x02000057 RID: 87
	internal enum ExceptionResource
	{
		// Token: 0x04000224 RID: 548
		ArrayDepthTooLarge,
		// Token: 0x04000225 RID: 549
		EndOfCommentNotFound,
		// Token: 0x04000226 RID: 550
		EndOfStringNotFound,
		// Token: 0x04000227 RID: 551
		RequiredDigitNotFoundAfterDecimal,
		// Token: 0x04000228 RID: 552
		RequiredDigitNotFoundAfterSign,
		// Token: 0x04000229 RID: 553
		RequiredDigitNotFoundEndOfData,
		// Token: 0x0400022A RID: 554
		ExpectedEndAfterSingleJson,
		// Token: 0x0400022B RID: 555
		ExpectedEndOfDigitNotFound,
		// Token: 0x0400022C RID: 556
		ExpectedFalse,
		// Token: 0x0400022D RID: 557
		ExpectedNextDigitEValueNotFound,
		// Token: 0x0400022E RID: 558
		ExpectedNull,
		// Token: 0x0400022F RID: 559
		ExpectedSeparatorAfterPropertyNameNotFound,
		// Token: 0x04000230 RID: 560
		ExpectedStartOfPropertyNotFound,
		// Token: 0x04000231 RID: 561
		ExpectedStartOfPropertyOrValueNotFound,
		// Token: 0x04000232 RID: 562
		ExpectedStartOfPropertyOrValueAfterComment,
		// Token: 0x04000233 RID: 563
		ExpectedStartOfValueNotFound,
		// Token: 0x04000234 RID: 564
		ExpectedTrue,
		// Token: 0x04000235 RID: 565
		ExpectedValueAfterPropertyNameNotFound,
		// Token: 0x04000236 RID: 566
		FoundInvalidCharacter,
		// Token: 0x04000237 RID: 567
		InvalidCharacterWithinString,
		// Token: 0x04000238 RID: 568
		InvalidCharacterAfterEscapeWithinString,
		// Token: 0x04000239 RID: 569
		InvalidHexCharacterWithinString,
		// Token: 0x0400023A RID: 570
		InvalidEndOfJsonNonPrimitive,
		// Token: 0x0400023B RID: 571
		MismatchedObjectArray,
		// Token: 0x0400023C RID: 572
		ObjectDepthTooLarge,
		// Token: 0x0400023D RID: 573
		ZeroDepthAtEnd,
		// Token: 0x0400023E RID: 574
		DepthTooLarge,
		// Token: 0x0400023F RID: 575
		CannotStartObjectArrayWithoutProperty,
		// Token: 0x04000240 RID: 576
		CannotStartObjectArrayAfterPrimitiveOrClose,
		// Token: 0x04000241 RID: 577
		CannotWriteValueWithinObject,
		// Token: 0x04000242 RID: 578
		CannotWriteValueAfterPrimitiveOrClose,
		// Token: 0x04000243 RID: 579
		CannotWritePropertyWithinArray,
		// Token: 0x04000244 RID: 580
		ExpectedJsonTokens,
		// Token: 0x04000245 RID: 581
		TrailingCommaNotAllowedBeforeArrayEnd,
		// Token: 0x04000246 RID: 582
		TrailingCommaNotAllowedBeforeObjectEnd,
		// Token: 0x04000247 RID: 583
		InvalidCharacterAtStartOfComment,
		// Token: 0x04000248 RID: 584
		UnexpectedEndOfDataWhileReadingComment,
		// Token: 0x04000249 RID: 585
		UnexpectedEndOfLineSeparator,
		// Token: 0x0400024A RID: 586
		ExpectedOneCompleteToken,
		// Token: 0x0400024B RID: 587
		NotEnoughData,
		// Token: 0x0400024C RID: 588
		InvalidLeadingZeroInNumber
	}
}
