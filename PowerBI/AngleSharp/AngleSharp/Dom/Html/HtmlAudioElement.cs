using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Dom.Media;
using AngleSharp.Html;
using AngleSharp.Services.Media;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000341 RID: 833
	internal sealed class HtmlAudioElement : HtmlMediaElement<IAudioInfo>, IHtmlAudioElement, IHtmlMediaElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, IMediaController, ILoadableElement
	{
		// Token: 0x0600192F RID: 6447 RVA: 0x0004FD63 File Offset: 0x0004DF63
		public HtmlAudioElement(Document owner, string prefix = null)
			: base(owner, TagNames.Audio, prefix)
		{
			this._audios = null;
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001930 RID: 6448 RVA: 0x0004FD79 File Offset: 0x0004DF79
		public override IAudioTrackList AudioTracks
		{
			get
			{
				return this._audios;
			}
		}

		// Token: 0x04000CC0 RID: 3264
		private IAudioTrackList _audios;
	}
}
