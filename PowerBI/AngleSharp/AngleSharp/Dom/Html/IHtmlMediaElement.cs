using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Dom.Media;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003CF RID: 975
	[DomName("HTMLMediaElement")]
	public interface IHtmlMediaElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, IMediaController, ILoadableElement
	{
		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06001F07 RID: 7943
		// (set) Token: 0x06001F08 RID: 7944
		[DomName("src")]
		string Source { get; set; }

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06001F09 RID: 7945
		// (set) Token: 0x06001F0A RID: 7946
		[DomName("crossOrigin")]
		string CrossOrigin { get; set; }

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06001F0B RID: 7947
		// (set) Token: 0x06001F0C RID: 7948
		[DomName("preload")]
		string Preload { get; set; }

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06001F0D RID: 7949
		// (set) Token: 0x06001F0E RID: 7950
		[DomName("mediaGroup")]
		string MediaGroup { get; set; }

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06001F0F RID: 7951
		[DomName("networkState")]
		MediaNetworkState NetworkState { get; }

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06001F10 RID: 7952
		[DomName("seeking")]
		bool IsSeeking { get; }

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06001F11 RID: 7953
		[DomName("currentSrc")]
		string CurrentSource { get; }

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06001F12 RID: 7954
		[DomName("error")]
		IMediaError MediaError { get; }

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06001F13 RID: 7955
		[DomName("controller")]
		IMediaController Controller { get; }

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06001F14 RID: 7956
		[DomName("ended")]
		bool IsEnded { get; }

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06001F15 RID: 7957
		// (set) Token: 0x06001F16 RID: 7958
		[DomName("autoplay")]
		bool IsAutoplay { get; set; }

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06001F17 RID: 7959
		// (set) Token: 0x06001F18 RID: 7960
		[DomName("loop")]
		bool IsLoop { get; set; }

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06001F19 RID: 7961
		// (set) Token: 0x06001F1A RID: 7962
		[DomName("controls")]
		bool IsShowingControls { get; set; }

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06001F1B RID: 7963
		// (set) Token: 0x06001F1C RID: 7964
		[DomName("defaultMuted")]
		bool IsDefaultMuted { get; set; }

		// Token: 0x06001F1D RID: 7965
		[DomName("load")]
		void Load();

		// Token: 0x06001F1E RID: 7966
		[DomName("canPlayType")]
		string CanPlayType(string type);

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06001F1F RID: 7967
		[DomName("startDate")]
		DateTime StartDate { get; }

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06001F20 RID: 7968
		[DomName("audioTracks")]
		IAudioTrackList AudioTracks { get; }

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06001F21 RID: 7969
		[DomName("videoTracks")]
		IVideoTrackList VideoTracks { get; }

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06001F22 RID: 7970
		[DomName("textTracks")]
		ITextTrackList TextTracks { get; }

		// Token: 0x06001F23 RID: 7971
		[DomName("addTextTrack")]
		ITextTrack AddTextTrack(string kind, string label = null, string language = null);
	}
}
