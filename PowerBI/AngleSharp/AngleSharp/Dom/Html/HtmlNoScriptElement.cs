using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000379 RID: 889
	internal sealed class HtmlNoScriptElement : HtmlElement
	{
		// Token: 0x06001BFE RID: 7166 RVA: 0x00053DE3 File Offset: 0x00051FE3
		public HtmlNoScriptElement(Document owner, string prefix = null)
			: base(owner, TagNames.NoScript, prefix, NodeFlags.Special | NodeFlags.LiteralText)
		{
		}
	}
}
