using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003BC RID: 956
	[DomName("HTMLDocument")]
	public interface IHtmlDocument : IDocument, INode, IEventTarget, IMarkupFormattable, IParentNode, IGlobalEventHandlers, IDocumentStyle, INonElementParentNode, IDisposable
	{
	}
}
