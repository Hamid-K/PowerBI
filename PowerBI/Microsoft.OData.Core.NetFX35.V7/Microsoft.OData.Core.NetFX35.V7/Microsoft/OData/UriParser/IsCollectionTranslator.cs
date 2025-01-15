using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200018E RID: 398
	internal sealed class IsCollectionTranslator : PathSegmentTranslator<bool>
	{
		// Token: 0x06001013 RID: 4115 RVA: 0x0002D7B8 File Offset: 0x0002B9B8
		public override bool Translate(NavigationPropertySegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<NavigationPropertySegment>(segment, "segment");
			return segment.NavigationProperty.Type.IsCollection();
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0002D7D6 File Offset: 0x0002B9D6
		public override bool Translate(EntitySetSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<EntitySetSegment>(segment, "segment");
			return true;
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0002D7E5 File Offset: 0x0002B9E5
		public override bool Translate(KeySegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<KeySegment>(segment, "segment");
			return false;
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0002D7F4 File Offset: 0x0002B9F4
		public override bool Translate(PropertySegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<PropertySegment>(segment, "segment");
			return false;
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x0002D803 File Offset: 0x0002BA03
		public override bool Translate(DynamicPathSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<DynamicPathSegment>(segment, "segment");
			return false;
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x0002D812 File Offset: 0x0002BA12
		public override bool Translate(CountSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<CountSegment>(segment, "segment");
			return false;
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x0002D821 File Offset: 0x0002BA21
		public override bool Translate(NavigationPropertyLinkSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<NavigationPropertyLinkSegment>(segment, "segment");
			return false;
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x0002D830 File Offset: 0x0002BA30
		public override bool Translate(BatchSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<BatchSegment>(segment, "segment");
			return false;
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x0002D83F File Offset: 0x0002BA3F
		public override bool Translate(BatchReferenceSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<BatchReferenceSegment>(segment, "segment");
			return false;
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0002D84E File Offset: 0x0002BA4E
		public override bool Translate(ValueSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<ValueSegment>(segment, "segment");
			throw new NotImplementedException(segment.ToString());
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0002D867 File Offset: 0x0002BA67
		public override bool Translate(MetadataSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<MetadataSegment>(segment, "segment");
			return false;
		}
	}
}
