using System;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200018D RID: 397
	public interface ISyntacticTreeVisitor<T>
	{
		// Token: 0x06000FFE RID: 4094
		T Visit(AllToken tokenIn);

		// Token: 0x06000FFF RID: 4095
		T Visit(AnyToken tokenIn);

		// Token: 0x06001000 RID: 4096
		T Visit(BinaryOperatorToken tokenIn);

		// Token: 0x06001001 RID: 4097
		T Visit(DottedIdentifierToken tokenIn);

		// Token: 0x06001002 RID: 4098
		T Visit(ExpandToken tokenIn);

		// Token: 0x06001003 RID: 4099
		T Visit(ExpandTermToken tokenIn);

		// Token: 0x06001004 RID: 4100
		T Visit(FunctionCallToken tokenIn);

		// Token: 0x06001005 RID: 4101
		T Visit(LambdaToken tokenIn);

		// Token: 0x06001006 RID: 4102
		T Visit(LiteralToken tokenIn);

		// Token: 0x06001007 RID: 4103
		T Visit(InnerPathToken tokenIn);

		// Token: 0x06001008 RID: 4104
		T Visit(OrderByToken tokenIn);

		// Token: 0x06001009 RID: 4105
		T Visit(EndPathToken tokenIn);

		// Token: 0x0600100A RID: 4106
		T Visit(CustomQueryOptionToken tokenIn);

		// Token: 0x0600100B RID: 4107
		T Visit(RangeVariableToken tokenIn);

		// Token: 0x0600100C RID: 4108
		T Visit(SelectToken tokenIn);

		// Token: 0x0600100D RID: 4109
		T Visit(StarToken tokenIn);

		// Token: 0x0600100E RID: 4110
		T Visit(UnaryOperatorToken tokenIn);

		// Token: 0x0600100F RID: 4111
		T Visit(FunctionParameterToken tokenIn);

		// Token: 0x06001010 RID: 4112
		T Visit(AggregateToken tokenIn);

		// Token: 0x06001011 RID: 4113
		T Visit(AggregateExpressionToken tokenIn);

		// Token: 0x06001012 RID: 4114
		T Visit(GroupByToken tokenIn);
	}
}
