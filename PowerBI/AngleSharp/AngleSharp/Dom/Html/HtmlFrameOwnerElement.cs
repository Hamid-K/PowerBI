using System;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200035E RID: 862
	internal abstract class HtmlFrameOwnerElement : HtmlElement
	{
		// Token: 0x06001AAF RID: 6831 RVA: 0x000525E5 File Offset: 0x000507E5
		public HtmlFrameOwnerElement(Document owner, string name, string prefix, NodeFlags flags = NodeFlags.None)
			: base(owner, name, prefix, flags)
		{
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06001AB0 RID: 6832 RVA: 0x000525F2 File Offset: 0x000507F2
		// (set) Token: 0x06001AB1 RID: 6833 RVA: 0x000525FA File Offset: 0x000507FA
		public bool CanContainRangeEndpoint { get; private set; }

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06001AB2 RID: 6834 RVA: 0x00052603 File Offset: 0x00050803
		// (set) Token: 0x06001AB3 RID: 6835 RVA: 0x000501DB File Offset: 0x0004E3DB
		public int DisplayWidth
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Width).ToInteger(0);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Width, value.ToString(), false);
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06001AB4 RID: 6836 RVA: 0x00052616 File Offset: 0x00050816
		// (set) Token: 0x06001AB5 RID: 6837 RVA: 0x00050207 File Offset: 0x0004E407
		public int DisplayHeight
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Height).ToInteger(0);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Height, value.ToString(), false);
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06001AB6 RID: 6838 RVA: 0x00052629 File Offset: 0x00050829
		// (set) Token: 0x06001AB7 RID: 6839 RVA: 0x0005263C File Offset: 0x0005083C
		public int MarginWidth
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.MarginWidth).ToInteger(0);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.MarginWidth, value.ToString(), false);
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001AB8 RID: 6840 RVA: 0x00052651 File Offset: 0x00050851
		// (set) Token: 0x06001AB9 RID: 6841 RVA: 0x00052664 File Offset: 0x00050864
		public int MarginHeight
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.MarginHeight).ToInteger(0);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.MarginHeight, value.ToString(), false);
			}
		}
	}
}
