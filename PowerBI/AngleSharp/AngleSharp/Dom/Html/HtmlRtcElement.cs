using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000388 RID: 904
	internal sealed class HtmlRtcElement : HtmlElement
	{
		// Token: 0x06001C54 RID: 7252 RVA: 0x000542A3 File Offset: 0x000524A3
		public HtmlRtcElement(Document owner, string prefix = null)
			: base(owner, TagNames.Rtc, prefix, NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd)
		{
		}
	}
}
