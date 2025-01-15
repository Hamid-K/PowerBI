using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000184 RID: 388
	internal enum ExpressionTokenKind
	{
		// Token: 0x04000810 RID: 2064
		Unknown,
		// Token: 0x04000811 RID: 2065
		End,
		// Token: 0x04000812 RID: 2066
		Equal,
		// Token: 0x04000813 RID: 2067
		Identifier,
		// Token: 0x04000814 RID: 2068
		NullLiteral,
		// Token: 0x04000815 RID: 2069
		BooleanLiteral,
		// Token: 0x04000816 RID: 2070
		StringLiteral,
		// Token: 0x04000817 RID: 2071
		IntegerLiteral,
		// Token: 0x04000818 RID: 2072
		Int64Literal,
		// Token: 0x04000819 RID: 2073
		SingleLiteral,
		// Token: 0x0400081A RID: 2074
		DateTimeLiteral,
		// Token: 0x0400081B RID: 2075
		DateTimeOffsetLiteral,
		// Token: 0x0400081C RID: 2076
		DurationLiteral,
		// Token: 0x0400081D RID: 2077
		DecimalLiteral,
		// Token: 0x0400081E RID: 2078
		DoubleLiteral,
		// Token: 0x0400081F RID: 2079
		GuidLiteral,
		// Token: 0x04000820 RID: 2080
		BinaryLiteral,
		// Token: 0x04000821 RID: 2081
		GeographyLiteral,
		// Token: 0x04000822 RID: 2082
		GeometryLiteral,
		// Token: 0x04000823 RID: 2083
		Exclamation,
		// Token: 0x04000824 RID: 2084
		OpenParen,
		// Token: 0x04000825 RID: 2085
		CloseParen,
		// Token: 0x04000826 RID: 2086
		Comma,
		// Token: 0x04000827 RID: 2087
		Colon,
		// Token: 0x04000828 RID: 2088
		Minus,
		// Token: 0x04000829 RID: 2089
		Slash,
		// Token: 0x0400082A RID: 2090
		Question,
		// Token: 0x0400082B RID: 2091
		Dot,
		// Token: 0x0400082C RID: 2092
		Star,
		// Token: 0x0400082D RID: 2093
		SemiColon,
		// Token: 0x0400082E RID: 2094
		ParameterAlias,
		// Token: 0x0400082F RID: 2095
		BracedExpression,
		// Token: 0x04000830 RID: 2096
		BracketedExpression,
		// Token: 0x04000831 RID: 2097
		QuotedLiteral,
		// Token: 0x04000832 RID: 2098
		DateLiteral,
		// Token: 0x04000833 RID: 2099
		TimeOfDayLiteral,
		// Token: 0x04000834 RID: 2100
		CustomTypeLiteral
	}
}
