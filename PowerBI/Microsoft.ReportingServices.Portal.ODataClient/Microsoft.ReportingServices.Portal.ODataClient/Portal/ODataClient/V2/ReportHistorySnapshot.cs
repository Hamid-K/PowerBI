using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200007A RID: 122
	[Key("HistoryId")]
	[OriginalName("ReportHistorySnapshot")]
	public class ReportHistorySnapshot : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x06000553 RID: 1363 RVA: 0x0000AD33 File Offset: 0x00008F33
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static ReportHistorySnapshot CreateReportHistorySnapshot(string historyId, DateTimeOffset creationDate, int size)
		{
			return new ReportHistorySnapshot
			{
				HistoryId = historyId,
				CreationDate = creationDate,
				Size = size
			};
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x0000AD4F File Offset: 0x00008F4F
		// (set) Token: 0x06000555 RID: 1365 RVA: 0x0000AD57 File Offset: 0x00008F57
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("HistoryId")]
		public string HistoryId
		{
			get
			{
				return this._HistoryId;
			}
			set
			{
				this._HistoryId = value;
				this.OnPropertyChanged("HistoryId");
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x0000AD6B File Offset: 0x00008F6B
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x0000AD73 File Offset: 0x00008F73
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("CreationDate")]
		public DateTimeOffset CreationDate
		{
			get
			{
				return this._CreationDate;
			}
			set
			{
				this._CreationDate = value;
				this.OnPropertyChanged("CreationDate");
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0000AD87 File Offset: 0x00008F87
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x0000AD8F File Offset: 0x00008F8F
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Size")]
		public int Size
		{
			get
			{
				return this._Size;
			}
			set
			{
				this._Size = value;
				this.OnPropertyChanged("Size");
			}
		}

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x0600055A RID: 1370 RVA: 0x0000ADA4 File Offset: 0x00008FA4
		// (remove) Token: 0x0600055B RID: 1371 RVA: 0x0000ADDC File Offset: 0x00008FDC
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600055C RID: 1372 RVA: 0x0000AE11 File Offset: 0x00009011
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400026B RID: 619
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _HistoryId;

		// Token: 0x0400026C RID: 620
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DateTimeOffset _CreationDate;

		// Token: 0x0400026D RID: 621
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _Size;
	}
}
