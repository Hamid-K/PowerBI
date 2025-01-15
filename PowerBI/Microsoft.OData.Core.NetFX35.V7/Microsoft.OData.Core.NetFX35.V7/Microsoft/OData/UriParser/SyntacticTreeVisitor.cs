using System;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200019B RID: 411
	internal abstract class SyntacticTreeVisitor<T> : ISyntacticTreeVisitor<T>
	{
		// Token: 0x060010A5 RID: 4261 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(AllToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(AnyToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(BinaryOperatorToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(DottedIdentifierToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(ExpandToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(ExpandTermToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(FunctionCallToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(LiteralToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(AggregateToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(GroupByToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(AggregateExpressionToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(LambdaToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(InnerPathToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(OrderByToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(EndPathToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(CustomQueryOptionToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(RangeVariableToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(SelectToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(StarToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(UnaryOperatorToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(FunctionParameterToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(ComputeToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(ComputeExpressionToken tokenIn)
		{
			throw new NotImplementedException();
		}
	}
}
