using System;
using System.Linq;
using AngleSharp.Dom.Collections;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200038C RID: 908
	internal sealed class HtmlSelectElement : HtmlFormControlElementWithState, IHtmlSelectElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, IValidation
	{
		// Token: 0x06001C6F RID: 7279 RVA: 0x0005454A File Offset: 0x0005274A
		public HtmlSelectElement(Document owner, string prefix = null)
			: base(owner, TagNames.Select, prefix, NodeFlags.None)
		{
		}

		// Token: 0x1700083F RID: 2111
		public IHtmlOptionElement this[int index]
		{
			get
			{
				return this.Options.GetOptionAt(index);
			}
			set
			{
				this.Options.SetOptionAt(index, value);
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06001C72 RID: 7282 RVA: 0x00054577 File Offset: 0x00052777
		// (set) Token: 0x06001C73 RID: 7283 RVA: 0x00052D45 File Offset: 0x00050F45
		public int Size
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Size).ToInteger(0);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Size, value.ToString(), false);
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06001C74 RID: 7284 RVA: 0x0005458A File Offset: 0x0005278A
		// (set) Token: 0x06001C75 RID: 7285 RVA: 0x00054597 File Offset: 0x00052797
		public bool IsRequired
		{
			get
			{
				return this.GetBoolAttribute(AttributeNames.Required);
			}
			set
			{
				this.SetBoolAttribute(AttributeNames.Required, value);
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06001C76 RID: 7286 RVA: 0x000545A8 File Offset: 0x000527A8
		public IHtmlCollection<IHtmlOptionElement> SelectedOptions
		{
			get
			{
				HtmlCollection<IHtmlOptionElement> htmlCollection;
				if ((htmlCollection = this._selected) == null)
				{
					htmlCollection = (this._selected = new HtmlCollection<IHtmlOptionElement>(this.Options.Where((IHtmlOptionElement m) => m.IsSelected)));
				}
				return htmlCollection;
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06001C77 RID: 7287 RVA: 0x000545F7 File Offset: 0x000527F7
		public int SelectedIndex
		{
			get
			{
				return this.Options.SelectedIndex;
			}
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06001C78 RID: 7288 RVA: 0x00054604 File Offset: 0x00052804
		// (set) Token: 0x06001C79 RID: 7289 RVA: 0x00054660 File Offset: 0x00052860
		public string Value
		{
			get
			{
				foreach (IHtmlOptionElement htmlOptionElement in this.Options)
				{
					if (htmlOptionElement.IsSelected)
					{
						return htmlOptionElement.Value;
					}
				}
				return null;
			}
			set
			{
				this.UpdateValue(value);
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06001C7A RID: 7290 RVA: 0x00054669 File Offset: 0x00052869
		public int Length
		{
			get
			{
				return this.Options.Length;
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06001C7B RID: 7291 RVA: 0x00052A41 File Offset: 0x00050C41
		// (set) Token: 0x06001C7C RID: 7292 RVA: 0x00052A4E File Offset: 0x00050C4E
		public bool IsMultiple
		{
			get
			{
				return this.GetBoolAttribute(AttributeNames.Multiple);
			}
			set
			{
				this.SetBoolAttribute(AttributeNames.Multiple, value);
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06001C7D RID: 7293 RVA: 0x00054678 File Offset: 0x00052878
		public IHtmlOptionsCollection Options
		{
			get
			{
				OptionsCollection optionsCollection;
				if ((optionsCollection = this._options) == null)
				{
					optionsCollection = (this._options = new OptionsCollection(this));
				}
				return optionsCollection;
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06001C7E RID: 7294 RVA: 0x0005469E File Offset: 0x0005289E
		public string Type
		{
			get
			{
				if (!this.IsMultiple)
				{
					return InputTypeNames.SelectOne;
				}
				return InputTypeNames.SelectMultiple;
			}
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x000546B3 File Offset: 0x000528B3
		public void AddOption(IHtmlOptionElement element, IHtmlElement before = null)
		{
			this.Options.Add(element, before);
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x000546C2 File Offset: 0x000528C2
		public void AddOption(IHtmlOptionsGroupElement element, IHtmlElement before = null)
		{
			this.Options.Add(element, before);
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x000546D1 File Offset: 0x000528D1
		public void RemoveOptionAt(int index)
		{
			this.Options.Remove(index);
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x000546DF File Offset: 0x000528DF
		internal override FormControlState SaveControlState()
		{
			return new FormControlState(base.Name, this.Type, this.Value);
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x000546F8 File Offset: 0x000528F8
		internal override void RestoreFormControlState(FormControlState state)
		{
			if (state.Type.Is(this.Type) && state.Name.Is(base.Name))
			{
				this.Value = state.Value;
			}
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x0005472C File Offset: 0x0005292C
		internal override void ConstructDataSet(FormDataSet dataSet, IHtmlElement submitter)
		{
			IHtmlOptionsCollection options = this.Options;
			for (int i = 0; i < options.Length; i++)
			{
				IHtmlOptionElement optionAt = options.GetOptionAt(i);
				if (optionAt.IsSelected && !optionAt.IsDisabled)
				{
					dataSet.Append(base.Name, optionAt.Value, this.Type);
				}
			}
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x00054784 File Offset: 0x00052984
		internal override void SetupElement()
		{
			base.SetupElement();
			string ownAttribute = this.GetOwnAttribute(AttributeNames.Value);
			if (ownAttribute != null)
			{
				this.UpdateValue(ownAttribute);
			}
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x000547B0 File Offset: 0x000529B0
		internal override void Reset()
		{
			IHtmlOptionsCollection options = this.Options;
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < options.Length; i++)
			{
				IHtmlOptionElement optionAt = options.GetOptionAt(i);
				optionAt.IsSelected = optionAt.IsDefaultSelected;
				if (optionAt.IsSelected)
				{
					num2 = i;
					num++;
				}
			}
			if (num != 1 && !this.IsMultiple && options.Length > 0)
			{
				foreach (IHtmlOptionElement htmlOptionElement in options)
				{
					htmlOptionElement.IsSelected = false;
				}
				options[num2].IsSelected = true;
			}
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x0005485C File Offset: 0x00052A5C
		internal void UpdateValue(string value)
		{
			foreach (IHtmlOptionElement htmlOptionElement in this.Options)
			{
				bool flag = htmlOptionElement.Value.Isi(value);
				htmlOptionElement.IsSelected = flag;
			}
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x000548B4 File Offset: 0x00052AB4
		protected override bool CanBeValidated()
		{
			return !this.HasDataListAncestor();
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x000548C0 File Offset: 0x00052AC0
		protected override void Check(ValidityState state)
		{
			string value = this.Value;
			state.IsValueMissing = this.IsRequired && string.IsNullOrEmpty(value);
		}

		// Token: 0x04000CF9 RID: 3321
		private OptionsCollection _options;

		// Token: 0x04000CFA RID: 3322
		private HtmlCollection<IHtmlOptionElement> _selected;
	}
}
