using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001E1 RID: 481
	internal sealed class PathSegmentToStringTranslator : PathSegmentTranslator<string>
	{
		// Token: 0x060015B5 RID: 5557 RVA: 0x0003E1D5 File Offset: 0x0003C3D5
		private PathSegmentToStringTranslator()
		{
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x0003E1DD File Offset: 0x0003C3DD
		public override string Translate(TypeSegment segment)
		{
			return segment.EdmType.FullTypeName();
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x0003E1EA File Offset: 0x0003C3EA
		public override string Translate(NavigationPropertySegment segment)
		{
			return segment.NavigationProperty.Name;
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x0003E1F7 File Offset: 0x0003C3F7
		public override string Translate(PropertySegment segment)
		{
			return segment.Property.Name;
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x0003E204 File Offset: 0x0003C404
		public override string Translate(AnnotationSegment segment)
		{
			return segment.Term.FullName();
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x0003E211 File Offset: 0x0003C411
		public override string Translate(OperationSegment segment)
		{
			return segment.Operations.OperationGroupFullName();
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x0003E21E File Offset: 0x0003C41E
		public override string Translate(OperationImportSegment segment)
		{
			return segment.OperationImports.OperationImportGroupFullName();
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x0003E22B File Offset: 0x0003C42B
		public override string Translate(DynamicPathSegment segment)
		{
			return segment.Identifier;
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x0003E233 File Offset: 0x0003C433
		public override string Translate(FilterSegment segment)
		{
			return segment.LiteralText;
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x0003E22B File Offset: 0x0003C42B
		public override string Translate(ReferenceSegment segment)
		{
			return segment.Identifier;
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x0003E22B File Offset: 0x0003C42B
		public override string Translate(EachSegment segment)
		{
			return segment.Identifier;
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x0003E23B File Offset: 0x0003C43B
		public override string Translate(PathTemplateSegment segment)
		{
			return segment.LiteralText;
		}

		// Token: 0x04000A05 RID: 2565
		internal static readonly PathSegmentToStringTranslator Instance = new PathSegmentToStringTranslator();
	}
}
