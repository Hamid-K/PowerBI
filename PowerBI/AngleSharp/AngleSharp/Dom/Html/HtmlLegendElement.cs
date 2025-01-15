using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200036B RID: 875
	internal sealed class HtmlLegendElement : HtmlElement, IHtmlLegendElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001B4C RID: 6988 RVA: 0x0005310D File Offset: 0x0005130D
		public HtmlLegendElement(Document owner, string prefix = null)
			: base(owner, TagNames.Legend, prefix, NodeFlags.None)
		{
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06001B4D RID: 6989 RVA: 0x0005311D File Offset: 0x0005131D
		public IHtmlFormElement Form
		{
			get
			{
				HtmlFieldSetElement htmlFieldSetElement = base.Parent as HtmlFieldSetElement;
				if (htmlFieldSetElement == null)
				{
					return null;
				}
				return htmlFieldSetElement.Form;
			}
		}
	}
}
