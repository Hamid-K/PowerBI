using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000129 RID: 297
	public sealed class BatchSegment : ODataPathSegment
	{
		// Token: 0x06000D92 RID: 3474 RVA: 0x00028A3E File Offset: 0x00026C3E
		private BatchSegment()
		{
			base.Identifier = "$batch";
			base.TargetKind = RequestTargetKind.Batch;
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x0000B41B File Offset: 0x0000961B
		public override IEdmType EdmType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00028A59 File Offset: 0x00026C59
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00028A6E File Offset: 0x00026C6E
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00028A83 File Offset: 0x00026C83
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			return other is BatchSegment;
		}

		// Token: 0x0400072F RID: 1839
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "BatchSegment is immutable")]
		public static readonly BatchSegment Instance = new BatchSegment();
	}
}
