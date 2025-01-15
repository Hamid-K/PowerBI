using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000387 RID: 903
	internal sealed class HtmlRpElement : HtmlElement
	{
		// Token: 0x06001C53 RID: 7251 RVA: 0x00054292 File Offset: 0x00052492
		public HtmlRpElement(Document owner, string prefix = null)
			: base(owner, TagNames.Rp, prefix, NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd)
		{
		}
	}
}
