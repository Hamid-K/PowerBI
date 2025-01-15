using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001D3 RID: 467
	internal enum InternalQueryNodeKind
	{
		// Token: 0x0400098C RID: 2444
		None,
		// Token: 0x0400098D RID: 2445
		Constant,
		// Token: 0x0400098E RID: 2446
		Convert,
		// Token: 0x0400098F RID: 2447
		NonResourceRangeVariableReference,
		// Token: 0x04000990 RID: 2448
		BinaryOperator,
		// Token: 0x04000991 RID: 2449
		UnaryOperator,
		// Token: 0x04000992 RID: 2450
		SingleValuePropertyAccess,
		// Token: 0x04000993 RID: 2451
		CollectionPropertyAccess,
		// Token: 0x04000994 RID: 2452
		SingleValueFunctionCall,
		// Token: 0x04000995 RID: 2453
		Any,
		// Token: 0x04000996 RID: 2454
		CollectionNavigationNode,
		// Token: 0x04000997 RID: 2455
		SingleNavigationNode,
		// Token: 0x04000998 RID: 2456
		SingleValueOpenPropertyAccess,
		// Token: 0x04000999 RID: 2457
		SingleResourceCast,
		// Token: 0x0400099A RID: 2458
		All,
		// Token: 0x0400099B RID: 2459
		CollectionResourceCast,
		// Token: 0x0400099C RID: 2460
		ResourceRangeVariableReference,
		// Token: 0x0400099D RID: 2461
		SingleResourceFunctionCall,
		// Token: 0x0400099E RID: 2462
		CollectionFunctionCall,
		// Token: 0x0400099F RID: 2463
		CollectionResourceFunctionCall,
		// Token: 0x040009A0 RID: 2464
		NamedFunctionParameter,
		// Token: 0x040009A1 RID: 2465
		ParameterAlias,
		// Token: 0x040009A2 RID: 2466
		EntitySet,
		// Token: 0x040009A3 RID: 2467
		KeyLookup,
		// Token: 0x040009A4 RID: 2468
		SearchTerm,
		// Token: 0x040009A5 RID: 2469
		CollectionOpenPropertyAccess,
		// Token: 0x040009A6 RID: 2470
		CollectionComplexNode,
		// Token: 0x040009A7 RID: 2471
		SingleComplexNode,
		// Token: 0x040009A8 RID: 2472
		Count,
		// Token: 0x040009A9 RID: 2473
		SingleValueCast,
		// Token: 0x040009AA RID: 2474
		CollectionPropertyNode,
		// Token: 0x040009AB RID: 2475
		AggregatedCollectionPropertyNode,
		// Token: 0x040009AC RID: 2476
		In,
		// Token: 0x040009AD RID: 2477
		CollectionConstant
	}
}
