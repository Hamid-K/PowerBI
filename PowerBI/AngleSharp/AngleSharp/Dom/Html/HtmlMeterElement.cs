using System;
using System.Globalization;
using AngleSharp.Dom.Collections;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000374 RID: 884
	internal sealed class HtmlMeterElement : HtmlElement, IHtmlMeterElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, ILabelabelElement
	{
		// Token: 0x06001BE8 RID: 7144 RVA: 0x00053BA1 File Offset: 0x00051DA1
		public HtmlMeterElement(Document owner, string prefix = null)
			: base(owner, TagNames.Meter, prefix, NodeFlags.None)
		{
			this._labels = new NodeList();
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06001BE9 RID: 7145 RVA: 0x00053BBC File Offset: 0x00051DBC
		public INodeList Labels
		{
			get
			{
				return this._labels;
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06001BEA RID: 7146 RVA: 0x00053BC4 File Offset: 0x00051DC4
		// (set) Token: 0x06001BEB RID: 7147 RVA: 0x00053BF0 File Offset: 0x00051DF0
		public double Value
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Value).ToDouble(0.0).Constraint(this.Minimum, this.Maximum);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Value, value.ToString(NumberFormatInfo.InvariantInfo), false);
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06001BEC RID: 7148 RVA: 0x00053C0A File Offset: 0x00051E0A
		// (set) Token: 0x06001BED RID: 7149 RVA: 0x00053C39 File Offset: 0x00051E39
		public double Maximum
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Max).ToDouble(1.0).Constraint(this.Minimum, double.PositiveInfinity);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Max, value.ToString(NumberFormatInfo.InvariantInfo), false);
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06001BEE RID: 7150 RVA: 0x00053C53 File Offset: 0x00051E53
		// (set) Token: 0x06001BEF RID: 7151 RVA: 0x00053C6E File Offset: 0x00051E6E
		public double Minimum
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Min).ToDouble(0.0);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Min, value.ToString(NumberFormatInfo.InvariantInfo), false);
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x00053C88 File Offset: 0x00051E88
		// (set) Token: 0x06001BF1 RID: 7153 RVA: 0x00053CB1 File Offset: 0x00051EB1
		public double Low
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Low).ToDouble(this.Minimum).Constraint(this.Minimum, this.Maximum);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Low, value.ToString(NumberFormatInfo.InvariantInfo), false);
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x00053CCB File Offset: 0x00051ECB
		// (set) Token: 0x06001BF3 RID: 7155 RVA: 0x00053CF4 File Offset: 0x00051EF4
		public double High
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.High).ToDouble(this.Maximum).Constraint(this.Low, this.Maximum);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.High, value.ToString(NumberFormatInfo.InvariantInfo), false);
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06001BF4 RID: 7156 RVA: 0x00053D0E File Offset: 0x00051F0E
		// (set) Token: 0x06001BF5 RID: 7157 RVA: 0x00053D48 File Offset: 0x00051F48
		public double Optimum
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Optimum).ToDouble((this.Maximum + this.Minimum) * 0.5).Constraint(this.Minimum, this.Maximum);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Optimum, value.ToString(NumberFormatInfo.InvariantInfo), false);
			}
		}

		// Token: 0x04000CEE RID: 3310
		private readonly NodeList _labels;
	}
}
