using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003AF RID: 943
	[DomName("HTMLAreaElement")]
	public interface IHtmlAreaElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, IUrlUtilities
	{
		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06001DC9 RID: 7625
		// (set) Token: 0x06001DCA RID: 7626
		[DomName("alt")]
		string AlternativeText { get; set; }

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06001DCB RID: 7627
		// (set) Token: 0x06001DCC RID: 7628
		[DomName("coords")]
		string Coordinates { get; set; }

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06001DCD RID: 7629
		// (set) Token: 0x06001DCE RID: 7630
		[DomName("shape")]
		string Shape { get; set; }

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06001DCF RID: 7631
		// (set) Token: 0x06001DD0 RID: 7632
		[DomName("target")]
		string Target { get; set; }

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06001DD1 RID: 7633
		// (set) Token: 0x06001DD2 RID: 7634
		[DomName("download")]
		string Download { get; set; }

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06001DD3 RID: 7635
		[DomName("ping")]
		ISettableTokenList Ping { get; }

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06001DD4 RID: 7636
		// (set) Token: 0x06001DD5 RID: 7637
		[DomName("rel")]
		string Relation { get; set; }

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06001DD6 RID: 7638
		[DomName("relList")]
		ITokenList RelationList { get; }

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06001DD7 RID: 7639
		// (set) Token: 0x06001DD8 RID: 7640
		[DomName("hreflang")]
		string TargetLanguage { get; set; }

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06001DD9 RID: 7641
		// (set) Token: 0x06001DDA RID: 7642
		[DomName("type")]
		string Type { get; set; }
	}
}
