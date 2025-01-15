using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003C7 RID: 967
	[DomName("HTMLIFrameElement")]
	public interface IHtmlInlineFrameElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, ILoadableElement
	{
		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06001E7C RID: 7804
		// (set) Token: 0x06001E7D RID: 7805
		[DomName("src")]
		string Source { get; set; }

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06001E7E RID: 7806
		// (set) Token: 0x06001E7F RID: 7807
		[DomName("srcdoc")]
		string ContentHtml { get; set; }

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06001E80 RID: 7808
		// (set) Token: 0x06001E81 RID: 7809
		[DomName("name")]
		string Name { get; set; }

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06001E82 RID: 7810
		[DomName("sandbox")]
		ISettableTokenList Sandbox { get; }

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06001E83 RID: 7811
		// (set) Token: 0x06001E84 RID: 7812
		[DomName("seamless")]
		bool IsSeamless { get; set; }

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06001E85 RID: 7813
		// (set) Token: 0x06001E86 RID: 7814
		[DomName("allowFullscreen")]
		bool IsFullscreenAllowed { get; set; }

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06001E87 RID: 7815
		// (set) Token: 0x06001E88 RID: 7816
		[DomName("width")]
		int DisplayWidth { get; set; }

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06001E89 RID: 7817
		// (set) Token: 0x06001E8A RID: 7818
		[DomName("height")]
		int DisplayHeight { get; set; }

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06001E8B RID: 7819
		[DomName("contentDocument")]
		IDocument ContentDocument { get; }

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06001E8C RID: 7820
		[DomName("contentWindow")]
		IWindow ContentWindow { get; }
	}
}
