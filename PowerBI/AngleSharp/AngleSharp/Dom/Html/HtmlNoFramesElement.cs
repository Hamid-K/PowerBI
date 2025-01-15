using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000377 RID: 887
	internal sealed class HtmlNoFramesElement : HtmlElement
	{
		// Token: 0x06001BFC RID: 7164 RVA: 0x00053DBF File Offset: 0x00051FBF
		public HtmlNoFramesElement(Document owner, string prefix = null)
			: base(owner, TagNames.NoFrames, prefix, NodeFlags.Special | NodeFlags.LiteralText)
		{
		}
	}
}
