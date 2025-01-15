using System;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000090 RID: 144
	public abstract class QueryNodeVisitor<T>
	{
		// Token: 0x06000351 RID: 849 RVA: 0x0000B840 File Offset: 0x00009A40
		public virtual T Visit(AllNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000B847 File Offset: 0x00009A47
		public virtual T Visit(AnyNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000B84E File Offset: 0x00009A4E
		public virtual T Visit(BinaryOperatorNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000B855 File Offset: 0x00009A55
		public virtual T Visit(CollectionNavigationNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000B85C File Offset: 0x00009A5C
		public virtual T Visit(CollectionPropertyAccessNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000B863 File Offset: 0x00009A63
		public virtual T Visit(ConstantNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000B86A File Offset: 0x00009A6A
		public virtual T Visit(ConvertNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000B871 File Offset: 0x00009A71
		public virtual T Visit(EntityCollectionCastNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000B878 File Offset: 0x00009A78
		public virtual T Visit(EntityRangeVariableReferenceNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000B87F File Offset: 0x00009A7F
		public virtual T Visit(NonentityRangeVariableReferenceNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000B886 File Offset: 0x00009A86
		public virtual T Visit(SingleEntityCastNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000B88D File Offset: 0x00009A8D
		public virtual T Visit(SingleNavigationNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000B894 File Offset: 0x00009A94
		public virtual T Visit(SingleEntityFunctionCallNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000B89B File Offset: 0x00009A9B
		public virtual T Visit(SingleValueFunctionCallNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000B8A2 File Offset: 0x00009AA2
		public virtual T Visit(EntityCollectionFunctionCallNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000B8A9 File Offset: 0x00009AA9
		public virtual T Visit(CollectionFunctionCallNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000B8B0 File Offset: 0x00009AB0
		public virtual T Visit(SingleValueOpenPropertyAccessNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000B8B7 File Offset: 0x00009AB7
		public virtual T Visit(SingleValuePropertyAccessNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000B8BE File Offset: 0x00009ABE
		public virtual T Visit(UnaryOperatorNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000B8C5 File Offset: 0x00009AC5
		public virtual T Visit(NamedFunctionParameterNode nodeIn)
		{
			throw new NotImplementedException();
		}
	}
}
