using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing.Template
{
	// Token: 0x02000088 RID: 136
	public class ODataPathSegmentTemplateTranslator : PathSegmentTranslator<ODataPathSegmentTemplate>
	{
		// Token: 0x060004E4 RID: 1252 RVA: 0x0000FFA8 File Offset: 0x0000E1A8
		public override ODataPathSegmentTemplate Translate(TypeSegment segment)
		{
			return new TypeSegmentTemplate(segment);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000FFB0 File Offset: 0x0000E1B0
		public override ODataPathSegmentTemplate Translate(NavigationPropertySegment segment)
		{
			return new NavigationPropertySegmentTemplate(segment);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000FFB8 File Offset: 0x0000E1B8
		public override ODataPathSegmentTemplate Translate(EntitySetSegment segment)
		{
			return new EntitySetSegmentTemplate(segment);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000FFC0 File Offset: 0x0000E1C0
		public override ODataPathSegmentTemplate Translate(SingletonSegment segment)
		{
			return new SingletonSegmentTemplate(segment);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000FFC8 File Offset: 0x0000E1C8
		public override ODataPathSegmentTemplate Translate(KeySegment segment)
		{
			return new KeySegmentTemplate(segment);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0000FFD0 File Offset: 0x0000E1D0
		public override ODataPathSegmentTemplate Translate(PropertySegment segment)
		{
			return new PropertySegmentTemplate(segment);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0000FFD8 File Offset: 0x0000E1D8
		public override ODataPathSegmentTemplate Translate(PathTemplateSegment segment)
		{
			return new PathTemplateSegmentTemplate(segment);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0000FFE0 File Offset: 0x0000E1E0
		public override ODataPathSegmentTemplate Translate(OperationImportSegment segment)
		{
			return new OperationImportSegmentTemplate(segment);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000FFE8 File Offset: 0x0000E1E8
		public override ODataPathSegmentTemplate Translate(OperationSegment segment)
		{
			return new OperationSegmentTemplate(segment);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0000FFF0 File Offset: 0x0000E1F0
		public override ODataPathSegmentTemplate Translate(DynamicPathSegment segment)
		{
			return new DynamicSegmentTemplate(segment);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000FFF8 File Offset: 0x0000E1F8
		public override ODataPathSegmentTemplate Translate(NavigationPropertyLinkSegment segment)
		{
			return new NavigationPropertyLinkSegmentTemplate(segment);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00010000 File Offset: 0x0000E200
		public override ODataPathSegmentTemplate Translate(CountSegment segment)
		{
			return new ODataPathSegmentTemplate<CountSegment>();
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00010007 File Offset: 0x0000E207
		public override ODataPathSegmentTemplate Translate(ValueSegment segment)
		{
			return new ODataPathSegmentTemplate<ValueSegment>();
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0001000E File Offset: 0x0000E20E
		public override ODataPathSegmentTemplate Translate(BatchSegment segment)
		{
			return new ODataPathSegmentTemplate<BatchSegment>();
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00010015 File Offset: 0x0000E215
		public override ODataPathSegmentTemplate Translate(MetadataSegment segment)
		{
			return new ODataPathSegmentTemplate<MetadataSegment>();
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001001C File Offset: 0x0000E21C
		public override ODataPathSegmentTemplate Translate(BatchReferenceSegment segment)
		{
			throw new ODataException(Error.Format(SRResources.TargetKindNotImplemented, new object[] { "ODataPathSegment", "BatchReferenceSegment" }));
		}
	}
}
