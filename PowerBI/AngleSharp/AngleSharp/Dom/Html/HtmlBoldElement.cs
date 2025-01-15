using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000347 RID: 839
	internal sealed class HtmlBoldElement : HtmlElement
	{
		// Token: 0x06001960 RID: 6496 RVA: 0x0004FF09 File Offset: 0x0004E109
		public HtmlBoldElement(Document owner, string prefix = null)
			: base(owner, TagNames.B, prefix, NodeFlags.HtmlFormatting)
		{
		}
	}
}
