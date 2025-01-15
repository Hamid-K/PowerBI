using System;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000040 RID: 64
	internal enum ExpressionTokenKind
	{
		// Token: 0x04000191 RID: 401
		Unknown,
		// Token: 0x04000192 RID: 402
		End,
		// Token: 0x04000193 RID: 403
		Equal,
		// Token: 0x04000194 RID: 404
		Identifier,
		// Token: 0x04000195 RID: 405
		NullLiteral,
		// Token: 0x04000196 RID: 406
		BooleanLiteral,
		// Token: 0x04000197 RID: 407
		StringLiteral,
		// Token: 0x04000198 RID: 408
		IntegerLiteral,
		// Token: 0x04000199 RID: 409
		Int64Literal,
		// Token: 0x0400019A RID: 410
		SingleLiteral,
		// Token: 0x0400019B RID: 411
		DateTimeLiteral,
		// Token: 0x0400019C RID: 412
		DateTimeOffsetLiteral,
		// Token: 0x0400019D RID: 413
		TimeLiteral,
		// Token: 0x0400019E RID: 414
		DecimalLiteral,
		// Token: 0x0400019F RID: 415
		DoubleLiteral,
		// Token: 0x040001A0 RID: 416
		GuidLiteral,
		// Token: 0x040001A1 RID: 417
		BinaryLiteral,
		// Token: 0x040001A2 RID: 418
		GeographyLiteral,
		// Token: 0x040001A3 RID: 419
		GeometryLiteral,
		// Token: 0x040001A4 RID: 420
		Exclamation,
		// Token: 0x040001A5 RID: 421
		OpenParen,
		// Token: 0x040001A6 RID: 422
		CloseParen,
		// Token: 0x040001A7 RID: 423
		Comma,
		// Token: 0x040001A8 RID: 424
		Minus,
		// Token: 0x040001A9 RID: 425
		Slash,
		// Token: 0x040001AA RID: 426
		Question,
		// Token: 0x040001AB RID: 427
		Dot,
		// Token: 0x040001AC RID: 428
		Star
	}
}
