using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200034E RID: 846
	internal sealed class HtmlDefinitionListElement : HtmlElement
	{
		// Token: 0x0600198A RID: 6538 RVA: 0x00050458 File Offset: 0x0004E658
		public HtmlDefinitionListElement(Document owner, string prefix = null)
			: base(owner, TagNames.Dl, prefix, NodeFlags.Special)
		{
		}
	}
}
