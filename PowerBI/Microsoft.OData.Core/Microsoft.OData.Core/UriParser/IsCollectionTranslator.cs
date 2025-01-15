using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001DD RID: 477
	internal sealed class IsCollectionTranslator : PathSegmentTranslator<bool>
	{
		// Token: 0x06001563 RID: 5475 RVA: 0x0003DC50 File Offset: 0x0003BE50
		public override bool Translate(NavigationPropertySegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<NavigationPropertySegment>(segment, "segment");
			return segment.NavigationProperty.Type.IsCollection();
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x0003DC6E File Offset: 0x0003BE6E
		public override bool Translate(EntitySetSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<EntitySetSegment>(segment, "segment");
			return true;
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x0003DC7D File Offset: 0x0003BE7D
		public override bool Translate(KeySegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<KeySegment>(segment, "segment");
			return false;
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x0003DC8C File Offset: 0x0003BE8C
		public override bool Translate(PropertySegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<PropertySegment>(segment, "segment");
			return false;
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x0003DC9B File Offset: 0x0003BE9B
		public override bool Translate(AnnotationSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<AnnotationSegment>(segment, "segment");
			return false;
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x0003DCAA File Offset: 0x0003BEAA
		public override bool Translate(DynamicPathSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<DynamicPathSegment>(segment, "segment");
			return false;
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x0003DCB9 File Offset: 0x0003BEB9
		public override bool Translate(CountSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<CountSegment>(segment, "segment");
			return false;
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x0003DCC8 File Offset: 0x0003BEC8
		public override bool Translate(FilterSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<FilterSegment>(segment, "segment");
			return true;
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x0003DCD7 File Offset: 0x0003BED7
		public override bool Translate(ReferenceSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<ReferenceSegment>(segment, "segment");
			return !segment.SingleResult;
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x0003DCEE File Offset: 0x0003BEEE
		public override bool Translate(EachSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<EachSegment>(segment, "segment");
			return false;
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x0003DCFD File Offset: 0x0003BEFD
		public override bool Translate(NavigationPropertyLinkSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<NavigationPropertyLinkSegment>(segment, "segment");
			return false;
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x0003DD0C File Offset: 0x0003BF0C
		public override bool Translate(BatchSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<BatchSegment>(segment, "segment");
			return false;
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x0003DD1B File Offset: 0x0003BF1B
		public override bool Translate(BatchReferenceSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<BatchReferenceSegment>(segment, "segment");
			return false;
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x0003DD2A File Offset: 0x0003BF2A
		public override bool Translate(ValueSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<ValueSegment>(segment, "segment");
			throw new NotImplementedException(segment.ToString());
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x0003DD43 File Offset: 0x0003BF43
		public override bool Translate(MetadataSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<MetadataSegment>(segment, "segment");
			return false;
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x0003DD52 File Offset: 0x0003BF52
		public override bool Translate(PathTemplateSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<PathTemplateSegment>(segment, "segment");
			return !segment.SingleResult;
		}
	}
}
