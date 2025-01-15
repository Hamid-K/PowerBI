using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200019D RID: 413
	public sealed class PathTemplateSegment : ODataPathSegment
	{
		// Token: 0x060013F4 RID: 5108 RVA: 0x0003ABCC File Offset: 0x00038DCC
		public PathTemplateSegment(string literalText)
		{
			this.LiteralText = literalText;
			base.Identifier = literalText;
			base.SingleResult = true;
			base.TargetKind = RequestTargetKind.Nothing;
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x0003ABF0 File Offset: 0x00038DF0
		// (set) Token: 0x060013F6 RID: 5110 RVA: 0x0003ABF8 File Offset: 0x00038DF8
		public string LiteralText { get; private set; }

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060013F7 RID: 5111 RVA: 0x0000360D File Offset: 0x0000180D
		public override IEdmType EdmType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x0003AC01 File Offset: 0x00038E01
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x0003AC16 File Offset: 0x00038E16
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}
	}
}
