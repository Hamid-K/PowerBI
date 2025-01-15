using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x02000104 RID: 260
	[Key("CatalogItemId")]
	[OriginalName("HistorySnapshotOptions")]
	public class HistorySnapshotOptions : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x06000B4D RID: 2893 RVA: 0x000163B7 File Offset: 0x000145B7
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static HistorySnapshotOptions CreateHistorySnapshotOptions(Guid catalogItemId)
		{
			return new HistorySnapshotOptions
			{
				CatalogItemId = catalogItemId
			};
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x000163C5 File Offset: 0x000145C5
		// (set) Token: 0x06000B4F RID: 2895 RVA: 0x000163CD File Offset: 0x000145CD
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("CatalogItemId")]
		public Guid CatalogItemId
		{
			get
			{
				return this._CatalogItemId;
			}
			set
			{
				this._CatalogItemId = value;
				this.OnPropertyChanged("CatalogItemId");
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x000163E1 File Offset: 0x000145E1
		// (set) Token: 0x06000B51 RID: 2897 RVA: 0x000163E9 File Offset: 0x000145E9
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("HistorySnapshotsOptions")]
		public ReportHistorySnapshotsOptions HistorySnapshotsOptions
		{
			get
			{
				return this._HistorySnapshotsOptions;
			}
			set
			{
				this._HistorySnapshotsOptions = value;
				this.OnPropertyChanged("HistorySnapshotsOptions");
			}
		}

		// Token: 0x1400007B RID: 123
		// (add) Token: 0x06000B52 RID: 2898 RVA: 0x00016400 File Offset: 0x00014600
		// (remove) Token: 0x06000B53 RID: 2899 RVA: 0x00016438 File Offset: 0x00014638
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000B54 RID: 2900 RVA: 0x0001646D File Offset: 0x0001466D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000529 RID: 1321
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _CatalogItemId;

		// Token: 0x0400052A RID: 1322
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ReportHistorySnapshotsOptions _HistorySnapshotsOptions;
	}
}
