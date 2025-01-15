using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003CA RID: 970
	[DomName("HTMLLabelElement")]
	public interface IHtmlLabelElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06001EEA RID: 7914
		[DomName("form")]
		IHtmlFormElement Form { get; }

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06001EEB RID: 7915
		// (set) Token: 0x06001EEC RID: 7916
		[DomName("htmlFor")]
		string HtmlFor { get; set; }

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06001EED RID: 7917
		[DomName("control")]
		IHtmlElement Control { get; }
	}
}
