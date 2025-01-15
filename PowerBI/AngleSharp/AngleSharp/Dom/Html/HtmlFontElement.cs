using System;
using AngleSharp.Attributes;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000358 RID: 856
	[DomHistorical]
	internal sealed class HtmlFontElement : HtmlElement
	{
		// Token: 0x06001A55 RID: 6741 RVA: 0x00051B18 File Offset: 0x0004FD18
		public HtmlFontElement(Document owner, string prefix = null)
			: base(owner, TagNames.Font, prefix, NodeFlags.HtmlFormatting)
		{
		}
	}
}
