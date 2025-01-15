using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000186 RID: 390
	internal enum InternalQueryNodeKind
	{
		// Token: 0x04000855 RID: 2133
		None,
		// Token: 0x04000856 RID: 2134
		Constant,
		// Token: 0x04000857 RID: 2135
		Convert,
		// Token: 0x04000858 RID: 2136
		NonResourceRangeVariableReference,
		// Token: 0x04000859 RID: 2137
		BinaryOperator,
		// Token: 0x0400085A RID: 2138
		UnaryOperator,
		// Token: 0x0400085B RID: 2139
		SingleValuePropertyAccess,
		// Token: 0x0400085C RID: 2140
		CollectionPropertyAccess,
		// Token: 0x0400085D RID: 2141
		SingleValueFunctionCall,
		// Token: 0x0400085E RID: 2142
		Any,
		// Token: 0x0400085F RID: 2143
		CollectionNavigationNode,
		// Token: 0x04000860 RID: 2144
		SingleNavigationNode,
		// Token: 0x04000861 RID: 2145
		SingleValueOpenPropertyAccess,
		// Token: 0x04000862 RID: 2146
		SingleResourceCast,
		// Token: 0x04000863 RID: 2147
		All,
		// Token: 0x04000864 RID: 2148
		CollectionResourceCast,
		// Token: 0x04000865 RID: 2149
		ResourceRangeVariableReference,
		// Token: 0x04000866 RID: 2150
		SingleResourceFunctionCall,
		// Token: 0x04000867 RID: 2151
		CollectionFunctionCall,
		// Token: 0x04000868 RID: 2152
		CollectionResourceFunctionCall,
		// Token: 0x04000869 RID: 2153
		NamedFunctionParameter,
		// Token: 0x0400086A RID: 2154
		ParameterAlias,
		// Token: 0x0400086B RID: 2155
		EntitySet,
		// Token: 0x0400086C RID: 2156
		KeyLookup,
		// Token: 0x0400086D RID: 2157
		SearchTerm,
		// Token: 0x0400086E RID: 2158
		CollectionOpenPropertyAccess,
		// Token: 0x0400086F RID: 2159
		CollectionComplexNode,
		// Token: 0x04000870 RID: 2160
		SingleComplexNode,
		// Token: 0x04000871 RID: 2161
		Count,
		// Token: 0x04000872 RID: 2162
		SingleValueCast
	}
}
