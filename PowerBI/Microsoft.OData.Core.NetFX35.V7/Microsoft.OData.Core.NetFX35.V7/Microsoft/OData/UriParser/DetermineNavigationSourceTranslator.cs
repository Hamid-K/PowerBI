using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000134 RID: 308
	internal sealed class DetermineNavigationSourceTranslator : PathSegmentTranslator<IEdmNavigationSource>
	{
		// Token: 0x06000DE4 RID: 3556 RVA: 0x000290ED File Offset: 0x000272ED
		public override IEdmNavigationSource Translate(NavigationPropertyLinkSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<NavigationPropertyLinkSegment>(segment, "segment");
			return segment.NavigationSource;
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x00029101 File Offset: 0x00027301
		public override IEdmNavigationSource Translate(TypeSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<TypeSegment>(segment, "segment");
			return segment.NavigationSource;
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x00029115 File Offset: 0x00027315
		public override IEdmNavigationSource Translate(NavigationPropertySegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<NavigationPropertySegment>(segment, "segment");
			return segment.NavigationSource;
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x00029129 File Offset: 0x00027329
		public override IEdmNavigationSource Translate(EntitySetSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<EntitySetSegment>(segment, "segment");
			return segment.EntitySet;
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x0002913D File Offset: 0x0002733D
		public override IEdmNavigationSource Translate(SingletonSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<SingletonSegment>(segment, "segment");
			return segment.Singleton;
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x00029151 File Offset: 0x00027351
		public override IEdmNavigationSource Translate(KeySegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<KeySegment>(segment, "segment");
			return segment.NavigationSource;
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00029165 File Offset: 0x00027365
		public override IEdmNavigationSource Translate(PropertySegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<PropertySegment>(segment, "segment");
			if (segment.EdmType.AsElementType() is IEdmComplexType)
			{
				return segment.TargetEdmNavigationSource;
			}
			return null;
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0002918D File Offset: 0x0002738D
		public override IEdmNavigationSource Translate(OperationImportSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<OperationImportSegment>(segment, "segment");
			return segment.EntitySet;
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x000291A1 File Offset: 0x000273A1
		public override IEdmNavigationSource Translate(OperationSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<OperationSegment>(segment, "segment");
			return segment.EntitySet;
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x000291B5 File Offset: 0x000273B5
		public override IEdmNavigationSource Translate(CountSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<CountSegment>(segment, "segment");
			return null;
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x000291C4 File Offset: 0x000273C4
		public override IEdmNavigationSource Translate(DynamicPathSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<DynamicPathSegment>(segment, "segment");
			return null;
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x000291D3 File Offset: 0x000273D3
		public override IEdmNavigationSource Translate(ValueSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<ValueSegment>(segment, "segment");
			return null;
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x000291E2 File Offset: 0x000273E2
		public override IEdmNavigationSource Translate(BatchSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<BatchSegment>(segment, "segment");
			return null;
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x000291F1 File Offset: 0x000273F1
		public override IEdmNavigationSource Translate(BatchReferenceSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<BatchReferenceSegment>(segment, "segment");
			return segment.EntitySet;
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00029205 File Offset: 0x00027405
		public override IEdmNavigationSource Translate(MetadataSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<MetadataSegment>(segment, "segment");
			return null;
		}
	}
}
