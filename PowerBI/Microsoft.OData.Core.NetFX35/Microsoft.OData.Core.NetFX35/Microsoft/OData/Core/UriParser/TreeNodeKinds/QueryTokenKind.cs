using System;

namespace Microsoft.OData.Core.UriParser.TreeNodeKinds
{
	// Token: 0x02000289 RID: 649
	internal enum QueryTokenKind
	{
		// Token: 0x040009AC RID: 2476
		BinaryOperator = 3,
		// Token: 0x040009AD RID: 2477
		UnaryOperator,
		// Token: 0x040009AE RID: 2478
		Literal,
		// Token: 0x040009AF RID: 2479
		FunctionCall,
		// Token: 0x040009B0 RID: 2480
		EndPath,
		// Token: 0x040009B1 RID: 2481
		OrderBy,
		// Token: 0x040009B2 RID: 2482
		CustomQueryOption,
		// Token: 0x040009B3 RID: 2483
		Select,
		// Token: 0x040009B4 RID: 2484
		Star,
		// Token: 0x040009B5 RID: 2485
		Expand = 13,
		// Token: 0x040009B6 RID: 2486
		TypeSegment,
		// Token: 0x040009B7 RID: 2487
		Any,
		// Token: 0x040009B8 RID: 2488
		InnerPath,
		// Token: 0x040009B9 RID: 2489
		DottedIdentifier,
		// Token: 0x040009BA RID: 2490
		RangeVariable,
		// Token: 0x040009BB RID: 2491
		All,
		// Token: 0x040009BC RID: 2492
		ExpandTerm,
		// Token: 0x040009BD RID: 2493
		FunctionParameter,
		// Token: 0x040009BE RID: 2494
		FunctionParameterAlias,
		// Token: 0x040009BF RID: 2495
		StringLiteral,
		// Token: 0x040009C0 RID: 2496
		Aggregate,
		// Token: 0x040009C1 RID: 2497
		AggregateExpression,
		// Token: 0x040009C2 RID: 2498
		AggregateGroupBy
	}
}
