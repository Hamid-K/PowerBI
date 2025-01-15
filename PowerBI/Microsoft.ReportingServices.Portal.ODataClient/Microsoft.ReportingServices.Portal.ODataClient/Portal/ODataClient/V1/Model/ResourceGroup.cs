using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x0200010D RID: 269
	[OriginalName("ResourceGroup")]
	public class ResourceGroup : INotifyPropertyChanged
	{
		// Token: 0x06000BA0 RID: 2976 RVA: 0x00016B75 File Offset: 0x00014D75
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static ResourceGroup CreateResourceGroup(MobileReportResourceGroupType type)
		{
			return new ResourceGroup
			{
				Type = type
			};
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x00016B83 File Offset: 0x00014D83
		// (set) Token: 0x06000BA2 RID: 2978 RVA: 0x00016B8B File Offset: 0x00014D8B
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

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x00016B9F File Offset: 0x00014D9F
		// (set) Token: 0x06000BA4 RID: 2980 RVA: 0x00016BA7 File Offset: 0x00014DA7
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Type")]
		public MobileReportResourceGroupType Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
				this.OnPropertyChanged("Type");
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x00016BBB File Offset: 0x00014DBB
		// (set) Token: 0x06000BA6 RID: 2982 RVA: 0x00016BC3 File Offset: 0x00014DC3
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Items")]
		public ObservableCollection<ResourceItem> Items
		{
			get
			{
				return this._Items;
			}
			set
			{
				this._Items = value;
				this.OnPropertyChanged("Items");
			}
		}

		// Token: 0x14000080 RID: 128
		// (add) Token: 0x06000BA7 RID: 2983 RVA: 0x00016BD8 File Offset: 0x00014DD8
		// (remove) Token: 0x06000BA8 RID: 2984 RVA: 0x00016C10 File Offset: 0x00014E10
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000BA9 RID: 2985 RVA: 0x00016C45 File Offset: 0x00014E45
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000549 RID: 1353
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x0400054A RID: 1354
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private MobileReportResourceGroupType _Type;

		// Token: 0x0400054B RID: 1355
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<ResourceItem> _Items = new ObservableCollection<ResourceItem>();
	}
}
