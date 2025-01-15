using System;
using AngleSharp.Dom.Collections;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200039D RID: 925
	internal sealed class HtmlTableRowElement : HtmlElement, IHtmlTableRowElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001D0A RID: 7434 RVA: 0x00055290 File Offset: 0x00053490
		public HtmlTableRowElement(Document owner, string prefix = null)
			: base(owner, TagNames.Tr, prefix, NodeFlags.Special | NodeFlags.ImplicitelyClosed)
		{
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06001D0B RID: 7435 RVA: 0x00054142 File Offset: 0x00052342
		// (set) Token: 0x06001D0C RID: 7436 RVA: 0x00054155 File Offset: 0x00052355
		public HorizontalAlignment Align
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Align).ToEnum(HorizontalAlignment.Left);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Align, value.ToString(), false);
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06001D0D RID: 7437 RVA: 0x00054C26 File Offset: 0x00052E26
		// (set) Token: 0x06001D0E RID: 7438 RVA: 0x00054C39 File Offset: 0x00052E39
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

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06001D0F RID: 7439 RVA: 0x0004FE99 File Offset: 0x0004E099
		// (set) Token: 0x06001D10 RID: 7440 RVA: 0x0004FEA6 File Offset: 0x0004E0A6
		public string BgColor
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.BgColor);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.BgColor, value, false);
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06001D11 RID: 7441 RVA: 0x000552A4 File Offset: 0x000534A4
		public IHtmlCollection<IHtmlTableCellElement> Cells
		{
			get
			{
				HtmlCollection<IHtmlTableCellElement> htmlCollection;
				if ((htmlCollection = this._cells) == null)
				{
					htmlCollection = (this._cells = new HtmlCollection<IHtmlTableCellElement>(this, false, null));
				}
				return htmlCollection;
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06001D12 RID: 7442 RVA: 0x000552CC File Offset: 0x000534CC
		public int Index
		{
			get
			{
				IHtmlTableElement ancestor = this.GetAncestor<IHtmlTableElement>();
				if (ancestor == null)
				{
					return -1;
				}
				return ancestor.Rows.Index(this);
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06001D13 RID: 7443 RVA: 0x000552E5 File Offset: 0x000534E5
		public int IndexInSection
		{
			get
			{
				IHtmlTableSectionElement htmlTableSectionElement = base.ParentElement as IHtmlTableSectionElement;
				if (htmlTableSectionElement == null)
				{
					return this.Index;
				}
				return htmlTableSectionElement.Rows.Index(this);
			}
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x00055308 File Offset: 0x00053508
		public IHtmlTableCellElement InsertCellAt(int index = -1)
		{
			IHtmlCollection<IHtmlTableCellElement> cells = this.Cells;
			IHtmlTableCellElement htmlTableCellElement = base.Owner.CreateElement(TagNames.Td) as IHtmlTableCellElement;
			if (index >= 0 && index < cells.Length)
			{
				base.InsertBefore(htmlTableCellElement, cells[index]);
			}
			else
			{
				base.AppendChild(htmlTableCellElement);
			}
			return htmlTableCellElement;
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x0005535C File Offset: 0x0005355C
		public void RemoveCellAt(int index)
		{
			IHtmlCollection<IHtmlTableCellElement> cells = this.Cells;
			if (index < 0)
			{
				index = cells.Length + index;
			}
			if (index >= 0 && index < cells.Length)
			{
				cells[index].Remove();
			}
		}

		// Token: 0x04000CFF RID: 3327
		private HtmlCollection<IHtmlTableCellElement> _cells;
	}
}
