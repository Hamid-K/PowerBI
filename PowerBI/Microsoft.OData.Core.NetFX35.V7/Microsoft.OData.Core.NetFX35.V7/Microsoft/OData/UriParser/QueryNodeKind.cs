using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000185 RID: 389
	[SuppressMessage("Microsoft.Design", "CA1027:MarkEnumsWithFlags", Justification = "QueryNodeKind is not a flag.")]
	public enum QueryNodeKind
	{
		// Token: 0x04000836 RID: 2102
		None,
		// Token: 0x04000837 RID: 2103
		Constant,
		// Token: 0x04000838 RID: 2104
		Convert,
		// Token: 0x04000839 RID: 2105
		NonResourceRangeVariableReference,
		// Token: 0x0400083A RID: 2106
		BinaryOperator,
		// Token: 0x0400083B RID: 2107
		UnaryOperator,
		// Token: 0x0400083C RID: 2108
		SingleValuePropertyAccess,
		// Token: 0x0400083D RID: 2109
		CollectionPropertyAccess,
		// Token: 0x0400083E RID: 2110
		SingleValueFunctionCall,
		// Token: 0x0400083F RID: 2111
		Any,
		// Token: 0x04000840 RID: 2112
		CollectionNavigationNode,
		// Token: 0x04000841 RID: 2113
		SingleNavigationNode,
		// Token: 0x04000842 RID: 2114
		SingleValueOpenPropertyAccess,
		// Token: 0x04000843 RID: 2115
		SingleResourceCast,
		// Token: 0x04000844 RID: 2116
		All,
		// Token: 0x04000845 RID: 2117
		CollectionResourceCast,
		// Token: 0x04000846 RID: 2118
		ResourceRangeVariableReference,
		// Token: 0x04000847 RID: 2119
		SingleResourceFunctionCall,
		// Token: 0x04000848 RID: 2120
		CollectionFunctionCall,
		// Token: 0x04000849 RID: 2121
		CollectionResourceFunctionCall,
		// Token: 0x0400084A RID: 2122
		NamedFunctionParameter,
		// Token: 0x0400084B RID: 2123
		ParameterAlias,
		// Token: 0x0400084C RID: 2124
		EntitySet,
		// Token: 0x0400084D RID: 2125
		KeyLookup,
		// Token: 0x0400084E RID: 2126
		SearchTerm,
		// Token: 0x0400084F RID: 2127
		CollectionOpenPropertyAccess,
		// Token: 0x04000850 RID: 2128
		CollectionComplexNode,
		// Token: 0x04000851 RID: 2129
		SingleComplexNode,
		// Token: 0x04000852 RID: 2130
		Count,
		// Token: 0x04000853 RID: 2131
		SingleValueCast
	}
}
