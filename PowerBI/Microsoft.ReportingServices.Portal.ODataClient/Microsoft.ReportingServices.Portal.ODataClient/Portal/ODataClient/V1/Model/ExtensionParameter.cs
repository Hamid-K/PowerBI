using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000DB RID: 219
	[OriginalName("ExtensionParameter")]
	public class ExtensionParameter : INotifyPropertyChanged
	{
		// Token: 0x060009CB RID: 2507 RVA: 0x00013F74 File Offset: 0x00012174
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static ExtensionParameter CreateExtensionParameter(bool required, bool readOnly, bool encrypted, bool isPassword, bool validValuesIsNull)
		{
			return new ExtensionParameter
			{
				Required = required,
				ReadOnly = readOnly,
				Encrypted = encrypted,
				IsPassword = isPassword,
				ValidValuesIsNull = validValuesIsNull
			};
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x00013F9F File Offset: 0x0001219F
		// (set) Token: 0x060009CD RID: 2509 RVA: 0x00013FA7 File Offset: 0x000121A7
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

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x00013FBB File Offset: 0x000121BB
		// (set) Token: 0x060009CF RID: 2511 RVA: 0x00013FC3 File Offset: 0x000121C3
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DisplayName")]
		public string DisplayName
		{
			get
			{
				return this._DisplayName;
			}
			set
			{
				this._DisplayName = value;
				this.OnPropertyChanged("DisplayName");
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x00013FD7 File Offset: 0x000121D7
		// (set) Token: 0x060009D1 RID: 2513 RVA: 0x00013FDF File Offset: 0x000121DF
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Required")]
		public bool Required
		{
			get
			{
				return this._Required;
			}
			set
			{
				this._Required = value;
				this.OnPropertyChanged("Required");
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x00013FF3 File Offset: 0x000121F3
		// (set) Token: 0x060009D3 RID: 2515 RVA: 0x00013FFB File Offset: 0x000121FB
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ReadOnly")]
		public bool ReadOnly
		{
			get
			{
				return this._ReadOnly;
			}
			set
			{
				this._ReadOnly = value;
				this.OnPropertyChanged("ReadOnly");
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x0001400F File Offset: 0x0001220F
		// (set) Token: 0x060009D5 RID: 2517 RVA: 0x00014017 File Offset: 0x00012217
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Value")]
		public string Value
		{
			get
			{
				return this._Value;
			}
			set
			{
				this._Value = value;
				this.OnPropertyChanged("Value");
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x0001402B File Offset: 0x0001222B
		// (set) Token: 0x060009D7 RID: 2519 RVA: 0x00014033 File Offset: 0x00012233
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Error")]
		public string Error
		{
			get
			{
				return this._Error;
			}
			set
			{
				this._Error = value;
				this.OnPropertyChanged("Error");
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x00014047 File Offset: 0x00012247
		// (set) Token: 0x060009D9 RID: 2521 RVA: 0x0001404F File Offset: 0x0001224F
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Encrypted")]
		public bool Encrypted
		{
			get
			{
				return this._Encrypted;
			}
			set
			{
				this._Encrypted = value;
				this.OnPropertyChanged("Encrypted");
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x00014063 File Offset: 0x00012263
		// (set) Token: 0x060009DB RID: 2523 RVA: 0x0001406B File Offset: 0x0001226B
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("IsPassword")]
		public bool IsPassword
		{
			get
			{
				return this._IsPassword;
			}
			set
			{
				this._IsPassword = value;
				this.OnPropertyChanged("IsPassword");
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x0001407F File Offset: 0x0001227F
		// (set) Token: 0x060009DD RID: 2525 RVA: 0x00014087 File Offset: 0x00012287
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

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x0001409B File Offset: 0x0001229B
		// (set) Token: 0x060009DF RID: 2527 RVA: 0x000140A3 File Offset: 0x000122A3
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

		// Token: 0x14000068 RID: 104
		// (add) Token: 0x060009E0 RID: 2528 RVA: 0x000140B8 File Offset: 0x000122B8
		// (remove) Token: 0x060009E1 RID: 2529 RVA: 0x000140F0 File Offset: 0x000122F0
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060009E2 RID: 2530 RVA: 0x00014125 File Offset: 0x00012325
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400048E RID: 1166
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x0400048F RID: 1167
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _DisplayName;

		// Token: 0x04000490 RID: 1168
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _Required;

		// Token: 0x04000491 RID: 1169
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _ReadOnly;

		// Token: 0x04000492 RID: 1170
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Value;

		// Token: 0x04000493 RID: 1171
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Error;

		// Token: 0x04000494 RID: 1172
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _Encrypted;

		// Token: 0x04000495 RID: 1173
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsPassword;

		// Token: 0x04000496 RID: 1174
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<ValidValue> _ValidValues = new ObservableCollection<ValidValue>();

		// Token: 0x04000497 RID: 1175
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _ValidValuesIsNull;
	}
}
