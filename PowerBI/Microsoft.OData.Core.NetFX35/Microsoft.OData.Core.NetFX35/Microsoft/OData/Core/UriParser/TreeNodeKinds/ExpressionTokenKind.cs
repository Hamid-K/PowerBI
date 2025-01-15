using System;

namespace Microsoft.OData.Core.UriParser.TreeNodeKinds
{
	// Token: 0x02000286 RID: 646
	internal enum ExpressionTokenKind
	{
		// Token: 0x0400094C RID: 2380
		Unknown,
		// Token: 0x0400094D RID: 2381
		End,
		// Token: 0x0400094E RID: 2382
		Equal,
		// Token: 0x0400094F RID: 2383
		Identifier,
		// Token: 0x04000950 RID: 2384
		NullLiteral,
		// Token: 0x04000951 RID: 2385
		BooleanLiteral,
		// Token: 0x04000952 RID: 2386
		StringLiteral,
		// Token: 0x04000953 RID: 2387
		IntegerLiteral,
		// Token: 0x04000954 RID: 2388
		Int64Literal,
		// Token: 0x04000955 RID: 2389
		SingleLiteral,
		// Token: 0x04000956 RID: 2390
		DateTimeLiteral,
		// Token: 0x04000957 RID: 2391
		DateTimeOffsetLiteral,
		// Token: 0x04000958 RID: 2392
		DurationLiteral,
		// Token: 0x04000959 RID: 2393
		DecimalLiteral,
		// Token: 0x0400095A RID: 2394
		DoubleLiteral,
		// Token: 0x0400095B RID: 2395
		GuidLiteral,
		// Token: 0x0400095C RID: 2396
		BinaryLiteral,
		// Token: 0x0400095D RID: 2397
		GeographyLiteral,
		// Token: 0x0400095E RID: 2398
		GeometryLiteral,
		// Token: 0x0400095F RID: 2399
		Exclamation,
		// Token: 0x04000960 RID: 2400
		OpenParen,
		// Token: 0x04000961 RID: 2401
		CloseParen,
		// Token: 0x04000962 RID: 2402
		Comma,
		// Token: 0x04000963 RID: 2403
		Colon,
		// Token: 0x04000964 RID: 2404
		Minus,
		// Token: 0x04000965 RID: 2405
		Slash,
		// Token: 0x04000966 RID: 2406
		Question,
		// Token: 0x04000967 RID: 2407
		Dot,
		// Token: 0x04000968 RID: 2408
		Star,
		// Token: 0x04000969 RID: 2409
		SemiColon,
		// Token: 0x0400096A RID: 2410
		ParameterAlias,
		// Token: 0x0400096B RID: 2411
		BracketedExpression,
		// Token: 0x0400096C RID: 2412
		QuotedLiteral,
		// Token: 0x0400096D RID: 2413
		DateLiteral,
		// Token: 0x0400096E RID: 2414
		TimeOfDayLiteral,
		// Token: 0x0400096F RID: 2415
		CustomTypeLiteral
	}
}
