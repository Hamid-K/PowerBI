using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000393 RID: 915
	internal sealed class HtmlStrongElement : HtmlElement
	{
		// Token: 0x06001C9E RID: 7326 RVA: 0x00054A19 File Offset: 0x00052C19
		public HtmlStrongElement(Document owner, string prefix = null)
			: base(owner, TagNames.Strong, prefix, NodeFlags.HtmlFormatting)
		{
		}
	}
}
