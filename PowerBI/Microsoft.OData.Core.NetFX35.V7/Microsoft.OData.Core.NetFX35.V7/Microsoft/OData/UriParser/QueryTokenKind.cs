using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200017C RID: 380
	[SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue")]
	public enum QueryTokenKind
	{
		// Token: 0x040007E0 RID: 2016
		BinaryOperator = 3,
		// Token: 0x040007E1 RID: 2017
		UnaryOperator,
		// Token: 0x040007E2 RID: 2018
		Literal,
		// Token: 0x040007E3 RID: 2019
		FunctionCall,
		// Token: 0x040007E4 RID: 2020
		EndPath,
		// Token: 0x040007E5 RID: 2021
		OrderBy,
		// Token: 0x040007E6 RID: 2022
		CustomQueryOption,
		// Token: 0x040007E7 RID: 2023
		Select,
		// Token: 0x040007E8 RID: 2024
		Star,
		// Token: 0x040007E9 RID: 2025
		Expand = 13,
		// Token: 0x040007EA RID: 2026
		TypeSegment,
		// Token: 0x040007EB RID: 2027
		Any,
		// Token: 0x040007EC RID: 2028
		InnerPath,
		// Token: 0x040007ED RID: 2029
		DottedIdentifier,
		// Token: 0x040007EE RID: 2030
		RangeVariable,
		// Token: 0x040007EF RID: 2031
		All,
		// Token: 0x040007F0 RID: 2032
		ExpandTerm,
		// Token: 0x040007F1 RID: 2033
		FunctionParameter,
		// Token: 0x040007F2 RID: 2034
		FunctionParameterAlias,
		// Token: 0x040007F3 RID: 2035
		StringLiteral,
		// Token: 0x040007F4 RID: 2036
		Aggregate,
		// Token: 0x040007F5 RID: 2037
		AggregateExpression,
		// Token: 0x040007F6 RID: 2038
		AggregateGroupBy,
		// Token: 0x040007F7 RID: 2039
		Compute,
		// Token: 0x040007F8 RID: 2040
		ComputeExpression
	}
}
