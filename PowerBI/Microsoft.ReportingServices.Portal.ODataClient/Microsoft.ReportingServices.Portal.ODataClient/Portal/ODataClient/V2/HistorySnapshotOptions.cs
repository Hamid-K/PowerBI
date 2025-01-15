using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000017 RID: 23
	[Key("CatalogItemId")]
	[OriginalName("HistorySnapshotOptions")]
	public class HistorySnapshotOptions : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x060000D2 RID: 210 RVA: 0x00003283 File Offset: 0x00001483
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static HistorySnapshotOptions CreateHistorySnapshotOptions(Guid catalogItemId)
		{
			return new HistorySnapshotOptions
			{
				CatalogItemId = catalogItemId
			};
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003291 File Offset: 0x00001491
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00003299 File Offset: 0x00001499
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

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x000032AD File Offset: 0x000014AD
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x000032B5 File Offset: 0x000014B5
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

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060000D7 RID: 215 RVA: 0x000032CC File Offset: 0x000014CC
		// (remove) Token: 0x060000D8 RID: 216 RVA: 0x00003304 File Offset: 0x00001504
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060000D9 RID: 217 RVA: 0x00003339 File Offset: 0x00001539
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000087 RID: 135
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _CatalogItemId;

		// Token: 0x04000088 RID: 136
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ReportHistorySnapshotsOptions _HistorySnapshotsOptions;
	}
}
