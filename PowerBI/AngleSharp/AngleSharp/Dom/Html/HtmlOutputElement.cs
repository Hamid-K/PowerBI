using System;
using AngleSharp.Dom.Collections;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200037E RID: 894
	internal sealed class HtmlOutputElement : HtmlFormControlElement, IHtmlOutputElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, IValidation
	{
		// Token: 0x06001C2F RID: 7215 RVA: 0x0005406A File Offset: 0x0005226A
		public HtmlOutputElement(Document owner, string prefix = null)
			: base(owner, TagNames.Output, prefix, NodeFlags.None)
		{
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06001C30 RID: 7216 RVA: 0x0005407A File Offset: 0x0005227A
		// (set) Token: 0x06001C31 RID: 7217 RVA: 0x0005408C File Offset: 0x0005228C
		public string DefaultValue
		{
			get
			{
				return this._defaultValue ?? this.TextContent;
			}
			set
			{
				this._defaultValue = value;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06001C32 RID: 7218 RVA: 0x00054095 File Offset: 0x00052295
		// (set) Token: 0x06001C33 RID: 7219 RVA: 0x000540B1 File Offset: 0x000522B1
		public override string TextContent
		{
			get
			{
				string text;
				if ((text = this._value) == null)
				{
					text = this._defaultValue ?? base.TextContent;
				}
				return text;
			}
			set
			{
				base.TextContent = value;
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06001C34 RID: 7220 RVA: 0x0004FCC7 File Offset: 0x0004DEC7
		// (set) Token: 0x06001C35 RID: 7221 RVA: 0x000540BA File Offset: 0x000522BA
		public string Value
		{
			get
			{
				return this.TextContent;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06001C36 RID: 7222 RVA: 0x000540C3 File Offset: 0x000522C3
		public ISettableTokenList HtmlFor
		{
			get
			{
				if (this._for == null)
				{
					this._for = new SettableTokenList(this.GetOwnAttribute(AttributeNames.For));
					this._for.Changed += delegate(string value)
					{
						base.UpdateAttribute(AttributeNames.For, value);
					};
				}
				return this._for;
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06001C37 RID: 7223 RVA: 0x00054100 File Offset: 0x00052300
		public string Type
		{
			get
			{
				return TagNames.Output;
			}
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x00054107 File Offset: 0x00052307
		internal override void Reset()
		{
			this._value = null;
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x00054110 File Offset: 0x00052310
		internal void UpdateFor(string value)
		{
			SettableTokenList @for = this._for;
			if (@for == null)
			{
				return;
			}
			@for.Update(value);
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x0002F0AA File Offset: 0x0002D2AA
		protected override bool CanBeValidated()
		{
			return true;
		}

		// Token: 0x04000CF1 RID: 3313
		private string _defaultValue;

		// Token: 0x04000CF2 RID: 3314
		private string _value;

		// Token: 0x04000CF3 RID: 3315
		private SettableTokenList _for;
	}
}
