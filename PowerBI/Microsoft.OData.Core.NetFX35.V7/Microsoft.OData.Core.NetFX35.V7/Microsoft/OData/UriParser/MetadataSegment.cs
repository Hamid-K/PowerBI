using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200013E RID: 318
	public sealed class MetadataSegment : ODataPathSegment
	{
		// Token: 0x06000E3C RID: 3644 RVA: 0x00029790 File Offset: 0x00027990
		private MetadataSegment()
		{
			base.Identifier = "$metadata";
			base.TargetKind = RequestTargetKind.Metadata;
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000E3D RID: 3645 RVA: 0x0000B41B File Offset: 0x0000961B
		public override IEdmType EdmType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x000297AA File Offset: 0x000279AA
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x000297BF File Offset: 0x000279BF
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x000297D4 File Offset: 0x000279D4
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			return other is MetadataSegment;
		}

		// Token: 0x0400076B RID: 1899
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "MetadataSegment is immutable")]
		public static readonly MetadataSegment Instance = new MetadataSegment();
	}
}
