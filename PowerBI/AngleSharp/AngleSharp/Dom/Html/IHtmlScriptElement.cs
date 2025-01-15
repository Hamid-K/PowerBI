using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003E0 RID: 992
	[DomName("HTMLScriptElement")]
	public interface IHtmlScriptElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, ILoadableElement
	{
		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06001F92 RID: 8082
		// (set) Token: 0x06001F93 RID: 8083
		[DomName("src")]
		string Source { get; set; }

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06001F94 RID: 8084
		// (set) Token: 0x06001F95 RID: 8085
		[DomName("async")]
		bool IsAsync { get; set; }

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06001F96 RID: 8086
		// (set) Token: 0x06001F97 RID: 8087
		[DomName("defer")]
		bool IsDeferred { get; set; }

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06001F98 RID: 8088
		// (set) Token: 0x06001F99 RID: 8089
		[DomName("type")]
		string Type { get; set; }

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06001F9A RID: 8090
		// (set) Token: 0x06001F9B RID: 8091
		[DomName("charset")]
		string CharacterSet { get; set; }

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06001F9C RID: 8092
		// (set) Token: 0x06001F9D RID: 8093
		[DomName("crossOrigin")]
		string CrossOrigin { get; set; }

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06001F9E RID: 8094
		// (set) Token: 0x06001F9F RID: 8095
		[DomName("text")]
		string Text { get; set; }

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06001FA0 RID: 8096
		// (set) Token: 0x06001FA1 RID: 8097
		[DomName("integrity")]
		string Integrity { get; set; }
	}
}
