using System;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;

namespace Model
{
	// Token: 0x0200000B RID: 11
	internal class LinkedReportRepository : LinkedReport
	{
		// Token: 0x0600001A RID: 26 RVA: 0x0000257F File Offset: 0x0000077F
		public LinkedReportRepository(IPrincipal userPrincipal, ICatalogRepository catalogRepository)
		{
			if (userPrincipal == null)
			{
				throw new ArgumentNullException("userPrincipal");
			}
			if (catalogRepository == null)
			{
				throw new ArgumentNullException("catalogRepository");
			}
			this._userPrincipal = userPrincipal;
			this._catalogRepository = catalogRepository;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000025B1 File Offset: 0x000007B1
		protected override IList<Subscription> LoadSubscriptions()
		{
			return this._catalogRepository.GetSubscriptions(this._userPrincipal, base.Path);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000025CA File Offset: 0x000007CA
		protected override IList<ReportHistorySnapshot> LoadReportHistorySnapshots()
		{
			return this._catalogRepository.GetReportHistorySnapshots(this._userPrincipal, base.Path);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000025E3 File Offset: 0x000007E3
		protected override IList<HistorySnapshot> LoadHistorySnapshots()
		{
			return this._catalogRepository.GetHistorySnapshots(this._userPrincipal, base.Path);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000025FC File Offset: 0x000007FC
		protected override HistorySnapshotOptions LoadHistorySnapshotOptions()
		{
			ReportHistorySnapshotsOptions reportHistorySnapshotsOptions = this._catalogRepository.GetReportHistorySnapshotsOptions(this._userPrincipal, base.Path);
			return new HistorySnapshotOptions
			{
				CatalogItemId = base.Id,
				HistorySnapshotsOptions = reportHistorySnapshotsOptions
			};
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002639 File Offset: 0x00000839
		protected override IList<ReportParameterDefinition> LoadParameterDefinitions()
		{
			return this._catalogRepository.GetReportParameterDefinitionsWithQuery(this._userPrincipal, base.Path);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002652 File Offset: 0x00000852
		protected override IList<CacheRefreshPlan> LoadCacheRefreshPlans()
		{
			return this._catalogRepository.GetCacheRefreshPlans(this._userPrincipal, base.Path);
		}

		// Token: 0x0400003A RID: 58
		private readonly IPrincipal _userPrincipal;

		// Token: 0x0400003B RID: 59
		private readonly ICatalogRepository _catalogRepository;
	}
}
