using System;
using System.Linq;
using AngleSharp.Dom.Collections;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000359 RID: 857
	internal abstract class HtmlFormControlElement : HtmlElement, ILabelabelElement, IValidation
	{
		// Token: 0x06001A56 RID: 6742 RVA: 0x00051B2C File Offset: 0x0004FD2C
		public HtmlFormControlElement(Document owner, string name, string prefix, NodeFlags flags = NodeFlags.None)
			: base(owner, name, prefix, flags | NodeFlags.Special)
		{
			this._vstate = new ValidityState();
			this._labels = new NodeList();
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06001A57 RID: 6743 RVA: 0x0004FCAB File Offset: 0x0004DEAB
		// (set) Token: 0x06001A58 RID: 6744 RVA: 0x0004FCB8 File Offset: 0x0004DEB8
		public string Name
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Name);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Name, value, false);
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06001A59 RID: 6745 RVA: 0x00051B51 File Offset: 0x0004FD51
		public IHtmlFormElement Form
		{
			get
			{
				return base.GetAssignedForm();
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06001A5A RID: 6746 RVA: 0x00051B59 File Offset: 0x0004FD59
		// (set) Token: 0x06001A5B RID: 6747 RVA: 0x00051B70 File Offset: 0x0004FD70
		public bool IsDisabled
		{
			get
			{
				return this.GetBoolAttribute(AttributeNames.Disabled) || this.IsFieldsetDisabled();
			}
			set
			{
				this.SetBoolAttribute(AttributeNames.Disabled, value);
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06001A5C RID: 6748 RVA: 0x00051B7E File Offset: 0x0004FD7E
		// (set) Token: 0x06001A5D RID: 6749 RVA: 0x00051B8B File Offset: 0x0004FD8B
		public bool Autofocus
		{
			get
			{
				return this.GetBoolAttribute(AttributeNames.AutoFocus);
			}
			set
			{
				this.SetBoolAttribute(AttributeNames.AutoFocus, value);
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06001A5E RID: 6750 RVA: 0x00051B99 File Offset: 0x0004FD99
		public INodeList Labels
		{
			get
			{
				return this._labels;
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06001A5F RID: 6751 RVA: 0x00051BA1 File Offset: 0x0004FDA1
		public string ValidationMessage
		{
			get
			{
				if (!this._vstate.IsCustomError)
				{
					return string.Empty;
				}
				return this._error;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06001A60 RID: 6752 RVA: 0x00051BBC File Offset: 0x0004FDBC
		public bool WillValidate
		{
			get
			{
				return !this.IsDisabled && this.CanBeValidated();
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06001A61 RID: 6753 RVA: 0x00051BCE File Offset: 0x0004FDCE
		public IValidityState Validity
		{
			get
			{
				this.Check(this._vstate);
				return this._vstate;
			}
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x00051BE2 File Offset: 0x0004FDE2
		public override INode Clone(bool deep = true)
		{
			HtmlFormControlElement htmlFormControlElement = (HtmlFormControlElement)base.Clone(deep);
			htmlFormControlElement.SetCustomValidity(this._error);
			return htmlFormControlElement;
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x00051BFC File Offset: 0x0004FDFC
		public bool CheckValidity()
		{
			return this.WillValidate && this.Validity.IsValid;
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x00051C13 File Offset: 0x0004FE13
		public void SetCustomValidity(string error)
		{
			this._vstate.IsCustomError = !string.IsNullOrEmpty(error);
			this._error = error;
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x00051C30 File Offset: 0x0004FE30
		protected virtual bool IsFieldsetDisabled()
		{
			foreach (IHtmlFieldSetElement htmlFieldSetElement in this.GetAncestors().OfType<IHtmlFieldSetElement>())
			{
				if (htmlFieldSetElement.IsDisabled)
				{
					INode node = htmlFieldSetElement.ChildNodes.FirstOrDefault((INode m) => m is IHtmlLegendElement);
					return !this.IsDescendantOf(node);
				}
			}
			return false;
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x00003C25 File Offset: 0x00001E25
		internal virtual void ConstructDataSet(FormDataSet dataSet, IHtmlElement submitter)
		{
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x00003C25 File Offset: 0x00001E25
		internal virtual void Reset()
		{
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x00003C25 File Offset: 0x00001E25
		protected virtual void Check(ValidityState state)
		{
		}

		// Token: 0x06001A69 RID: 6761
		protected abstract bool CanBeValidated();

		// Token: 0x04000CCC RID: 3276
		private readonly NodeList _labels;

		// Token: 0x04000CCD RID: 3277
		private readonly ValidityState _vstate;

		// Token: 0x04000CCE RID: 3278
		private string _error;
	}
}
