using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Dom.Io;
using AngleSharp.Extensions;
using AngleSharp.Html;
using AngleSharp.Html.InputTypes;
using AngleSharp.Services;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000366 RID: 870
	internal sealed class HtmlInputElement : HtmlTextFormControlElement, IHtmlInputElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, IValidation
	{
		// Token: 0x06001AEE RID: 6894 RVA: 0x000529C9 File Offset: 0x00050BC9
		public HtmlInputElement(Document owner, string prefix = null)
			: base(owner, TagNames.Input, prefix, NodeFlags.SelfClosing)
		{
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06001AEF RID: 6895 RVA: 0x000500B6 File Offset: 0x0004E2B6
		// (set) Token: 0x06001AF0 RID: 6896 RVA: 0x000500CC File Offset: 0x0004E2CC
		public override string DefaultValue
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

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x000529D9 File Offset: 0x00050BD9
		// (set) Token: 0x06001AF2 RID: 6898 RVA: 0x000529E6 File Offset: 0x00050BE6
		public bool IsDefaultChecked
		{
			get
			{
				return this.GetBoolAttribute(AttributeNames.Checked);
			}
			set
			{
				this.SetBoolAttribute(AttributeNames.Checked, value);
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x000529F4 File Offset: 0x00050BF4
		// (set) Token: 0x06001AF4 RID: 6900 RVA: 0x00052A15 File Offset: 0x00050C15
		public bool IsChecked
		{
			get
			{
				if (this._checked == null)
				{
					return this.IsDefaultChecked;
				}
				return this._checked.Value;
			}
			set
			{
				this._checked = new bool?(value);
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x00052A23 File Offset: 0x00050C23
		// (set) Token: 0x06001AF6 RID: 6902 RVA: 0x0004FF58 File Offset: 0x0004E158
		public string Type
		{
			get
			{
				return this._type.Name;
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Type, value, false);
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x00052A30 File Offset: 0x00050C30
		// (set) Token: 0x06001AF8 RID: 6904 RVA: 0x00052A38 File Offset: 0x00050C38
		public bool IsIndeterminate { get; set; }

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x00052A41 File Offset: 0x00050C41
		// (set) Token: 0x06001AFA RID: 6906 RVA: 0x00052A4E File Offset: 0x00050C4E
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

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06001AFB RID: 6907 RVA: 0x00052A5C File Offset: 0x00050C5C
		// (set) Token: 0x06001AFC RID: 6908 RVA: 0x00052A6F File Offset: 0x00050C6F
		public DateTime? ValueAsDate
		{
			get
			{
				return this._type.ConvertToDate(base.Value);
			}
			set
			{
				if (value == null)
				{
					base.Value = string.Empty;
					return;
				}
				base.Value = this._type.ConvertFromDate(value.Value);
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06001AFD RID: 6909 RVA: 0x00052AA0 File Offset: 0x00050CA0
		// (set) Token: 0x06001AFE RID: 6910 RVA: 0x00052AD9 File Offset: 0x00050CD9
		public double ValueAsNumber
		{
			get
			{
				double? num = this._type.ConvertToNumber(base.Value);
				if (num == null)
				{
					return double.NaN;
				}
				return num.GetValueOrDefault();
			}
			set
			{
				if (double.IsInfinity(value))
				{
					throw new DomException(DomError.TypeMismatch);
				}
				if (double.IsNaN(value))
				{
					base.Value = string.Empty;
					return;
				}
				base.Value = this._type.ConvertFromNumber(value);
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06001AFF RID: 6911 RVA: 0x00052B14 File Offset: 0x00050D14
		// (set) Token: 0x06001B00 RID: 6912 RVA: 0x00052B38 File Offset: 0x00050D38
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

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001B01 RID: 6913 RVA: 0x00052B58 File Offset: 0x00050D58
		// (set) Token: 0x06001B02 RID: 6914 RVA: 0x00052B7C File Offset: 0x00050D7C
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

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001B03 RID: 6915 RVA: 0x00052B9C File Offset: 0x00050D9C
		// (set) Token: 0x06001B04 RID: 6916 RVA: 0x00052BC0 File Offset: 0x00050DC0
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

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001B05 RID: 6917 RVA: 0x00052BE0 File Offset: 0x00050DE0
		// (set) Token: 0x06001B06 RID: 6918 RVA: 0x00052C00 File Offset: 0x00050E00
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

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06001B07 RID: 6919 RVA: 0x00052C20 File Offset: 0x00050E20
		// (set) Token: 0x06001B08 RID: 6920 RVA: 0x00052C44 File Offset: 0x00050E44
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

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06001B09 RID: 6921 RVA: 0x00052C62 File Offset: 0x00050E62
		// (set) Token: 0x06001B0A RID: 6922 RVA: 0x00052C6F File Offset: 0x00050E6F
		public string Accept
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Accept);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Accept, value, false);
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06001B0B RID: 6923 RVA: 0x00052C7E File Offset: 0x00050E7E
		// (set) Token: 0x06001B0C RID: 6924 RVA: 0x00052761 File Offset: 0x00050961
		public Alignment Align
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Align).ToEnum(Alignment.Left);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Align, value.ToString(), false);
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x0004FD0F File Offset: 0x0004DF0F
		// (set) Token: 0x06001B0E RID: 6926 RVA: 0x0004FD1C File Offset: 0x0004DF1C
		public string AlternativeText
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Alt);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Alt, value, false);
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x00051D97 File Offset: 0x0004FF97
		// (set) Token: 0x06001B10 RID: 6928 RVA: 0x00051DA4 File Offset: 0x0004FFA4
		public string Autocomplete
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.AutoComplete);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.AutoComplete, value, false);
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06001B11 RID: 6929 RVA: 0x00052C91 File Offset: 0x00050E91
		public IFileList Files
		{
			get
			{
				FileInputType fileInputType = this._type as FileInputType;
				if (fileInputType == null)
				{
					return null;
				}
				return fileInputType.Files;
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06001B12 RID: 6930 RVA: 0x00052CAC File Offset: 0x00050EAC
		public IHtmlDataListElement List
		{
			get
			{
				string ownAttribute = this.GetOwnAttribute(AttributeNames.List);
				Document owner = base.Owner;
				return ((owner != null) ? owner.GetElementById(ownAttribute) : null) as IHtmlDataListElement;
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06001B13 RID: 6931 RVA: 0x00052CDD File Offset: 0x00050EDD
		// (set) Token: 0x06001B14 RID: 6932 RVA: 0x00052CEA File Offset: 0x00050EEA
		public string Maximum
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Max);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Max, value, false);
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06001B15 RID: 6933 RVA: 0x00052CF9 File Offset: 0x00050EF9
		// (set) Token: 0x06001B16 RID: 6934 RVA: 0x00052D06 File Offset: 0x00050F06
		public string Minimum
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Min);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Min, value, false);
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06001B17 RID: 6935 RVA: 0x00052D15 File Offset: 0x00050F15
		// (set) Token: 0x06001B18 RID: 6936 RVA: 0x00052D22 File Offset: 0x00050F22
		public string Pattern
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Pattern);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Pattern, value, false);
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06001B19 RID: 6937 RVA: 0x00052D31 File Offset: 0x00050F31
		// (set) Token: 0x06001B1A RID: 6938 RVA: 0x00052D45 File Offset: 0x00050F45
		public int Size
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Size).ToInteger(20);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Size, value.ToString(), false);
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06001B1B RID: 6939 RVA: 0x00051A0B File Offset: 0x0004FC0B
		// (set) Token: 0x06001B1C RID: 6940 RVA: 0x00051A18 File Offset: 0x0004FC18
		public string Source
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Src);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Src, value, false);
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001B1D RID: 6941 RVA: 0x00052D5A File Offset: 0x00050F5A
		// (set) Token: 0x06001B1E RID: 6942 RVA: 0x00052D67 File Offset: 0x00050F67
		public string Step
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Step);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Step, value, false);
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06001B1F RID: 6943 RVA: 0x000528FA File Offset: 0x00050AFA
		// (set) Token: 0x06001B20 RID: 6944 RVA: 0x00052907 File Offset: 0x00050B07
		public string UseMap
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.UseMap);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.UseMap, value, false);
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06001B21 RID: 6945 RVA: 0x00052D76 File Offset: 0x00050F76
		// (set) Token: 0x06001B22 RID: 6946 RVA: 0x000501DB File Offset: 0x0004E3DB
		public int DisplayWidth
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Width).ToInteger(this.OriginalWidth);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Width, value.ToString(), false);
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06001B23 RID: 6947 RVA: 0x00052D8E File Offset: 0x00050F8E
		// (set) Token: 0x06001B24 RID: 6948 RVA: 0x00050207 File Offset: 0x0004E407
		public int DisplayHeight
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Height).ToInteger(this.OriginalHeight);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Height, value.ToString(), false);
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06001B25 RID: 6949 RVA: 0x00052DA6 File Offset: 0x00050FA6
		public int OriginalWidth
		{
			get
			{
				ImageInputType imageInputType = this._type as ImageInputType;
				if (imageInputType == null)
				{
					return 0;
				}
				return imageInputType.Width;
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06001B26 RID: 6950 RVA: 0x00052DBE File Offset: 0x00050FBE
		public int OriginalHeight
		{
			get
			{
				ImageInputType imageInputType = this._type as ImageInputType;
				if (imageInputType == null)
				{
					return 0;
				}
				return imageInputType.Height;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06001B27 RID: 6951 RVA: 0x00052DD6 File Offset: 0x00050FD6
		// (set) Token: 0x06001B28 RID: 6952 RVA: 0x00052DDE File Offset: 0x00050FDE
		internal bool IsVisited { get; set; }

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06001B29 RID: 6953 RVA: 0x00052DE7 File Offset: 0x00050FE7
		// (set) Token: 0x06001B2A RID: 6954 RVA: 0x00052DEF File Offset: 0x00050FEF
		internal bool IsActive { get; set; }

		// Token: 0x06001B2B RID: 6955 RVA: 0x00052DF8 File Offset: 0x00050FF8
		public override void DoClick()
		{
			if (!base.IsClickedCancelled())
			{
				string type = this.Type;
				if (type.Is(InputTypeNames.Submit))
				{
					IHtmlFormElement form = base.Form;
					if (form == null)
					{
						return;
					}
					form.SubmitAsync();
					return;
				}
				else if (type.Is(InputTypeNames.Reset))
				{
					IHtmlFormElement form2 = base.Form;
					if (form2 == null)
					{
						return;
					}
					form2.Reset();
				}
			}
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x00052E50 File Offset: 0x00051050
		public sealed override INode Clone(bool deep = true)
		{
			HtmlInputElement htmlInputElement = (HtmlInputElement)base.Clone(deep);
			htmlInputElement._checked = this._checked;
			htmlInputElement.UpdateType(this._type.Name);
			return htmlInputElement;
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x00052E7B File Offset: 0x0005107B
		internal override FormControlState SaveControlState()
		{
			return new FormControlState(base.Name, this.Type, base.Value);
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x00052E94 File Offset: 0x00051094
		internal override void RestoreFormControlState(FormControlState state)
		{
			if (state.Type.Is(this.Type) && state.Name.Is(base.Name))
			{
				base.Value = state.Value;
			}
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x00052EC8 File Offset: 0x000510C8
		public void StepUp(int n = 1)
		{
			this._type.DoStep(n);
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x00052ED6 File Offset: 0x000510D6
		public void StepDown(int n = 1)
		{
			this._type.DoStep(-n);
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06001B31 RID: 6961 RVA: 0x00052EE5 File Offset: 0x000510E5
		internal bool IsMutable
		{
			get
			{
				return !base.IsDisabled && !base.IsReadOnly;
			}
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x00052EFC File Offset: 0x000510FC
		internal override void SetupElement()
		{
			base.SetupElement();
			string ownAttribute = this.GetOwnAttribute(AttributeNames.Type);
			this.UpdateType(ownAttribute);
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x00052F24 File Offset: 0x00051124
		internal void UpdateType(string value)
		{
			IInputTypeFactory factory = base.Owner.Options.GetFactory<IInputTypeFactory>();
			this._type = factory.Create(this, value);
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x00052F50 File Offset: 0x00051150
		internal override void ConstructDataSet(FormDataSet dataSet, IHtmlElement submitter)
		{
			if (this._type.IsAppendingData(submitter))
			{
				this._type.ConstructDataSet(dataSet);
			}
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x00052F6C File Offset: 0x0005116C
		internal override void Reset()
		{
			base.Reset();
			this._checked = null;
			this.UpdateType(this.Type);
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x00052F8C File Offset: 0x0005118C
		protected override void Check(ValidityState state)
		{
			base.Check(state);
			this._type.Check(state);
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x00052FA1 File Offset: 0x000511A1
		protected override bool CanBeValidated()
		{
			return this._type.CanBeValidated && base.CanBeValidated();
		}

		// Token: 0x04000CD7 RID: 3287
		private BaseInputType _type;

		// Token: 0x04000CD8 RID: 3288
		private bool? _checked;
	}
}
