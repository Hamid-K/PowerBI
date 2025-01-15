using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200034B RID: 843
	internal sealed class HtmlCodeElement : HtmlElement
	{
		// Token: 0x06001984 RID: 6532 RVA: 0x000503EE File Offset: 0x0004E5EE
		public HtmlCodeElement(Document owner, string prefix = null)
			: base(owner, TagNames.Code, prefix, NodeFlags.HtmlFormatting)
		{
		}
	}
}
