using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000350 RID: 848
	internal sealed class HtmlDialogElement : HtmlElement, IHtmlDialogElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x0600198E RID: 6542 RVA: 0x00050493 File Offset: 0x0004E693
		public HtmlDialogElement(Document owner, string prefix = null)
			: base(owner, TagNames.Dialog, prefix, NodeFlags.None)
		{
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x0600198F RID: 6543 RVA: 0x00050478 File Offset: 0x0004E678
		// (set) Token: 0x06001990 RID: 6544 RVA: 0x00050485 File Offset: 0x0004E685
		public bool Open
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

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06001991 RID: 6545 RVA: 0x000504A3 File Offset: 0x0004E6A3
		// (set) Token: 0x06001992 RID: 6546 RVA: 0x000504AB File Offset: 0x0004E6AB
		public string ReturnValue
		{
			get
			{
				return this._returnValue;
			}
			set
			{
				this._returnValue = value;
			}
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x000504B4 File Offset: 0x0004E6B4
		public void Show(IElement anchor = null)
		{
			this.Open = true;
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x000504B4 File Offset: 0x0004E6B4
		public void ShowModal(IElement anchor = null)
		{
			this.Open = true;
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x000504BD File Offset: 0x0004E6BD
		public void Close(string returnValue = null)
		{
			this.Open = false;
			this.ReturnValue = returnValue;
		}

		// Token: 0x04000CC6 RID: 3270
		private string _returnValue;
	}
}
