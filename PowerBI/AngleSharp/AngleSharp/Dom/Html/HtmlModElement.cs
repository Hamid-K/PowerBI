using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000375 RID: 885
	internal sealed class HtmlModElement : HtmlElement, IHtmlModElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001BF6 RID: 7158 RVA: 0x00053D62 File Offset: 0x00051F62
		public HtmlModElement(Document owner, string name = null, string prefix = null)
			: base(owner, name ?? TagNames.Ins, prefix, NodeFlags.None)
		{
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x00053D77 File Offset: 0x00051F77
		// (set) Token: 0x06001BF8 RID: 7160 RVA: 0x00053D84 File Offset: 0x00051F84
		public string Citation
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Cite);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Cite, value, false);
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06001BF9 RID: 7161 RVA: 0x00053D93 File Offset: 0x00051F93
		// (set) Token: 0x06001BFA RID: 7162 RVA: 0x00053DA0 File Offset: 0x00051FA0
		public string DateTime
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Datetime);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Datetime, value, false);
			}
		}
	}
}
