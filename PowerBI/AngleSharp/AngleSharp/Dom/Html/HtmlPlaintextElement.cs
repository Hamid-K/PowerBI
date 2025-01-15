using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000382 RID: 898
	internal sealed class HtmlPlaintextElement : HtmlElement
	{
		// Token: 0x06001C45 RID: 7237 RVA: 0x00054190 File Offset: 0x00052390
		public HtmlPlaintextElement(Document owner, string prefix)
			: base(owner, TagNames.Plaintext, prefix, NodeFlags.Special | NodeFlags.LiteralText)
		{
		}
	}
}
