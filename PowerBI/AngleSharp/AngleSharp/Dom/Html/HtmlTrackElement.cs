using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Dom.Media;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003A5 RID: 933
	internal sealed class HtmlTrackElement : HtmlElement, IHtmlTrackElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001D5B RID: 7515 RVA: 0x00055957 File Offset: 0x00053B57
		public HtmlTrackElement(Document owner, string prefix = null)
			: base(owner, TagNames.Track, prefix, NodeFlags.SelfClosing | NodeFlags.Special)
		{
			this._ready = TrackReadyState.None;
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06001D5C RID: 7516 RVA: 0x0005596E File Offset: 0x00053B6E
		// (set) Token: 0x06001D5D RID: 7517 RVA: 0x0005597B File Offset: 0x00053B7B
		public string Kind
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Kind);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Kind, value, false);
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06001D5E RID: 7518 RVA: 0x000524DE File Offset: 0x000506DE
		// (set) Token: 0x06001D5F RID: 7519 RVA: 0x00051A18 File Offset: 0x0004FC18
		public string Source
		{
			get
			{
				return this.GetUrlAttribute(AttributeNames.Src);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Src, value, false);
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06001D60 RID: 7520 RVA: 0x0005598A File Offset: 0x00053B8A
		// (set) Token: 0x06001D61 RID: 7521 RVA: 0x00055997 File Offset: 0x00053B97
		public string SourceLanguage
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.SrcLang);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.SrcLang, value, false);
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06001D62 RID: 7522 RVA: 0x00053A01 File Offset: 0x00051C01
		// (set) Token: 0x06001D63 RID: 7523 RVA: 0x00053A0E File Offset: 0x00051C0E
		public string Label
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Label);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Label, value, false);
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06001D64 RID: 7524 RVA: 0x00053AA7 File Offset: 0x00051CA7
		// (set) Token: 0x06001D65 RID: 7525 RVA: 0x00053AB4 File Offset: 0x00051CB4
		public bool IsDefault
		{
			get
			{
				return this.GetBoolAttribute(AttributeNames.Default);
			}
			set
			{
				this.SetBoolAttribute(AttributeNames.Default, value);
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06001D66 RID: 7526 RVA: 0x000559A6 File Offset: 0x00053BA6
		public TrackReadyState ReadyState
		{
			get
			{
				return this._ready;
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06001D67 RID: 7527 RVA: 0x0000C295 File Offset: 0x0000A495
		public ITextTrack Track
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000D07 RID: 3335
		private TrackReadyState _ready;
	}
}
