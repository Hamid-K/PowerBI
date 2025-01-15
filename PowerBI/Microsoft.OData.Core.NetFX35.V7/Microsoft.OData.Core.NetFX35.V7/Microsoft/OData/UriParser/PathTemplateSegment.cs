using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000151 RID: 337
	public sealed class PathTemplateSegment : ODataPathSegment
	{
		// Token: 0x06000ECF RID: 3791 RVA: 0x0002AC83 File Offset: 0x00028E83
		public PathTemplateSegment(string literalText)
		{
			this.LiteralText = literalText;
			base.Identifier = literalText;
			base.SingleResult = true;
			base.TargetKind = RequestTargetKind.Nothing;
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x0002ACA7 File Offset: 0x00028EA7
		// (set) Token: 0x06000ED1 RID: 3793 RVA: 0x0002ACAF File Offset: 0x00028EAF
		public string LiteralText { get; private set; }

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x0000B41B File Offset: 0x0000961B
		public override IEdmType EdmType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0002ACB8 File Offset: 0x00028EB8
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0002ACCD File Offset: 0x00028ECD
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}
	}
}
