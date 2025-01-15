using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200039F RID: 927
	internal sealed class HtmlTeletypeTextElement : HtmlElement
	{
		// Token: 0x06001D1E RID: 7454 RVA: 0x00055459 File Offset: 0x00053659
		public HtmlTeletypeTextElement(Document owner, string prefix = null)
			: base(owner, TagNames.Tt, prefix, NodeFlags.HtmlFormatting)
		{
		}
	}
}
