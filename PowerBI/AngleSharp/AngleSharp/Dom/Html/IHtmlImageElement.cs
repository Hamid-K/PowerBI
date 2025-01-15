using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003C6 RID: 966
	[DomName("HTMLImageElement")]
	public interface IHtmlImageElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, ILoadableElement
	{
		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06001E66 RID: 7782
		// (set) Token: 0x06001E67 RID: 7783
		[DomName("alt")]
		string AlternativeText { get; set; }

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06001E68 RID: 7784
		[DomName("currentSrc")]
		string ActualSource { get; }

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06001E69 RID: 7785
		// (set) Token: 0x06001E6A RID: 7786
		[DomName("src")]
		string Source { get; set; }

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06001E6B RID: 7787
		// (set) Token: 0x06001E6C RID: 7788
		[DomName("srcset")]
		string SourceSet { get; set; }

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06001E6D RID: 7789
		// (set) Token: 0x06001E6E RID: 7790
		[DomName("sizes")]
		string Sizes { get; set; }

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06001E6F RID: 7791
		// (set) Token: 0x06001E70 RID: 7792
		[DomName("crossOrigin")]
		string CrossOrigin { get; set; }

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06001E71 RID: 7793
		// (set) Token: 0x06001E72 RID: 7794
		[DomName("useMap")]
		string UseMap { get; set; }

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06001E73 RID: 7795
		// (set) Token: 0x06001E74 RID: 7796
		[DomName("isMap")]
		bool IsMap { get; set; }

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06001E75 RID: 7797
		// (set) Token: 0x06001E76 RID: 7798
		[DomName("width")]
		int DisplayWidth { get; set; }

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06001E77 RID: 7799
		// (set) Token: 0x06001E78 RID: 7800
		[DomName("height")]
		int DisplayHeight { get; set; }

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06001E79 RID: 7801
		[DomName("naturalWidth")]
		int OriginalWidth { get; }

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06001E7A RID: 7802
		[DomName("naturalHeight")]
		int OriginalHeight { get; }

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06001E7B RID: 7803
		[DomName("complete")]
		bool IsCompleted { get; }
	}
}
