using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x0200006E RID: 110
	public sealed class MetadataSegment : ODataPathSegment
	{
		// Token: 0x060002A1 RID: 673 RVA: 0x0000A49F File Offset: 0x0000869F
		private MetadataSegment()
		{
			base.Identifier = "$metadata";
			base.TargetKind = RequestTargetKind.Metadata;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000A4B9 File Offset: 0x000086B9
		public override IEdmType EdmType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000A4BC File Offset: 0x000086BC
		public override T Translate<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000A4D0 File Offset: 0x000086D0
		public override void Handle(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "translator");
			handler.Handle(this);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000A4E4 File Offset: 0x000086E4
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			return other is MetadataSegment;
		}

		// Token: 0x040000B8 RID: 184
		public static readonly MetadataSegment Instance = new MetadataSegment();
	}
}
