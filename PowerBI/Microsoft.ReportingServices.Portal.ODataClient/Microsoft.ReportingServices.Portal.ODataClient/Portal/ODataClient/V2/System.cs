using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000048 RID: 72
	[Key("Id")]
	[OriginalName("System")]
	public class System : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x0600032F RID: 815 RVA: 0x00007BC1 File Offset: 0x00005DC1
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static Microsoft.ReportingServices.Portal.ODataClient.V2.System CreateSystem(Guid ID, ProductType productType)
		{
			return new Microsoft.ReportingServices.Portal.ODataClient.V2.System
			{
				Id = ID,
				ProductType = productType
			};
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00007BD6 File Offset: 0x00005DD6
		// (set) Token: 0x06000331 RID: 817 RVA: 0x00007BDE File Offset: 0x00005DDE
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Id")]
		public Guid Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
				this.OnPropertyChanged("Id");
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000332 RID: 818 RVA: 0x00007BF2 File Offset: 0x00005DF2
		// (set) Token: 0x06000333 RID: 819 RVA: 0x00007BFA File Offset: 0x00005DFA
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ReportServerAbsoluteUrl")]
		public string ReportServerAbsoluteUrl
		{
			get
			{
				return this._ReportServerAbsoluteUrl;
			}
			set
			{
				this._ReportServerAbsoluteUrl = value;
				this.OnPropertyChanged("ReportServerAbsoluteUrl");
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00007C0E File Offset: 0x00005E0E
		// (set) Token: 0x06000335 RID: 821 RVA: 0x00007C16 File Offset: 0x00005E16
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ReportServerRelativeUrl")]
		public string ReportServerRelativeUrl
		{
			get
			{
				return this._ReportServerRelativeUrl;
			}
			set
			{
				this._ReportServerRelativeUrl = value;
				this.OnPropertyChanged("ReportServerRelativeUrl");
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000336 RID: 822 RVA: 0x00007C2A File Offset: 0x00005E2A
		// (set) Token: 0x06000337 RID: 823 RVA: 0x00007C32 File Offset: 0x00005E32
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("WebPortalRelativeUrl")]
		public string WebPortalRelativeUrl
		{
			get
			{
				return this._WebPortalRelativeUrl;
			}
			set
			{
				this._WebPortalRelativeUrl = value;
				this.OnPropertyChanged("WebPortalRelativeUrl");
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00007C46 File Offset: 0x00005E46
		// (set) Token: 0x06000339 RID: 825 RVA: 0x00007C4E File Offset: 0x00005E4E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ProductName")]
		public string ProductName
		{
			get
			{
				return this._ProductName;
			}
			set
			{
				this._ProductName = value;
				this.OnPropertyChanged("ProductName");
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00007C62 File Offset: 0x00005E62
		// (set) Token: 0x0600033B RID: 827 RVA: 0x00007C6A File Offset: 0x00005E6A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ProductVersion")]
		public string ProductVersion
		{
			get
			{
				return this._ProductVersion;
			}
			set
			{
				this._ProductVersion = value;
				this.OnPropertyChanged("ProductVersion");
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00007C7E File Offset: 0x00005E7E
		// (set) Token: 0x0600033D RID: 829 RVA: 0x00007C86 File Offset: 0x00005E86
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("TimeZone")]
		public string TimeZone
		{
			get
			{
				return this._TimeZone;
			}
			set
			{
				this._TimeZone = value;
				this.OnPropertyChanged("TimeZone");
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00007C9A File Offset: 0x00005E9A
		// (set) Token: 0x0600033F RID: 831 RVA: 0x00007CA2 File Offset: 0x00005EA2
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ProductType")]
		public ProductType ProductType
		{
			get
			{
				return this._ProductType;
			}
			set
			{
				this._ProductType = value;
				this.OnPropertyChanged("ProductType");
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00007CB6 File Offset: 0x00005EB6
		// (set) Token: 0x06000341 RID: 833 RVA: 0x00007CBE File Offset: 0x00005EBE
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Roles")]
		public ObservableCollection<Role> Roles
		{
			get
			{
				return this._Roles;
			}
			set
			{
				this._Roles = value;
				this.OnPropertyChanged("Roles");
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00007CD2 File Offset: 0x00005ED2
		// (set) Token: 0x06000343 RID: 835 RVA: 0x00007CDA File Offset: 0x00005EDA
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("AllowedActions")]
		public DataServiceCollection<AllowedAction> AllowedActions
		{
			get
			{
				return this._AllowedActions;
			}
			set
			{
				this._AllowedActions = value;
				this.OnPropertyChanged("AllowedActions");
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00007CEE File Offset: 0x00005EEE
		// (set) Token: 0x06000345 RID: 837 RVA: 0x00007CF6 File Offset: 0x00005EF6
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Policies")]
		public DataServiceCollection<SystemPolicy> Policies
		{
			get
			{
				return this._Policies;
			}
			set
			{
				this._Policies = value;
				this.OnPropertyChanged("Policies");
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00007D0A File Offset: 0x00005F0A
		// (set) Token: 0x06000347 RID: 839 RVA: 0x00007D12 File Offset: 0x00005F12
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Properties")]
		public DataServiceCollection<Property> Properties
		{
			get
			{
				return this._Properties;
			}
			set
			{
				this._Properties = value;
				this.OnPropertyChanged("Properties");
			}
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000348 RID: 840 RVA: 0x00007D28 File Offset: 0x00005F28
		// (remove) Token: 0x06000349 RID: 841 RVA: 0x00007D60 File Offset: 0x00005F60
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600034A RID: 842 RVA: 0x00007D95 File Offset: 0x00005F95
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000199 RID: 409
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x0400019A RID: 410
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _ReportServerAbsoluteUrl;

		// Token: 0x0400019B RID: 411
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _ReportServerRelativeUrl;

		// Token: 0x0400019C RID: 412
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _WebPortalRelativeUrl;

		// Token: 0x0400019D RID: 413
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _ProductName;

		// Token: 0x0400019E RID: 414
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _ProductVersion;

		// Token: 0x0400019F RID: 415
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _TimeZone;

		// Token: 0x040001A0 RID: 416
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ProductType _ProductType;

		// Token: 0x040001A1 RID: 417
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<Role> _Roles = new ObservableCollection<Role>();

		// Token: 0x040001A2 RID: 418
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<AllowedAction> _AllowedActions = new DataServiceCollection<AllowedAction>(null, TrackingMode.None);

		// Token: 0x040001A3 RID: 419
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<SystemPolicy> _Policies = new DataServiceCollection<SystemPolicy>(null, TrackingMode.None);

		// Token: 0x040001A4 RID: 420
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<Property> _Properties = new DataServiceCollection<Property>(null, TrackingMode.None);
	}
}
