using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001D2 RID: 466
	[SuppressMessage("Microsoft.Design", "CA1027:MarkEnumsWithFlags", Justification = "QueryNodeKind is not a flag.")]
	public enum QueryNodeKind
	{
		// Token: 0x04000969 RID: 2409
		None,
		// Token: 0x0400096A RID: 2410
		Constant,
		// Token: 0x0400096B RID: 2411
		Convert,
		// Token: 0x0400096C RID: 2412
		NonResourceRangeVariableReference,
		// Token: 0x0400096D RID: 2413
		BinaryOperator,
		// Token: 0x0400096E RID: 2414
		UnaryOperator,
		// Token: 0x0400096F RID: 2415
		SingleValuePropertyAccess,
		// Token: 0x04000970 RID: 2416
		CollectionPropertyAccess,
		// Token: 0x04000971 RID: 2417
		SingleValueFunctionCall,
		// Token: 0x04000972 RID: 2418
		Any,
		// Token: 0x04000973 RID: 2419
		CollectionNavigationNode,
		// Token: 0x04000974 RID: 2420
		SingleNavigationNode,
		// Token: 0x04000975 RID: 2421
		SingleValueOpenPropertyAccess,
		// Token: 0x04000976 RID: 2422
		SingleResourceCast,
		// Token: 0x04000977 RID: 2423
		All,
		// Token: 0x04000978 RID: 2424
		CollectionResourceCast,
		// Token: 0x04000979 RID: 2425
		ResourceRangeVariableReference,
		// Token: 0x0400097A RID: 2426
		SingleResourceFunctionCall,
		// Token: 0x0400097B RID: 2427
		CollectionFunctionCall,
		// Token: 0x0400097C RID: 2428
		CollectionResourceFunctionCall,
		// Token: 0x0400097D RID: 2429
		NamedFunctionParameter,
		// Token: 0x0400097E RID: 2430
		ParameterAlias,
		// Token: 0x0400097F RID: 2431
		EntitySet,
		// Token: 0x04000980 RID: 2432
		KeyLookup,
		// Token: 0x04000981 RID: 2433
		SearchTerm,
		// Token: 0x04000982 RID: 2434
		CollectionOpenPropertyAccess,
		// Token: 0x04000983 RID: 2435
		CollectionComplexNode,
		// Token: 0x04000984 RID: 2436
		SingleComplexNode,
		// Token: 0x04000985 RID: 2437
		Count,
		// Token: 0x04000986 RID: 2438
		SingleValueCast,
		// Token: 0x04000987 RID: 2439
		CollectionPropertyNode,
		// Token: 0x04000988 RID: 2440
		AggregatedCollectionPropertyNode,
		// Token: 0x04000989 RID: 2441
		In,
		// Token: 0x0400098A RID: 2442
		CollectionConstant
	}
}
