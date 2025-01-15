using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x0200010B RID: 267
	[OriginalName("MobileReportManifest")]
	public class MobileReportManifest : INotifyPropertyChanged
	{
		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x0001694B File Offset: 0x00014B4B
		// (set) Token: 0x06000B88 RID: 2952 RVA: 0x00016953 File Offset: 0x00014B53
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

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x00016967 File Offset: 0x00014B67
		// (set) Token: 0x06000B8A RID: 2954 RVA: 0x0001696F File Offset: 0x00014B6F
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

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x00016983 File Offset: 0x00014B83
		// (set) Token: 0x06000B8C RID: 2956 RVA: 0x0001698B File Offset: 0x00014B8B
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

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x0001699F File Offset: 0x00014B9F
		// (set) Token: 0x06000B8E RID: 2958 RVA: 0x000169A7 File Offset: 0x00014BA7
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

		// Token: 0x1400007E RID: 126
		// (add) Token: 0x06000B8F RID: 2959 RVA: 0x000169BC File Offset: 0x00014BBC
		// (remove) Token: 0x06000B90 RID: 2960 RVA: 0x000169F4 File Offset: 0x00014BF4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000B91 RID: 2961 RVA: 0x00016A29 File Offset: 0x00014C29
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400053F RID: 1343
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DefinitionItem _Definition;

		// Token: 0x04000540 RID: 1344
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<ResourceGroup> _Resources = new ObservableCollection<ResourceGroup>();

		// Token: 0x04000541 RID: 1345
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<DataSetItem> _DataSets = new ObservableCollection<DataSetItem>();

		// Token: 0x04000542 RID: 1346
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<ThumbnailItem> _Thumbnails = new ObservableCollection<ThumbnailItem>();
	}
}
