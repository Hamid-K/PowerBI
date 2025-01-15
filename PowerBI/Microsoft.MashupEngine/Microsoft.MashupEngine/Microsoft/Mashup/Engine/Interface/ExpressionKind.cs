using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000BC RID: 188
	public enum ExpressionKind
	{
		// Token: 0x040001DB RID: 475
		Binary,
		// Token: 0x040001DC RID: 476
		Constant,
		// Token: 0x040001DD RID: 477
		ElementAccess,
		// Token: 0x040001DE RID: 478
		Exports,
		// Token: 0x040001DF RID: 479
		FieldAccess,
		// Token: 0x040001E0 RID: 480
		Function,
		// Token: 0x040001E1 RID: 481
		Identifier,
		// Token: 0x040001E2 RID: 482
		If,
		// Token: 0x040001E3 RID: 483
		Invocation,
		// Token: 0x040001E4 RID: 484
		Let,
		// Token: 0x040001E5 RID: 485
		List,
		// Token: 0x040001E6 RID: 486
		MultiFieldRecordProjection,
		// Token: 0x040001E7 RID: 487
		NotImplemented,
		// Token: 0x040001E8 RID: 488
		Parentheses,
		// Token: 0x040001E9 RID: 489
		RangeList,
		// Token: 0x040001EA RID: 490
		Record,
		// Token: 0x040001EB RID: 491
		SectionIdentifier,
		// Token: 0x040001EC RID: 492
		Throw,
		// Token: 0x040001ED RID: 493
		TryCatch,
		// Token: 0x040001EE RID: 494
		Unary,
		// Token: 0x040001EF RID: 495
		Verbatim,
		// Token: 0x040001F0 RID: 496
		ImplicitIdentifier,
		// Token: 0x040001F1 RID: 497
		Type,
		// Token: 0x040001F2 RID: 498
		RecordType,
		// Token: 0x040001F3 RID: 499
		ListType,
		// Token: 0x040001F4 RID: 500
		TableType,
		// Token: 0x040001F5 RID: 501
		NullableType,
		// Token: 0x040001F6 RID: 502
		FunctionType
	}
}
