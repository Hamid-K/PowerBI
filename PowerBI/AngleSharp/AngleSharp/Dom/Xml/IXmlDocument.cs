using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Xml
{
	// Token: 0x020001AD RID: 429
	[DomName("XMLDocument")]
	public interface IXmlDocument : IDocument, INode, IEventTarget, IMarkupFormattable, IParentNode, IGlobalEventHandlers, IDocumentStyle, INonElementParentNode, IDisposable
	{
	}
}
