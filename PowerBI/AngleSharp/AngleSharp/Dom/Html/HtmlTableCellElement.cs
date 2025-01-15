using System;
using AngleSharp.Dom.Collections;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000397 RID: 919
	internal abstract class HtmlTableCellElement : HtmlElement, IHtmlTableCellElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001CB4 RID: 7348 RVA: 0x00054BDD File Offset: 0x00052DDD
		public HtmlTableCellElement(Document owner, string name, string prefix)
			: base(owner, name, prefix, NodeFlags.Special | NodeFlags.ImplicitelyClosed | NodeFlags.Scoped)
		{
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06001CB5 RID: 7349 RVA: 0x00054BEC File Offset: 0x00052DEC
		public int Index
		{
			get
			{
				IElement element = base.ParentElement;
				while (element != null && !(element is IHtmlTableRowElement))
				{
					element = element.ParentElement;
				}
				HtmlTableRowElement htmlTableRowElement = element as HtmlTableRowElement;
				if (htmlTableRowElement == null)
				{
					return -1;
				}
				return htmlTableRowElement.IndexOf(this);
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06001CB6 RID: 7350 RVA: 0x00054142 File Offset: 0x00052342
		// (set) Token: 0x06001CB7 RID: 7351 RVA: 0x00054155 File Offset: 0x00052355
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

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06001CB8 RID: 7352 RVA: 0x00054C26 File Offset: 0x00052E26
		// (set) Token: 0x06001CB9 RID: 7353 RVA: 0x00054C39 File Offset: 0x00052E39
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

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06001CBA RID: 7354 RVA: 0x0004FE99 File Offset: 0x0004E099
		// (set) Token: 0x06001CBB RID: 7355 RVA: 0x0004FEA6 File Offset: 0x0004E0A6
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

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06001CBC RID: 7356 RVA: 0x00051A34 File Offset: 0x0004FC34
		// (set) Token: 0x06001CBD RID: 7357 RVA: 0x00051A41 File Offset: 0x0004FC41
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

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06001CBE RID: 7358 RVA: 0x00051A50 File Offset: 0x0004FC50
		// (set) Token: 0x06001CBF RID: 7359 RVA: 0x00051A5D File Offset: 0x0004FC5D
		public string Height
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Height);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Height, value, false);
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06001CC0 RID: 7360 RVA: 0x00054C54 File Offset: 0x00052E54
		// (set) Token: 0x06001CC1 RID: 7361 RVA: 0x00054C6C File Offset: 0x00052E6C
		public int ColumnSpan
		{
			get
			{
				return HtmlTableCellElement.LimitColSpan(this.GetOwnAttribute(AttributeNames.ColSpan).ToInteger(1));
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.ColSpan, value.ToString(), false);
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x00054C81 File Offset: 0x00052E81
		// (set) Token: 0x06001CC3 RID: 7363 RVA: 0x00054C99 File Offset: 0x00052E99
		public int RowSpan
		{
			get
			{
				return HtmlTableCellElement.LimitRowSpan(this.GetOwnAttribute(AttributeNames.RowSpan).ToInteger(1));
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.RowSpan, value.ToString(), false);
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06001CC4 RID: 7364 RVA: 0x00054CAE File Offset: 0x00052EAE
		// (set) Token: 0x06001CC5 RID: 7365 RVA: 0x00054CC1 File Offset: 0x00052EC1
		public bool NoWrap
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.NoWrap).ToBoolean(false);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.NoWrap, value.ToString(), false);
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06001CC6 RID: 7366 RVA: 0x00054CD6 File Offset: 0x00052ED6
		// (set) Token: 0x06001CC7 RID: 7367 RVA: 0x00054CE3 File Offset: 0x00052EE3
		public string Abbr
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Abbr);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Abbr, value, false);
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06001CC8 RID: 7368 RVA: 0x00054CF2 File Offset: 0x00052EF2
		// (set) Token: 0x06001CC9 RID: 7369 RVA: 0x00054CFF File Offset: 0x00052EFF
		public string Scope
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Scope);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Scope, value, false);
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06001CCA RID: 7370 RVA: 0x00054D0E File Offset: 0x00052F0E
		public ISettableTokenList Headers
		{
			get
			{
				if (this._headers == null)
				{
					this._headers = new SettableTokenList(this.GetOwnAttribute(AttributeNames.Headers));
					this._headers.Changed += delegate(string value)
					{
						base.UpdateAttribute(AttributeNames.Headers, value);
					};
				}
				return this._headers;
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06001CCB RID: 7371 RVA: 0x00054D4B File Offset: 0x00052F4B
		// (set) Token: 0x06001CCC RID: 7372 RVA: 0x00054D58 File Offset: 0x00052F58
		public string Axis
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Axis);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Axis, value, false);
			}
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x00054D67 File Offset: 0x00052F67
		internal void UpdateHeaders(string value)
		{
			SettableTokenList headers = this._headers;
			if (headers == null)
			{
				return;
			}
			headers.Update(value);
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x00054D7A File Offset: 0x00052F7A
		private static int LimitColSpan(int value)
		{
			if (value < 1 || value > 1000)
			{
				return 1;
			}
			return value;
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x00054D8B File Offset: 0x00052F8B
		private static int LimitRowSpan(int value)
		{
			if (value < 0)
			{
				return 1;
			}
			return Math.Min(value, 65534);
		}

		// Token: 0x04000CFC RID: 3324
		private SettableTokenList _headers;
	}
}
