using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001D4 RID: 468
	[SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue")]
	public enum QueryTokenKind
	{
		// Token: 0x040009AF RID: 2479
		BinaryOperator = 3,
		// Token: 0x040009B0 RID: 2480
		UnaryOperator,
		// Token: 0x040009B1 RID: 2481
		Literal,
		// Token: 0x040009B2 RID: 2482
		FunctionCall,
		// Token: 0x040009B3 RID: 2483
		EndPath,
		// Token: 0x040009B4 RID: 2484
		OrderBy,
		// Token: 0x040009B5 RID: 2485
		CustomQueryOption,
		// Token: 0x040009B6 RID: 2486
		Select,
		// Token: 0x040009B7 RID: 2487
		Star,
		// Token: 0x040009B8 RID: 2488
		Expand = 13,
		// Token: 0x040009B9 RID: 2489
		TypeSegment,
		// Token: 0x040009BA RID: 2490
		Any,
		// Token: 0x040009BB RID: 2491
		InnerPath,
		// Token: 0x040009BC RID: 2492
		DottedIdentifier,
		// Token: 0x040009BD RID: 2493
		RangeVariable,
		// Token: 0x040009BE RID: 2494
		All,
		// Token: 0x040009BF RID: 2495
		ExpandTerm,
		// Token: 0x040009C0 RID: 2496
		FunctionParameter,
		// Token: 0x040009C1 RID: 2497
		FunctionParameterAlias,
		// Token: 0x040009C2 RID: 2498
		StringLiteral,
		// Token: 0x040009C3 RID: 2499
		Aggregate,
		// Token: 0x040009C4 RID: 2500
		AggregateExpression,
		// Token: 0x040009C5 RID: 2501
		AggregateGroupBy,
		// Token: 0x040009C6 RID: 2502
		Compute,
		// Token: 0x040009C7 RID: 2503
		ComputeExpression,
		// Token: 0x040009C8 RID: 2504
		EntitySetAggregateExpression,
		// Token: 0x040009C9 RID: 2505
		In,
		// Token: 0x040009CA RID: 2506
		SelectTerm
	}
}
