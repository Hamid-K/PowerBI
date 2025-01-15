using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003AB RID: 939
	internal sealed class HtmlWbrElement : HtmlElement
	{
		// Token: 0x06001DA3 RID: 7587 RVA: 0x00055F39 File Offset: 0x00054139
		public HtmlWbrElement(Document owner, string prefix = null)
			: base(owner, TagNames.Wbr, prefix, NodeFlags.SelfClosing | NodeFlags.Special)
		{
		}
	}
}
