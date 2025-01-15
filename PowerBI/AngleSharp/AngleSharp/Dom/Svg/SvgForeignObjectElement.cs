using System;
using AngleSharp.Dom.Css;
using AngleSharp.Html;

namespace AngleSharp.Dom.Svg
{
	// Token: 0x020001B2 RID: 434
	internal sealed class SvgForeignObjectElement : SvgElement, ISvgForeignObjectElement, ISvgElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle
	{
		// Token: 0x06000F29 RID: 3881 RVA: 0x00046E35 File Offset: 0x00045035
		public SvgForeignObjectElement(Document owner, string prefix = null)
			: base(owner, TagNames.ForeignObject, prefix, NodeFlags.Special | NodeFlags.Scoped | NodeFlags.HtmlTip)
		{
		}
	}
}
