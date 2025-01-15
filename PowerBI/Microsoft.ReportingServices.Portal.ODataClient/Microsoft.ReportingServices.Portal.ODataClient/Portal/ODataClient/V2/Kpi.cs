using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000036 RID: 54
	[Key("Id")]
	[EntitySet("Kpis")]
	[OriginalName("Kpi")]
	public class Kpi : CatalogItem
	{
		// Token: 0x06000237 RID: 567 RVA: 0x00005B04 File Offset: 0x00003D04
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

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00005B5A File Offset: 0x00003D5A
		// (set) Token: 0x06000239 RID: 569 RVA: 0x00005B62 File Offset: 0x00003D62
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

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00005B76 File Offset: 0x00003D76
		// (set) Token: 0x0600023B RID: 571 RVA: 0x00005B7E File Offset: 0x00003D7E
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

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00005B92 File Offset: 0x00003D92
		// (set) Token: 0x0600023D RID: 573 RVA: 0x00005B9A File Offset: 0x00003D9A
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

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00005BAE File Offset: 0x00003DAE
		// (set) Token: 0x0600023F RID: 575 RVA: 0x00005BB6 File Offset: 0x00003DB6
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

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00005BCA File Offset: 0x00003DCA
		// (set) Token: 0x06000241 RID: 577 RVA: 0x00005BD2 File Offset: 0x00003DD2
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

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00005BE6 File Offset: 0x00003DE6
		// (set) Token: 0x06000243 RID: 579 RVA: 0x00005BEE File Offset: 0x00003DEE
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

		// Token: 0x06000244 RID: 580 RVA: 0x00005C04 File Offset: 0x00003E04
		[OriginalName("Upload")]
		public DataServiceActionQuerySingle<Kpi> Upload()
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuerySingle<Kpi>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.Upload", Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x04000122 RID: 290
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private KpiValueFormat _ValueFormat;

		// Token: 0x04000123 RID: 291
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private KpiVisualization _Visualization;

		// Token: 0x04000124 RID: 292
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DrillthroughTarget _DrillthroughTarget;

		// Token: 0x04000125 RID: 293
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Currency;

		// Token: 0x04000126 RID: 294
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private KpiValues _Values;

		// Token: 0x04000127 RID: 295
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private KpiData _Data;
	}
}
