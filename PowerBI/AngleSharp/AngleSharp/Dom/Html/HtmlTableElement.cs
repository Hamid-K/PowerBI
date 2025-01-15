using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom.Collections;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200039B RID: 923
	internal sealed class HtmlTableElement : HtmlElement, IHtmlTableElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001CE4 RID: 7396 RVA: 0x00054E16 File Offset: 0x00053016
		public HtmlTableElement(Document owner, string prefix = null)
			: base(owner, TagNames.Table, prefix, NodeFlags.Special | NodeFlags.Scoped | NodeFlags.HtmlTableSectionScoped | NodeFlags.HtmlTableScoped)
		{
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06001CE5 RID: 7397 RVA: 0x00054E2A File Offset: 0x0005302A
		// (set) Token: 0x06001CE6 RID: 7398 RVA: 0x00054E5B File Offset: 0x0005305B
		public IHtmlTableCaptionElement Caption
		{
			get
			{
				return base.ChildNodes.OfType<IHtmlTableCaptionElement>().FirstOrDefault((IHtmlTableCaptionElement m) => m.LocalName.Is(TagNames.Caption));
			}
			set
			{
				this.DeleteCaption();
				base.InsertChild(0, value);
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06001CE7 RID: 7399 RVA: 0x00054E6C File Offset: 0x0005306C
		// (set) Token: 0x06001CE8 RID: 7400 RVA: 0x00054E9D File Offset: 0x0005309D
		public IHtmlTableSectionElement Head
		{
			get
			{
				return base.ChildNodes.OfType<IHtmlTableSectionElement>().FirstOrDefault((IHtmlTableSectionElement m) => m.LocalName.Is(TagNames.Thead));
			}
			set
			{
				this.DeleteHead();
				base.AppendChild(value);
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06001CE9 RID: 7401 RVA: 0x00054EB0 File Offset: 0x000530B0
		public IHtmlCollection<IHtmlTableSectionElement> Bodies
		{
			get
			{
				HtmlCollection<IHtmlTableSectionElement> htmlCollection;
				if ((htmlCollection = this._bodies) == null)
				{
					htmlCollection = (this._bodies = new HtmlCollection<IHtmlTableSectionElement>(this, false, (IHtmlTableSectionElement m) => m.LocalName.Is(TagNames.Tbody)));
				}
				return htmlCollection;
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06001CEA RID: 7402 RVA: 0x00054EF6 File Offset: 0x000530F6
		// (set) Token: 0x06001CEB RID: 7403 RVA: 0x00054F27 File Offset: 0x00053127
		public IHtmlTableSectionElement Foot
		{
			get
			{
				return base.ChildNodes.OfType<IHtmlTableSectionElement>().FirstOrDefault((IHtmlTableSectionElement m) => m.LocalName.Is(TagNames.Tfoot));
			}
			set
			{
				this.DeleteFoot();
				base.AppendChild(value);
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06001CEC RID: 7404 RVA: 0x00054F37 File Offset: 0x00053137
		public IEnumerable<IHtmlTableRowElement> AllRows
		{
			get
			{
				IEnumerable<IHtmlTableSectionElement> enumerable = from m in this.ChildNodes.OfType<IHtmlTableSectionElement>()
					where m.LocalName.Is(TagNames.Thead)
					select m;
				IEnumerable<IHtmlTableSectionElement> foots = from m in this.ChildNodes.OfType<IHtmlTableSectionElement>()
					where m.LocalName.Is(TagNames.Tfoot)
					select m;
				foreach (IHtmlTableSectionElement htmlTableSectionElement in enumerable)
				{
					foreach (IHtmlTableRowElement htmlTableRowElement in htmlTableSectionElement.Rows)
					{
						yield return htmlTableRowElement;
					}
					IEnumerator<IHtmlTableRowElement> enumerator2 = null;
				}
				IEnumerator<IHtmlTableSectionElement> enumerator = null;
				foreach (INode child in this.ChildNodes)
				{
					if (child is IHtmlTableSectionElement)
					{
						IHtmlTableSectionElement htmlTableSectionElement2 = (IHtmlTableSectionElement)child;
						if (htmlTableSectionElement2.LocalName.Is(TagNames.Tbody))
						{
							foreach (IHtmlTableRowElement htmlTableRowElement2 in htmlTableSectionElement2.Rows)
							{
								yield return htmlTableRowElement2;
							}
							IEnumerator<IHtmlTableRowElement> enumerator2 = null;
						}
					}
					else if (child is IHtmlTableRowElement)
					{
						yield return (IHtmlTableRowElement)child;
					}
					child = null;
				}
				IEnumerator<INode> enumerator3 = null;
				foreach (IHtmlTableSectionElement htmlTableSectionElement3 in foots)
				{
					foreach (IHtmlTableRowElement htmlTableRowElement3 in htmlTableSectionElement3.Rows)
					{
						yield return htmlTableRowElement3;
					}
					IEnumerator<IHtmlTableRowElement> enumerator2 = null;
				}
				enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06001CED RID: 7405 RVA: 0x00054F48 File Offset: 0x00053148
		public IHtmlCollection<IHtmlTableRowElement> Rows
		{
			get
			{
				HtmlCollection<IHtmlTableRowElement> htmlCollection;
				if ((htmlCollection = this._rows) == null)
				{
					htmlCollection = (this._rows = new HtmlCollection<IHtmlTableRowElement>(this.AllRows));
				}
				return htmlCollection;
			}
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06001CEE RID: 7406 RVA: 0x00054142 File Offset: 0x00052342
		// (set) Token: 0x06001CEF RID: 7407 RVA: 0x00054155 File Offset: 0x00052355
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

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06001CF0 RID: 7408 RVA: 0x0004FE99 File Offset: 0x0004E099
		// (set) Token: 0x06001CF1 RID: 7409 RVA: 0x0004FEA6 File Offset: 0x0004E0A6
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

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06001CF2 RID: 7410 RVA: 0x00054F73 File Offset: 0x00053173
		// (set) Token: 0x06001CF3 RID: 7411 RVA: 0x00054F86 File Offset: 0x00053186
		public uint Border
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Border).ToInteger(0U);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Border, value.ToString(), false);
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06001CF4 RID: 7412 RVA: 0x00054F9B File Offset: 0x0005319B
		// (set) Token: 0x06001CF5 RID: 7413 RVA: 0x00054FAE File Offset: 0x000531AE
		public int CellPadding
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.CellPadding).ToInteger(0);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.CellPadding, value.ToString(), false);
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06001CF6 RID: 7414 RVA: 0x00054FC3 File Offset: 0x000531C3
		// (set) Token: 0x06001CF7 RID: 7415 RVA: 0x00054FD6 File Offset: 0x000531D6
		public int CellSpacing
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.CellSpacing).ToInteger(0);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.CellSpacing, value.ToString(), false);
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06001CF8 RID: 7416 RVA: 0x00054FEB File Offset: 0x000531EB
		// (set) Token: 0x06001CF9 RID: 7417 RVA: 0x00054FFE File Offset: 0x000531FE
		public TableFrames Frame
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Frame).ToEnum(TableFrames.Void);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Frame, value.ToString(), false);
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06001CFA RID: 7418 RVA: 0x00055019 File Offset: 0x00053219
		// (set) Token: 0x06001CFB RID: 7419 RVA: 0x0005502C File Offset: 0x0005322C
		public TableRules Rules
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Rules).ToEnum(TableRules.All);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Rules, value.ToString(), false);
			}
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06001CFC RID: 7420 RVA: 0x00055047 File Offset: 0x00053247
		// (set) Token: 0x06001CFD RID: 7421 RVA: 0x00055054 File Offset: 0x00053254
		public string Summary
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Summary);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Summary, value, false);
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06001CFE RID: 7422 RVA: 0x00051A34 File Offset: 0x0004FC34
		// (set) Token: 0x06001CFF RID: 7423 RVA: 0x00051A41 File Offset: 0x0004FC41
		public string Width
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Width);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Width, value, false);
			}
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x00055064 File Offset: 0x00053264
		public IHtmlTableRowElement InsertRowAt(int index = -1)
		{
			IHtmlCollection<IHtmlTableRowElement> rows = this.Rows;
			IHtmlTableRowElement htmlTableRowElement = base.Owner.CreateElement(TagNames.Tr) as IHtmlTableRowElement;
			if (index >= 0 && index < rows.Length)
			{
				IHtmlTableRowElement htmlTableRowElement2 = rows[index];
				htmlTableRowElement2.ParentElement.InsertBefore(htmlTableRowElement, htmlTableRowElement2);
			}
			else if (rows.Length == 0)
			{
				IHtmlCollection<IHtmlTableSectionElement> bodies = this.Bodies;
				if (bodies.Length == 0)
				{
					IElement element = base.Owner.CreateElement(TagNames.Tbody);
					base.AppendChild(element);
				}
				bodies[bodies.Length - 1].AppendChild(htmlTableRowElement);
			}
			else
			{
				rows[rows.Length - 1].ParentElement.AppendChild(htmlTableRowElement);
			}
			return htmlTableRowElement;
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x00055114 File Offset: 0x00053314
		public void RemoveRowAt(int index)
		{
			IHtmlCollection<IHtmlTableRowElement> rows = this.Rows;
			if (index >= 0 && index < rows.Length)
			{
				rows[index].Remove();
			}
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x00055144 File Offset: 0x00053344
		public IHtmlTableSectionElement CreateHead()
		{
			IHtmlTableSectionElement htmlTableSectionElement = this.Head;
			if (htmlTableSectionElement == null)
			{
				htmlTableSectionElement = base.Owner.CreateElement(TagNames.Thead) as IHtmlTableSectionElement;
				base.AppendChild(htmlTableSectionElement);
			}
			return htmlTableSectionElement;
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x0005517C File Offset: 0x0005337C
		public IHtmlTableSectionElement CreateBody()
		{
			IHtmlTableSectionElement htmlTableSectionElement = this.Bodies.LastOrDefault<IHtmlTableSectionElement>();
			IHtmlTableSectionElement htmlTableSectionElement2 = base.Owner.CreateElement(TagNames.Tbody) as IHtmlTableSectionElement;
			int length = base.ChildNodes.Length;
			int num = ((htmlTableSectionElement != null) ? (htmlTableSectionElement.Index() + 1) : length);
			if (num == length)
			{
				base.AppendChild(htmlTableSectionElement2);
			}
			else
			{
				base.InsertChild(num, htmlTableSectionElement2);
			}
			return htmlTableSectionElement2;
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x000551DE File Offset: 0x000533DE
		public void DeleteHead()
		{
			IHtmlTableSectionElement head = this.Head;
			if (head == null)
			{
				return;
			}
			head.Remove();
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x000551F0 File Offset: 0x000533F0
		public IHtmlTableSectionElement CreateFoot()
		{
			IHtmlTableSectionElement htmlTableSectionElement = this.Foot;
			if (htmlTableSectionElement == null)
			{
				htmlTableSectionElement = base.Owner.CreateElement(TagNames.Tfoot) as IHtmlTableSectionElement;
				base.AppendChild(htmlTableSectionElement);
			}
			return htmlTableSectionElement;
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x00055226 File Offset: 0x00053426
		public void DeleteFoot()
		{
			IHtmlTableSectionElement foot = this.Foot;
			if (foot == null)
			{
				return;
			}
			foot.Remove();
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x00055238 File Offset: 0x00053438
		public IHtmlTableCaptionElement CreateCaption()
		{
			IHtmlTableCaptionElement htmlTableCaptionElement = this.Caption;
			if (htmlTableCaptionElement == null)
			{
				htmlTableCaptionElement = base.Owner.CreateElement(TagNames.Caption) as IHtmlTableCaptionElement;
				base.InsertChild(0, htmlTableCaptionElement);
			}
			return htmlTableCaptionElement;
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x0005526F File Offset: 0x0005346F
		public void DeleteCaption()
		{
			IHtmlTableCaptionElement caption = this.Caption;
			if (caption == null)
			{
				return;
			}
			caption.Remove();
		}

		// Token: 0x04000CFD RID: 3325
		private HtmlCollection<IHtmlTableSectionElement> _bodies;

		// Token: 0x04000CFE RID: 3326
		private HtmlCollection<IHtmlTableRowElement> _rows;
	}
}
