using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000AA RID: 170
	[OriginalName("KpiSharedDataItem")]
	public class KpiSharedDataItem : KpiDataItem
	{
		// Token: 0x06000710 RID: 1808 RVA: 0x0000EBE9 File Offset: 0x0000CDE9
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

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x0000EC05 File Offset: 0x0000CE05
		// (set) Token: 0x06000712 RID: 1810 RVA: 0x0000EC0D File Offset: 0x0000CE0D
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

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x0000EC21 File Offset: 0x0000CE21
		// (set) Token: 0x06000714 RID: 1812 RVA: 0x0000EC29 File Offset: 0x0000CE29
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

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x0000EC3D File Offset: 0x0000CE3D
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x0000EC45 File Offset: 0x0000CE45
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

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0000EC59 File Offset: 0x0000CE59
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x0000EC61 File Offset: 0x0000CE61
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

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x0000EC75 File Offset: 0x0000CE75
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x0000EC7D File Offset: 0x0000CE7D
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

		// Token: 0x04000376 RID: 886
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x04000377 RID: 887
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Path;

		// Token: 0x04000378 RID: 888
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<DataSetParameter> _Parameters = new ObservableCollection<DataSetParameter>();

		// Token: 0x04000379 RID: 889
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private KpiSharedDataItemAggregation _Aggregation;

		// Token: 0x0400037A RID: 890
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Column;
	}
}
