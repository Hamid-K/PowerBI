using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200038D RID: 909
	internal sealed class HtmlSemanticElement : HtmlElement
	{
		// Token: 0x06001C8A RID: 7306 RVA: 0x000548EB File Offset: 0x00052AEB
		public HtmlSemanticElement(Document owner, string name, string prefix = null)
			: base(owner, name, prefix, NodeFlags.Special)
		{
		}
	}
}
