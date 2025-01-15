using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;

namespace AngleSharp.Dom.Svg
{
	// Token: 0x020001B8 RID: 440
	[DomName("SVGElement")]
	public interface ISvgElement : IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle
	{
	}
}
