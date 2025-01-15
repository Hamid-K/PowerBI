using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200034F RID: 847
	internal sealed class HtmlDetailsElement : HtmlElement, IHtmlDetailsElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x0600198B RID: 6539 RVA: 0x00050468 File Offset: 0x0004E668
		public HtmlDetailsElement(Document owner, string prefix = null)
			: base(owner, TagNames.Details, prefix, NodeFlags.Special)
		{
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x0600198C RID: 6540 RVA: 0x00050478 File Offset: 0x0004E678
		// (set) Token: 0x0600198D RID: 6541 RVA: 0x00050485 File Offset: 0x0004E685
		public bool IsOpen
		{
			get
			{
				return this.GetBoolAttribute(AttributeNames.Open);
			}
			set
			{
				this.SetBoolAttribute(AttributeNames.Open, value);
			}
		}
	}
}
