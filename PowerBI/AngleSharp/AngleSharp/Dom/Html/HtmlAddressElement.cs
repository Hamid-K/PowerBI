using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200033D RID: 829
	internal sealed class HtmlAddressElement : HtmlElement
	{
		// Token: 0x0600191E RID: 6430 RVA: 0x0004FC6B File Offset: 0x0004DE6B
		public HtmlAddressElement(Document owner, string prefix = null)
			: base(owner, TagNames.Address, prefix, NodeFlags.Special)
		{
		}
	}
}
