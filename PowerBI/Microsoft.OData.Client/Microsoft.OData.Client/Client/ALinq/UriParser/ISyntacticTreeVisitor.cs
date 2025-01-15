using System;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x02000128 RID: 296
	public interface ISyntacticTreeVisitor<T>
	{
		// Token: 0x06000C2A RID: 3114
		T Visit(AllToken tokenIn);

		// Token: 0x06000C2B RID: 3115
		T Visit(AnyToken tokenIn);

		// Token: 0x06000C2C RID: 3116
		T Visit(BinaryOperatorToken tokenIn);

		// Token: 0x06000C2D RID: 3117
		T Visit(InToken tokenIn);

		// Token: 0x06000C2E RID: 3118
		T Visit(DottedIdentifierToken tokenIn);

		// Token: 0x06000C2F RID: 3119
		T Visit(ExpandToken tokenIn);

		// Token: 0x06000C30 RID: 3120
		T Visit(ExpandTermToken tokenIn);

		// Token: 0x06000C31 RID: 3121
		T Visit(FunctionCallToken tokenIn);

		// Token: 0x06000C32 RID: 3122
		T Visit(LambdaToken tokenIn);

		// Token: 0x06000C33 RID: 3123
		T Visit(LiteralToken tokenIn);

		// Token: 0x06000C34 RID: 3124
		T Visit(InnerPathToken tokenIn);

		// Token: 0x06000C35 RID: 3125
		T Visit(OrderByToken tokenIn);

		// Token: 0x06000C36 RID: 3126
		T Visit(EndPathToken tokenIn);

		// Token: 0x06000C37 RID: 3127
		T Visit(CustomQueryOptionToken tokenIn);

		// Token: 0x06000C38 RID: 3128
		T Visit(RangeVariableToken tokenIn);

		// Token: 0x06000C39 RID: 3129
		T Visit(SelectToken tokenIn);

		// Token: 0x06000C3A RID: 3130
		T Visit(SelectTermToken tokenIn);

		// Token: 0x06000C3B RID: 3131
		T Visit(StarToken tokenIn);

		// Token: 0x06000C3C RID: 3132
		T Visit(UnaryOperatorToken tokenIn);

		// Token: 0x06000C3D RID: 3133
		T Visit(FunctionParameterToken tokenIn);

		// Token: 0x06000C3E RID: 3134
		T Visit(AggregateToken tokenIn);

		// Token: 0x06000C3F RID: 3135
		T Visit(AggregateExpressionToken tokenIn);

		// Token: 0x06000C40 RID: 3136
		T Visit(EntitySetAggregateToken tokenIn);

		// Token: 0x06000C41 RID: 3137
		T Visit(GroupByToken tokenIn);
	}
}
