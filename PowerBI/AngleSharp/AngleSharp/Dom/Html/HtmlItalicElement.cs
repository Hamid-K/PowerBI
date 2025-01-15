using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000368 RID: 872
	internal sealed class HtmlItalicElement : HtmlElement
	{
		// Token: 0x06001B3D RID: 6973 RVA: 0x00052FF5 File Offset: 0x000511F5
		public HtmlItalicElement(Document owner, string prefix = null)
			: base(owner, TagNames.I, prefix, NodeFlags.HtmlFormatting)
		{
		}
	}
}
