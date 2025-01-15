using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000133 RID: 307
	public sealed class CountSegment : ODataPathSegment
	{
		// Token: 0x06000DDE RID: 3550 RVA: 0x0002906D File Offset: 0x0002726D
		private CountSegment()
		{
			base.Identifier = "$count";
			base.SingleResult = true;
			base.TargetKind = RequestTargetKind.PrimitiveValue;
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x0002908E File Offset: 0x0002728E
		public override IEdmType EdmType
		{
			get
			{
				return EdmCoreModel.Instance.GetInt32(false).Definition;
			}
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x000290A0 File Offset: 0x000272A0
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x000290B5 File Offset: 0x000272B5
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x000290CA File Offset: 0x000272CA
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			return other is CountSegment;
		}

		// Token: 0x0400074D RID: 1869
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "CountSegment is immutable")]
		public static readonly CountSegment Instance = new CountSegment();
	}
}
