using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200016E RID: 366
	public sealed class BatchSegment : ODataPathSegment
	{
		// Token: 0x0600126A RID: 4714 RVA: 0x00038372 File Offset: 0x00036572
		private BatchSegment()
		{
			base.Identifier = "$batch";
			base.TargetKind = RequestTargetKind.Batch;
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x0000360D File Offset: 0x0000180D
		public override IEdmType EdmType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x0003838D File Offset: 0x0003658D
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x000383A2 File Offset: 0x000365A2
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x000383B7 File Offset: 0x000365B7
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			return other is BatchSegment;
		}

		// Token: 0x04000850 RID: 2128
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "BatchSegment is immutable")]
		public static readonly BatchSegment Instance = new BatchSegment();
	}
}
