using System;
using AngleSharp.Attributes;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000344 RID: 836
	[DomHistorical]
	internal sealed class HtmlBgsoundElement : HtmlElement
	{
		// Token: 0x06001939 RID: 6457 RVA: 0x0004FE2C File Offset: 0x0004E02C
		public HtmlBgsoundElement(Document owner, string prefix = null)
			: base(owner, TagNames.Bgsound, prefix, NodeFlags.SelfClosing | NodeFlags.Special)
		{
		}
	}
}
