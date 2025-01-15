using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200007F RID: 127
	[OriginalName("ResourceGroup")]
	public class ResourceGroup : INotifyPropertyChanged
	{
		// Token: 0x06000587 RID: 1415 RVA: 0x0000B18D File Offset: 0x0000938D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static ResourceGroup CreateResourceGroup(MobileReportResourceGroupType type)
		{
			return new ResourceGroup
			{
				Type = type
			};
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0000B19B File Offset: 0x0000939B
		// (set) Token: 0x06000589 RID: 1417 RVA: 0x0000B1A3 File Offset: 0x000093A3
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

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x0000B1B7 File Offset: 0x000093B7
		// (set) Token: 0x0600058B RID: 1419 RVA: 0x0000B1BF File Offset: 0x000093BF
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

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x0000B1D3 File Offset: 0x000093D3
		// (set) Token: 0x0600058D RID: 1421 RVA: 0x0000B1DB File Offset: 0x000093DB
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

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x0600058E RID: 1422 RVA: 0x0000B1F0 File Offset: 0x000093F0
		// (remove) Token: 0x0600058F RID: 1423 RVA: 0x0000B228 File Offset: 0x00009428
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000590 RID: 1424 RVA: 0x0000B25D File Offset: 0x0000945D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400027E RID: 638
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x0400027F RID: 639
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private MobileReportResourceGroupType _Type;

		// Token: 0x04000280 RID: 640
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<ResourceItem> _Items = new ObservableCollection<ResourceItem>();
	}
}
