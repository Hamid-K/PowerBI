using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003AE RID: 942
	[DomName("HTMLAnchorElement")]
	public interface IHtmlAnchorElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, IUrlUtilities
	{
		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06001DBD RID: 7613
		// (set) Token: 0x06001DBE RID: 7614
		[DomName("target")]
		string Target { get; set; }

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06001DBF RID: 7615
		// (set) Token: 0x06001DC0 RID: 7616
		[DomName("download")]
		string Download { get; set; }

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06001DC1 RID: 7617
		[DomName("ping")]
		ISettableTokenList Ping { get; }

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06001DC2 RID: 7618
		// (set) Token: 0x06001DC3 RID: 7619
		[DomName("rel")]
		string Relation { get; set; }

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06001DC4 RID: 7620
		[DomName("relList")]
		ITokenList RelationList { get; }

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06001DC5 RID: 7621
		// (set) Token: 0x06001DC6 RID: 7622
		[DomName("hreflang")]
		string TargetLanguage { get; set; }

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06001DC7 RID: 7623
		[DomName("type")]
		string Type { get; }

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06001DC8 RID: 7624
		[DomName("text")]
		string Text { get; }
	}
}
