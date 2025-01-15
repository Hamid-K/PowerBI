using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200019A RID: 410
	internal sealed class SplitEndingSegmentOfTypeHandler<T> : PathSegmentHandler where T : ODataPathSegment
	{
		// Token: 0x06001092 RID: 4242 RVA: 0x0002DD8D File Offset: 0x0002BF8D
		public SplitEndingSegmentOfTypeHandler()
		{
			this.first = new Queue<ODataPathSegment>();
			this.last = new Queue<ODataPathSegment>();
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001093 RID: 4243 RVA: 0x0002DDAB File Offset: 0x0002BFAB
		public ODataPath FirstPart
		{
			get
			{
				return new ODataPath(this.first);
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x0002DDB8 File Offset: 0x0002BFB8
		public ODataPath LastPart
		{
			get
			{
				return new ODataPath(this.last);
			}
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x0002DDC5 File Offset: 0x0002BFC5
		public override void Handle(TypeSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x0002DDC5 File Offset: 0x0002BFC5
		public override void Handle(NavigationPropertySegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x0002DDC5 File Offset: 0x0002BFC5
		public override void Handle(EntitySetSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x0002DDC5 File Offset: 0x0002BFC5
		public override void Handle(SingletonSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0002DDC5 File Offset: 0x0002BFC5
		public override void Handle(KeySegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0002DDC5 File Offset: 0x0002BFC5
		public override void Handle(PropertySegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x0002DDC5 File Offset: 0x0002BFC5
		public override void Handle(OperationImportSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x0002DDC5 File Offset: 0x0002BFC5
		public override void Handle(OperationSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x0002DDC5 File Offset: 0x0002BFC5
		public override void Handle(DynamicPathSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x0002DDC5 File Offset: 0x0002BFC5
		public override void Handle(CountSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x0002DDC5 File Offset: 0x0002BFC5
		public override void Handle(NavigationPropertyLinkSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x0002DDC5 File Offset: 0x0002BFC5
		public override void Handle(ValueSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x0002DDC5 File Offset: 0x0002BFC5
		public override void Handle(BatchSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x0002DDC5 File Offset: 0x0002BFC5
		public override void Handle(BatchReferenceSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0002DDC5 File Offset: 0x0002BFC5
		public override void Handle(MetadataSegment segment)
		{
			this.CommonHandler(segment);
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x0002DDD0 File Offset: 0x0002BFD0
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

		// Token: 0x040008AD RID: 2221
		private readonly Queue<ODataPathSegment> first;

		// Token: 0x040008AE RID: 2222
		private readonly Queue<ODataPathSegment> last;
	}
}
