using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000389 RID: 905
	internal sealed class HtmlRtElement : HtmlElement
	{
		// Token: 0x06001C55 RID: 7253 RVA: 0x000542B4 File Offset: 0x000524B4
		public HtmlRtElement(Document owner, string prefix = null)
			: base(owner, TagNames.Rt, prefix, NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd)
		{
		}
	}
}
