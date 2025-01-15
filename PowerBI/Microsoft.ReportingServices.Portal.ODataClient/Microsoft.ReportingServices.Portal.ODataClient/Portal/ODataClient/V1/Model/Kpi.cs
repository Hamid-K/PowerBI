using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x02000100 RID: 256
	[Key("Id")]
	[OriginalName("Kpi")]
	public class Kpi : CatalogItem
	{
		// Token: 0x06000B24 RID: 2852 RVA: 0x00016094 File Offset: 0x00014294
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static Kpi CreateKpi(Guid ID, CatalogItemType type, bool hidden, long size, DateTimeOffset modifiedDate, DateTimeOffset createdDate, bool isFavorite, KpiValueFormat valueFormat, KpiVisualization visualization)
		{
			return new Kpi
			{
				Id = ID,
				Type = type,
				Hidden = hidden,
				Size = size,
				ModifiedDate = modifiedDate,
				CreatedDate = createdDate,
				IsFavorite = isFavorite,
				ValueFormat = valueFormat,
				Visualization = visualization
			};
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x000160EA File Offset: 0x000142EA
		// (set) Token: 0x06000B26 RID: 2854 RVA: 0x000160F2 File Offset: 0x000142F2
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ValueFormat")]
		public KpiValueFormat ValueFormat
		{
			get
			{
				return this._ValueFormat;
			}
			set
			{
				this._ValueFormat = value;
				this.OnPropertyChanged("ValueFormat");
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x00016106 File Offset: 0x00014306
		// (set) Token: 0x06000B28 RID: 2856 RVA: 0x0001610E File Offset: 0x0001430E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Visualization")]
		public KpiVisualization Visualization
		{
			get
			{
				return this._Visualization;
			}
			set
			{
				this._Visualization = value;
				this.OnPropertyChanged("Visualization");
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x00016122 File Offset: 0x00014322
		// (set) Token: 0x06000B2A RID: 2858 RVA: 0x0001612A File Offset: 0x0001432A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DrillthroughTarget")]
		public DrillthroughTarget DrillthroughTarget
		{
			get
			{
				return this._DrillthroughTarget;
			}
			set
			{
				this._DrillthroughTarget = value;
				this.OnPropertyChanged("DrillthroughTarget");
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0001613E File Offset: 0x0001433E
		// (set) Token: 0x06000B2C RID: 2860 RVA: 0x00016146 File Offset: 0x00014346
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Currency")]
		public string Currency
		{
			get
			{
				return this._Currency;
			}
			set
			{
				this._Currency = value;
				this.OnPropertyChanged("Currency");
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x0001615A File Offset: 0x0001435A
		// (set) Token: 0x06000B2E RID: 2862 RVA: 0x00016162 File Offset: 0x00014362
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Values")]
		public KpiValues Values
		{
			get
			{
				return this._Values;
			}
			set
			{
				this._Values = value;
				this.OnPropertyChanged("Values");
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x00016176 File Offset: 0x00014376
		// (set) Token: 0x06000B30 RID: 2864 RVA: 0x0001617E File Offset: 0x0001437E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Data")]
		public KpiData Data
		{
			get
			{
				return this._Data;
			}
			set
			{
				this._Data = value;
				this.OnPropertyChanged("Data");
			}
		}

		// Token: 0x04000519 RID: 1305
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private KpiValueFormat _ValueFormat;

		// Token: 0x0400051A RID: 1306
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private KpiVisualization _Visualization;

		// Token: 0x0400051B RID: 1307
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DrillthroughTarget _DrillthroughTarget;

		// Token: 0x0400051C RID: 1308
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Currency;

		// Token: 0x0400051D RID: 1309
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private KpiValues _Values;

		// Token: 0x0400051E RID: 1310
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private KpiData _Data;
	}
}
