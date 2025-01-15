using System;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001DC RID: 476
	public interface ISyntacticTreeVisitor<T>
	{
		// Token: 0x0600154B RID: 5451
		T Visit(AllToken tokenIn);

		// Token: 0x0600154C RID: 5452
		T Visit(AnyToken tokenIn);

		// Token: 0x0600154D RID: 5453
		T Visit(BinaryOperatorToken tokenIn);

		// Token: 0x0600154E RID: 5454
		T Visit(InToken tokenIn);

		// Token: 0x0600154F RID: 5455
		T Visit(DottedIdentifierToken tokenIn);

		// Token: 0x06001550 RID: 5456
		T Visit(ExpandToken tokenIn);

		// Token: 0x06001551 RID: 5457
		T Visit(ExpandTermToken tokenIn);

		// Token: 0x06001552 RID: 5458
		T Visit(FunctionCallToken tokenIn);

		// Token: 0x06001553 RID: 5459
		T Visit(LambdaToken tokenIn);

		// Token: 0x06001554 RID: 5460
		T Visit(LiteralToken tokenIn);

		// Token: 0x06001555 RID: 5461
		T Visit(InnerPathToken tokenIn);

		// Token: 0x06001556 RID: 5462
		T Visit(OrderByToken tokenIn);

		// Token: 0x06001557 RID: 5463
		T Visit(EndPathToken tokenIn);

		// Token: 0x06001558 RID: 5464
		T Visit(CustomQueryOptionToken tokenIn);

		// Token: 0x06001559 RID: 5465
		T Visit(RangeVariableToken tokenIn);

		// Token: 0x0600155A RID: 5466
		T Visit(SelectToken tokenIn);

		// Token: 0x0600155B RID: 5467
		T Visit(SelectTermToken tokenIn);

		// Token: 0x0600155C RID: 5468
		T Visit(StarToken tokenIn);

		// Token: 0x0600155D RID: 5469
		T Visit(UnaryOperatorToken tokenIn);

		// Token: 0x0600155E RID: 5470
		T Visit(FunctionParameterToken tokenIn);

		// Token: 0x0600155F RID: 5471
		T Visit(AggregateToken tokenIn);

		// Token: 0x06001560 RID: 5472
		T Visit(AggregateExpressionToken tokenIn);

		// Token: 0x06001561 RID: 5473
		T Visit(EntitySetAggregateToken tokenIn);

		// Token: 0x06001562 RID: 5474
		T Visit(GroupByToken tokenIn);
	}
}
