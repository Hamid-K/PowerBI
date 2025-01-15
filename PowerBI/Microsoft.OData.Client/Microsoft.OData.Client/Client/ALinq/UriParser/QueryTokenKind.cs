using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x02000132 RID: 306
	[SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue")]
	public enum QueryTokenKind
	{
		// Token: 0x04000682 RID: 1666
		BinaryOperator = 3,
		// Token: 0x04000683 RID: 1667
		UnaryOperator,
		// Token: 0x04000684 RID: 1668
		Literal,
		// Token: 0x04000685 RID: 1669
		FunctionCall,
		// Token: 0x04000686 RID: 1670
		EndPath,
		// Token: 0x04000687 RID: 1671
		OrderBy,
		// Token: 0x04000688 RID: 1672
		CustomQueryOption,
		// Token: 0x04000689 RID: 1673
		Select,
		// Token: 0x0400068A RID: 1674
		Star,
		// Token: 0x0400068B RID: 1675
		Expand = 13,
		// Token: 0x0400068C RID: 1676
		TypeSegment,
		// Token: 0x0400068D RID: 1677
		Any,
		// Token: 0x0400068E RID: 1678
		InnerPath,
		// Token: 0x0400068F RID: 1679
		DottedIdentifier,
		// Token: 0x04000690 RID: 1680
		RangeVariable,
		// Token: 0x04000691 RID: 1681
		All,
		// Token: 0x04000692 RID: 1682
		ExpandTerm,
		// Token: 0x04000693 RID: 1683
		FunctionParameter,
		// Token: 0x04000694 RID: 1684
		FunctionParameterAlias,
		// Token: 0x04000695 RID: 1685
		StringLiteral,
		// Token: 0x04000696 RID: 1686
		Aggregate,
		// Token: 0x04000697 RID: 1687
		AggregateExpression,
		// Token: 0x04000698 RID: 1688
		AggregateGroupBy,
		// Token: 0x04000699 RID: 1689
		Compute,
		// Token: 0x0400069A RID: 1690
		ComputeExpression,
		// Token: 0x0400069B RID: 1691
		EntitySetAggregateExpression,
		// Token: 0x0400069C RID: 1692
		In,
		// Token: 0x0400069D RID: 1693
		SelectTerm
	}
}
