using System;
using AngleSharp.Attributes;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200033F RID: 831
	[DomHistorical]
	internal sealed class HtmlAppletElement : HtmlElement
	{
		// Token: 0x06001927 RID: 6439 RVA: 0x0004FCEE File Offset: 0x0004DEEE
		public HtmlAppletElement(Document owner, string prefix = null)
			: base(owner, TagNames.Applet, prefix, NodeFlags.Special | NodeFlags.Scoped)
		{
		}
	}
}
