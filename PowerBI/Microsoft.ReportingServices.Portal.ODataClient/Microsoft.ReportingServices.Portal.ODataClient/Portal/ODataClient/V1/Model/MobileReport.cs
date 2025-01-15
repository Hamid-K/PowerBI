using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x0200010A RID: 266
	[Key("Id")]
	[OriginalName("MobileReport")]
	public class MobileReport : CatalogItem
	{
		// Token: 0x06000B7D RID: 2941 RVA: 0x00016870 File Offset: 0x00014A70
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static MobileReport CreateMobileReport(Guid ID, CatalogItemType type, bool hidden, long size, DateTimeOffset modifiedDate, DateTimeOffset createdDate, bool isFavorite, bool allowCaching, bool hasSharedDataSets)
		{
			return new MobileReport
			{
				Id = ID,
				Type = type,
				Hidden = hidden,
				Size = size,
				ModifiedDate = modifiedDate,
				CreatedDate = createdDate,
				IsFavorite = isFavorite,
				AllowCaching = allowCaching,
				HasSharedDataSets = hasSharedDataSets
			};
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x000168C6 File Offset: 0x00014AC6
		// (set) Token: 0x06000B7F RID: 2943 RVA: 0x000168CE File Offset: 0x00014ACE
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("AllowCaching")]
		public bool AllowCaching
		{
			get
			{
				return this._AllowCaching;
			}
			set
			{
				this._AllowCaching = value;
				this.OnPropertyChanged("AllowCaching");
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x000168E2 File Offset: 0x00014AE2
		// (set) Token: 0x06000B81 RID: 2945 RVA: 0x000168EA File Offset: 0x00014AEA
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Manifest")]
		public MobileReportManifest Manifest
		{
			get
			{
				return this._Manifest;
			}
			set
			{
				this._Manifest = value;
				this.OnPropertyChanged("Manifest");
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x000168FE File Offset: 0x00014AFE
		// (set) Token: 0x06000B83 RID: 2947 RVA: 0x00016906 File Offset: 0x00014B06
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("HasSharedDataSets")]
		public bool HasSharedDataSets
		{
			get
			{
				return this._HasSharedDataSets;
			}
			set
			{
				this._HasSharedDataSets = value;
				this.OnPropertyChanged("HasSharedDataSets");
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x0001691A File Offset: 0x00014B1A
		// (set) Token: 0x06000B85 RID: 2949 RVA: 0x00016922 File Offset: 0x00014B22
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("SharedDataSets")]
		public DataServiceCollection<DataSet> SharedDataSets
		{
			get
			{
				return this._SharedDataSets;
			}
			set
			{
				this._SharedDataSets = value;
				this.OnPropertyChanged("SharedDataSets");
			}
		}

		// Token: 0x0400053B RID: 1339
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _AllowCaching;

		// Token: 0x0400053C RID: 1340
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private MobileReportManifest _Manifest;

		// Token: 0x0400053D RID: 1341
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _HasSharedDataSets;

		// Token: 0x0400053E RID: 1342
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<DataSet> _SharedDataSets = new DataServiceCollection<DataSet>(null, TrackingMode.None);
	}
}
