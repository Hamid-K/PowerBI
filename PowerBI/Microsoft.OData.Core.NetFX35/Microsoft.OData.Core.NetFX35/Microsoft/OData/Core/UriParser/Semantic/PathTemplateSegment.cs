using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000258 RID: 600
	public sealed class PathTemplateSegment : ODataPathSegment
	{
		// Token: 0x0600153D RID: 5437 RVA: 0x0004AF4E File Offset: 0x0004914E
		public PathTemplateSegment(string literalText)
		{
			this.LiteralText = literalText;
			base.Identifier = literalText;
			base.SingleResult = true;
			base.TargetKind = RequestTargetKind.Nothing;
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x0004AF72 File Offset: 0x00049172
		// (set) Token: 0x0600153F RID: 5439 RVA: 0x0004AF7A File Offset: 0x0004917A
		public string LiteralText { get; private set; }

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001540 RID: 5440 RVA: 0x0004AF83 File Offset: 0x00049183
		public override IEdmType EdmType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x0004AF86 File Offset: 0x00049186
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x0004AF9A File Offset: 0x0004919A
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}
	}
}
