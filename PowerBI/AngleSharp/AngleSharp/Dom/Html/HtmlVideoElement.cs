using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Dom.Media;
using AngleSharp.Extensions;
using AngleSharp.Html;
using AngleSharp.Services.Media;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003AA RID: 938
	internal sealed class HtmlVideoElement : HtmlMediaElement<IVideoInfo>, IHtmlVideoElement, IHtmlMediaElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, IMediaController, ILoadableElement
	{
		// Token: 0x06001D99 RID: 7577 RVA: 0x00055EA9 File Offset: 0x000540A9
		public HtmlVideoElement(Document owner, string prefix = null)
			: base(owner, TagNames.Video, prefix)
		{
			this._videos = null;
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06001D9A RID: 7578 RVA: 0x00055EBF File Offset: 0x000540BF
		public override IVideoTrackList VideoTracks
		{
			get
			{
				return this._videos;
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06001D9B RID: 7579 RVA: 0x00055EC7 File Offset: 0x000540C7
		// (set) Token: 0x06001D9C RID: 7580 RVA: 0x000501DB File Offset: 0x0004E3DB
		public int DisplayWidth
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Width).ToInteger(this.OriginalWidth);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Width, value.ToString(), false);
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06001D9D RID: 7581 RVA: 0x00055EDF File Offset: 0x000540DF
		// (set) Token: 0x06001D9E RID: 7582 RVA: 0x00050207 File Offset: 0x0004E407
		public int DisplayHeight
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Height).ToInteger(this.OriginalHeight);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Height, value.ToString(), false);
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06001D9F RID: 7583 RVA: 0x00055EF7 File Offset: 0x000540F7
		public int OriginalWidth
		{
			get
			{
				IVideoInfo media = base.Media;
				if (media == null)
				{
					return 0;
				}
				return media.Width;
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06001DA0 RID: 7584 RVA: 0x00055F0A File Offset: 0x0005410A
		public int OriginalHeight
		{
			get
			{
				IVideoInfo media = base.Media;
				if (media == null)
				{
					return 0;
				}
				return media.Height;
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06001DA1 RID: 7585 RVA: 0x00055F1D File Offset: 0x0005411D
		// (set) Token: 0x06001DA2 RID: 7586 RVA: 0x00055F2A File Offset: 0x0005412A
		public string Poster
		{
			get
			{
				return this.GetUrlAttribute(AttributeNames.Poster);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Poster, value, false);
			}
		}

		// Token: 0x04000D0C RID: 3340
		private IVideoTrackList _videos;
	}
}
