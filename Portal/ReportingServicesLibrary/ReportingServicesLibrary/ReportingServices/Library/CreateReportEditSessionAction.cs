using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001C4 RID: 452
	internal class CreateReportEditSessionAction : CreateReportActionBase<CreateEditSessionParameters>
	{
		// Token: 0x06000FE8 RID: 4072 RVA: 0x00038845 File Offset: 0x00036A45
		public CreateReportEditSessionAction(RSService service)
			: base("CreateReportEditSessionAction", service)
		{
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06000FEA RID: 4074 RVA: 0x00005BEF File Offset: 0x00003DEF
		protected override bool UsePermanentCompiledDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x00038853 File Offset: 0x00036A53
		protected override void AddActionToBatch()
		{
			throw new InternalCatalogException("Batch not supported.");
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06000FEC RID: 4076 RVA: 0x000053DC File Offset: 0x000035DC
		protected override bool AllowVirtualItems
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x00038853 File Offset: 0x00036A53
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			throw new InternalCatalogException("Batch not supported.");
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x00038860 File Offset: 0x00036A60
		protected override void CreateItem(ProfessionalReportCatalogItem item)
		{
			RSTrace.CatalogTrace.Assert(item != null, "supported type not implementing IEditSessionAware");
			string text = ((IEditSessionAware)item).CreateEditSession();
			base.ActionParameters.EditSessionID = text;
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x00038895 File Offset: 0x00036A95
		protected override void PerformVirtualItemSecurityCheck(ProfessionalReportCatalogItem item)
		{
			if (base.ResolvedItemType != ItemType.DataSet)
			{
				Security.SafeCheckExecuteReportDefinitionPermission(base.Service, null, false);
				return;
			}
			RSTrace.CatalogTrace.Assert(WebRequestUtil.IsViaPortal(), "DataSets On Edit Session are only valid in connections initiated by the Reporting Services Portal");
			item.ThrowIfNoAccess(ReportOperation.ExecuteAndView);
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x000388CC File Offset: 0x00036ACC
		protected override void CreateOverwrite(ProfessionalReportCatalogItem item)
		{
			this.PerformVirtualItemSecurityCheck(item);
			item.ThrowIfNoAccess(CommonOperation.ReadDatasource);
			item.ThrowIfNoAccess(CommonOperation.ReadProperties);
			item.ThrowIfNoAccess(CommonOperation.UpdateProperties);
			item.ThrowIfNoAccess(ReportOperation.ReadReportDefinition);
			item.ThrowIfNoAccess(ReportOperation.UpdateReportDefinition);
			item.ThrowIfNoAccess(ReportOperation.ExecuteAndView);
			DataSourceInfoCollection dataSources = item.DataSources;
			RSTrace.CatalogTrace.Assert(dataSources != null, "prePublishDataSources");
			item.LoadStoredAndDerivedProperties();
			item.CreationDate = DateTime.Now;
			this.PrepareForNewItem(item);
			DataSourceInfoCollection dataSourceInfoCollection = dataSources.CombineOnSetDefinition(item.DataSources);
			item.DataSources = dataSourceInfoCollection;
			RSTrace.CatalogTrace.Assert(item != null, "supported type not implementing IEditSessionAware");
			string text = ((IEditSessionAware)item).CreateEditSession();
			base.ActionParameters.EditSessionID = text;
		}
	}
}
