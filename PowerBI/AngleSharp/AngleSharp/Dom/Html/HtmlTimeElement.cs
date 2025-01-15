using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003A3 RID: 931
	internal sealed class HtmlTimeElement : HtmlElement, IHtmlTimeElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001D55 RID: 7509 RVA: 0x00055937 File Offset: 0x00053B37
		public HtmlTimeElement(Document owner, string prefix = null)
			: base(owner, TagNames.Time, prefix, NodeFlags.Special)
		{
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06001D56 RID: 7510 RVA: 0x00053D93 File Offset: 0x00051F93
		// (set) Token: 0x06001D57 RID: 7511 RVA: 0x00053DA0 File Offset: 0x00051FA0
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
