using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000376 RID: 886
	internal sealed class HtmlNoEmbedElement : HtmlElement
	{
		// Token: 0x06001BFB RID: 7163 RVA: 0x00053DAF File Offset: 0x00051FAF
		public HtmlNoEmbedElement(Document owner, string prefix = null)
			: base(owner, TagNames.NoEmbed, prefix, NodeFlags.Special | NodeFlags.LiteralText)
		{
		}
	}
}
