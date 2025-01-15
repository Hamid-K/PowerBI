using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200038A RID: 906
	internal sealed class HtmlRubyElement : HtmlElement
	{
		// Token: 0x06001C56 RID: 7254 RVA: 0x000542C5 File Offset: 0x000524C5
		public HtmlRubyElement(Document owner, string prefix = null)
			: base(owner, TagNames.Ruby, prefix, NodeFlags.None)
		{
		}
	}
}
