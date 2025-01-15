using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000345 RID: 837
	internal sealed class HtmlBigElement : HtmlElement
	{
		// Token: 0x0600193A RID: 6458 RVA: 0x0004FE3C File Offset: 0x0004E03C
		public HtmlBigElement(Document owner, string prefix = null)
			: base(owner, TagNames.Big, prefix, NodeFlags.HtmlFormatting)
		{
		}
	}
}
