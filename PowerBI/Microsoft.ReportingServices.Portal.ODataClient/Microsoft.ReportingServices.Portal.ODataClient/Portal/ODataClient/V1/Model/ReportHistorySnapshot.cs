using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x02000106 RID: 262
	[Key("HistoryId")]
	[OriginalName("ReportHistorySnapshot")]
	public class ReportHistorySnapshot : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x06000B59 RID: 2905 RVA: 0x000164A7 File Offset: 0x000146A7
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

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x000164C3 File Offset: 0x000146C3
		// (set) Token: 0x06000B5B RID: 2907 RVA: 0x000164CB File Offset: 0x000146CB
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

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x000164DF File Offset: 0x000146DF
		// (set) Token: 0x06000B5D RID: 2909 RVA: 0x000164E7 File Offset: 0x000146E7
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

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x000164FB File Offset: 0x000146FB
		// (set) Token: 0x06000B5F RID: 2911 RVA: 0x00016503 File Offset: 0x00014703
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

		// Token: 0x1400007C RID: 124
		// (add) Token: 0x06000B60 RID: 2912 RVA: 0x00016518 File Offset: 0x00014718
		// (remove) Token: 0x06000B61 RID: 2913 RVA: 0x00016550 File Offset: 0x00014750
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000B62 RID: 2914 RVA: 0x00016585 File Offset: 0x00014785
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400052C RID: 1324
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _HistoryId;

		// Token: 0x0400052D RID: 1325
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DateTimeOffset _CreationDate;

		// Token: 0x0400052E RID: 1326
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _Size;
	}
}
