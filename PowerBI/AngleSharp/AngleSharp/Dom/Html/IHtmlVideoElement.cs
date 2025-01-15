using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Dom.Media;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003F5 RID: 1013
	[DomName("HTMLVideoElement")]
	public interface IHtmlVideoElement : IHtmlMediaElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, IMediaController, ILoadableElement
	{
		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x0600202C RID: 8236
		// (set) Token: 0x0600202D RID: 8237
		[DomName("width")]
		int DisplayWidth { get; set; }

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x0600202E RID: 8238
		// (set) Token: 0x0600202F RID: 8239
		[DomName("height")]
		int DisplayHeight { get; set; }

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06002030 RID: 8240
		[DomName("videoWidth")]
		int OriginalWidth { get; }

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06002031 RID: 8241
		[DomName("videoHeight")]
		int OriginalHeight { get; }

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06002032 RID: 8242
		// (set) Token: 0x06002033 RID: 8243
		[DomName("poster")]
		string Poster { get; set; }
	}
}
