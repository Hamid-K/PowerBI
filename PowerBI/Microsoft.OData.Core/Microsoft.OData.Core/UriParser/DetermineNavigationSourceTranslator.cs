using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000179 RID: 377
	internal sealed class DetermineNavigationSourceTranslator : PathSegmentTranslator<IEdmNavigationSource>
	{
		// Token: 0x060012BC RID: 4796 RVA: 0x00038A1D File Offset: 0x00036C1D
		public override IEdmNavigationSource Translate(NavigationPropertyLinkSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<NavigationPropertyLinkSegment>(segment, "segment");
			return segment.NavigationSource;
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x00038A31 File Offset: 0x00036C31
		public override IEdmNavigationSource Translate(TypeSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<TypeSegment>(segment, "segment");
			return segment.NavigationSource;
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x00038A45 File Offset: 0x00036C45
		public override IEdmNavigationSource Translate(NavigationPropertySegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<NavigationPropertySegment>(segment, "segment");
			return segment.NavigationSource;
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x00038A59 File Offset: 0x00036C59
		public override IEdmNavigationSource Translate(EntitySetSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<EntitySetSegment>(segment, "segment");
			return segment.EntitySet;
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x00038A6D File Offset: 0x00036C6D
		public override IEdmNavigationSource Translate(SingletonSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<SingletonSegment>(segment, "segment");
			return segment.Singleton;
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x00038A81 File Offset: 0x00036C81
		public override IEdmNavigationSource Translate(KeySegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<KeySegment>(segment, "segment");
			return segment.NavigationSource;
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x00038A95 File Offset: 0x00036C95
		public override IEdmNavigationSource Translate(PropertySegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<PropertySegment>(segment, "segment");
			if (segment.EdmType.AsElementType() is IEdmComplexType)
			{
				return segment.TargetEdmNavigationSource;
			}
			return null;
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x00038ABD File Offset: 0x00036CBD
		public override IEdmNavigationSource Translate(OperationImportSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<OperationImportSegment>(segment, "segment");
			return segment.EntitySet;
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x00038AD1 File Offset: 0x00036CD1
		public override IEdmNavigationSource Translate(OperationSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<OperationSegment>(segment, "segment");
			return segment.EntitySet;
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x00038AE5 File Offset: 0x00036CE5
		public override IEdmNavigationSource Translate(CountSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<CountSegment>(segment, "segment");
			return null;
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x00038AF4 File Offset: 0x00036CF4
		public override IEdmNavigationSource Translate(FilterSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<FilterSegment>(segment, "segment");
			return segment.TargetEdmNavigationSource;
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x00038B08 File Offset: 0x00036D08
		public override IEdmNavigationSource Translate(ReferenceSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<ReferenceSegment>(segment, "segment");
			return segment.TargetEdmNavigationSource;
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x00038B1C File Offset: 0x00036D1C
		public override IEdmNavigationSource Translate(EachSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<EachSegment>(segment, "segment");
			return segment.TargetEdmNavigationSource;
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x00038B30 File Offset: 0x00036D30
		public override IEdmNavigationSource Translate(DynamicPathSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<DynamicPathSegment>(segment, "segment");
			return null;
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x00038B3F File Offset: 0x00036D3F
		public override IEdmNavigationSource Translate(ValueSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<ValueSegment>(segment, "segment");
			return null;
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x00038B4E File Offset: 0x00036D4E
		public override IEdmNavigationSource Translate(BatchSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<BatchSegment>(segment, "segment");
			return null;
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x00038B5D File Offset: 0x00036D5D
		public override IEdmNavigationSource Translate(BatchReferenceSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<BatchReferenceSegment>(segment, "segment");
			return segment.EntitySet;
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x00038B71 File Offset: 0x00036D71
		public override IEdmNavigationSource Translate(MetadataSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<MetadataSegment>(segment, "segment");
			return null;
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x00038B80 File Offset: 0x00036D80
		public override IEdmNavigationSource Translate(PathTemplateSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<PathTemplateSegment>(segment, "segment");
			return null;
		}
	}
}
