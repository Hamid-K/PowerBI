using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000392 RID: 914
	internal sealed class HtmlStrikeElement : HtmlElement
	{
		// Token: 0x06001C9D RID: 7325 RVA: 0x00054A05 File Offset: 0x00052C05
		public HtmlStrikeElement(Document owner, string prefix = null)
			: base(owner, TagNames.Strike, prefix, NodeFlags.HtmlFormatting)
		{
		}
	}
}
