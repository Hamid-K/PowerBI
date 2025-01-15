using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001E9 RID: 489
	internal sealed class SplitEndingSegmentOfTypeHandler<T> : PathSegmentHandler where T : ODataPathSegment
	{
		// Token: 0x0600160C RID: 5644 RVA: 0x0003E2BC File Offset: 0x0003C4BC
		public SplitEndingSegmentOfTypeHandler()
		{
			this.first = new Queue<ODataPathSegment>();
			this.last = new Queue<ODataPathSegment>();
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x0003E2DA File Offset: 0x0003C4DA
		public ODataPath FirstPart
		{
			get
			{
				return new ODataPath(this.first);
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x0600160E RID: 5646 RVA: 0x0003E2E7 File Offset: 0x0003C4E7
		public ODataPath LastPart
		{
			get
			{
				return new ODataPath(this.last);
			}
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x0003E2F4 File Offset: 0x0003C4F4
		public override void Handle(TypeSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x0003E2F4 File Offset: 0x0003C4F4
		public override void Handle(NavigationPropertySegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x0003E2F4 File Offset: 0x0003C4F4
		public override void Handle(EntitySetSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x0003E2F4 File Offset: 0x0003C4F4
		public override void Handle(SingletonSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x0003E2F4 File Offset: 0x0003C4F4
		public override void Handle(KeySegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x0003E2F4 File Offset: 0x0003C4F4
		public override void Handle(PropertySegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x0003E2F4 File Offset: 0x0003C4F4
		public override void Handle(AnnotationSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x0003E2F4 File Offset: 0x0003C4F4
		public override void Handle(OperationImportSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x0003E2F4 File Offset: 0x0003C4F4
		public override void Handle(OperationSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x0003E2F4 File Offset: 0x0003C4F4
		public override void Handle(DynamicPathSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x0003E2F4 File Offset: 0x0003C4F4
		public override void Handle(CountSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x0003E2F4 File Offset: 0x0003C4F4
		public override void Handle(FilterSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x0003E2F4 File Offset: 0x0003C4F4
		public override void Handle(EachSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x0003E2F4 File Offset: 0x0003C4F4
		public override void Handle(ReferenceSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x0003E2F4 File Offset: 0x0003C4F4
		public override void Handle(PathTemplateSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x0003E2F4 File Offset: 0x0003C4F4
		public override void Handle(NavigationPropertyLinkSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x0003E2F4 File Offset: 0x0003C4F4
		public override void Handle(ValueSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x0003E2F4 File Offset: 0x0003C4F4
		public override void Handle(BatchSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x0003E2F4 File Offset: 0x0003C4F4
		public override void Handle(BatchReferenceSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x0003E2F4 File Offset: 0x0003C4F4
		public override void Handle(MetadataSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x0003E300 File Offset: 0x0003C500
		private void CommonHandler(ODataPathSegment segment)
		{
			if (segment is T)
			{
				this.last.Enqueue(segment);
				return;
			}
			while (this.last.Any<ODataPathSegment>())
			{
				this.first.Enqueue(this.last.Dequeue());
			}
			this.first.Enqueue(segment);
		}

		// Token: 0x04000A06 RID: 2566
		private readonly Queue<ODataPathSegment> first;

		// Token: 0x04000A07 RID: 2567
		private readonly Queue<ODataPathSegment> last;
	}
}
