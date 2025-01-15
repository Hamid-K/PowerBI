using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.Core.UriParser.TreeNodeKinds
{
	// Token: 0x02000287 RID: 647
	[SuppressMessage("Microsoft.Design", "CA1027:MarkEnumsWithFlags", Justification = "QueryNodeKind is not a flag.")]
	public enum QueryNodeKind
	{
		// Token: 0x04000971 RID: 2417
		None,
		// Token: 0x04000972 RID: 2418
		Constant,
		// Token: 0x04000973 RID: 2419
		Convert,
		// Token: 0x04000974 RID: 2420
		NonentityRangeVariableReference,
		// Token: 0x04000975 RID: 2421
		BinaryOperator,
		// Token: 0x04000976 RID: 2422
		UnaryOperator,
		// Token: 0x04000977 RID: 2423
		SingleValuePropertyAccess,
		// Token: 0x04000978 RID: 2424
		CollectionPropertyAccess,
		// Token: 0x04000979 RID: 2425
		SingleValueFunctionCall,
		// Token: 0x0400097A RID: 2426
		Any,
		// Token: 0x0400097B RID: 2427
		CollectionNavigationNode,
		// Token: 0x0400097C RID: 2428
		SingleNavigationNode,
		// Token: 0x0400097D RID: 2429
		SingleValueOpenPropertyAccess,
		// Token: 0x0400097E RID: 2430
		SingleEntityCast,
		// Token: 0x0400097F RID: 2431
		All,
		// Token: 0x04000980 RID: 2432
		EntityCollectionCast,
		// Token: 0x04000981 RID: 2433
		EntityRangeVariableReference,
		// Token: 0x04000982 RID: 2434
		SingleEntityFunctionCall,
		// Token: 0x04000983 RID: 2435
		CollectionFunctionCall,
		// Token: 0x04000984 RID: 2436
		EntityCollectionFunctionCall,
		// Token: 0x04000985 RID: 2437
		NamedFunctionParameter,
		// Token: 0x04000986 RID: 2438
		ParameterAlias,
		// Token: 0x04000987 RID: 2439
		EntitySet,
		// Token: 0x04000988 RID: 2440
		KeyLookup,
		// Token: 0x04000989 RID: 2441
		SearchTerm,
		// Token: 0x0400098A RID: 2442
		CollectionOpenPropertyAccess,
		// Token: 0x0400098B RID: 2443
		CollectionPropertyCast,
		// Token: 0x0400098C RID: 2444
		SingleValueCast
	}
}
