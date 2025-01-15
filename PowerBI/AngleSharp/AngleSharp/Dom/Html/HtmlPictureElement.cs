using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000381 RID: 897
	internal sealed class HtmlPictureElement : HtmlElement
	{
		// Token: 0x06001C44 RID: 7236 RVA: 0x00054180 File Offset: 0x00052380
		public HtmlPictureElement(Document owner, string prefix = null)
			: base(owner, TagNames.Picture, prefix, NodeFlags.None)
		{
		}
	}
}
