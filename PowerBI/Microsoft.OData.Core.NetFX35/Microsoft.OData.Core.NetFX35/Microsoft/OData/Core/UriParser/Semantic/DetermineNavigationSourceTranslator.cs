using System;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000232 RID: 562
	internal sealed class DetermineNavigationSourceTranslator : PathSegmentTranslator<IEdmNavigationSource>
	{
		// Token: 0x06001434 RID: 5172 RVA: 0x000493D6 File Offset: 0x000475D6
		public override IEdmNavigationSource Translate(NavigationPropertyLinkSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<NavigationPropertyLinkSegment>(segment, "segment");
			return segment.NavigationSource;
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x000493E9 File Offset: 0x000475E9
		public override IEdmNavigationSource Translate(TypeSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<TypeSegment>(segment, "segment");
			return segment.NavigationSource;
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x000493FC File Offset: 0x000475FC
		public override IEdmNavigationSource Translate(NavigationPropertySegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<NavigationPropertySegment>(segment, "segment");
			return segment.NavigationSource;
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x0004940F File Offset: 0x0004760F
		public override IEdmNavigationSource Translate(EntitySetSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<EntitySetSegment>(segment, "segment");
			return segment.EntitySet;
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x00049422 File Offset: 0x00047622
		public override IEdmNavigationSource Translate(SingletonSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<SingletonSegment>(segment, "segment");
			return segment.Singleton;
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x00049435 File Offset: 0x00047635
		public override IEdmNavigationSource Translate(KeySegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<KeySegment>(segment, "segment");
			return segment.NavigationSource;
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x00049448 File Offset: 0x00047648
		public override IEdmNavigationSource Translate(PropertySegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<PropertySegment>(segment, "segment");
			return null;
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x00049456 File Offset: 0x00047656
		public override IEdmNavigationSource Translate(OperationImportSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<OperationImportSegment>(segment, "segment");
			return segment.EntitySet;
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x00049469 File Offset: 0x00047669
		public override IEdmNavigationSource Translate(OperationSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<OperationSegment>(segment, "segment");
			return segment.EntitySet;
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x0004947C File Offset: 0x0004767C
		public override IEdmNavigationSource Translate(CountSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<CountSegment>(segment, "segment");
			return null;
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x0004948A File Offset: 0x0004768A
		public override IEdmNavigationSource Translate(OpenPropertySegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<OpenPropertySegment>(segment, "segment");
			return null;
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x00049498 File Offset: 0x00047698
		public override IEdmNavigationSource Translate(ValueSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<ValueSegment>(segment, "segment");
			return null;
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x000494A6 File Offset: 0x000476A6
		public override IEdmNavigationSource Translate(BatchSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<BatchSegment>(segment, "segment");
			return null;
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x000494B4 File Offset: 0x000476B4
		public override IEdmNavigationSource Translate(BatchReferenceSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<BatchReferenceSegment>(segment, "segment");
			return segment.EntitySet;
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x000494C7 File Offset: 0x000476C7
		public override IEdmNavigationSource Translate(MetadataSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<MetadataSegment>(segment, "segment");
			return null;
		}
	}
}
