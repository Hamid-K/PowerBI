using System;
using AngleSharp.Attributes;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000351 RID: 849
	[DomHistorical]
	internal sealed class HtmlDirectoryElement : HtmlElement
	{
		// Token: 0x06001996 RID: 6550 RVA: 0x000504CD File Offset: 0x0004E6CD
		public HtmlDirectoryElement(Document owner, string prefix = null)
			: base(owner, TagNames.Dir, prefix, NodeFlags.Special)
		{
		}
	}
}
