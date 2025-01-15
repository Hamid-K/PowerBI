using System;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001EA RID: 490
	internal abstract class SyntacticTreeVisitor<T> : ISyntacticTreeVisitor<T>
	{
		// Token: 0x06001624 RID: 5668 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(AllToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(AnyToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(BinaryOperatorToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(InToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(DottedIdentifierToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(ExpandToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(ExpandTermToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(FunctionCallToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(LiteralToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(AggregateToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(GroupByToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(AggregateExpressionToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(EntitySetAggregateToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(LambdaToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(InnerPathToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(OrderByToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(EndPathToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(CustomQueryOptionToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(RangeVariableToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(SelectToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(SelectTermToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(StarToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(UnaryOperatorToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(FunctionParameterToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(ComputeToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(ComputeExpressionToken tokenIn)
		{
			throw new NotImplementedException();
		}
	}
}
