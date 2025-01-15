using System;
using AngleSharp.Attributes;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200035F RID: 863
	[DomHistorical]
	internal sealed class HtmlFrameSetElement : HtmlElement
	{
		// Token: 0x06001ABA RID: 6842 RVA: 0x00052679 File Offset: 0x00050879
		public HtmlFrameSetElement(Document owner, string prefix = null)
			: base(owner, TagNames.Frameset, prefix, NodeFlags.Special)
		{
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001ABB RID: 6843 RVA: 0x00052689 File Offset: 0x00050889
		// (set) Token: 0x06001ABC RID: 6844 RVA: 0x0005269C File Offset: 0x0005089C
		public int Columns
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Cols).ToInteger(1);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Cols, value.ToString(), false);
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001ABD RID: 6845 RVA: 0x000526B1 File Offset: 0x000508B1
		// (set) Token: 0x06001ABE RID: 6846 RVA: 0x000526C4 File Offset: 0x000508C4
		public int Rows
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Rows).ToInteger(1);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Rows, value.ToString(), false);
			}
		}
	}
}
