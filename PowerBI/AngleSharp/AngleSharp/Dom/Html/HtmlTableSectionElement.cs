using System;
using AngleSharp.Dom.Collections;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200039E RID: 926
	internal sealed class HtmlTableSectionElement : HtmlElement, IHtmlTableSectionElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001D16 RID: 7446 RVA: 0x00055397 File Offset: 0x00053597
		public HtmlTableSectionElement(Document owner, string name = null, string prefix = null)
			: base(owner, name ?? TagNames.Tbody, prefix, NodeFlags.Special | NodeFlags.ImplicitelyClosed | NodeFlags.HtmlTableSectionScoped)
		{
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06001D17 RID: 7447 RVA: 0x00054DBC File Offset: 0x00052FBC
		// (set) Token: 0x06001D18 RID: 7448 RVA: 0x00054155 File Offset: 0x00052355
		public HorizontalAlignment Align
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Align).ToEnum(HorizontalAlignment.Center);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Align, value.ToString(), false);
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06001D19 RID: 7449 RVA: 0x000553B0 File Offset: 0x000535B0
		public IHtmlCollection<IHtmlTableRowElement> Rows
		{
			get
			{
				HtmlCollection<IHtmlTableRowElement> htmlCollection;
				if ((htmlCollection = this._rows) == null)
				{
					htmlCollection = (this._rows = new HtmlCollection<IHtmlTableRowElement>(this, false, null));
				}
				return htmlCollection;
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06001D1A RID: 7450 RVA: 0x00054C26 File Offset: 0x00052E26
		// (set) Token: 0x06001D1B RID: 7451 RVA: 0x00054C39 File Offset: 0x00052E39
		public VerticalAlignment VAlign
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Valign).ToEnum(VerticalAlignment.Middle);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Valign, value.ToString(), false);
			}
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x000553D8 File Offset: 0x000535D8
		public IHtmlTableRowElement InsertRowAt(int index = -1)
		{
			IHtmlCollection<IHtmlTableRowElement> rows = this.Rows;
			IHtmlTableRowElement htmlTableRowElement = base.Owner.CreateElement(TagNames.Tr) as IHtmlTableRowElement;
			if (index >= 0 && index < rows.Length)
			{
				base.InsertBefore(htmlTableRowElement, rows[index]);
			}
			else
			{
				base.AppendChild(htmlTableRowElement);
			}
			return htmlTableRowElement;
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x0005542C File Offset: 0x0005362C
		public void RemoveRowAt(int index)
		{
			IHtmlCollection<IHtmlTableRowElement> rows = this.Rows;
			if (index >= 0 && index < rows.Length)
			{
				rows[index].Remove();
			}
		}

		// Token: 0x04000D00 RID: 3328
		private HtmlCollection<IHtmlTableRowElement> _rows;
	}
}
