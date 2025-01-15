using System;
using System.Data;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001CB RID: 459
	internal abstract class UpdateCacheAction<TItemType> : RSSoapAction<UpdateCacheActionParameters> where TItemType : BaseExecutableCatalogItem
	{
		// Token: 0x06001011 RID: 4113
		protected abstract void PerformItemTypeCheck(CatalogItem item);

		// Token: 0x06001012 RID: 4114
		protected abstract CancelablePhaseBase ConstructExecutionPhase(TItemType item, JobType jobType);

		// Token: 0x06001013 RID: 4115 RVA: 0x00038FB4 File Offset: 0x000371B4
		protected UpdateCacheAction(string actionName, RSService service)
			: base(actionName, service)
		{
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x00037632 File Offset: 0x00035832
		protected override IsolationLevel IsolationLevel
		{
			get
			{
				return IsolationLevel.RepeatableRead;
			}
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x00038FC0 File Offset: 0x000371C0
		internal override void PerformActionNow()
		{
			ExternalItemPath externalItemPath = new ExternalItemPath(base.ActionParameters.ItemPath);
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service);
			catalogItemContext.SetPath(externalItemPath, ItemPathOptions.Default);
			catalogItemContext.RSRequestParameters.SetReportParameters(base.ActionParameters.Parameters);
			CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
			this.PerformItemTypeCheck(catalogItem);
			TItemType titemType = catalogItem as TItemType;
			RSTrace.CatalogTrace.Assert(titemType != null, "executableItem");
			titemType.ThrowIfNoAccess(ReportOperation.Execute);
			base.Service.EnsureCachingIsEnabled(catalogItemContext);
			JobType jobType = new JobType(base.ActionParameters.JobType);
			using (CancelablePhaseBase cancelablePhaseBase = this.ConstructExecutionPhase(titemType, jobType))
			{
				cancelablePhaseBase.ExecuteWrapper();
			}
		}
	}
}
