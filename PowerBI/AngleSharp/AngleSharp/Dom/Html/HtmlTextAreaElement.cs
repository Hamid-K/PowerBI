using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003A1 RID: 929
	internal sealed class HtmlTextAreaElement : HtmlTextFormControlElement, IHtmlTextAreaElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, IValidation
	{
		// Token: 0x06001D25 RID: 7461 RVA: 0x00055578 File Offset: 0x00053778
		public HtmlTextAreaElement(Document owner, string prefix = null)
			: base(owner, TagNames.Textarea, prefix, NodeFlags.LineTolerance)
		{
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06001D26 RID: 7462 RVA: 0x00055588 File Offset: 0x00053788
		// (set) Token: 0x06001D27 RID: 7463 RVA: 0x00055595 File Offset: 0x00053795
		public string Wrap
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Wrap);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Wrap, value, false);
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06001D28 RID: 7464 RVA: 0x0004FCC7 File Offset: 0x0004DEC7
		// (set) Token: 0x06001D29 RID: 7465 RVA: 0x0004FCCF File Offset: 0x0004DECF
		public override string DefaultValue
		{
			get
			{
				return this.TextContent;
			}
			set
			{
				this.TextContent = value;
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06001D2A RID: 7466 RVA: 0x000555A4 File Offset: 0x000537A4
		public int TextLength
		{
			get
			{
				return base.Value.Length;
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06001D2B RID: 7467 RVA: 0x000555B1 File Offset: 0x000537B1
		// (set) Token: 0x06001D2C RID: 7468 RVA: 0x000526C4 File Offset: 0x000508C4
		public int Rows
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Rows).ToInteger(2);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Rows, value.ToString(), false);
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06001D2D RID: 7469 RVA: 0x000555C4 File Offset: 0x000537C4
		// (set) Token: 0x06001D2E RID: 7470 RVA: 0x0005269C File Offset: 0x0005089C
		public int Columns
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Cols).ToInteger(20);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Cols, value.ToString(), false);
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06001D2F RID: 7471 RVA: 0x000555D8 File Offset: 0x000537D8
		public string Type
		{
			get
			{
				return TagNames.Textarea;
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06001D30 RID: 7472 RVA: 0x00052EE5 File Offset: 0x000510E5
		internal bool IsMutable
		{
			get
			{
				return !base.IsDisabled && !base.IsReadOnly;
			}
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x000555DF File Offset: 0x000537DF
		internal override void ConstructDataSet(FormDataSet dataSet, IHtmlElement submitter)
		{
			base.ConstructDataSet(dataSet, this.Type);
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x000555EE File Offset: 0x000537EE
		internal override FormControlState SaveControlState()
		{
			return new FormControlState(base.Name, this.Type, base.Value);
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x00055607 File Offset: 0x00053807
		internal override void RestoreFormControlState(FormControlState state)
		{
			if (state.Type.Is(this.Type) && state.Name.Is(base.Name))
			{
				base.Value = state.Value;
			}
		}
	}
}
