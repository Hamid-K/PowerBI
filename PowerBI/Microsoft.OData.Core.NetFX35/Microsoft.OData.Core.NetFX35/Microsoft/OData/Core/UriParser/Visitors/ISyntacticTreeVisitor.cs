using System;
using Microsoft.OData.Core.UriParser.Aggregation;
using Microsoft.OData.Core.UriParser.Syntactic;

namespace Microsoft.OData.Core.UriParser.Visitors
{
	// Token: 0x02000291 RID: 657
	internal interface ISyntacticTreeVisitor<T>
	{
		// Token: 0x06001672 RID: 5746
		T Visit(AllToken tokenIn);

		// Token: 0x06001673 RID: 5747
		T Visit(AnyToken tokenIn);

		// Token: 0x06001674 RID: 5748
		T Visit(BinaryOperatorToken tokenIn);

		// Token: 0x06001675 RID: 5749
		T Visit(DottedIdentifierToken tokenIn);

		// Token: 0x06001676 RID: 5750
		T Visit(ExpandToken tokenIn);

		// Token: 0x06001677 RID: 5751
		T Visit(ExpandTermToken tokenIn);

		// Token: 0x06001678 RID: 5752
		T Visit(FunctionCallToken tokenIn);

		// Token: 0x06001679 RID: 5753
		T Visit(LambdaToken tokenIn);

		// Token: 0x0600167A RID: 5754
		T Visit(LiteralToken tokenIn);

		// Token: 0x0600167B RID: 5755
		T Visit(InnerPathToken tokenIn);

		// Token: 0x0600167C RID: 5756
		T Visit(OrderByToken tokenIn);

		// Token: 0x0600167D RID: 5757
		T Visit(EndPathToken tokenIn);

		// Token: 0x0600167E RID: 5758
		T Visit(CustomQueryOptionToken tokenIn);

		// Token: 0x0600167F RID: 5759
		T Visit(RangeVariableToken tokenIn);

		// Token: 0x06001680 RID: 5760
		T Visit(SelectToken tokenIn);

		// Token: 0x06001681 RID: 5761
		T Visit(StarToken tokenIn);

		// Token: 0x06001682 RID: 5762
		T Visit(UnaryOperatorToken tokenIn);

		// Token: 0x06001683 RID: 5763
		T Visit(FunctionParameterToken tokenIn);

		// Token: 0x06001684 RID: 5764
		T Visit(AggregateToken tokenIn);

		// Token: 0x06001685 RID: 5765
		T Visit(AggregateExpressionToken tokenIn);

		// Token: 0x06001686 RID: 5766
		T Visit(GroupByToken tokenIn);
	}
}
