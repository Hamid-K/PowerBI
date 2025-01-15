using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000178 RID: 376
	public sealed class CountSegment : ODataPathSegment
	{
		// Token: 0x060012B6 RID: 4790 RVA: 0x0003899D File Offset: 0x00036B9D
		private CountSegment()
		{
			base.Identifier = "$count";
			base.SingleResult = true;
			base.TargetKind = RequestTargetKind.PrimitiveValue;
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x060012B7 RID: 4791 RVA: 0x000389BE File Offset: 0x00036BBE
		public override IEdmType EdmType
		{
			get
			{
				return EdmCoreModel.Instance.GetInt32(false).Definition;
			}
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x000389D0 File Offset: 0x00036BD0
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x000389E5 File Offset: 0x00036BE5
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x000389FA File Offset: 0x00036BFA
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			return other is CountSegment;
		}

		// Token: 0x0400086E RID: 2158
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "CountSegment is immutable")]
		public static readonly CountSegment Instance = new CountSegment();
	}
}
