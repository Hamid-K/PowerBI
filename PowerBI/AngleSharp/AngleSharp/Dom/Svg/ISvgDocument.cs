using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Svg
{
	// Token: 0x020001B7 RID: 439
	[DomName("SVGDocument")]
	public interface ISvgDocument : IDocument, INode, IEventTarget, IMarkupFormattable, IParentNode, IGlobalEventHandlers, IDocumentStyle, INonElementParentNode, IDisposable
	{
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000F2C RID: 3884
		[DomName("rootElement")]
		ISvgSvgElement RootElement { get; }
	}
}
