using System;
using AngleSharp.Dom.Collections;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000357 RID: 855
	internal sealed class HtmlFieldSetElement : HtmlFormControlElement, IHtmlFieldSetElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, IValidation
	{
		// Token: 0x06001A50 RID: 6736 RVA: 0x00051AD2 File Offset: 0x0004FCD2
		public HtmlFieldSetElement(Document owner, string prefix = null)
			: base(owner, TagNames.Fieldset, prefix, NodeFlags.None)
		{
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06001A51 RID: 6737 RVA: 0x00051AE2 File Offset: 0x0004FCE2
		public string Type
		{
			get
			{
				return TagNames.Fieldset;
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06001A52 RID: 6738 RVA: 0x00051AEC File Offset: 0x0004FCEC
		public IHtmlFormControlsCollection Elements
		{
			get
			{
				HtmlFormControlsCollection htmlFormControlsCollection;
				if ((htmlFormControlsCollection = this._elements) == null)
				{
					htmlFormControlsCollection = (this._elements = new HtmlFormControlsCollection(base.Form, this));
				}
				return htmlFormControlsCollection;
			}
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x0000EE9F File Offset: 0x0000D09F
		protected override bool IsFieldsetDisabled()
		{
			return false;
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x0002F0AA File Offset: 0x0002D2AA
		protected override bool CanBeValidated()
		{
			return true;
		}

		// Token: 0x04000CCB RID: 3275
		private HtmlFormControlsCollection _elements;
	}
}
