using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x02000097 RID: 151
	internal abstract class SyntacticTreeVisitor<T> : ISyntacticTreeVisitor<T>
	{
		// Token: 0x0600038D RID: 909 RVA: 0x0000BB5A File Offset: 0x00009D5A
		public virtual T Visit(AllToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000BB61 File Offset: 0x00009D61
		public virtual T Visit(AnyToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000BB68 File Offset: 0x00009D68
		public virtual T Visit(BinaryOperatorToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000BB6F File Offset: 0x00009D6F
		public virtual T Visit(DottedIdentifierToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000BB76 File Offset: 0x00009D76
		public virtual T Visit(ExpandToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000BB7D File Offset: 0x00009D7D
		public virtual T Visit(ExpandTermToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000BB84 File Offset: 0x00009D84
		public virtual T Visit(FunctionCallToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000BB8B File Offset: 0x00009D8B
		public virtual T Visit(LiteralToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000BB92 File Offset: 0x00009D92
		public virtual T Visit(LambdaToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000BB99 File Offset: 0x00009D99
		public virtual T Visit(InnerPathToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000BBA0 File Offset: 0x00009DA0
		public virtual T Visit(OrderByToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000BBA7 File Offset: 0x00009DA7
		public virtual T Visit(EndPathToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000BBAE File Offset: 0x00009DAE
		public virtual T Visit(CustomQueryOptionToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000BBB5 File Offset: 0x00009DB5
		public virtual T Visit(RangeVariableToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000BBBC File Offset: 0x00009DBC
		public virtual T Visit(SelectToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000BBC3 File Offset: 0x00009DC3
		public virtual T Visit(StarToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000BBCA File Offset: 0x00009DCA
		public virtual T Visit(UnaryOperatorToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000BBD1 File Offset: 0x00009DD1
		public virtual T Visit(FunctionParameterToken tokenIn)
		{
			throw new NotImplementedException();
		}
	}
}
