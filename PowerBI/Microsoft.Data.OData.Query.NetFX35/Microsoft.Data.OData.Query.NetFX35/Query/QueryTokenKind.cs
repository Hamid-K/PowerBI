using System;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000054 RID: 84
	public enum QueryTokenKind
	{
		// Token: 0x040001EE RID: 494
		Extension,
		// Token: 0x040001EF RID: 495
		QueryDescriptor,
		// Token: 0x040001F0 RID: 496
		Segment,
		// Token: 0x040001F1 RID: 497
		BinaryOperator,
		// Token: 0x040001F2 RID: 498
		UnaryOperator,
		// Token: 0x040001F3 RID: 499
		Literal,
		// Token: 0x040001F4 RID: 500
		FunctionCall,
		// Token: 0x040001F5 RID: 501
		PropertyAccess,
		// Token: 0x040001F6 RID: 502
		OrderBy,
		// Token: 0x040001F7 RID: 503
		QueryOption,
		// Token: 0x040001F8 RID: 504
		Select,
		// Token: 0x040001F9 RID: 505
		Star,
		// Token: 0x040001FA RID: 506
		KeywordSegment,
		// Token: 0x040001FB RID: 507
		Expand
	}
}
