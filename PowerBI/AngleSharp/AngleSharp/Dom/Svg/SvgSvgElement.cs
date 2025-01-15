using System;
using AngleSharp.Dom.Css;
using AngleSharp.Html;

namespace AngleSharp.Dom.Svg
{
	// Token: 0x020001B3 RID: 435
	internal sealed class SvgSvgElement : SvgElement, ISvgSvgElement, ISvgElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle
	{
		// Token: 0x06000F2A RID: 3882 RVA: 0x00046E49 File Offset: 0x00045049
		public SvgSvgElement(Document owner, string prefix = null)
			: base(owner, TagNames.Svg, prefix, NodeFlags.None)
		{
		}
	}
}
