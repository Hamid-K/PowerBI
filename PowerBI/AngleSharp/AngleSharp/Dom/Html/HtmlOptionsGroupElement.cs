using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200037C RID: 892
	internal sealed class HtmlOptionsGroupElement : HtmlElement, IHtmlOptionsGroupElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001C23 RID: 7203 RVA: 0x00053FFF File Offset: 0x000521FF
		public HtmlOptionsGroupElement(Document owner, string prefix = null)
			: base(owner, TagNames.Optgroup, prefix, NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd | NodeFlags.HtmlSelectScoped)
		{
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06001C24 RID: 7204 RVA: 0x00053A01 File Offset: 0x00051C01
		// (set) Token: 0x06001C25 RID: 7205 RVA: 0x00053A0E File Offset: 0x00051C0E
		public string Label
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Label);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Label, value, false);
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06001C26 RID: 7206 RVA: 0x00053260 File Offset: 0x00051460
		// (set) Token: 0x06001C27 RID: 7207 RVA: 0x00051B70 File Offset: 0x0004FD70
		public bool IsDisabled
		{
			get
			{
				return this.GetBoolAttribute(AttributeNames.Disabled);
			}
			set
			{
				this.SetBoolAttribute(AttributeNames.Disabled, value);
			}
		}
	}
}
