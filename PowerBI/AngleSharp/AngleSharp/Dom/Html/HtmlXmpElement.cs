using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003AC RID: 940
	internal sealed class HtmlXmpElement : HtmlElement
	{
		// Token: 0x06001DA4 RID: 7588 RVA: 0x00055F49 File Offset: 0x00054149
		public HtmlXmpElement(Document owner, string prefix = null)
			: base(owner, TagNames.Xmp, prefix, NodeFlags.Special | NodeFlags.LiteralText)
		{
		}
	}
}
