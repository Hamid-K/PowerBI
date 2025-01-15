using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000349 RID: 841
	internal sealed class HtmlButtonElement : HtmlFormControlElement, IHtmlButtonElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, IValidation
	{
		// Token: 0x06001962 RID: 6498 RVA: 0x0004FF2D File Offset: 0x0004E12D
		public HtmlButtonElement(Document owner, string prefix = null)
			: base(owner, TagNames.Button, prefix, NodeFlags.None)
		{
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06001963 RID: 6499 RVA: 0x0004FF3D File Offset: 0x0004E13D
		// (set) Token: 0x06001964 RID: 6500 RVA: 0x0004FF58 File Offset: 0x0004E158
		public string Type
		{
			get
			{
				return (this.GetOwnAttribute(AttributeNames.Type) ?? InputTypeNames.Submit).ToLowerInvariant();
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Type, value, false);
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001965 RID: 6501 RVA: 0x0004FF68 File Offset: 0x0004E168
		// (set) Token: 0x06001966 RID: 6502 RVA: 0x0004FF8C File Offset: 0x0004E18C
		public string FormAction
		{
			get
			{
				IHtmlFormElement form = base.Form;
				if (form == null)
				{
					return string.Empty;
				}
				return form.Action;
			}
			set
			{
				IHtmlFormElement form = base.Form;
				if (form != null)
				{
					form.Action = value;
				}
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001967 RID: 6503 RVA: 0x0004FFAC File Offset: 0x0004E1AC
		// (set) Token: 0x06001968 RID: 6504 RVA: 0x0004FFD0 File Offset: 0x0004E1D0
		public string FormEncType
		{
			get
			{
				IHtmlFormElement form = base.Form;
				if (form == null)
				{
					return string.Empty;
				}
				return form.Enctype;
			}
			set
			{
				IHtmlFormElement form = base.Form;
				if (form != null)
				{
					form.Enctype = value;
				}
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06001969 RID: 6505 RVA: 0x0004FFF0 File Offset: 0x0004E1F0
		// (set) Token: 0x0600196A RID: 6506 RVA: 0x00050014 File Offset: 0x0004E214
		public string FormMethod
		{
			get
			{
				IHtmlFormElement form = base.Form;
				if (form == null)
				{
					return string.Empty;
				}
				return form.Method;
			}
			set
			{
				IHtmlFormElement form = base.Form;
				if (form != null)
				{
					form.Method = value;
				}
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x0600196B RID: 6507 RVA: 0x00050034 File Offset: 0x0004E234
		// (set) Token: 0x0600196C RID: 6508 RVA: 0x00050054 File Offset: 0x0004E254
		public bool FormNoValidate
		{
			get
			{
				IHtmlFormElement form = base.Form;
				return form != null && form.NoValidate;
			}
			set
			{
				IHtmlFormElement form = base.Form;
				if (form != null)
				{
					form.NoValidate = value;
				}
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x0600196D RID: 6509 RVA: 0x00050074 File Offset: 0x0004E274
		// (set) Token: 0x0600196E RID: 6510 RVA: 0x00050098 File Offset: 0x0004E298
		public string FormTarget
		{
			get
			{
				IHtmlFormElement form = base.Form;
				if (form == null)
				{
					return string.Empty;
				}
				return form.Target;
			}
			set
			{
				IHtmlFormElement form = base.Form;
				if (form != null)
				{
					form.Target = value;
				}
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x0600196F RID: 6511 RVA: 0x000500B6 File Offset: 0x0004E2B6
		// (set) Token: 0x06001970 RID: 6512 RVA: 0x000500CC File Offset: 0x0004E2CC
		public string Value
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Value) ?? string.Empty;
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Value, value, false);
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001971 RID: 6513 RVA: 0x000500DB File Offset: 0x0004E2DB
		// (set) Token: 0x06001972 RID: 6514 RVA: 0x000500E3 File Offset: 0x0004E2E3
		internal bool IsVisited { get; set; }

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001973 RID: 6515 RVA: 0x000500EC File Offset: 0x0004E2EC
		// (set) Token: 0x06001974 RID: 6516 RVA: 0x000500F4 File Offset: 0x0004E2F4
		internal bool IsActive { get; set; }

		// Token: 0x06001975 RID: 6517 RVA: 0x00050100 File Offset: 0x0004E300
		public override void DoClick()
		{
			IHtmlFormElement form = base.Form;
			if (!base.IsClickedCancelled() && form != null)
			{
				string type = this.Type;
				if (type.Is(InputTypeNames.Submit))
				{
					form.SubmitAsync(this);
					return;
				}
				if (type.Is(InputTypeNames.Reset))
				{
					form.Reset();
				}
			}
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x0005014F File Offset: 0x0004E34F
		protected override bool CanBeValidated()
		{
			return this.Type.Is(InputTypeNames.Submit) && !this.HasDataListAncestor();
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x00050170 File Offset: 0x0004E370
		internal override void ConstructDataSet(FormDataSet dataSet, IHtmlElement submitter)
		{
			string type = this.Type;
			if (this == submitter && type.IsOneOf(InputTypeNames.Submit, InputTypeNames.Reset))
			{
				dataSet.Append(base.Name, this.Value, type);
			}
		}
	}
}
