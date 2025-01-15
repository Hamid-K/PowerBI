using System;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Visitors
{
	// Token: 0x02000295 RID: 661
	internal sealed class PathSegmentToStringTranslator : PathSegmentTranslator<string>
	{
		// Token: 0x060016B7 RID: 5815 RVA: 0x0004E786 File Offset: 0x0004C986
		private PathSegmentToStringTranslator()
		{
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x0004E78E File Offset: 0x0004C98E
		public override string Translate(TypeSegment segment)
		{
			return segment.EdmType.FullTypeName();
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x0004E79B File Offset: 0x0004C99B
		public override string Translate(NavigationPropertySegment segment)
		{
			return segment.NavigationProperty.Name;
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x0004E7A8 File Offset: 0x0004C9A8
		public override string Translate(PropertySegment segment)
		{
			return segment.Property.Name;
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x0004E7B5 File Offset: 0x0004C9B5
		public override string Translate(OperationSegment segment)
		{
			return segment.Operations.OperationGroupFullName();
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x0004E7C2 File Offset: 0x0004C9C2
		public override string Translate(OperationImportSegment segment)
		{
			return segment.OperationImports.OperationImportGroupFullName();
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x0004E7CF File Offset: 0x0004C9CF
		public override string Translate(OpenPropertySegment segment)
		{
			return segment.PropertyName;
		}

		// Token: 0x040009FF RID: 2559
		internal static readonly PathSegmentToStringTranslator Instance = new PathSegmentToStringTranslator();
	}
}
