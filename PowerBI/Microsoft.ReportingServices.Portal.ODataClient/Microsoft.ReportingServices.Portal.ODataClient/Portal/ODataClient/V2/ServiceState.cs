using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200005D RID: 93
	[OriginalName("ServiceState")]
	public class ServiceState : INotifyPropertyChanged
	{
		// Token: 0x06000431 RID: 1073 RVA: 0x00009341 File Offset: 0x00007541
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static ServiceState CreateServiceState(bool isAvailable, bool userHasFavorites, bool requireIntune, ProductType productType)
		{
			return new ServiceState
			{
				IsAvailable = isAvailable,
				UserHasFavorites = userHasFavorites,
				RequireIntune = requireIntune,
				ProductType = productType
			};
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x00009364 File Offset: 0x00007564
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x0000936C File Offset: 0x0000756C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("IsAvailable")]
		public bool IsAvailable
		{
			get
			{
				return this._IsAvailable;
			}
			set
			{
				this._IsAvailable = value;
				this.OnPropertyChanged("IsAvailable");
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x00009380 File Offset: 0x00007580
		// (set) Token: 0x06000435 RID: 1077 RVA: 0x00009388 File Offset: 0x00007588
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("RestrictedFeatures")]
		public ObservableCollection<string> RestrictedFeatures
		{
			get
			{
				return this._RestrictedFeatures;
			}
			set
			{
				this._RestrictedFeatures = value;
				this.OnPropertyChanged("RestrictedFeatures");
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0000939C File Offset: 0x0000759C
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x000093A4 File Offset: 0x000075A4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("AllowedSystemActions")]
		public ObservableCollection<string> AllowedSystemActions
		{
			get
			{
				return this._AllowedSystemActions;
			}
			set
			{
				this._AllowedSystemActions = value;
				this.OnPropertyChanged("AllowedSystemActions");
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x000093B8 File Offset: 0x000075B8
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x000093C0 File Offset: 0x000075C0
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

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x000093D4 File Offset: 0x000075D4
		// (set) Token: 0x0600043B RID: 1083 RVA: 0x000093DC File Offset: 0x000075DC
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("UserHasFavorites")]
		public bool UserHasFavorites
		{
			get
			{
				return this._UserHasFavorites;
			}
			set
			{
				this._UserHasFavorites = value;
				this.OnPropertyChanged("UserHasFavorites");
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x000093F0 File Offset: 0x000075F0
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x000093F8 File Offset: 0x000075F8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("AcceptLanguage")]
		public string AcceptLanguage
		{
			get
			{
				return this._AcceptLanguage;
			}
			set
			{
				this._AcceptLanguage = value;
				this.OnPropertyChanged("AcceptLanguage");
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0000940C File Offset: 0x0000760C
		// (set) Token: 0x0600043F RID: 1087 RVA: 0x00009414 File Offset: 0x00007614
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("RequireIntune")]
		public bool RequireIntune
		{
			get
			{
				return this._RequireIntune;
			}
			set
			{
				this._RequireIntune = value;
				this.OnPropertyChanged("RequireIntune");
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x00009428 File Offset: 0x00007628
		// (set) Token: 0x06000441 RID: 1089 RVA: 0x00009430 File Offset: 0x00007630
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

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06000442 RID: 1090 RVA: 0x00009444 File Offset: 0x00007644
		// (remove) Token: 0x06000443 RID: 1091 RVA: 0x0000947C File Offset: 0x0000767C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000444 RID: 1092 RVA: 0x000094B1 File Offset: 0x000076B1
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040001FF RID: 511
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsAvailable;

		// Token: 0x04000200 RID: 512
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<string> _RestrictedFeatures = new ObservableCollection<string>();

		// Token: 0x04000201 RID: 513
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<string> _AllowedSystemActions = new ObservableCollection<string>();

		// Token: 0x04000202 RID: 514
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _TimeZone;

		// Token: 0x04000203 RID: 515
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _UserHasFavorites;

		// Token: 0x04000204 RID: 516
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _AcceptLanguage;

		// Token: 0x04000205 RID: 517
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _RequireIntune;

		// Token: 0x04000206 RID: 518
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ProductType _ProductType;
	}
}
