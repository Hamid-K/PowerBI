using System;
using System.Globalization;
using AngleSharp.Dom.Collections;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000384 RID: 900
	internal sealed class HtmlProgressElement : HtmlElement, IHtmlProgressElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, ILabelabelElement
	{
		// Token: 0x06001C47 RID: 7239 RVA: 0x000541B1 File Offset: 0x000523B1
		public HtmlProgressElement(Document owner, string prefix = null)
			: base(owner, TagNames.Progress, prefix, NodeFlags.None)
		{
			this._labels = new NodeList();
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06001C48 RID: 7240 RVA: 0x000541CC File Offset: 0x000523CC
		public INodeList Labels
		{
			get
			{
				return this._labels;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06001C49 RID: 7241 RVA: 0x000541D4 File Offset: 0x000523D4
		public bool IsDeterminate
		{
			get
			{
				return !string.IsNullOrEmpty(this.GetOwnAttribute(AttributeNames.Value));
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06001C4A RID: 7242 RVA: 0x000541E9 File Offset: 0x000523E9
		// (set) Token: 0x06001C4B RID: 7243 RVA: 0x00053BF0 File Offset: 0x00051DF0
		public double Value
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Value).ToDouble(0.0);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Value, value.ToString(NumberFormatInfo.InvariantInfo), false);
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06001C4C RID: 7244 RVA: 0x00054204 File Offset: 0x00052404
		// (set) Token: 0x06001C4D RID: 7245 RVA: 0x00053C39 File Offset: 0x00051E39
		public double Maximum
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Max).ToDouble(1.0);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Max, value.ToString(NumberFormatInfo.InvariantInfo), false);
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06001C4E RID: 7246 RVA: 0x0005421F File Offset: 0x0005241F
		public double Position
		{
			get
			{
				if (!this.IsDeterminate)
				{
					return -1.0;
				}
				return Math.Max(Math.Min(this.Value / this.Maximum, 1.0), 0.0);
			}
		}

		// Token: 0x04000CF4 RID: 3316
		private readonly NodeList _labels;
	}
}
