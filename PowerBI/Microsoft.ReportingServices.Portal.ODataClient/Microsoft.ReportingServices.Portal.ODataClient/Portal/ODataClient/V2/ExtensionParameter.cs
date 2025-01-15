using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200002F RID: 47
	[OriginalName("ExtensionParameter")]
	public class ExtensionParameter : INotifyPropertyChanged
	{
		// Token: 0x060001EB RID: 491 RVA: 0x000051C0 File Offset: 0x000033C0
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

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001EC RID: 492 RVA: 0x000051EB File Offset: 0x000033EB
		// (set) Token: 0x060001ED RID: 493 RVA: 0x000051F3 File Offset: 0x000033F3
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

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00005207 File Offset: 0x00003407
		// (set) Token: 0x060001EF RID: 495 RVA: 0x0000520F File Offset: 0x0000340F
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

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00005223 File Offset: 0x00003423
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x0000522B File Offset: 0x0000342B
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

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000523F File Offset: 0x0000343F
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x00005247 File Offset: 0x00003447
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

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000525B File Offset: 0x0000345B
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00005263 File Offset: 0x00003463
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

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00005277 File Offset: 0x00003477
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x0000527F File Offset: 0x0000347F
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

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00005293 File Offset: 0x00003493
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x0000529B File Offset: 0x0000349B
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

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001FA RID: 506 RVA: 0x000052AF File Offset: 0x000034AF
		// (set) Token: 0x060001FB RID: 507 RVA: 0x000052B7 File Offset: 0x000034B7
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

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001FC RID: 508 RVA: 0x000052CB File Offset: 0x000034CB
		// (set) Token: 0x060001FD RID: 509 RVA: 0x000052D3 File Offset: 0x000034D3
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

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001FE RID: 510 RVA: 0x000052E7 File Offset: 0x000034E7
		// (set) Token: 0x060001FF RID: 511 RVA: 0x000052EF File Offset: 0x000034EF
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

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000200 RID: 512 RVA: 0x00005304 File Offset: 0x00003504
		// (remove) Token: 0x06000201 RID: 513 RVA: 0x0000533C File Offset: 0x0000353C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000202 RID: 514 RVA: 0x00005371 File Offset: 0x00003571
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040000FF RID: 255
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x04000100 RID: 256
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _DisplayName;

		// Token: 0x04000101 RID: 257
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _Required;

		// Token: 0x04000102 RID: 258
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _ReadOnly;

		// Token: 0x04000103 RID: 259
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Value;

		// Token: 0x04000104 RID: 260
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Error;

		// Token: 0x04000105 RID: 261
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _Encrypted;

		// Token: 0x04000106 RID: 262
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsPassword;

		// Token: 0x04000107 RID: 263
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<ValidValue> _ValidValues = new ObservableCollection<ValidValue>();

		// Token: 0x04000108 RID: 264
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _ValidValuesIsNull;
	}
}
