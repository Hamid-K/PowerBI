using System;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200035C RID: 860
	internal sealed class HtmlFrameElement : HtmlFrameElementBase
	{
		// Token: 0x06001A9B RID: 6811 RVA: 0x00052478 File Offset: 0x00050678
		public HtmlFrameElement(Document owner, string prefix = null)
			: base(owner, TagNames.Frame, prefix, NodeFlags.SelfClosing)
		{
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06001A9C RID: 6812 RVA: 0x00052488 File Offset: 0x00050688
		// (set) Token: 0x06001A9D RID: 6813 RVA: 0x0005249B File Offset: 0x0005069B
		public bool NoResize
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.NoResize).ToBoolean(false);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.NoResize, value.ToString(), false);
			}
		}
	}
}
