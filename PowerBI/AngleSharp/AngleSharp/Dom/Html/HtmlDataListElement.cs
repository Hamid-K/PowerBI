using System;
using AngleSharp.Dom.Collections;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200034D RID: 845
	internal sealed class HtmlDataListElement : HtmlElement, IHtmlDataListElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001988 RID: 6536 RVA: 0x0005041F File Offset: 0x0004E61F
		public HtmlDataListElement(Document owner, string prefix = null)
			: base(owner, TagNames.Datalist, prefix, NodeFlags.None)
		{
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06001989 RID: 6537 RVA: 0x00050430 File Offset: 0x0004E630
		public IHtmlCollection<IHtmlOptionElement> Options
		{
			get
			{
				HtmlCollection<IHtmlOptionElement> htmlCollection;
				if ((htmlCollection = this._options) == null)
				{
					htmlCollection = (this._options = new HtmlCollection<IHtmlOptionElement>(this, true, null));
				}
				return htmlCollection;
			}
		}

		// Token: 0x04000CC5 RID: 3269
		private HtmlCollection<IHtmlOptionElement> _options;
	}
}
