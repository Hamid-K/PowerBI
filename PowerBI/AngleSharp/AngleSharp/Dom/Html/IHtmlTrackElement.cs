using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Dom.Media;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003F2 RID: 1010
	[DomName("HTMLTrackElement")]
	public interface IHtmlTrackElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06002020 RID: 8224
		// (set) Token: 0x06002021 RID: 8225
		[DomName("kind")]
		string Kind { get; set; }

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06002022 RID: 8226
		// (set) Token: 0x06002023 RID: 8227
		[DomName("src")]
		string Source { get; set; }

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06002024 RID: 8228
		// (set) Token: 0x06002025 RID: 8229
		[DomName("srclang")]
		string SourceLanguage { get; set; }

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06002026 RID: 8230
		// (set) Token: 0x06002027 RID: 8231
		[DomName("label")]
		string Label { get; set; }

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06002028 RID: 8232
		// (set) Token: 0x06002029 RID: 8233
		[DomName("default")]
		bool IsDefault { get; set; }

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x0600202A RID: 8234
		[DomName("readyState")]
		TrackReadyState ReadyState { get; }

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x0600202B RID: 8235
		[DomName("track")]
		ITextTrack Track { get; }
	}
}
