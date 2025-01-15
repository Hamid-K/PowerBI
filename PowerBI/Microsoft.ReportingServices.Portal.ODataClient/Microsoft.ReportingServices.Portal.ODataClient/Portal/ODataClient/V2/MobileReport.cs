using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200003A RID: 58
	[Key("Id")]
	[EntitySet("MobileReports")]
	[OriginalName("MobileReport")]
	public class MobileReport : CatalogItem
	{
		// Token: 0x06000279 RID: 633 RVA: 0x000064EC File Offset: 0x000046EC
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

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600027A RID: 634 RVA: 0x00006542 File Offset: 0x00004742
		// (set) Token: 0x0600027B RID: 635 RVA: 0x0000654A File Offset: 0x0000474A
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

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000655E File Offset: 0x0000475E
		// (set) Token: 0x0600027D RID: 637 RVA: 0x00006566 File Offset: 0x00004766
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

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000657A File Offset: 0x0000477A
		// (set) Token: 0x0600027F RID: 639 RVA: 0x00006582 File Offset: 0x00004782
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

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00006596 File Offset: 0x00004796
		// (set) Token: 0x06000281 RID: 641 RVA: 0x0000659E File Offset: 0x0000479E
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

		// Token: 0x06000282 RID: 642 RVA: 0x000065B4 File Offset: 0x000047B4
		[OriginalName("Upload")]
		public DataServiceActionQuerySingle<MobileReport> Upload()
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuerySingle<MobileReport>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.Upload", Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00006618 File Offset: 0x00004818
		[OriginalName("UpdateReportDataSets")]
		public DataServiceActionQuery UpdateReportDataSets(ICollection<DataSet> DataSets)
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuery(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.UpdateReportDataSets", new BodyOperationParameter[]
			{
				new BodyOperationParameter("DataSets", DataSets)
			});
		}

		// Token: 0x04000147 RID: 327
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _AllowCaching;

		// Token: 0x04000148 RID: 328
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private MobileReportManifest _Manifest;

		// Token: 0x04000149 RID: 329
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _HasSharedDataSets;

		// Token: 0x0400014A RID: 330
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<DataSet> _SharedDataSets = new DataServiceCollection<DataSet>(null, TrackingMode.None);
	}
}
