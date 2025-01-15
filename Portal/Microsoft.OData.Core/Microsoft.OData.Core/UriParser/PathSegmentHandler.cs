using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001DE RID: 478
	public abstract class PathSegmentHandler
	{
		// Token: 0x06001574 RID: 5492 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void Handle(ODataPathSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void Handle(TypeSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void Handle(NavigationPropertySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void Handle(EntitySetSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void Handle(SingletonSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void Handle(KeySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void Handle(PropertySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void Handle(AnnotationSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void Handle(OperationImportSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void Handle(OperationSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void Handle(DynamicPathSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void Handle(CountSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void Handle(FilterSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void Handle(ReferenceSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void Handle(EachSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void Handle(NavigationPropertyLinkSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void Handle(ValueSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void Handle(BatchSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void Handle(BatchReferenceSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void Handle(MetadataSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void Handle(PathTemplateSegment segment)
		{
			throw new NotImplementedException();
		}
	}
}
