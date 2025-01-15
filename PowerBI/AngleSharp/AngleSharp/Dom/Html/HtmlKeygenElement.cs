using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000369 RID: 873
	internal sealed class HtmlKeygenElement : HtmlFormControlElementWithState, IHtmlKeygenElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, IValidation
	{
		// Token: 0x06001B3E RID: 6974 RVA: 0x00053009 File Offset: 0x00051209
		public HtmlKeygenElement(Document owner, string prefix = null)
			: base(owner, TagNames.Keygen, prefix, NodeFlags.SelfClosing)
		{
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06001B3F RID: 6975 RVA: 0x00053019 File Offset: 0x00051219
		// (set) Token: 0x06001B40 RID: 6976 RVA: 0x00053026 File Offset: 0x00051226
		public string Challenge
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Challenge);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Challenge, value, false);
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06001B41 RID: 6977 RVA: 0x00053035 File Offset: 0x00051235
		// (set) Token: 0x06001B42 RID: 6978 RVA: 0x00053042 File Offset: 0x00051242
		public string KeyEncryption
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Keytype);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Keytype, value, false);
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06001B43 RID: 6979 RVA: 0x00053051 File Offset: 0x00051251
		public string Type
		{
			get
			{
				return TagNames.Keygen;
			}
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x00053058 File Offset: 0x00051258
		internal override FormControlState SaveControlState()
		{
			return new FormControlState(base.Name, this.Type, this.Challenge);
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x00053071 File Offset: 0x00051271
		internal override void RestoreFormControlState(FormControlState state)
		{
			if (state.Type.Is(this.Type) && state.Name.Is(base.Name))
			{
				this.Challenge = state.Value;
			}
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x0000EE9F File Offset: 0x0000D09F
		protected override bool CanBeValidated()
		{
			return false;
		}
	}
}
