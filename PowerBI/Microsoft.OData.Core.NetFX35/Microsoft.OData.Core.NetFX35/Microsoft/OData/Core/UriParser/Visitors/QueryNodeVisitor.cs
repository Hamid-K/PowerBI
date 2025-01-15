using System;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.OData.Core.UriParser.Visitors
{
	// Token: 0x020001BB RID: 443
	public abstract class QueryNodeVisitor<T>
	{
		// Token: 0x06001068 RID: 4200 RVA: 0x0003952B File Offset: 0x0003772B
		public virtual T Visit(AllNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x00039532 File Offset: 0x00037732
		public virtual T Visit(AnyNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x00039539 File Offset: 0x00037739
		public virtual T Visit(BinaryOperatorNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x00039540 File Offset: 0x00037740
		public virtual T Visit(CountNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x00039547 File Offset: 0x00037747
		public virtual T Visit(CollectionNavigationNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0003954E File Offset: 0x0003774E
		public virtual T Visit(CollectionPropertyAccessNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x00039555 File Offset: 0x00037755
		public virtual T Visit(CollectionOpenPropertyAccessNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0003955C File Offset: 0x0003775C
		public virtual T Visit(ConstantNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x00039563 File Offset: 0x00037763
		public virtual T Visit(ConvertNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x0003956A File Offset: 0x0003776A
		public virtual T Visit(EntityCollectionCastNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x00039571 File Offset: 0x00037771
		public virtual T Visit(EntityRangeVariableReferenceNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x00039578 File Offset: 0x00037778
		public virtual T Visit(NonentityRangeVariableReferenceNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x0003957F File Offset: 0x0003777F
		public virtual T Visit(SingleEntityCastNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x00039586 File Offset: 0x00037786
		public virtual T Visit(SingleNavigationNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x0003958D File Offset: 0x0003778D
		public virtual T Visit(SingleEntityFunctionCallNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x00039594 File Offset: 0x00037794
		public virtual T Visit(SingleValueFunctionCallNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0003959B File Offset: 0x0003779B
		public virtual T Visit(EntityCollectionFunctionCallNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x000395A2 File Offset: 0x000377A2
		public virtual T Visit(CollectionFunctionCallNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x000395A9 File Offset: 0x000377A9
		public virtual T Visit(SingleValueOpenPropertyAccessNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x000395B0 File Offset: 0x000377B0
		public virtual T Visit(SingleValuePropertyAccessNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x000395B7 File Offset: 0x000377B7
		public virtual T Visit(UnaryOperatorNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x000395BE File Offset: 0x000377BE
		public virtual T Visit(NamedFunctionParameterNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x000395C5 File Offset: 0x000377C5
		public virtual T Visit(ParameterAliasNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x000395CC File Offset: 0x000377CC
		public virtual T Visit(SearchTermNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x000395D3 File Offset: 0x000377D3
		public virtual T Visit(CollectionPropertyCastNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x000395DA File Offset: 0x000377DA
		public virtual T Visit(SingleValueCastNode nodeIn)
		{
			throw new NotImplementedException();
		}
	}
}
