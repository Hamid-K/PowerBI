using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000340 RID: 832
	internal sealed class HtmlAreaElement : HtmlUrlBaseElement, IHtmlAreaElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, IUrlUtilities
	{
		// Token: 0x06001928 RID: 6440 RVA: 0x0004FCFF File Offset: 0x0004DEFF
		public HtmlAreaElement(Document owner, string prefix = null)
			: base(owner, TagNames.Area, prefix, NodeFlags.SelfClosing | NodeFlags.Special)
		{
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001929 RID: 6441 RVA: 0x0004FD0F File Offset: 0x0004DF0F
		// (set) Token: 0x0600192A RID: 6442 RVA: 0x0004FD1C File Offset: 0x0004DF1C
		public string AlternativeText
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Alt);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Alt, value, false);
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x0600192B RID: 6443 RVA: 0x0004FD2B File Offset: 0x0004DF2B
		// (set) Token: 0x0600192C RID: 6444 RVA: 0x0004FD38 File Offset: 0x0004DF38
		public string Coordinates
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Coords);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Coords, value, false);
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x0600192D RID: 6445 RVA: 0x0004FD47 File Offset: 0x0004DF47
		// (set) Token: 0x0600192E RID: 6446 RVA: 0x0004FD54 File Offset: 0x0004DF54
		public string Shape
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Shape);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Shape, value, false);
			}
		}
	}
}
