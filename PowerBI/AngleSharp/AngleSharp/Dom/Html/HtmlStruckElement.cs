using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000394 RID: 916
	internal sealed class HtmlStruckElement : HtmlElement
	{
		// Token: 0x06001C9F RID: 7327 RVA: 0x00054A2D File Offset: 0x00052C2D
		public HtmlStruckElement(Document owner, string prefix = null)
			: base(owner, TagNames.S, prefix, NodeFlags.HtmlFormatting)
		{
		}
	}
}
