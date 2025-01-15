using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.OData.Core.UriParser.Visitors
{
	// Token: 0x0200029A RID: 666
	internal sealed class SplitEndingSegmentOfTypeHandler<T> : PathSegmentHandler where T : ODataPathSegment
	{
		// Token: 0x060016D1 RID: 5841 RVA: 0x0004EB30 File Offset: 0x0004CD30
		public SplitEndingSegmentOfTypeHandler()
		{
			this.first = new Queue<ODataPathSegment>();
			this.last = new Queue<ODataPathSegment>();
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060016D2 RID: 5842 RVA: 0x0004EB4E File Offset: 0x0004CD4E
		public ODataPath FirstPart
		{
			get
			{
				return new ODataPath(this.first);
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060016D3 RID: 5843 RVA: 0x0004EB5B File Offset: 0x0004CD5B
		public ODataPath LastPart
		{
			get
			{
				return new ODataPath(this.last);
			}
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x0004EB68 File Offset: 0x0004CD68
		public override void Handle(TypeSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x0004EB71 File Offset: 0x0004CD71
		public override void Handle(NavigationPropertySegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x0004EB7A File Offset: 0x0004CD7A
		public override void Handle(EntitySetSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x0004EB83 File Offset: 0x0004CD83
		public override void Handle(SingletonSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x0004EB8C File Offset: 0x0004CD8C
		public override void Handle(KeySegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x0004EB95 File Offset: 0x0004CD95
		public override void Handle(PropertySegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x0004EB9E File Offset: 0x0004CD9E
		public override void Handle(OperationImportSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x0004EBA7 File Offset: 0x0004CDA7
		public override void Handle(OperationSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x0004EBB0 File Offset: 0x0004CDB0
		public override void Handle(OpenPropertySegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x0004EBB9 File Offset: 0x0004CDB9
		public override void Handle(CountSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x0004EBC2 File Offset: 0x0004CDC2
		public override void Handle(NavigationPropertyLinkSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x0004EBCB File Offset: 0x0004CDCB
		public override void Handle(ValueSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x0004EBD4 File Offset: 0x0004CDD4
		public override void Handle(BatchSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x0004EBDD File Offset: 0x0004CDDD
		public override void Handle(BatchReferenceSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x0004EBE6 File Offset: 0x0004CDE6
		public override void Handle(MetadataSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x0004EBF0 File Offset: 0x0004CDF0
		private void CommonHandler(ODataPathSegment segment)
		{
			if (segment is T)
			{
				this.last.Enqueue(segment);
				return;
			}
			while (Enumerable.Any<ODataPathSegment>(this.last))
			{
				this.first.Enqueue(this.last.Dequeue());
			}
			this.first.Enqueue(segment);
		}

		// Token: 0x04000A05 RID: 2565
		private readonly Queue<ODataPathSegment> first;

		// Token: 0x04000A06 RID: 2566
		private readonly Queue<ODataPathSegment> last;
	}
}
