using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001D1 RID: 465
	internal enum ExpressionTokenKind
	{
		// Token: 0x04000942 RID: 2370
		Unknown,
		// Token: 0x04000943 RID: 2371
		End,
		// Token: 0x04000944 RID: 2372
		Equal,
		// Token: 0x04000945 RID: 2373
		Identifier,
		// Token: 0x04000946 RID: 2374
		NullLiteral,
		// Token: 0x04000947 RID: 2375
		BooleanLiteral,
		// Token: 0x04000948 RID: 2376
		StringLiteral,
		// Token: 0x04000949 RID: 2377
		IntegerLiteral,
		// Token: 0x0400094A RID: 2378
		Int64Literal,
		// Token: 0x0400094B RID: 2379
		SingleLiteral,
		// Token: 0x0400094C RID: 2380
		DateTimeLiteral,
		// Token: 0x0400094D RID: 2381
		DateTimeOffsetLiteral,
		// Token: 0x0400094E RID: 2382
		DurationLiteral,
		// Token: 0x0400094F RID: 2383
		DecimalLiteral,
		// Token: 0x04000950 RID: 2384
		DoubleLiteral,
		// Token: 0x04000951 RID: 2385
		GuidLiteral,
		// Token: 0x04000952 RID: 2386
		BinaryLiteral,
		// Token: 0x04000953 RID: 2387
		GeographyLiteral,
		// Token: 0x04000954 RID: 2388
		GeometryLiteral,
		// Token: 0x04000955 RID: 2389
		Exclamation,
		// Token: 0x04000956 RID: 2390
		OpenParen,
		// Token: 0x04000957 RID: 2391
		CloseParen,
		// Token: 0x04000958 RID: 2392
		Comma,
		// Token: 0x04000959 RID: 2393
		Colon,
		// Token: 0x0400095A RID: 2394
		Minus,
		// Token: 0x0400095B RID: 2395
		Slash,
		// Token: 0x0400095C RID: 2396
		Question,
		// Token: 0x0400095D RID: 2397
		Dot,
		// Token: 0x0400095E RID: 2398
		Star,
		// Token: 0x0400095F RID: 2399
		SemiColon,
		// Token: 0x04000960 RID: 2400
		ParameterAlias,
		// Token: 0x04000961 RID: 2401
		BracedExpression,
		// Token: 0x04000962 RID: 2402
		BracketedExpression,
		// Token: 0x04000963 RID: 2403
		QuotedLiteral,
		// Token: 0x04000964 RID: 2404
		DateLiteral,
		// Token: 0x04000965 RID: 2405
		TimeOfDayLiteral,
		// Token: 0x04000966 RID: 2406
		CustomTypeLiteral,
		// Token: 0x04000967 RID: 2407
		ParenthesesExpression
	}
}
