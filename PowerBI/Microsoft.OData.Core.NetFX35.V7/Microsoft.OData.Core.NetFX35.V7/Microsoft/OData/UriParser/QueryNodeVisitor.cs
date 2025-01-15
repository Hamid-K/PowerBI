using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000196 RID: 406
	public abstract class QueryNodeVisitor<T>
	{
		// Token: 0x06001065 RID: 4197 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(AllNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(AnyNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(BinaryOperatorNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(CountNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(CollectionNavigationNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(CollectionPropertyAccessNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(CollectionOpenPropertyAccessNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(ConstantNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(ConvertNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(CollectionResourceCastNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(ResourceRangeVariableReferenceNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(NonResourceRangeVariableReferenceNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(SingleResourceCastNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(SingleNavigationNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(SingleResourceFunctionCallNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(SingleValueFunctionCallNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(CollectionResourceFunctionCallNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(CollectionFunctionCallNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(SingleValueOpenPropertyAccessNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(SingleValuePropertyAccessNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(UnaryOperatorNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(NamedFunctionParameterNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(ParameterAliasNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(SearchTermNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(SingleComplexNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(CollectionComplexNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(SingleValueCastNode nodeIn)
		{
			throw new NotImplementedException();
		}
	}
}
