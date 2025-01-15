using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003CC RID: 972
	[DomName("HTMLLinkElement")]
	public interface IHtmlLinkElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, ILinkStyle, ILinkImport, ILoadableElement
	{
		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06001EEF RID: 7919
		// (set) Token: 0x06001EF0 RID: 7920
		[DomName("disabled")]
		bool IsDisabled { get; set; }

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06001EF1 RID: 7921
		// (set) Token: 0x06001EF2 RID: 7922
		[DomName("href")]
		string Href { get; set; }

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06001EF3 RID: 7923
		// (set) Token: 0x06001EF4 RID: 7924
		[DomName("rel")]
		string Relation { get; set; }

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06001EF5 RID: 7925
		[DomName("relList")]
		ITokenList RelationList { get; }

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06001EF6 RID: 7926
		// (set) Token: 0x06001EF7 RID: 7927
		[DomName("media")]
		string Media { get; set; }

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06001EF8 RID: 7928
		// (set) Token: 0x06001EF9 RID: 7929
		[DomName("hreflang")]
		string TargetLanguage { get; set; }

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06001EFA RID: 7930
		// (set) Token: 0x06001EFB RID: 7931
		[DomName("type")]
		string Type { get; set; }

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06001EFC RID: 7932
		[DomName("sizes")]
		ISettableTokenList Sizes { get; }

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06001EFD RID: 7933
		// (set) Token: 0x06001EFE RID: 7934
		[DomName("integrity")]
		string Integrity { get; set; }

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06001EFF RID: 7935
		// (set) Token: 0x06001F00 RID: 7936
		[DomName("crossOrigin")]
		string CrossOrigin { get; set; }
	}
}
