using System;
using AngleSharp.Dom.Events;
using AngleSharp.Html;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020001FC RID: 508
	internal sealed class CssMediaQueryList : EventTarget, IMediaQueryList, IEventTarget
	{
		// Token: 0x140000F7 RID: 247
		// (add) Token: 0x06001135 RID: 4405 RVA: 0x00040DAF File Offset: 0x0003EFAF
		// (remove) Token: 0x06001136 RID: 4406 RVA: 0x00040DBE File Offset: 0x0003EFBE
		public event DomEventHandler Changed
		{
			add
			{
				base.AddEventListener(EventNames.Change, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.Change, value, false);
			}
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x00047B5D File Offset: 0x00045D5D
		public CssMediaQueryList(IWindow window, IMediaList media)
		{
			this._media = media;
			this._matched = this.ComputeMatched(window);
			window.Resized += this.Resized;
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x00047B8B File Offset: 0x00045D8B
		public string MediaText
		{
			get
			{
				return this._media.MediaText;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x00047B98 File Offset: 0x00045D98
		public IMediaList Media
		{
			get
			{
				return this._media;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x0600113A RID: 4410 RVA: 0x00047BA0 File Offset: 0x00045DA0
		public bool IsMatched
		{
			get
			{
				return this._matched;
			}
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x0000EE9F File Offset: 0x0000D09F
		private bool ComputeMatched(IWindow window)
		{
			return false;
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00047BA8 File Offset: 0x00045DA8
		private void Resized(object sender, Event ev)
		{
			IWindow window = (IWindow)sender;
			bool flag = this.ComputeMatched(window);
			if (flag != this._matched)
			{
				MediaQueryListEvent mediaQueryListEvent = new MediaQueryListEvent(EventNames.Change, false, false, this._media.MediaText, flag);
				base.Dispatch(mediaQueryListEvent);
			}
			this._matched = flag;
		}

		// Token: 0x04000A8A RID: 2698
		private readonly IMediaList _media;

		// Token: 0x04000A8B RID: 2699
		private bool _matched;
	}
}
