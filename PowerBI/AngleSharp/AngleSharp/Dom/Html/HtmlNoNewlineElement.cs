using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000378 RID: 888
	internal sealed class HtmlNoNewlineElement : HtmlElement
	{
		// Token: 0x06001BFD RID: 7165 RVA: 0x00053DCF File Offset: 0x00051FCF
		public HtmlNoNewlineElement(Document owner, string prefix = null)
			: base(owner, TagNames.NoBr, prefix, NodeFlags.HtmlFormatting)
		{
		}
	}
}
