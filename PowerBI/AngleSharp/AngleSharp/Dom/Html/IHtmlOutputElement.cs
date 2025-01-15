using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003DA RID: 986
	[DomName("HTMLOutputElement")]
	public interface IHtmlOutputElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, IValidation
	{
		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06001F7D RID: 8061
		[DomName("htmlFor")]
		ISettableTokenList HtmlFor { get; }

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06001F7E RID: 8062
		// (set) Token: 0x06001F7F RID: 8063
		[DomName("defaultValue")]
		string DefaultValue { get; set; }

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06001F80 RID: 8064
		// (set) Token: 0x06001F81 RID: 8065
		[DomName("value")]
		string Value { get; set; }

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06001F82 RID: 8066
		[DomName("labels")]
		INodeList Labels { get; }

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06001F83 RID: 8067
		[DomName("type")]
		string Type { get; }

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06001F84 RID: 8068
		[DomName("form")]
		IHtmlFormElement Form { get; }

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06001F85 RID: 8069
		// (set) Token: 0x06001F86 RID: 8070
		[DomName("name")]
		string Name { get; set; }
	}
}
