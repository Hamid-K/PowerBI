using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003A6 RID: 934
	internal sealed class HtmlUnderlineElement : HtmlElement
	{
		// Token: 0x06001D68 RID: 7528 RVA: 0x000559AE File Offset: 0x00053BAE
		public HtmlUnderlineElement(Document owner, string prefix = null)
			: base(owner, TagNames.U, prefix, NodeFlags.HtmlFormatting)
		{
		}
	}
}
