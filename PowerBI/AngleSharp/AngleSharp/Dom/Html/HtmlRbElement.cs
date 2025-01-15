using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000386 RID: 902
	internal sealed class HtmlRbElement : HtmlElement
	{
		// Token: 0x06001C52 RID: 7250 RVA: 0x00054281 File Offset: 0x00052481
		public HtmlRbElement(Document owner, string prefix = null)
			: base(owner, TagNames.Rb, prefix, NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd)
		{
		}
	}
}
