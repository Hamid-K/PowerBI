using System;

namespace Microsoft.OData.Core.UriParser.TreeNodeKinds
{
	// Token: 0x02000288 RID: 648
	internal enum InternalQueryNodeKind
	{
		// Token: 0x0400098E RID: 2446
		None,
		// Token: 0x0400098F RID: 2447
		Constant,
		// Token: 0x04000990 RID: 2448
		Convert,
		// Token: 0x04000991 RID: 2449
		NonentityRangeVariableReference,
		// Token: 0x04000992 RID: 2450
		BinaryOperator,
		// Token: 0x04000993 RID: 2451
		UnaryOperator,
		// Token: 0x04000994 RID: 2452
		SingleValuePropertyAccess,
		// Token: 0x04000995 RID: 2453
		CollectionPropertyAccess,
		// Token: 0x04000996 RID: 2454
		SingleValueFunctionCall,
		// Token: 0x04000997 RID: 2455
		Any,
		// Token: 0x04000998 RID: 2456
		CollectionNavigationNode,
		// Token: 0x04000999 RID: 2457
		SingleNavigationNode,
		// Token: 0x0400099A RID: 2458
		SingleValueOpenPropertyAccess,
		// Token: 0x0400099B RID: 2459
		SingleEntityCast,
		// Token: 0x0400099C RID: 2460
		All,
		// Token: 0x0400099D RID: 2461
		EntityCollectionCast,
		// Token: 0x0400099E RID: 2462
		EntityRangeVariableReference,
		// Token: 0x0400099F RID: 2463
		SingleEntityFunctionCall,
		// Token: 0x040009A0 RID: 2464
		CollectionFunctionCall,
		// Token: 0x040009A1 RID: 2465
		EntityCollectionFunctionCall,
		// Token: 0x040009A2 RID: 2466
		NamedFunctionParameter,
		// Token: 0x040009A3 RID: 2467
		ParameterAlias,
		// Token: 0x040009A4 RID: 2468
		EntitySet,
		// Token: 0x040009A5 RID: 2469
		KeyLookup,
		// Token: 0x040009A6 RID: 2470
		SearchTerm,
		// Token: 0x040009A7 RID: 2471
		CollectionOpenPropertyAccess,
		// Token: 0x040009A8 RID: 2472
		CollectionPropertyCast,
		// Token: 0x040009A9 RID: 2473
		SingleValueCast,
		// Token: 0x040009AA RID: 2474
		CollectionCount
	}
}
