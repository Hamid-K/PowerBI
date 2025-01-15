using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200036A RID: 874
	internal sealed class HtmlLabelElement : HtmlElement, IHtmlLabelElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001B47 RID: 6983 RVA: 0x000530A5 File Offset: 0x000512A5
		public HtmlLabelElement(Document owner, string prefix = null)
			: base(owner, TagNames.Label, prefix, NodeFlags.None)
		{
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06001B48 RID: 6984 RVA: 0x000530B8 File Offset: 0x000512B8
		public IHtmlElement Control
		{
			get
			{
				string htmlFor = this.HtmlFor;
				if (!string.IsNullOrEmpty(htmlFor))
				{
					IHtmlElement htmlElement = base.Owner.GetElementById(htmlFor) as IHtmlElement;
					if (htmlElement is ILabelabelElement)
					{
						return htmlElement;
					}
				}
				return null;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06001B49 RID: 6985 RVA: 0x000530F1 File Offset: 0x000512F1
		// (set) Token: 0x06001B4A RID: 6986 RVA: 0x000530FE File Offset: 0x000512FE
		public string HtmlFor
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.For);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.For, value, false);
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06001B4B RID: 6987 RVA: 0x00051B51 File Offset: 0x0004FD51
		public IHtmlFormElement Form
		{
			get
			{
				return base.GetAssignedForm();
			}
		}
	}
}
