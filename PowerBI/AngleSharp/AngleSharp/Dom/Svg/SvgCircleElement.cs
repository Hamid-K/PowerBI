using System;
using AngleSharp.Dom.Css;
using AngleSharp.Html;

namespace AngleSharp.Dom.Svg
{
	// Token: 0x020001AE RID: 430
	internal sealed class SvgCircleElement : SvgElement, ISvgCircleElement, ISvgElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle
	{
		// Token: 0x06000F1C RID: 3868 RVA: 0x00046C62 File Offset: 0x00044E62
		public SvgCircleElement(Document owner, string prefix = null)
			: base(owner, TagNames.Circle, prefix, NodeFlags.None)
		{
		}
	}
}
