using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200003E RID: 62
	[Key("Id")]
	[EntitySet("PowerBIReports")]
	[OriginalName("PowerBIReport")]
	public class PowerBIReport : CatalogItem
	{
		// Token: 0x060002A0 RID: 672 RVA: 0x00006AA8 File Offset: 0x00004CA8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static PowerBIReport CreatePowerBIReport(Guid ID, CatalogItemType type, bool hidden, long size, DateTimeOffset modifiedDate, DateTimeOffset createdDate, bool isFavorite, bool hasDataSources)
		{
			return new PowerBIReport
			{
				Id = ID,
				Type = type,
				Hidden = hidden,
				Size = size,
				ModifiedDate = modifiedDate,
				CreatedDate = createdDate,
				IsFavorite = isFavorite,
				HasDataSources = hasDataSources
			};
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00006AF6 File Offset: 0x00004CF6
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x00006AFE File Offset: 0x00004CFE
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("HasDataSources")]
		public bool HasDataSources
		{
			get
			{
				return this._HasDataSources;
			}
			set
			{
				this._HasDataSources = value;
				this.OnPropertyChanged("HasDataSources");
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00006B12 File Offset: 0x00004D12
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x00006B1A File Offset: 0x00004D1A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DataSources")]
		public DataServiceCollection<DataSource> DataSources
		{
			get
			{
				return this._DataSources;
			}
			set
			{
				this._DataSources = value;
				this.OnPropertyChanged("DataSources");
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00006B2E File Offset: 0x00004D2E
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x00006B36 File Offset: 0x00004D36
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DataModelParameters")]
		public DataServiceCollection<DataModelParameter> DataModelParameters
		{
			get
			{
				return this._DataModelParameters;
			}
			set
			{
				this._DataModelParameters = value;
				this.OnPropertyChanged("DataModelParameters");
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x00006B4A File Offset: 0x00004D4A
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x00006B52 File Offset: 0x00004D52
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("CacheRefreshPlans")]
		public DataServiceCollection<CacheRefreshPlan> CacheRefreshPlans
		{
			get
			{
				return this._CacheRefreshPlans;
			}
			set
			{
				this._CacheRefreshPlans = value;
				this.OnPropertyChanged("CacheRefreshPlans");
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00006B66 File Offset: 0x00004D66
		// (set) Token: 0x060002AA RID: 682 RVA: 0x00006B6E File Offset: 0x00004D6E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DataModelRoleAssignments")]
		public DataServiceCollection<DataModelRoleAssignment> DataModelRoleAssignments
		{
			get
			{
				return this._DataModelRoleAssignments;
			}
			set
			{
				this._DataModelRoleAssignments = value;
				this.OnPropertyChanged("DataModelRoleAssignments");
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00006B82 File Offset: 0x00004D82
		// (set) Token: 0x060002AC RID: 684 RVA: 0x00006B8A File Offset: 0x00004D8A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DataModelRoles")]
		public DataServiceCollection<DataModelRole> DataModelRoles
		{
			get
			{
				return this._DataModelRoles;
			}
			set
			{
				this._DataModelRoles = value;
				this.OnPropertyChanged("DataModelRoles");
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00006BA0 File Offset: 0x00004DA0
		[OriginalName("Upload")]
		public DataServiceActionQuerySingle<PowerBIReport> Upload()
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuerySingle<PowerBIReport>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.Upload", Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00006C04 File Offset: 0x00004E04
		[OriginalName("CheckDataSourceConnection")]
		public DataServiceActionQuerySingle<DataSourceCheckResult> CheckDataSourceConnection(string DataSourceName)
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuerySingle<DataSourceCheckResult>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.CheckDataSourceConnection", new BodyOperationParameter[]
			{
				new BodyOperationParameter("DataSourceName", DataSourceName)
			});
		}

		// Token: 0x0400015A RID: 346
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _HasDataSources;

		// Token: 0x0400015B RID: 347
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<DataSource> _DataSources = new DataServiceCollection<DataSource>(null, TrackingMode.None);

		// Token: 0x0400015C RID: 348
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<DataModelParameter> _DataModelParameters = new DataServiceCollection<DataModelParameter>(null, TrackingMode.None);

		// Token: 0x0400015D RID: 349
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<CacheRefreshPlan> _CacheRefreshPlans = new DataServiceCollection<CacheRefreshPlan>(null, TrackingMode.None);

		// Token: 0x0400015E RID: 350
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<DataModelRoleAssignment> _DataModelRoleAssignments = new DataServiceCollection<DataModelRoleAssignment>(null, TrackingMode.None);

		// Token: 0x0400015F RID: 351
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<DataModelRole> _DataModelRoles = new DataServiceCollection<DataModelRole>(null, TrackingMode.None);
	}
}
