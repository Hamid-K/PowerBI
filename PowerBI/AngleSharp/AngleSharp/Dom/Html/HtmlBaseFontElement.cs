using System;
using AngleSharp.Attributes;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000343 RID: 835
	[DomHistorical]
	internal sealed class HtmlBaseFontElement : HtmlElement
	{
		// Token: 0x06001938 RID: 6456 RVA: 0x0004FE1C File Offset: 0x0004E01C
		public HtmlBaseFontElement(Document owner, string prefix = null)
			: base(owner, TagNames.BaseFont, prefix, NodeFlags.SelfClosing | NodeFlags.Special)
		{
		}
	}
}
