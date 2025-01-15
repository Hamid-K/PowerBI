using System;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000356 RID: 854
	internal sealed class HtmlEmphasizeElement : HtmlElement
	{
		// Token: 0x06001A4F RID: 6735 RVA: 0x00051ABE File Offset: 0x0004FCBE
		public HtmlEmphasizeElement(Document owner, string prefix = null)
			: base(owner, TagNames.Em, prefix, NodeFlags.HtmlFormatting)
		{
		}
	}
}
