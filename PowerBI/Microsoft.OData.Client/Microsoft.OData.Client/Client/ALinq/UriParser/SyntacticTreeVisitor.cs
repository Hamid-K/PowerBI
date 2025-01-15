using System;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x02000129 RID: 297
	internal abstract class SyntacticTreeVisitor<T> : ISyntacticTreeVisitor<T>
	{
		// Token: 0x06000C42 RID: 3138 RVA: 0x00006FEF File Offset: 0x000051EF
		public virtual T Visit(AllToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x00006FEF File Offset: 0x000051EF
		public virtual T Visit(AnyToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x00006FEF File Offset: 0x000051EF
		public virtual T Visit(BinaryOperatorToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x00006FEF File Offset: 0x000051EF
		public virtual T Visit(InToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00006FEF File Offset: 0x000051EF
		public virtual T Visit(DottedIdentifierToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x00006FEF File Offset: 0x000051EF
		public virtual T Visit(ExpandToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x00006FEF File Offset: 0x000051EF
		public virtual T Visit(ExpandTermToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x00006FEF File Offset: 0x000051EF
		public virtual T Visit(FunctionCallToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x00006FEF File Offset: 0x000051EF
		public virtual T Visit(LiteralToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x00006FEF File Offset: 0x000051EF
		public virtual T Visit(AggregateToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00006FEF File Offset: 0x000051EF
		public virtual T Visit(GroupByToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x00006FEF File Offset: 0x000051EF
		public virtual T Visit(AggregateExpressionToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00006FEF File Offset: 0x000051EF
		public virtual T Visit(EntitySetAggregateToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00006FEF File Offset: 0x000051EF
		public virtual T Visit(LambdaToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00006FEF File Offset: 0x000051EF
		public virtual T Visit(InnerPathToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x00006FEF File Offset: 0x000051EF
		public virtual T Visit(OrderByToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x00006FEF File Offset: 0x000051EF
		public virtual T Visit(EndPathToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00006FEF File Offset: 0x000051EF
		public virtual T Visit(CustomQueryOptionToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00006FEF File Offset: 0x000051EF
		public virtual T Visit(RangeVariableToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00006FEF File Offset: 0x000051EF
		public virtual T Visit(SelectToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00006FEF File Offset: 0x000051EF
		public virtual T Visit(SelectTermToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x00006FEF File Offset: 0x000051EF
		public virtual T Visit(StarToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x00006FEF File Offset: 0x000051EF
		public virtual T Visit(UnaryOperatorToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x00006FEF File Offset: 0x000051EF
		public virtual T Visit(FunctionParameterToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x00006FEF File Offset: 0x000051EF
		public virtual T Visit(ComputeToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x00006FEF File Offset: 0x000051EF
		public virtual T Visit(ComputeExpressionToken tokenIn)
		{
			throw new NotImplementedException();
		}
	}
}
