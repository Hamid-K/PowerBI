using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200007D RID: 125
	[OriginalName("MobileReportManifest")]
	public class MobileReportManifest : INotifyPropertyChanged
	{
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x0000AF61 File Offset: 0x00009161
		// (set) Token: 0x0600056F RID: 1391 RVA: 0x0000AF69 File Offset: 0x00009169
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Definition")]
		public DefinitionItem Definition
		{
			get
			{
				return this._Definition;
			}
			set
			{
				this._Definition = value;
				this.OnPropertyChanged("Definition");
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x0000AF7D File Offset: 0x0000917D
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x0000AF85 File Offset: 0x00009185
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Resources")]
		public ObservableCollection<ResourceGroup> Resources
		{
			get
			{
				return this._Resources;
			}
			set
			{
				this._Resources = value;
				this.OnPropertyChanged("Resources");
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x0000AF99 File Offset: 0x00009199
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x0000AFA1 File Offset: 0x000091A1
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DataSets")]
		public ObservableCollection<DataSetItem> DataSets
		{
			get
			{
				return this._DataSets;
			}
			set
			{
				this._DataSets = value;
				this.OnPropertyChanged("DataSets");
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0000AFB5 File Offset: 0x000091B5
		// (set) Token: 0x06000575 RID: 1397 RVA: 0x0000AFBD File Offset: 0x000091BD
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Thumbnails")]
		public ObservableCollection<ThumbnailItem> Thumbnails
		{
			get
			{
				return this._Thumbnails;
			}
			set
			{
				this._Thumbnails = value;
				this.OnPropertyChanged("Thumbnails");
			}
		}

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x06000576 RID: 1398 RVA: 0x0000AFD4 File Offset: 0x000091D4
		// (remove) Token: 0x06000577 RID: 1399 RVA: 0x0000B00C File Offset: 0x0000920C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000578 RID: 1400 RVA: 0x0000B041 File Offset: 0x00009241
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000274 RID: 628
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DefinitionItem _Definition;

		// Token: 0x04000275 RID: 629
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<ResourceGroup> _Resources = new ObservableCollection<ResourceGroup>();

		// Token: 0x04000276 RID: 630
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<DataSetItem> _DataSets = new ObservableCollection<DataSetItem>();

		// Token: 0x04000277 RID: 631
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<ThumbnailItem> _Thumbnails = new ObservableCollection<ThumbnailItem>();
	}
}
