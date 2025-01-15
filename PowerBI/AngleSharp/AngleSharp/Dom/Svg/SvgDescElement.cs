using System;
using AngleSharp.Dom.Css;
using AngleSharp.Html;

namespace AngleSharp.Dom.Svg
{
	// Token: 0x020001AF RID: 431
	internal sealed class SvgDescElement : SvgElement, ISvgDescriptionElement, ISvgElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle
	{
		// Token: 0x06000F1D RID: 3869 RVA: 0x00046C72 File Offset: 0x00044E72
		public SvgDescElement(Document owner, string prefix = null)
			: base(owner, TagNames.Desc, prefix, NodeFlags.Special | NodeFlags.Scoped | NodeFlags.HtmlTip)
		{
		}
	}
}
