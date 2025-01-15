using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000336 RID: 822
	[DomName("MediaQueryList")]
	public interface IMediaQueryList : IEventTarget
	{
		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001912 RID: 6418
		[DomName("media")]
		string MediaText { get; }

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001913 RID: 6419
		IMediaList Media { get; }

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001914 RID: 6420
		[DomName("matches")]
		bool IsMatched { get; }

		// Token: 0x140000F9 RID: 249
		// (add) Token: 0x06001915 RID: 6421
		// (remove) Token: 0x06001916 RID: 6422
		[DomName("onchange")]
		event DomEventHandler Changed;
	}
}
