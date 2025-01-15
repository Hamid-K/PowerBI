using System;
using Microsoft.OData.Core.UriParser.Aggregation;
using Microsoft.OData.Core.UriParser.Syntactic;

namespace Microsoft.OData.Core.UriParser.Visitors
{
	// Token: 0x0200029B RID: 667
	internal abstract class SyntacticTreeVisitor<T> : ISyntacticTreeVisitor<T>
	{
		// Token: 0x060016E4 RID: 5860 RVA: 0x0004EC41 File Offset: 0x0004CE41
		public virtual T Visit(AllToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x0004EC48 File Offset: 0x0004CE48
		public virtual T Visit(AnyToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x0004EC4F File Offset: 0x0004CE4F
		public virtual T Visit(BinaryOperatorToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x0004EC56 File Offset: 0x0004CE56
		public virtual T Visit(DottedIdentifierToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x0004EC5D File Offset: 0x0004CE5D
		public virtual T Visit(ExpandToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x0004EC64 File Offset: 0x0004CE64
		public virtual T Visit(ExpandTermToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x0004EC6B File Offset: 0x0004CE6B
		public virtual T Visit(FunctionCallToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x0004EC72 File Offset: 0x0004CE72
		public virtual T Visit(LiteralToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x0004EC79 File Offset: 0x0004CE79
		public virtual T Visit(AggregateToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x0004EC80 File Offset: 0x0004CE80
		public virtual T Visit(GroupByToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x0004EC87 File Offset: 0x0004CE87
		public virtual T Visit(AggregateExpressionToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x0004EC8E File Offset: 0x0004CE8E
		public virtual T Visit(LambdaToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x0004EC95 File Offset: 0x0004CE95
		public virtual T Visit(InnerPathToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x0004EC9C File Offset: 0x0004CE9C
		public virtual T Visit(OrderByToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x0004ECA3 File Offset: 0x0004CEA3
		public virtual T Visit(EndPathToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x0004ECAA File Offset: 0x0004CEAA
		public virtual T Visit(CustomQueryOptionToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x0004ECB1 File Offset: 0x0004CEB1
		public virtual T Visit(RangeVariableToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x0004ECB8 File Offset: 0x0004CEB8
		public virtual T Visit(SelectToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x0004ECBF File Offset: 0x0004CEBF
		public virtual T Visit(StarToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x0004ECC6 File Offset: 0x0004CEC6
		public virtual T Visit(UnaryOperatorToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x0004ECCD File Offset: 0x0004CECD
		public virtual T Visit(FunctionParameterToken tokenIn)
		{
			throw new NotImplementedException();
		}
	}
}
