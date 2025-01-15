using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200036D RID: 877
	internal sealed class HtmlListItemElement : HtmlElement, IHtmlListItemElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001B77 RID: 7031 RVA: 0x00053450 File Offset: 0x00051650
		public HtmlListItemElement(Document owner, string name = null, string prefix = null)
			: base(owner, name ?? TagNames.Li, prefix, NodeFlags.Special | NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd)
		{
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06001B78 RID: 7032 RVA: 0x00053468 File Offset: 0x00051668
		// (set) Token: 0x06001B79 RID: 7033 RVA: 0x0005349C File Offset: 0x0005169C
		public int? Value
		{
			get
			{
				int num = 0;
				if (!int.TryParse(this.GetOwnAttribute(AttributeNames.Value), out num))
				{
					return null;
				}
				return new int?(num);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Value, (value != null) ? value.Value.ToString() : null, false);
			}
		}
	}
}
