using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000BC RID: 188
	[Key("Name")]
	[EntitySet("ReportParameters")]
	[OriginalName("ReportParameterDefinition")]
	public class ReportParameterDefinition : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x060007FF RID: 2047 RVA: 0x00010274 File Offset: 0x0000E474
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

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x000102EA File Offset: 0x0000E4EA
		// (set) Token: 0x06000801 RID: 2049 RVA: 0x000102F2 File Offset: 0x0000E4F2
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

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x00010306 File Offset: 0x0000E506
		// (set) Token: 0x06000803 RID: 2051 RVA: 0x0001030E File Offset: 0x0000E50E
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

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x00010322 File Offset: 0x0000E522
		// (set) Token: 0x06000805 RID: 2053 RVA: 0x0001032A File Offset: 0x0000E52A
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

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x0001033E File Offset: 0x0000E53E
		// (set) Token: 0x06000807 RID: 2055 RVA: 0x00010346 File Offset: 0x0000E546
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

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x0001035A File Offset: 0x0000E55A
		// (set) Token: 0x06000809 RID: 2057 RVA: 0x00010362 File Offset: 0x0000E562
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

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x00010376 File Offset: 0x0000E576
		// (set) Token: 0x0600080B RID: 2059 RVA: 0x0001037E File Offset: 0x0000E57E
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

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x00010392 File Offset: 0x0000E592
		// (set) Token: 0x0600080D RID: 2061 RVA: 0x0001039A File Offset: 0x0000E59A
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

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x000103AE File Offset: 0x0000E5AE
		// (set) Token: 0x0600080F RID: 2063 RVA: 0x000103B6 File Offset: 0x0000E5B6
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

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x000103CA File Offset: 0x0000E5CA
		// (set) Token: 0x06000811 RID: 2065 RVA: 0x000103D2 File Offset: 0x0000E5D2
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

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x000103E6 File Offset: 0x0000E5E6
		// (set) Token: 0x06000813 RID: 2067 RVA: 0x000103EE File Offset: 0x0000E5EE
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

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x00010402 File Offset: 0x0000E602
		// (set) Token: 0x06000815 RID: 2069 RVA: 0x0001040A File Offset: 0x0000E60A
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

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x0001041E File Offset: 0x0000E61E
		// (set) Token: 0x06000817 RID: 2071 RVA: 0x00010426 File Offset: 0x0000E626
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

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x0001043A File Offset: 0x0000E63A
		// (set) Token: 0x06000819 RID: 2073 RVA: 0x00010442 File Offset: 0x0000E642
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

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x00010456 File Offset: 0x0000E656
		// (set) Token: 0x0600081B RID: 2075 RVA: 0x0001045E File Offset: 0x0000E65E
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

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x00010472 File Offset: 0x0000E672
		// (set) Token: 0x0600081D RID: 2077 RVA: 0x0001047A File Offset: 0x0000E67A
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

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x0001048E File Offset: 0x0000E68E
		// (set) Token: 0x0600081F RID: 2079 RVA: 0x00010496 File Offset: 0x0000E696
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

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x000104AA File Offset: 0x0000E6AA
		// (set) Token: 0x06000821 RID: 2081 RVA: 0x000104B2 File Offset: 0x0000E6B2
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

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x000104C6 File Offset: 0x0000E6C6
		// (set) Token: 0x06000823 RID: 2083 RVA: 0x000104CE File Offset: 0x0000E6CE
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

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x06000824 RID: 2084 RVA: 0x000104E4 File Offset: 0x0000E6E4
		// (remove) Token: 0x06000825 RID: 2085 RVA: 0x0001051C File Offset: 0x0000E71C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000826 RID: 2086 RVA: 0x00010551 File Offset: 0x0000E751
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040003D5 RID: 981
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x040003D6 RID: 982
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ReportParameterType _ParameterType;

		// Token: 0x040003D7 RID: 983
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ReportParameterVisibility _ParameterVisibility;

		// Token: 0x040003D8 RID: 984
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ReportParameterState _ParameterState;

		// Token: 0x040003D9 RID: 985
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<ValidValue> _ValidValues = new ObservableCollection<ValidValue>();

		// Token: 0x040003DA RID: 986
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _ValidValuesIsNull;

		// Token: 0x040003DB RID: 987
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _Nullable;

		// Token: 0x040003DC RID: 988
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _AllowBlank;

		// Token: 0x040003DD RID: 989
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _MultiValue;

		// Token: 0x040003DE RID: 990
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Prompt;

		// Token: 0x040003DF RID: 991
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _PromptUser;

		// Token: 0x040003E0 RID: 992
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _QueryParameter;

		// Token: 0x040003E1 RID: 993
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _DefaultValuesQueryBased;

		// Token: 0x040003E2 RID: 994
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _ValidValuesQueryBased;

		// Token: 0x040003E3 RID: 995
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<string> _Dependencies = new ObservableCollection<string>();

		// Token: 0x040003E4 RID: 996
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<string> _DefaultValues = new ObservableCollection<string>();

		// Token: 0x040003E5 RID: 997
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _DefaultValuesIsNull;

		// Token: 0x040003E6 RID: 998
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _ErrorMessage;
	}
}
