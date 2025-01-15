using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200002E RID: 46
	[Key("Name")]
	[EntitySet("Extensions")]
	[OriginalName("Extension")]
	public class Extension : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x060001DC RID: 476 RVA: 0x0000507C File Offset: 0x0000327C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static Extension CreateExtension(ExtensionType extensionType, string name, bool visible)
		{
			return new Extension
			{
				ExtensionType = extensionType,
				Name = name,
				Visible = visible
			};
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00005098 File Offset: 0x00003298
		// (set) Token: 0x060001DE RID: 478 RVA: 0x000050A0 File Offset: 0x000032A0
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ExtensionType")]
		public ExtensionType ExtensionType
		{
			get
			{
				return this._ExtensionType;
			}
			set
			{
				this._ExtensionType = value;
				this.OnPropertyChanged("ExtensionType");
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001DF RID: 479 RVA: 0x000050B4 File Offset: 0x000032B4
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x000050BC File Offset: 0x000032BC
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

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x000050D0 File Offset: 0x000032D0
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x000050D8 File Offset: 0x000032D8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("LocalizedName")]
		public string LocalizedName
		{
			get
			{
				return this._LocalizedName;
			}
			set
			{
				this._LocalizedName = value;
				this.OnPropertyChanged("LocalizedName");
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x000050EC File Offset: 0x000032EC
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x000050F4 File Offset: 0x000032F4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Visible")]
		public bool Visible
		{
			get
			{
				return this._Visible;
			}
			set
			{
				this._Visible = value;
				this.OnPropertyChanged("Visible");
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00005108 File Offset: 0x00003308
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x00005110 File Offset: 0x00003310
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Parameters")]
		public ObservableCollection<ExtensionParameter> Parameters
		{
			get
			{
				return this._Parameters;
			}
			set
			{
				this._Parameters = value;
				this.OnPropertyChanged("Parameters");
			}
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x060001E7 RID: 487 RVA: 0x00005124 File Offset: 0x00003324
		// (remove) Token: 0x060001E8 RID: 488 RVA: 0x0000515C File Offset: 0x0000335C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060001E9 RID: 489 RVA: 0x00005191 File Offset: 0x00003391
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040000F9 RID: 249
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ExtensionType _ExtensionType;

		// Token: 0x040000FA RID: 250
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x040000FB RID: 251
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _LocalizedName;

		// Token: 0x040000FC RID: 252
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _Visible;

		// Token: 0x040000FD RID: 253
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<ExtensionParameter> _Parameters = new ObservableCollection<ExtensionParameter>();
	}
}
