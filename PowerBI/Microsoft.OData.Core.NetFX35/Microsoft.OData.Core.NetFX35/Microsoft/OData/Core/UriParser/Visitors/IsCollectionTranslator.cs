using System;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Visitors
{
	// Token: 0x02000292 RID: 658
	internal sealed class IsCollectionTranslator : PathSegmentTranslator<bool>
	{
		// Token: 0x06001687 RID: 5767 RVA: 0x0004E4A9 File Offset: 0x0004C6A9
		public override bool Translate(NavigationPropertySegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<NavigationPropertySegment>(segment, "segment");
			return segment.NavigationProperty.Type.IsCollection();
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x0004E4C6 File Offset: 0x0004C6C6
		public override bool Translate(EntitySetSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<EntitySetSegment>(segment, "segment");
			return true;
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x0004E4D4 File Offset: 0x0004C6D4
		public override bool Translate(KeySegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<KeySegment>(segment, "segment");
			return false;
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x0004E4E2 File Offset: 0x0004C6E2
		public override bool Translate(PropertySegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<PropertySegment>(segment, "segment");
			return false;
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x0004E4F0 File Offset: 0x0004C6F0
		public override bool Translate(OpenPropertySegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<OpenPropertySegment>(segment, "segment");
			return false;
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x0004E4FE File Offset: 0x0004C6FE
		public override bool Translate(CountSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<CountSegment>(segment, "segment");
			return false;
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x0004E50C File Offset: 0x0004C70C
		public override bool Translate(NavigationPropertyLinkSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<NavigationPropertyLinkSegment>(segment, "segment");
			return false;
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x0004E51A File Offset: 0x0004C71A
		public override bool Translate(BatchSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<BatchSegment>(segment, "segment");
			return false;
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x0004E528 File Offset: 0x0004C728
		public override bool Translate(BatchReferenceSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<BatchReferenceSegment>(segment, "segment");
			return false;
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x0004E536 File Offset: 0x0004C736
		public override bool Translate(ValueSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<ValueSegment>(segment, "segment");
			throw new NotImplementedException(segment.ToString());
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x0004E54E File Offset: 0x0004C74E
		public override bool Translate(MetadataSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<MetadataSegment>(segment, "segment");
			return false;
		}
	}
}
