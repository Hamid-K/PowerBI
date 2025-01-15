using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000046 RID: 70
	[Key("Name")]
	[EntitySet("ParameterDefinitions")]
	[OriginalName("ReportParameterDefinition")]
	public class ReportParameterDefinition : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x06000300 RID: 768 RVA: 0x000077C4 File Offset: 0x000059C4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static ReportParameterDefinition CreateReportParameterDefinition(string name, ReportParameterType parameterType, ReportParameterVisibility parameterVisibility, ReportParameterState parameterState, bool validValuesIsNull, bool nullable, bool allowBlank, bool multiValue, bool promptUser, bool queryParameter, bool defaultValuesQueryBased, bool validValuesQueryBased, bool defaultValuesIsNull)
		{
			return new ReportParameterDefinition
			{
				Name = name,
				ParameterType = parameterType,
				ParameterVisibility = parameterVisibility,
				ParameterState = parameterState,
				ValidValuesIsNull = validValuesIsNull,
				Nullable = nullable,
				AllowBlank = allowBlank,
				MultiValue = multiValue,
				PromptUser = promptUser,
				QueryParameter = queryParameter,
				DefaultValuesQueryBased = defaultValuesQueryBased,
				ValidValuesQueryBased = validValuesQueryBased,
				DefaultValuesIsNull = defaultValuesIsNull
			};
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000783A File Offset: 0x00005A3A
		// (set) Token: 0x06000302 RID: 770 RVA: 0x00007842 File Offset: 0x00005A42
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Name")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
				this.OnPropertyChanged("Name");
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00007856 File Offset: 0x00005A56
		// (set) Token: 0x06000304 RID: 772 RVA: 0x0000785E File Offset: 0x00005A5E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ParameterType")]
		public ReportParameterType ParameterType
		{
			get
			{
				return this._ParameterType;
			}
			set
			{
				this._ParameterType = value;
				this.OnPropertyChanged("ParameterType");
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000305 RID: 773 RVA: 0x00007872 File Offset: 0x00005A72
		// (set) Token: 0x06000306 RID: 774 RVA: 0x0000787A File Offset: 0x00005A7A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ParameterVisibility")]
		public ReportParameterVisibility ParameterVisibility
		{
			get
			{
				return this._ParameterVisibility;
			}
			set
			{
				this._ParameterVisibility = value;
				this.OnPropertyChanged("ParameterVisibility");
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000788E File Offset: 0x00005A8E
		// (set) Token: 0x06000308 RID: 776 RVA: 0x00007896 File Offset: 0x00005A96
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ParameterState")]
		public ReportParameterState ParameterState
		{
			get
			{
				return this._ParameterState;
			}
			set
			{
				this._ParameterState = value;
				this.OnPropertyChanged("ParameterState");
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000309 RID: 777 RVA: 0x000078AA File Offset: 0x00005AAA
		// (set) Token: 0x0600030A RID: 778 RVA: 0x000078B2 File Offset: 0x00005AB2
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ValidValues")]
		public ObservableCollection<ValidValue> ValidValues
		{
			get
			{
				return this._ValidValues;
			}
			set
			{
				this._ValidValues = value;
				this.OnPropertyChanged("ValidValues");
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600030B RID: 779 RVA: 0x000078C6 File Offset: 0x00005AC6
		// (set) Token: 0x0600030C RID: 780 RVA: 0x000078CE File Offset: 0x00005ACE
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ValidValuesIsNull")]
		public bool ValidValuesIsNull
		{
			get
			{
				return this._ValidValuesIsNull;
			}
			set
			{
				this._ValidValuesIsNull = value;
				this.OnPropertyChanged("ValidValuesIsNull");
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600030D RID: 781 RVA: 0x000078E2 File Offset: 0x00005AE2
		// (set) Token: 0x0600030E RID: 782 RVA: 0x000078EA File Offset: 0x00005AEA
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Nullable")]
		public bool Nullable
		{
			get
			{
				return this._Nullable;
			}
			set
			{
				this._Nullable = value;
				this.OnPropertyChanged("Nullable");
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600030F RID: 783 RVA: 0x000078FE File Offset: 0x00005AFE
		// (set) Token: 0x06000310 RID: 784 RVA: 0x00007906 File Offset: 0x00005B06
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("AllowBlank")]
		public bool AllowBlank
		{
			get
			{
				return this._AllowBlank;
			}
			set
			{
				this._AllowBlank = value;
				this.OnPropertyChanged("AllowBlank");
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000791A File Offset: 0x00005B1A
		// (set) Token: 0x06000312 RID: 786 RVA: 0x00007922 File Offset: 0x00005B22
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("MultiValue")]
		public bool MultiValue
		{
			get
			{
				return this._MultiValue;
			}
			set
			{
				this._MultiValue = value;
				this.OnPropertyChanged("MultiValue");
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00007936 File Offset: 0x00005B36
		// (set) Token: 0x06000314 RID: 788 RVA: 0x0000793E File Offset: 0x00005B3E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Prompt")]
		public string Prompt
		{
			get
			{
				return this._Prompt;
			}
			set
			{
				this._Prompt = value;
				this.OnPropertyChanged("Prompt");
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00007952 File Offset: 0x00005B52
		// (set) Token: 0x06000316 RID: 790 RVA: 0x0000795A File Offset: 0x00005B5A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("PromptUser")]
		public bool PromptUser
		{
			get
			{
				return this._PromptUser;
			}
			set
			{
				this._PromptUser = value;
				this.OnPropertyChanged("PromptUser");
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000796E File Offset: 0x00005B6E
		// (set) Token: 0x06000318 RID: 792 RVA: 0x00007976 File Offset: 0x00005B76
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("QueryParameter")]
		public bool QueryParameter
		{
			get
			{
				return this._QueryParameter;
			}
			set
			{
				this._QueryParameter = value;
				this.OnPropertyChanged("QueryParameter");
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000798A File Offset: 0x00005B8A
		// (set) Token: 0x0600031A RID: 794 RVA: 0x00007992 File Offset: 0x00005B92
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DefaultValuesQueryBased")]
		public bool DefaultValuesQueryBased
		{
			get
			{
				return this._DefaultValuesQueryBased;
			}
			set
			{
				this._DefaultValuesQueryBased = value;
				this.OnPropertyChanged("DefaultValuesQueryBased");
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600031B RID: 795 RVA: 0x000079A6 File Offset: 0x00005BA6
		// (set) Token: 0x0600031C RID: 796 RVA: 0x000079AE File Offset: 0x00005BAE
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ValidValuesQueryBased")]
		public bool ValidValuesQueryBased
		{
			get
			{
				return this._ValidValuesQueryBased;
			}
			set
			{
				this._ValidValuesQueryBased = value;
				this.OnPropertyChanged("ValidValuesQueryBased");
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600031D RID: 797 RVA: 0x000079C2 File Offset: 0x00005BC2
		// (set) Token: 0x0600031E RID: 798 RVA: 0x000079CA File Offset: 0x00005BCA
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Dependencies")]
		public ObservableCollection<string> Dependencies
		{
			get
			{
				return this._Dependencies;
			}
			set
			{
				this._Dependencies = value;
				this.OnPropertyChanged("Dependencies");
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600031F RID: 799 RVA: 0x000079DE File Offset: 0x00005BDE
		// (set) Token: 0x06000320 RID: 800 RVA: 0x000079E6 File Offset: 0x00005BE6
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DefaultValues")]
		public ObservableCollection<string> DefaultValues
		{
			get
			{
				return this._DefaultValues;
			}
			set
			{
				this._DefaultValues = value;
				this.OnPropertyChanged("DefaultValues");
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000321 RID: 801 RVA: 0x000079FA File Offset: 0x00005BFA
		// (set) Token: 0x06000322 RID: 802 RVA: 0x00007A02 File Offset: 0x00005C02
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DefaultValuesIsNull")]
		public bool DefaultValuesIsNull
		{
			get
			{
				return this._DefaultValuesIsNull;
			}
			set
			{
				this._DefaultValuesIsNull = value;
				this.OnPropertyChanged("DefaultValuesIsNull");
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000323 RID: 803 RVA: 0x00007A16 File Offset: 0x00005C16
		// (set) Token: 0x06000324 RID: 804 RVA: 0x00007A1E File Offset: 0x00005C1E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ErrorMessage")]
		public string ErrorMessage
		{
			get
			{
				return this._ErrorMessage;
			}
			set
			{
				this._ErrorMessage = value;
				this.OnPropertyChanged("ErrorMessage");
			}
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000325 RID: 805 RVA: 0x00007A34 File Offset: 0x00005C34
		// (remove) Token: 0x06000326 RID: 806 RVA: 0x00007A6C File Offset: 0x00005C6C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000327 RID: 807 RVA: 0x00007AA1 File Offset: 0x00005CA1
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000183 RID: 387
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x04000184 RID: 388
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ReportParameterType _ParameterType;

		// Token: 0x04000185 RID: 389
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ReportParameterVisibility _ParameterVisibility;

		// Token: 0x04000186 RID: 390
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ReportParameterState _ParameterState;

		// Token: 0x04000187 RID: 391
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<ValidValue> _ValidValues = new ObservableCollection<ValidValue>();

		// Token: 0x04000188 RID: 392
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _ValidValuesIsNull;

		// Token: 0x04000189 RID: 393
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _Nullable;

		// Token: 0x0400018A RID: 394
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _AllowBlank;

		// Token: 0x0400018B RID: 395
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _MultiValue;

		// Token: 0x0400018C RID: 396
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Prompt;

		// Token: 0x0400018D RID: 397
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _PromptUser;

		// Token: 0x0400018E RID: 398
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _QueryParameter;

		// Token: 0x0400018F RID: 399
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _DefaultValuesQueryBased;

		// Token: 0x04000190 RID: 400
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _ValidValuesQueryBased;

		// Token: 0x04000191 RID: 401
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<string> _Dependencies = new ObservableCollection<string>();

		// Token: 0x04000192 RID: 402
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<string> _DefaultValues = new ObservableCollection<string>();

		// Token: 0x04000193 RID: 403
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _DefaultValuesIsNull;

		// Token: 0x04000194 RID: 404
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _ErrorMessage;
	}
}
