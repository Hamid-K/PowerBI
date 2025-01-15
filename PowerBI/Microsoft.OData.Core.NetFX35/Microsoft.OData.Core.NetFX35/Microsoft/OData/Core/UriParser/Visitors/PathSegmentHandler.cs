using System;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.OData.Core.UriParser.Visitors
{
	// Token: 0x02000293 RID: 659
	public abstract class PathSegmentHandler
	{
		// Token: 0x06001693 RID: 5779 RVA: 0x0004E564 File Offset: 0x0004C764
		public virtual void Handle(TypeSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x0004E56B File Offset: 0x0004C76B
		public virtual void Handle(NavigationPropertySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x0004E572 File Offset: 0x0004C772
		public virtual void Handle(EntitySetSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x0004E579 File Offset: 0x0004C779
		public virtual void Handle(SingletonSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x0004E580 File Offset: 0x0004C780
		public virtual void Handle(KeySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x0004E587 File Offset: 0x0004C787
		public virtual void Handle(PropertySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x0004E58E File Offset: 0x0004C78E
		public virtual void Handle(OperationImportSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x0004E595 File Offset: 0x0004C795
		public virtual void Handle(OperationSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x0004E59C File Offset: 0x0004C79C
		public virtual void Handle(OpenPropertySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x0004E5A3 File Offset: 0x0004C7A3
		public virtual void Handle(CountSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x0004E5AA File Offset: 0x0004C7AA
		public virtual void Handle(NavigationPropertyLinkSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x0004E5B1 File Offset: 0x0004C7B1
		public virtual void Handle(ValueSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x0004E5B8 File Offset: 0x0004C7B8
		public virtual void Handle(BatchSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x0004E5BF File Offset: 0x0004C7BF
		public virtual void Handle(BatchReferenceSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x0004E5C6 File Offset: 0x0004C7C6
		public virtual void Handle(MetadataSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x0004E5CD File Offset: 0x0004C7CD
		public virtual void Handle(PathTemplateSegment segment)
		{
			throw new NotImplementedException();
		}
	}
}
