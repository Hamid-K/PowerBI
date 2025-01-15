using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001E5 RID: 485
	public abstract class PathSegmentTranslator<T>
	{
		// Token: 0x060015CC RID: 5580 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Translate(TypeSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Translate(NavigationPropertySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Translate(EntitySetSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Translate(SingletonSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Translate(KeySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Translate(PropertySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Translate(AnnotationSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Translate(OperationImportSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Translate(OperationSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Translate(DynamicPathSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Translate(CountSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Translate(FilterSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Translate(ReferenceSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Translate(EachSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Translate(NavigationPropertyLinkSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Translate(ValueSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Translate(BatchSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Translate(BatchReferenceSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Translate(MetadataSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Translate(PathTemplateSegment segment)
		{
			throw new NotImplementedException();
		}
	}
}
