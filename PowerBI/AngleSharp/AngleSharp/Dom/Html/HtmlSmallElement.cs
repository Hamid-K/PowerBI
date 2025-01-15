using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200038F RID: 911
	internal sealed class HtmlSmallElement : HtmlElement
	{
		// Token: 0x06001C90 RID: 7312 RVA: 0x000549D1 File Offset: 0x00052BD1
		public HtmlSmallElement(Document owner, string prefix = null)
			: base(owner, TagNames.Small, prefix, NodeFlags.HtmlFormatting)
		{
		}
	}
}
