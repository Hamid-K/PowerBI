using System;
using AngleSharp.Dom.Css;
using AngleSharp.Html;

namespace AngleSharp.Dom.Svg
{
	// Token: 0x020001B4 RID: 436
	internal sealed class SvgTitleElement : SvgElement, ISvgTitleElement, ISvgElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle
	{
		// Token: 0x06000F2B RID: 3883 RVA: 0x00046E59 File Offset: 0x00045059
		public SvgTitleElement(Document owner, string prefix = null)
			: base(owner, TagNames.Title, prefix, NodeFlags.Special | NodeFlags.Scoped | NodeFlags.HtmlTip)
		{
		}
	}
}
