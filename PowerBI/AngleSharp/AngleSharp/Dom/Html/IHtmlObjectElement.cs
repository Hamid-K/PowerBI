using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003D5 RID: 981
	[DomName("HTMLObjectElement")]
	public interface IHtmlObjectElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, IValidation, ILoadableElement
	{
		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06001F4D RID: 8013
		// (set) Token: 0x06001F4E RID: 8014
		[DomName("data")]
		string Source { get; set; }

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06001F4F RID: 8015
		// (set) Token: 0x06001F50 RID: 8016
		[DomName("type")]
		string Type { get; set; }

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06001F51 RID: 8017
		// (set) Token: 0x06001F52 RID: 8018
		[DomName("typeMustMatch")]
		bool TypeMustMatch { get; set; }

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06001F53 RID: 8019
		// (set) Token: 0x06001F54 RID: 8020
		[DomName("name")]
		string Name { get; set; }

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06001F55 RID: 8021
		// (set) Token: 0x06001F56 RID: 8022
		[DomName("useMap")]
		string UseMap { get; set; }

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06001F57 RID: 8023
		[DomName("form")]
		IHtmlFormElement Form { get; }

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06001F58 RID: 8024
		// (set) Token: 0x06001F59 RID: 8025
		[DomName("width")]
		int DisplayWidth { get; set; }

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06001F5A RID: 8026
		// (set) Token: 0x06001F5B RID: 8027
		[DomName("height")]
		int DisplayHeight { get; set; }

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06001F5C RID: 8028
		[DomName("contentDocument")]
		IDocument ContentDocument { get; }

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06001F5D RID: 8029
		[DomName("contentWindow")]
		IWindow ContentWindow { get; }
	}
}
