using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000371 RID: 881
	internal sealed class HtmlMenuElement : HtmlElement, IHtmlMenuElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001BC3 RID: 7107 RVA: 0x000539F1 File Offset: 0x00051BF1
		public HtmlMenuElement(Document owner, string prefix = null)
			: base(owner, TagNames.Menu, prefix, NodeFlags.Special)
		{
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06001BC4 RID: 7108 RVA: 0x00051A27 File Offset: 0x0004FC27
		// (set) Token: 0x06001BC5 RID: 7109 RVA: 0x0004FF58 File Offset: 0x0004E158
		public string Type
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Type);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Type, value, false);
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06001BC6 RID: 7110 RVA: 0x00053A01 File Offset: 0x00051C01
		// (set) Token: 0x06001BC7 RID: 7111 RVA: 0x00053A0E File Offset: 0x00051C0E
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
	}
}
