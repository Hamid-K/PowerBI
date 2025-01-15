using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000007 RID: 7
	[OriginalName("KpiSharedDataItem")]
	public class KpiSharedDataItem : KpiDataItem
	{
		// Token: 0x06000043 RID: 67 RVA: 0x000027AD File Offset: 0x000009AD
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static KpiSharedDataItem CreateKpiSharedDataItem(KpiDataItemType type, Guid ID, KpiSharedDataItemAggregation aggregation)
		{
			return new KpiSharedDataItem
			{
				Type = type,
				Id = ID,
				Aggregation = aggregation
			};
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000027C9 File Offset: 0x000009C9
		// (set) Token: 0x06000045 RID: 69 RVA: 0x000027D1 File Offset: 0x000009D1
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

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000027E5 File Offset: 0x000009E5
		// (set) Token: 0x06000047 RID: 71 RVA: 0x000027ED File Offset: 0x000009ED
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Path")]
		public string Path
		{
			get
			{
				return this._Path;
			}
			set
			{
				this._Path = value;
				this.OnPropertyChanged("Path");
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002801 File Offset: 0x00000A01
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002809 File Offset: 0x00000A09
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Parameters")]
		public ObservableCollection<DataSetParameter> Parameters
		{
			get
			{
				return this._Parameters;
			}
			set
			{
				this._Parameters = value;
				this.OnPropertyChanged("Parameters");
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600004A RID: 74 RVA: 0x0000281D File Offset: 0x00000A1D
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002825 File Offset: 0x00000A25
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Aggregation")]
		public KpiSharedDataItemAggregation Aggregation
		{
			get
			{
				return this._Aggregation;
			}
			set
			{
				this._Aggregation = value;
				this.OnPropertyChanged("Aggregation");
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002839 File Offset: 0x00000A39
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002841 File Offset: 0x00000A41
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Column")]
		public string Column
		{
			get
			{
				return this._Column;
			}
			set
			{
				this._Column = value;
				this.OnPropertyChanged("Column");
			}
		}

		// Token: 0x04000054 RID: 84
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x04000055 RID: 85
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Path;

		// Token: 0x04000056 RID: 86
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<DataSetParameter> _Parameters = new ObservableCollection<DataSetParameter>();

		// Token: 0x04000057 RID: 87
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private KpiSharedDataItemAggregation _Aggregation;

		// Token: 0x04000058 RID: 88
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Column;
	}
}
