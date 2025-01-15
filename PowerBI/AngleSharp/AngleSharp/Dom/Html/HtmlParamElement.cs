using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000380 RID: 896
	internal sealed class HtmlParamElement : HtmlElement, IHtmlParamElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001C3F RID: 7231 RVA: 0x00054170 File Offset: 0x00052370
		public HtmlParamElement(Document owner, string prefix = null)
			: base(owner, TagNames.Param, prefix, NodeFlags.SelfClosing | NodeFlags.Special)
		{
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06001C40 RID: 7232 RVA: 0x00050412 File Offset: 0x0004E612
		// (set) Token: 0x06001C41 RID: 7233 RVA: 0x000500CC File Offset: 0x0004E2CC
		public string Value
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Value);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Value, value, false);
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06001C42 RID: 7234 RVA: 0x0004FCAB File Offset: 0x0004DEAB
		// (set) Token: 0x06001C43 RID: 7235 RVA: 0x0004FCB8 File Offset: 0x0004DEB8
		public string Name
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Name);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Name, value, false);
			}
		}
	}
}
