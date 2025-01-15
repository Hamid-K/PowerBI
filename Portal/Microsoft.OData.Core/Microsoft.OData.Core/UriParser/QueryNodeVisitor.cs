using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001E6 RID: 486
	public abstract class QueryNodeVisitor<T>
	{
		// Token: 0x060015E1 RID: 5601 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(AllNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(AnyNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(BinaryOperatorNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(CountNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(CollectionNavigationNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(CollectionPropertyAccessNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(CollectionOpenPropertyAccessNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(ConstantNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(CollectionConstantNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(ConvertNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(CollectionResourceCastNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(ResourceRangeVariableReferenceNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(NonResourceRangeVariableReferenceNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(SingleResourceCastNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(SingleNavigationNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(SingleResourceFunctionCallNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(SingleValueFunctionCallNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(CollectionResourceFunctionCallNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(CollectionFunctionCallNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(SingleValueOpenPropertyAccessNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(SingleValuePropertyAccessNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(UnaryOperatorNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(NamedFunctionParameterNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(ParameterAliasNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(SearchTermNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(SingleComplexNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(CollectionComplexNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(SingleValueCastNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(AggregatedCollectionPropertyNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(InNode nodeIn)
		{
			throw new NotImplementedException();
		}
	}
}
